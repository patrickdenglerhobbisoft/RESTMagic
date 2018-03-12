using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestMagic.Lib.Data;
using RestMagic.Lib.Language;
using RestMagic.RestService.Models;

namespace RestMagic.UnitTests
{
    [TestClass]
    public class ReflectionTests
    {
        [TestMethod]
        public void GetModelsFromAssemblyName()
        {
            Type result = ReflectionHelper.GetDataModelTypesFromAssemblyName(ReflectionHelper.SDK_ASSEMBLY_NAMESPACE).First<Type>();
            Assert.IsTrue(result == typeof(MyNewObject));
        }
    }
}
