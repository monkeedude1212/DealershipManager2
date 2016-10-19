namespace DealershipManager2._0
{
    partial class ReportViewerForm
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.Current_VehiclesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DealershipManagerDataSet = new DealershipManager2._0.DealershipManagerDataSet();
            this.TmpCalculationBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DealershipManagerDataSet1 = new DealershipManager2._0.DealershipManagerDataSet1();
            this.rptviewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.CustomerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.CustomerTableAdapter = new DealershipManager2._0.DealershipManagerDataSetTableAdapters.CustomerTableAdapter();
            this.FinancialReport = new System.Windows.Forms.BindingSource(this.components);
            this.financeTableAdapter = new DealershipManager2._0.DealershipManagerDataSetTableAdapters.FinanceTableAdapter();
            this.saleTableAdapter = new DealershipManager2._0.DealershipManagerDataSetTableAdapters.SaleTableAdapter();
            this.leaseTableAdapter = new DealershipManager2._0.DealershipManagerDataSetTableAdapters.LeaseTableAdapter();
            this.leasePaymentsTableAdapter = new DealershipManager2._0.DealershipManagerDataSetTableAdapters.LeasePaymentsTableAdapter();
            this.vehicleTableAdapter = new DealershipManager2._0.DealershipManagerDataSetTableAdapters.VehicleTableAdapter();
            this.TmpCalculationTableAdapter = new DealershipManager2._0.DealershipManagerDataSet1TableAdapters.TmpCalculationTableAdapter();
            this.Current_VehiclesTableAdapter = new DealershipManager2._0.DealershipManagerDataSetTableAdapters.Current_VehiclesTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.Current_VehiclesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DealershipManagerDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TmpCalculationBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DealershipManagerDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CustomerBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FinancialReport)).BeginInit();
            this.SuspendLayout();
            // 
            // Current_VehiclesBindingSource
            // 
            this.Current_VehiclesBindingSource.DataMember = "Current_Vehicles";
            this.Current_VehiclesBindingSource.DataSource = this.DealershipManagerDataSet;
            // 
            // DealershipManagerDataSet
            // 
            this.DealershipManagerDataSet.DataSetName = "DealershipManagerDataSet";
            this.DealershipManagerDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // TmpCalculationBindingSource
            // 
            this.TmpCalculationBindingSource.DataMember = "TmpCalculation";
            this.TmpCalculationBindingSource.DataSource = this.DealershipManagerDataSet1;
            // 
            // DealershipManagerDataSet1
            // 
            this.DealershipManagerDataSet1.DataSetName = "DealershipManagerDataSet1";
            this.DealershipManagerDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // rptviewer
            // 
            reportDataSource1.Name = "TempCalcRows";
            reportDataSource1.Value = this.TmpCalculationBindingSource;
            this.rptviewer.LocalReport.DataSources.Add(reportDataSource1);
            this.rptviewer.LocalReport.ReportEmbeddedResource = "DealershipManager2._0.TempCalculationReport.rdlc";
            this.rptviewer.Location = new System.Drawing.Point(12, 12);
            this.rptviewer.Name = "rptviewer";
            this.rptviewer.Size = new System.Drawing.Size(775, 671);
            this.rptviewer.TabIndex = 5;
            this.rptviewer.Visible = false;
            // 
            // CustomerBindingSource
            // 
            this.CustomerBindingSource.DataMember = "Customer";
            this.CustomerBindingSource.DataSource = this.DealershipManagerDataSet;
            // 
            // CustomerTableAdapter
            // 
            this.CustomerTableAdapter.ClearBeforeFill = true;
            // 
            // FinancialReport
            // 
            this.FinancialReport.DataMember = "Finance";
            this.FinancialReport.DataSource = this.DealershipManagerDataSet;
            // 
            // financeTableAdapter
            // 
            this.financeTableAdapter.ClearBeforeFill = true;
            // 
            // saleTableAdapter
            // 
            this.saleTableAdapter.ClearBeforeFill = true;
            // 
            // leaseTableAdapter
            // 
            this.leaseTableAdapter.ClearBeforeFill = true;
            // 
            // leasePaymentsTableAdapter
            // 
            this.leasePaymentsTableAdapter.ClearBeforeFill = true;
            // 
            // vehicleTableAdapter
            // 
            this.vehicleTableAdapter.ClearBeforeFill = true;
            // 
            // TmpCalculationTableAdapter
            // 
            this.TmpCalculationTableAdapter.ClearBeforeFill = true;
            // 
            // Current_VehiclesTableAdapter
            // 
            this.Current_VehiclesTableAdapter.ClearBeforeFill = true;
            // 
            // ReportViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 713);
            this.Controls.Add(this.rptviewer);
            this.Name = "ReportViewerForm";
            this.Text = "ReportViewerForm";
            this.Load += new System.EventHandler(this.ReportViewerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Current_VehiclesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DealershipManagerDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TmpCalculationBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DealershipManagerDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CustomerBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FinancialReport)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public Microsoft.Reporting.WinForms.ReportViewer rptviewer;
        private System.Windows.Forms.BindingSource CustomerBindingSource;
        private DealershipManagerDataSet DealershipManagerDataSet;
        private DealershipManagerDataSetTableAdapters.CustomerTableAdapter CustomerTableAdapter;
        private System.Windows.Forms.BindingSource TmpCalculationBindingSource;
        private DealershipManagerDataSet1 DealershipManagerDataSet1;
        private DealershipManagerDataSet1TableAdapters.TmpCalculationTableAdapter TmpCalculationTableAdapter;
        private System.Windows.Forms.BindingSource FinancialReport;
        private DealershipManagerDataSetTableAdapters.FinanceTableAdapter financeTableAdapter;
        private DealershipManagerDataSetTableAdapters.CustomerTableAdapter customerTableAdapter;
        private DealershipManagerDataSetTableAdapters.VehicleTableAdapter vehicleTableAdapter;
        private DealershipManagerDataSetTableAdapters.SaleTableAdapter saleTableAdapter;
        private DealershipManagerDataSetTableAdapters.LeaseTableAdapter leaseTableAdapter;
        private DealershipManagerDataSetTableAdapters.LeasePaymentsTableAdapter leasePaymentsTableAdapter;
        private System.Windows.Forms.BindingSource Current_VehiclesBindingSource;
        private DealershipManagerDataSetTableAdapters.Current_VehiclesTableAdapter Current_VehiclesTableAdapter;

    }
}