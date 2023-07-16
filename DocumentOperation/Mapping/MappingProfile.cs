using AutoMapper;
using DocumentOperation.API.Models;
using DocumentOperation.Data.Entities;


namespace DocumentOperation.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Invoice, Document>()
                .ForMember(dest => dest.InvoiceHeader, opt => opt.MapFrom(src => src.InvoiceHeader))
                .ForMember(dest => dest.InvoiceLine, opt => opt.MapFrom(src => src.InvoiceLines));

            CreateMap<Document, Invoice>()
                .ForMember(dest => dest.InvoiceHeader, opt => opt.MapFrom(src => src.InvoiceHeader))
                .ForMember(dest => dest.InvoiceLines, opt => opt.MapFrom(src => src.InvoiceLine))
                .ForMember(dest => dest.InvoiceId, opt => opt.MapFrom(src => src.InvoiceHeader.InvoiceId));

            CreateMap<DocumentHeader, InvoiceHeader>(); // Adjusted mapping
            CreateMap<DocumentDetail, InvoiceDetail>(); // Adjusted mapping

        }
    }

}