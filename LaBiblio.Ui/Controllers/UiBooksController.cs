using Microsoft.AspNetCore.Mvc;

namespace LaBiblio.Ui.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UiBooksController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UiBooksController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        public async Task<IActionResult> OpretBog([FromBody] BookModel model)
        {
            var httpClient = _httpClientFactory.CreateClient("catalog");
            var response = await httpClient.PostAsJsonAsync("/api/books", new
            {
                Name = model.Name,
                Author = model.Author
            });

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode(500, "Fejl ved oprettelse af bog i Catalog Microservice");
            }

            return Ok();
        }
    }

    public class BookModel
    {
        public string Name { get; set; }
        public string Author { get; set; }
    }
}
