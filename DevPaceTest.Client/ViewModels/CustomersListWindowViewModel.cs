using Microsoft.Toolkit.Mvvm.ComponentModel;
using DevPaceTest.BLL.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using DevPaceTestClient.Services;
using DevPaceTestClient.Models;
using DevPaceTestClient.Enums;
using System.Windows;

namespace DevPaceTestClient.ViewModels
{
    public class CustomersListWindowViewModel : ObservableObject
    {
        #region Fields
        private List<string> _orderList;
        private ObservableCollection<CustomerDTO> _customersList;
        private ObservableCollection<CustomerDTO> _customersListStorage;
        private string _errorMessage;
        private string _selectedOrder;
        private string _numberOfPages;
        private string _numberOfRows;
        private string _nameFilter;
        private string _companyFilter;
        private string _phoneFilter;
        private string _emailFilter;
        private string _searchFilter;
        private CustomerDTO _selectedCustomer;
        private RelayCommand _getAllCommand;
        private RelayCommand _deleteCommand;
        private RelayCommand _connectCommand;
        private RelayCommand _updateCommand;
        private RelayCommand _createCommand;
        private RelayCommand _readCommand;
        private RelayCommand _filterCommand;
        private RelayCommand _refreshCommand;

        private SignalRService _service;
        private bool _buttonsEnabled;

        #endregion

        #region Properties
        public ObservableCollection<CustomerDTO> CustomersList
        {
            get => _customersList;
            set => SetProperty(ref _customersList, value);
        }
        public ObservableCollection<CustomerDTO> CustomersListStorage
        {
            get => _customersListStorage;
            set => SetProperty(ref _customersListStorage, value);
        }
        public string EmailFilter
        {
            get => _emailFilter;
            set => SetProperty(ref _emailFilter, value);
        }
        public string NameFilter
        {
            get => _nameFilter;
            set => SetProperty(ref _nameFilter, value);
        }
        public string CompanyFilter
        {
            get => _companyFilter;
            set => SetProperty(ref _companyFilter, value);
        }
        public string PhoneFilter
        {
            get => _phoneFilter;
            set => SetProperty(ref _phoneFilter, value);
        }
        public List<string> OrderList
        {
            get => _orderList;
            set => SetProperty(ref _orderList, value);
        }
        public string SelectedOrder
        {
            get => _selectedOrder;
            set => SetProperty(ref _selectedOrder, value);
        }
        public string NumberOfPages
        {
            get => _numberOfPages;
            set => SetProperty(ref _numberOfPages, value);
        }
        public string NumberOfRows
        {
            get => _numberOfRows;
            set => SetProperty(ref _numberOfRows, value);
        }
        public string SearchFilter
        {
            get => _searchFilter;
            set => SetProperty(ref _searchFilter, value);
        }
        public CustomerDTO SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                if (value != null)
                {
                    ButtonsEnabled = true;
                }
                else
                {
                    ButtonsEnabled = false;
                }
                SetProperty(ref _selectedCustomer, value);
            }
        }
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }
        public bool ButtonsEnabled
        {
            get => _buttonsEnabled;
            set => SetProperty(ref _buttonsEnabled, value);
        }
        #endregion

        public CustomersListWindowViewModel(SignalRService service)
        {
            CustomersList = new ObservableCollection<CustomerDTO>();
            OrderList = new List<string>()
            {
               "Name", "CompanyName", "Phone", "Email",
            };
            SelectedOrder = OrderList[0];
            ButtonsEnabled = false;
            _service = service;
            _service.CustomersListReceived += CustomersListReceived;
            _service.ErrorMessageReceived += _service_ErrorMessageReceived; ;           
        }

        private void _service_ErrorMessageReceived(string obj)
        {
            MessageBox.Show(obj);
        }

        private void CustomersListReceived(List<CustomerDTO> table)
        {
            CustomersList = new ObservableCollection<CustomerDTO>(table);
            CustomersListStorage = new ObservableCollection<CustomerDTO>(table);
        }

        #region Commands
        public ICommand FilterCommand
        {
            get
            {
                if (_filterCommand == null)
                    _filterCommand = new RelayCommand(
                        () => this.FilterItems());

                return _filterCommand;
            }
        }
        public ICommand ConnectCommand
        {
            get
            {
                if (_connectCommand == null)
                    _connectCommand = new RelayCommand(
                        () => this.Connect());

                return _connectCommand;
            }
        }        

        public ICommand RefreshCommand
        {
            get
            {
                if (_refreshCommand == null)
                    _refreshCommand = new RelayCommand(
                        () => this.RefreshItems());

                return _refreshCommand;
            }
        }        

        public ICommand GetAllCommand
        {
            get
            {
                if (_getAllCommand == null)
                    _getAllCommand = new RelayCommand(
                        () => 
                        { 
                            CustomersListWindowModel.GetAllCustomers(_service); 
                        });

                return _getAllCommand;
            }
        }
        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                    _deleteCommand = new RelayCommand(
                        () => CustomersListWindowModel.DeleteCustomer(_service, SelectedCustomer.Name));

                return _deleteCommand;
            }
        }
        public ICommand UpdateCommand
        {
            get
            {
                if (_updateCommand == null)
                    _updateCommand = new RelayCommand(
                        () => this.OpenEditWindow(EditModes.Update));

                return _updateCommand;
            }
        }

        public ICommand CreateCommand
        {
            get
            {
                if (_createCommand == null)
                    _createCommand = new RelayCommand(
                        () => this.OpenEditWindow(EditModes.Create));

                return _createCommand;
            }
        }
        public ICommand SearchCommand
        {
            get
            {
                if (_readCommand == null)
                    _readCommand = new RelayCommand(
                        () => CustomersListWindowModel.SearchCustomer(_service, SearchFilter, SelectedOrder, NumberOfPages, NumberOfRows));

                return _readCommand;
            }
        }
        #endregion

        #region Methods
        private async void Connect()
        {
            await _service.Connect();
        }

        private void FilterItems()
        {
            //saving all values to storage, showing filtered values to user
            var result = CustomersListWindowModel.FilterItems(CustomersList.ToList(), NameFilter, CompanyFilter, PhoneFilter, EmailFilter);
            CustomersList = new ObservableCollection<CustomerDTO>(result);
        }
        private void RefreshItems()
        {
            //taking value from storage
            CustomersList = new ObservableCollection<CustomerDTO>(CustomersListStorage);
        }
        public async void OpenEditWindow(EditModes mode)
        {
            //open window regarding to which mode was selected by user           
            CustomerDTO resultCustomer = null;
            if (mode == EditModes.Update)
            {
                resultCustomer = CustomersListWindowModel.GetResultFromDialogWindow(SelectedCustomer, mode);
                if (resultCustomer != null)
                    await _service.SendUpdateCustomer(resultCustomer);
            }
            else
            {
                resultCustomer = CustomersListWindowModel.GetResultFromDialogWindow(resultCustomer, mode);
                if (resultCustomer != null)
                {
                    await _service.SendCreateCustomer(resultCustomer);
                }
            }
        }
        #endregion
    }
}
