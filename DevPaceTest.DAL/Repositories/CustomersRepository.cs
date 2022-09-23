using System.Collections.Generic;
using System.Linq;

namespace DevPaceTest.DAL.Repositories
{
    public class CustomersRepository
    {
        private DevPaceTestEntities db;
        public CustomersRepository(DevPaceTestEntities context)
        {
            this.db = context;
        }
        public List<Customer> GetAll()
        {
            return db.FindAllCustomers().ToList();
        }
        public Customer Get(string name)
        {
            return db.Customers.Find(name);
        }
        public List<string> GetAllIds()
        {
            return db.Customers.Select(k => k.Name).ToList();
        }
        public List<Customer> SearchCustomers(string filter, string order, int pages, int rows)
        {
            return db.FindCustomer(filter, order, pages, rows).ToList();
        }
        public void Add(Customer customer)
        {
            db.Customers.Add(customer);
        }
        public void Update(Customer customer)
        {
            var elem = db.Customers.Single(k => k.Name == customer.Name);
            elem.CompanyName = customer.CompanyName;
            elem.Phone = customer.Phone;
            elem.Email = customer.Email;
            //db.Entry(customer).State = System.Data.Entity.EntityState.Modified;
        }
        public void Delete(string customerName)
        {
            db.Customers.Remove(db.Customers.Single(k => k.Name == customerName));
        }
    }
}
