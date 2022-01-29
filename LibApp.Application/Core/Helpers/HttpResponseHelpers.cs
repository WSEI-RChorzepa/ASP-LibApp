using LibApp.Application.Core.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace LibApp.Application.Core.Helpers
{
    public static class HttpResponseHelpers
    {
        public static List<ValidationError> MapValidationErrors(HttpResponseMessage responseMessage)
        {
            var httpErrorObject = responseMessage.Content.ReadAsStringAsync().Result;

            var deserializedErrorObject = JsonConvert.DeserializeAnonymousType(httpErrorObject, new
            {
                message = "",
                ModelState = new Dictionary<string, string[]>()
            });

            return deserializedErrorObject.ModelState != null
                ? deserializedErrorObject
                    .ModelState
                    .Select(x => new ValidationError 
                    {
                        Property = x.Key,
                        Message = string.Join("", x.Value)
                    })
                    .ToList()
                : new List<ValidationError>();

        }
    }
}
