using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;

        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }

        public IResult Add([FromForm(Name = "file")] IFormFile file, [FromForm] CarImage carImage)
        {
            var result = BusinessRules.Run(CheckIfCarImagesLimitExceeded(carImage.CarId));
            if (result != null)
            {
                return result;
            }

            ImageUpload(file, carImage);

            _carImageDal.Add(carImage);

            return new SuccessResult(Messages.Added);
        }



        public IResult Delete(CarImage carImage)
        {
            _carImageDal.Delete(carImage);
            return new SuccessResult(Messages.Deleted);
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(), Messages.Listed);

        }

        public IResult Update([FromForm(Name = "file")] IFormFile file, [FromForm] CarImage carImage)
        {
            ImageUpload(file, carImage);
            _carImageDal.Update(carImage);
            return new SuccessResult(Messages.Updated);
        }

        private static void ImageUpload(IFormFile file, CarImage carImage)
        {
            if (file == null)
            {
                carImage.ImagePath = "logo.png";

            }
            else
            {
                var extention = Path.GetExtension(file.FileName);
                var randomName = string.Format($"{Guid.NewGuid()}{extention}");
                carImage.ImagePath = randomName;
                carImage.Date = DateTime.Now;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", randomName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }

        }

        private IResult CheckIfCarImagesLimitExceeded(int carId)
        {
            var result = _carImageDal.GetAll(c => c.CarId == carId).Count;
            if (result >= 5)
            {
                return new ErrorResult(Messages.CarImagesLimitExceeded);
            }
            return new SuccessResult();
        }

        public IDataResult<List<CarDetailDto>> GetImagesByDetail()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carImageDal.GetImageDetails(), Messages.Listed);
        }

        public DataResult<List<CarDetailDto>> GetImagesDetailById(int carId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carImageDal.GetImageDetails(c => c.Id == carId), Messages.Listed);
        }
    }
}

