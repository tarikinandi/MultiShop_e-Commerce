using AutoMapper;
using MultiShop.Cargo.DtoLayer.Dtos.CargoCompanyDtos;
using MultiShop.Cargo.DtoLayer.Dtos.CargoDetailDtos;
using MultiShop.Cargo.EntityLayer.Concrete;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MultiShop.Cargo.BusinessLayer.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateCargoCompanyDTO, CargoCompany>().ReverseMap();
            CreateMap<UpdateCargoCompanyDTO, CargoCompany>().ReverseMap();

            CreateMap<CreateCargoDetailDTO, CargoDetail>().ReverseMap();
            CreateMap<UpdateCargoDetailDTO, CargoDetail>().ReverseMap();
        }
    }
}
