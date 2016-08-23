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
                Options = { ShouldInferColumnOrderFromHeader = true }
            };
            var records = engine.ReadFile(FileTest.Good.CustomersVerticalBarWithHeader.Path);

            Assert.AreEqual(EnumerableExtensions.Count(records), 91);

            var firstRecord = records.First();
            Assert.AreEqual(firstRecord.CustomerID, "ALFKI");
            Assert.AreEqual(firstRecord.CompanyName, "Alfreds Futterkiste");
            Assert.AreEqual(firstRecord.ContactName, "Maria Anders");
            Assert.AreEqual(firstRecord.ContactTitle, "Sales Representative");
            Assert.AreEqual(firstRecord.Address, "Obere Str. 57");
            Assert.AreEqual(firstRecord.City, "Berlin");
            Assert.AreEqual(firstRecord.Country, "Germany");
        }

        [Test]
        public void UseHeaderToResolveFieldsAsync()
        {
            var engine = new FileHelperAsyncEngine<CustomersVerticalBarWithHeader>
            {
                Options = { ShouldInferColumnOrderFromHeader = true }
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

            Assert.AreEqual(firstRecord.CustomerID, "ALFKI");
            Assert.AreEqual(firstRecord.CompanyName, "Alfreds Futterkiste");
            Assert.AreEqual(firstRecord.ContactName, "Maria Anders");
            Assert.AreEqual(firstRecord.ContactTitle, "Sales Representative");
            Assert.AreEqual(firstRecord.Address, "Obere Str. 57");
            Assert.AreEqual(firstRecord.City, "Berlin");
            Assert.AreEqual(firstRecord.Country, "Germany");
        }
    }
}