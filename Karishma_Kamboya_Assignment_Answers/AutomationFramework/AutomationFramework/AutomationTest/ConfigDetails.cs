using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTest
{
    public class ConfigDetails
    {

        public static class LoginDetails
        {
            public static string fullName = "KarishmaKamboya";
            public static string orgName = "Infosys";
            public static string Email = EmailDetails.email + "@mailinator.com";
        }

        public static class EmailDetails
        {
            public static string email = "jabatalks";
        }
    }       
}

