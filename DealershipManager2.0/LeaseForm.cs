using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace DealershipManager2._0
{
    public partial class LeaseForm : Form
    {
        public Lease currentLease;
        public DealForm dealForm;
        public Customer currentCustomer;
        public LeaseForm()
        {
            InitializeComponent();
        }

        private void LeaseForm_Load(object sender, EventArgs e)
        {
            this.Size = new Size(Parent.Size.Width * 2, Parent.Size.Height * 2);
            this.rdbMonthly.Checked = true;
            this.chkIncludeGST.Checked = true;
        }

        private void btnCalculateMonthlyPayment_Click(object sender, EventArgs e)
        {
            decimal paymentAmount;
            decimal capCost;
            decimal residual;
            decimal interestRate;
            int months;
            decimal gstRate;
            if (chkIncludeGST.Checked)
            {
                gstRate = decimal.Parse(Properties.Settings.Default["GST"].ToString());
            }
            else
            {
                gstRate = 0;
            }

            decimal.TryParse(txtResidualValue.Text, out residual);
            decimal.TryParse(txtCapitalizedCost.Text, NumberStyles.Currency, null, out capCost);
            decimal.TryParse(txtInterestRate.Text, out interestRate);
            int.TryParse(txtNumberOfMonths.Text, out months);
            interestRate = interestRate / 1200;

            DataTable dataTable = new DataTable("Lease");
            dataTable.Columns.Add("Payment No.", typeof(string));
            dataTable.Columns.Add("Payment", typeof(string));
            dataTable.Columns.Add("Principle", typeof(string));
            dataTable.Columns.Add("Interest", typeof(string));
            dataTable.Columns.Add("GST", typeof(string));
            dataTable.Columns.Add("Balance", typeof(string));

            //calculating monthly payment... here goes   
            if (interestRate > 0)
            {
                paymentAmount = -1 * ((residual) - ((capCost) * (decimal)Math.Pow((double)(1 + interestRate), (double)months))) / ((((decimal)Math.Pow((double)(1 + interestRate), (double)months) - 1) / interestRate));
            }
            else
            {
                paymentAmount = ((capCost) - (residual)) / months;
            }

            decimal gst = 0;
            if (chkIncludeGST.Checked)
            {
                //gst = paymentAmount - (paymentAmount / (1 + gstRate));

                capCost = capCost + (gst * months);
                paymentAmount = -1 * ((residual) - ((capCost) * (decimal)Math.Pow((double)(1 + interestRate), (double)months))) / ((((decimal)Math.Pow((double)(1 + interestRate), (double)months) - 1) / interestRate));


                gst = paymentAmount * gstRate;
                paymentAmount = paymentAmount + gst;

            }
            else
            {
                gst = 0;
            }

            if (rdbMonthly.Checked)
            {
                decimal balance = capCost;
                for (int i = 1; i <= months; i++)
                {
                    decimal interest = balance * interestRate;
                    decimal principle = paymentAmount - gst - interest;
                    balance = balance - principle;

                    string paymentNo = i.ToString();
                    string payment = paymentAmount.ToString("C");
                    string sPrinciple = principle.ToString("C");
                    string sInterest = interest.ToString("C");
                    string sGST = gst.ToString("C");

                    dataTable.Rows.Add(paymentNo, payment, sPrinciple, sInterest, sGST, balance.ToString("C"));
                }
                txtResidualValue.Text = balance.ToString("C");
                txtMonthlyPayment.Text = paymentAmount.ToString("C");
            }
            else if (rdbAnnually.Checked)
            {
                decimal balance = capCost;
                decimal interest = 0;
                for (int i = 0; i < months; i++)
                {
                    ;
                    if (i % 12 == 0)
                    {
                        interest = balance * interestRate;
                    }
                    decimal principle = paymentAmount - gst - interest;
                    balance = balance - principle;

                    string paymentNo = i.ToString();
                    string payment = paymentAmount.ToString("C");
                    string sPrinciple = principle.ToString("C");
                    string sInterest = interest.ToString("C");
                    string sGST = gst.ToString("C");

                    dataTable.Rows.Add(paymentNo, payment, sPrinciple, sInterest, sGST, balance.ToString("C"));
                }
                txtResidualValue.Text = balance.ToString("C");
                txtMonthlyPayment.Text = paymentAmount.ToString("C");
            }
            else
            {
                throw new Exception("Annual/Monthly Not selected");
            }

            decimal totalInterest = 0;
            foreach (DataRow dr in dataTable.Rows)
            {
                string tempInt = dr["Interest"].ToString();
                tempInt = tempInt.Replace("$", "").Trim();
                decimal tempIntVal;
                decimal.TryParse(tempInt, out tempIntVal);
                totalInterest += tempIntVal;
            }
            txtTotalInterest.Text = totalInterest.ToString();

            dgvLeaseInfo.DataSource = dataTable;
            dgvLeaseInfo.Refresh();
            dgvLeaseInfo.Show();
        }

        private void btnCalculateResidualValue_Click(object sender, EventArgs e)
        {
            decimal paymentAmount;
            decimal capCost;
            decimal residual;
            decimal interestRate;
            int months;
            decimal gstRate;
            if (chkIncludeGST.Checked)
            {
                gstRate = decimal.Parse(Properties.Settings.Default["GST"].ToString());
            }
            else
            {
                gstRate = 0;
            }
            decimal.TryParse(txtMonthlyPayment.Text, NumberStyles.Currency, null, out paymentAmount);
            decimal.TryParse(txtCapitalizedCost.Text, NumberStyles.Currency, null, out capCost);
            decimal.TryParse(txtInterestRate.Text, out interestRate);
            int.TryParse(txtNumberOfMonths.Text, out months);
            interestRate = interestRate / 1200;

            DataTable dataTable = new DataTable("Lease");
            dataTable.Columns.Add("Payment No.", typeof(string));
            dataTable.Columns.Add("Payment", typeof(string));
            dataTable.Columns.Add("Principle", typeof(string));
            dataTable.Columns.Add("Interest", typeof(string));
            dataTable.Columns.Add("GST", typeof(string));
            dataTable.Columns.Add("Balance", typeof(string));

            if (rdbMonthly.Checked)
            {
                decimal balance = capCost;
                for (int i = 1; i <= months; i++)
                {
                    decimal interest = balance * interestRate;
                    decimal gst;
                    if (chkIncludeGST.Checked)
                    {
                        gst = paymentAmount - (paymentAmount / (1 + gstRate));

                        //capCost = capCost + (gst * months);
                        //paymentAmount = -1 * ((residual) - ((capCost) * (decimal)Math.Pow((double)(1 + interestRate), (double)months))) / ((((decimal)Math.Pow((double)(1 + interestRate), (double)months) - 1) / interestRate));
                        //paymentAmount = paymentAmount + gst;
                    }
                    else
                    {
                        gst = 0;
                    }
                    decimal principle = paymentAmount - gst - interest;
                    balance = balance - principle;

                    string paymentNo = i.ToString();
                    string payment = paymentAmount.ToString("C");
                    string sPrinciple = principle.ToString("C");
                    string sInterest = interest.ToString("C");
                    string sGST = gst.ToString("C");

                    dataTable.Rows.Add(paymentNo, payment, sPrinciple, sInterest, sGST, balance.ToString("C"));
                }
                txtResidualValue.Text = balance.ToString("C");
            }
            else if (rdbAnnually.Checked)
            {
                decimal balance = capCost;
                decimal interest = 0;
                for (int i = 0; i < months; i++)
                {
                    ;
                    if (i % 12 == 0)
                    {
                        interest = balance * interestRate;
                    }
                    decimal gst;
                    if (chkIncludeGST.Checked)
                    {
                        gst = paymentAmount - (paymentAmount / (1 + gstRate));

                        //capCost = capCost + (gst * months);
                        //paymentAmount = -1 * ((residual) - ((capCost) * (decimal)Math.Pow((double)(1 + interestRate), (double)months))) / ((((decimal)Math.Pow((double)(1 + interestRate), (double)months) - 1) / interestRate));
                        //paymentAmount = paymentAmount + gst;
                    }
                    else
                    {
                        gst = 0;
                    }
                    decimal principle = paymentAmount - gst - interest;
                    balance = balance - principle;

                    string paymentNo = i.ToString();
                    string payment = paymentAmount.ToString("C");
                    string sPrinciple = principle.ToString("C");
                    string sInterest = interest.ToString("C");
                    string sGST = gst.ToString("C");

                    dataTable.Rows.Add(paymentNo, payment, sPrinciple, sInterest, sGST, balance.ToString("C"));
                }
                txtResidualValue.Text = balance.ToString("C");
            }
            else
            {
                throw new Exception("Annual/Monthly Not selected");
            }

            decimal totalInterest = 0;
            foreach (DataRow dr in dataTable.Rows)
            {
                string tempInt = dr["Interest"].ToString();
                tempInt = tempInt.Replace("$", "").Trim();
                decimal tempIntVal;
                decimal.TryParse(tempInt, out tempIntVal);
                totalInterest += tempIntVal;
            }
            txtTotalInterest.Text = totalInterest.ToString();

            dgvLeaseInfo.DataSource = dataTable;
            dgvLeaseInfo.Refresh();
            dgvLeaseInfo.Show();
        }

        private void UnbindForm(Control c)
        {
            if (c.HasChildren)
            {
                foreach (Control child in c.Controls)
                {
                    UnbindForm(child);
                }
            }
            else if (c.GetType() == typeof(TextBox))
            {
                (c as TextBox).Text = "";
            }
            else if (c.GetType() == typeof(CheckBox))
            {
                (c as CheckBox).Checked = false;
            }
            else if (c.GetType() == typeof(RadioButton))
            {
                (c as RadioButton).Checked = false;
            }
            else if (c.GetType() == typeof(ListBox))
            {
                (c as ListBox).ClearSelected();
            }

            else if (c.GetType() == typeof(ComboBox))
            {
                //
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.Controls)
            {
                UnbindForm(c);
            }
            dgvLeaseInfo.DataSource = null;
            dgvLeaseInfo.Refresh();

            LeaseForm_Load(this, null);
        }

        private void btnSavePaymentInfo_Click(object sender, EventArgs e)
        {

            if (currentLease == null)
            {
                currentLease = new Lease();

                MessageBox.Show("This Lease Payment Information won't be saved until you save the Lease itself.");

                DealForm df;
                if (this.dealForm == null)
                {
                    df = new DealForm();

                }
                else
                {
                    df = this.dealForm;
                }
                if (rdbAnnually.Checked)
                {
                    df.rdbLeaseCalculateAnnually.Checked = true;
                }
                else if (rdbMonthly.Checked)
                {
                    df.rdbCalculateMonthly.Checked = true;
                }
                df.txtFinanceInterestAmount.Text = txtTotalInterest.Text;
                df.txtFinanceInterestRate.Text = txtInterestRate.Text;
                df.txtFinanceNumberofPayments.Text = txtNumberOfMonths.Text;
                df.txtFinancePaymentAmount.Text = txtMonthlyPayment.Text;
                df.txtFinanceSubtotal.Text = txtCapitalizedCost.Text;
                df.txtFinanceTotalBalanceDue.Text = df.CalculateTotal("Finance").ToString("C");

                df.txtLeaseInterestRate.Text = txtInterestRate.Text;
                df.txtLeaseNumberofPayments.Text = txtNumberOfMonths.Text;
                df.txtLeasePaymentAmount.Text = txtMonthlyPayment.Text;
                df.txtLeaseSubtotal.Text = txtCapitalizedCost.Text;


                df.MdiParent = this.MdiParent;

                df.Show();
            }
        }

        private void btnPrintReport_Click(object sender, EventArgs e)
        {
            using (DealershipManagerEntities context = new DealershipManagerEntities())
            {
                try
                {
                    context.ExecuteStoreCommand("DELETE FROM tmpCalculation");
                }
                catch
                {
                }
                finally
                {
                    context.SaveChanges();
                }

                int id = 0;
                foreach (DataGridViewRow row in dgvLeaseInfo.Rows)
                {
                    try
                    {
                        TmpCalculation tmp = new TmpCalculation();

                        tmp.id = id;
                        id++;
                        Double balance = 0;
                        Double.TryParse(row.Cells["Balance"].Value.ToString().Replace("$", ""), out balance);
                        tmp.Balance = balance;

                        double cost = 0;
                        Double.TryParse(txtCapitalizedCost.Text, out cost);
                        tmp.Cost = cost;

                        Double gst = 0;
                        Double.TryParse(row.Cells["GST"].Value.ToString().Replace("$", ""), out gst);
                        tmp.GST = gst;

                        Double interest = 0;
                        Double.TryParse(row.Cells["Interest"].Value.ToString().Replace("$", ""), out interest);
                        tmp.Interest = interest;

                        Double interestrate = 0;
                        Double.TryParse(txtInterestRate.Text, out interestrate);
                        tmp.InterestRate = interestrate;

                        int numberofmonths = 0;
                        int.TryParse(txtNumberOfMonths.Text, out numberofmonths);
                        tmp.Months = numberofmonths;

                        Double paymentAmount = 0;
                        Double.TryParse(row.Cells["Payment"].Value.ToString().Replace("$", ""), out paymentAmount);
                        tmp.PaymentAmount = paymentAmount;

                        int paymentno = 0;
                        int.TryParse(row.Cells["Payment No."].Value.ToString().Replace("$", ""), out paymentno);
                        tmp.PmtNo = paymentno;

                        Double principle = 0;
                        Double.TryParse(row.Cells["Principle"].Value.ToString().Replace("$", ""), out principle);
                        tmp.Principle = principle;

                        context.TmpCalculations.AddObject(tmp);
                    }
                    catch (Exception ex)
                    {
                        string test = ex.Message;
                    }
                    finally
                    {
                        context.SaveChanges();
                    }
                }
            }

            ReportViewerForm form = new ReportViewerForm();
            form.rptviewer.LocalReport.ReportPath = "TempCalculationReport.rdlc";
            form.SetTempCalcReportDataSource();
            form.rptviewer.Show();
            form.Show();
            form.rptviewer.RefreshReport();
            form.rptviewer.LocalReport.Refresh();


            //ReportViewerForm rptv = new ReportViewerForm();
            //rptv.MdiParent = this.MdiParent;
            //rptv.Show();
            //rptv.rptviewer.LocalReport.Refresh();
            //rptv.rptviewer.Refresh();
            //rptv.rptviewer.RefreshReport();
            //rptv.rptviewer.Visible = true;
        }

        private void LeaseForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dealForm != null)
            {
                dealForm.Show();
            }
        }
    }
}
