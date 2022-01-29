using LibApp.Application.Core.Dtos;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace LibApp.Application.UseCases.MembershipTypes.Queries
{
    public static class GetMembershipTypes
    {
        public class Query : IRequest<IReadOnlyCollection<MembershipTypeDto>>
        {

        }

        public class Handler : IRequestHandler<Query, IReadOnlyCollection<MembershipTypeDto>>
        {
            private readonly string _endpoint = "/api/membershipTypes";
            private readonly ILogger<Handler> _logger;
            private readonly HttpClient _httpClient;

            public Handler(ILogger<Handler> logger, IHttpClientFactory httpClientFactory)
            {
                _logger = logger;
                _httpClient = httpClientFactory.CreateClient("APIClient");
            }

            public async Task<IReadOnlyCollection<MembershipTypeDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var response = await _httpClient.GetAsync($"{_endpoint}");

                    response.EnsureSuccessStatusCode();

                    var content = await response.Content.ReadAsStringAsync();
                    var genres = JsonConvert.DeserializeObject<List<MembershipTypeDto>>(content);

                    return genres.AsReadOnly();
                }
                catch(Exception exception)
                {
                    _logger.LogError(exception.Message, exception);
                    throw;
                }
            }
        }
    }

}
