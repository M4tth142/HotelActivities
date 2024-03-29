﻿using Hotel.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Hotel.Presentation.Customer.Model
{
    public class CustomerUI : INotifyPropertyChanged
    {
        public CustomerUI(string name, string email, string address, string phone, int nrOfMembers)
        {
            Name = name;
            Email = email;
            Address = address;
            Phone = phone;
            NrOfMembers = nrOfMembers;
        }

        public CustomerUI(int? id, string name, string email, string address, string phone, int nrOfMembers)
        {
            Id = id;
            Name = name;
            Email = email;
            Address = address;
            Phone = phone;
            NrOfMembers = nrOfMembers;
        }

        public int? Id { get; set; }
        private string _name;
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged(); } }
        private string _email;
        public string Email { get { return _email; } set { _email = value; OnPropertyChanged(); } }
        public string Address { get; set; }
        private string _phone;
        public string Phone { get { return _phone; } set { _phone = value; OnPropertyChanged(); } }
        public int NrOfMembers { get; set; }

        private void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public event PropertyChangedEventHandler? PropertyChanged;


        public Hotel.Domain.Model.Customer ToCustomer()
        {
            ContactInfo contactInfo = new ContactInfo(Email, Phone, new Address(Address));
            return new Hotel.Domain.Model.Customer(Name, contactInfo);
            ;
        }

        public static CustomerUI DTOCustomerToUI(Hotel.Domain.Model.Customer customer)
        {
            // Note: This method assumes that the provided Customer has a non-null Contact.
            int id = customer.Id;
            string name = customer.Name;
            string email = customer.Contact.Email;
            Address address = customer.Contact.Address;
            string phone = customer.Contact.Phone;
            int nrOfMembers = customer.GetMembers().Count;

            return new CustomerUI(id, name, email, address.ToAddressLine(), phone, nrOfMembers);
        }

    }
}
