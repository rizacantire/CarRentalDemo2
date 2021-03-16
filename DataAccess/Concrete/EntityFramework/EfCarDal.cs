using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, CarRentalContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails(Expression<Func<Car, bool>> filter = null)
        {
            using (var context = new CarRentalContext())
            {
                var result = from car in filter == null ? context.Cars : context.Cars.Where(filter)
                             join color in context.Colors on car.ColorId equals color.Id
                             join brand in context.Brands on car.BrandId equals brand.Id
                             select new CarDetailDto
                             {
                                 CarId = car.Id,
                                 BrandName = brand.Name,
                                 CarName = car.CarName,
                                 Description = car.Description,
                                 ColorName = color.Name,
                                 DailyPrice = car.DailyPrice

                             };
                return result.ToList();
            }
        }

        
        
    }
}
