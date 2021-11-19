using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CasaDoCodigo.RelatorioWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelatorioController : ControllerBase
    {
        private static readonly List<string> Relatorio =
            new List<string>()
            {
                "Primeiro pedido",
                "Segundo pedido"
            };

        // GET: api/Relatorio
        [HttpGet]
        public string Get()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in Relatorio)
            {
                stringBuilder.AppendLine(item);
            }

            return stringBuilder.ToString();
        }

        // POST: api/Relatorio
        [HttpPost]
        public void Post([FromBody] string value)
        {
            Relatorio.Add(value);
        }
    }
}
