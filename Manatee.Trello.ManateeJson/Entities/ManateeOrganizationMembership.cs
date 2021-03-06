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
 
	File Name:		ManateeOrganizationMembership.cs
	Namespace:		Manatee.Trello.ManateeJson.Entities
	Class Name:		ManateeOrganizationMembership
	Purpose:		Implements IJsonOrganizationMembership for Manatee.Json.

***************************************************************************************/

using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
{
	internal class ManateeOrganizationMembership : IJsonOrganizationMembership, IJsonSerializable
	{
		public string Id { get; set; }
		public IJsonMember Member { get; set; }
		public OrganizationMembershipType? MemberType { get; set; }
		public bool? Unconfirmed { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Id = obj.TryGetString("id");
			Member = obj.Deserialize<IJsonMember>(serializer, "idMember");
			MemberType = obj.Deserialize<OrganizationMembershipType?>(serializer, "memberType");
			Unconfirmed = obj.TryGetBoolean("unconfirmed");
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			var json = new JsonObject();
			Id.Serialize(json, serializer, "id");
			Member.SerializeId(json, "idMember");
			MemberType.Serialize(json, serializer, "type");
			return json;
		}
	}
}