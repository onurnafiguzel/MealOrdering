using MealOrdering.Shared.CustomExceptions;
using MealOrdering.Shared.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MealOrdering.Client.Utils
{
    public static class HttpClientExtension
    {
        public async static Task<T> GetServiceResponse<T>(this HttpClient httpClient, string url, bool ThrowWhenNotSuccess = false)
        {
            
            var httpRes = await httpClient.GetFromJsonAsync<ServiceResponse<T>>(url);

            if (!httpRes.Success && ThrowWhenNotSuccess)
                throw new ApiException(httpRes.Message);

            return httpRes.Value;
        }
    }
}
