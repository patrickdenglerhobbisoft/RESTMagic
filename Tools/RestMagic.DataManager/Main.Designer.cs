﻿namespace RestMagic.DataManager
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataModelNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataFieldNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sourceTableNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sourceFieldNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsQueryable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.metaData = new RestMagic.DataManager.Data.MetaData();
            this.linkNewModel = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.listModelSchema = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.listModelTest = new System.Windows.Forms.ComboBox();
            this.propertyGridModel = new System.Windows.Forms.PropertyGrid();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.txtResults = new System.Windows.Forms.TextBox();
            this.btnProcess = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.chkModelGenerate = new System.Windows.Forms.CheckedListBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.metaData)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(793, 582);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Controls.Add(this.linkNewModel);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.listModelSchema);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(785, 553);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Schema to Model Mapping";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataModelNameDataGridViewTextBoxColumn,
            this.dataFieldNameDataGridViewTextBoxColumn,
            this.sourceTableNameDataGridViewTextBoxColumn,
            this.sourceFieldNameDataGridViewTextBoxColumn,
            this.IsQueryable});
            this.dataGridView1.DataMember = "DataModelDetails";
            this.dataGridView1.DataSource = this.metaData;
            this.dataGridView1.Location = new System.Drawing.Point(31, 62);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(730, 466);
            this.dataGridView1.TabIndex = 3;
            // 
            // dataModelNameDataGridViewTextBoxColumn
            // 
            this.dataModelNameDataGridViewTextBoxColumn.DataPropertyName = "DataModelName";
            this.dataModelNameDataGridViewTextBoxColumn.HeaderText = "Model Name";
            this.dataModelNameDataGridViewTextBoxColumn.MinimumWidth = 150;
            this.dataModelNameDataGridViewTextBoxColumn.Name = "dataModelNameDataGridViewTextBoxColumn";
            this.dataModelNameDataGridViewTextBoxColumn.Width = 150;
            // 
            // dataFieldNameDataGridViewTextBoxColumn
            // 
            this.dataFieldNameDataGridViewTextBoxColumn.DataPropertyName = "DataFieldName";
            this.dataFieldNameDataGridViewTextBoxColumn.HeaderText = "Model Field Name";
            this.dataFieldNameDataGridViewTextBoxColumn.Name = "dataFieldNameDataGridViewTextBoxColumn";
            this.dataFieldNameDataGridViewTextBoxColumn.Width = 150;
            // 
            // sourceTableNameDataGridViewTextBoxColumn
            // 
            this.sourceTableNameDataGridViewTextBoxColumn.DataPropertyName = "SourceTableName";
            this.sourceTableNameDataGridViewTextBoxColumn.HeaderText = "Source Table Name";
            this.sourceTableNameDataGridViewTextBoxColumn.Name = "sourceTableNameDataGridViewTextBoxColumn";
            this.sourceTableNameDataGridViewTextBoxColumn.Width = 180;
            // 
            // sourceFieldNameDataGridViewTextBoxColumn
            // 
            this.sourceFieldNameDataGridViewTextBoxColumn.DataPropertyName = "SourceFieldName";
            this.sourceFieldNameDataGridViewTextBoxColumn.HeaderText = "Source Field Name";
            this.sourceFieldNameDataGridViewTextBoxColumn.Name = "sourceFieldNameDataGridViewTextBoxColumn";
            this.sourceFieldNameDataGridViewTextBoxColumn.Width = 180;
            // 
            // IsQueryable
            // 
            this.IsQueryable.DataPropertyName = "IsQueryable";
            this.IsQueryable.HeaderText = "Queryable";
            this.IsQueryable.Name = "IsQueryable";
            // 
            // metaData
            // 
            this.metaData.DataSetName = "MetaData";
            this.metaData.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // linkNewModel
            // 
            this.linkNewModel.AutoSize = true;
            this.linkNewModel.Enabled = false;
            this.linkNewModel.Location = new System.Drawing.Point(275, 24);
            this.linkNewModel.Name = "linkNewModel";
            this.linkNewModel.Size = new System.Drawing.Size(106, 17);
            this.linkNewModel.TabIndex = 2;
            this.linkNewModel.TabStop = true;
            this.linkNewModel.Text = "Add New Model";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Enabled = false;
            this.label1.Location = new System.Drawing.Point(28, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Filter By Model";
            // 
            // listModelSchema
            // 
            this.listModelSchema.DataSource = this.metaData;
            this.listModelSchema.DisplayMember = "DataModels.DataModelName";
            this.listModelSchema.Enabled = false;
            this.listModelSchema.FormattingEnabled = true;
            this.listModelSchema.Location = new System.Drawing.Point(133, 21);
            this.listModelSchema.Name = "listModelSchema";
            this.listModelSchema.Size = new System.Drawing.Size(121, 24);
            this.listModelSchema.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.listModelTest);
            this.tabPage2.Controls.Add(this.propertyGridModel);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(785, 553);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Testing";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // listModelTest
            // 
            this.listModelTest.FormattingEnabled = true;
            this.listModelTest.Location = new System.Drawing.Point(20, 20);
            this.listModelTest.Name = "listModelTest";
            this.listModelTest.Size = new System.Drawing.Size(121, 24);
            this.listModelTest.TabIndex = 1;
            this.listModelTest.SelectedValueChanged += new System.EventHandler(this.listModelTest_SelectedValueChanged);
            // 
            // propertyGridModel
            // 
            this.propertyGridModel.HelpVisible = false;
            this.propertyGridModel.Location = new System.Drawing.Point(20, 67);
            this.propertyGridModel.Name = "propertyGridModel";
            this.propertyGridModel.Size = new System.Drawing.Size(338, 328);
            this.propertyGridModel.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.txtResults);
            this.tabPage3.Controls.Add(this.btnProcess);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.chkModelGenerate);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(785, 553);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Generate REST Interfaces";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // txtResults
            // 
            this.txtResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResults.Location = new System.Drawing.Point(261, 56);
            this.txtResults.Multiline = true;
            this.txtResults.Name = "txtResults";
            this.txtResults.Size = new System.Drawing.Size(499, 480);
            this.txtResults.TabIndex = 2;
            // 
            // btnProcess
            // 
            this.btnProcess.Location = new System.Drawing.Point(261, 19);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(75, 23);
            this.btnProcess.TabIndex = 1;
            this.btnProcess.Text = "Start";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(181, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Select Models To Generate";
            // 
            // chkModelGenerate
            // 
            this.chkModelGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.chkModelGenerate.DataBindings.Add(new System.Windows.Forms.Binding("SelectedItem", this.metaData, "DataModels.DataModelName", true));
            this.chkModelGenerate.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.metaData, "DataModels.DataModelName", true));
            this.chkModelGenerate.FormattingEnabled = true;
            this.chkModelGenerate.Location = new System.Drawing.Point(24, 56);
            this.chkModelGenerate.Margin = new System.Windows.Forms.Padding(5);
            this.chkModelGenerate.Name = "chkModelGenerate";
            this.chkModelGenerate.Size = new System.Drawing.Size(208, 480);
            this.chkModelGenerate.TabIndex = 0;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 582);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.Text = "RestMagic Data Manager";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.metaData)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.LinkLabel linkNewModel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox listModelSchema;
        private System.Windows.Forms.ComboBox listModelTest;
        private System.Windows.Forms.PropertyGrid propertyGridModel;
        private Data.MetaData metaData;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataModelNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataFieldNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sourceTableNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sourceFieldNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsQueryable;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckedListBox chkModelGenerate;
        private System.Windows.Forms.TextBox txtResults;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.Label label2;
    }
}

