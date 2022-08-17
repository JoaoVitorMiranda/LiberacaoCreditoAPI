using Dapper;
using LiberacaoCredito.Devedor.Domain.Interfaces.Repository;
using LiberacaoCredito.Devedor.Infra.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LiberacaoCredito.Devedor.Infra.Repository
{
    public class CreditoRepository : EntityBaseRepository<Domain.Models.Database.Credito>, ICreditoRepository
    {
        private readonly DapperContext _dapperContext;

        public CreditoRepository(EntityContext context, DapperContext dapperContext)
            : base(context)
        {
            _dapperContext = dapperContext;
        }

        public async Task<IEnumerable<Domain.Models.Database.Credito>> GetAllAsync()
        {
            var query = @"SELECT * FROM Devedor";
            return await _dapperContext.DapperConnection.QueryAsync<Domain.Models.Database.Credito>(query, null, null, null, null);
        }

        public async Task<IEnumerable<Domain.Models.Database.Credito>> GetAllByCreditoId(int id)
        {
            var query = @"SELECT * FROM Credito WHERE DevedorId = @idDevedor";

            return await _dapperContext.DapperConnection.QueryAsync<Domain.Models.Database.Credito>(query, new { id = id });
        }

        public async Task<Domain.Models.Database.Credito> GetById(int id)
        {
            string query = $@"SELECT * FROM Credito WHERE Id = @Id";

            Domain.Models.Database.Credito parcela = await _dapperContext.DapperConnection.QueryFirstOrDefaultAsync<Domain.Models.Database.Credito>(query, new { Id = id });

            return parcela;
        }
    }
}
