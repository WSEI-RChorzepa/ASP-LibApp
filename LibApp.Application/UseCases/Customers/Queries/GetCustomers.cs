using LibApp.Application.Core.Dtos;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace LibApp.Application.UseCases.Customers.Queries
{
    public static class GetCustomers
    {
        public class Query : IRequest<IReadOnlyCollection<CustomerDto>>
        {

        }

        public class Handler : IRequestHandler<Query, IReadOnlyCollection<CustomerDto>>
        {
            private readonly string _endpoint = "/api/customers";
            private readonly ILogger<Handler> _logger;
            private readonly HttpClient _httpClient;

            public Handler(ILogger<Handler> logger, IHttpClientFactory httpClientFactory)
            {
                _logger = logger;
                _httpClient = httpClientFactory.CreateClient("APIClient");
            }

            public async Task<IReadOnlyCollection<CustomerDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var response = await _httpClient.GetAsync($"{_endpoint}");

                    response.EnsureSuccessStatusCode();

                    var content = await response.Content.ReadAsStringAsync();
                    var customers = JsonConvert.DeserializeObject<List<CustomerDto>>(content);

                    return customers.AsReadOnly();

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
