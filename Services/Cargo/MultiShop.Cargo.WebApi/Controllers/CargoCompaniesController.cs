using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.Dtos.CargoCompanyDtos;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoCompaniesController : ControllerBase
    {
        private readonly ICargoCompanyService _cargoCompanyService;
        private readonly IMapper _mapper;

        public CargoCompaniesController(ICargoCompanyService cargoCompanyService , IMapper mapper)
        {
            _cargoCompanyService = cargoCompanyService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult CargoCompanyList()
        {
            var values = _cargoCompanyService.TGetAll();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public IActionResult CargoCompanyGetById(int id)
        {
            var value = _cargoCompanyService.TGetById(id);
            if (value == null)
            {
                return NotFound();
            }
            return Ok(value);
        }

        [HttpPost]
        public IActionResult CreateCargoCompany(CreateCargoCompanyDTO companyDTO)
        {
            var entity = _mapper.Map<CargoCompany>(companyDTO);
            _cargoCompanyService.TInsert(entity);
            return Ok("Kargo şirketi başarıyla oluşturuldu.");
        }



        [HttpDelete]
        public IActionResult RemoveCargoCompany(int id)
        {
            _cargoCompanyService.TDelete(id);
            return Ok("Kargo şirketi başarıyla silindi.");
        }

        [HttpPut]
        public IActionResult UpdateCargoCompany(UpdateCargoCompanyDTO companyDTO)
        {
            var entity = _mapper.Map<CargoCompany>(companyDTO);
            _cargoCompanyService.TUpdate(entity);
            return Ok("Kargo şirketi başarıyla güncellendi.");
        }

    }
}
