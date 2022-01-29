using LibApp.Application.Core.Dtos;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LibApp.Application.UseCases.Books.Queries
{
    public static class GetBooks
    {
        public class Query : IRequest<IReadOnlyCollection<BookDto>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, IReadOnlyCollection<BookDto>>
        {
            private readonly string _endpoint = "/api/books";
            private readonly ILogger<Handler> _logger;
            private readonly HttpClient _httpClient;

            public Handler(ILogger<Handler> logger, IHttpClientFactory httpClientFactory)
            {
                _logger = logger;
                _httpClient = httpClientFactory.CreateClient("APIClient");
            }

            public async Task<IReadOnlyCollection<BookDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var response = await _httpClient.GetAsync($"{_endpoint}");

                    response.EnsureSuccessStatusCode();

                    var content = await response.Content.ReadAsStringAsync();
                    var books = JsonConvert.DeserializeObject<List<BookDto>>(content);

                    return books.AsReadOnly();
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
