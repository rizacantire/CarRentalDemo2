using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarImageDal : EfEntityRepositoryBase<CarImage, CarRentalContext>, ICarImageDal
    {
        public List<CarDetailDto> GetImageDetails(Expression<Func<Car, bool>> filter = null)
        {
            using (var context = new CarRentalContext())
            {
                var result = from car in filter == null ? context.Cars : context.Cars.Where(filter)
                             join color in context.Colors on car.ColorId equals color.Id
                             join brand in context.Brands on car.BrandId equals brand.Id
                             join carImage in context.CarImages on car.Id equals carImage.CarId
                             select new CarDetailDto
                             {
                                 CarId = car.Id,
                                 BrandName = brand.Name,
                                 CarName = car.CarName,
                                 Description = car.Description,
                                 ColorName = color.Name,
                                 DailyPrice = car.DailyPrice,
                                 ImagePath = carImage.ImagePath

                             };
                return result.ToList();
            }
        }
    }
}
