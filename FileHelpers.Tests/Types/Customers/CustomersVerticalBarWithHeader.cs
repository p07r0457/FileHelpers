using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers.Tests
{
    [DelimitedRecord("|")]
    [IgnoreFirst(1)]
    public class CustomersVerticalBarWithHeader
      : IComparable<CustomersVerticalBarWithHeader>
    {
        public string Country;
        public string CompanyName;
        public string CustomerID;
        public string ContactTitle;
        public string Address;
        public string ContactName;
        public string City;
        
        public int CompareTo(CustomersVerticalBarWithHeader other)
        {
            return CustomerID.CompareTo(other.CustomerID);

        }
    }
}