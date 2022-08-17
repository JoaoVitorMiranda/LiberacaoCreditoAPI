namespace LiberacaoCredito.Devedor.Domain.Models.Enums
{
    public class TipoCredito
    {
        public enum Tipo
        {
            Direto,
            Consignado,
            PessoaJuridica,
            PessoaFisica,
            Imobiliário
        };

        public enum Status
        {
            Aprovado,
            Recusado
        };
    }
}
