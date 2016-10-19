using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DealershipManager2._0
{
	public partial class DealershipManager : Form
	{

		public DealershipManager()
		{
			InitializeComponent();
            String sMessage = "";
            try
            {
                using (DealershipManagerEntities context = new DealershipManagerEntities())
                {
                    List<Insurance> insuranceQuery = (from insurance in context.Insurances
                                                      select insurance).ToList<Insurance>();

                    List<Lien> lienQuery = (from lien in context.Liens
                                            select lien).ToList<Lien>();

                    insuranceQuery = insuranceQuery.OrderBy(x => x.ExpiryDate).ToList();
                    sMessage += "The following people have insurance that will expire in the next seven days:\n";
                    sMessage += "-------------------------------------------------------------------------------\n";
                    try
                    {
                        foreach (var insurance in insuranceQuery)
                        {
                            int custId = insurance.CustomerId ?? 0;
                            //if (insurance.ExpiryDate.Value < DateTime.Now.AddDays(7) && insurance.ExpiryDate.Value > DateTime.Now)
                            if (insurance.ExpiryDate.Value < DateTime.Now.AddDays(7))
                            {
                                Customer customerQuery = (from customer in context.Customers
                                                          where customer.Id == custId
                                                          select customer).FirstOrDefault();

                                sMessage += "\n";
                                sMessage += customerQuery.LastName + ", " + customerQuery.FirstName + "  -  " + insurance.ExpiryDate.Value.ToShortDateString();
                            }
                        }

                        sMessage += "\n";
                        sMessage += "\n";
                        sMessage += "The following liens will expire in the next seven days:\n";
                        sMessage += "-------------------------------------------------------------------------------\n";

                        foreach (var lien in lienQuery)
                        {
                            int custId = lien.Customer ?? 0;
                            //if (lien.ExpiryDate.Value < DateTime.Now.AddDays(7) && lien.ExpiryDate.Value > DateTime.Now)
                            if (lien.ExpiryDate.Value < DateTime.Now.AddDays(7))
                            {
                                Vehicle vehicleQuery = (from vehicle in context.Vehicles
                                                        where vehicle.Id == lien.VehicleId
                                                        select vehicle).FirstOrDefault();
                                Customer customerQuery = (from customer in context.Customers
                                                          where customer.Id == custId
                                                          select customer).FirstOrDefault();

                                sMessage += "\n";
                                sMessage += vehicleQuery.Year.ToString() + " " + vehicleQuery.Make.ToString() + " " + vehicleQuery.Model.ToString() + " - " + vehicleQuery.VIN.ToString() + "\n";
                                sMessage += customerQuery.LastName + ", " + customerQuery.FirstName + "\n";
                                sMessage += "Registration Number: " + lien.RegNumber.ToString() + "   Expiry Date: " + lien.ExpiryDate.Value.ToShortDateString() + "\n";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }

                    MessageBox.Show(sMessage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.InnerException.Message);
            }
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
            System.Diagnostics.Process.GetCurrentProcess().Kill();
            Application.Exit();
			this.Close();
			this.Dispose();
		}

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

            this.Close();
            this.Dispose();
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closeAllChildForms();
            CustomerForm customerForm = new CustomerForm();
            customerForm.MdiParent = this;
            customerForm.Show();
        }

        private void vehicleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closeAllChildForms();
            VehicleForm vehicleForm = new VehicleForm();
            vehicleForm.MdiParent = this;
            vehicleForm.Show();
        }
        public void closeAllChildForms()
        {
            //foreach (Form child in this.MdiChildren)
            //{
            //    child.Close();
            //    this.RemoveOwnedForm(child);
            //    child.Dispose();
            //}
        }

        private void dealToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closeAllChildForms();
            DealForm dealForm = new DealForm();
            dealForm.MdiParent = this;
            dealForm.Show();
        }

        private void staffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closeAllChildForms();
            StaffForm staffForm = new StaffForm();
            staffForm.MdiParent = this;
            staffForm.Show();
        }

        private void requestsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closeAllChildForms();
            RequestsForm requestsForm = new RequestsForm();
            requestsForm.MdiParent = this;
            requestsForm.Show();
        }

        private void leaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closeAllChildForms();
            LeaseForm leaseForm = new LeaseForm();
            leaseForm.MdiParent = this;
            leaseForm.Show();
        }

        private void contactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closeAllChildForms();
            ContactsForm contactsForm = new ContactsForm();
            contactsForm.MdiParent = this;
            contactsForm.Show();
        }

        private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closeAllChildForms();
            ReportsForm reportsForm = new ReportsForm();
            reportsForm.MdiParent = this;
            reportsForm.Show();
        }
	}
}
