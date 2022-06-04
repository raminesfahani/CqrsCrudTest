using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.AcceptanceTests.Infrastructure
{
    public class Helper
    {
        public static string GetSaltString()
        {
            Random randomGenerator = new();
            int randomInt = randomGenerator.Next(1000);
            return "username" + randomInt;
        }
    }
}
