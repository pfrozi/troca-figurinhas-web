using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrocaFigurinhas.Models.Business
{
    [Serializable()]
    public class BusinessException : Exception
    {
        public BusinessException() : base() { }
        public BusinessException(string message) : base(message) { }
        public BusinessException(string message, System.Exception inner) : base(message, inner) { }

    }
}