using System.Collections.Generic;
using System.Threading.Tasks;


namespace LiberacaoCredito.Devedor.Domain.Interfaces.Repository
{
    public interface ICreditoRepository : IEntityBaseRepository<Models.Database.Credito>
    {
        Task<IEnumerable<Models.Database.Credito>> GetAllAsync();

        Task<IEnumerable<Models.Database.Credito>> GetAllByCreditoId(int id);

        Task<Models.Database.Credito> GetById(int id);
    }
}
