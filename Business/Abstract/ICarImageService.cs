using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        IResult Add(IFormFile file,CarImage carImage);
        IResult Delete(CarImage carImage);
        IResult Update([FromForm(Name = "file")] IFormFile file, [FromForm] CarImage carImage);
        IDataResult<List<CarImage>> GetAll();
        IDataResult<List<CarDetailDto>> GetImagesByDetail();
        DataResult<List<CarDetailDto>> GetImagesDetailById(int carId);


    }
}
