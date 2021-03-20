using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface ICarService
    {
        IResult Add(Car car);
        IResult Delete(Car car);
        IResult Update(Car car);
        IDataResult<List<Car>> GetAll();
        IDataResult<List<Car>> GetCarsByColorId(int colorId);

        IDataResult<List<Car>> GetByUnitPrice(decimal min, decimal max);
        IDataResult<List<CarDetailDto>> GetCarDetails();
        IDataResult<List<CarDetailDto>> GetCarsByCarId(int carId);
        IDataResult<List<CarDetailDto>> GetCarsDetailsByBrand(int brandId); 
         IDataResult<List<CarDetailDto>> GetCarDetailsBrand(int brandId);
        IDataResult<List<CarDetailDto>> GetCarsDetailsByColor(int colorId);
        IDataResult<List<CarDetailDto>> GetCarsDetailByBrandIdAndColorId(int brandId,int colorId); 
         IDataResult<List<Car>> GetCarsByBrandId(int brandId); 
        IDataResult<List<Car>> GetById(int carId);

        IResult AddTransactional(Car car);

        
    }
}
