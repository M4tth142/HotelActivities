using Hotel.Domain.Exceptions;
using Hotel.Domain.Interfaces;
using Hotel.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Managers
{
    public class AddressManager
    {
        public Address GetCustomers(string filter)
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                throw new CustomerManagerException("GetCustomers", ex);
            }
        }
    }
}
