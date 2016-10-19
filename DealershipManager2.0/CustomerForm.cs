using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace DealershipManager2._0
{
    public partial class CustomerForm : Form
    {

        public DealForm dealHolder;
        public Customer currentCustomer;
        Insurance currentInsurance;
        LeasePayment currentLeasePayment;
        Finance currentFinancePayment;
        List<Insurance> allInsurances;
        public List<InsuranceVehicle> allInsuranceVehicles;
        List<object> allLeasesFinances;
        int leaseSaleIndex;
        object currentLeaseFinance;
        List<LeasePayment> currentLeasePayments;
        List<Finance> currentFinancePayments;
        public InsuranceBroker currentInsuranceBroker;
        public InsuranceCompany currentInsuranceCompany;
        InsuranceVehicle currentInsuranceVehicle;
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
        Sale currentSaleHistory;
        bool isNewCustomer;

        public CustomerForm()
        {
            InitializeComponent();
            dtpBirthday.Format = DateTimePickerFormat.Custom;
        }

        public CustomerForm(Customer c)
        {
            currentCustomer = c;
            isNewCustomer = false;
            InitializeComponent();
            dtpBirthday.Format = DateTimePickerFormat.Custom;
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            allInsuranceVehicles = new List<InsuranceVehicle>();
            if (dealHolder != null)
            {
                btnReturnToDeal.Visible = true;
            }
            // TODO: This line of code loads data into the 'dealershipManagerDataSet.InsuranceBroker' table. You can move, or remove it, as needed.
            this.insuranceBrokerTableAdapter.Fill(this.dealershipManagerDataSet.InsuranceBroker);

            // TODO: This line of code loads data into the 'dealershipManagerDataSet.InsuranceCompany' table. You can move, or remove it, as needed.
            this.insuranceCompanyTableAdapter.Fill(this.dealershipManagerDataSet.InsuranceCompany);
            cboInsuranceBroker.ValueMember = "BrokerID";
            cboInsuranceCompany.ValueMember = "CompanyID";

            lbxCustomers.ValueMember = "Id";
            BindCustomersListBox();
            cboInsuranceCompany_SelectedIndexChanged(this, EventArgs.Empty);
            cboInsuranceBroker_SelectedIndexChanged(this, EventArgs.Empty);
            if (currentCustomer != null)
            {
                try
                {
                    foreach (Customer c in lbxCustomers.Items)
                    {
                        if (c.Id == currentCustomer.Id)
                        {
                            lbxCustomers.SelectedItem = c;
                            lbxCustomers_SelectedIndexChanged(this, EventArgs.Empty);
                        }
                    }
                    SetCustomerNameLabels(currentCustomer.FirstName + " " + currentCustomer.LastName);
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                }
            }
            BindInsuranceBrokerToForm();
            BindInsuranceCompanyToForm();


            this.Size = new Size(Parent.Size.Width * 2, Parent.Size.Height * 2);
        }

        private void DisableCustomerFields()
        {
            txtFirstName.Enabled = false;
            txtLastName.Enabled = false;
            txtNotes.Enabled = false;
            txtHomePhone.Enabled = false;
            txtWorkPhone.Enabled = false;
            txtOtherPhone.Enabled = false;
            txtEmail.Enabled = false;
            txtAddress.Enabled = false;
            txtPostal.Enabled = false;
            txtCity.Enabled = false;
            cboProvince.Enabled = false;
            cboCountry.Enabled = false;
            dtpBirthday.Enabled = false;
            txtBandName.Enabled = false;
            txtBandNumber.Enabled = false;
            txtCrossReference.Enabled = false;

        }

        private void EnableCustomerFields()
        {
            txtFirstName.Enabled = true;
            txtLastName.Enabled = true;
            txtNotes.Enabled = true;
            txtHomePhone.Enabled = true;
            txtWorkPhone.Enabled = true;
            txtOtherPhone.Enabled = true;
            txtEmail.Enabled = true;
            txtAddress.Enabled = true;
            txtCity.Enabled = true;
            txtPostal.Enabled = true;
            cboProvince.Enabled = true;
            cboCountry.Enabled = true;
            dtpBirthday.Enabled = true;
            txtBandName.Enabled = true;
            txtBandNumber.Enabled = true;
            txtCrossReference.Enabled = true;
        }

        private void SetCustomerNameLabels(string customerName)
        {
            lblInsuranceCustomerName.Text = customerName;
            lblPaymentInfoCustomerName.Text = customerName;
            lblSalesHistoryCustomerName.Text = customerName;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            BindCustomerToForm();
            DisableCustomerFields();
        }

        public void BindInsuranceCompanyToForm()
        {
            if (null != currentInsuranceCompany)
            {
                string breakpoint = "TEST!";
                cboInsuranceCompany.SelectedValue = currentInsuranceCompany.CompanyId;
                txtInsuranceCompanyAddress.Text = currentInsuranceCompany.CompanyAddress;
                txtInsuranceCompanyCity.Text = currentInsuranceCompany.CompanyCity;
                cboInsuranceCompanyProvince.SelectedItem = currentInsuranceCompany.CompanyProvince;
                txtInsuranceCompanyPostal.Text = currentInsuranceCompany.CompanyPostal;
                cboInsuranceCompanyCountry.SelectedItem = currentInsuranceCompany.CompanyCountry;
                txtInsuranceCompanyPhone.Text = currentInsuranceCompany.CompanyPhone;
                txtInsuranceCompanyFax.Text = currentInsuranceCompany.CompanyFax;
                txtInsuranceCompanyEmail.Text = currentInsuranceCompany.CompanyEmail;
            }
        }

        public void BindInsuranceBrokerToForm()
        {
            if (null != currentInsuranceBroker)
            {
                cboInsuranceBroker.SelectedValue = currentInsuranceBroker.BrokerId;
                txtInsuranceBrokerAddress.Text = currentInsuranceBroker.BrokerAddress;
                txtInsuranceBrokerCity.Text = currentInsuranceBroker.BrokerCity;
                cboInsuranceBrokerProvince.SelectedItem = currentInsuranceBroker.BrokerProvince;
                cboInsuranceBrokerCountry.SelectedItem = currentInsuranceBroker.BrokerCountry;
                txtInsuranceBrokerPostalCode.Text = currentInsuranceBroker.BrokerPostal;
                txtInsuranceBrokerPhone.Text = currentInsuranceBroker.BrokerPhone;
                txtInsuranceBrokerFax.Text = currentInsuranceBroker.BrokerFax;
                txtInsuranceBrokerEmail.Text = currentInsuranceBroker.BrokerEmail;
            }
        }

        private void BindCustomerToForm()
        {
            if (currentCustomer != null)
            {
                txtFirstName.Text = currentCustomer.FirstName;
                txtLastName.Text = currentCustomer.LastName;
                txtAddress.Text = currentCustomer.Street;
                txtHomePhone.Text = currentCustomer.HomePhone;
                txtCity.Text = currentCustomer.City;
                txtWorkPhone.Text = currentCustomer.BusPhone;
                cboProvince.SelectedItem = currentCustomer.Province;
                txtOtherPhone.Text = currentCustomer.OtherPhone;
                txtPostal.Text = currentCustomer.Postal;
                txtEmail.Text = currentCustomer.Email;
                cboCountry.SelectedItem = currentCustomer.Country;
                try
                {
                    dtpBirthday.Value = DateTime.Parse(currentCustomer.Birthdate);
                }
                catch { }
                txtNotes.Text = currentCustomer.Notes;
                txtBandName.Text = currentCustomer.BandName;
                txtBandNumber.Text = currentCustomer.BandNumber;
                txtCrossReference.Text = currentCustomer.Crossreference;

                //cboInsuranceCompany
                BindInsuranceCompanyToForm();
                BindInsuranceBrokerToForm();

                if (currentInsurance != null)
                {
                    txtInsurancePolicyNumber.Text = currentInsurance.PolicyNumber;
                    txtInsuranceCoverage.Text = (currentInsurance.Coverage ?? 0).ToString();
                    dtpExpiryDate.Value = currentInsurance.ExpiryDate ?? DateTime.Today;
                    txtInsuranceNotes.Text = currentInsurance.Notes;
                }
            }
        }

        private void CreateCustomerFromForm()
        {
            currentCustomer.FirstName = txtFirstName.Text;
            currentCustomer.LastName = txtLastName.Text;
            currentCustomer.Street = txtAddress.Text;
            currentCustomer.HomePhone = txtHomePhone.Text;
            currentCustomer.City = txtCity.Text;
            currentCustomer.BusPhone = txtWorkPhone.Text;
            currentCustomer.Province = cboProvince.SelectedText;
            currentCustomer.OtherPhone = txtOtherPhone.Text;
            currentCustomer.Postal = txtPostal.Text;
            currentCustomer.Email = txtEmail.Text;
            currentCustomer.Country = cboCountry.SelectedText;
            try
            {
                currentCustomer.Birthdate = dtpBirthday.Value.ToString();
            }
            catch { }
            currentCustomer.Notes = txtNotes.Text;
            currentCustomer.BandName = txtBandName.Text;
            currentCustomer.BandNumber = txtBandNumber.Text;
            currentCustomer.Crossreference = txtCrossReference.Text;
        }

        private void BindCustomersListBox()
        {
            lbxCustomers.Items.Clear();
            using (DealershipManagerEntities context = new DealershipManagerEntities())
            {
                List<Customer> customerQuery = (from customer in context.Customers
                                                select customer).ToList<Customer>();

                customerQuery = customerQuery.OrderBy(x => x.LastName).ThenBy(y => y.FirstName).ToList();
                try
                {
                    foreach (var customer in customerQuery)
                    {
                        lbxCustomers.Items.Add(customer);
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void EmptyVariables()
        {
            currentLeaseFinance = null;
            currentLeasePayment = null;
            currentLeasePayments = null;
            currentFinancePayments = null;
            currentFinancePayment = null;
            txtFinanceVIN.Text = "";
            txtFinanceMake.Text = "";
            txtFinanceModel.Text = "";
            txtPaymentDue.Text = "";
            txtNoOfPayments.Text = "";
            txtBalanceOwing.Text = "";
            txtSalesHistoryMake.Text = "";
            txtSalesHistoryMileage.Text = "";
            txtSalesHistoryModel.Text = "";
            txtSalesHistoryNotes.Text = "";
            txtSalesHistorySalesPerson.Text = "";
            txtSalesHistoryVIN.Text = "";
            txtSalesHistoryYear.Text = "";
        }

        private void lbxCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            EmptyVariables();
            currentCustomer = (Customer)lbxCustomers.SelectedItem;
            SetCustomerNameLabels(currentCustomer.FirstName + " " + currentCustomer.LastName);


            if (null != currentCustomer)
            {
                currentLeaseFinance = null;
                currentInsurance = null;
                allInsuranceVehicles = new List<InsuranceVehicle>();
                BindInsuranceStuff();
                BindPaymentInfo();
                BindSalesHistory();
                BindCustomerToForm();
                dtpPaymentDate.Value = DateTime.Today;
                txtPayment.Text = "";
                txtAddin.Text = "";
                txtPaymentDescription.Text = "";
            }

            clearFinancePaymentSelection();
        }

        public void BindSalesHistory()
        {
            lbxSalesHistory.Items.Clear();
            lbxLeaseHistory.Items.Clear();
            using (DealershipManagerEntities context = new DealershipManagerEntities())
            {
                IQueryable<Sale> saleQuery = from sale in context.Sales
                                             where sale.CustomerId == currentCustomer.Id
                                             orderby sale.Date descending
                                             select sale;
                foreach (Sale s in saleQuery)
                {
                    lbxSalesHistory.Items.Add(s);
                }
            }

            using (DealershipManagerEntities context = new DealershipManagerEntities())
            {
                IQueryable<Lease> leaseQuery = from lease in context.Leases
                                               where lease.CustomerId == currentCustomer.Id
                                               orderby lease.Date descending
                                               select lease;

                foreach (Lease l in leaseQuery)
                {
                    lbxLeaseHistory.Items.Add(l);
                }
            }
        }

        public void BindFinanceInfo()
        {
            currentFinancePayment = null;
            currentLeasePayment = null;
            currentFinancePayments.Clear();
            currentLeasePayments.Clear();
            dgvPaymentInfo.DataSource = null;
            dgvPaymentInfo.Refresh();
            //Check if we're on current lease or current finance... probably need some kind of index...
            if (currentLeaseFinance.GetType() == typeof(Lease))
            {
                gpboxLeaseInfo.Visible = true;
                gbxLeasePayment.Visible = true;
                gbxFinancePayment.Visible = false;
                gpboxFinanceInfo.Visible = false;
                Lease currentLease = (Lease)currentLeaseFinance;

                using (DealershipManagerEntities context = new DealershipManagerEntities())
                {
                    Vehicle vehicleQuery = (from vehicle in context.Vehicles
                                            where vehicle.Id == currentLease.VehicleId
                                            select vehicle).FirstOrDefault();

                    txtPaymentInfoVIN.Text = vehicleQuery.VIN;
                    txtPaymentInfoMake.Text = vehicleQuery.Make;
                    txtPaymentInfoModel.Text = vehicleQuery.Model;
                    txtPaymentInfoLeaseAmount.Text = currentLease.TotalCostofVehicle.ToString();
                    txtPaymentInfoMonthlyPayment.Text = currentLease.PaymentAmount.ToString();
                    txtPaymentInfoNumberOfMonths.Text = currentLease.NoPayments.ToString();
                    txtPaymentInfoRate.Text = currentLease.Rate.ToString();

                    IQueryable<LeasePayment> leasePaymentQuery = from leasePayment in context.LeasePayments
                                                                 where leasePayment.LeaseId == currentLease.Id
                                                                 orderby leasePayment.DatePaid ascending
                                                                 select leasePayment;

                    leasePaymentQuery = leasePaymentQuery.OrderBy(x => x.PmtNo);
                    foreach (LeasePayment lp in leasePaymentQuery)
                    {
                        currentLeasePayments.Add(lp);
                    }
                    dgvPaymentInfo.DataSource = null;
                    dgvPaymentInfo.DataBindings.Clear();

                    dgvPaymentInfo.DataSource = currentLeasePayments;
                    dgvPaymentInfo.Refresh();

                    //if (dgvPaymentInfo.Rows.Count < 2)
                    //{

                    //}
                }

                if (dgvPaymentInfo.Columns["leaseId"] != null)
                    dgvPaymentInfo.Columns["leaseId"].Visible = false;

                if (dgvPaymentInfo.Columns["Payment"] != null)
                    dgvPaymentInfo.Columns["Payment"].Visible = false;

                if (dgvPaymentInfo.Columns["Description"] != null)
                    dgvPaymentInfo.Columns["Description"].Visible = false;

                if (dgvPaymentInfo.Columns["Short"] != null)
                    dgvPaymentInfo.Columns["Short"].Visible = false;

                if (dgvPaymentInfo.Columns["Addin"] != null)
                    dgvPaymentInfo.Columns["Addin"].Visible = false;

                if (dgvPaymentInfo.Columns["Open"] != null)
                    dgvPaymentInfo.Columns["Open"].Visible = false;

                if (dgvPaymentInfo.Columns["PmtNo"] != null)
                    dgvPaymentInfo.Columns["PmtNo"].Visible = true;

                if (dgvPaymentInfo.Columns["PaymentAmount"] != null)
                    dgvPaymentInfo.Columns["PaymentAmount"].Visible = true;

                if (dgvPaymentInfo.Columns["Principle"] != null)
                    dgvPaymentInfo.Columns["Principle"].Visible = true;

                if (dgvPaymentInfo.Columns["Interest"] != null)
                    dgvPaymentInfo.Columns["Interest"].Visible = true;

                if (dgvPaymentInfo.Columns["GST"] != null)
                    dgvPaymentInfo.Columns["GST"].Visible = true;
            }
            else if (currentLeaseFinance.GetType() == typeof(Sale))
            {
                gpboxLeaseInfo.Visible = false;
                gbxLeasePayment.Visible = false;
                gbxFinancePayment.Visible = true;
                gpboxFinanceInfo.Visible = true;
                Sale currentSale = (Sale)currentLeaseFinance;

                using (DealershipManagerEntities context = new DealershipManagerEntities())
                {
                    Vehicle vehicleQuery = (from vehicle in context.Vehicles
                                            where vehicle.Id == currentSale.VehicleId
                                            select vehicle).FirstOrDefault();


                    txtFinanceVIN.Text = vehicleQuery.VIN;
                    txtFinanceMake.Text = vehicleQuery.Make;
                    txtFinanceModel.Text = vehicleQuery.Model;
                    txtPaymentDue.Text = currentSale.PaymentDue.ToString();
                    txtNoOfPayments.Text = currentSale.noPayments.ToString();
                    chkFinanceCurrent.Checked = currentSale.current;

                    IQueryable<Finance> financePaymentQuery = from finance in context.Finances
                                                              where finance.saleId == currentSale.id
                                                              orderby finance.DatePaid ascending
                                                              select finance;

                    financePaymentQuery = financePaymentQuery.OrderBy(x => x.DatePaid);
                    foreach (Finance finance in financePaymentQuery)
                    {
                        currentFinancePayments.Add(finance);
                        txtBalanceOwing.Text = finance.Balance.ToString();
                    }

                    dgvPaymentInfo.DataSource = currentFinancePayments;
                    dgvPaymentInfo.Refresh();
                    if (dgvPaymentInfo.Rows.Count < 2)
                    {
                        txtPaymentInfoLeaseAmount.Text = "";
                        txtPaymentInfoMake.Text = "";
                        txtPaymentInfoModel.Text = "";
                        txtPaymentInfoMonthlyPayment.Text = "";
                        txtPaymentInfoNumberOfMonths.Text = "";
                        txtPaymentInfoRate.Text = "";
                        txtPaymentInfoVIN.Text = "";
                    }

                    if (dgvPaymentInfo.Columns["Payment"] != null)
                        dgvPaymentInfo.Columns["Payment"].Visible = true;

                    if (dgvPaymentInfo.Columns["Description"] != null)
                        dgvPaymentInfo.Columns["Description"].Visible = true;

                    if (dgvPaymentInfo.Columns["Short"] != null)
                        dgvPaymentInfo.Columns["Short"].Visible = true;

                    if (dgvPaymentInfo.Columns["Addin"] != null)
                        dgvPaymentInfo.Columns["Addin"].Visible = true;

                    if (dgvPaymentInfo.Columns["Open"] != null)
                        dgvPaymentInfo.Columns["Open"].Visible = true;

                    if (dgvPaymentInfo.Columns["PmtNo"] != null)
                        dgvPaymentInfo.Columns["PmtNo"].Visible = false;

                    if (dgvPaymentInfo.Columns["PaymentAmount"] != null)
                        dgvPaymentInfo.Columns["PaymentAmount"].Visible = false;

                    if (dgvPaymentInfo.Columns["Principle"] != null)
                        dgvPaymentInfo.Columns["Principle"].Visible = false;

                    if (dgvPaymentInfo.Columns["Interest"] != null)
                        dgvPaymentInfo.Columns["Interest"].Visible = false;

                    if (dgvPaymentInfo.Columns["GST"] != null)
                        dgvPaymentInfo.Columns["GST"].Visible = false;

                    if (dgvPaymentInfo.Columns["saleId"] != null)
                        dgvPaymentInfo.Columns["saleId"].Visible = false;
                }
            }
            else
            {
                //WTF happened here?
                txtPaymentInfoLeaseAmount.Text = "";
                txtPaymentInfoMake.Text = "";
                txtPaymentInfoModel.Text = "";
                txtPaymentInfoMonthlyPayment.Text = "";
                txtPaymentInfoNumberOfMonths.Text = "";
                txtPaymentInfoRate.Text = "";
                txtPaymentInfoVIN.Text = "";
            }


            dgvPaymentInfo.Columns["Id"].Visible = false;
            dgvPaymentInfo.Columns["DatePaid"].DefaultCellStyle.Format = "d";

            clearFinancePaymentSelection();
        }

        public void BindPaymentInfo()
        {
            currentFinancePayments = new List<Finance>();
            currentLeasePayments = new List<LeasePayment>();
            allLeasesFinances = new List<object>();

            gpboxLeaseInfo.Visible = false;
            using (DealershipManagerEntities context = new DealershipManagerEntities())
            {
                IQueryable<Lease> leaseQuery = from lease in context.Leases
                                               where lease.CustomerId == currentCustomer.Id && lease.Current == true
                                               select lease;

                IQueryable<Sale> saleQuery = from sale in context.Sales
                                             where sale.CustomerId == currentCustomer.Id && sale.current == true
                                             select sale;

                foreach (Lease l in leaseQuery)
                {
                    allLeasesFinances.Add(l);
                }

                foreach (Sale s in saleQuery)
                {
                    allLeasesFinances.Add(s);
                }

                if (allLeasesFinances.Count > 0)
                {
                    currentLeaseFinance = allLeasesFinances[0];
                }

                if (currentLeaseFinance != null)
                {
                    leaseSaleIndex = 1;
                    lblPaymentInfo.Text = "1 of " + allLeasesFinances.Count.ToString();
                    BindFinanceInfo();
                }
                else
                {
                    lblPaymentInfo.Text = "No Payment Info on record for this customer.";
                    dgvPaymentInfo.DataSource = null;
                    dgvPaymentInfo.Refresh();
                    txtPaymentInfoLeaseAmount.Text = null;
                    txtPaymentInfoMake.Text = null;
                    txtPaymentInfoModel.Text = null;
                    txtPaymentInfoMonthlyPayment.Text = null;
                    txtPaymentInfoNumberOfMonths.Text = null;
                    txtPaymentInfoRate.Text = null;
                    txtPaymentInfoVIN.Text = null;
                }
            }

            clearFinancePaymentSelection();
        }

        public void BindOtherInsuranceInfo()
        {
            allInsuranceVehicles.Clear();
            using (DealershipManagerEntities context = new DealershipManagerEntities())
            {
                IQueryable<InsuranceBroker> insuranceBrokerQuery = from broker in context.InsuranceBrokers
                                                                   where broker.BrokerId == currentInsurance.Brokerid
                                                                   select broker;


                InsuranceBroker insBroker = insuranceBrokerQuery.FirstOrDefault();
                if (null == insBroker)
                {
                    insBroker = new InsuranceBroker();
                }
                currentInsuranceBroker = insBroker;
            }
            using (DealershipManagerEntities context = new DealershipManagerEntities())
            {
                IQueryable<InsuranceCompany> insuranceCompanyQuery = from insuranceCompany in context.InsuranceCompanies
                                                                     where insuranceCompany.CompanyId == currentInsurance.InsuranceCompanyId
                                                                     select insuranceCompany;

                InsuranceCompany insCompany = insuranceCompanyQuery.FirstOrDefault();
                if (null == insCompany)
                {
                    insCompany = new InsuranceCompany();
                }
                currentInsuranceCompany = insCompany;
            }

            using (DealershipManagerEntities context = new DealershipManagerEntities())
            {
                IQueryable<InsuranceVehicle> insuranceVehicleQuery = from insuranceVehicle in context.InsuranceVehicles
                                                                     where insuranceVehicle.insuranceid == currentInsurance.id
                                                                     select insuranceVehicle;



                foreach (InsuranceVehicle insv in insuranceVehicleQuery)
                {
                    if (!allInsuranceVehicles.Exists(x => x.id == insv.id))
                        allInsuranceVehicles.Add(insv);
                }
            }
            txtInsurancePolicyNumber.Text = currentInsurance.PolicyNumber;
            txtInsuranceCoverage.Text = (currentInsurance.Coverage ?? 0).ToString();
            dtpExpiryDate.Value = currentInsurance.ExpiryDate ?? DateTime.Today;
            txtInsuranceNotes.Text = currentInsurance.Notes;

            BindInsuranceBrokerToForm();
            BindInsuranceCompanyToForm();
            BindInsuranceVehiclesToForm();
        }

        public void BindInsuranceVehiclesToForm()
        {
            lbxInsuredVehicles.Items.Clear();
            foreach (InsuranceVehicle insv in allInsuranceVehicles)
            {
                using (DealershipManagerEntities context = new DealershipManagerEntities())
                {
                    Vehicle vehicleQuery = (from vehicle in context.Vehicles
                                            where vehicle.Id == insv.VehicleId
                                            select vehicle).FirstOrDefault();

                    lbxInsuredVehicles.Items.Add(vehicleQuery);
                }
            }
        }

        public void BindInsuranceStuff()
        {
            lbxInsuredVehicles.Items.Clear();
            allInsurances = new List<Insurance>();
            //Insurance Stuff
            //
            using (DealershipManagerEntities context = new DealershipManagerEntities())
            {
                IQueryable<Insurance> insuranceQuery = from insurance in context.Insurances
                                                       where insurance.CustomerId == currentCustomer.Id
                                                       select insurance;

                foreach (Insurance tempInsurance in insuranceQuery)
                {
                    allInsurances.Add(tempInsurance);
                    currentInsurance = allInsurances[0];
                }
            }

            if (currentInsurance != null)
            {
                lblInsuranceCount.Text = "1 of " + allInsurances.Count.ToString();
                BindOtherInsuranceInfo();
            }
            else
            {
                currentInsurance = new Insurance();
                currentInsuranceBroker = null;
                currentInsuranceCompany = null;
                currentInsuranceVehicle = null;
                lblInsuranceCount.Text = "No Insurance on record for this customer.";
                txtInsuranceBrokerAddress.Text = "";
                txtInsuranceBrokerCity.Text = "";
                txtInsuranceBrokerEmail.Text = "";
                txtInsuranceBrokerFax.Text = "";
                txtInsuranceBrokerPhone.Text = "";
                txtInsuranceBrokerPostalCode.Text = "";
                txtInsuranceCompanyAddress.Text = "";
                txtInsuranceCompanyCity.Text = "";
                txtInsuranceCompanyEmail.Text = "";
                txtInsuranceCompanyFax.Text = "";
                txtInsuranceCompanyPhone.Text = "";
                txtInsuranceCompanyPostal.Text = "";
                txtInsuranceCoverage.Text = "";
                dtpExpiryDate.Value = DateTime.Today;
                txtInsuranceNotes.Text = "";
                txtInsurancePolicyNumber.Text = "";
                cboInsuranceBroker.SelectedIndex = -1;
                cboInsuranceBrokerProvince.SelectedIndex = -1;
                cboInsuranceCompany.SelectedIndex = -1;
                cboInsuranceCompanyProvince.SelectedIndex = -1;

                //BindOtherInsuranceInfo();                
            }
        }

        private void lbxCustomers_Format(object sender, ListControlConvertEventArgs e)
        {
            string firstName = ((Customer)e.ListItem).FirstName.ToString();
            string lastName = ((Customer)e.ListItem).LastName.ToString();

            e.Value = lastName + ", " + firstName;
        }

        private void lbxSalesHistory_Format(object sender, ListControlConvertEventArgs e)
        {
            string vehicleInfo = "";
            int id = ((Sale)e.ListItem).VehicleId;
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

                vehicleInfo = veh.Year.ToString() + " " + veh.Make.ToString() + " " + veh.Model.ToString();
            }

            string VID = ((Sale)e.ListItem).Date.ToShortDateString() + " - " + vehicleInfo;

            e.Value = VID;
        }

        private void bindCustomer(Customer c)
        {
            c.BandName = currentCustomer.BandName;
            c.BandNumber = currentCustomer.BandNumber;
            c.Birthdate = currentCustomer.Birthdate;
            c.BusPhone = currentCustomer.BusPhone;
            c.City = currentCustomer.City;
            c.Country = currentCustomer.Country;
            c.Crossreference = currentCustomer.Crossreference;
            c.Email = currentCustomer.Email;
            c.FirstName = currentCustomer.FirstName;
            c.LastName = currentCustomer.LastName;
            c.Notes = currentCustomer.Notes;
            c.OtherPhone = currentCustomer.OtherPhone;
            c.Postal = currentCustomer.Postal;
            c.Province = currentCustomer.Province;
            c.Street = currentCustomer.Street;
        }

        private void btnSaveCustomer_Click(object sender, EventArgs e)
        {
            DisableCustomerFields();
            CreateCustomerFromForm();

            using (DealershipManagerEntities context = new DealershipManagerEntities())
            {
                if (isNewCustomer)
                {
                    Customer newCustomer = currentCustomer;
                    context.AddToCustomers(newCustomer);
                }
                else
                {
                    var customer = (from c in context.Customers
                                    where c.Id == currentCustomer.Id
                                    select c).First();
                    bindCustomer(customer);
                }
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                }
                isNewCustomer = false;
            }
            BindCustomersListBox();
            lbxCustomers.SelectedItem = currentCustomer;
        }

        public void btnNewCustomer_Click(object sender, EventArgs e)
        {
            isNewCustomer = true;
            using (DealershipManagerEntities context = new DealershipManagerEntities())
            {
                Customer newCustomer = new Customer();
                try
                {
                    newCustomer.FirstName = "New";
                    newCustomer.LastName = "Customer";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + " " + ex.InnerException.Message);
                    string test = ex.Message;
                }

                BindCustomersListBox();
                lbxCustomers.SelectedItem = newCustomer;
                currentCustomer = newCustomer;
                BindCustomerToForm();
                EnableCustomerFields();
                txtFirstName.Focus();
                txtHomePhone.Text = "403-";
                txtWorkPhone.Text = "403-";
                txtOtherPhone.Text = "403-";
                cboProvince.SelectedItem = "AB";
                cboCountry.SelectedItem = "Canada";
            }
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

        private void btnEditCustomer_Click(object sender, EventArgs e)
        {
            EnableCustomerFields();
        }

        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this customer?", "ALERT", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (DealershipManagerEntities context = new DealershipManagerEntities())
                {
                    var customer = (from c in context.Customers
                                    where c.Id == currentCustomer.Id
                                    select c).First();

                    context.Customers.DeleteObject(customer);
                    context.SaveChanges();
                }

            };
            BindCustomersListBox();
            DisableCustomerFields();
        }

        private void cboInsuranceCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (null != cboInsuranceCompany.SelectedValue)
            {
                int id = (int)cboInsuranceCompany.SelectedValue;
                using (DealershipManagerEntities context = new DealershipManagerEntities())
                {
                    IQueryable<InsuranceCompany> insuranceCompanyQuery = from insuranceCompany in context.InsuranceCompanies
                                                                         where insuranceCompany.CompanyId == id
                                                                         select insuranceCompany;

                    InsuranceCompany insCompany = insuranceCompanyQuery.FirstOrDefault();
                    if (null == insCompany)
                    {
                        insCompany = new InsuranceCompany();
                    }
                    currentInsuranceCompany = insCompany;
                    BindInsuranceCompanyToForm();
                }
            }
        }

        private void cboInsuranceBroker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (null != cboInsuranceBroker.SelectedValue)
            {
                int id = (int)cboInsuranceBroker.SelectedValue;
                using (DealershipManagerEntities context = new DealershipManagerEntities())
                {
                    IQueryable<InsuranceBroker> insuranceBrokerQuery = from insuranceBroker in context.InsuranceBrokers
                                                                       where insuranceBroker.BrokerId == id
                                                                       select insuranceBroker;

                    InsuranceBroker insBroker = insuranceBrokerQuery.FirstOrDefault();
                    if (null == insBroker)
                    {
                        insBroker = new InsuranceBroker();
                    }
                    currentInsuranceBroker = insBroker;
                    BindInsuranceBrokerToForm();
                }
            }
        }

        private void lbxSalesHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                currentSaleHistory = (Sale)lbxSalesHistory.SelectedItem;


                using (DealershipManagerEntities context = new DealershipManagerEntities())
                {
                    IQueryable<Vehicle> vehicleQuery = from vehicle in context.Vehicles
                                                       where vehicle.Id == currentSaleHistory.VehicleId
                                                       select vehicle;

                    Vehicle veh = vehicleQuery.FirstOrDefault();
                    if (null == veh)
                    {
                        veh = new Vehicle();
                    }

                    //IQueryable<Lease> leaseQuery = from lease in context.Leases
                    //                               where lease.VehicleId == veh.Id
                    //                               select lease;

                    //Lease vehicleLease = leaseQuery.FirstOrDefault();
                    //if (null == vehicleLease)
                    //{
                    rdbSalesHistorySale.Checked = true;
                    //}
                    //else
                    //{
                    //    rdbSalesHistoryLease.Checked = true;
                    //}

                    Staff salesperson = (from staff in context.Staffs
                                         where staff.Id == currentSaleHistory.SalesPerson
                                         select staff).FirstOrDefault();

                    txtSalesHistoryMake.Text = veh.Make.ToString();
                    txtSalesHistoryMileage.Text = currentSaleHistory.VehicleMilage.ToString();
                    txtSalesHistoryModel.Text = veh.Model.ToString();
                    txtSalesHistoryNotes.Text = currentSaleHistory.Notes.ToString();
                    txtSalesHistorySalesPerson.Text = salesperson.FirstName + " " + salesperson.LastName;
                    txtSalesHistoryVIN.Text = veh.VIN.ToString();
                    txtSalesHistoryYear.Text = veh.Year.ToString();
                }
            }
            catch
            {

            }
        }

        private void btnEditSaleLease_Click(object sender, EventArgs e)
        {
            if (rdbSalesHistoryLease.Checked)
            {
                using (DealershipManagerEntities context = new DealershipManagerEntities())
                {
                    Lease lease = (Lease)lbxLeaseHistory.SelectedItem;

                    DealForm dealform = new DealForm(lease);
                    dealform.MdiParent = this.MdiParent;
                    dealform.Show();
                }
            }
            else if (rdbSalesHistorySale.Checked)
            {
                using (DealershipManagerEntities context = new DealershipManagerEntities())
                {
                    Sale sale = (Sale)lbxSalesHistory.SelectedItem;
                    DealForm dealform = new DealForm(sale);
                    dealform.MdiParent = this.MdiParent;
                    dealform.Show();
                }
            }
        }

        private void btnSearchByName_Click(object sender, EventArgs e)
        {
            string firstName = txtSearchFirstName.Text;
            string lastName = txtSearchLastName.Text;

            using (DealershipManagerEntities context = new DealershipManagerEntities())
            {
                IQueryable<Customer> customerQuery = from customer in context.Customers
                                                     where (customer.FirstName == firstName || customer.LastName == lastName)
                                                     orderby customer.LastName, customer.FirstName
                                                     select customer;

                lbxCustomers.Items.Clear();
                foreach (Customer c in customerQuery)
                {
                    lbxCustomers.Items.Add(c);
                }

            }
            tabControl1.SelectedTab = tabControl1.TabPages["Customer"];

        }

        private void btnSearchByAddress_Click(object sender, EventArgs e)
        {
            string city = txtSearchCity.Text;
            string province = cboSearchProvince.SelectedText;
            string phone = txtSearchPhone.Text;

            using (DealershipManagerEntities context = new DealershipManagerEntities())
            {
                IQueryable<Customer> customerQuery = from customer in context.Customers
                                                     where (customer.City == city || customer.Province == province || customer.HomePhone.Contains(phone) || customer.BusPhone.Contains(phone) || customer.OtherPhone.Contains(phone))
                                                     orderby customer.LastName, customer.FirstName
                                                     select customer;

                lbxCustomers.Items.Clear();
                foreach (Customer c in customerQuery)
                {
                    lbxCustomers.Items.Add(c);
                }

            }
            tabControl1.SelectedTab = tabControl1.TabPages["Customer"];
        }

        private void btnSearchByBand_Click(object sender, EventArgs e)
        {
            lbxCustomers.Items.Clear();
            string bandName = txtSearchBandName.Text;
            string bandNumber = txtSearchBandNumber.Text;


            using (DealershipManagerEntities context = new DealershipManagerEntities())
            {
                IQueryable<Customer> customerQuery;
                if (!string.IsNullOrEmpty(bandName))
                {
                    customerQuery = from customer in context.Customers
                                    where (customer.BandName.ToUpper().Contains(bandName.ToUpper()))
                                    orderby customer.LastName, customer.FirstName
                                    select customer;

                    foreach (Customer c in customerQuery)
                    {
                        lbxCustomers.Items.Add(c);
                    }
                }
                if (!string.IsNullOrEmpty(bandNumber))
                {
                    customerQuery = from customer in context.Customers
                                    where (customer.BandNumber.ToUpper().Contains(bandNumber.ToUpper()))
                                    orderby customer.LastName, customer.FirstName
                                    select customer;
                    foreach (Customer c in customerQuery)
                    {
                        lbxCustomers.Items.Add(c);
                    }
                }

            }
            tabControl1.SelectedTab = tabControl1.TabPages["Customer"];
        }

        private void btnSearchByReference_Click(object sender, EventArgs e)
        {
            string crossReference = txtSearchCrossReference.Text;

            using (DealershipManagerEntities context = new DealershipManagerEntities())
            {
                IQueryable<Customer> customerQuery = from customer in context.Customers
                                                     where (customer.Crossreference.Contains(crossReference))
                                                     orderby customer.LastName, customer.FirstName
                                                     select customer;

                lbxCustomers.Items.Clear();
                foreach (Customer c in customerQuery)
                {
                    lbxCustomers.Items.Add(c);
                }

            }
            tabControl1.SelectedTab = tabControl1.TabPages["Customer"];
        }

        private void btnListAllCustomers_Click(object sender, EventArgs e)
        {
            BindCustomersListBox();
            tabControl1.SelectedTab = tabControl1.TabPages["Customer"];
        }

        private void btnListAllNatives_Click(object sender, EventArgs e)
        {
            using (DealershipManagerEntities context = new DealershipManagerEntities())
            {
                IQueryable<Customer> customerQuery = from customer in context.Customers
                                                     where (customer.BandName != "")
                                                     orderby customer.LastName, customer.FirstName
                                                     select customer;

                lbxCustomers.Items.Clear();
                foreach (Customer c in customerQuery)
                {
                    lbxCustomers.Items.Add(c);
                }

            }
            tabControl1.SelectedTab = tabControl1.TabPages["Customer"];
        }

        private void btnReturnToDeal_Click(object sender, EventArgs e)
        {
            try
            {
                dealHolder.Show();
            }
            catch
            {

            }
        }

        private void lbxInsuredVehicles_Format(object sender, ListControlConvertEventArgs e)
        {
            string year = ((Vehicle)e.ListItem).Year.ToString();
            string make = ((Vehicle)e.ListItem).Make ?? "".ToString() ?? "";
            string model = ((Vehicle)e.ListItem).Model ?? "".ToString() ?? "";

            e.Value = year + " " + make + " " + model;
        }

        private void btnRemoveInsuredVehicle_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to remove this vehicle from the insurance?", "ALERT", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    if (lbxInsuredVehicles.SelectedItem != null)
                    {
                        Vehicle removalVehicle = (Vehicle)lbxInsuredVehicles.SelectedItem;
                        lbxInsuredVehicles.Items.Remove(removalVehicle);
                        using (DealershipManagerEntities context = new DealershipManagerEntities())
                        {
                            IQueryable<InsuranceVehicle> InsuranceVehicleQuery = from vehicle in context.InsuranceVehicles
                                                                                 where (vehicle.VehicleId == removalVehicle.Id) && (vehicle.insuranceid == currentInsurance.id)
                                                                                 select vehicle;


                            //context.InsuranceVehicles.DeleteObject(InsuranceVehicleQuery.FirstOrDefault());
                            //context.SaveChanges();          
                            InsuranceVehicle insv = InsuranceVehicleQuery.FirstOrDefault();
                            InsuranceVehicle insv2 = allInsuranceVehicles.Where(x => x.id == insv.id).FirstOrDefault();
                            allInsuranceVehicles.Remove(insv2);
                            BindInsuranceVehiclesToForm();
                        }
                    }
                }
                catch
                {

                }
            }
        }

        private void btnAddInsuredVehicle_Click(object sender, EventArgs e)
        {
            SelectVehicle selectVehicleForm = new SelectVehicle(currentCustomer, currentInsurance);
            selectVehicleForm.Show();
            selectVehicleForm.customerForm = this;
        }

        private void btnViewLeaseInfo_Click(object sender, EventArgs e)
        {
            using (DealershipManagerEntities context = new DealershipManagerEntities())
            {
                Lease currentLease = (Lease)currentLeaseFinance;
                Lease leaseQuery = (from lease in context.Leases
                                    where lease.Id == currentLease.Id
                                    select lease).FirstOrDefault();

                DealForm dealform = new DealForm(leaseQuery);
                dealform.MdiParent = this.MdiParent;
                dealform.Show();

            }
        }

        private void btnViewSaleInfo_Click(object sender, EventArgs e)
        {
            using (DealershipManagerEntities context = new DealershipManagerEntities())
            {
                Sale currentSale = (Sale)currentLeaseFinance;
                Sale saleQuery = (from sale in context.Sales
                                  where sale.id == currentSale.id
                                  select sale).FirstOrDefault();

                DealForm dealform = new DealForm(saleQuery);
                dealform.MdiParent = this.MdiParent;
                dealform.Show();

            }
        }

        private void lbxLeaseHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Lease currentLeaseHistory = (Lease)lbxLeaseHistory.SelectedItem;

                using (DealershipManagerEntities context = new DealershipManagerEntities())
                {
                    IQueryable<Vehicle> vehicleQuery = from vehicle in context.Vehicles
                                                       where vehicle.Id == currentLeaseHistory.VehicleId
                                                       select vehicle;

                    Vehicle veh = vehicleQuery.FirstOrDefault();
                    if (null == veh)
                    {
                        veh = new Vehicle();
                    }

                    //IQueryable<Lease> leaseQuery = from lease in context.Leases
                    //                               where lease.VehicleId == veh.Id
                    //                               select lease;

                    //Lease vehicleLease = leaseQuery.FirstOrDefault();
                    //if (null == vehicleLease)
                    //{
                    //    rdbSalesHistorySale.Checked = true;
                    //}
                    //else
                    //{
                    rdbSalesHistoryLease.Checked = true;
                    //}
                    Staff salesperson = (from staff in context.Staffs
                                         where staff.Id == currentLeaseHistory.SalesPerson
                                         select staff).FirstOrDefault();

                    txtSalesHistoryMake.Text = veh.Make.ToString();
                    txtSalesHistoryMileage.Text = currentLeaseHistory.Milage.ToString();
                    txtSalesHistoryModel.Text = veh.Model.ToString();
                    txtSalesHistoryNotes.Text = currentLeaseHistory.notes.ToString();
                    txtSalesHistorySalesPerson.Text = salesperson.FirstName + " " + salesperson.LastName;
                    txtSalesHistoryVIN.Text = veh.VIN.ToString();
                    txtSalesHistoryYear.Text = veh.Year.ToString();
                }
            }
            catch
            {

            }
        }

        private void lbxLeaseHistory_Format(object sender, ListControlConvertEventArgs e)
        {
            string vehicleInfo = "";
            int id = ((Lease)e.ListItem).VehicleId;
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

                vehicleInfo = veh.Year.ToString() + " " + veh.Make.ToString() + " " + veh.Model.ToString();
            }

            string VID = ((Lease)e.ListItem).Date.Value.ToShortDateString() + " - " + vehicleInfo;

            e.Value = VID;
        }

        private void btnSalesHistoryEditVehicle_Click(object sender, EventArgs e)
        {
            Vehicle v = null;
            Lease currentLeaseHistory = (Lease)lbxLeaseHistory.SelectedItem;
            Sale currentSaleHistory = (Sale)lbxSalesHistory.SelectedItem;
            using (DealershipManagerEntities context = new DealershipManagerEntities())
            {
                if (currentLeaseHistory != null)
                {
                    IQueryable<Vehicle> vehicleQuery = from vehicle in context.Vehicles
                                                       where vehicle.Id == currentLeaseHistory.VehicleId
                                                       select vehicle;

                    v = vehicleQuery.First();
                }
                else if (currentSaleHistory != null)
                {
                    IQueryable<Vehicle> vehicleQuery = from vehicle in context.Vehicles
                                                       where vehicle.Id == currentSaleHistory.VehicleId
                                                       select vehicle;
                    v = vehicleQuery.First();
                }
                else
                {
                    //nothing selected, do nothing
                }

                if (v != null)
                {

                    VehicleForm vehicleForm = new VehicleForm(v);
                    vehicleForm.customerHolder = this;
                    vehicleForm.MdiParent = this.MdiParent;
                    this.Hide();
                    vehicleForm.Show();
                }
            }
        }

        private void btnSalesHistoryEditLien_Click(object sender, EventArgs e)
        {
            Vehicle v = null;
            Lease currentLeaseHistory = (Lease)lbxLeaseHistory.SelectedItem;
            Sale currentSaleHistory = (Sale)lbxSalesHistory.SelectedItem;
            using (DealershipManagerEntities context = new DealershipManagerEntities())
            {
                if (currentLeaseHistory != null)
                {
                    IQueryable<Vehicle> vehicleQuery = from vehicle in context.Vehicles
                                                       where vehicle.Id == currentLeaseHistory.VehicleId
                                                       select vehicle;

                    v = vehicleQuery.First();
                }
                else if (currentSaleHistory != null)
                {
                    IQueryable<Vehicle> vehicleQuery = from vehicle in context.Vehicles
                                                       where vehicle.Id == currentSaleHistory.VehicleId
                                                       select vehicle;
                    v = vehicleQuery.First();
                }
                else
                {
                    //nothing selected, do nothing
                }

                if (v != null)
                {
                    VehicleForm vehicleForm = new VehicleForm(v);
                    vehicleForm.customerHolder = this;
                    vehicleForm.MdiParent = this.MdiParent;
                    vehicleForm.tabControlVehicleForm.TabPages["HistoryLiens"].Select();
                    vehicleForm.tabControlVehicleForm.SelectedTab = vehicleForm.tabControlVehicleForm.TabPages["HistoryLiens"];
                    this.Hide();
                    vehicleForm.Show();
                }
            }
        }

        private void btnPrintableReport_Click(object sender, EventArgs e)
        {
            ////Open the print dialog
            //PrintDialog printDialog = new PrintDialog();
            //printDialog.Document = printDocument1;
            //printDialog.UseEXDialog = true;
            ////Get the document
            //if (DialogResult.OK == printDialog.ShowDialog())
            //{
            //    printDocument1.DocumentName = "Payment Information";
            //    printDocument1.Print();
            //}
            /*
            Note: In case you want to show the Print Preview Dialog instead of 
            Print Dialog then comment the above code and uncomment the following code
            */

            //Open the print preview dialog
            //PrintPreviewDialog objPPdialog = new PrintPreviewDialog();

            //printDocument1.DocumentName = "Payment Information";
            //objPPdialog.Document = printDocument1;
            //objPPdialog.ShowDialog();
            if (currentLeaseFinance.GetType() == typeof(Lease))
            {
                //Lease
                Lease selectedLease = (Lease)currentLeaseFinance;
                ReportViewerForm form = new ReportViewerForm();
                form.rptviewer.LocalReport.ReportPath = "LeasePaymentReport.rdlc";
                using (DealershipManagerEntities context = new DealershipManagerEntities())
                {
                    var lease = (from l in context.Leases
                                 where l.Id == selectedLease.Id
                                 select l).FirstOrDefault();
                    form.SetLeaseDataSource(lease);
                }
                form.rptviewer.Show();
                form.Show();
                form.rptviewer.RefreshReport();
                form.rptviewer.LocalReport.Refresh();
            }
            else if (currentLeaseFinance.GetType() == typeof(Sale))
            {
                //Sale
                Sale selectedSale = (Sale)currentLeaseFinance;
                ReportViewerForm form = new ReportViewerForm();
                form.rptviewer.LocalReport.ReportPath = "FinanceReport.rdlc";
                using (DealershipManagerEntities context = new DealershipManagerEntities())
                {
                    var sale = (from s in context.Sales
                                where s.id == selectedSale.id
                                select s).FirstOrDefault();
                    form.SetFinanceDataSource(sale);


                }
                form.rptviewer.Show();
                form.Show();
                form.rptviewer.RefreshReport();
                form.rptviewer.LocalReport.Refresh();
            }
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            List<DataGridViewColumn> toremove = new List<DataGridViewColumn>();
            foreach (DataGridViewColumn GridCol in dgvPaymentInfo.Columns)
            {
                if (!GridCol.Visible)
                {
                    toremove.Add(GridCol);
                }
            }
            foreach (DataGridViewColumn GridCol in toremove)
            {
                dgvPaymentInfo.Columns.Remove(GridCol);
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

                foreach (DataGridViewColumn dgvGridCol in dgvPaymentInfo.Columns)
                {
                    iTotalWidth += dgvGridCol.Width;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    foreach (DataGridViewColumn GridCol in dgvPaymentInfo.Columns)
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
                while (iRow <= dgvPaymentInfo.Rows.Count - 1)
                {
                    DataGridViewRow GridRow = dgvPaymentInfo.Rows[iRow];
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
                            e.Graphics.DrawString(lblSalesHistoryCustomerName.Text + " Payment Information " + txtPaymentInfoMake.Text + " " + txtPaymentInfoModel.Text,
                                new Font(dgvPaymentInfo.Font, FontStyle.Bold),
                                Brushes.Black, e.MarginBounds.Left,
                                e.MarginBounds.Top - e.Graphics.MeasureString(lblSalesHistoryCustomerName.Text + " Payment Information " + txtPaymentInfoMake.Text + " " + txtPaymentInfoModel.Text,
                                new Font(dgvPaymentInfo.Font, FontStyle.Bold),
                                e.MarginBounds.Width).Height - 13);

                            String strDate = DateTime.Now.ToLongDateString() + " " +
                                DateTime.Now.ToShortTimeString();
                            //Draw Date
                            e.Graphics.DrawString(strDate,
                                new Font(dgvPaymentInfo.Font, FontStyle.Bold), Brushes.Black,
                                e.MarginBounds.Left +
                                (e.MarginBounds.Width - e.Graphics.MeasureString(strDate,
                                new Font(dgvPaymentInfo.Font, FontStyle.Bold),
                                e.MarginBounds.Width).Width),
                                e.MarginBounds.Top - e.Graphics.MeasureString(lblSalesHistoryCustomerName.Text + " Payment Information " + txtPaymentInfoMake.Text + " " + txtPaymentInfoModel.Text,
                                new Font(new Font(dgvPaymentInfo.Font, FontStyle.Bold),
                                FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            //Draw Columns                 
                            iTopMargin = e.MarginBounds.Top;
                            foreach (DataGridViewColumn GridCol in dgvPaymentInfo.Columns)
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

        private void btnNextInsurance_Click(object sender, EventArgs e)
        {
            lbxInsuredVehicles.Items.Clear();
            int index = allInsurances.IndexOf(currentInsurance);
            if (index != allInsurances.Count - 1)
            {
                currentInsurance = allInsurances[index + 1];
                BindOtherInsuranceInfo();

                lblInsuranceCount.Text = (index + 2).ToString() + " of " + allInsurances.Count.ToString();
            }
            else if (index == allInsurances.Count - 1)
            {
                currentInsurance = new Insurance();
                BindOtherInsuranceInfo();
                lblInsuranceCount.Text = "New Insurance";
            }
        }

        private void btnPreviousInsurance_Click(object sender, EventArgs e)
        {
            //lbxInsuredVehicles.Items.Clear();
            try
            {
                int index = allInsurances.IndexOf(currentInsurance);
                if (index > 0)
                {
                    currentInsurance = allInsurances[index - 1];
                    BindOtherInsuranceInfo();
                    BindInsuranceBrokerToForm();
                    BindInsuranceCompanyToForm();

                    lblInsuranceCount.Text = (index).ToString() + " of " + allInsurances.Count.ToString();
                }
                else if (index == 0)
                {
                    currentInsurance = new Insurance();
                    BindOtherInsuranceInfo();
                    lblInsuranceCount.Text = "New Insurance";
                }
                else if (index == -1)
                {
                    currentInsurance = allInsurances[allInsurances.Count - 1];
                    BindOtherInsuranceInfo();
                    BindInsuranceBrokerToForm();
                    BindInsuranceCompanyToForm();

                    lblInsuranceCount.Text = allInsurances.Count.ToString() + " of " + allInsurances.Count.ToString();
                }

            }
            catch { }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int index = allLeasesFinances.IndexOf(currentLeaseFinance);
            if (index != allLeasesFinances.Count - 1)
            {
                currentLeaseFinance = allLeasesFinances[index + 1];
                BindFinanceInfo();

                lblPaymentInfo.Text = (index + 2).ToString() + " of " + allLeasesFinances.Count.ToString();
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            int index = allLeasesFinances.IndexOf(currentLeaseFinance);
            if (index > 0)
            {
                currentLeaseFinance = allLeasesFinances[index - 1];
                BindFinanceInfo();

                lblPaymentInfo.Text = (index).ToString() + " of " + allLeasesFinances.Count.ToString();
            }
        }

        private void btnViewLien2_Click(object sender, EventArgs e)
        {
            btnViewLien_Click(sender, e);
        }

        private void btnViewLien_Click(object sender, EventArgs e)
        {
            //TODO whatever this is
        }

        private void btnFinanceNewPayment_Click(object sender, EventArgs e)
        {
            clearFinancePaymentSelection();

            dtpPaymentDate.Value = DateTime.Today;
            txtPayment.Text = "";
            txtAddin.Text = "";
            txtPaymentDescription.Text = "";
        }

        private void btnNewPayment_Click(object sender, EventArgs e)
        {
            dtpPaymentInfoDate.Value = DateTime.Today;
        }

        private void clearFinancePaymentSelection()
        {
            currentLeasePayment = null;
            currentFinancePayment = null;
            dgvPaymentInfo.ClearSelection();
            dgvPaymentInfo.CurrentCell = null;
            foreach (DataGridViewRow item in dgvPaymentInfo.Rows)
            {
                item.Selected = false;
            }
        }

        private void btnSavePayment_Click(object sender, EventArgs e)
        {
            if (null != dgvPaymentInfo.SelectedRows)
            {
                foreach (DataGridViewRow dr in dgvPaymentInfo.SelectedRows)
                {
                    string value = dr.Cells["Id"].Value.ToString();
                    int iValue;
                    int.TryParse(value, out iValue);
                    using (DealershipManagerEntities context = new DealershipManagerEntities())
                    {
                        LeasePayment leaseQuery = (from leasePayment in context.LeasePayments
                                                   where leasePayment.Id == iValue
                                                   select leasePayment).FirstOrDefault();

                        leaseQuery.DatePaid = dtpPaymentInfoDate.Value;
                        context.SaveChanges();
                    }
                }
            }
            else
            {
                //using (DealershipManagerEntities context = new DealershipManagerEntities())
                //{
                //Might use this? 

                //    context.SaveChanges();
                //}
            }
            BindFinanceInfo();
            clearFinancePaymentSelection();
        }

        private void btnPayOutLease_Click(object sender, EventArgs e)
        {
            try
            {
                Lease currentLease = (Lease)currentLeaseFinance;
                using (DealershipManagerEntities context = new DealershipManagerEntities())
                {
                    Lease leaseQuery = (from leasePayment in context.Leases
                                        where leasePayment.Id == currentLease.Id
                                        select leasePayment).FirstOrDefault();

                    leaseQuery.Current = false;
                    context.SaveChanges();
                }
                MessageBox.Show("Lease Paid Out successfully");
            }
            catch
            {
                MessageBox.Show("Error paying out Lease");
            }
            this.CustomerForm_Load(this, null);
        }

        private void btnFinanceSavePayment_Click(object sender, EventArgs e)
        {

            try
            {
                DataGridViewRow lastRow = dgvPaymentInfo.Rows[dgvPaymentInfo.Rows.Count - 1];
                double balance = 0;
                double.TryParse(lastRow.Cells["Balance"].Value.ToString(), out balance);

                if (currentLeaseFinance.GetType() == typeof(Sale))
                {
                    Sale currentSale = (Sale)currentLeaseFinance;
                    if (dgvPaymentInfo.SelectedRows.Count > 0)
                    {
                        DataGridViewRow row = dgvPaymentInfo.SelectedRows[0];
                        DateTime date = Convert.ToDateTime(row.Cells["DatePaid"].Value.ToString());
                        String description = row.Cells["Description"].Value.ToString();

                        using (DealershipManagerEntities context = new DealershipManagerEntities())
                        {
                            Finance payment = (from f in context.Finances
                                               where f.saleId == currentSale.id && f.Description == description && f.DatePaid == date
                                               select f).FirstOrDefault();

                            currentFinancePayment = payment;
                        }

                    }
                    if (currentFinancePayment == null)
                    {
                        using (DealershipManagerEntities context = new DealershipManagerEntities())
                        {
                            Finance newFinance = new Finance();
                            Sale sale = (from s in context.Sales
                                         where s.id == currentSale.id
                                         select s).FirstOrDefault();

                            double addin = 0;
                            double.TryParse(txtAddin.Text, out addin);

                            double payment = 0;
                            double.TryParse(txtPayment.Text, out payment);

                            newFinance.saleId = currentSale.id;
                            newFinance.DatePaid = dtpPaymentDate.Value;
                            newFinance.Addin = addin;
                            newFinance.Payment = payment;
                            newFinance.Description = txtPaymentDescription.Text;
                            double ishort = 0;
                            if (payment < sale.PaymentDue)
                            {
                                ishort = sale.PaymentDue??payment - payment;
                            }
                            newFinance.Short = ishort;
                            balance = balance + addin - payment;
                            newFinance.Balance = balance;
                            newFinance.Open = 1;
                            if (!chkFinanceCurrent.Checked)
                            {
                                sale.current = false;
                            }
                            //if (balance < 0)
                            //{
                            //    newFinance.Open = 0;
                            //    sale.current = false;
                            //}

                            context.Finances.AddObject(newFinance);
                            context.SaveChanges();
                        }
                    }
                    else
                    {
                        using (DealershipManagerEntities context = new DealershipManagerEntities())
                        {
                            Finance financeQuery = (from f in context.Finances
                                                    where f.id == currentFinancePayment.id
                                                    select f).FirstOrDefault();
                            if (financeQuery.Description != "Total Balance")
                            {
                                Sale sale = (from s in context.Sales
                                             where s.id == currentSale.id
                                             select s).FirstOrDefault();
                                double addin = 0;
                                double.TryParse(txtAddin.Text, out addin);

                                double payment = 0;
                                double.TryParse(txtPayment.Text, out payment);

                                financeQuery.saleId = currentSale.id;
                                financeQuery.DatePaid = dtpPaymentDate.Value;
                                financeQuery.Addin = addin;
                                financeQuery.Payment = payment;
                                financeQuery.Description = txtPaymentDescription.Text;
                                double ishort = 0;
                                if (payment < sale.PaymentDue)
                                {
                                    ishort = (sale.PaymentDue ?? payment) - payment;
                                }
                                financeQuery.Short = ishort;
                                balance = balance + addin - payment;
                                financeQuery.Balance = balance;
                                financeQuery.Open = 1;
                                if (!chkFinanceCurrent.Checked)
                                {
                                    sale.current = false;
                                }
                                //if (balance < 0)
                                //{
                                //    financeQuery.Open = 0;
                                //    sale.current = false;
                                //}
                            }
                            context.SaveChanges();
                        }
                    }
                    RecalculateFinanceInfo((Sale)currentLeaseFinance);
                }
                else if (currentLeaseFinance.GetType() == typeof(Lease))
                {

                    Lease currentLease = (Lease)currentLeaseFinance;
                    if (currentLeasePayment != null)
                    {
                        using (DealershipManagerEntities context = new DealershipManagerEntities())
                        {
                            LeasePayment newLeasePayment = new LeasePayment();

                            Lease curLease = (from l in context.Leases
                                              where l.Id == currentLease.Id
                                              select l).FirstOrDefault();

                            double payment = 0;
                            double.TryParse(txtPayment.Text, out payment);

                            newLeasePayment.LeaseId = currentLease.Id;
                            newLeasePayment.DatePaid = dtpPaymentDate.Value;
                            newLeasePayment.PaymentAmount = payment;
                            balance = balance - payment;
                            newLeasePayment.Balance = balance;
                            if (!chkFinanceCurrent.Checked)
                            {
                                curLease.Current = false;
                            }
                            //if (balance < 0)
                            //{
                            //    curLease.Current = false;
                            //}
                            context.LeasePayments.AddObject(newLeasePayment);
                            context.SaveChanges();
                        }
                    }
                    else
                    {
                        using (DealershipManagerEntities context = new DealershipManagerEntities())
                        {
                            LeasePayment leasePaymentQuery = (from l in context.LeasePayments
                                                              where l.Id == currentLeasePayment.Id
                                                              select l).FirstOrDefault();

                            Lease curLease = (from l in context.Leases
                                              where l.Id == leasePaymentQuery.LeaseId
                                              select l).FirstOrDefault();

                            double addin = 0;
                            double.TryParse(txtAddin.Text, out addin);

                            double payment = 0;
                            double.TryParse(txtPayment.Text, out payment);

                            leasePaymentQuery.LeaseId = currentLease.Id;
                            leasePaymentQuery.DatePaid = dtpPaymentDate.Value;
                            leasePaymentQuery.PaymentAmount = payment;
                            balance = balance + addin - payment;
                            leasePaymentQuery.Balance = balance;
                            if (!chkFinanceCurrent.Checked)
                            {
                                curLease.Current = false;
                            }
                            //if (balance < 0)
                            //{
                            //    curLease.Current = false;
                            //}
                            context.SaveChanges();
                        }
                    }

                    RecalculateLeaseInfo((Lease)currentLeaseFinance);
                }
            }
            catch (Exception ex)
            {
                try
                {
                    if (!chkFinanceCurrent.Checked)
                    {
                        using (DealershipManagerEntities context = new DealershipManagerEntities())
                        {
                            if (currentLeaseFinance.GetType() == typeof(Lease))
                            {
                                int id = (currentLeaseFinance as Lease).Id;
                                Lease curLease = (from l in context.Leases
                                                  where l.Id == id
                                                  select l).FirstOrDefault();
                                curLease.Current = false;
                            }
                            else if (currentLeaseFinance.GetType() == typeof(Sale))
                            {
                                int id = (currentLeaseFinance as Sale).id;
                                Sale curSale = (from s in context.Sales
                                                where s.id == id
                                                select s).FirstOrDefault();
                                curSale.current = false;                                
                            }
                            context.SaveChanges();
                        }
                    }
                }
                catch (Exception ex2)
                {
                    string test = ex2.Message;
                }
            }


            EmptyVariables();
            BindPaymentInfo();
            clearFinancePaymentSelection();
        }

        private void btnSaveInsurance_Click(object sender, EventArgs e)
        {
            if (currentInsurance == null || currentInsurance.id == 0)
            {
                //New
                currentInsurance = new Insurance();
                try
                {
                    currentInsurance.Brokerid = (int)cboInsuranceBroker.SelectedValue;
                }
                catch
                {
                    MessageBox.Show("Please select a Broker");
                }

                int coverage = 0;
                int.TryParse(txtInsuranceCoverage.Text, out coverage);
                currentInsurance.Coverage = coverage;
                currentInsurance.CustomerId = currentCustomer.Id;
                currentInsurance.ExpiryDate = dtpExpiryDate.Value;

                try
                {
                    currentInsurance.InsuranceCompanyId = (int)cboInsuranceCompany.SelectedValue;
                }
                catch
                {
                    MessageBox.Show("Please select an Insurance Company");
                }
                currentInsurance.Notes = txtInsuranceNotes.Text;
                currentInsurance.PolicyNumber = txtInsurancePolicyNumber.Text;
                using (DealershipManagerEntities context = new DealershipManagerEntities())
                {
                    context.Insurances.AddObject(currentInsurance);
                    context.SaveChanges();


                    foreach (InsuranceVehicle insv in allInsuranceVehicles)
                    {
                        insv.insuranceid = currentInsurance.id;
                        context.InsuranceVehicles.AddObject(insv);
                    }
                    context.SaveChanges();
                }
            }
            else
            {
                using (DealershipManagerEntities context = new DealershipManagerEntities())
                {
                    Insurance insuranceQuery = (from i in context.Insurances
                                                where i.id == currentInsurance.id
                                                select i).FirstOrDefault();

                    insuranceQuery.Brokerid = (int)cboInsuranceBroker.SelectedValue;
                    int coverage = 0;
                    int.TryParse(txtInsuranceCoverage.Text, out coverage);
                    insuranceQuery.Coverage = coverage;
                    insuranceQuery.CustomerId = currentCustomer.Id;
                    insuranceQuery.ExpiryDate = dtpExpiryDate.Value;
                    insuranceQuery.InsuranceCompanyId = (int)cboInsuranceCompany.SelectedValue;
                    insuranceQuery.Notes = txtInsuranceNotes.Text;
                    insuranceQuery.PolicyNumber = txtInsurancePolicyNumber.Text;
                    context.SaveChanges();

                    IQueryable<InsuranceVehicle> insuranceVehicleQuery = from insuranceVehicle in context.InsuranceVehicles
                                                                         where insuranceVehicle.insuranceid == currentInsurance.id
                                                                         select insuranceVehicle;
                    foreach (InsuranceVehicle insurv in insuranceVehicleQuery)
                    {
                        try
                        {
                            if (!allInsuranceVehicles.Exists(x => x.id == insurv.id)) //It's not in the list, remove it from DB.
                                context.InsuranceVehicles.DeleteObject(insurv);
                        }
                        catch (Exception ex)
                        {
                            string test = ex.Message;
                        }

                    }
                    context.SaveChanges();
                    foreach (InsuranceVehicle insv in allInsuranceVehicles)
                    {
                        var dta = context.InsuranceVehicles.Where(x => x.id == insv.id);
                        if (dta.Count() == 0) //It's on the list but not in the DB.
                            context.InsuranceVehicles.AddObject(insv);
                    }
                    context.SaveChanges();
                }
            }
            MessageBox.Show("Insurance Information has been saved");
            BindInsuranceStuff();
        }

        private void btnNewInsuranceCompany_Click(object sender, EventArgs e)
        {
            InsuranceCompanyForm icf = new InsuranceCompanyForm();
            icf.customerForm = this;
            icf.Show();
        }

        private void btnEditInsuranceCompany_Click(object sender, EventArgs e)
        {
            InsuranceCompanyForm icf = new InsuranceCompanyForm((int)cboInsuranceCompany.SelectedValue);
            icf.customerForm = this;
            icf.Show();
        }

        private void btnNewBroker_Click(object sender, EventArgs e)
        {
            InsuranceBrokerForm ibf = new InsuranceBrokerForm();
            ibf.customerForm = this;
            ibf.Show();
        }

        private void btnEditBroker_Click(object sender, EventArgs e)
        {
            InsuranceBrokerForm ibf = new InsuranceBrokerForm((int)cboInsuranceBroker.SelectedValue);
            ibf.customerForm = this;
            ibf.Show();
        }

        private void btnDeleteInsurance_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this Insurance??", "ALERT", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (DealershipManagerEntities context = new DealershipManagerEntities())
                {
                    var insurance = (from i in context.Insurances
                                     where i.id == currentInsurance.id
                                     select i).First();

                    context.Insurances.DeleteObject(insurance);
                    context.SaveChanges();
                }

            };
            currentInsurance = null;
            BindInsuranceStuff();
        }

        private void btnPrintLeaseFinanceReport_Click(object sender, EventArgs e)
        {
            if (rdbSalesHistoryLease.Checked)
            {
                //Lease
                Lease selectedLease = (Lease)lbxLeaseHistory.SelectedItem;
                ReportViewerForm form = new ReportViewerForm();
                form.rptviewer.LocalReport.ReportPath = "LeasePaymentReport.rdlc";
                using (DealershipManagerEntities context = new DealershipManagerEntities())
                {
                    var lease = (from l in context.Leases
                                 where l.Id == selectedLease.Id
                                 select l).FirstOrDefault();
                    form.SetLeaseDataSource(lease);
                }
                form.rptviewer.Show();
                form.Show();
                form.rptviewer.RefreshReport();
                form.rptviewer.LocalReport.Refresh();
            }
            else if (rdbSalesHistorySale.Checked)
            {
                //Sale
                Sale selectedSale = (Sale)lbxSalesHistory.SelectedItem;
                ReportViewerForm form = new ReportViewerForm();
                form.rptviewer.LocalReport.ReportPath = "FinanceReport.rdlc";
                using (DealershipManagerEntities context = new DealershipManagerEntities())
                {
                    var sale = (from s in context.Sales
                                where s.id == selectedSale.id
                                select s).FirstOrDefault();
                    form.SetFinanceDataSource(sale);


                }
                form.rptviewer.Show();
                form.Show();
                form.rptviewer.RefreshReport();
                form.rptviewer.LocalReport.Refresh();
            }

        }

        private void btnViewSaleInfo_Click_1(object sender, EventArgs e)
        {
            if (currentLeasePayments.Count > 0)
            {
                using (DealershipManagerEntities context = new DealershipManagerEntities())
                {
                    Lease lease = (Lease)currentLeaseFinance;

                    DealForm dealform = new DealForm(lease);
                    dealform.MdiParent = this.MdiParent;
                    dealform.Show();

                }
            }
            else if (currentFinancePayments.Count > 0)
            {
                using (DealershipManagerEntities context = new DealershipManagerEntities())
                {
                    Sale sale = (Sale)currentLeaseFinance;
                    DealForm dealform = new DealForm(sale);
                    dealform.MdiParent = this.MdiParent;
                    dealform.Show();

                }
            }
        }

        private void dgvPaymentInfo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (currentLeaseFinance.GetType() == typeof(Lease))
            {
                DataGridView gridview = (DataGridView)sender;
                List<LeasePayment> list = (List<LeasePayment>)gridview.DataSource;

                currentLeasePayment = list[e.RowIndex];

                txtPayment.Text = currentLeasePayment.PaymentAmount.Value.ToString();
                dtpPaymentDate.Value = currentLeasePayment.DatePaid ?? DateTime.Today;
            }
        }

        private void dgvPaymentInfo_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (currentLeaseFinance != null)
            {
                if (currentLeaseFinance.GetType() == typeof(Lease))
                {
                    DataGridView gridview = (DataGridView)sender;
                    List<LeasePayment> list = (List<LeasePayment>)gridview.DataSource;

                    currentLeasePayment = list[e.RowIndex];
                    using (DealershipManagerEntities context = new DealershipManagerEntities())
                    {
                        LeasePayment leasePayment = (from l in context.LeasePayments
                                                     where l.Id == currentLeasePayment.Id
                                                     select l).FirstOrDefault();

                        leasePayment.Balance = currentLeasePayment.Balance;
                        leasePayment.DatePaid = currentLeasePayment.DatePaid;
                        leasePayment.GST = currentLeasePayment.GST;
                        leasePayment.Interest = currentLeasePayment.Interest;
                        leasePayment.Principle = currentLeasePayment.Principle;
                        leasePayment.PaymentAmount = currentLeasePayment.PaymentAmount;
                        context.SaveChanges();
                        // other changed properties
                    }

                    RecalculateLeaseInfo((Lease)currentLeaseFinance);
                }
                else if (currentLeaseFinance.GetType() == typeof(Sale))
                {
                    DataGridView gridview = (DataGridView)sender;
                    List<Finance> list = (List<Finance>)gridview.DataSource;

                    currentFinancePayment = list[e.RowIndex];
                    using (DealershipManagerEntities context = new DealershipManagerEntities())
                    {
                        Finance financePayment = (from f in context.Finances
                                                  where f.id == currentFinancePayment.id
                                                  select f).FirstOrDefault();

                        financePayment.Balance = currentFinancePayment.Balance;
                        financePayment.DatePaid = currentFinancePayment.DatePaid;
                        financePayment.Payment = currentFinancePayment.Payment;
                        financePayment.Addin = currentFinancePayment.Addin;
                        financePayment.Description = currentFinancePayment.Description;
                        financePayment.Open = currentFinancePayment.Open;
                        financePayment.Short = currentFinancePayment.Short;
                        context.SaveChanges();
                    }
                    RecalculateFinanceInfo((Sale)currentLeaseFinance);
                }

                BindFinanceInfo();
            }

        }

        private void RecalculateFinanceInfo(Sale sale)
        {
            try
            {
                using (DealershipManagerEntities context = new DealershipManagerEntities())
                {
                    double total = sale.TotalCostofVehicle ?? 0;
                    IQueryable<Finance> financeIQ = (from f in context.Finances
                                                     where f.saleId == sale.id
                                                     select f);

                    List<Finance> financePayments = financeIQ.OrderBy(x => x.DatePaid).ToList();

                    double previousBalance = total;
                    foreach (Finance payment in financePayments)
                    {
                        payment.Balance = previousBalance + payment.Addin - payment.Payment;
                        context.SaveChanges();
                        previousBalance = payment.Balance ?? 0;
                    }
                }
            }
            catch (Exception ex)
            {
                string test = ex.Message;
            }
        }

        private void RecalculateLeaseInfo(Lease lease)
        {
            try
            {
                using (DealershipManagerEntities context = new DealershipManagerEntities())
                {
                    double total = lease.InitialPrice ?? 0;
                    IQueryable<LeasePayment> leasePaymentsIQ = (from l in context.LeasePayments
                                                                where l.LeaseId == lease.Id
                                                                select l);

                    List<LeasePayment> leasePayments = leasePaymentsIQ.OrderBy(x => x.PmtNo).ToList();

                    double previousBalance = total;
                    foreach (LeasePayment payment in leasePayments)
                    {

                        payment.GST = payment.PaymentAmount * double.Parse(Properties.Settings.Default["GST"].ToString());
                        payment.Principle = payment.PaymentAmount - payment.Interest - payment.GST;
                        payment.Balance = previousBalance + payment.Interest + payment.GST - payment.PaymentAmount;

                        context.SaveChanges();
                        previousBalance = payment.Balance ?? 0;

                    }
                }
            }
            catch (Exception ex)
            {
                string test = ex.Message;
            }
        }

        private void dgvPaymentInfo_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                if (this.dgvPaymentInfo.Columns["PmtNo"] != null)
                    this.dgvPaymentInfo.Columns["PmtNo"].DefaultCellStyle.Format = "";

                if (this.dgvPaymentInfo.Columns["PaymentAmount"] != null)
                    this.dgvPaymentInfo.Columns["PaymentAmount"].DefaultCellStyle.Format = "c";

                if (this.dgvPaymentInfo.Columns["Principle"] != null)
                    this.dgvPaymentInfo.Columns["Principle"].DefaultCellStyle.Format = "c";

                if (this.dgvPaymentInfo.Columns["Interest"] != null)
                    this.dgvPaymentInfo.Columns["Interest"].DefaultCellStyle.Format = "c";

                if (this.dgvPaymentInfo.Columns["GST"] != null)
                    this.dgvPaymentInfo.Columns["GST"].DefaultCellStyle.Format = "c";

                if (this.dgvPaymentInfo.Columns["Balance"] != null)
                {
                    this.dgvPaymentInfo.Columns["Balance"].DefaultCellStyle.Format = "c";
                    this.dgvPaymentInfo.Columns["Balance"].ReadOnly = true;
                }

                if (this.dgvPaymentInfo.Columns["Payment"] != null)
                    this.dgvPaymentInfo.Columns["Payment"].DefaultCellStyle.Format = "c";

                if (this.dgvPaymentInfo.Columns["Description"] != null)
                    this.dgvPaymentInfo.Columns["Description"].DefaultCellStyle.Format = "";

                if (this.dgvPaymentInfo.Columns["Short"] != null)
                    this.dgvPaymentInfo.Columns["Short"].DefaultCellStyle.Format = "c";

                if (this.dgvPaymentInfo.Columns["Addin"] != null)
                    this.dgvPaymentInfo.Columns["Addin"].DefaultCellStyle.Format = "c";

                if (this.dgvPaymentInfo.Columns["Open"] != null)
                    this.dgvPaymentInfo.Columns["Open"].DefaultCellStyle.Format = "";

                if (this.dgvPaymentInfo.Columns["DatePaid"] != null)
                    dgvPaymentInfo.Columns["DatePaid"].DefaultCellStyle.Format = "d";
            }
            catch
            {
            }

            dgvPaymentInfo.ScrollBars = ScrollBars.Vertical;
            try
            {
                dgvPaymentInfo.FirstDisplayedScrollingRowIndex = dgvPaymentInfo.RowCount - 1;
            }
            catch
            {
            }
        }

        private void dgvPaymentInfo_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridView dgv = sender as DataGridView;
                if (dgv != null)
                {
                    if (currentLeaseFinance.GetType() == typeof(Lease))
                    {
                        dtpPaymentInfoDate.Value = Convert.ToDateTime(dgv["DatePaid", e.RowIndex].Value.ToString());

                    }
                    else if (currentLeaseFinance.GetType() == typeof(Sale))
                    {
                        dtpPaymentDate.Value = Convert.ToDateTime(dgv["DatePaid", e.RowIndex].Value.ToString());
                        txtPaymentDescription.Text = dgv["Description", e.RowIndex].Value.ToString();
                        txtAddin.Text = dgv["Addin", e.RowIndex].Value.ToString();
                        txtPayment.Text = dgv["Payment", e.RowIndex].Value.ToString();
                    }
                }
            }
            catch
            {
            }
        }

        private void dgvPaymentInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridView dgv = sender as DataGridView;
                if (dgv != null)
                {
                    if (currentLeaseFinance.GetType() == typeof(Lease))
                    {
                        if (dgv["DatePaid", e.RowIndex] != null & dgv["DatePaid", e.RowIndex].Value != null)
                        {

                            dtpPaymentInfoDate.Value = Convert.ToDateTime(dgv["DatePaid", e.RowIndex].Value.ToString());
                        }
                    }
                    else if (currentLeaseFinance.GetType() == typeof(Sale))
                    {
                        dtpPaymentDate.Value = Convert.ToDateTime(dgv["DatePaid", e.RowIndex].Value.ToString());
                        txtPaymentDescription.Text = dgv["Description", e.RowIndex].Value.ToString();
                        txtAddin.Text = dgv["Addin", e.RowIndex].Value.ToString();
                        txtPayment.Text = dgv["Payment", e.RowIndex].Value.ToString();
                    }
                }
            }
            catch
            {
            }
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            clearFinancePaymentSelection();
        }

        private void dgvPaymentInfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}
