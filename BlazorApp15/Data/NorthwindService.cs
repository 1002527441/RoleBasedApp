using BlazorApp15.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace BlazorApp15.Data
{
    public class NorthwindService
    {
        private readonly NorthwindContext db;

        public NorthwindService(NorthwindContext db)
        {
            this.db = db;
        }


        public List<Customers> GetCustomer()
        {
            var query = db.Customers as IQueryable;

            return query.ToDynamicList<Customers>();
        }


        public int UpdateCustomer(Customers customers)
        {

            var entity = db.Customers.Find(customers.CustomerId);

            db.Entry(entity).CurrentValues.SetValues(customers);

          
            var ent1 = db.Entry(entity);

            db.EnsureAutoHistory();
            return  db.SaveChanges();
  
        }


        public Customers GetSingleCustomer(string Id)
        {
            return db.Customers.Find(Id);

        }


        public List<Microsoft.EntityFrameworkCore.AutoHistory> GetAutoHistory()
        {
            return db.AutoHistory.ToList();
        }
    }
}
