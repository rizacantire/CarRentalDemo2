using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFrameWork
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, CarRentalContext>, IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetails()
        {
            using (var context = new CarRentalContext())
            {
                var result = from c in context.Cars
                             join r in context.Rentals
                             on c.Id equals r.CarId
                             join b in context.Brands
                             on c.BrandId equals b.Id
                             join co in context.Customers
                             on r.CustomerId equals co.Id
                             join u in context.Users
                             on co.UserId equals u.Id


                             select new RentalDetailDto
                             {
                                 BrandName = b.Name,
                                 FirstName = u.FirstName,
                                 LastName = u.LastName,
                                 RentDate = r.RentDate,
                                 ReturnDate = r.ReturnDate,
                                 CarId = c.Id
                                 
                                 
                                 
                                 
                                
                             };
                return result.ToList();
            }
        }
    }
}
