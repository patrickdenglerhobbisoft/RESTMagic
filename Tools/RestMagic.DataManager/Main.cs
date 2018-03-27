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
using RestMagic.Lib.Language;
using System.Data.SqlClient;
using RestMagic.DataManager.Data.MetaDataTableAdapters;

namespace RestMagic.DataManager
{
    public partial class Main : Form
    {
         DataModelDetailsTableAdapter dataModelDetailsTableAdapter;
         DataModelsTableAdapter dataModelTableAdapter;
        string connectionString = string.Empty;
        public Main()
        {
            InitializeComponent();
            LoadAllData();
            
        }

        private void LoadAllData()
        {
            SqlConnection sqlConnection = new SqlConnection(Helpers.InitConnectionString());

            dataModelDetailsTableAdapter = new  DataModelDetailsTableAdapter();
                 dataModelDetailsTableAdapter.Fill(metaData.DataModelDetails);

            dataModelTableAdapter = new  DataModelsTableAdapter();
     
            dataModelTableAdapter.Fill(metaData.DataModels);

            foreach (MetaData.DataModelsRow row in metaData.DataModels.Rows)
            {
                chkModelGenerate.Items.Add(row.DataModelName.ToString(),true);
                listModelTest.Items.Add(row.DataModelName.ToString());
            }
            sqlConnection.Close();
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

        private void listModelTest_SelectedValueChanged(object sender, EventArgs e)
        {
           
            string typeSelected = listModelTest.SelectedItem.ToString();
            var objectToTarget = ReflectionHelper.GetObjectTypeInstance(typeSelected,ReflectionHelper.SDK_ASSEMBLY_NAMESPACE);
            if (objectToTarget != null)
            {
                propertyGridModel.SelectedObject = objectToTarget;
            }
            else
            {
                MessageBox.Show("Couldn't find type selected in assembly " + ReflectionHelper.SDK_ASSEMBLY_NAMESPACE);
            }
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show("Couldn't find type selected in assembly " + ReflectionHelper.SDK_ASSEMBLY_NAMESPACE);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {

            }
            else
            {

            }
        }
    }
}
