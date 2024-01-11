using Hotel.Domain.Exceptions;
using Hotel.Domain.Managers;
using Hotel.Persistence.Repositories;
using Hotel.Presentation.Customer.Model;
using Hotel.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hotel.Presentation.Customer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<CustomerUI> customerUIs=new ObservableCollection<CustomerUI>();
        private CustomerManager customerManager;
        //private string conn = "Data Source=NB21-6CDPYD3\\SQLEXPRESS;Initial Catalog=HotelDonderdag;Integrated Security=True";
        public MainWindow()
        {
            InitializeComponent();
            customerManager = new CustomerManager(RepositoryFactory.CustomerRepository);
            customerUIs =new ObservableCollection<CustomerUI>(customerManager.GetCustomers(null).Select(x => new CustomerUI(x.Id,x.Name,x.Contact.Email,x.Contact.Address.ToString(),x.Contact.Phone,x.GetMembers().Count)).ToList());
            CustomerDataGrid.ItemsSource = customerUIs;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            customerUIs =new ObservableCollection<CustomerUI>(customerManager.GetCustomers(SearchTextBox.Text).Select(x => new CustomerUI(x.Id, x.Name, x.Contact.Email, x.Contact.Address.ToString(), x.Contact.Phone, x.GetMembers().Count)).ToList());
            CustomerDataGrid.ItemsSource = customerUIs;
        }

        private void MenuItemAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            CustomerWindow w = new CustomerWindow(null);
            if (w.ShowDialog()==true)
                customerUIs.Add(w.CustomerUI);
        }
        private void MenuItemDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Not selected", "Delete");
            }
            else
            {
                CustomerUI selectedCustomer = (CustomerUI)CustomerDataGrid.SelectedItem;

                MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete customer {selectedCustomer.Name}?", "Delete Customer", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        // Check if Id has a value before attempting to delete
                        if (selectedCustomer.Id.HasValue)
                        {
                            customerManager.DeleteCustomer(selectedCustomer.Id.Value);
                            customerUIs.Remove(selectedCustomer);
                        }
                        else
                        {
                            MessageBox.Show("Customer ID is null. Unable to delete.", "Delete Customer");
                        }
                    }
                    catch (CustomerManagerException ex)
                    {
                        MessageBox.Show($"Error deleting customer: {ex.Message}", "Delete Customer");
                    }
                }
            }
        }

        private void MenuItemUpdateCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerDataGrid.SelectedItem==null) MessageBox.Show("not selected", "update");
            else
            {
                CustomerWindow w = new CustomerWindow((CustomerUI)CustomerDataGrid.SelectedItem);
                w.ShowDialog();
            }
        }

        private void MenuItemBookActivity_Click(object sender, RoutedEventArgs e)
        {
            ActivityWindow activityWindow = new ActivityWindow();
            activityWindow.Show();

            //activityWindow.ShowDialog(); // Use ShowDialog if you want it as a modal window
        }

    }
}
