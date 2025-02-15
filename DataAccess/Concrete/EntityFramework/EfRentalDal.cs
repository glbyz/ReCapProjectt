﻿using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, ReCapContext>, IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetails()
        {
            using (ReCapContext context=new ReCapContext())
            {
                var result = from r in context.Rentals
                             join c in context.Cars
                             on r.CarId equals c.Id
                             join cu in context.Customers
                             on r.CustomerId equals cu.CustomerId
                             select new RentalDetailDto
                             {
                                 CarName=c.CarName,
                                 RentDate=r.RentDate,
                                 ReturnDate=r.ReturnDate,
                                 CompanyName=cu.CompanyName
                             };
                return result.ToList();
            }
        }
    }
}
