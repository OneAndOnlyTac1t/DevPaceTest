using System.Windows.Input;
using DevPaceTest.BLL.DTO;
using DevPaceTestClient.Enums;
using DevPaceTestClient.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace DevPaceTestClient.ViewModels
{
    public class EditWindowViewModel : ObservableObject
    {
        #region Fields
        private CustomerDTO _customer;
        private string _name;
        private string _email;
        private string _companyName;
        private string _phone;
        private RelayCommand _sendCommand;
        private RelayCommand _cancelCommand;
        private RelayCommand _validateCommand;
        private bool _isNameEnabled;
        private bool _isButtonEnabled;
        #endregion

        #region Properties
        public bool IsNameEnabled
        {
            get => _isNameEnabled;
            set => SetProperty(ref _isNameEnabled, value);
        }
        public bool IsButtonEnabled
        {
            get => _isButtonEnabled;
            set => SetProperty(ref _isButtonEnabled, value);
        }
        public CustomerDTO Customer
        {
            get => _customer;
            set => SetProperty(ref _customer, value);
        }
        public string ModeName { get; set; }
        public string Name
        {
            get => _name;
            set
            {
                IsButtonEnabled = false;
                SetProperty(ref _name, value);
            }
        }
        public string CompanyName
        {
            get => _companyName;
            set
            {
                IsButtonEnabled = false;
                SetProperty(ref _companyName, value);
            }
        }
        public string Phone
        {
            get => _phone;
            set
            {
                SetProperty(ref _phone, value);
            }
        }
        public string Email
        {
            get => _email;
            set
            {
                IsButtonEnabled = false;
                SetProperty(ref _email, value);
            }
        }
        #endregion
        #region Commands
        public ICommand SendCommand
        {
            get
            {
                if (_sendCommand == null)
                    _sendCommand = new RelayCommand(
                        () => this.SendCustomer());

                return _sendCommand;
            }
        }
        public ICommand ValidateCommand
        {
            get
            {
                if (_validateCommand == null)
                    _validateCommand = new RelayCommand(
                        () => this.ValidateValues());

                return _validateCommand;
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                    _cancelCommand = new RelayCommand(
                        () => this.Cancel());

                return _cancelCommand;
            }
        }
        #endregion
        private void ValidateValues()
        {
            IsButtonEnabled = EditWindowModel.ValidateAllValues(Name, CompanyName, Email, Phone);
        }       

        private void Cancel()
        {
            UserDialogResult = DialogResult.Cancel;
        }
        private void SendCustomer()
        {
            Customer = EditWindowModel.GenerateCustomer(Name, CompanyName, Email, Phone);

            UserDialogResult = DialogResult.Send;
        }

        public DialogResult UserDialogResult
        {
            get;
            private set;
        }

        public EditWindowViewModel(CustomerDTO customer, EditModes mode)
        {
            ModeName = mode.ToString();
            if (mode == EditModes.Update)
            {
                //setting init values
                IsNameEnabled = false;
                Name = customer.Name;
                CompanyName = customer.CompanyName;
                Email = customer.Email;
                Phone = customer.Phone;
            }
            else
            {
                IsNameEnabled = true;
            }
            //default behaviour for close button
            UserDialogResult = DialogResult.Cancel;
        }
    }
}
