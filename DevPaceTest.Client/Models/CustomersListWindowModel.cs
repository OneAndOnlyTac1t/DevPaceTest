using DevPaceTest.BLL.DTO;
using DevPaceTestClient.Enums;
using DevPaceTestClient.Services;
using DevPaceTestClient.Validation;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace DevPaceTestClient.Models
{
    public static class CustomersListWindowModel
    {
        public static async void DeleteCustomer(SignalRService service, string customerName)
        {
            await service.SendDeleteCustomer(customerName);
        }

        public static async void GetAllCustomers(SignalRService service)
        {
            await service.ReceiveCustomersTable();
        }
        public static async void SearchCustomer(SignalRService service, string filter, string orderby, string page, string numberOfRows)
        {
            int pageInt, rowsInt;
            if (StringToIntValidator.ValidateNumber(out pageInt, page))
            {
                if (StringToIntValidator.ValidateNumber(out rowsInt, numberOfRows))
                {
                    if (filter != null)
                    {
                        await service.SendSearchCustomer(filter, orderby, pageInt, rowsInt);
                    }
                    else
                    {
                        MessageBox.Show("Wrong number of filter. Please insert value");
                    }
                }
                else
                {
                    MessageBox.Show("Wrong number of rows value. Please insert number");
                }
            }
            else
            {
                MessageBox.Show("Wrong page value. Please insert number");
            }
        }
        public static CustomerDTO GetResultFromDialogWindow(CustomerDTO customer, EditModes mode)
        {
            //open dialog edit window and receive result from it
            EditWindow editWindow = new EditWindow(customer, mode);
            editWindow.ShowDialog();
            DialogResult result =
            (editWindow.DataContext as ViewModels.EditWindowViewModel).UserDialogResult;
            if (result == DialogResult.Send)
            {
                return (editWindow.DataContext as ViewModels.EditWindowViewModel).Customer;
            }
            else
            {
                return null;
            }
        }

        public static List<CustomerDTO> FilterItems(List<CustomerDTO> customers, string nameFilter, string companyfilter, string phoneFilter, string emailFilter)
        {
            var result = new List<CustomerDTO>(customers);
            if (!string.IsNullOrEmpty(nameFilter))
            {
                result = result.Where(l => l.Name == nameFilter).ToList();
            }
            if (!string.IsNullOrEmpty(companyfilter))
            {
                result = result.Where(l => l.CompanyName == companyfilter).ToList();
            }
            if (!string.IsNullOrEmpty(phoneFilter))
            {
                result = result.Where(l => l.Phone == phoneFilter).ToList();
            }
            if (!string.IsNullOrEmpty(emailFilter))
            {
                result = result.Where(l => l.Email == emailFilter).ToList();
            }
            return result;
        }
    }
}
