using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ef_core_web_api.Repositories;

namespace ef_core_web_api.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientRepository _repository;

        public ClientsController(IClientRepository repository)
        {
            _repository = repository;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var client = await _repository.GetAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            var result = await _repository.DeleteAsync(client);

            if(result == null)
            {
                return StatusCode(409, "Client has assigned trips");
            }

            return new OkObjectResult($"Client #{result.IdClient} has been deleted from database");
        }
    }
}
