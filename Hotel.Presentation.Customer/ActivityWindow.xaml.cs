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
        private ObservableCollection<ActivityUI> activityUI;
        //private ObservableCollection<ActivityUI> activityUIDescription;

        private ActivityManager activityManager;

        public ActivityWindow()
        {
            InitializeComponent();

            activityManager = new ActivityManager(RepositoryFactory.ActivityRepository);

            // Load activities into the ObservableCollection
            activityUI = new ObservableCollection<ActivityUI>(activityManager.GetActivities(null).Select(x => new ActivityUI(x.Title,x.Description,x.AvailableSlots)));
            //activityUIDescription = new ObservableCollection<ActivityUI>(activityManager.GetActivities(null).Select(x => new ActivityUI(x.Description)));

            // Set the item sources for the DataGrids
            ActivityDataGrid.ItemsSource = activityUI;
            //DescriptionsDataGrid.ItemsSource = activityUIDescription;

            // Event handler for TitlesDataGrid selection changed
            ActivityDataGrid.SelectionChanged += TitlesDataGrid_SelectionChanged;
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
            // Add your logic for searching here
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
