using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Cb.Payments.Model
{
    public class Authorization
    {
        public string DevKey { get; set; }
        public string ClerkKey { get; set; }

        public string ClerkPart
        {
            get
            {
                return (!String.IsNullOrEmpty(ClerkKey))
                ? ":" + ClerkKey : "";
            }
        }

        public override string ToString()
        {
            return DevKey + ClerkPart;            
        }
    }
}
