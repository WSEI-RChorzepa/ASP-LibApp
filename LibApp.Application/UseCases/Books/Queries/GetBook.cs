using LibApp.Application.Core.Dtos;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace LibApp.Application.UseCases.Books.Queries
{
    public static class GetBook
    {
        public class Query : IRequest<BookDto>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, BookDto>
        {
            private readonly string _endpoint = "/api/books";
            private readonly ILogger<Handler> _logger;
            private readonly HttpClient _httpClient;

            public Handler(ILogger<Handler> logger, IHttpClientFactory httpClientFactory)
            {
                _logger = logger;
                _httpClient = httpClientFactory.CreateClient("APIClient");
            }

            public async Task<BookDto> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var response = await _httpClient.GetAsync($"{_endpoint}/{request.Id}");

                    response.EnsureSuccessStatusCode();

                    var content = await response.Content.ReadAsStringAsync();
                    var book = JsonConvert.DeserializeObject<BookDto>(content);

                    return book;
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
