using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Core.Entities.Concrete;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFrameWork
{
    public class EfUserDal : EfEntityRepositoryBase<User, CarRentalContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new CarRentalContext())
            {
                var result = from oC in context.OperationClaims
                             join uOC in context.UserOperationClaims
                             on oC.Id equals uOC.OperationClaimId
                             where uOC.UserId == user.Id
                             select new OperationClaim { Id = oC.Id, Name = oC.Name };
                return result.ToList();

            }
        }
    }
}
