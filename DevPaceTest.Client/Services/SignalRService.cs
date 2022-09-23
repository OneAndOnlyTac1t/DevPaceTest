using DevPaceTest.BLL.DTO;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace DevPaceTestClient.Services
{
    public class SignalRService
    {
        private readonly HubConnection _connection;

        public event Action<List<CustomerDTO>> CustomersListReceived;
        public event Action<string> ErrorMessageReceived;

        public SignalRService(HubConnection connection)
        {
            _connection = connection;
            _connection.On<List<CustomerDTO>>("ReceiveCustomersTable", (table) => CustomersListReceived?.Invoke(table));
            _connection.On<string>("ReceiveErrorMessage", (message) => ErrorMessageReceived?.Invoke(message));
        }
        public async Task Connect()
        {
            await _connection.StartAsync();
        }
        public async Task ReceiveCustomersTable()
        {
            if (_connection.State == HubConnectionState.Connected)
            {
                await _connection.SendAsync("SendCustomersTable");
            }
            else
            {
                MessageBox.Show("Client is disconected from server. Connect pls client");
            }
        }

        public async Task SendSearchCustomer(string filter, string order, int page, int numberOfRows)
        {
            if (_connection.State == HubConnectionState.Connected)
            {
                await _connection.SendAsync("SearchCustomer", filter, order, page, numberOfRows);
            }
            else
            {
                MessageBox.Show("Client is disconected from server. Connect pls client");
            }
        }

        public async Task SendCreateCustomer(CustomerDTO customer)
        {
            if (_connection.State == HubConnectionState.Connected)
            {
                await _connection.SendAsync("CreateCustomer", customer);
            }
            else
            {
                MessageBox.Show("Client is disconected from server. Connect pls client");
            }
        }

        public async Task SendUpdateCustomer(CustomerDTO customer)
        {
            if (_connection.State == HubConnectionState.Connected)
            {
                await _connection.SendAsync("UpdateCustomer", customer);
            } 
            else
            {
                MessageBox.Show("Client is disconected from server. Connect pls client");
            }
        }

        public async Task SendDeleteCustomer(string customerName)
        {
            if (_connection.State == HubConnectionState.Connected)
            {
                await _connection.SendAsync("DeleteCustomer", customerName);
            }
            else
            {
                MessageBox.Show("Client is disconected from server. Connect pls client");
            }
        }
    }
}
