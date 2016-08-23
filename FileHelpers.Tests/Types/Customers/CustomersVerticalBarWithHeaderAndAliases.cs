using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers.Tests
{
    [DelimitedRecord("|")]
    [IgnoreFirst(1)]
    public class CustomersVerticalBarWithHeaderAndAliases
      : IComparable<CustomersVerticalBarWithHeaderAndAliases>
    {
        public string Country;

        [FieldCaption("Company Name")]
        public string CompanyName;

        [FieldCaption("Customer ID")]
        public string CustomerID;

        [FieldCaption("Contact Title")]
        public string ContactTitle;
        
        public string Address;

        [FieldCaption("Contact Name")]
        public string ContactName;
        
        public string City;

        public int CompareTo(CustomersVerticalBarWithHeaderAndAliases other)
        {
            return CustomerID.CompareTo(other.CustomerID);

        }
    }
}