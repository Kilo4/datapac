using AutoMapper;
using datapac_interview.Dto.Book.request;
using datapac_interview.Dto.Order.request;
using datapac_interview.Models;

namespace datapac_interview.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        AllowNullCollections = true;
        
        CreateMap<CreateBookRequest, Book>();
        CreateMap<UpdateBookRequest, Book>();
        CreateMap<UpdateOrderedBookRequest, Book>();

        CreateMap<CreateOrderRequest, Order>();
    }
}