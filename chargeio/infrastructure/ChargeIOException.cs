using System;
using System.Collections.Generic;
using System.Net;

namespace ChargeIo.Infrastructure
{
	[Serializable]
	public class ChargeIoException : Exception
	{
		public HttpStatusCode HttpStatusCode { get; set; }
		public List<ChargeIoError> Errors { get; set; }

		public ChargeIoException() 
		{ 
		}

        public ChargeIoException(string message) : base(message)
        {
        }

        public ChargeIoException(HttpStatusCode httpStatusCode, List<ChargeIoError> errors, string message)
            : base(message)
        {
            HttpStatusCode = httpStatusCode;
            Errors = errors;
        }

  	}
}
