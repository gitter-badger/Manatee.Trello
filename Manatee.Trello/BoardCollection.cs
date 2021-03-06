﻿/***************************************************************************************

	Copyright 2015 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		BoardCollection.cs
	Namespace:		Manatee.Trello
	Class Name:		ReadOnlyBoardCollection, BoardCollection
	Purpose:		Collection objects for boards.

***************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Exceptions;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// A read-only collectin of boards.
	/// </summary>
	public class ReadOnlyBoardCollection : ReadOnlyCollection<Board>
	{
		private readonly EntityRequestType _updateRequestType;
		private Dictionary<string, object> _additionalParameters;

		/// <summary>
		/// Retrieves a board which matches the supplied key.
		/// </summary>
		/// <param name="key">The key to match.</param>
		/// <returns>The matching board, or null if none found.</returns>
		/// <remarks>
		/// Matches on Board.Id and Board.Name.  Comparison is case-sensitive.
		/// </remarks>
		public Board this[string key] => GetByKey(key);

		internal ReadOnlyBoardCollection(Type type, Func<string> getOwnerId, TrelloAuthorization auth)
			: base(getOwnerId, auth)
		{
			_updateRequestType = type == typeof (Organization)
				                     ? EntityRequestType.Organization_Read_Boards
				                     : EntityRequestType.Member_Read_Boards;
		}
		internal ReadOnlyBoardCollection(ReadOnlyBoardCollection source, TrelloAuthorization auth)
			: base(() => source.OwnerId, auth)
		{
			_updateRequestType = source._updateRequestType;
			if (source._additionalParameters != null)
				_additionalParameters = new Dictionary<string, object>(source._additionalParameters);
		}

		/// <summary>
		/// Implement to provide data to the collection.
		/// </summary>
		protected override sealed void Update()
		{
			IncorporateLimit(_additionalParameters);

			var endpoint = EndpointFactory.Build(_updateRequestType, new Dictionary<string, object> { { "_id", OwnerId } });
			var newData = JsonRepository.Execute<List<IJsonBoard>>(Auth, endpoint, _additionalParameters);

			Items.Clear();
			Items.AddRange(newData.Select(jb =>
				{
					var board = jb.GetFromCache<Board>(Auth);
					board.Json = jb;
					return board;
				}));
		}

		internal void SetFilter(BoardFilter cardStatus)
		{
			if (_additionalParameters == null)
				_additionalParameters = new Dictionary<string, object>();
			_additionalParameters["filter"] = cardStatus.GetDescription();
		}

		private Board GetByKey(string key)
		{
			return this.FirstOrDefault(b => key.In(b.Id, b.Name));
		}
	}

	/// <summary>
	/// A collection of boards.
	/// </summary>
	public class BoardCollection : ReadOnlyBoardCollection
	{
		private readonly EntityRequestType _addRequestType;

		internal BoardCollection(Type type, Func<string> getOwnerId, TrelloAuthorization auth)
			: base(type, getOwnerId, auth)
		{
			_addRequestType = type == typeof (Organization)
				                  ? EntityRequestType.Organization_Write_CreateBoard
				                  : EntityRequestType.Member_Write_CreateBoard;
		}

		/// <summary>
		/// Creates a new board.
		/// </summary>
		/// <param name="name">The name of the board to create.</param>
		/// <param name="source">A board to use as a template.</param>
		/// <returns>The <see cref="Board"/> generated by Trello.</returns>
		public Board Add(string name, Board source = null)
		{
			var error = NotNullOrWhiteSpaceRule.Instance.Validate(null, name);
			if (error != null)
				throw new ValidationException<string>(name, new[] {error});

			var json = TrelloConfiguration.JsonFactory.Create<IJsonBoard>();
			json.Name = name;
			if (source != null)
				json.BoardSource = source.Json;
			if (_addRequestType == EntityRequestType.Organization_Write_CreateBoard)
			{
				json.Organization = TrelloConfiguration.JsonFactory.Create<IJsonOrganization>();
				json.Organization.Id = OwnerId;
			}

			var endpoint = EndpointFactory.Build(_addRequestType);
			var newData = JsonRepository.Execute(Auth, endpoint, json);

			return new Board(newData, Auth);
		}
	}
}