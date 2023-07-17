using AutoMapper;
using DocumentOperation.API.Models;
using DocumentOperation.Data.Entities;


namespace DocumentOperation.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Invoice, InvoiceViewModel>()
                .ForMember(dest => dest.InvoiceHeader, opt => opt.MapFrom(src => src.InvoiceHeader))
                .ForMember(dest => dest.InvoiceLine, opt => opt.MapFrom(src => src.InvoiceLines));

            CreateMap<InvoiceViewModel, Invoice>()
                .ForMember(dest => dest.InvoiceHeader, opt => opt.MapFrom(src => src.InvoiceHeader))
                .ForMember(dest => dest.InvoiceLines, opt => opt.MapFrom(src => src.InvoiceLine))
                .ForMember(dest => dest.InvoiceId, opt => opt.MapFrom(src => src.InvoiceHeader.InvoiceId));

            CreateMap<InvoiceHeaderViewModel, InvoiceHeader>(); // Adjusted mapping
            CreateMap<InvoiceDetailViewModel, InvoiceDetail>(); // Adjusted mapping

        }
    }

}