using LibApp.Application.Core.Dtos;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace LibApp.Application.UseCases.Customers.Queries
{
    public static class GetCustomer
    {
        public class Query : IRequest<CustomerDto>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, CustomerDto>
        {
            private readonly string _endpoint = "/api/customers";
            private readonly ILogger<Handler> _logger;
            private readonly HttpClient _httpClient;

            public Handler(ILogger<Handler> logger, IHttpClientFactory httpClientFactory)
            {
                _logger = logger;
                _httpClient = httpClientFactory.CreateClient("APIClient");
            }

            public async Task<CustomerDto> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var response = await _httpClient.GetAsync($"{_endpoint}/{request.Id}");

                    response.EnsureSuccessStatusCode();

                    var content = await response.Content.ReadAsStringAsync();
                    var customer = JsonConvert.DeserializeObject<CustomerDto>(content);

                    return customer;
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message, exception);
                    throw;
                }
            }
        }
    }
}
