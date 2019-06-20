﻿using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace ChallongeApi.Responses
{
    [PublicAPI]
    public interface IChallongeResponse
    {
        HttpStatusCode StatusCode { get; }
    }

    /// <summary>
    /// The base class that represents a response from the Challonge API.
    /// </summary>
    [PublicAPI]
    public abstract class ChallongeResponse : IChallongeResponse
    {
        public HttpStatusCode StatusCode { get; internal set; }

        [PublicAPI]
        public static async Task<T> FromHttpResponseMessageAsync<T>(HttpResponseMessage responseMessage)
            where T : ChallongeResponse
        {
            string jsonResponse = await responseMessage.Content.ReadAsStringAsync();

            var challongeResponse = JsonConvert.DeserializeObject<T>(
                jsonResponse,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

            challongeResponse.StatusCode = responseMessage.StatusCode;

            return challongeResponse;
        }
    }
}
