/***************************************************************************************

	Copyright 2013 Little Crab Solutions

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		IJsonToken.cs
	Namespace:		Manatee.Trello.Json
	Class Name:		IJsonToken
	Purpose:		Defines the JSON structure for the Token object.

***************************************************************************************/
using System;
using System.Collections.Generic;

namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the Token object.
	/// </summary>
	public interface IJsonToken : IJsonCacheable
	{
		/// <summary>
		/// Gets or sets the identifier of the application which requested the token.
		/// </summary>
		[JsonDeserialize]
		string Identifier { get; set; }
		/// <summary>
		/// Gets or sets the ID of the member who issued the token.
		/// </summary>
		[JsonDeserialize]
		IJsonMember Member { get; set; }
		/// <summary>
		/// Gets or sets the date the token was created.
		/// </summary>
		[JsonDeserialize]
		DateTime? DateCreated { get; set; }
		/// <summary>
		/// Gets or sets the date the token will expire, if any.
		/// </summary>
		[JsonDeserialize]
		DateTime? DateExpires { get; set; }
		/// <summary>
		/// Gets or sets the collection of permissions granted by the token.
		/// </summary>
		[JsonDeserialize]
		List<IJsonTokenPermission> Permissions { get; set; }
	}
}