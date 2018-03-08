using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System.Collections.Generic;
using RestMagic.RestService.Models;
using System.Data.SqlClient;
using System.Diagnostics;
 
using RestMagic.Platform.UnitTests.Helpers;
using System.Linq;

namespace RestMagic.Platform.UnitTests
{
    [TestClass]
    public class DomainControllerTests
    {

        //[TestMethod]
        //public void TestBulkDownload()
        //{
        //    string testToUse = DataHelper.TestList.Keys.First();
        //    var testScenario = DataHelper.TestList[testToUse];

        //    Debug.WriteLine("Testing Bulk Download for Scenario " + testToUse + ".");
        //}
        [TestMethod]
        public void TestModels(Dictionary<string,DomainControllerTestData> ScenariosToTest = null)
        {
            Dictionary<string, DomainControllerTestData> testingScenarios = ScenariosToTest == null ? IntegrationTestHelpers.TestList : ScenariosToTest;

            var client = new RestClient("http://localhost/RestMagic.RestService/api/v1.0/DataModel");
            try
            {
                foreach (var testScenario in testingScenarios)
                {
                    Debug.WriteLine("Testing Scenario " + testScenario.Key);

                    object result = new object();  // clearing in case types are floating around in memory
                    var modelName = testScenario.Value.DataModelName;
                    string classString = "RestMagic.RestService.Models." + modelName;
                    Type type = Type.GetType(classString);

                    var request = new RestRequest(modelName, Method.POST);
                    request.RequestFormat = DataFormat.Json;
                    request.AddJsonBody(testScenario.Value.QueryModel);

             
                    // var dataObject = Activator.CreateInstance(null, classString).Unwrap();
                    // result = (dataObject as DataModel).GetList(parameters, type);
                    //var objectTypeInstance = Activator.CreateInstance<T>();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
         
        }
    }
}
