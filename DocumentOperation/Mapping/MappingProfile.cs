using AutoMapper;
using DocumentOperation.API.Models;
using DocumentOperation.Data.Entities;


namespace DocumentOperation.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<InvoiceDataModel, InvoiceViewModel>()
                .ForMember(dest => dest.InvoiceHeader, opt => opt.MapFrom(src => src.InvoiceHeader))
                .ForMember(dest => dest.InvoiceLine, opt => opt.MapFrom(src => src.InvoiceLine));

            CreateMap<InvoiceViewModel, InvoiceDataModel>()
                .ForMember(dest => dest.InvoiceHeader, opt => opt.MapFrom(src => src.InvoiceHeader))
                .ForMember(dest => dest.InvoiceId, opt => opt.MapFrom(src => src.InvoiceHeader.InvoiceId))
                .ForMember(dest => dest.InvoiceLine, opt => opt.MapFrom(src => src.InvoiceLine))
                .AfterMap((src, dest) =>
                 {
                     // Set the InvoiceId for each element in the InvoiceDetails list
                     foreach (var detail in dest.InvoiceLine)
                     {
                         detail.InvoiceId = dest.InvoiceHeader.InvoiceId;
                     }
                 });

            CreateMap<InvoiceHeaderViewModel, InvoiceHeaderDataModel>(); // Adjusted mapping
            CreateMap<InvoiceDetailViewModel, InvoiceDetailDataModel>()
                .ForMember(dest => dest.InvoiceDetailId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opt => opt.Ignore());


        }
    }

}