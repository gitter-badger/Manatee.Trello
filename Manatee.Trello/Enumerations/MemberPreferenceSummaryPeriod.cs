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
 
	File Name:		MemberPreferenceSummaryPeriod.cs
	Namespace:		Manatee.Trello
	Class Name:		MemberPreferenceSummaryPeriod
	Purpose:		Enumerates the accepted values for the MinutesBetweenSummaries
					property on the MemberPreferences object.

***************************************************************************************/

using System.ComponentModel;

namespace Manatee.Trello
{
	/// <summary>
	/// Enumerates the accepted values for the MinutesBetweenSummaries property on the
	/// MemberPreferences object.
	/// </summary>
	public enum MemberPreferenceSummaryPeriod
	{
		/// <summary>
		/// Indicates that summary emails are disabled.
		/// </summary>
		[Description("disabled")]
		Disabled = -1,
		/// <summary>
		/// Indicates that summary emails should be sent every minute, when notifications
		/// are present.
		/// </summary>
		[Description("oneMinute")]
		OneMinute = 1,
		/// <summary>
		/// Indicates that summary emails should be sent every hour, when notifications
		/// are present.
		/// </summary>
		[Description("oneHour")]
		OneHour = 60
	}
}