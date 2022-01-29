using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace LibApp.Application.UseCases.Books.Commands
{
    public static class RemoveBook
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly string _endpoint = "/api/books";
            private readonly ILogger<Handler> _logger;
            private readonly HttpClient _httpClient;

            public Handler(ILogger<Handler> logger, IHttpClientFactory httpClientFactory)
            {
                _logger = logger;
                _httpClient = httpClientFactory.CreateClient("APIClient");
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var response = await _httpClient.DeleteAsync(_endpoint + $"/{request.Id}");

                    response.EnsureSuccessStatusCode();

                    return Unit.Value;
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
