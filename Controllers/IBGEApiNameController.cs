using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class IBGENamesController : ControllerBase
{
    [HttpGet(Name = "IBGE")]
    public async Task<IActionResult> NumbersOfNames(string name)
    {
        using (HttpClient client = new HttpClient())
        {
            string apiUrl = $"https://servicodados.ibge.gov.br/api/v2/censos/nomes/{name}";

            try
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();

                    return Content(result, "application/json");
                }
                else 
                {
                    return BadRequest($"Error! {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error! {ex.Message}");
            }
        }
    }
}