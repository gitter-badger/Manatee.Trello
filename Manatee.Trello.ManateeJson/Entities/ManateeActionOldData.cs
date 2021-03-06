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
 
	File Name:		ManateeActionOldData.cs
	Namespace:		Manatee.Trello.ManateeJson.Entities
	Class Name:		ManateeActionOldData
	Purpose:		Implements IJsonActionOldData for Manatee.Json.

***************************************************************************************/
using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
{
	internal class ManateeActionOldData : IJsonActionOldData, IJsonSerializable
	{
		public string Desc { get; set; }
		public IJsonList List { get; set; }
		public double? Pos { get; set; }
		public string Text { get; set; }
		public bool? Closed { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Desc = obj.TryGetString("desc");
			List = obj.Deserialize<IJsonList>(serializer, "list");
			Text = obj.TryGetString("text");
			Pos = obj.TryGetNumber("pos");
			Closed = obj.TryGetBoolean("closed");
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			return null;
		}
	}
}