using System;
using System.Net;
using System.Collections;
using System.Collections.Generic;

namespace chargeio
{
	[Serializable]
	public class ChargeIOException : Exception
	{
		public HttpStatusCode HttpStatusCode { get; set; }
		public List<ChargeIOError> Errors { get; set; }

		public ChargeIOException() 
		{ 
		}

        public ChargeIOException(string message) : base(message)
        {
        }

        public ChargeIOException(HttpStatusCode httpStatusCode, List<ChargeIOError> errors, string message)
            : base(message)
        {
            HttpStatusCode = httpStatusCode;
            Errors = errors;
        }

  	}
}
