using LibApp.Application.Core.Dtos;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace LibApp.Application.UseCases.Genres.Queries
{
    public static class GetGenres
    {
        public class Query : IRequest<IReadOnlyCollection<GenreDto>>
        {

        }

        public class Handler : IRequestHandler<Query, IReadOnlyCollection<GenreDto>>
        {
            private readonly string _endpoint = "/api/genres";
            private readonly ILogger<Handler> _logger;
            private readonly HttpClient _httpClient;

            public Handler(ILogger<Handler> logger, IHttpClientFactory httpClientFactory)
            {
                _logger = logger;
                _httpClient = httpClientFactory.CreateClient("APIClient");
            }

            public async Task<IReadOnlyCollection<GenreDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var response = await _httpClient.GetAsync($"{_endpoint}");

                    response.EnsureSuccessStatusCode();

                    var content = await response.Content.ReadAsStringAsync();
                    var genres = JsonConvert.DeserializeObject<List<GenreDto>>(content);

                    return genres.AsReadOnly();
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
