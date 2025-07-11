using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.Dtos.CargoDetailDtos;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoDetailsController : ControllerBase
    {
        private readonly ICargoDetailService _cargoDetailService;
        private readonly IMapper _mapper;

        public CargoDetailsController(ICargoDetailService cargoDetailService, IMapper mapper)
        {
            _cargoDetailService = cargoDetailService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult CargoDetailList()
        {
            var values = _cargoDetailService.TGetAll();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public IActionResult CargoDetailGetById(int id)
        {
            var value = _cargoDetailService.TGetById(id);
            if (value == null)
            {
                return NotFound();
            }
            return Ok(value);
        }

        [HttpPost]
        public IActionResult CreateCargoDetail(CreateCargoDetailDTO detailDTO)
        {
            var entity = _mapper.Map<CargoDetail>(detailDTO);
            _cargoDetailService.TInsert(entity);
            return Ok("Kargo detayı başarıyla oluşturuldu.");
        }

        [HttpDelete]
        public IActionResult RemoveCargoDetail(int id)
        {
            _cargoDetailService.TDelete(id);
            return Ok("Kargo detayı başarıyla silindi.");
        }

        [HttpPut]
        public IActionResult UpdateCargoDetail(UpdateCargoDetailDTO detailDTO)
        {
            var entity = _mapper.Map<CargoDetail>(detailDTO);
            _cargoDetailService.TUpdate(entity);
            return Ok("Kargo detayı başarıyla güncellendi.");

        }
    }
}
