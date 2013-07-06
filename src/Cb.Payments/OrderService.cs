using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cb.Payments
{
    public class OrderService
    {
        public OrderService()
        {

        }

        public OrderService(string developerKey, string clerkKey)
        {

        }

        public List<string> List {
            get
            {
                throw new ArgumentException("Access Denied");
            }
        }
    }
}
