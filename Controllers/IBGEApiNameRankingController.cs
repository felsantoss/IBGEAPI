using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class IBGENameRankingController : ControllerBase
{
    [HttpGet(Name = "RankingName")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RankingOfNames()
    {
        using (HttpClient client = new HttpClient())
        {
            string apiUrl = "https://servicodados.ibge.gov.br/api/v2/censos/nomes/ranking";

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