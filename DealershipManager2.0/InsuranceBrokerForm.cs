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
    public partial class InsuranceBrokerForm : Form
    {

        InsuranceBroker currentInsuranceBroker;
        public CustomerForm customerForm;
        public InsuranceBrokerForm()
        {
            InitializeComponent();
        }

        public InsuranceBrokerForm(int brokerId)
        {
            InitializeComponent();
            using (DealershipManagerEntities context = new DealershipManagerEntities())
            {
                InsuranceBroker insuranceBroker = (from insBroker in context.InsuranceBrokers
                                                   where insBroker.BrokerId == brokerId
                                                     select insBroker).FirstOrDefault();
                currentInsuranceBroker = insuranceBroker;
                txtAddress.Text = currentInsuranceBroker.BrokerAddress;
                txtCity.Text = currentInsuranceBroker.BrokerCity;
                txtEmail.Text = currentInsuranceBroker.BrokerEmail;
                txtFax.Text = currentInsuranceBroker.BrokerFax;
                txtInsuranceBrokerName.Text = currentInsuranceBroker.BrokerName;
                txtPhone.Text = currentInsuranceBroker.BrokerPhone;
                txtPostal.Text = currentInsuranceBroker.BrokerPostal;
                cboCountry.SelectedItem = currentInsuranceBroker.BrokerCountry;
                cboProvince.SelectedItem = currentInsuranceBroker.BrokerProvince;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (DealershipManagerEntities context = new DealershipManagerEntities())
            {
                if (currentInsuranceBroker != null)
                {
                    //It exists, find it, and edit it.
                    InsuranceBroker insuranceBroker = (from insBroker in context.InsuranceBrokers
                                                         where insBroker.BrokerId == currentInsuranceBroker.BrokerId
                                                         select insBroker).FirstOrDefault();
                    updateInsuranceBroker(insuranceBroker);
                    context.SaveChanges();
                    context.Connection.Close();
                }
                else
                {
                    InsuranceBroker insuranceBroker = new InsuranceBroker();
                    updateInsuranceBroker(insuranceBroker);
                    context.InsuranceBrokers.AddObject(insuranceBroker);
                    context.SaveChanges();
                    context.Connection.Close();
                }
            }
            MessageBox.Show("Insurance Broker has been saved");
            customerForm.insuranceBrokerTableAdapter.Fill(customerForm.dealershipManagerDataSet.InsuranceBroker);
            customerForm.cboInsuranceBroker.SelectedItem = currentInsuranceBroker;
            customerForm.currentInsuranceBroker = currentInsuranceBroker;
            customerForm.BindInsuranceBrokerToForm();
            this.Close();
        }

        private void updateInsuranceBroker(InsuranceBroker c)
        {
            c.BrokerName = txtInsuranceBrokerName.Text;
            c.BrokerAddress = txtAddress.Text;
            c.BrokerCity = txtCity.Text;
            c.BrokerEmail = txtEmail.Text;
            c.BrokerFax = txtFax.Text;
            c.BrokerPhone = txtPhone.Text;
            c.BrokerPostal = txtPostal.Text;

            if (null != cboCountry.SelectedItem)
                c.BrokerCountry = cboCountry.SelectedItem.ToString();

            if (null != cboProvince.SelectedItem)
                c.BrokerProvince = cboProvince.SelectedItem.ToString();
        }
    }
}
