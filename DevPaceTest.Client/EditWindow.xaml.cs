using DevPaceTest.BLL.DTO;
using DevPaceTestClient.Enums;
using DevPaceTestClient.ViewModels;
using System.Windows;

namespace DevPaceTestClient
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        public EditWindow(CustomerDTO customer, EditModes mode)
        {
            DataContext = new EditWindowViewModel(customer, mode);
            InitializeComponent();
        }
    }
}
