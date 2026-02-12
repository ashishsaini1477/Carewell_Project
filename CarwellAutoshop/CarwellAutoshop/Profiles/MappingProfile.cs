using CarwellAutoshop.Domain.DTOs.Request;
using CarwellAutoshop.Domain.DTOs.Response;
using CarwellAutoshop.Domain.Entities;
using AutoMapper;

namespace CarwellAutoshop.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CustomerRequest, Customer>()
                .ForMember(d => d.CustomerId, o => o.Ignore())
                .ForMember(d => d.CreatedDate, o => o.Ignore());
            CreateMap<Customer, CustomerResponse>();
            CreateMap<UpdateCustomerRequest, Customer>()
                .ForMember(d => d.CreatedDate, o => o.Ignore())
                .ForMember(d => d.IsActive, o => o.Ignore());
            CreateMap<Customer, CustomerWithVehiclesResponse>();
            CreateMap<Vehicle, VehicleResponse>();

            CreateMap<InvoiceLineItem, InvoiceLineItemResponse>();
            CreateMap<LabourWork, LabourWorkResponse>();


            CreateMap<Invoice, InvoiceResponse>()
                .ForMember(d => d.LineItems, o => o.MapFrom(s => s.LineItems))
                .ForMember(d => d.LabourWorks, o => o.MapFrom(s => s.LabourWorks));


            CreateMap<JobCard, JobCardResponse>()
    .ForMember(dest => dest.CustomerId,
        opt => opt.MapFrom(src => src.Vehicle.CustomerId))
    .ForMember(dest => dest.FuelTypeId,
        opt => opt.MapFrom(src => src.Vehicle.FuelTypeId))
    .ForMember(dest => dest.Model,
        opt => opt.MapFrom(src => src.Vehicle.Model))
             .ForMember(dest => dest.RegistrationNo,
        opt => opt.MapFrom(src => src.Vehicle.RegistrationNo));

            CreateMap<JobCardRemarkDto, JobCardRemark>();
            CreateMap<CreateJobCardDto, JobCard>();

            CreateMap<FuelType, FuelTypeResponse>();
            CreateMap<JobCardStatus, JobCardStatusResponse>();
            CreateMap<PaymentMode, PaymentModeResponse>();
            CreateMap<VehicleDto, Vehicle>();
            
        }
    }
}
