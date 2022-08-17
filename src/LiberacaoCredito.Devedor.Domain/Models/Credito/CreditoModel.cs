using LiberacaoCredito.Devedor.Domain.Models.Enums;
using System;

namespace LiberacaoCredito.Devedor.Domain.Models.Credito
{
    public class CreditoModel
    {
        public int Id { get; set; }

        public int QtdParcelas { get; set; }

        public decimal Valor { get; set; }

        public DateTime Vencimento { get; set; }

        public TipoCredito.Tipo Tipo { get; set; }

        public decimal VlrTotalJuros { get; set; }

        public decimal Juros { get; set; }

        public TipoCredito.Status Status { get; set; }
    }
}
