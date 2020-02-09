using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLLCache
{
    public class DllCacheClass
    {

        public static String DsnName = "";

        public void getDSN(String dsnEntrada)
        {
            DsnName = dsnEntrada; 
        }

    }
}
