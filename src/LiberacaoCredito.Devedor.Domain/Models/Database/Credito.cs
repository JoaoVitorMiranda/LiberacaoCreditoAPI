using LiberacaoCredito.Devedor.Infra.Common.DapperExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LiberacaoCredito.Devedor.Domain.Models.Database
{
    [QueryTable("Credito")]
    public partial class Credito
    {
        public Credito()
        {
           
        }

        [QueryField("Id")]
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }

        [QueryField("Valor")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Valor { get; set; }

        [QueryField("QtdParcelas")]
        public int QtdParcelas { get; set; }

        [QueryField("Vencimento")]
        [Column(TypeName = "datetime")]
        public DateTime Vencimento { get; set; }

        [QueryField("Tipo")]
        public int Tipo { get; set; }
    }
}
