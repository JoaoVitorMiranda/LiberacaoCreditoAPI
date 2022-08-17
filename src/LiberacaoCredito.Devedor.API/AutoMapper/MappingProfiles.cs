using AutoMapper;
using LiberacaoCredito.Devedor.Domain.Models.Credito;
using System.Diagnostics.CodeAnalysis;

namespace LiberacaoCredito.Devedor.API.AutoMapper
{
    [ExcludeFromCodeCoverage]
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            #region Credito
            CreateMap<Domain.Models.Database.Credito, CreditoModel>().ReverseMap();
            CreateMap<Domain.Models.Database.Credito, CreditoAddModel>().ReverseMap();
            CreateMap<Domain.Models.Database.Credito, CreditoViewModel>().ReverseMap();
            CreateMap<CreditoViewModel, Domain.Models.Database.Credito>().ReverseMap();
            CreateMap<CreditoModel, Domain.Models.Database.Credito>().ReverseMap();
            CreateMap<CreditoAddModel, Domain.Models.Database.Credito>().ReverseMap();
            #endregion
        }
    }
}
