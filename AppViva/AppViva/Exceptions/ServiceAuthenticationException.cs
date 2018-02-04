using System;
using System.Collections.Generic;
using System.Text;

namespace AppViva.Exceptions
{
    public class ServiceAuthenticationException : Exception
    {
        public string Content { get; }


        public ServiceAuthenticationException(string content)
        {
            Content = content;
        }
    }
}
