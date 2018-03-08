using RestMagic.RestService.Models;
using System.Collections.Generic;
using RestMagic.Lib.Data;

namespace RestMagic.Platform.UnitTests.Helpers
{
    class IntegrationTestHelpers
    {
        private static Dictionary<string, DomainControllerTestData> restIntegrationTestList = null;

        public static Dictionary<string, DomainControllerTestData> TestList
        {
            get
            {
                if (restIntegrationTestList == null) InitRestIntegrationTests();
                return restIntegrationTestList;
            }
        }


        private static void InitRestIntegrationTests()
        {
            restIntegrationTestList = new Dictionary<string, DomainControllerTestData>();

            // Get Provider by Key
            restIntegrationTestList.Add("SampleByInt", new DomainControllerTestData()
            {
                DataModelName = "SampleData",
                // TODO: Once we refactor DataModel classes, we can share them with consumers if 
                //       that is helpful.

                ExpectedDataModel = new List<DataModel>()
                {
                    new SampleDataModel()
                    { }
                },
                QueryModel = new QueryModel()
                {
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
        public string DataModelName { get; set; }
        public QueryModel QueryModel;
        public List<DataModel> ExpectedDataModel { get; set; }

    }

}

