using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
           
            var result = _carService.GetAll();
            
            if(result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getcars")]
        public IActionResult GetCars()
        {
            
            var result = _carService.GetCarDetails();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getcarscolor")]
        public IActionResult GetCarsColor(int id)
        {

            var result = _carService.GetCarsByColorId(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpGet("getcarsbrandid")]
        public IActionResult GetCarsBrandId(int id)
        {

            var result = _carService.GetCarDetailsBrand(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpGet("getcarsdetailsbybrand")]
        public IActionResult GetCarsDetailsByBrand(int id)
        {
            var result = _carService.GetCarsDetailsByBrand(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getcardetailsbyid")]
        public IActionResult getcardetailsbyid(int carId)
        {
            var result = _carService.GetCarsByCarId(carId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getcarsdetailsbycolor")]
        public IActionResult GetCarsDetailsByColor(int id)
        {
            var result = _carService.GetCarsDetailsByColor(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getcarsbrand")]
        public IActionResult GetCarsBrand(int id)
        {

            var result = _carService.GetCarsByBrandId(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        


        [HttpPost("add")]
        public IActionResult Add(Car car)
        {
            var result = _carService.Add(car);
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {

            var result = _carService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }

        [HttpPost("addtr")]
        public IActionResult AddTr(Car car)
        {
            var result = _carService.AddTransactional(car);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbybrandandcolor")]
        public IActionResult GetByBrandAndColor(int brandId, int colorId)
        {
            var result = _carService.GetCarsDetailByBrandIdAndColorId(brandId, colorId);
            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);

        }
    }
}
