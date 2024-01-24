using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.Customer.Model
{
    public class MemberUI
    {
        public MemberUI(string name)
        {
            Name = name;
        }

        string Name { get; set; }
    }
}
