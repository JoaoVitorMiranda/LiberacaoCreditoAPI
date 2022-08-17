using System.Collections.Generic;
using System.Threading.Tasks;


namespace LiberacaoCredito.Devedor.Domain.Interfaces.Services
{
    public interface ICreditoService : IServiceBase
    {
        Task<IEnumerable<Models.Credito.CreditoModel>> GetAllAsync();

        Task<Models.Credito.CreditoModel> Post(Models.Credito.CreditoModel model);

        Task<IEnumerable<Models.Credito.CreditoModel>> GetAllByCreditoId(int id);

        Task<Models.Credito.CreditoModel> GetById(int id);
    }
}
