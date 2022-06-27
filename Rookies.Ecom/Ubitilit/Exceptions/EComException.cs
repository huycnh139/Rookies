using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Ubitilit.Exceptions
{
    public class EComException : Exception
    {
        public EComException()
        {
        }

        public EComException(string message)
            : base (message)
        {

        }

        public EComException(string message, Exception inner)
            : base(message, inner)
        {
        }

    }
}
