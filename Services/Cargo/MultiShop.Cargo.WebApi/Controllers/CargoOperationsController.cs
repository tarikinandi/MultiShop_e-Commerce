using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.Dtos.CargoOperationDtos;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoOperationsController : ControllerBase
    {
        private readonly ICargoOperationService _cargoOperationService;
        private readonly IMapper _mapper;

        public CargoOperationsController(ICargoOperationService cargoOperationService, IMapper mapper)
        {
            _cargoOperationService = cargoOperationService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult CargoOperationList()
        {
            var values = _cargoOperationService.TGetAll();
            if (values == null || !values.Any())
            {
                return NotFound("No cargo operations found.");
            }
            return Ok(values);
        }

        [HttpGet("{id}")]
        public IActionResult CargoOperationGetById(int id)
        {
            var value = _cargoOperationService.TGetById(id);
            if (value == null)
            {
                return NotFound($"Cargo operation with ID {id} not found.");
            }
            return Ok(value);
        }

        [HttpPost]
        public IActionResult CreateCargoOperation(CreateCargoOperationDTO cargoOperation)
        {
            var entity = _mapper.Map<CargoOperation>(cargoOperation);
            if (entity == null)
            {
                return BadRequest("Invalid cargo operation data.");
            }
            _cargoOperationService.TInsert(entity);
            return Ok("Cargo operation created successfully.");
        }

        [HttpDelete]
        public IActionResult RemoveCargoOperation(int id)
        {
            var existingOperation = _cargoOperationService.TGetById(id);
            if (existingOperation == null)
            {
                return NotFound($"Cargo operation with ID {id} not found.");
            }
            _cargoOperationService.TDelete(id);
            return Ok("Cargo operation deleted successfully.");
        }

        [HttpPut]
        public IActionResult UpdateCargoOperation(UpdateCargoOperationDTO cargoOperation)
        {
            var entity = _mapper.Map<CargoOperation>(cargoOperation);
            if (entity == null)
            {
                return BadRequest("Invalid cargo operation data.");
            }
            var existingOperation = _cargoOperationService.TGetById(entity.CargoOperationId);
            if (existingOperation == null)
            {
                return NotFound($"Cargo operation with ID {entity.CargoOperationId} not found.");
            }
            _cargoOperationService.TUpdate(entity);
            return Ok("Cargo operation updated successfully.");
        }
    }
}
