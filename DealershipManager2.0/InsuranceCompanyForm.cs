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
    public partial class InsuranceCompanyForm : Form
    {
        InsuranceCompany currentInsuranceCompany;
        public CustomerForm customerForm;
        public InsuranceCompanyForm()
        {
            InitializeComponent();
            currentInsuranceCompany = null; // Just to be safe
        }

        public InsuranceCompanyForm(int companyId)
        {
            InitializeComponent();
            using (DealershipManagerEntities context = new DealershipManagerEntities())
            {
                InsuranceCompany insuranceCompany = (from insCompany in context.InsuranceCompanies
                                                     where insCompany.CompanyId == companyId
                                                     select insCompany).FirstOrDefault();
                currentInsuranceCompany = insuranceCompany;
                txtAddress.Text = currentInsuranceCompany.CompanyAddress;
                txtCity.Text = currentInsuranceCompany.CompanyCity;
                txtEmail.Text = currentInsuranceCompany.CompanyEmail;
                txtFax.Text = currentInsuranceCompany.CompanyFax;
                txtInsuranceCompanyName.Text = currentInsuranceCompany.InsuranceCompany1;
                txtPhone.Text = currentInsuranceCompany.CompanyPhone;
                txtPostal.Text = currentInsuranceCompany.CompanyPostal;
                cboCountry.SelectedItem = currentInsuranceCompany.CompanyCountry;
                cboProvince.SelectedItem = currentInsuranceCompany.CompanyProvince;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (DealershipManagerEntities context = new DealershipManagerEntities())
            {
                if (currentInsuranceCompany != null)
                {
                    //It exists, find it, and edit it.
                    InsuranceCompany insuranceCompany = (from insCompany in context.InsuranceCompanies
                                                         where insCompany.CompanyId == currentInsuranceCompany.CompanyId
                                                         select insCompany).FirstOrDefault();
                    updateInsuranceCompany(insuranceCompany);
                    context.SaveChanges();
                    context.Connection.Close();
                }
                else
                {
                    InsuranceCompany insuranceCompany = new InsuranceCompany();
                    updateInsuranceCompany(insuranceCompany);
                    context.InsuranceCompanies.AddObject(insuranceCompany);
                    context.SaveChanges();
                    context.Connection.Close();
                }
            }
            MessageBox.Show("Insurance Company has been saved");
            customerForm.insuranceCompanyTableAdapter.Fill(customerForm.dealershipManagerDataSet.InsuranceCompany);
            customerForm.cboInsuranceCompany.SelectedItem = currentInsuranceCompany;
            customerForm.currentInsuranceCompany = currentInsuranceCompany;
            customerForm.BindInsuranceCompanyToForm();
            this.Close();
        }

        private void updateInsuranceCompany(InsuranceCompany c)
        {
            c.InsuranceCompany1 = txtInsuranceCompanyName.Text;
            c.CompanyAddress = txtAddress.Text;
            c.CompanyCity = txtCity.Text;
            c.CompanyEmail = txtEmail.Text;
            c.CompanyFax = txtFax.Text;
            c.CompanyPhone = txtPhone.Text;
            c.CompanyPostal = txtPostal.Text;

            if (null != cboCountry.SelectedItem)
                c.CompanyCountry = cboCountry.SelectedItem.ToString();

            if (null != cboProvince.SelectedItem)
                c.CompanyProvince = cboProvince.SelectedItem.ToString();
        }
    }
}
