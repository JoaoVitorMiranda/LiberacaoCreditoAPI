using AutoMapper;
using LiberacaoCredito.Devedor.Domain.Interfaces.Repository;
using LiberacaoCredito.Devedor.Domain.Interfaces.Services;
using LiberacaoCredito.Devedor.Domain.Interfaces.UoW;
using LiberacaoCredito.Devedor.Domain.Models.Credito;
using LiberacaoCredito.Devedor.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LiberacaoCredito.Devedor.API.Services
{
    public class CreditoService : ServiceBase, ICreditoService
    {

        private readonly ICreditoRepository _creditoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public CreditoService(ICreditoRepository creditoRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _creditoRepository = creditoRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CreditoModel>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<CreditoModel>>(await _creditoRepository.GetAllAsync());
        }

        public async Task<IEnumerable<CreditoModel>> GetAllByCreditoId(int id)
        {
            return _mapper.Map<IEnumerable<CreditoModel>>(await _creditoRepository.GetAllByCreditoId(id));
        }

        public async Task<CreditoModel> GetById(int id)
        {
            return _mapper.Map<CreditoModel>(await _creditoRepository.GetById(id));
        }

        public async Task<CreditoModel> Post(CreditoModel model)
        {
            Domain.Models.Database.Credito dbCredito = _mapper.Map<Domain.Models.Database.Credito>(model);
            var creditoModel = _mapper.Map<CreditoModel>(dbCredito);
            int taxa = 0;

            switch (model.Tipo)
            {
                case TipoCredito.Tipo.Direto:
                    taxa = 2;
                    break;
                case TipoCredito.Tipo.Consignado:
                    taxa = 1;
                    break;
                case TipoCredito.Tipo.PessoaJuridica:
                    taxa = 5;
                    break;
                case TipoCredito.Tipo.PessoaFisica:
                    taxa = 3;
                    break;
                case TipoCredito.Tipo.Imobiliário:
                    taxa = 9;
                    break;
            }

            try
            {
                #region Validações
                //O valor máximo a ser lib erad o para qualquer tipo de empréstimo é de R$ 1.000.000,00
                if (model.Valor > 1000000)
                {
                    throw new ArgumentException("O valor máximo a ser lib erad o para qualquer tipo de empréstimo é de R$ 1.000.000,00");
                }

                //A quantidade mínima de parce las é de 5x e a máxima é de 72x
                if (model.QtdParcelas < 5 || model.QtdParcelas > 75)
                {
                    throw new ArgumentException("A quantidade mínima de parce las é de 5x e a máxima é de 72x");
                }

                //Para o crédito de pessoa jurídica , o valor mínimo a ser liberado é de R$ 15.000,00
                if (model.Tipo == TipoCredito.Tipo.PessoaJuridica && model.Valor < 15000)
                {
                    throw new ArgumentException("Para o crédito de pessoa jurídica , o valor mínimo a ser liberado é de R$ 15.000,00");
                }

                //A data do primeiro vencimento sempre será no mí ni mo 15 dias e no máximo 40 dias a partir da data atual
                var diferencaData = (int)model.Vencimento.Subtract(DateTime.Today).TotalDays;
                if (diferencaData > 40 || diferencaData < 15)
                {
                    throw new ArgumentException("A data do primeiro vencimento sempre será no mí ni mo 15 dias e no máximo 40 dias a partir da data atual");
                }
                #endregion

                _creditoRepository.Add(dbCredito);
                _unitOfWork.Commit();

                creditoModel.Status = TipoCredito.Status.Aprovado;

                var porcentagem = ((double)taxa / 100) * (double)model.Valor;
                creditoModel.Valor = creditoModel.Valor + (decimal)porcentagem;

            }
            catch (Exception ex)
            {
                creditoModel.Status = TipoCredito.Status.Recusado;
            }

            creditoModel.Juros = taxa;

            return creditoModel;
        }
    }
}
