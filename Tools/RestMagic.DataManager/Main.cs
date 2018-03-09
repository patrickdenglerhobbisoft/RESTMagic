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
        public Main()
        {
            InitializeComponent();
            LoadAllData();
        }

        private void LoadAllData()
        {
            dataModelDetailsTableAdapter = new Data.MetaDataTableAdapters.DataModelDetailsTableAdapter();
            dataModelDetailsTableAdapter.Fill(metaData.DataModelDetails);
        }
    }
}
