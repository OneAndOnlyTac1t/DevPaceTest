using DevPaceTest.BLL.DTO;
using System.Windows;

namespace DevPaceTestClient.Models
{
    public class EditWindowModel
    {
        public static bool ValidateAllValues(string name, string companyName, string email, string phone)
        {
            if(name==null|| companyName == null || email == null || phone == null)
            {
                MessageBox.Show("Values cannot be null");
                return false;
            }
            if (!Validation.MaxCharValidation.ValidateMaxCharLength(name))
            {
                MessageBox.Show("Name is too long");
                return false;
            }
            if (!Validation.MaxCharValidation.ValidateMaxCharLength(companyName))
            {
                MessageBox.Show("Company name is too long");
                return false;
            }
            if (!Validation.EmailValidator.ValidateEmail(email))
            {
                MessageBox.Show("Invalid email");
                return false;
            }
            return true;
        }
        public static CustomerDTO GenerateCustomer(string name, string companyName, string email, string phone)
        {            
            return new CustomerDTO() { CompanyName = companyName, Name = name, Phone = phone, Email = email };
        }
    }
}
