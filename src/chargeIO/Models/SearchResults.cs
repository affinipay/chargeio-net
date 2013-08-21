using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Newtonsoft.Json;

namespace ChargeIO
{
    public class SearchResults<T> : List<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalEntries { get; set; }
        
        public int TotalPages
        {
            get
            {
                return (TotalEntries == 0) ? 1 : ((TotalEntries + PageSize - 1) / PageSize);
            }
        }
        
        public int PreviousPage
        {
            //returns previous page number, -1 if no previous page
            get { return Page > 1 ? (Page - 1) : -1; }
        }
        public int NextPage
        {
            //return next page number, -1 if no next page
            get { return Page < TotalPages ? (Page + 1) : -1; }
        }
    }
}
