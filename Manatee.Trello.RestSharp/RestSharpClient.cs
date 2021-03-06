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
 
	File Name:		RestSharpClient.cs
	Namespace:		Manatee.Trello.Rest
	Class Name:		RestSharpClient<T>
	Purpose:		A RestSharp client which implemements IRestClient.

***************************************************************************************/

using System.Net;
using Manatee.Trello.Contracts;
using RestSharp;
using IRestClient = Manatee.Trello.Rest.IRestClient;
using IRestRequest = Manatee.Trello.Rest.IRestRequest;
using IRestResponse = Manatee.Trello.Rest.IRestResponse;

namespace Manatee.Trello.RestSharp
{
	internal class RestSharpClient : RestClient, IRestClient
	{
		private readonly ILog _log;
		private readonly global::RestSharp.Deserializers.IDeserializer _deserializer;

		public RestSharpClient(ILog log, global::RestSharp.Deserializers.IDeserializer deserializer, string apiBaseUrl)
			: base(apiBaseUrl)
		{
			_log = log;
			_deserializer = deserializer;
			ClearHandlers();
			AddHandler("application/json", _deserializer);
		}

		public IRestResponse Execute(IRestRequest request)
		{
			var restSharpRequest = (RestSharpRequest)request;
			var restSharpResponse = base.Execute(restSharpRequest);
			ValidateResponse(restSharpResponse);
			return new RestSharpResponse(restSharpResponse);
		}

		public Rest.IRestResponse<T> Execute<T>(IRestRequest request)
			where T : class
		{
			var restSharpRequest = (RestSharpRequest) request;
			var restSharpResponse = base.Execute(restSharpRequest);
			ValidateResponse(restSharpResponse);
			var data = _deserializer.Deserialize<T>(restSharpResponse);
			return new RestSharpResponse<T>(restSharpResponse, data);
		}

		private void ValidateResponse(global::RestSharp.IRestResponse response)
		{
			if (response == null)
				_log.Error(new WebException("Received null response from Trello."));
			if (response.StatusCode != HttpStatusCode.OK)
				_log.Error(new WebException($"Trello returned with an error.\nError Message: {response.ErrorMessage}\nContent: {response.Content}",
				                            response.ErrorException));
		}
	}
}