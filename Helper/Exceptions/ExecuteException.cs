using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Helper.Exceptions
{
    internal class ExecuteException : Exception
    {
        public ExecuteException(string message) : base(message) { }
    }
}
