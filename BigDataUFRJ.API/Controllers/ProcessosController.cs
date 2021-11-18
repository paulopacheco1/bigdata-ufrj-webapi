using BigDataUFRJ.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BigDataUFRJ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessosController : ControllerBase
    {

        [HttpGet("{id}")]
        public ProcessoJudicial Get(int id)
        {
            return new ProcessoJudicial();
        }

        [HttpGet]
        public ProcessoJudicial Get([FromQuery] string search)
        {
            return new ProcessoJudicial();
        }
    }
}
