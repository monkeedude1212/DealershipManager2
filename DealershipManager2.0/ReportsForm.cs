using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Globalization;


namespace DealershipManager2._0
{
    public partial class ReportsForm : Form
    {
        StringFormat strFormat; //Used to format the grid rows.
        ArrayList arrColumnLefts = new ArrayList();//Used to save left coordinates of columns
        ArrayList arrColumnWidths = new ArrayList();//Used to save column widths
        int iCellHeight = 0; //Used to get/set the datagridview cell height
        int iTotalWidth = 0; //
        int iRow = 0;//Used as counter
        int iCount = 0;
        bool bFirstPage = false; //Used to check whether we are printing first page
        bool bNewPage = false;// Used to check whether we are printing a new page
        int iHeaderHeight = 0; //Used for the header height

        public ReportsForm()
        {
            InitializeComponent();
        }

        private void ReportsForm_Load(object sender, EventArgs e)
        {
            RecentPaymentReport.PrinterSettings.PrintRange = System.Drawing.Printing.PrintRange.AllPages;
            this.customerReport.PrinterSettings.PrintRange = System.Drawing.Printing.PrintRange.AllPages;
            
            // TODO: This line of code loads data into the 'dealershipManagerDataSet.Sale' table. You can move, or remove it, as needed.
            this.saleTableAdapter.Fill(this.dealershipManagerDataSet.Sale);
            // TODO: This line of code loads data into the 'dealershipManagerDataSet.Vehicle' table. You can move, or remove it, as needed.
            this.vehicleTableAdapter.Fill(this.dealershipManagerDataSet.Vehicle);

            // TODO: This line of code loads data into the 'dealershipManagerDataSet.Customer' table. You can move, or remove it, as needed.

            this.customerTableAdapter.Fill(this.dealershipManagerDataSet.Customer);
            // TODO: This line of code loads data into the 'DealershipManagerDataSet.CustomerDocuments' table. You can move, or remove it, as needed.
            this.RecentPaymentsTableAdapter.Fill(this.DealershipManagerDataSetRecentPayments.RecentPayments);


            this.Size = new Size(Parent.Size.Width * 2, Parent.Size.Height * 2);

        }

        private void btnCustomerListReport_Click(object sender, EventArgs e)
        {
            customerReport.Visible = true;
            ReportViewer.Select();
            tabReports.SelectedTab = tabReports.TabPages["ReportViewer"];
            customerReport.Refresh();
            customerReport.RefreshReport();
        }

        private void btnBasicInventoryReport_Click(object sender, EventArgs e)
        {
            ReportViewerForm form = new ReportViewerForm();
            form.rptviewer.LocalReport.ReportPath = "BasicInventory.rdlc";
            form.SetInventoryReportDataSource();
            form.rptviewer.Show();
            form.Show();
            form.rptviewer.RefreshReport();
            form.rptviewer.LocalReport.Refresh();
        }

        private void btnRecentPaymentReport_Click(object sender, EventArgs e)
        {
            try
            {
                ReportParameter startDate = new ReportParameter("StartDate", dtpCustomerStart.Value.ToShortDateString());
                ReportParameter endDate = new ReportParameter("EndDate", dtpCustomerEnd.Value.ToShortDateString());
                RecentPaymentReport.LocalReport.SetParameters(new ReportParameter[] { startDate, endDate });
                RecentPaymentReport.Visible = true;
                ReportViewer.Select();
                tabReports.SelectedTab = tabReports.TabPages["ReportViewer"];
                RecentPaymentReport.Refresh();
                RecentPaymentReport.RefreshReport();
            }
            catch (Exception ex)
            {
                string test = ex.Message;   
            }
        }

        private void cboVehicle_Format(object sender, ListControlConvertEventArgs e)
        {
            DataRow r = ((DataRowView)e.ListItem).Row;
            e.Value = r["Year"] + " " + r["Make"] + " " + r["Model"];
        }

        private void BindVehicleInfo(Vehicle v)
        {
            txtMake.Text = v.Make;
            txtModel.Text = v.Model;
            txtYear.Text = v.Year.ToString();
            txtVIN.Text = v.VIN;
        }

        private void cboVehicle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (null != cboVehicle.SelectedValue)
            {
                int id = (int)cboVehicle.SelectedValue;
                using (DealershipManagerEntities context = new DealershipManagerEntities())
                {
                    IQueryable<Vehicle> vehicleQuery = from vehicle in context.Vehicles
                                                       where vehicle.Id == id
                                                       select vehicle;

                    Vehicle veh = vehicleQuery.FirstOrDefault();
                    if (null == veh)
                    {
                        veh = new Vehicle();
                    }

                    BindVehicleInfo(veh);
                }
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            try
            {
                //Set the left margin
                int iLeftMargin = e.MarginBounds.Left;
                //Set the top margin
                int iTopMargin = e.MarginBounds.Top;
                //Whether more pages have to print or not
                bool bMorePagesToPrint = false;
                int iTmpWidth = 0;

                //For the first page to print set the cell width and header height

                if (bFirstPage)
                {
                    foreach (DataGridViewColumn GridCol in dgvReport.Columns)
                    {
                        iTmpWidth = (int)(Math.Floor((double)((double)GridCol.Width /
                            (double)iTotalWidth * (double)iTotalWidth *
                            ((double)e.MarginBounds.Width / (double)iTotalWidth))));

                        iHeaderHeight = (int)(e.Graphics.MeasureString(GridCol.HeaderText,
                            GridCol.InheritedStyle.Font, iTmpWidth).Height) + 11;

                        // Save width and height of headers
                        arrColumnLefts.Add(iLeftMargin);
                        arrColumnWidths.Add(iTmpWidth);
                        iLeftMargin += iTmpWidth;
                    }
                }
                //Loop till all the grid rows not get printed
                while (iRow <= dgvReport.Rows.Count - 1)
                {
                    DataGridViewRow GridRow = dgvReport.Rows[iRow];
                    //Set the cell height
                    iCellHeight = GridRow.Height + 5;
                    int iCount = 0;
                    //Check whether the current page settings allows more rows to print
                    if (iTopMargin + iCellHeight >= e.MarginBounds.Height + e.MarginBounds.Top)
                    {
                        bNewPage = true;
                        bFirstPage = false;
                        bMorePagesToPrint = true;
                        break;
                    }
                    else
                    {
                        if (bNewPage)
                        {
                            //Draw Header
                            e.Graphics.DrawString("MORLEY CUSTOMER PAYMENTS",
                                new Font(dgvReport.Font, FontStyle.Bold),
                                Brushes.Black, e.MarginBounds.Left,
                                e.MarginBounds.Top - e.Graphics.MeasureString("MORLEY CUSTOMER PAYMENTS",
                                new Font(dgvReport.Font, FontStyle.Bold),
                                e.MarginBounds.Width).Height - 13);

                            String strDate = DateTime.Now.ToLongDateString() + " " +
                                DateTime.Now.ToShortTimeString();
                            //Draw Date
                            e.Graphics.DrawString(strDate,
                                new Font(dgvReport.Font, FontStyle.Bold), Brushes.Black,
                                e.MarginBounds.Left +
                                (e.MarginBounds.Width - e.Graphics.MeasureString(strDate,
                                new Font(dgvReport.Font, FontStyle.Bold),
                                e.MarginBounds.Width).Width),
                                e.MarginBounds.Top - e.Graphics.MeasureString("MORLEY CUSTOMER PAYMENTS",
                                new Font(new Font(dgvReport.Font, FontStyle.Bold),
                                FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            //Draw Columns                 
                            iTopMargin = e.MarginBounds.Top;
                            foreach (DataGridViewColumn GridCol in dgvReport.Columns)
                            {
                                e.Graphics.FillRectangle(new SolidBrush(Color.LightGray),
                                    new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight));

                                e.Graphics.DrawRectangle(Pens.Black,
                                    new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight));

                                e.Graphics.DrawString(GridCol.HeaderText,
                                    GridCol.InheritedStyle.Font,
                                    new SolidBrush(GridCol.InheritedStyle.ForeColor),
                                    new RectangleF((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight), strFormat);
                                iCount++;
                            }
                            bNewPage = false;
                            iTopMargin += iHeaderHeight;
                        }
                        iCount = 0;
                        //Draw Columns Contents                
                        foreach (DataGridViewCell Cel in GridRow.Cells)
                        {
                            if (Cel.Value != null)
                            {
                                String value = Cel.Value.ToString();
                                if (Cel.Value.GetType() == typeof(DateTime))
                                {
                                    value = ((DateTime)(Cel.Value)).ToString("dd/MM/yyyy");
                                }
                                decimal money = 0;
                                decimal.TryParse(Cel.Value.ToString(), NumberStyles.Currency, null, out money);
                                if (money != 0)
                                {
                                    value = money.ToString("C");
                                }
                                if (Cel.OwningColumn.Name == "PmtNo")
                                {
                                    value = Cel.Value.ToString();
                                }

                                e.Graphics.DrawString(value,
                                    Cel.InheritedStyle.Font,
                                    new SolidBrush(Cel.InheritedStyle.ForeColor),
                                    new RectangleF((int)arrColumnLefts[iCount],
                                    (float)iTopMargin,
                                    (int)arrColumnWidths[iCount], (float)iCellHeight),
                                    strFormat);
                            }
                            //Drawing Cells Borders 
                            e.Graphics.DrawRectangle(Pens.Black,
                                new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                (int)arrColumnWidths[iCount], iCellHeight));
                            iCount++;
                        }
                    }
                    iRow++;
                    iTopMargin += iCellHeight;
                }
                //If more lines exist, print another page.
                if (bMorePagesToPrint)
                    e.HasMorePages = true;
                else
                    e.HasMorePages = false;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
            }
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            List<DataGridViewColumn> toremove = new List<DataGridViewColumn>();
            foreach (DataGridViewColumn GridCol in dgvReport.Columns)
            {
                if (!GridCol.Visible)
                {
                    toremove.Add(GridCol);
                }
            }
            foreach (DataGridViewColumn GridCol in toremove)
            {
                dgvReport.Columns.Remove(GridCol);
            }
            try
            {
                StringFormat strFormat = new StringFormat();
                strFormat.Alignment = StringAlignment.Near;
                strFormat.LineAlignment = StringAlignment.Center;
                strFormat.Trimming = StringTrimming.EllipsisCharacter;


                arrColumnLefts.Clear();
                arrColumnWidths.Clear();
                iCellHeight = 0;
                iCount = 0;
                bFirstPage = true;
                bNewPage = true;

                // Calculating Total Widths
                iTotalWidth = 0;

                foreach (DataGridViewColumn dgvGridCol in dgvReport.Columns)
                {
                    iTotalWidth += dgvGridCol.Width;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCustomerCompleteFinanceReport_Click(object sender, EventArgs e)
        {
            
            using (DealershipManagerEntities context = new DealershipManagerEntities())
            {
                IQueryable<Customer> customerQuery = (from customer in context.Customers
                                                      join sale in context.Sales on customer.Id equals sale.CustomerId
                                                      where customer.BandName != "" && sale.current == true
                                                      orderby customer.LastName, customer.FirstName
                                                      select customer).Distinct().OrderBy(x => x.LastName).ThenBy(y => y.FirstName);




                List<FinanceReportRow> rows = new List<FinanceReportRow>();
                List<Customer> customers = customerQuery.ToList();
                foreach (Customer c in customerQuery)
                {
                    FinanceReportRow frr = new FinanceReportRow();
                    frr.First_Name = c.FirstName;
                    frr.Last_Name = c.LastName;
                    frr.Notes = "";

                    Finance financeQuery = (from finance in context.Finances
                                            join sale in context.Sales on finance.saleId equals sale.id
                                            join customer in customerQuery on sale.CustomerId equals customer.Id
                                            where sale.CurrentBalance > 0 && sale.current == true && customer.Id == c.Id
                                            orderby finance.DatePaid descending
                                            select finance).FirstOrDefault();
                    if (financeQuery != null)
                    {
                        frr.Current_Balance = financeQuery.Balance ?? 0;
                        frr.Payment_Due = financeQuery.Payment ?? 0;
                        rows.Add(frr);
                    }
                }

                ReportViewerForm form = new ReportViewerForm();
                form.rptviewer.LocalReport.ReportPath = "CompleteFinanceReport.rdlc";
                form.rptviewer.LocalReport.DataSources[0].Value = rows;
                form.rptviewer.LocalReport.DataSources[0].Name = "FinanceRows";
                form.rptviewer.Show();
                form.Show();
                form.rptviewer.RefreshReport();
                form.rptviewer.LocalReport.Refresh();

                //dgvReport.Columns.Add("Last_Name", "Last Name");
                //dgvReport.Columns["Last_Name"].DataPropertyName = "Last_Name";
                //dgvReport.Columns.Add("First_Name", "First Name");
                //dgvReport.Columns["First_Name"].DataPropertyName = "First_Name";
                //dgvReport.Columns.Add("Current_Balance", "Current Balance");
                //dgvReport.Columns["Current_Balance"].DataPropertyName = "Current_Balance";
                //dgvReport.Columns["Current_Balance"].DefaultCellStyle.Format = "C";
                //dgvReport.Columns.Add("Payment_Due", "Payment Due");
                //dgvReport.Columns["Payment_Due"].DataPropertyName = "Payment_Due";
                //dgvReport.Columns["Payment_Due"].DefaultCellStyle.Format = "C";
                //dgvReport.Columns.Add("Notes", "Notes");
                //dgvReport.Columns["Notes"].DataPropertyName = "Notes";

                //// Initialize the DataGridView.
                //dgvReport.AutoGenerateColumns = false;
                //dgvReport.AutoSize = true;

                //dgvReport.DataSource = rows;
                //dgvReport.Refresh();

                //PrintPreviewDialog objPPdialog = new PrintPreviewDialog();

                //printDocument1.DocumentName = "Payment Information";
                //objPPdialog.Document = printDocument1;
                //objPPdialog.ShowDialog();
            }
        }
    }


    public partial class FinanceReportRow
    {
        public String Last_Name
        {
            get;
            set;
        }
        public String First_Name
        {
            get;
            set;
        }
        public String Notes
        {
            get;
            set;
        }

        public double Current_Balance
        {
            get;
            set;
        }

        public double Payment_Due
        {
            get;
            set;
        }
    }
}


