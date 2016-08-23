using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NFluent;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
    [TestFixture]
    public class DynamicColumnOrder
    {
        [Test]
        public void UseHeaderToResolveFields()
        {
            var engine = new FileHelperEngine<CustomersVerticalBarWithHeader>
            {
                Options = { InferColumnOrderFromHeader = true }
            };
            var records = engine.ReadFile(FileTest.Good.CustomersVerticalBarWithHeader.Path);

            Check.That(EnumerableExtensions.Count(records) == 91);

            var firstRecord = records.First();
            Check.That(firstRecord.CustomerID == "ALFKI");
            Check.That(firstRecord.CompanyName == "Alfreds Futterkiste");
            Check.That(firstRecord.ContactName == "Maria Anders");
            Check.That(firstRecord.ContactTitle == "Sales Representative");
            Check.That(firstRecord.Address == "Obere Str. 57");
            Check.That(firstRecord.City == "Berlin");
            Check.That(firstRecord.Country == "Germany");
        }

        [Test]
        public void UseHeaderToResolveFieldsAsync()
        {
            var engine = new FileHelperAsyncEngine<CustomersVerticalBarWithHeader>
            {
                Options = { InferColumnOrderFromHeader = true }
            };

            CustomersVerticalBarWithHeader firstRecord;

            using (var textReader = File.OpenText(FileTest.Good.CustomersVerticalBarWithHeader.Path))
            using (engine.BeginReadStream(textReader))
            {
                var enumerator = ((IEnumerable<CustomersVerticalBarWithHeader>) engine).GetEnumerator();
                enumerator.MoveNext();
                firstRecord = enumerator.Current;
                enumerator.MoveNext();
                enumerator.MoveNext();
                enumerator.MoveNext();
            }

            Check.That(firstRecord.CustomerID == "ALFKI");
            Check.That(firstRecord.CompanyName == "Alfreds Futterkiste");
            Check.That(firstRecord.ContactName == "Maria Anders");
            Check.That(firstRecord.ContactTitle == "Sales Representative");
            Check.That(firstRecord.Address == "Obere Str. 57");
            Check.That(firstRecord.City == "Berlin");
            Check.That(firstRecord.Country == "Germany");
        }
    }
}