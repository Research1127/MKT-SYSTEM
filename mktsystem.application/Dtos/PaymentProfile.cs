using AutoMapper;
using mktsystem.domain.Entities;

namespace mktsystem.application.Dtos;

public class PaymentProfile : Profile
{
    public PaymentProfile()
    {
        CreateMap<Payments, PaymentDto>()
            .ForMember(dest => dest.Month, opt => opt.MapFrom(src =>
                new DateTime(src.Year, src.Month, 1).ToString("MMMM"))); // Convert month number to name
    }
}