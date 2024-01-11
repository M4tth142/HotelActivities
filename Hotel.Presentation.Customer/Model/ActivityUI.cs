using Hotel.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Hotel.Presentation.Customer.Model
{
    public class ActivityUI : INotifyPropertyChanged
    {
        //uit
        public ActivityUI(string name, string description, int availableSlots) 
        {
            Name = name;
            Description = description;
            AvailableSlots = availableSlots;
        }
        //in
        public ActivityUI(int? id, string name, string description, DateTime date, string address, int availableSlots, TimeSpan durationTime, string email, string phone)
        {
            Id = id;
            Name = name;
            Description = description;
            Date = date;
            Location = address;
            AvailableSlots = availableSlots;
            DurationTime = durationTime;
            Email = email;
            Phone = phone;
        }

        public int? Id { get; set; }
        private string _title;
        public string Name { get { return _title; } set { _title = value; OnPropertyChanged(); } }
        private string _description;
        public string Description { get { return _description; } set { _description = value; OnPropertyChanged(); } }
        public DateTime Date { get; set; }
        private string _location;
        public string Location { get { return _location; } set { _location = value; OnPropertyChanged(); } }
        public int AvailableSlots { get; set; }
        public TimeSpan DurationTime { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        private void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public event PropertyChangedEventHandler? PropertyChanged;


        public static ActivityUI FromDatabaseActivity(Activity databaseActivity)
        {
            // Concatenate the desired properties from the database activity
            string name = databaseActivity.Title;
            string description = $"{databaseActivity.Email}, {databaseActivity.Location}, {databaseActivity.Phone}, {databaseActivity.Description}";
            int availableSlots = databaseActivity.AvailableSlots;

            // Create and return a new ActivityUI object
            return new ActivityUI(name, description, availableSlots);
        }


    }
}
