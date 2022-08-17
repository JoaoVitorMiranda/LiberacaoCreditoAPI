using LiberacaoCredito.Devedor.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiberacaoCredito.Devedor.Domain.Models.Credito
{
    public class CreditoAddModel
    {
        public int Id { get; set; }

        public int QtdParcelas { get; set; }

        public decimal Valor { get; set; }

        public DateTime Vencimento { get; set; }

        public TipoCredito Tipo { get; set; }
    }
}
