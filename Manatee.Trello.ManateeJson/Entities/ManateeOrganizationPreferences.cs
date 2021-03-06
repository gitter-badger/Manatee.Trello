﻿/***************************************************************************************

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
 
	File Name:		ManateeOrganizationPreferences.cs
	Namespace:		Manatee.Trello.ManateeJson.Entities
	Class Name:		ManateeOrganizationPreferences
	Purpose:		Implements IJsonOrganizationPreferences for Manatee.Json.

***************************************************************************************/

using System.Collections.Generic;
using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
{
	internal class ManateeOrganizationPreferences : IJsonOrganizationPreferences, IJsonSerializable
	{
		public OrganizationPermissionLevel? PermissionLevel { get; set; }
		public List<object> OrgInviteRestrict { get; set; }
		public bool? ExternalMembersDisabled { get; set; }
		public string AssociatedDomain { get; set; }
		public IJsonBoardVisibilityRestrict BoardVisibilityRestrict { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			if (obj.ContainsKey("permissionLevel") && obj["permissionLevel"].Type == JsonValueType.Object)
				obj = obj["permissionLevel"].Object["prefs"].Object;

			PermissionLevel = obj.Deserialize<OrganizationPermissionLevel?>(serializer, "permissionLevel");
			//OrgInviteRestrict = obj.TryGetArray("orgInviteRestrict").Cast<object>().ToList();
			ExternalMembersDisabled = obj.TryGetBoolean("externalMembersDisabled");
			AssociatedDomain = obj.TryGetString("associatedDomain");
			BoardVisibilityRestrict = obj.Deserialize<IJsonBoardVisibilityRestrict>(serializer, "boardVisibilityRestrict") ?? new ManateeBoardVisibilityRestrict();
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			return null;
		}
	}
}
