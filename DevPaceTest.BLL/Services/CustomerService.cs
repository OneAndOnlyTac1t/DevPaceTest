using DevPaceTest.BLL.BussinessModels;
using DevPaceTest.BLL.DTO;
using DevPaceTest.DAL.Repositories;
using System.Collections.Generic;

namespace DevPaceTest.BLL.Services
{
    public class CustomerService
    {
        RepositoryManager _repManager;
        public CustomerService(RepositoryManager rm)
        {
            _repManager = rm;
        }

        public List<CustomerDTO> GetAll()
        {
           return CRUDManager.GetAllCustomers(_repManager);
        }
        public List<string> GetAllIds()
        {
            return CRUDManager.GetAllIds(_repManager);
        }
        public List<CustomerDTO> SearchCustomers(string filter, string order, int pages, int rows)
        {
            return CRUDManager.FindCustomers(_repManager, filter, order, pages, rows);
        }
        public bool AddCustomer(CustomerDTO customerDTO)
        {
            //validation
            if (_repManager.CustomersRepository.Get(customerDTO.Name) == null)
            {
                CRUDManager.CreateCustomer(_repManager, customerDTO);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool UpdateCustomer(CustomerDTO customerDTO)
        {
            //validation
            if (_repManager.CustomersRepository.Get(customerDTO.Name) != null)
            {
                CRUDManager.UpdateCustomer(_repManager, customerDTO);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool DeleteCustomer(string customerName)
        {
            if (_repManager.CustomersRepository.Get(customerName) != null)
            {
                CRUDManager.DeleteCustomer(_repManager, customerName);

                //validation if result was successfull
                return _repManager.CustomersRepository.Get(customerName) == null;               
            }
            else
            {
                return false;
            }
        }
    }
}
