using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheFerreteria
{
    public class ClassCacheFerreteria
    {
        static string userCode;

        public void setUsuario(string userCode2)
        {
            userCode = userCode2;
        }

        public string getUsuario()
        {
            if(userCode == "")
            {
                return "1";
            }
            else
            {
                return userCode;
            }
        }

    }
}
