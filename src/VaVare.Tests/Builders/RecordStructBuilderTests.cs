using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Formatting;
using Microsoft.CodeAnalysis.Formatting;
using NUnit.Framework;
using VaVare.Builders;
using VaVare.Models;
using VaVare.Models.Properties;

namespace VaVare.Tests.Builders
{
    [TestFixture]
    public class RecordStructBuilderTests : RecordBuilderBaseTests
    {
        protected override string RecordSpecifier => "recordstruct";
        
        [SetUp]
        public void SetUp()
        {
            RecordBuilder = new RecordBuilder("TestRecord", "MyNamespace", true);
        }
    }
}
