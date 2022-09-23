using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using DevPaceTest.BLL.DTO;
using DevPaceTest.BLL.Services;
using DevPaceTest.DAL.Repositories;

namespace SignalRServer.Hubs
{
    public class SignalRHub:Hub
    {
        static CustomerService _customerService;
        public override Task OnConnectedAsync()
        {
            _customerService = new CustomerService(new RepositoryManager()); 
            return base.OnConnectedAsync();
        }
        public async Task SendCustomersTable()
        {
            await Clients.Caller.SendAsync("ReceiveCustomersTable", _customerService.GetAll());
        }
        public async Task SendIDsList()
        {
            await Clients.Caller.SendAsync("ReceiveIdsList", _customerService.GetAllIds());
        }

        public async Task SearchCustomer(string filter, string order, int page, int numberOfRows)
        {
            var result = _customerService.SearchCustomers(filter, order, page, numberOfRows);
            await Clients.Caller.SendAsync("ReceiveCustomersTable", result);
        }

        public async Task CreateCustomer(CustomerDTO customer)
        {
            if (_customerService.AddCustomer(customer))
            {
                await Clients.Caller.SendAsync("ReceiveCustomersTable", _customerService.GetAll());
            }
            else 
            {
                await Clients.Caller.SendAsync("ReceiveErrorMessage", $"Customer name {customer.Name} is not unique, change customer Name");
            }
        }

        public async Task UpdateCustomer(CustomerDTO customer)
        {
            if (_customerService.UpdateCustomer(customer))
            {
                await Clients.Caller.SendAsync("ReceiveCustomersTable", _customerService.GetAll());
            }
            else
            {
                await Clients.Caller.SendAsync("ReceiveErrorMessage", $"Customer with this Name {customer.Name} does not exist");
            }
        }

        public async Task DeleteCustomer(string customerName)
        {
            if (_customerService.DeleteCustomer(customerName))
            {
                await Clients.Caller.SendAsync("ReceiveCustomersTable", _customerService.GetAll());
            }
            else
            {
                await Clients.Caller.SendAsync("ReceiveErrorMessage", "Customer with Name {}");
            }
        }
    }
}
