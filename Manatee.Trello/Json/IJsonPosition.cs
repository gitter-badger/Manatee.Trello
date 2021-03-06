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
 
	File Name:		IJsonPosition.cs
	Namespace:		Manatee.Trello.Json
	Class Name:		IJsonPosition
	Purpose:		Defines the JSON structure for the Position object.

***************************************************************************************/

namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the Position object.
	/// </summary>
	public interface IJsonPosition
	{
		/// <summary>
		/// Gets or sets an explicit numeric value for the position.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		double? Explicit { get; set; }
		/// <summary>
		/// Gets or sets a named value for the position.
		/// </summary>
		/// <remarks>
		/// Valid values are "top" and "bottom".
		/// </remarks>
		[JsonDeserialize]
		[JsonSerialize]
		string Named { get; set; }
	}
}
