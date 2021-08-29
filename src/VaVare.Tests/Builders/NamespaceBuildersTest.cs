using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using VaVare.Builders;
using VaVare.Builders.BuildMembers;
using VaVare.Models;
using VaVare.Saver;

namespace VaVare.Tests.Builders
{
    [TestFixture]
    public class NamespaceBuildersTest
    {
        [Test]
        public void Build_WhenBuildingNamespace_CodeShouldContainMembers()
        {
            var @namespace = new NamespaceBuilder("MyNamespace")
                .WithUsings("System")
                .With(new EnumBuildMember("MyEnum", new List<EnumMember> {new EnumMember("SomeEnum", 2)}))
                .With(new ClassBuildMember(new ClassBuilder("MyClass", null).BuildWithoutNamespace()))
                .Build();

            Assert.AreEqual("usingSystem;namespaceMyNamespace{enumMyEnum{SomeEnum=2}publicclassMyClass{}}", @namespace.ToString());
        }
    }
}
