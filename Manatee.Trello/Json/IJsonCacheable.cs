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
 
	File Name:		IJsonCacheable.cs
	Namespace:		Manatee.Trello.Json
	Class Name:		IJsonCacheable
	Purpose:		Defines properties required for TrelloService to cache an item.

***************************************************************************************/
namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines properties required for TrelloService to cache an item.
	/// </summary>
	public interface IJsonCacheable
	{
		/// <summary>
		/// Gets or sets a unique identifier (not necessarily a GUID).
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize(IsRequired = true)]
		string Id { get; set; }
	}
}