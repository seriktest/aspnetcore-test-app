using AutoMapper;
using Ordering.Application.Features.Orders.Queries.GetOrdersList;

namespace Ordering.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.Entities.Order, OrdersVm>().ReverseMap();
    }

}