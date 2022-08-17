using System.Collections.Generic;
using System.Threading.Tasks;
using LiberacaoCredito.Devedor.Domain.Models;
using LiberacaoCredito.Devedor.Domain.Models.Dapper;

namespace LiberacaoCredito.Devedor.Domain.Interfaces.Repository
{
    public interface ICustomerRepository : IEntityBaseRepository<Customer>, IDapperReadRepository<Customer>
    {
        Task<IEnumerable<CustomerAddress>> GetAllAsync();
        Task<CustomerAddress> GetAddressByIdAsync(int id);
        Task<CustomerAddress> GetByNameAsync(string name);
    }
}
