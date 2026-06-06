using System.Net.Http.Json;
using Loan.UseCases.Ports;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace Loan.Infrastructure.ExternalServices
{
    public class DaprInventoryService : IInventoryService
    {
        private readonly HttpClient _http;

        public DaprInventoryService([FromKeyedServices("inventory")] HttpClient http)
        {
            _http = http;
        }

        public async Task<bool> ReserveBookAsync(Guid loanId, Guid bookId)
        {
            try
            {
                var request = new {LoanId =  loanId, BookId = bookId };
                var response = await _http.PostAsJsonAsync("api/inventory/reserve", request);
                return response.IsSuccessStatusCode;
            }
            catch { return false; }
        }

        public async Task ReleaseBookAsync(Guid loanId, Guid bookId)
        {
            var request = new {  LoanId = loanId, BookId =bookId };
            await _http.PostAsJsonAsync("api/inventory/release", request);
        }
    }
}
