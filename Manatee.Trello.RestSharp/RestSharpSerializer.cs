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
 
	File Name:		RestSharpSerializer.cs
	Namespace:		Manatee.Trello.Rest
	Class Name:		RestSharpSerializer
	Purpose:		Wraps an ISerializer implementation in RestSharp's ISerializer.

***************************************************************************************/

using RestSharp.Serializers;

namespace Manatee.Trello.RestSharp
{
	internal class RestSharpSerializer : ISerializer
	{
		private readonly Json.ISerializer _inner;

		public string RootElement { get; set; }
		public string Namespace { get; set; }
		public string DateFormat { get; set; }
		public string ContentType { get; set; }

		public RestSharpSerializer(Json.ISerializer inner)
		{
			_inner = inner;
		}

		public string Serialize(object obj)
		{
			return _inner.Serialize(obj);
		}
	}
}
