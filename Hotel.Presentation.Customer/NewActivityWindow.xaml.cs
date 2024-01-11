using Hotel.Domain.Managers;
using Hotel.Domain.Model;
using System.Windows;
using System;

namespace Hotel.Presentation.Customer
{
    public partial class NewActivityWindow : Window
    {
        private ActivityManager _activityManager;

        public NewActivityWindow(ActivityManager activityManager)
        {
            InitializeComponent();
            _activityManager = activityManager;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Handle CancelButton click event
            Close();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Retrieve values from TextBoxes and DatePicker
                string description = DescriptionTextBox.Text;
                string durationString = DurationTextBox.Text;
                DateTime date = DatePicker.SelectedDate ?? DateTime.Now; // Default to the current date if no date selected
                string location = LocationTextBox.Text;
                string name = NameTextBox.Text;
                int availableSlots = int.Parse(AvailableSlotsTextBox.Text);
                string email = EmailTextBox.Text;
                string phone = PhoneTextBox.Text;

                // Try to convert the duration string to TimeSpan
                if (TimeSpan.TryParse(durationString, out TimeSpan duration))
                {
                    // Create a new Activity object
                    Activity newActivity = new Activity(name, description, availableSlots, duration, location,date, email, phone);

                    // Pass the newActivity to your ActivityManager for further processing
                    _activityManager.AddActivity(newActivity);

                    // Optionally, you can display a success message or close the window
                    MessageBox.Show("Activity created successfully!");
                    Close();
                }
                else
                {
                    // Handle invalid duration input
                    MessageBox.Show("Invalid duration input. Please enter a valid time duration.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions (e.g., validation errors or database errors)
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
