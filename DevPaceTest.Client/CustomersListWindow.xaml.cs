using DevPaceTestClient.Services;
using DevPaceTestClient.ViewModels;
using Microsoft.AspNetCore.SignalR.Client;
using System.Windows;

namespace DevPaceTestClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class CustomersListWindow : Window
    {
        public CustomersListWindow()
        {
            HubConnection connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:5001/customersTable")
                .Build();
            DataContext = new CustomersListWindowViewModel(new SignalRService(connection));
            InitializeComponent();            
        }
    }
}
