using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeLibrary.util
{
    internal class ExceptionHandler
    {

        public class BaseException : Exception
        {
            public const string DefaultErrorMessage = "An Unknown Error Occured";

            public BaseException() : base(DefaultErrorMessage) { }
            public BaseException(string message) : base(message) { }
            public BaseException(string message, System.Exception inner) : base(message, inner) { }
        }



    }
}
