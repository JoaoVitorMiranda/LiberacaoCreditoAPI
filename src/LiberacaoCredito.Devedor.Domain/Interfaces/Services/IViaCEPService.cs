using System.Threading.Tasks;
using LiberacaoCredito.Devedor.Domain.Models.Services;

namespace LiberacaoCredito.Devedor.Domain.Interfaces.Services
{
    public interface IViaCEPService
    {
        Task<ViaCEP> GetByCEPAsync(string cep);
    }
}
