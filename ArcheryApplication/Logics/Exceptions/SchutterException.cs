using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArcheryApplication.Classes;

namespace ArcheryApplication.Exceptions
{
    public class SchutterException : Exception
    {
        public SchutterException()
        {
            
        }
        public SchutterException(string message) : base(message)
        {

        }
    }
}
