using System;
using System.Collections.Generic;
using System.Net;
using System.Numerics;
using System.Xml.Linq;


namespace Hotel.Domain.Model
{
    public class Activity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AvailableSlots { get; set; }
        public TimeSpan Duration { get; set; }
        public ContactInfo Contact { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        //recieved from ui
        //name, description, availableSlots, duration, date, location, email, phone
        public Activity(string title, string description, int availableSlots, TimeSpan duration, string address, DateTime date, string email, string phone)
        {
            Title = title;
            Description = description;
            AvailableSlots = availableSlots;
            Duration = duration;
            Date = date;
            Location = address;
            Email = email;
            Phone = phone;
            Email = email;
            Phone = phone;
        }
        //recieve db
        public Activity(int id,string title, string email, string address,string phone ,string description, int availableSlots)
        {
            Id = id;
            Title = title;
            Email = email;
            Location = address;
            Phone = phone;
            Description = description;
            AvailableSlots = availableSlots;
        }
        //send db
        public Activity(string title, string email, string address, string phone, string description, int availableSlots)
        {
            Title = title;
            Email = email;
            Location = address;
            Phone = phone;
            Description = description;
            AvailableSlots = availableSlots;
        }
       
    }
}
