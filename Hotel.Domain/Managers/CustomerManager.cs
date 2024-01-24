using Hotel.Domain.Exceptions;
using Hotel.Domain.Interfaces;
using Hotel.Domain.Model;
using System;
using System.Collections.Generic;

namespace Hotel.Domain.Managers
{
    public class CustomerManager
    {
        private ICustomerRepository _customerRepository;

        public CustomerManager(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public IReadOnlyList<Customer> GetCustomers(string filter)
        {
            try
            {
                return _customerRepository.GetCustomers(filter);
            }
            catch (Exception ex)
            {
                throw new CustomerManagerException("GetCustomers", ex);
            }
        }

        public void CreateCustomer(Customer customer)
        {
            ValidateCustomer(customer); // Perform validations before creating the customer

            try
            {
                _customerRepository.AddCustomer(customer);
            }
            catch (Exception ex)
            {
                throw new CustomerManagerException("CreateCustomer", ex);
            }
        }

        private void ValidateCustomer(Customer customer)
        {
            // Validate customer name
            if (string.IsNullOrWhiteSpace(customer.Name) || customer.Name.Length > 500)
            {
                throw new CustomerManagerException("Invalid customer name. It must have a maximum length of 500 characters and must not be empty.");
            }

            // Validate email address
            if (!customer.Contact.Email.Contains('@'))
            {
                throw new CustomerManagerException("Invalid email address. It must contain the '@' character.");
            }

            // Validate phone number
            if (string.IsNullOrWhiteSpace(customer.Contact.Phone))
            {
                throw new CustomerManagerException("Phone number must be provided.");
            }

            // Additional validations can be added based on your requirements
        }

        public void DeleteCustomer(int customerId)
        {
            try
            {
                _customerRepository.DeleteCustomer(customerId);
            }
            catch (Exception ex)
            {
                throw new CustomerManagerException("DeleteCustomer", ex);
            }
        }

        public void UpdateCustomer(Customer customer)
        {
            ValidateCustomer(customer); // Perform validations before updating the customer

            try
            {
                _customerRepository.UpdateCustomer(customer);
            }
            catch (Exception ex)
            {
                throw new CustomerManagerException("UpdateCustomer", ex);
            }
        }
    }
}
