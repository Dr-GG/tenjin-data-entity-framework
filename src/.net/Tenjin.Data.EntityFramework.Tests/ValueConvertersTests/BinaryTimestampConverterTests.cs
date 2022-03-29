using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Tenjin.Data.EntityFramework.ValueConverters;

namespace Tenjin.Data.EntityFramework.Tests.ValueConvertersTests
{
    [TestFixture]
    public class BinaryTimestampConverterTests
    {
        public const string TestStringValue = "ABC_abc";

        public static readonly IEnumerable<byte> TestBytes = new[]
        {
            (byte)65, // A
            (byte)66, // B
            (byte)67, // C
            (byte)95, // _
            (byte)97, // a
            (byte)98, // b
            (byte)99, // v
        };

        [Test]
        public void ToDatabase_WhenProvidedWithTestString_MatchesTestBytes()
        {
            var converter = new BinaryTimestampConverter();
            var bytes = converter.ConvertFromProvider(TestStringValue);

            Assert.AreEqual(TestBytes.ToArray(), bytes);
        }

        [Test]
        public void FromDatabase_WhenProvidedWithTestBytes_MatchesTestString()
        {
            var converter = new BinaryTimestampConverter();
            var stringValue = converter.ConvertToProvider(TestBytes);

            Assert.AreEqual(TestStringValue, stringValue);
        }
    }
}
