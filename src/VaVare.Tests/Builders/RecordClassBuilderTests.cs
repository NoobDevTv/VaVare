using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnit.Framework;
using VaVare.Builders;
using VaVare.Models;
using VaVare.Models.Properties;

namespace VaVare.Tests.Builders
{
    [TestFixture]
    public class RecordClassBuilderTests : RecordBuilderBaseTests
    {
        protected override string RecordSpecifier => "record";

        [SetUp]
        public void SetUp()
        {
            RecordBuilder = new RecordBuilder("TestRecord", "MyNamespace");
        }

    }
}
