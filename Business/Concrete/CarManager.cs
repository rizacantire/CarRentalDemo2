using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Business.BusinessAspects.Autofac;
using Core.Aspects.Autofac.Caching;
using System.Linq;
using Core.Utilities.Business;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }
        [SecuredOperation("admin")]
        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Add(Car car)
        {
            IResult result = BusinessRules.Run(CheckIfCarNameExists(car.CarName));
            _carDal.Add(car);

            return new Result(true, Messages.CarAdded);
        }

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);

            return new Result(true, Messages.CarDeleted);
        }


        [CacheAspect]
        public IDataResult<List<Car>> GetAll()
        {


            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.CarListed);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails());
        }

        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        [TransactionScopeAspect]
        public IResult Update(Car car)
        {
            
            _carDal.Update(car);

            return new Result(true, Messages.CarUpdated);
        }


        public IDataResult<List<Car>> GetCarsByColorId(int colorId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == colorId), Messages.CarListed);
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int brandId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == brandId), Messages.CarListed);
        }

        public IDataResult<List<Car>> GetByUnitPrice(decimal min, decimal max)
        {
            throw new NotImplementedException();
        }


        [CacheAspect]
        public IDataResult<List<Car>> GetById(int carId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.Id == carId), Messages.CarListed);
        }

        private IResult CheckIfCarNameExists(string productName)
        {
            var result = _carDal.GetAll(p => p.CarName == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.CarNameAlreadyExists);
            }

            return new ErrorResult(Messages.CarNameAlreadyExists);
        }
        [TransactionScopeAspect]
        public IResult AddTransactional(Car car)
        {
            Add(car);
            if(car.DailyPrice<500)
            {
                throw new Exception("Fiyat 500 liranın altında olamaz");
            }

            Add(car);
            return null;
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailsBrand(int brandId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(c=>c.BrandId == brandId));
        }

        public IDataResult<List<CarDetailDto>> GetCarsDetailsByBrand(int brandId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(c => c.BrandId == brandId), Messages.CarListed);
        }

        public IDataResult<List<CarDetailDto>> GetCarsDetailsByColor(int colorId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(c => c.ColorId == colorId), Messages.CarListed);
        }
    }
}
