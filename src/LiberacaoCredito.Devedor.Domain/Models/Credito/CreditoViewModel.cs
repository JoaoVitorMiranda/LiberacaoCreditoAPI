using LiberacaoCredito.Devedor.Domain.Models.Enums;
using System;

namespace LiberacaoCredito.Devedor.Domain.Models.Credito
{
    public class CreditoViewModel
    {
        public int Id { get; set; }

        public int QtdParcelas { get; set; }

        public decimal Valor { get; set; }

        public DateTime Vencimento { get; set; }

        public TipoCredito Tipo { get; set; }
    }
}
