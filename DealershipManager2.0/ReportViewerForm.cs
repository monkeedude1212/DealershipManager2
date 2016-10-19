using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DealershipManager2._0
{
    public partial class ReportViewerForm : Form
    {
        private string reportLocation = "";
        private string exeLocation;
        public ReportViewerForm()
        {
            InitializeComponent();
        }

        private void ReportViewerForm_Load(object sender, EventArgs e)
        {

            rptviewer.PrinterSettings.PrintRange = System.Drawing.Printing.PrintRange.AllPages; 
            // TODO: This line of code loads data into the 'DealershipManagerDataSet.Current_Vehicles' table. You can move, or remove it, as needed.
            this.Current_VehiclesTableAdapter.Fill(this.DealershipManagerDataSet.Current_Vehicles);
            // TODO: This line of code loads data into the 'DealershipManagerDataSet.Finance' table. You can move, or remove it, as needed.
            this.financeTableAdapter.Fill(this.DealershipManagerDataSet.Finance);
            // TODO: This line of code loads data into the 'DealershipManagerDataSet1.TmpCalculation' table. You can move, or remove it, as needed.
            this.TmpCalculationTableAdapter.Fill(this.DealershipManagerDataSet1.TmpCalculation);
            // TODO: This line of code loads data into the 'DealershipManagerDataSet.Customer' table. You can move, or remove it, as needed.
            
            this.CustomerTableAdapter.Fill(this.DealershipManagerDataSet.Customer);            
            this.vehicleTableAdapter.Fill(this.DealershipManagerDataSet.Vehicle);
            this.financeTableAdapter.Fill(this.DealershipManagerDataSet.Finance);
            this.saleTableAdapter.Fill(this.DealershipManagerDataSet.Sale);
            
            
        }

        public void SetInventoryReportDataSource()
        {
            rptviewer.LocalReport.DataSources.Clear();

            this.Current_VehiclesTableAdapter.Fill(this.DealershipManagerDataSet.Current_Vehicles);
            DataTable vehiclesTable = this.DealershipManagerDataSet.Current_Vehicles.CopyToDataTable();
            rptviewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("CurrentVehicles", vehiclesTable));
        }

        public void SetTempCalcReportDataSource()
        {
            rptviewer.LocalReport.DataSources.Clear();

            this.TmpCalculationTableAdapter.Fill(this.DealershipManagerDataSet1.TmpCalculation);
            DataTable tempCalcsTable = this.DealershipManagerDataSet1.TmpCalculation.CopyToDataTable();
            rptviewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("TempCalcRows", tempCalcsTable));
        }

        public void SetFinanceDataSource(Sale currentSale)
        {
            rptviewer.LocalReport.DataSources.Clear();
            this.CustomerTableAdapter.Fill(this.DealershipManagerDataSet.Customer);
            this.vehicleTableAdapter.Fill(this.DealershipManagerDataSet.Vehicle);
            this.financeTableAdapter.Fill(this.DealershipManagerDataSet.Finance);
            this.saleTableAdapter.Fill(this.DealershipManagerDataSet.Sale);
            
            var dataTable = this.DealershipManagerDataSet.Finance;

            DataTable financeTable = this.DealershipManagerDataSet.Finance.CopyToDataTable();
            financeTable.Clear();
            dataTable.Where(x => x.saleId == currentSale.id).OrderBy(y => y.DatePaid).CopyToDataTable(financeTable, LoadOption.OverwriteChanges);
            rptviewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("FinanceDataSet", financeTable));

            DataTable customerTable = this.DealershipManagerDataSet.Customer.CopyToDataTable();
            customerTable.Clear();
            var dataTable3 = this.DealershipManagerDataSet.Customer;
            dataTable3.Where(x => x.Id == currentSale.CustomerId).CopyToDataTable(customerTable, LoadOption.OverwriteChanges);
            rptviewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("CustomerDataSet", customerTable));

            DataTable saleTable = this.DealershipManagerDataSet.Sale.CopyToDataTable();
            saleTable.Clear();
            var dataTable2 = this.DealershipManagerDataSet.Sale;
            dataTable2.Where(x => x.id == currentSale.id).CopyToDataTable(saleTable, LoadOption.OverwriteChanges);
            rptviewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("SaleDataSet", saleTable));
            
            DataTable vehicleTable = this.DealershipManagerDataSet.Vehicle.CopyToDataTable();
            vehicleTable.Clear();
            var dataTable4 = this.DealershipManagerDataSet.Vehicle;
            dataTable4.Where(x => x.Id == currentSale.VehicleId).CopyToDataTable(vehicleTable, LoadOption.OverwriteChanges);
            rptviewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("VehicleDataSet", vehicleTable));
        }

        public void SetLeaseDataSource(Lease currentLease)
        {
            rptviewer.LocalReport.DataSources.Clear();
            this.CustomerTableAdapter.Fill(this.DealershipManagerDataSet.Customer);
            this.vehicleTableAdapter.Fill(this.DealershipManagerDataSet.Vehicle);
            this.leaseTableAdapter.Fill(this.DealershipManagerDataSet.Lease);
            this.leasePaymentsTableAdapter.Fill(this.DealershipManagerDataSet.LeasePayments);

            var dataTable = this.DealershipManagerDataSet.Lease;

            DataTable leaseTable = this.DealershipManagerDataSet.Lease.CopyToDataTable();
            leaseTable.Clear();
            dataTable.Where(x => x.Id == currentLease.Id).CopyToDataTable(leaseTable, LoadOption.OverwriteChanges);
            rptviewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("LeaseDataSet", leaseTable));

            DataTable customerTable = this.DealershipManagerDataSet.Customer.CopyToDataTable();
            customerTable.Clear();
            var dataTable3 = this.DealershipManagerDataSet.Customer;
            dataTable3.Where(x => x.Id == currentLease.CustomerId).CopyToDataTable(customerTable, LoadOption.OverwriteChanges);
            rptviewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("CustomerDataSet", customerTable));

            DataTable leasePaymentsTable = this.DealershipManagerDataSet.LeasePayments.CopyToDataTable();
            leasePaymentsTable.Clear();
            var dataTable2 = this.DealershipManagerDataSet.LeasePayments;
            dataTable2.Where(x => x.LeaseId == currentLease.Id).CopyToDataTable(leasePaymentsTable, LoadOption.OverwriteChanges);
            rptviewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("LeasePaymentsDataSet", leasePaymentsTable));

            DataTable vehicleTable = this.DealershipManagerDataSet.Vehicle.CopyToDataTable();
            vehicleTable.Clear();
            var dataTable4 = this.DealershipManagerDataSet.Vehicle;
            dataTable4.Where(x => x.Id == currentLease.VehicleId).CopyToDataTable(vehicleTable, LoadOption.OverwriteChanges);
            rptviewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("VehicleDataSet", vehicleTable));
        }
    }
}
