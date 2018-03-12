using RestMagic.RestService.Models;
using System.Collections.Generic;
using RestMagic.Lib.Data;

namespace RestMagic.Platform.UnitTests.Helpers
{
    class IntegrationTestHelpers
    {
        private static Dictionary<string, DomainControllerTestData> dataIntegrationTests = null;

        public static Dictionary<string, DomainControllerTestData> TestList
        {
            get
            {
                if (dataIntegrationTests == null) InitRestIntegrationTests();
                return dataIntegrationTests;
            }
        }


        private static void InitRestIntegrationTests()
        {
            dataIntegrationTests = new Dictionary<string, DomainControllerTestData>();

            // Get Provider by Key
            dataIntegrationTests.Add("SampleByInt", new DomainControllerTestData()
            {
              
                // TODO: Once we refactor DataModel classes, we can share them with consumers if 
                //       that is helpful.

                ExpectedDataModel = new List<DataModel>()
                {
                    new MyNewObject()
                    { }
                },
                QueryModel = new QueryModel()
                {
                    DataModel  = "SampleDataModel",
                    Parameters = new Dictionary<string, object>()
                    {
                        {  "Id","000000001" }
                    },
                }
            });

          
        }
    }

    public class DomainControllerTestData
    {
        
        public QueryModel QueryModel;
        public List<DataModel> ExpectedDataModel { get; set; }

    }

}

