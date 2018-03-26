using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestMagic.DataManager;
using RestMagic.DataManager.Data;
using RestMagic.Lib.Data;
using RestMagic.Lib.Language;
using RestMagic.RestService.Models;

namespace RestMagic.Tools.UnitTests
{
    [TestClass]
    public class ProcessorTests
    {
        MetaData metaData = new MetaData();

        [TestInitialize]
        public void Init()
        {
            var dataModelDetailsTableAdapter = new DataManager.Data.MetaDataTableAdapters.DataModelDetailsTableAdapter();
            dataModelDetailsTableAdapter.Fill(metaData.DataModelDetails);

            var dataModelTableAdapter = new DataManager.Data.MetaDataTableAdapters.DataModelsTableAdapter();
            dataModelTableAdapter.Fill(metaData.DataModels);
        }


        [TestMethod]
        public void ProcessModel()
        {
            Processor processor = new Processor();
            processor.Generate(new string[] { "MyNewObject" }, metaData);

        }
    }
}
