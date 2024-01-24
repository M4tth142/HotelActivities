using Hotel.Domain.Managers;
using Hotel.Presentation.Customer.Model;
using Hotel.Util;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Hotel.Presentation.Customer
{
    public partial class ActivityWindow : Window
    {
        private ObservableCollection<ActivityUI> activityUIs;
        private ObservableCollection<MemberUI> members;


        private ActivityManager activityManager;
        private MemberManager memberManager;

        public ActivityWindow(CustomerUI customer)
        {
            InitializeComponent();

            activityManager = new ActivityManager(RepositoryFactory.ActivityRepository);
            memberManager = new MemberManager(RepositoryFactory.MemberRepository);


            // Load activities into the ObservableCollection
            activityUIs = new ObservableCollection<ActivityUI>(activityManager.GetActivities(null).Select(x => new ActivityUI(x.Title, x.Description, x.AvailableSlots)));

            // Check if customer.Id has a value before using it
            if (customer.Id.HasValue)
            {
                // Assuming memberManager is initialized somewhere in your code
                members = new ObservableCollection<MemberUI>(memberManager.GetMember(null).Select(x => new MemberUI(x.Name)));
            }
            else
            {
                // Handle the case where customer.Id is null (if needed)
                members = new ObservableCollection<MemberUI>();
            }

            // Set the item sources for the DataGrids
            ActivityDataGrid.ItemsSource = activityUIs;
            MembersDataGrid.ItemsSource = members;
            //DescriptionsDataGrid.ItemsSource = activityUIDescription;

            // Event handler for TitlesDataGrid selection changed
            ActivityDataGrid.SelectionChanged += TitlesDataGrid_SelectionChanged;
            MembersDataGrid.SelectionChanged += TitlesDataGrid_SelectionChanged;

        }


        private void TitlesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        // Existing event handlers...

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            activityUIs = new ObservableCollection<ActivityUI>(activityManager.GetActivities(SearchTextBox.Text).Select(x => new ActivityUI(x.Title ,x.Description,x.AvailableSlots)));
            ActivityDataGrid.ItemsSource = activityUIs;
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            NewActivityWindow w = new NewActivityWindow(activityManager);
            w.ShowDialog();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Reservation completed!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
