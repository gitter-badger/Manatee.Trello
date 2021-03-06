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
 
	File Name:		ManateeImagePreview.cs
	Namespace:		Manatee.Trello.ManateeJson.Entities
	Class Name:		ManateeImagePreview
	Purpose:		Implements IJsonImagePreview for Manatee.Json.

***************************************************************************************/

using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
{
	internal class ManateeImagePreview : IJsonImagePreview, IJsonSerializable
	{
		public int? Width { get; set; }
		public int? Height { get; set; }
		public string Url { get; set; }
		public string Id { get; set; }
		public bool? Scaled { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Width = (int?) obj.TryGetNumber("width");
			Height = (int?)obj.TryGetNumber("height");
			Url = obj.TryGetString("url");
			Id = obj.TryGetString("id");
			Scaled = obj.TryGetBoolean("scaled");
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			return null;
		}
	}
}