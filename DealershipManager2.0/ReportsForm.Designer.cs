namespace DealershipManager2._0
{
    partial class ReportsForm
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.RecentPaymentsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DealershipManagerDataSetRecentPayments = new DealershipManager2._0.DealershipManagerDataSetRecentPayments();
            this.customerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dealershipManagerDataSet = new DealershipManager2._0.DealershipManagerDataSet();
            this.SalesReports = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnLeaseStatistics = new System.Windows.Forms.Button();
            this.btnLeaseReport = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnSaleStatistics = new System.Windows.Forms.Button();
            this.btnSalesReport = new System.Windows.Forms.Button();
            this.dtpSalesEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpSalesStart = new System.Windows.Forms.DateTimePicker();
            this.VehicleReports = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnVehicleLienReport = new System.Windows.Forms.Button();
            this.btnVehicleRequestReport = new System.Windows.Forms.Button();
            this.btnDetailedInventoryReport = new System.Windows.Forms.Button();
            this.btnBasicInventoryReport = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtOdometer = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtYear = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMake = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtVIN = new System.Windows.Forms.TextBox();
            this.cboVehicle = new System.Windows.Forms.ComboBox();
            this.vehicleBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnVehicleInformationReport = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.CustomerReports = new System.Windows.Forms.TabPage();
            this.dgvReport = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCustomerListReport = new System.Windows.Forms.Button();
            this.btnCustomerInsuranceReport = new System.Windows.Forms.Button();
            this.btnCustomerCompleteFinanceReport = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRecentPaymentReport = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpCustomerEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpCustomerStart = new System.Windows.Forms.DateTimePicker();
            this.tabReports = new System.Windows.Forms.TabControl();
            this.ReportViewer = new System.Windows.Forms.TabPage();
            this.RecentPaymentReport = new Microsoft.Reporting.WinForms.ReportViewer();
            this.customerReport = new Microsoft.Reporting.WinForms.ReportViewer();
            this.customerTableAdapter = new DealershipManager2._0.DealershipManagerDataSetTableAdapters.CustomerTableAdapter();
            this.RecentPaymentsTableAdapter = new DealershipManager2._0.DealershipManagerDataSetRecentPaymentsTableAdapters.RecentPaymentsTableAdapter();
            this.vehicleTableAdapter = new DealershipManager2._0.DealershipManagerDataSetTableAdapters.VehicleTableAdapter();
            this.fillBy1ToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saleBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.saleTableAdapter = new DealershipManager2._0.DealershipManagerDataSetTableAdapters.SaleTableAdapter();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            ((System.ComponentModel.ISupportInitialize)(this.RecentPaymentsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DealershipManagerDataSetRecentPayments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customerBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dealershipManagerDataSet)).BeginInit();
            this.SalesReports.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.VehicleReports.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vehicleBindingSource)).BeginInit();
            this.CustomerReports.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabReports.SuspendLayout();
            this.ReportViewer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.saleBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // RecentPaymentsBindingSource
            // 
            this.RecentPaymentsBindingSource.DataMember = "RecentPayments";
            this.RecentPaymentsBindingSource.DataSource = this.DealershipManagerDataSetRecentPayments;
            // 
            // DealershipManagerDataSetRecentPayments
            // 
            this.DealershipManagerDataSetRecentPayments.DataSetName = "DealershipManagerDataSetRecentPayments";
            this.DealershipManagerDataSetRecentPayments.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // customerBindingSource
            // 
            this.customerBindingSource.DataMember = "Customer";
            this.customerBindingSource.DataSource = this.dealershipManagerDataSet;
            // 
            // dealershipManagerDataSet
            // 
            this.dealershipManagerDataSet.DataSetName = "DealershipManagerDataSet";
            this.dealershipManagerDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // SalesReports
            // 
            this.SalesReports.Controls.Add(this.groupBox6);
            this.SalesReports.Controls.Add(this.groupBox5);
            this.SalesReports.Location = new System.Drawing.Point(4, 22);
            this.SalesReports.Name = "SalesReports";
            this.SalesReports.Size = new System.Drawing.Size(954, 678);
            this.SalesReports.TabIndex = 2;
            this.SalesReports.Text = "Sales Reports";
            this.SalesReports.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Location = new System.Drawing.Point(341, 25);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(610, 641);
            this.groupBox6.TabIndex = 1;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Sales Statistics";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnLeaseStatistics);
            this.groupBox5.Controls.Add(this.btnLeaseReport);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.btnSaleStatistics);
            this.groupBox5.Controls.Add(this.btnSalesReport);
            this.groupBox5.Controls.Add(this.dtpSalesEnd);
            this.groupBox5.Controls.Add(this.dtpSalesStart);
            this.groupBox5.Location = new System.Drawing.Point(15, 25);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(320, 641);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Printable Reports";
            // 
            // btnLeaseStatistics
            // 
            this.btnLeaseStatistics.Location = new System.Drawing.Point(186, 508);
            this.btnLeaseStatistics.Name = "btnLeaseStatistics";
            this.btnLeaseStatistics.Size = new System.Drawing.Size(92, 38);
            this.btnLeaseStatistics.TabIndex = 8;
            this.btnLeaseStatistics.Text = "Lease Statistics";
            this.btnLeaseStatistics.UseVisualStyleBackColor = true;
            // 
            // btnLeaseReport
            // 
            this.btnLeaseReport.Location = new System.Drawing.Point(114, 180);
            this.btnLeaseReport.Name = "btnLeaseReport";
            this.btnLeaseReport.Size = new System.Drawing.Size(126, 38);
            this.btnLeaseReport.TabIndex = 7;
            this.btnLeaseReport.Text = "Lease Report";
            this.btnLeaseReport.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(50, 454);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(228, 38);
            this.label11.TabIndex = 6;
            this.label11.Text = "Note that the sale/lease statistics displayed in this graph are indicitive of pro" +
    "fts only";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(53, 74);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "End Date:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(50, 48);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(58, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Start Date:";
            // 
            // btnSaleStatistics
            // 
            this.btnSaleStatistics.Location = new System.Drawing.Point(53, 508);
            this.btnSaleStatistics.Name = "btnSaleStatistics";
            this.btnSaleStatistics.Size = new System.Drawing.Size(92, 38);
            this.btnSaleStatistics.TabIndex = 3;
            this.btnSaleStatistics.Text = "Sale Statistics";
            this.btnSaleStatistics.UseVisualStyleBackColor = true;
            // 
            // btnSalesReport
            // 
            this.btnSalesReport.Location = new System.Drawing.Point(114, 136);
            this.btnSalesReport.Name = "btnSalesReport";
            this.btnSalesReport.Size = new System.Drawing.Size(126, 38);
            this.btnSalesReport.TabIndex = 2;
            this.btnSalesReport.Text = "Sales Report";
            this.btnSalesReport.UseVisualStyleBackColor = true;
            // 
            // dtpSalesEnd
            // 
            this.dtpSalesEnd.Location = new System.Drawing.Point(114, 68);
            this.dtpSalesEnd.Name = "dtpSalesEnd";
            this.dtpSalesEnd.Size = new System.Drawing.Size(200, 20);
            this.dtpSalesEnd.TabIndex = 1;
            // 
            // dtpSalesStart
            // 
            this.dtpSalesStart.Location = new System.Drawing.Point(114, 42);
            this.dtpSalesStart.Name = "dtpSalesStart";
            this.dtpSalesStart.Size = new System.Drawing.Size(200, 20);
            this.dtpSalesStart.TabIndex = 0;
            // 
            // VehicleReports
            // 
            this.VehicleReports.Controls.Add(this.groupBox3);
            this.VehicleReports.Controls.Add(this.groupBox4);
            this.VehicleReports.Location = new System.Drawing.Point(4, 22);
            this.VehicleReports.Name = "VehicleReports";
            this.VehicleReports.Padding = new System.Windows.Forms.Padding(3);
            this.VehicleReports.Size = new System.Drawing.Size(954, 678);
            this.VehicleReports.TabIndex = 1;
            this.VehicleReports.Text = "Vehicle Reports";
            this.VehicleReports.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnVehicleLienReport);
            this.groupBox3.Controls.Add(this.btnVehicleRequestReport);
            this.groupBox3.Controls.Add(this.btnDetailedInventoryReport);
            this.groupBox3.Controls.Add(this.btnBasicInventoryReport);
            this.groupBox3.Location = new System.Drawing.Point(415, 79);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(385, 261);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Vehicle Reports";
            // 
            // btnVehicleLienReport
            // 
            this.btnVehicleLienReport.Location = new System.Drawing.Point(110, 144);
            this.btnVehicleLienReport.Name = "btnVehicleLienReport";
            this.btnVehicleLienReport.Size = new System.Drawing.Size(177, 31);
            this.btnVehicleLienReport.TabIndex = 8;
            this.btnVehicleLienReport.Text = "Vehicle Lien Report";
            this.btnVehicleLienReport.UseVisualStyleBackColor = true;
            // 
            // btnVehicleRequestReport
            // 
            this.btnVehicleRequestReport.Location = new System.Drawing.Point(110, 107);
            this.btnVehicleRequestReport.Name = "btnVehicleRequestReport";
            this.btnVehicleRequestReport.Size = new System.Drawing.Size(177, 31);
            this.btnVehicleRequestReport.TabIndex = 7;
            this.btnVehicleRequestReport.Text = "Vehicle Request Report";
            this.btnVehicleRequestReport.UseVisualStyleBackColor = true;
            // 
            // btnDetailedInventoryReport
            // 
            this.btnDetailedInventoryReport.Location = new System.Drawing.Point(110, 70);
            this.btnDetailedInventoryReport.Name = "btnDetailedInventoryReport";
            this.btnDetailedInventoryReport.Size = new System.Drawing.Size(177, 31);
            this.btnDetailedInventoryReport.TabIndex = 6;
            this.btnDetailedInventoryReport.Text = "Detailed Inventory Report";
            this.btnDetailedInventoryReport.UseVisualStyleBackColor = true;
            // 
            // btnBasicInventoryReport
            // 
            this.btnBasicInventoryReport.Location = new System.Drawing.Point(110, 33);
            this.btnBasicInventoryReport.Name = "btnBasicInventoryReport";
            this.btnBasicInventoryReport.Size = new System.Drawing.Size(177, 31);
            this.btnBasicInventoryReport.TabIndex = 5;
            this.btnBasicInventoryReport.Text = "Basic Inventory Report";
            this.btnBasicInventoryReport.UseVisualStyleBackColor = true;
            this.btnBasicInventoryReport.Click += new System.EventHandler(this.btnBasicInventoryReport_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtOdometer);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.txtModel);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.txtYear);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.txtMake);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.txtVIN);
            this.groupBox4.Controls.Add(this.cboVehicle);
            this.groupBox4.Controls.Add(this.btnVehicleInformationReport);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Location = new System.Drawing.Point(24, 79);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(388, 261);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Vehicles in Stock";
            // 
            // txtOdometer
            // 
            this.txtOdometer.Enabled = false;
            this.txtOdometer.Location = new System.Drawing.Point(248, 124);
            this.txtOdometer.Name = "txtOdometer";
            this.txtOdometer.Size = new System.Drawing.Size(120, 20);
            this.txtOdometer.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(186, 127);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Odometer:";
            // 
            // txtModel
            // 
            this.txtModel.Enabled = false;
            this.txtModel.Location = new System.Drawing.Point(50, 124);
            this.txtModel.Name = "txtModel";
            this.txtModel.Size = new System.Drawing.Size(120, 20);
            this.txtModel.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 127);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Model:";
            // 
            // txtYear
            // 
            this.txtYear.Enabled = false;
            this.txtYear.Location = new System.Drawing.Point(248, 98);
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(120, 20);
            this.txtYear.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(210, 101);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Year:";
            // 
            // txtMake
            // 
            this.txtMake.Enabled = false;
            this.txtMake.Location = new System.Drawing.Point(50, 98);
            this.txtMake.Name = "txtMake";
            this.txtMake.Size = new System.Drawing.Size(120, 20);
            this.txtMake.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 101);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Make:";
            // 
            // txtVIN
            // 
            this.txtVIN.Enabled = false;
            this.txtVIN.Location = new System.Drawing.Point(50, 60);
            this.txtVIN.Name = "txtVIN";
            this.txtVIN.Size = new System.Drawing.Size(318, 20);
            this.txtVIN.TabIndex = 6;
            // 
            // cboVehicle
            // 
            this.cboVehicle.DataSource = this.vehicleBindingSource;
            this.cboVehicle.DisplayMember = "Id";
            this.cboVehicle.FormattingEnabled = true;
            this.cboVehicle.Location = new System.Drawing.Point(109, 33);
            this.cboVehicle.Name = "cboVehicle";
            this.cboVehicle.Size = new System.Drawing.Size(259, 21);
            this.cboVehicle.TabIndex = 5;
            this.cboVehicle.ValueMember = "Id";
            this.cboVehicle.SelectedIndexChanged += new System.EventHandler(this.cboVehicle_SelectedIndexChanged);
            this.cboVehicle.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.cboVehicle_Format);
            // 
            // vehicleBindingSource
            // 
            this.vehicleBindingSource.DataMember = "Vehicle";
            this.vehicleBindingSource.DataSource = this.dealershipManagerDataSet;
            // 
            // btnVehicleInformationReport
            // 
            this.btnVehicleInformationReport.Location = new System.Drawing.Point(74, 204);
            this.btnVehicleInformationReport.Name = "btnVehicleInformationReport";
            this.btnVehicleInformationReport.Size = new System.Drawing.Size(227, 31);
            this.btnVehicleInformationReport.TabIndex = 4;
            this.btnVehicleInformationReport.Text = "Vehicle Information Report";
            this.btnVehicleInformationReport.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "VIN:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Select a Vehicle:";
            // 
            // CustomerReports
            // 
            this.CustomerReports.Controls.Add(this.dgvReport);
            this.CustomerReports.Controls.Add(this.groupBox2);
            this.CustomerReports.Controls.Add(this.groupBox1);
            this.CustomerReports.Location = new System.Drawing.Point(4, 22);
            this.CustomerReports.Name = "CustomerReports";
            this.CustomerReports.Padding = new System.Windows.Forms.Padding(3);
            this.CustomerReports.Size = new System.Drawing.Size(954, 678);
            this.CustomerReports.TabIndex = 0;
            this.CustomerReports.Text = "Customer Reports";
            this.CustomerReports.UseVisualStyleBackColor = true;
            // 
            // dgvReport
            // 
            this.dgvReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReport.Location = new System.Drawing.Point(24, 385);
            this.dgvReport.Name = "dgvReport";
            this.dgvReport.Size = new System.Drawing.Size(776, 260);
            this.dgvReport.TabIndex = 2;
            this.dgvReport.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnCustomerListReport);
            this.groupBox2.Controls.Add(this.btnCustomerInsuranceReport);
            this.groupBox2.Controls.Add(this.btnCustomerCompleteFinanceReport);
            this.groupBox2.Location = new System.Drawing.Point(415, 79);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(385, 261);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // btnCustomerListReport
            // 
            this.btnCustomerListReport.Location = new System.Drawing.Point(110, 135);
            this.btnCustomerListReport.Name = "btnCustomerListReport";
            this.btnCustomerListReport.Size = new System.Drawing.Size(177, 31);
            this.btnCustomerListReport.TabIndex = 7;
            this.btnCustomerListReport.Text = "Customer List";
            this.btnCustomerListReport.UseVisualStyleBackColor = true;
            this.btnCustomerListReport.Click += new System.EventHandler(this.btnCustomerListReport_Click);
            // 
            // btnCustomerInsuranceReport
            // 
            this.btnCustomerInsuranceReport.Location = new System.Drawing.Point(110, 98);
            this.btnCustomerInsuranceReport.Name = "btnCustomerInsuranceReport";
            this.btnCustomerInsuranceReport.Size = new System.Drawing.Size(177, 31);
            this.btnCustomerInsuranceReport.TabIndex = 6;
            this.btnCustomerInsuranceReport.Text = "Insurance Report";
            this.btnCustomerInsuranceReport.UseVisualStyleBackColor = true;
            // 
            // btnCustomerCompleteFinanceReport
            // 
            this.btnCustomerCompleteFinanceReport.Location = new System.Drawing.Point(110, 61);
            this.btnCustomerCompleteFinanceReport.Name = "btnCustomerCompleteFinanceReport";
            this.btnCustomerCompleteFinanceReport.Size = new System.Drawing.Size(177, 31);
            this.btnCustomerCompleteFinanceReport.TabIndex = 5;
            this.btnCustomerCompleteFinanceReport.Text = "Complete Finance Report";
            this.btnCustomerCompleteFinanceReport.UseVisualStyleBackColor = true;
            this.btnCustomerCompleteFinanceReport.Click += new System.EventHandler(this.btnCustomerCompleteFinanceReport_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnRecentPaymentReport);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtpCustomerEnd);
            this.groupBox1.Controls.Add(this.dtpCustomerStart);
            this.groupBox1.Location = new System.Drawing.Point(24, 79);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(385, 261);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnRecentPaymentReport
            // 
            this.btnRecentPaymentReport.Location = new System.Drawing.Point(109, 145);
            this.btnRecentPaymentReport.Name = "btnRecentPaymentReport";
            this.btnRecentPaymentReport.Size = new System.Drawing.Size(137, 31);
            this.btnRecentPaymentReport.TabIndex = 4;
            this.btnRecentPaymentReport.Text = "Recent Payment Report";
            this.btnRecentPaymentReport.UseVisualStyleBackColor = true;
            this.btnRecentPaymentReport.Click += new System.EventHandler(this.btnRecentPaymentReport_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "End Date:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Start Date:";
            // 
            // dtpCustomerEnd
            // 
            this.dtpCustomerEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpCustomerEnd.Location = new System.Drawing.Point(109, 99);
            this.dtpCustomerEnd.Name = "dtpCustomerEnd";
            this.dtpCustomerEnd.Size = new System.Drawing.Size(200, 20);
            this.dtpCustomerEnd.TabIndex = 1;
            // 
            // dtpCustomerStart
            // 
            this.dtpCustomerStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpCustomerStart.Location = new System.Drawing.Point(109, 61);
            this.dtpCustomerStart.Name = "dtpCustomerStart";
            this.dtpCustomerStart.Size = new System.Drawing.Size(200, 20);
            this.dtpCustomerStart.TabIndex = 0;
            // 
            // tabReports
            // 
            this.tabReports.Controls.Add(this.CustomerReports);
            this.tabReports.Controls.Add(this.VehicleReports);
            this.tabReports.Controls.Add(this.SalesReports);
            this.tabReports.Controls.Add(this.ReportViewer);
            this.tabReports.Location = new System.Drawing.Point(12, 14);
            this.tabReports.Name = "tabReports";
            this.tabReports.SelectedIndex = 0;
            this.tabReports.Size = new System.Drawing.Size(962, 704);
            this.tabReports.TabIndex = 0;
            // 
            // ReportViewer
            // 
            this.ReportViewer.Controls.Add(this.RecentPaymentReport);
            this.ReportViewer.Controls.Add(this.customerReport);
            this.ReportViewer.Location = new System.Drawing.Point(4, 22);
            this.ReportViewer.Name = "ReportViewer";
            this.ReportViewer.Padding = new System.Windows.Forms.Padding(3);
            this.ReportViewer.Size = new System.Drawing.Size(954, 678);
            this.ReportViewer.TabIndex = 3;
            this.ReportViewer.Text = "ReportViewer";
            this.ReportViewer.UseVisualStyleBackColor = true;
            // 
            // RecentPaymentReport
            // 
            reportDataSource1.Name = "RecentPaymentsDataSet";
            reportDataSource1.Value = this.RecentPaymentsBindingSource;
            this.RecentPaymentReport.LocalReport.DataSources.Add(reportDataSource1);
            this.RecentPaymentReport.LocalReport.ReportEmbeddedResource = "DealershipManager2._0.RecentPaymentsReport.rdlc";
            this.RecentPaymentReport.Location = new System.Drawing.Point(0, 0);
            this.RecentPaymentReport.Name = "RecentPaymentReport";
            this.RecentPaymentReport.Size = new System.Drawing.Size(948, 678);
            this.RecentPaymentReport.TabIndex = 6;
            this.RecentPaymentReport.Visible = false;
            // 
            // customerReport
            // 
            reportDataSource2.Name = "CustomerDataSet";
            reportDataSource2.Value = this.customerBindingSource;
            this.customerReport.LocalReport.DataSources.Add(reportDataSource2);
            this.customerReport.LocalReport.ReportEmbeddedResource = "DealershipManager2._0.CustomerReport.rdlc";
            this.customerReport.Location = new System.Drawing.Point(0, 6);
            this.customerReport.Name = "customerReport";
            this.customerReport.Size = new System.Drawing.Size(948, 672);
            this.customerReport.TabIndex = 3;
            this.customerReport.Visible = false;
            // 
            // customerTableAdapter
            // 
            this.customerTableAdapter.ClearBeforeFill = true;
            // 
            // RecentPaymentsTableAdapter
            // 
            this.RecentPaymentsTableAdapter.ClearBeforeFill = true;
            // 
            // vehicleTableAdapter
            // 
            this.vehicleTableAdapter.ClearBeforeFill = true;
            // 
            // fillBy1ToolStripButton
            // 
            this.fillBy1ToolStripButton.Name = "fillBy1ToolStripButton";
            this.fillBy1ToolStripButton.Size = new System.Drawing.Size(23, 23);
            // 
            // saleBindingSource
            // 
            this.saleBindingSource.DataMember = "Sale";
            this.saleBindingSource.DataSource = this.dealershipManagerDataSet;
            // 
            // saleTableAdapter
            // 
            this.saleTableAdapter.ClearBeforeFill = true;
            // 
            // printDocument1
            // 
            this.printDocument1.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument1_BeginPrint);
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // ReportsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.tabReports);
            this.Name = "ReportsForm";
            this.Text = "ReportsForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ReportsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.RecentPaymentsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DealershipManagerDataSetRecentPayments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customerBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dealershipManagerDataSet)).EndInit();
            this.SalesReports.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.VehicleReports.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vehicleBindingSource)).EndInit();
            this.CustomerReports.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabReports.ResumeLayout(false);
            this.ReportViewer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.saleBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage SalesReports;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnLeaseStatistics;
        private System.Windows.Forms.Button btnLeaseReport;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnSaleStatistics;
        private System.Windows.Forms.Button btnSalesReport;
        private System.Windows.Forms.DateTimePicker dtpSalesEnd;
        private System.Windows.Forms.DateTimePicker dtpSalesStart;
        private System.Windows.Forms.TabPage VehicleReports;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnVehicleLienReport;
        private System.Windows.Forms.Button btnVehicleRequestReport;
        private System.Windows.Forms.Button btnDetailedInventoryReport;
        private System.Windows.Forms.Button btnBasicInventoryReport;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtOdometer;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtModel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtYear;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtMake;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtVIN;
        private System.Windows.Forms.ComboBox cboVehicle;
        private System.Windows.Forms.Button btnVehicleInformationReport;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabPage CustomerReports;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnCustomerListReport;
        private System.Windows.Forms.Button btnCustomerInsuranceReport;
        private System.Windows.Forms.Button btnCustomerCompleteFinanceReport;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnRecentPaymentReport;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpCustomerEnd;
        private System.Windows.Forms.DateTimePicker dtpCustomerStart;
        private System.Windows.Forms.TabControl tabReports;
        private DealershipManagerDataSet dealershipManagerDataSet;
        private System.Windows.Forms.BindingSource customerBindingSource;
        private DealershipManagerDataSetTableAdapters.CustomerTableAdapter customerTableAdapter;
        private System.Windows.Forms.TabPage ReportViewer;
        private Microsoft.Reporting.WinForms.ReportViewer customerReport;
        private System.Windows.Forms.BindingSource RecentPaymentsBindingSource;
        private DealershipManagerDataSetRecentPayments DealershipManagerDataSetRecentPayments;
        private DealershipManagerDataSetRecentPaymentsTableAdapters.RecentPaymentsTableAdapter RecentPaymentsTableAdapter;
        private System.Windows.Forms.BindingSource vehicleBindingSource;
        private DealershipManagerDataSetTableAdapters.VehicleTableAdapter vehicleTableAdapter;
        private System.Windows.Forms.ToolStripButton fillBy1ToolStripButton;
        private Microsoft.Reporting.WinForms.ReportViewer RecentPaymentReport;
        private System.Windows.Forms.DataGridView dgvReport;
        private System.Windows.Forms.BindingSource saleBindingSource;
        private DealershipManagerDataSetTableAdapters.SaleTableAdapter saleTableAdapter;
        private System.Drawing.Printing.PrintDocument printDocument1;

    }
}