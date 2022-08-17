using FluentValidation;
using LiberacaoCredito.Devedor.Domain.Models;

namespace LiberacaoCredito.Devedor.Domain.Validation.CustomerValidation
{
    public class CustomerDeleteValidation : AbstractValidator<Customer>
    {
        public CustomerDeleteValidation()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .WithMessage("Id não pode ser nulo");
        }
    }
}
