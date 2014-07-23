/***************************************************************************************

	Copyright 2014 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		OrganizationCollection.cs
	Namespace:		Manatee.Trello
	Class Name:		ReadOnlyOrganizationCollection, OrganizationCollection
	Purpose:		Collection objects for organizations.

***************************************************************************************/
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// A read-only collection of organizations.
	/// </summary>
	public class ReadOnlyOrganizationCollection : ReadOnlyCollection<Organization>
	{
		internal ReadOnlyOrganizationCollection(string ownerId)
			: base(ownerId) { }

		/// <summary>
		/// Implement to provide data to the collection.
		/// </summary>
		protected override sealed void Update()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Member_Read_Organizations, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = JsonRepository.Execute<List<IJsonOrganization>>(TrelloAuthorization.Default, endpoint);

			Items.Clear();
			Items.AddRange(newData.Select(jc => jc.GetFromCache<Organization>()));
		}
	}

	/// <summary>
	/// A collection of organizations.
	/// </summary>
	public class OrganizationCollection : ReadOnlyOrganizationCollection
	{
		internal OrganizationCollection(string ownerId)
			: base(ownerId) {}

		/// <summary>
		/// Creates a new organization.
		/// </summary>
		/// <param name="name">The name of the organization to add.</param>
		/// <returns>The <see cref="Organization"/> generated by Trello.</returns>
		public Organization Add(string name)
		{
			var json = TrelloConfiguration.JsonFactory.Create<IJsonOrganization>();
			json.Name = name;

			var endpoint = EndpointFactory.Build(EntityRequestType.Member_Write_CreateOrganization);
			var newData = JsonRepository.Execute(TrelloAuthorization.Default, endpoint, json);

			return new Organization(newData, true);
		}
	}
}