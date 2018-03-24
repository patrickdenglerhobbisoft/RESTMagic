using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System.Collections.Generic;
using System.Diagnostics;
using RestMagic.Platform.UnitTests.Helpers;
using NUnit.Framework;
using System.Threading;

namespace RestMagic.Platform.UnitTests
{
    [TestClass]
    public class DomainControllerTests
    {
        private Process _iisProcess;
        string port = "50194";
      
       // [TestInitialize]
        public void Init()
        {
            var thread = new Thread(StartIisExpress) { IsBackground = true };

            thread.Start();
        }
        private void StartIisExpress()
        {
            string apppLocation = "http://localhost";
           
            var startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Normal,
                ErrorDialog = true,
                LoadUserProfile = true,
                CreateNoWindow = false,
                UseShellExecute = false,
                Arguments = string.Format("/path:\"{0}\" /port:{1}", apppLocation, port)
            };

            var programfiles = string.IsNullOrEmpty(startInfo.EnvironmentVariables["programfiles"])
            ? startInfo.EnvironmentVariables["programfiles(x86)"]
            : startInfo.EnvironmentVariables["programfiles"];

            startInfo.FileName = programfiles + "\\IIS Express\\iisexpress.exe";

            try
            {
                _iisProcess = new Process { StartInfo = startInfo };

                _iisProcess.Start();
                _iisProcess.WaitForExit();
            }
            catch
            {
                _iisProcess.CloseMainWindow();
                _iisProcess.Dispose();
            }
        }
        [TestMethod]
        public void TestModels()
        {
            Dictionary<string, DomainControllerTestData> testingScenarios = IntegrationTestHelpers.TestList;// : ScenariosToTest;
            string clientHost = "http://localhost:" + port + "/api/v1.0/Get";
            var client = new RestClient(clientHost);
            try
            {
                foreach (var testScenario in testingScenarios)
                {
                    Debug.WriteLine("Testing Scenario " + testScenario.Key);

                    object result = new object();  // clearing in case types are floating around in memory
                    var modelName = testScenario.Value.QueryModel.DataModel;
                    string classString = "RestMagic.RestService.Models." + modelName;
                 

                    var request = new RestRequest(Method.POST);
                    request.RequestFormat = DataFormat.Json;
                    request.AddJsonBody(testScenario.Value.QueryModel);


                    //Type type = Type.GetType(classString);
                    //var listType = typeof(List<>);
                    //Type constructedListType = listType.MakeGenericType(type);
                    //dynamic instance = Activator.CreateInstance(constructedListType);

                    dynamic instance =   client.Execute (request);

                     
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
         
        }

        //[TestMethod]
        //public void TestBulkDownload()
        //{
        //    string testToUse = IntegrationTestHelpers.TestList.Keys.First();
        //    var testScenario = IntegrationTestHelpers.TestList[testToUse];

        //    Debug.WriteLine("Testing Bulk Download for Scenario " + testToUse + ".");
        //}
    }
}
