using DevPaceTest.BLL.DTO;
using DevPaceTest.DAL.Repositories;
using System.Collections.Generic;

namespace DevPaceTest.BLL.BussinessModels
{
    public class CRUDManager
    {
        public static List<CustomerDTO> GetAllCustomers(RepositoryManager repManager)
        {
            //Automapper is incopatible with .net framework, so mapping is manual
            var result = new List<CustomerDTO>();
            foreach (var customer in repManager.CustomersRepository.GetAll())
            {
                result.Add(new CustomerDTO()
                {
                    CompanyName = customer.CompanyName,
                    Email = customer.Email,
                    Name = customer.Name,
                    Phone = customer.Phone
                });
            }
            return result;
        }
        public static void UpdateCustomer(RepositoryManager repManager, CustomerDTO customerDTO)
        {
            repManager.CustomersRepository.Update(new DAL.Customer()
            {
                CompanyName = customerDTO.CompanyName,
                Email = customerDTO.Email,
                Name = customerDTO.Name,
                Phone = customerDTO.Phone
            });
            repManager.Save();
        }
        public static void CreateCustomer(RepositoryManager repManager, CustomerDTO customerDTO)
        {
            repManager.CustomersRepository.Add(new DAL.Customer()
            {
                CompanyName = customerDTO.CompanyName,
                Email = customerDTO.Email,
                Name = customerDTO.Name,
                Phone = customerDTO.Phone
            });
            repManager.Save();
        }

        public static List<string> GetAllIds(RepositoryManager repManager)
        {
            return repManager.CustomersRepository.GetAllIds();
        }
        public static List<CustomerDTO> FindCustomers(RepositoryManager repManager, string filter, string order, int pages, int rows)
        {
            var result = new List<CustomerDTO>();
            var databaseList = repManager.CustomersRepository.SearchCustomers(filter, order, pages, rows);
            foreach (var customer in databaseList)
            {
                result.Add(new CustomerDTO()
                {
                    CompanyName = customer.CompanyName,
                    Email = customer.Email,
                    Name = customer.Name,
                    Phone = customer.Phone
                });
            }
            return result;
        }
        public static void DeleteCustomer(RepositoryManager repManager, string customerName)
        {
            repManager.CustomersRepository.Delete(customerName);
            repManager.Save();
        }
    }
}
