using BigDataUFRJ.API.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BigDataUFRJ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessosController : ControllerBase
    {
        private readonly IMongoCollection<ProcessoJudicial> _processos;

        public ProcessosController(ProcessosDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _processos = database.GetCollection<ProcessoJudicial>(settings.CollectionName);

        }

        [HttpGet("{numeroProcesso}")]
        public async Task<IActionResult> Get(string numeroProcesso)
        {
            var processo = await _processos.Find(processo => processo.NumJustica == numeroProcesso).FirstOrDefaultAsync();
            if (processo == null) return NotFound("Processo não encontrado");
            return Ok(processo);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] string search)
        {
            if (search == null) search = string.Empty;

            var builder = Builders<ProcessoJudicial>.Filter;
            var processos = await _processos.Find(
                builder.Where(p => string.IsNullOrEmpty(search)) |
                builder.Where(p => p.NumJustica.ToLower().StartsWith(search.ToLower()))
            ).ToListAsync();

            return Ok(processos.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProcessoJudicialCreateDTO dto)
        {
            if (string.IsNullOrEmpty(dto.NumJustica)) return BadRequest("Número do processo é obrigatório");

            var processoJaExiste = await _processos.Find(processo => processo.NumJustica == dto.NumJustica).FirstOrDefaultAsync();
            if (processoJaExiste != null) return BadRequest("Já existe um processo cadastrado com esse número");

            var processo = dto.ToModel();
            processo.CreatedAt = DateTime.Now;
            processo.UpdatedAt = DateTime.Now;

            await _processos.InsertOneAsync(processo);
            return CreatedAtAction(nameof(Get), new { numeroProcesso = processo.NumJustica }, processo);
        }

        [HttpPut("{numeroProcesso}")]
        public async Task<IActionResult> Update(string numeroProcesso, [FromBody] ProcessoJudicialUpdateDTO dto)
        {
            var processo = await _processos.Find(processo => processo.NumJustica == numeroProcesso).FirstOrDefaultAsync();
            if (processo == null) return NotFound("Processo não encontrado");

            var update = Builders<ProcessoJudicial>.Update.Set(p => p.UpdatedAt, DateTime.Now);
            foreach (var prop in dto.GetType().GetProperties())
            {
                if (prop.GetValue(dto) != default)
                {
                    update = update.Set(prop.Name, prop.GetValue(dto));
                }
            }

            await _processos.UpdateOneAsync(Builders<ProcessoJudicial>.Filter.Eq(p => p.NumJustica, numeroProcesso), update);
            var processoUpdated = await _processos.Find(processo => processo.NumJustica == numeroProcesso).FirstOrDefaultAsync();

            return Ok(processoUpdated);
        }

        [HttpDelete("{numeroProcesso}")]
        public async Task<IActionResult> Delete(string numeroProcesso)
        {
            var processo = await _processos.Find(processo => processo.NumJustica == numeroProcesso).FirstOrDefaultAsync();
            if (processo == null) return NotFound("Processo não encontrado");

            await _processos.DeleteOneAsync(Builders<ProcessoJudicial>.Filter.Eq(p => p.NumJustica, numeroProcesso));
            return NoContent();
        }
    }

    public class ProcessosDBSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
    }
}
