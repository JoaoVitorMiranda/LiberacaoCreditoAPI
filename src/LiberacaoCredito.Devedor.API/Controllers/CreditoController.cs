using LiberacaoCredito.Devedor.Domain.Interfaces.Services;
using LiberacaoCredito.Devedor.Domain.Models.Credito;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LiberacaoCredito.Devedor.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/v1/Devedor")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class CreditoController : BaseController
    {
        private readonly ICreditoService _devedorService;

        public CreditoController(ICreditoService devedorService)
        {
            _devedorService = devedorService;
        }

        /// <summary>
        /// Retorna uma lista de devedores.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CreditoModel>>> GetAllAsync()
        {
            return CreaterResponse(_devedorService, await _devedorService.GetAllAsync());
        }

        /// <summary>
        /// Cadastro de devedores.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CreditoModel>> Post([FromBody] CreditoModel request)
        {
            return CreaterResponse(_devedorService, await _devedorService.Post(request));
        }
    }
}
