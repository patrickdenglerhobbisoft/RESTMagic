using RestMagic.DataManager.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestMagic.DataManager;
namespace RestMagic.DataManager
{
    public partial class Main : Form
    {
        Data.MetaDataTableAdapters.DataModelDetailsTableAdapter dataModelDetailsTableAdapter;
        Data.MetaDataTableAdapters.DataModelsTableAdapter dataModelTableAdapter;
        public Main()
        {
            InitializeComponent();
            LoadAllData();
            
        }

        private void LoadAllData()
        {
            dataModelDetailsTableAdapter = new Data.MetaDataTableAdapters.DataModelDetailsTableAdapter();
            dataModelDetailsTableAdapter.Fill(metaData.DataModelDetails);

            dataModelTableAdapter = new Data.MetaDataTableAdapters.DataModelsTableAdapter();
            dataModelTableAdapter.Fill(metaData.DataModels);

            foreach (MetaData.DataModelsRow row in metaData.DataModels.Rows)
            {
                chkModelGenerate.Items.Add(row.DataModelName.ToString(),true);
                
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            List<string> modelsToProcess = new List<string>();
            foreach (var item in chkModelGenerate.CheckedItems)
            {
                modelsToProcess.Add(item.ToString());
            }
            Processor processor = new Processor();
            processor.Generate(modelsToProcess.ToArray(), metaData);

        }

      
    }
}
