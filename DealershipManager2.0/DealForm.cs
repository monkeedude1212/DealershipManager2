using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace DealershipManager2._0
{
    public partial class DealForm : Form
    {
        public Finance currentFinance;
        public Lease currentLease;
        public Sale currentSale;
        public Customer currentCustomer;

        private decimal salePrice;

        public DealForm()
        {
            InitializeComponent();

            DataSet emptyDataset = new DataSet();

        }

        public DealForm(Lease l)
        {
            InitializeComponent();
            tabControl1.SelectedTab = tabControl1.TabPages["Lease"];
            currentLease = l;

            txtLeaseSalePrice.Text = currentLease.InitialPrice.ToString();
            txtLeaseTradeInAllowance.Text = currentLease.TradeInAllowance.ToString();
            txtLeaseAdministrationFee.Text = currentLease.AdminFee.ToString();
            txtLeaseWarranty.Text = currentLease.Warranty.ToString();
            txtLeaseCashDownPayment.Text = currentLease.Down.ToString();
            txtLeaseGSTDownPayment.Text = currentLease.WarrantyGST.ToString();
            txtLeaseInterestRate.Text = currentLease.Rate.ToString();
            txtLeaseNumberofPayments.Text = currentLease.NoPayments.ToString();
            txtLeasePaymentAmount.Text = currentLease.PaymentAmount.ToString();

            txtLeaseSubtotal.Text = ((double)currentLease.InitialPrice - (double)currentLease.TradeInAllowance + (double)currentLease.AdminFee).ToString();

        }

        public DealForm(Sale s)
        {
            InitializeComponent();
            currentSale = s;
            using (DealershipManagerEntities context = new DealershipManagerEntities())
            {
                context.Connection.Open();
                IQueryable<Finance> financeQuery = from finance in context.Finances
                                                   where finance.saleId == s.id
                                                   select finance;

                if (financeQuery.Count() > 0)
                {
                    //Is a financing plan
                    tabControl1.SelectedTab = tabControl1.TabPages["Finance"];

                    txtFinanceSalePrice.Text = currentSale.Price.ToString();
                    txtFinanceTradeInAllowance.Text = currentSale.TradeAllowance.ToString();
                    txtFinanceAdministrationFee.Text = currentSale.AdminFee.ToString();

                    txtFinanceGST.Text = currentSale.GST.ToString();
                    txtFinanceWarranty.Text = currentSale.Warranty.ToString();
                    txtFinanceGSTOnWarranty.Text = currentSale.WarrantyGST.ToString();
                    txtFinanceDeposit.Text = currentSale.Deposit.ToString();
                    txtPayoutOnLien.Text = currentSale.PayoutonLien.ToString();
                    txtFinanceInterestAmount.Text = currentSale.Interest.ToString();
                    txtFinanceInterestRate.Text = currentSale.Rate.ToString();
                    txtFinanceTotalBalanceDue.Text = currentSale.TotalBalance.ToString();
                    txtFinanceNumberofPayments.Text = currentSale.noPayments.ToString();
                    txtFinancePaymentAmount.Text = currentSale.PaymentDue.ToString();
                    txtFinanceNotes.Text = currentSale.Notes;

                    txtFinanceSubtotal.Text = ((double)currentSale.Price - (double)currentSale.TradeAllowance + (double)currentSale.AdminFee).ToString();
                    txtFinanceTotalBalanceDue.Text = CalculateTotal("Finance").ToString("C");

                    cboFinances.SelectedValue = s.id;
                }
                else
                {
                    tabControl1.SelectedTab = tabControl1.TabPages["Sale"];

                    txtSalePrice.Text = currentSale.Price.ToString();
                    txtSaleTradeInAllowance.Text = currentSale.TradeAllowance.ToString();
                    txtSaleAdministrationFee.Text = currentSale.AdminFee.ToString();
                    txtSaleGST.Text = currentSale.GST.ToString();
                    txtSaleDeposit.Text = currentSale.Deposit.ToString();
                    txtSaleWarranty.Text = currentSale.Warranty.ToString();
                    txtSaleWarrantyGST.Text = currentSale.WarrantyGST.ToString();
                    txtSalePayoutOnLien.Text = currentSale.PayoutonLien.ToString();
                    txtSaleTotalBalance.Text = currentSale.TotalBalance.ToString();
                    txtSaleCustomerNotes.Text = currentSale.Notes.ToString();

                    txtSaleSubtotal.Text = ((double)currentSale.Price - (double)currentSale.TradeAllowance + (double)currentSale.AdminFee).ToString();
                    txtSaleTotalBalance.Text = CalculateTotal("Sale").ToString("C");

                    cboSales.SelectedValue = s.id;
                }
            }
        }

        private void DealForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DealershipManagerDataSetLeaseInfo.LeaseInfo' table. You can move, or remove it, as needed.
            this.leaseInfoTableAdapter.Fill(this.DealershipManagerDataSetLeaseInfo.LeaseInfo);

            // TODO: This line of code loads data into the 'dealerShipManagerDataSetSaleInfo.SaleInfo' table. You can move, or remove it, as needed.
            this.saleInfoTableAdapter.Fill(this.dealerShipManagerDataSetSaleInfo.SaleInfo);
            // TODO: This line of code loads data into the 'dealershipManagerDataSet.Sale' table. You can move, or remove it, as needed.
            this.saleTableAdapter.Fill(this.dealershipManagerDataSet.Sale);
            // TODO: This line of code loads data into the 'dealershipManagerDataSet.Vehicle' table. You can move, or remove it, as needed.


            this.vehicleTableAdapter.FillBy(this.dealershipManagerDataSet.Vehicle); //FillBy Does in stock

            cboSaleVehicle.ValueMember = "Id";
            cboSaleVehicle.DataSource = getCurrentVehicleDataSource();

            cboLeaseVehicle.ValueMember = "Id";
            cboLeaseVehicle.DataSource = getCurrentVehicleDataSource();

            cboFinanceVehicle.ValueMember = "Id";
            cboFinanceVehicle.DataSource = getCurrentVehicleDataSource();

            this.vehicleTableAdapter.Fill(this.dealershipManagerDataSet.Vehicle); //Fill does All vehicles...

            cboSaleTradeInVehicle.ValueMember = "Id";
            cboSaleTradeInVehicle.DataSource = getCurrentVehicleDataSource();

            cboLeaseTradeIn.ValueMember = "Id";
            cboLeaseTradeIn.DataSource = getCurrentVehicleDataSource();

            cboFinanceTradeInVehicle.ValueMember = "Id";
            cboFinanceTradeInVehicle.DataSource = getCurrentVehicleDataSource();


            // TODO: This line of code loads data into the 'dealershipManagerDataSet1.Staff' table. You can move, or remove it, as needed.
            this.staffTableAdapter.Fill(this.dealershipManagerDataSet.Staff);

            // TODO: This line of code loads data into the 'dealershipManagerDataSet.Customer' table. You can move, or remove it, as needed.
            this.customerTableAdapter.FillByLastName(this.dealershipManagerDataSet.Customer);

            cboSales.SelectedIndex = -1;
            cboSaleCustomer.SelectedIndex = -1;
            cboLeases.SelectedIndex = -1;
            cboLeaseCustomer.SelectedIndex = -1;
            cboFinances.SelectedIndex = -1;
            cboFinanceCustomer.SelectedIndex = -1;

            cboSaleVehicle.SelectedIndex = -1;
            cboLeaseVehicle.SelectedIndex = -1;
            cboFinanceVehicle.SelectedIndex = -1;
            cboSaleTradeInVehicle.SelectedIndex = -1;
            cboLeaseTradeIn.SelectedIndex = -1;
            cboFinanceTradeInVehicle.SelectedIndex = -1;

            this.Size = new Size(Parent.Size.Width * 2, Parent.Size.Height * 2);

            if (currentLease != null)
            {
                this.vehicleTableAdapter.Fill(this.dealershipManagerDataSet.Vehicle); //Fill does All vehicles...
                cboLeaseVehicle.ValueMember = "Id";
                cboLeaseVehicle.DataSource = getAllVehicleDataSource();

                cboLeaseCustomer.SelectedValue = currentLease.CustomerId;
                cboLeaseSalesPerson.SelectedValue = currentLease.SalesPerson;
                cboLeaseVehicle.SelectedValue = currentLease.VehicleId;
                cboLeaseTradeIn.SelectedValue = currentLease.TradeInId ?? -1;
                cboLeases.SelectedValue = currentLease.Id;
                //Set the selected vehicle and customer info...?
            }
            if (currentSale != null)
            {
                this.vehicleTableAdapter.Fill(this.dealershipManagerDataSet.Vehicle); //Fill does All vehicles...
                cboSaleVehicle.ValueMember = "Id";
                cboFinanceVehicle.ValueMember = "Id";
                cboSaleVehicle.DataSource = getAllVehicleDataSource();
                cboFinanceVehicle.DataSource = getAllVehicleDataSource();

                cboSaleCustomer.SelectedValue = currentSale.CustomerId;
                cboFinanceCustomer.SelectedValue = currentSale.CustomerId;
                cboSaleSalesPerson.SelectedValue = currentSale.SalesPerson;
                cboFinanceSalesPerson.SelectedValue = currentSale.SalesPerson;
                cboSaleVehicle.SelectedValue = currentSale.VehicleId;
                cboFinanceVehicle.SelectedValue = currentSale.VehicleId;
                cboSaleTradeInVehicle.SelectedValue = currentSale.TradeInVehicle;
                cboFinanceTradeInVehicle.SelectedValue = currentSale.TradeInVehicle;
                cboSales.SelectedValue = currentSale.id;
                cboFinances.SelectedValue = currentSale.id;


                //Set the selected vehicle and customer info...?
            }
        }

        private List<Vehicle> getCurrentVehicleDataSource()
        {
            List<Vehicle> list = new List<Vehicle>();
            int type = 0;
            Vehicle cars = new Vehicle();
            Vehicle trucks = new Vehicle();
            Vehicle suvs = new Vehicle();
            Vehicle vans = new Vehicle();
            cars.Model = "------------CARS------------";
            trucks.Model = "-----------TRUCKS-----------";
            suvs.Model = "------------SUVS------------";
            vans.Model = "------------VANS------------";
            list.Add(cars);
            using (DealershipManagerEntities context = new DealershipManagerEntities())
            {
                context.Connection.Open();
                IQueryable<Vehicle> vehicleQuery = from vehicle in context.Vehicles
                                                   where vehicle.InStock == 1
                                                   select vehicle;

                vehicleQuery = vehicleQuery.OrderBy(x => x.Type).ThenByDescending(y => y.Year).ThenBy(z => z.Make).ThenBy(m => m.Model);
                try
                {
                    foreach (var vehicle in vehicleQuery)
                    {

                        if (vehicle.Type != type)
                        {
                            if (vehicle.Type == 1)
                            {
                                type = 1;
                                list.Add(trucks);
                            }
                            else if (vehicle.Type == 2)
                            {
                                type = 2;
                                list.Add(suvs);
                            }
                            else if (vehicle.Type == 3)
                            {
                                type = 3;
                                list.Add(vans);
                            }
                            list.Add(vehicle);
                        }
                        else
                        {
                            list.Add(vehicle);
                        }
                    }
                }
                catch (Exception ex)
                {
                }
                context.Connection.Close();
            }

            return list;
        }

        private List<Vehicle> getAllVehicleDataSource()
        {
            List<Vehicle> list = new List<Vehicle>();
            int type = 0;
            Vehicle cars = new Vehicle();
            Vehicle trucks = new Vehicle();
            Vehicle suvs = new Vehicle();
            Vehicle vans = new Vehicle();
            cars.Model = "------------CARS------------";
            trucks.Model = "-----------TRUCKS-----------";
            suvs.Model = "------------SUVS------------";
            vans.Model = "------------VANS------------";
            list.Add(cars);
            using (DealershipManagerEntities context = new DealershipManagerEntities())
            {
                context.Connection.Open();
                IQueryable<Vehicle> vehicleQuery = from vehicle in context.Vehicles
                                                   select vehicle;

                vehicleQuery = vehicleQuery.OrderBy(x => x.Type).ThenByDescending(y => y.Year).ThenBy(z => z.Make).ThenBy(m => m.Model);
                try
                {
                    foreach (var vehicle in vehicleQuery)
                    {

                        if (vehicle.Type != type)
                        {
                            if (vehicle.Type == 1)
                            {
                                type = 1;
                                list.Add(trucks);
                            }
                            else if (vehicle.Type == 2)
                            {
                                type = 2;
                                list.Add(suvs);
                            }
                            else if (vehicle.Type == 3)
                            {
                                type = 3;
                                list.Add(vans);
                            }
                            list.Add(vehicle);
                        }
                        else
                        {
                            list.Add(vehicle);
                        }
                    }
                }
                catch (Exception ex)
                {
                }
                context.Connection.Close();
            }

            return list;
        }

        private void cboSaleCustomer_Format(object sender, ListControlConvertEventArgs e)
        {
            DataRow r = ((DataRowView)e.ListItem).Row;
            e.Value = r["LastName"] + ", " + r["FirstName"];
        }

        private void cboSaleSalesPerson_Format(object sender, ListControlConvertEventArgs e)
        {
            DataRow r = ((DataRowView)e.ListItem).Row;
            e.Value = r["LastName"] + ", " + r["FirstName"];
        }

        private void cboSaleVehicle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (null != cboSaleVehicle.SelectedValue)
            {
                int id = (int)cboSaleVehicle.SelectedValue;
                cboLeaseVehicle.SelectedValue = cboSaleVehicle.SelectedValue;
                cboFinanceVehicle.SelectedValue = cboSaleVehicle.SelectedValue;
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
                    BindSaleVehicleInfo(veh);
                }
            }
        }

        private void cboSaleTradeInVehicle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (null != cboSaleTradeInVehicle.SelectedValue)
            {
                int id = (int)cboSaleTradeInVehicle.SelectedValue;
                cboLeaseTradeIn.SelectedValue = cboSaleTradeInVehicle.SelectedValue;
                cboFinanceTradeInVehicle.SelectedValue = cboSaleTradeInVehicle.SelectedValue;
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
                    BindTradeInVehicleInfo(veh);
                }
            }
        }

        private void BindSaleVehicleInfo(Vehicle v)
        {
            txtSaleMake.Text = v.Make;
            txtSaleModel.Text = v.Model;
            txtSaleYear.Text = v.Year.ToString();
            txtSaleVIN.Text = v.VIN;

            txtLeaseMake.Text = v.Make;
            txtLeaseModel.Text = v.Model;
            txtLeaseYear.Text = v.Year.ToString();
            txtLeaseVIN.Text = v.VIN;

            txtFinanceMake.Text = v.Make;
            txtFinanceModel.Text = v.Model;
            txtFinanceYear.Text = v.Year.ToString();
            txtFinanceVIN.Text = v.VIN;
        }

        private void BindTradeInVehicleInfo(Vehicle v)
        {
            txtSaleTradeInMake.Text = v.Make;
            txtSaleTradeInModel.Text = v.Model;
            txtSaleTradeInYear.Text = v.Year.ToString();
            txtSaleTradeInVIN.Text = v.VIN;

            txtLeaseTradeInMake.Text = v.Make;
            txtLeaseTradeInModel.Text = v.Model;
            txtLeaseTradeInYear.Text = v.Year.ToString();
            txtLeaseTradeInVIN.Text = v.VIN;

            txtFinanceTradeInMake.Text = v.Make;
            txtFinanceTradeInModel.Text = v.Model;
            txtFinanceTradeInYear.Text = v.Year.ToString();
            txtFinanceTradeInVIN.Text = v.VIN;
        }

        private void btnSaleAddNewTradeIn_Click(object sender, EventArgs e)
        {
            VehicleForm vehicleForm = new VehicleForm();
            vehicleForm.MdiParent = this.MdiParent;
            vehicleForm.Show();
            vehicleForm.btnNewVehicle_Click(this, null);            
        }

        private void btnSaleNewCustomer_Click(object sender, EventArgs e)
        {
            CustomerForm customerForm = new CustomerForm();
            customerForm.MdiParent = this.MdiParent;
            customerForm.Show();
            customerForm.btnNewCustomer_Click(this, null);            
        }

        private void btnSaleEditCustomer_Click(object sender, EventArgs e)
        {

            int id = (int)cboSaleCustomer.SelectedValue;
            using (DealershipManagerEntities context = new DealershipManagerEntities())
            {
                IQueryable<Customer> customerQuery = from customer in context.Customers
                                                     where customer.Id == id
                                                     select customer;

                Customer c = customerQuery.FirstOrDefault();
                if (null == c)
                {
                    c = new Customer();
                }
                CustomerForm customerForm = new CustomerForm(c);
                customerForm.dealHolder = this;
                customerForm.MdiParent = this.MdiParent;
                customerForm.Show();
                this.Hide();
            }

        }

        private void btnLeaseNewCustomer_Click(object sender, EventArgs e)
        {
            CustomerForm customerForm = new CustomerForm();
            customerForm.MdiParent = this.MdiParent;
            customerForm.Show();
            customerForm.btnNewCustomer_Click(this, null);
            
        }

        private void btnLeaseEditCustomer_Click(object sender, EventArgs e)
        {
            int id = (int)cboLeaseCustomer.SelectedValue;
            using (DealershipManagerEntities context = new DealershipManagerEntities())
            {
                IQueryable<Customer> customerQuery = from customer in context.Customers
                                                     where customer.Id == id
                                                     select customer;

                Customer c = customerQuery.FirstOrDefault();
                if (null == c)
                {
                    c = new Customer();
                }
                CustomerForm customerForm = new CustomerForm(c);
                customerForm.dealHolder = this;
                customerForm.MdiParent = this.MdiParent;
                customerForm.Show();
                this.Hide();
            }
        }

        private void btnFinanceNewCustomer_Click(object sender, EventArgs e)
        {
            CustomerForm customerForm = new CustomerForm();
            customerForm.MdiParent = this.MdiParent;
            customerForm.Show();
            customerForm.btnNewCustomer_Click(this, null);
            
        }

        private void btnFinanceEditCustomer_Click(object sender, EventArgs e)
        {
            int id = (int)cboFinanceCustomer.SelectedValue;
            using (DealershipManagerEntities context = new DealershipManagerEntities())
            {
                IQueryable<Customer> customerQuery = from customer in context.Customers
                                                     where customer.Id == id
                                                     select customer;

                Customer c = customerQuery.FirstOrDefault();
                if (null == c)
                {
                    c = new Customer();
                }
                CustomerForm customerForm = new CustomerForm(c);
                customerForm.dealHolder = this;
                customerForm.MdiParent = this.MdiParent;
                customerForm.Show();
                this.Hide();
            }
        }

        private void BindFinanceCustomerInfo()
        {
            DataRow r = ((DataRowView)cboFinances.SelectedItem).Row;

            int customerId = (int)r["CustomerId"];
            int salespersonId = (int)r["SalesPerson"];
            using (DealershipManagerEntities context = new DealershipManagerEntities())
            {
                IQueryable<Customer> customerQuery = from customer in context.Customers
                                                     where customer.Id == customerId
                                                     select customer;

                Customer c = customerQuery.FirstOrDefault();
                if (null == c)
                {
                    c = new Customer();
                }
                foreach (DataRowView drv in cboFinanceCustomer.Items)
                {
                    if (c.Id.ToString() == drv.Row["Id"].ToString())
                    {
                        cboFinanceCustomer.SelectedItem = drv;
                    }
                }

                IQueryable<Staff> staffQuery = from staff in context.Staffs
                                               where staff.Id == salespersonId
                                               select staff;
                Staff s = staffQuery.FirstOrDefault();
                if (null == s)
                {
                    s = new Staff();
                }
                foreach (DataRowView drv in cboFinanceSalesPerson.Items)
                {
                    if (s.Id.ToString() == drv.Row["Id"].ToString())
                    {
                        cboFinanceSalesPerson.SelectedItem = drv;
                    }
                }
            }
        }

        private void BindSaleCustomerInfo()
        {
            DataRow r = ((DataRowView)cboSales.SelectedItem).Row;

            int customerId = (int)r["CustomerId"];
            int salespersonId = (int)r["SalesPerson"];
            using (DealershipManagerEntities context = new DealershipManagerEntities())
            {
                IQueryable<Customer> customerQuery = from customer in context.Customers
                                                     where customer.Id == customerId
                                                     select customer;

                Customer c = customerQuery.FirstOrDefault();
                if (null == c)
                {
                    c = new Customer();
                }
                foreach (DataRowView drv in cboSaleCustomer.Items)
                {
                    if (c.Id.ToString() == drv.Row["Id"].ToString())
                    {
                        cboSaleCustomer.SelectedItem = drv;
                    }
                }

                IQueryable<Staff> staffQuery = from staff in context.Staffs
                                               where staff.Id == salespersonId
                                               select staff;
                Staff s = staffQuery.FirstOrDefault();
                if (null == s)
                {
                    s = new Staff();
                }
                foreach (DataRowView drv in cboSaleSalesPerson.Items)
                {
                    if (s.Id.ToString() == drv.Row["Id"].ToString())
                    {
                        cboSaleSalesPerson.SelectedItem = drv;
                    }
                }
            }
        }

        private void BindLeaseCustomerInfo()
        {
            DataRow r = ((DataRowView)cboLeases.SelectedItem).Row;

            int customerId = (int)r["CustomerId"];
            int salespersonId = (int)r["SalesPerson"];
            using (DealershipManagerEntities context = new DealershipManagerEntities())
            {
                IQueryable<Customer> customerQuery = from customer in context.Customers
                                                     where customer.Id == customerId
                                                     select customer;

                Customer c = customerQuery.FirstOrDefault();
                if (null == c)
                {
                    c = new Customer();
                }
                foreach (DataRowView drv in cboSaleCustomer.Items)
                {
                    if (c.Id.ToString() == drv.Row["Id"].ToString())
                    {
                        cboLeaseCustomer.SelectedItem = drv;
                    }
                }

                IQueryable<Staff> staffQuery = from staff in context.Staffs
                                               where staff.Id == salespersonId
                                               select staff;
                Staff s = staffQuery.FirstOrDefault();
                if (null == s)
                {
                    s = new Staff();
                }
                foreach (DataRowView drv in cboSaleSalesPerson.Items)
                {
                    if (s.Id.ToString() == drv.Row["Id"].ToString())
                    {

                        cboLeaseSalesPerson.SelectedItem = drv;
                    }
                }
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
        private void BindFinanceVehiclesInfo()
        {

            DataRow r = ((DataRowView)cboFinances.SelectedItem).Row;
            int vehicleId = (int)r["VehicleId"];

            using (DealershipManagerEntities context = new DealershipManagerEntities())
            {
                IQueryable<Vehicle> vehicleQuery = from vehicle in context.Vehicles
                                                   where vehicle.Id == vehicleId
                                                   select vehicle;

                Vehicle v = vehicleQuery.FirstOrDefault();
                if (null == v)
                {
                    v = new Vehicle();
                }

                foreach (Vehicle veh in cboFinanceVehicle.Items)
                {
                    if (v.Id.ToString() == veh.Id.ToString())
                    {
                        cboSaleVehicle.SelectedItem = veh;
                        cboLeaseVehicle.SelectedItem = veh;
                        cboFinanceVehicle.SelectedItem = veh;
                    }
                }


                if ("" != r["TradeID"].ToString())
                {
                    int TradeInId = (int)r["TradeID"];
                    IQueryable<Vehicle> tradeInQuery = from vehicle in context.Vehicles
                                                       where vehicle.Id == TradeInId
                                                       select vehicle;

                    Vehicle t = tradeInQuery.FirstOrDefault();
                    if (null == t)
                    {
                        t = new Vehicle();
                    }

                    foreach (DataRowView drv in cboFinanceTradeInVehicle.Items)
                    {
                        if (t.Id.ToString() == drv.Row["Id"].ToString())
                        {
                            cboSaleTradeInVehicle.SelectedItem = drv;
                            cboLeaseTradeIn.SelectedItem = drv;
                            cboFinanceTradeInVehicle.SelectedItem = drv;
                        }
                    }
                }
                else
                {
                    cboSaleTradeInVehicle.SelectedIndex = -1;
                    txtSaleTradeInMake.Text = "";
                    txtSaleTradeInModel.Text = "";
                    txtSaleTradeInOdometer.Text = "";
                    txtSaleTradeInVIN.Text = "";
                    txtSaleTradeInYear.Text = "";
                    cboLeaseTradeIn.SelectedIndex = -1;
                    txtLeaseTradeInMake.Text = "";
                    txtLeaseTradeInModel.Text = "";
                    txtLeaseTradeInOdometer.Text = "";
                    txtLeaseTradeInVIN.Text = "";
                    txtLeaseTradeInYear.Text = "";
                    cboFinanceTradeInVehicle.SelectedIndex = -1;
                    txtFinanceTradeInMake.Text = "";
                    txtFinanceTradeInModel.Text = "";
                    txtFinanceTradeInOdometer.Text = "";
                    txtFinanceTradeInVIN.Text = "";
                    txtFinanceTradeInYear.Text = "";
                }
            }
        }

        private void BindSalesVehiclesInfo()
        {
            DataRow r = ((DataRowView)cboSales.SelectedItem).Row;
            int vehicleId = (int)r["VehicleId"];

            using (DealershipManagerEntities context = new DealershipManagerEntities())
            {
                IQueryable<Vehicle> vehicleQuery = from vehicle in context.Vehicles
                                                   where vehicle.Id == vehicleId
                                                   select vehicle;

                Vehicle v = vehicleQuery.FirstOrDefault();
                if (null == v)
                {
                    v = new Vehicle();
                }

                foreach (Vehicle veh in cboSaleVehicle.Items)
                {
                    if (v.Id.ToString() == veh.Id.ToString())
                    {
                        cboSaleVehicle.SelectedItem = veh;
                        cboLeaseVehicle.SelectedItem = veh;
                        cboFinanceVehicle.SelectedItem = veh;
                    }
                }

                if ("" != r["TradeID"].ToString())
                {
                    int TradeInId = (int)r["TradeID"];
                    IQueryable<Vehicle> tradeInQuery = from vehicle in context.Vehicles
                                                       where vehicle.Id == TradeInId
                                                       select vehicle;

                    Vehicle t = tradeInQuery.FirstOrDefault();
                    if (null == t)
                    {
                        t = new Vehicle();
                    }
                    foreach (DataRowView drv in cboSaleTradeInVehicle.Items)
                    {
                        if (t.Id.ToString() == drv.Row["Id"].ToString())
                        {
                            cboSaleTradeInVehicle.SelectedItem = drv;
                            cboLeaseTradeIn.SelectedItem = drv;
                            cboFinanceTradeInVehicle.SelectedItem = drv;
                        }
                    }
                }
                else
                {
                    cboSaleTradeInVehicle.SelectedIndex = -1;
                    txtSaleTradeInMake.Text = "";
                    txtSaleTradeInModel.Text = "";
                    txtSaleTradeInOdometer.Text = "";
                    txtSaleTradeInVIN.Text = "";
                    txtSaleTradeInYear.Text = "";
                    cboLeaseTradeIn.SelectedIndex = -1;
                    txtLeaseTradeInMake.Text = "";
                    txtLeaseTradeInModel.Text = "";
                    txtLeaseTradeInOdometer.Text = "";
                    txtLeaseTradeInVIN.Text = "";
                    txtLeaseTradeInYear.Text = "";
                    cboFinanceTradeInVehicle.SelectedIndex = -1;
                    txtFinanceTradeInMake.Text = "";
                    txtFinanceTradeInModel.Text = "";
                    txtFinanceTradeInOdometer.Text = "";
                    txtFinanceTradeInVIN.Text = "";
                    txtFinanceTradeInYear.Text = "";
                }
            }
        }

        private void BindLeasesVehiclesInfo()
        {
            DataRow r = ((DataRowView)cboLeases.SelectedItem).Row;
            int vehicleId = (int)r["VehicleId"];

            using (DealershipManagerEntities context = new DealershipManagerEntities())
            {
                IQueryable<Vehicle> vehicleQuery = from vehicle in context.Vehicles
                                                   where vehicle.Id == vehicleId
                                                   select vehicle;

                Vehicle v = vehicleQuery.FirstOrDefault();
                if (null == v)
                {
                    v = new Vehicle();
                }
                foreach (Vehicle veh in cboLeaseVehicle.Items)
                {
                    if (v.Id.ToString() == veh.Id.ToString())
                    {
                        cboSaleVehicle.SelectedItem = veh;
                        cboLeaseVehicle.SelectedItem = veh;
                        cboFinanceVehicle.SelectedItem = veh;
                    }
                }

                if ("" != r["TradeID"].ToString())
                {
                    int TradeInId = (int)r["TradeID"];
                    IQueryable<Vehicle> tradeInQuery = from vehicle in context.Vehicles
                                                       where vehicle.Id == TradeInId
                                                       select vehicle;

                    Vehicle t = tradeInQuery.FirstOrDefault();
                    if (null == t)
                    {
                        t = new Vehicle();
                    }
                    foreach (DataRowView drv in cboLeaseTradeIn.Items)
                    {
                        if (t.Id.ToString() == drv.Row["Id"].ToString())
                        {
                            cboSaleTradeInVehicle.SelectedItem = drv;
                            cboLeaseTradeIn.SelectedItem = drv;
                            cboFinanceTradeInVehicle.SelectedItem = drv;
                        }
                    }
                }
                else
                {
                    cboSaleTradeInVehicle.SelectedIndex = -1;
                    txtSaleTradeInMake.Text = "";
                    txtSaleTradeInModel.Text = "";
                    txtSaleTradeInOdometer.Text = "";
                    txtSaleTradeInVIN.Text = "";
                    txtSaleTradeInYear.Text = "";
                    cboLeaseTradeIn.SelectedIndex = -1;
                    txtLeaseTradeInMake.Text = "";
                    txtLeaseTradeInModel.Text = "";
                    txtLeaseTradeInOdometer.Text = "";
                    txtLeaseTradeInVIN.Text = "";
                    txtLeaseTradeInYear.Text = "";
                    cboFinanceTradeInVehicle.SelectedIndex = -1;
                    txtFinanceTradeInMake.Text = "";
                    txtFinanceTradeInModel.Text = "";
                    txtFinanceTradeInOdometer.Text = "";
                    txtFinanceTradeInVIN.Text = "";
                    txtFinanceTradeInYear.Text = "";
                }
            }
        }

        private void BindFinancesVehiclesInfo()
        {
            DataRow r = ((DataRowView)cboFinances.SelectedItem).Row;
            int vehicleId = (int)r["VehicleId"];

            using (DealershipManagerEntities context = new DealershipManagerEntities())
            {
                IQueryable<Vehicle> vehicleQuery = from vehicle in context.Vehicles
                                                   where vehicle.Id == vehicleId
                                                   select vehicle;

                Vehicle v = vehicleQuery.FirstOrDefault();
                if (null == v)
                {
                    v = new Vehicle();
                }
                foreach (DataRowView drv in cboSaleVehicle.Items)
                {
                    if (v.Id.ToString() == drv.Row["Id"].ToString())
                    {
                        cboSaleVehicle.SelectedItem = drv;
                        cboLeaseVehicle.SelectedItem = drv;
                        cboFinanceVehicle.SelectedItem = drv;
                    }
                }


                if ("" != r["TradeID"].ToString())
                {
                    int TradeInId = (int)r["TradeID"];
                    IQueryable<Vehicle> tradeInQuery = from vehicle in context.Vehicles
                                                       where vehicle.Id == TradeInId
                                                       select vehicle;

                    Vehicle t = tradeInQuery.FirstOrDefault();
                    if (null == t)
                    {
                        t = new Vehicle();
                    }
                    foreach (DataRowView drv in cboSaleTradeInVehicle.Items)
                    {
                        if (t.Id.ToString() == drv.Row["Id"].ToString())
                        {
                            cboSaleTradeInVehicle.SelectedItem = drv;
                            cboLeaseTradeIn.SelectedItem = drv;
                            cboFinanceTradeInVehicle.SelectedItem = drv;
                        }
                    }
                }
                else
                {
                    cboSaleTradeInVehicle.SelectedIndex = -1;
                    txtSaleTradeInMake.Text = "";
                    txtSaleTradeInModel.Text = "";
                    txtSaleTradeInOdometer.Text = "";
                    txtSaleTradeInVIN.Text = "";
                    txtSaleTradeInYear.Text = "";
                    cboLeaseTradeIn.SelectedIndex = -1;
                    txtLeaseTradeInMake.Text = "";
                    txtLeaseTradeInModel.Text = "";
                    txtLeaseTradeInOdometer.Text = "";
                    txtLeaseTradeInVIN.Text = "";
                    txtLeaseTradeInYear.Text = "";
                    cboFinanceTradeInVehicle.SelectedIndex = -1;
                    txtFinanceTradeInMake.Text = "";
                    txtFinanceTradeInModel.Text = "";
                    txtFinanceTradeInOdometer.Text = "";
                    txtFinanceTradeInVIN.Text = "";
                    txtFinanceTradeInYear.Text = "";
                }
            }
        }

        private void BindFinanceInfo()
        {
            DataRow r = ((DataRowView)cboFinances.SelectedItem).Row;

            txtFinanceSalePrice.Text = r["Price"].ToString();
            txtFinanceTradeInAllowance.Text = r["TradeAllowance"].ToString();
            txtFinanceAdministrationFee.Text = r["AdminFee"].ToString();
            txtFinanceGST.Text = r["GST"].ToString();
            txtFinanceDeposit.Text = r["Deposit"].ToString();
            txtFinanceWarranty.Text = r["Warranty"].ToString();
            txtFinanceGSTOnWarranty.Text = r["WarrantyGST"].ToString();
            txtPayoutOnLien.Text = r["PayoutonLien"].ToString();
            txtFinanceTotalBalanceDue.Text = r["TotalBalance"].ToString();
            txtFinanceNotes.Text = r["Notes"].ToString();
            txtFinanceInterestAmount.Text = r["Interest"].ToString();

            txtFinanceSubtotal.Text = ((double)r["Price"] - (double)r["TradeAllowance"] + (double)r["AdminFee"]).ToString();

            txtFinanceInterestRate.Text = r["Rate"].ToString();
            txtFinanceNumberofPayments.Text = r["noPayments"].ToString();
            txtFinancePaymentAmount.Text = r["PaymentDue"].ToString();

        }

        private void BindSaleInfo()
        {
            DataRow r = ((DataRowView)cboSales.SelectedItem).Row;

            txtSalePrice.Text = r["Price"].ToString();
            txtSaleTradeInAllowance.Text = r["TradeAllowance"].ToString();
            txtSaleAdministrationFee.Text = r["AdminFee"].ToString();
            txtSaleGST.Text = r["GST"].ToString();
            txtSaleDeposit.Text = r["Deposit"].ToString();
            txtSaleWarranty.Text = r["Warranty"].ToString();
            txtSaleWarrantyGST.Text = r["WarrantyGST"].ToString();
            txtSalePayoutOnLien.Text = r["PayoutonLien"].ToString();
            txtSaleTotalBalance.Text = r["TotalBalance"].ToString();
            txtSaleCustomerNotes.Text = r["Notes"].ToString();

            txtSaleSubtotal.Text = ((double)r["Price"] - (double)r["TradeAllowance"] + (double)r["AdminFee"]).ToString();


            if (r["PaymentDue"].ToString() != "0" || r["PaymentDue"].ToString() != "")
            {
                chkSalePaid.Checked = false;
            }
            else
            {
                chkSalePaid.Checked = true;
            }
        }

        private void BindLeaseInfo()
        {
            DataRow r = ((DataRowView)cboLeases.SelectedItem).Row;

            txtLeaseSalePrice.Text = r["InitialPrice"].ToString();
            txtLeaseTradeInAllowance.Text = r["TradeInAllowance"].ToString();
            txtLeaseAdministrationFee.Text = r["AdminFee"].ToString();
            txtLeaseWarranty.Text = r["Warranty"].ToString();
            txtLeaseCashDownPayment.Text = r["Down"].ToString();
            txtLeaseGSTDownPayment.Text = r["WarrantyGST"].ToString();
            txtLeaseInterestRate.Text = r["Rate"].ToString();
            txtLeaseNumberofPayments.Text = r["NoPayments"].ToString();
            txtLeasePaymentAmount.Text = r["PaymentAmount"].ToString();

            txtLeaseSubtotal.Text = ((double)r["InitialPrice"] - (double)r["TradeInAllowance"] + (double)r["AdminFee"]).ToString();


        }

        private void cboSales_Format(object sender, ListControlConvertEventArgs e)
        {
            DataRow r = ((DataRowView)e.ListItem).Row;
            DateTime dt = (DateTime)r["SaleDate"];
            e.Value = dt.ToShortDateString() + " - " + r["FirstName"] + " " + r["LastName"];
        }

        private void btnEditExistingSale_Click(object sender, EventArgs e)
        {
            cboSaleVehicle.DataSource = getAllVehicleDataSource();
            BindSaleInfo();
            BindSaleCustomerInfo();
            BindSalesVehiclesInfo();
        }

        private void btnSaleCancel_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.Controls)
            {
                UnbindForm(c);
            }

            currentFinance = null;
            currentLease = null;
            currentSale = null;

            cboFinances.SelectedIndex = -1;
            DealForm_Load(this, null);
        }

        private void btnDeleteSale_Click(object sender, EventArgs e)
        {
            int id = (int)cboSales.SelectedValue;
            if (MessageBox.Show("Are you sure you want to delete this Sale?", "ALERT", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (DealershipManagerEntities context = new DealershipManagerEntities())
                {
                    var sale = (from s in context.Sales
                                where s.id == id
                                select s).First();

                    context.Sales.DeleteObject(sale);
                    context.SaveChanges();
                }

            };
            foreach (Control c in this.Controls)
            {
                UnbindForm(c);
            }

            DealForm_Load(this, null);
        }

        private void btnSaveSale_Click(object sender, EventArgs e)
        {
            bool isNewSale = cboSales.SelectedIndex == -1;
            if (isNewSale)
            {
                using (DealershipManagerEntities context = new DealershipManagerEntities())
                {
                    Sale newSale = new Sale();
                    try
                    {
                        newSale.CustomerId = (int)cboSaleCustomer.SelectedValue;
                        newSale.VehicleId = (int)cboSaleVehicle.SelectedValue;
                        if (cboSaleTradeInVehicle.SelectedIndex != -1)
                        {
                            newSale.TradeInVehicle = cboSaleVehicle.SelectedValue.ToString();
                        }
                        else
                        {
                            newSale.TradeInVehicle = "0";
                        }
                        newSale.SalesPerson = (int)cboSaleSalesPerson.SelectedValue;
                        newSale.Date = dtpSaleDate.Value;

                        double price = 0;
                        Double.TryParse(txtSalePrice.Text, NumberStyles.Currency, null, out price);
                        newSale.Price = price;

                        double tradein = 0;
                        Double.TryParse(txtSaleTradeInAllowance.Text, NumberStyles.Currency, null, out tradein);
                        newSale.TradeAllowance = tradein;

                        double adminFee = 0;
                        Double.TryParse(txtSaleAdministrationFee.Text, NumberStyles.Currency, null, out adminFee);
                        newSale.AdminFee = adminFee;

                        double gst = 0;
                        Double.TryParse(txtSaleGST.Text, NumberStyles.Currency, null, out gst);
                        newSale.GST = gst;

                        double deposit = 0;
                        Double.TryParse(txtSaleDeposit.Text, NumberStyles.Currency, null, out deposit);
                        newSale.Deposit = deposit;

                        double warranty = 0;
                        Double.TryParse(txtSaleWarranty.Text, NumberStyles.Currency, null, out warranty);
                        newSale.Warranty = warranty;

                        double warrantyGST = 0;
                        Double.TryParse(txtSaleWarrantyGST.Text, NumberStyles.Currency, null, out warrantyGST);
                        newSale.WarrantyGST = warrantyGST;

                        double lienpayout = 0;
                        Double.TryParse(txtSalePayoutOnLien.Text, NumberStyles.Currency, null, out lienpayout);
                        newSale.PayoutonLien = lienpayout;

                        double totalBalance = 0;
                        Double.TryParse(txtSaleTotalBalance.Text, NumberStyles.Currency, null, out totalBalance);
                        newSale.TotalBalance = totalBalance;

                        newSale.Notes = txtSaleCustomerNotes.Text;

                        //NOT SURE WHAT THESE DO BUT THEY ARE IMPORTANT
                        newSale.current = false;
                        newSale.Interest = 0;
                        newSale.noPayments = 0;
                        newSale.PaymentDue = 0;
                        newSale.paymentFrequency = 0;
                        newSale.Rate = 0;
                        newSale.TotalCostofVehicle = totalBalance;
                        newSale.VehicleMilage = "0";
                        newSale.GrossCommission = 0;
                        newSale.FlatCommission = 0;
                        newSale.CurrentBalance = totalBalance;

                        double totalCost = price - tradein + adminFee + gst - deposit + warranty + warrantyGST + lienpayout;

                        Vehicle vehicle = (from v in context.Vehicles
                                           where v.Id == newSale.VehicleId
                                           select v).FirstOrDefault();

                        vehicle.InStock = 0;

                        context.Sales.AddObject(newSale);
                        context.SaveChanges();
                        MessageBox.Show("Sale Saved Successfully");
                        currentSale = newSale;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error Saving: " + ex.Message);
                    }

                }
            }
            else
            {
                using (DealershipManagerEntities context = new DealershipManagerEntities())
                {
                    DataRow r = ((DataRowView)cboSales.SelectedItem).Row;

                    int id = (int)r["Id"];
                    IQueryable<Sale> saleQuery = from sale in context.Sales
                                                 where sale.id == id
                                                 select sale;

                    Sale s = saleQuery.FirstOrDefault();

                    currentSale = s;
                    try
                    {
                        currentSale.CustomerId = (int)cboSaleCustomer.SelectedValue;
                        currentSale.VehicleId = (int)cboSaleVehicle.SelectedValue;
                        if (cboSaleTradeInVehicle.SelectedIndex != -1)
                        {
                            currentSale.TradeInVehicle = cboSaleVehicle.SelectedValue.ToString();
                        }
                        currentSale.SalesPerson = (int)cboSaleSalesPerson.SelectedValue;
                        currentSale.Date = dtpSaleDate.Value;


                        double price = 0;
                        Double.TryParse(txtSalePrice.Text, NumberStyles.Currency, null, out price);
                        currentSale.Price = price;

                        double tradein = 0;
                        Double.TryParse(txtSaleTradeInAllowance.Text, NumberStyles.Currency, null, out tradein);
                        currentSale.TradeAllowance = tradein;

                        double adminFee = 0;
                        Double.TryParse(txtSaleAdministrationFee.Text, NumberStyles.Currency, null, out adminFee);
                        currentSale.AdminFee = adminFee;

                        double gst = 0;
                        Double.TryParse(txtSaleGST.Text, NumberStyles.Currency, null, out gst);
                        currentSale.GST = gst;

                        double deposit = 0;
                        Double.TryParse(txtSaleDeposit.Text, NumberStyles.Currency, null, out deposit);
                        currentSale.Deposit = deposit;

                        double warranty = 0;
                        Double.TryParse(txtSaleWarranty.Text, NumberStyles.Currency, null, out warranty);
                        currentSale.Warranty = warranty;

                        double warrantyGST = 0;
                        Double.TryParse(txtSaleWarrantyGST.Text, NumberStyles.Currency, null, out warrantyGST);
                        currentSale.WarrantyGST = warrantyGST;

                        double lienpayout = 0;
                        Double.TryParse(txtSalePayoutOnLien.Text, NumberStyles.Currency, null, out lienpayout);
                        currentSale.PayoutonLien = lienpayout;

                        double totalBalance = 0;
                        Double.TryParse(txtSaleTotalBalance.Text, NumberStyles.Currency, null, out totalBalance);
                        currentSale.TotalBalance = totalBalance;

                        currentSale.Notes = txtSaleCustomerNotes.Text;

                        //NOT SURE WHAT THESE DO BUT THEY ARE IMPORTANT
                        currentSale.current = false;
                        currentSale.Interest = 0;
                        currentSale.noPayments = 0;
                        currentSale.PaymentDue = 0;
                        currentSale.paymentFrequency = 0;
                        currentSale.Rate = 0;
                        currentSale.TotalCostofVehicle = totalBalance;
                        currentSale.VehicleMilage = "0";
                        currentSale.GrossCommission = 0;
                        currentSale.FlatCommission = 0;

                        double totalCost = price - tradein + adminFee + gst - deposit + warranty + warrantyGST + lienpayout;

                        Vehicle vehicle = (from v in context.Vehicles
                                           where v.Id == currentSale.VehicleId
                                           select v).FirstOrDefault();

                        vehicle.InStock = 0;

                        context.SaveChanges();

                        MessageBox.Show("Sale Saved Successfully");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error Saving: " + ex.Message);
                    }
                }
            }
            try
            {
                if (currentSale.Warranty > 0)
                {
                    String warrantyValue = Microsoft.VisualBasic.Interaction.InputBox("What is the cost of the warranty?");
                    String warrantyLength = Microsoft.VisualBasic.Interaction.InputBox("How many months does the warranty last?");

                    Cost newCost = new Cost();
                    int expense;
                    int.TryParse(warrantyValue, out expense);
                    newCost.Cost1 = expense;
                    newCost.Date = currentSale.Date;
                    newCost.Description = "Warranty";
                    newCost.VIN = currentSale.VehicleId;
                    newCost.WO = "0";
                    using (DealershipManagerEntities context = new DealershipManagerEntities())
                    {
                        context.Costs.AddObject(newCost);
                        context.SaveChanges();
                        context.Connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving: " + ex.Message);
            }

            DealForm_Load(this, null);
        }

        private decimal CalculateSubtotal(string tab)
        {
            decimal price, tradein, adminFee, warranty, cashdown, downGST;

            if (tab == "Sale")
            {
                price = (txtSalePrice.Text != "") ? decimal.Parse(txtSalePrice.Text, NumberStyles.Currency) : 0;
                tradein = (txtSaleTradeInAllowance.Text != "") ? decimal.Parse(txtSaleTradeInAllowance.Text, NumberStyles.Currency) : 0;
                adminFee = (txtSaleAdministrationFee.Text != "") ? decimal.Parse(txtSaleAdministrationFee.Text, NumberStyles.Currency) : 0;
                warranty = 0;
                cashdown = 0;
                downGST = 0;
            }
            else if (tab == "Lease")
            {
                price = (txtLeaseSalePrice.Text != "") ? decimal.Parse(txtLeaseSalePrice.Text, NumberStyles.Currency) : 0;
                tradein = (txtLeaseTradeInAllowance.Text != "") ? decimal.Parse(txtLeaseTradeInAllowance.Text, NumberStyles.Currency) : 0;
                adminFee = (txtLeaseAdministrationFee.Text != "") ? decimal.Parse(txtLeaseAdministrationFee.Text, NumberStyles.Currency) : 0;
                warranty = (txtLeaseWarranty.Text != "") ? decimal.Parse(txtLeaseWarranty.Text, NumberStyles.Currency) : 0;
                cashdown = (txtLeaseCashDownPayment.Text != "") ? decimal.Parse(txtLeaseCashDownPayment.Text, NumberStyles.Currency) : 0;
                downGST = (txtLeaseGSTDownPayment.Text != "") ? decimal.Parse(txtLeaseGSTDownPayment.Text, NumberStyles.Currency) : 0;
            }
            else if (tab == "Finance")
            {

                price = (txtFinanceSalePrice.Text != "") ? decimal.Parse(txtFinanceSalePrice.Text, NumberStyles.Currency) : 0;
                tradein = (txtFinanceTradeInAllowance.Text != "") ? decimal.Parse(txtFinanceTradeInAllowance.Text, NumberStyles.Currency) : 0;
                adminFee = (txtFinanceAdministrationFee.Text != "") ? decimal.Parse(txtFinanceAdministrationFee.Text, NumberStyles.Currency) : 0;
                warranty = 0;
                cashdown = 0;
                downGST = 0;
            }
            else
            {
                price = 0;
                tradein = 0;
                adminFee = 0;
                warranty = 0;
                cashdown = 0;
                downGST = 0;
            }

            return price - tradein + adminFee + warranty - cashdown + downGST;
        }

        public decimal CalculateTotal(String tab)
        {
            decimal price, tradein, adminFee, gst, deposit, warranty, warrantyGST, lienpayout, interestAmount;
            if (tab == "Sale")
            {
                price = (txtSalePrice.Text != "") ? decimal.Parse(txtSalePrice.Text, NumberStyles.Currency) : 0;
                tradein = (txtSaleTradeInAllowance.Text != "") ? decimal.Parse(txtSaleTradeInAllowance.Text, NumberStyles.Currency) : 0;
                adminFee = (txtSaleAdministrationFee.Text != "") ? decimal.Parse(txtSaleAdministrationFee.Text, NumberStyles.Currency) : 0;
                gst = (txtSaleGST.Text != "") ? decimal.Parse(txtSaleGST.Text, NumberStyles.Currency) : 0;
                deposit = (txtSaleDeposit.Text != "") ? decimal.Parse(txtSaleDeposit.Text, NumberStyles.Currency) : 0;
                warranty = (txtSaleWarranty.Text != "") ? decimal.Parse(txtSaleWarranty.Text, NumberStyles.Currency) : 0;
                warrantyGST = (txtSaleWarrantyGST.Text != "") ? decimal.Parse(txtSaleWarrantyGST.Text, NumberStyles.Currency) : 0;
                lienpayout = (txtSalePayoutOnLien.Text != "") ? decimal.Parse(txtSalePayoutOnLien.Text, NumberStyles.Currency) : 0;
                interestAmount = 0;
            }
            else if (tab == "Finance")
            {

                price = (txtFinanceSalePrice.Text != "") ? decimal.Parse(txtFinanceSalePrice.Text, NumberStyles.Currency) : 0;
                tradein = (txtFinanceTradeInAllowance.Text != "") ? decimal.Parse(txtFinanceTradeInAllowance.Text, NumberStyles.Currency) : 0;
                adminFee = (txtFinanceAdministrationFee.Text != "") ? decimal.Parse(txtFinanceAdministrationFee.Text, NumberStyles.Currency) : 0;
                gst = (txtFinanceGST.Text != "") ? decimal.Parse(txtFinanceGST.Text, NumberStyles.Currency) : 0;
                deposit = (txtFinanceDeposit.Text != "") ? decimal.Parse(txtFinanceDeposit.Text, NumberStyles.Currency) : 0;
                warranty = (txtFinanceWarranty.Text != "") ? decimal.Parse(txtFinanceWarranty.Text, NumberStyles.Currency) : 0;
                warrantyGST = (txtFinanceGSTOnWarranty.Text != "") ? decimal.Parse(txtFinanceGSTOnWarranty.Text, NumberStyles.Currency) : 0;
                lienpayout = (txtPayoutOnLien.Text != "") ? decimal.Parse(txtPayoutOnLien.Text, NumberStyles.Currency) : 0;
                interestAmount = (txtFinanceInterestAmount.Text != "") ? decimal.Parse(txtFinanceInterestAmount.Text, NumberStyles.Currency) : 0;
            }
            else
            {
                price = 0;
                tradein = 0;
                adminFee = 0;
                gst = 0;
                deposit = 0;
                warranty = 0;
                warrantyGST = 0;
                lienpayout = 0;
                interestAmount = 0;
            }

            return price - tradein + adminFee - deposit + gst + warranty + warrantyGST + lienpayout + interestAmount;
        }

        private void cboLeases_Format(object sender, ListControlConvertEventArgs e)
        {
            DataRow r = ((DataRowView)e.ListItem).Row;
            DateTime dt = (DateTime)r["Date"];
            e.Value = dt.ToShortDateString() + " - " + r["FirstName"] + " " + r["LastName"];
        }

        private void btnEditExistingLease_Click(object sender, EventArgs e)
        {
            cboLeaseVehicle.DataSource = getAllVehicleDataSource();
            BindLeaseInfo();
            BindLeaseCustomerInfo();
            BindLeasesVehiclesInfo();
        }

        private void cboLeaseCustomer_Format(object sender, ListControlConvertEventArgs e)
        {
            DataRow r = ((DataRowView)e.ListItem).Row;
            e.Value = r["LastName"] + ", " + r["FirstName"];
        }

        private void cboFinanceCustomer_Format(object sender, ListControlConvertEventArgs e)
        {
            DataRow r = ((DataRowView)e.ListItem).Row;
            e.Value = r["LastName"] + ", " + r["FirstName"];
        }

        private void cboSaleVehicle_Format(object sender, ListControlConvertEventArgs e)
        {
            string lastvin = "";
            string year = "";
            string make = "";
            string model = "";
            try
            {
                Vehicle v = ((Vehicle)e.ListItem);

                year = (v.Year ?? 0).ToString();
                if (year == "0")
                    year = "";
                make = v.Make ?? "".ToString();
                model = v.Model ?? "".ToString();
                lastvin = (v.VIN ?? "".ToString()).GetLast(4);
            }
            catch
            {
            }
            finally
            {
                e.Value = year + " " + make + " " + model + " " + lastvin;
            }
        }

        private void cboSaleTradeInVehicle_Format(object sender, ListControlConvertEventArgs e)
        {
            string lastvin = "";
            string year = "";
            string make = "";
            string model = "";
            try
            {
                Vehicle v = ((Vehicle)e.ListItem);

                year = (v.Year ?? 0).ToString();
                if (year == "0")
                    year = "";
                make = v.Make ?? "".ToString();
                model = v.Model ?? "".ToString();
                lastvin = (v.VIN ?? "".ToString()).GetLast(4);
            }
            catch
            {
            }
            finally
            {
                e.Value = year + " " + make + " " + model + " " + lastvin;
            }
        }

        private void cboLeaseVehicle_Format(object sender, ListControlConvertEventArgs e)
        {
            string lastvin = "";
            string year = "";
            string make = "";
            string model = "";
            try
            {
                Vehicle v = ((Vehicle)e.ListItem);

                year = (v.Year ?? 0).ToString();
                if (year == "0")
                    year = "";
                make = v.Make ?? "".ToString();
                model = v.Model ?? "".ToString();
                lastvin = (v.VIN ?? "".ToString()).GetLast(4);
            }
            catch
            {
            }
            finally
            {
                e.Value = year + " " + make + " " + model + " " + lastvin;
            }
        }

        private void cboLeaseTradeIn_Format(object sender, ListControlConvertEventArgs e)
        {
            string lastvin = "";
            string year = "";
            string make = "";
            string model = "";
            try
            {
                Vehicle v = ((Vehicle)e.ListItem);

                year = (v.Year ?? 0).ToString();
                if (year == "0")
                    year = "";
                make = v.Make ?? "".ToString();
                model = v.Model ?? "".ToString();
                lastvin = (v.VIN ?? "".ToString()).GetLast(4);
            }
            catch
            {
            }
            finally
            {
                e.Value = year + " " + make + " " + model + " " + lastvin;
            }
        }

        private void cboFinanceVehicle_Format(object sender, ListControlConvertEventArgs e)
        {
            string lastvin = "";
            string year = "";
            string make = "";
            string model = "";
            try
            {
                Vehicle v = ((Vehicle)e.ListItem);

                year = (v.Year ?? 0).ToString();
                if (year == "0")
                    year = "";
                make = v.Make ?? "".ToString();
                model = v.Model ?? "".ToString();
                lastvin = (v.VIN ?? "".ToString()).GetLast(4);
            }
            catch
            {
            }
            finally
            {
                e.Value = year + " " + make + " " + model + " " + lastvin;
            }
        }

        private void cboFinanceTradeInVehicle_Format(object sender, ListControlConvertEventArgs e)
        {
            string lastvin = "";
            string year = "";
            string make = "";
            string model = "";
            try
            {
                Vehicle v = ((Vehicle)e.ListItem);

                year = (v.Year ?? 0).ToString();
                if (year == "0")
                    year = "";
                make = v.Make ?? "".ToString();
                model = v.Model ?? "".ToString();
                lastvin = (v.VIN ?? "".ToString()).GetLast(4);
            }
            catch
            {
            }
            finally
            {
                e.Value = year + " " + make + " " + model + " " + lastvin;
            }
        }

        private void cboFinanceVehicle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (null != cboFinanceVehicle.SelectedValue)
            {
                int id = (int)cboFinanceVehicle.SelectedValue;
                cboSaleVehicle.SelectedValue = cboFinanceVehicle.SelectedValue;
                cboLeaseVehicle.SelectedValue = cboFinanceVehicle.SelectedValue;
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
                    BindSaleVehicleInfo(veh);
                }
            }
        }

        private void cboLeaseVehicle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (null != cboLeaseVehicle.SelectedValue)
            {
                int id = (int)cboLeaseVehicle.SelectedValue;
                cboSaleVehicle.SelectedValue = cboLeaseVehicle.SelectedValue;
                cboFinanceVehicle.SelectedValue = cboLeaseVehicle.SelectedValue;
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
                    BindSaleVehicleInfo(veh);
                }
            }
        }

        private void cboLeaseTradeIn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (null != cboLeaseTradeIn.SelectedValue)
            {
                int id = (int)cboLeaseTradeIn.SelectedValue;
                cboSaleTradeInVehicle.SelectedValue = cboLeaseTradeIn.SelectedValue;
                cboFinanceTradeInVehicle.SelectedValue = cboLeaseTradeIn.SelectedValue;
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
                    BindTradeInVehicleInfo(veh);
                }
            }
        }

        private void cboFinanceTradeInVehicle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (null != cboFinanceTradeInVehicle.SelectedValue)
            {
                int id = (int)cboFinanceTradeInVehicle.SelectedValue;
                cboLeaseTradeIn.SelectedValue = cboFinanceTradeInVehicle.SelectedValue;
                cboFinanceTradeInVehicle.SelectedValue = cboFinanceTradeInVehicle.SelectedValue;
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
                    BindTradeInVehicleInfo(veh);
                }
            }
        }

        private void cboLeaseSalesPerson_Format(object sender, ListControlConvertEventArgs e)
        {
            DataRow r = ((DataRowView)e.ListItem).Row;
            e.Value = r["LastName"] + ", " + r["FirstName"];
        }

        private void cboFinanceSalesPerson_Format(object sender, ListControlConvertEventArgs e)
        {
            DataRow r = ((DataRowView)e.ListItem).Row;
            e.Value = r["LastName"] + ", " + r["FirstName"];
        }

        private void btnFinanceEditExistingSale_Click(object sender, EventArgs e)
        {
            cboFinanceVehicle.DataSource = null;
            cboFinanceVehicle.Items.Clear();
            cboFinanceVehicle.DataSource = getAllVehicleDataSource();
            cboFinanceTradeInVehicle.DataSource = getAllVehicleDataSource();
            BindFinanceInfo();
            BindFinanceCustomerInfo();
            BindFinanceVehiclesInfo();
        }
        private void btnLeaseCancel_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.Controls)
            {
                UnbindForm(c);
            }
            currentFinance = null;
            currentLease = null;
            currentSale = null;

            cboFinances.SelectedIndex = -1;
            DealForm_Load(this, null);
        }

        private void btnFinanceCancel_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.Controls)
            {
                UnbindForm(c);
            }
            currentFinance = null;
            currentLease = null;
            currentSale = null;

            cboFinances.SelectedIndex = -1;
            DealForm_Load(this, null);
        }

        private void btnSaveLease_Click(object sender, EventArgs e)
        {
            bool isNewLease = cboLeases.SelectedIndex == -1;
            if (isNewLease)
            {
                using (DealershipManagerEntities context = new DealershipManagerEntities())
                {
                    Lease newLease = new Lease();
                    try
                    {
                        newLease.CustomerId = (int)cboLeaseCustomer.SelectedValue;
                        newLease.VehicleId = (int)cboLeaseVehicle.SelectedValue;
                        if (cboLeaseTradeIn.SelectedIndex != -1)
                        {
                            newLease.TradeInId = (int)cboLeaseVehicle.SelectedValue;
                        }
                        newLease.SalesPerson = (int)cboLeaseSalesPerson.SelectedValue;
                        newLease.Date = dtpLeaseDate.Value;

                        double price = 0;
                        Double.TryParse(txtLeaseSalePrice.Text, NumberStyles.Currency, null, out price);
                        newLease.InitialPrice = price;

                        double tradein = 0;
                        Double.TryParse(txtLeaseTradeInAllowance.Text, NumberStyles.Currency, null, out tradein);
                        newLease.TradeInAllowance = tradein;

                        double adminFee = 0;
                        Double.TryParse(txtLeaseAdministrationFee.Text, NumberStyles.Currency, null, out adminFee);
                        newLease.AdminFee = adminFee;

                        double downPayment = 0;
                        Double.TryParse(txtLeaseCashDownPayment.Text, NumberStyles.Currency, null, out downPayment);
                        newLease.Down = downPayment;

                        double warranty = 0;
                        Double.TryParse(txtLeaseWarranty.Text, NumberStyles.Currency, null, out warranty);
                        newLease.Warranty = warranty;


                        double warrantyGST = 0;
                        Double.TryParse(txtLeaseGSTDownPayment.Text, NumberStyles.Currency, null, out warrantyGST);
                        newLease.WarrantyGST = warrantyGST;

                        int noPayments = 0;
                        int.TryParse(txtLeaseNumberofPayments.Text, NumberStyles.Currency, null, out noPayments);
                        newLease.NoPayments = noPayments;

                        double paymentAmount = 0;
                        Double.TryParse(txtLeasePaymentAmount.Text, NumberStyles.Currency, null, out paymentAmount);
                        newLease.PaymentAmount = paymentAmount;

                        double rate = 0;
                        Double.TryParse(txtLeaseInterestRate.Text, NumberStyles.Currency, null, out rate);
                        newLease.Rate = rate;

                        double totalCost = 0;
                        Double.TryParse(txtLeaseSubtotal.Text, NumberStyles.Currency, null, out totalCost);
                        newLease.TotalCostofVehicle = totalCost;

                        newLease.notes = txtLeaseNotes.Text;

                        //NOT SURE WHAT THESE DO BUT THEY ARE IMPORTANT

                        newLease.Current = (dtpLeaseDate.Value < dtpLeaseDate.Value.AddMonths(noPayments));
                        //newLease.Current = true;
                        newLease.InterestCalculated = 1;
                        newLease.Milage = 0;

                        Vehicle vehicle = (from v in context.Vehicles
                                           where v.Id == newLease.VehicleId
                                           select v).FirstOrDefault();

                        vehicle.InStock = 0;

                        context.Leases.AddObject(newLease);
                        context.SaveChanges();

                        double tempBalance = totalCost;
                        for (int i = 0; i < noPayments; i++)
                        {
                            LeasePayment newPayment = new LeasePayment();
                            newPayment.PaymentAmount = newLease.PaymentAmount;
                            newPayment.Interest = (newLease.Rate / 1200) * tempBalance;
                            double gst = double.Parse(Properties.Settings.Default["GST"].ToString());
                            newPayment.GST = paymentAmount * gst;
                            newPayment.Principle = paymentAmount - newPayment.Interest - newPayment.GST;
                            tempBalance = tempBalance - newPayment.Principle ?? 0;
                            newPayment.Balance = tempBalance;
                            newPayment.PmtNo = i + 1;
                            newPayment.LeaseId = newLease.Id;
                            context.LeasePayments.AddObject(newPayment);
                            context.SaveChanges();
                        }

                        MessageBox.Show("Lease Saved Successfully");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error Saving: " + ex.Message);
                    }

                }
            }
            else
            {
                using (DealershipManagerEntities context = new DealershipManagerEntities())
                {
                    DataRow r = ((DataRowView)cboLeases.SelectedItem).Row;

                    int id = (int)r["Id"];
                    IQueryable<Lease> LeaseQuery = from Lease in context.Leases
                                                   where Lease.Id == id
                                                   select Lease;

                    Lease s = LeaseQuery.FirstOrDefault();

                    currentLease = s;
                    try
                    {
                        currentLease.CustomerId = (int)cboLeaseCustomer.SelectedValue;
                        currentLease.VehicleId = (int)cboLeaseVehicle.SelectedValue;
                        if (cboLeaseTradeIn.SelectedIndex != -1)
                        {
                            currentLease.TradeInId = (int)cboLeaseVehicle.SelectedValue;
                        }
                        currentLease.SalesPerson = (int)cboLeaseSalesPerson.SelectedValue;
                        currentLease.Date = dtpLeaseDate.Value;

                        double price = 0;
                        Double.TryParse(txtLeaseSalePrice.Text, NumberStyles.Currency, null, out price);
                        currentLease.InitialPrice = price;

                        double tradein = 0;
                        Double.TryParse(txtLeaseTradeInAllowance.Text, NumberStyles.Currency, null, out tradein);
                        currentLease.TradeInAllowance = tradein;

                        double adminFee = 0;
                        Double.TryParse(txtLeaseAdministrationFee.Text, NumberStyles.Currency, null, out adminFee);
                        currentLease.AdminFee = adminFee;

                        double downPayment = 0;
                        Double.TryParse(txtLeaseCashDownPayment.Text, NumberStyles.Currency, null, out downPayment);
                        currentLease.Down = downPayment;

                        double warranty = 0;
                        Double.TryParse(txtLeaseWarranty.Text, NumberStyles.Currency, null, out warranty);
                        currentLease.Warranty = warranty;


                        double warrantyGST = 0;
                        Double.TryParse(txtLeaseGSTDownPayment.Text, NumberStyles.Currency, null, out warrantyGST);
                        currentLease.WarrantyGST = warrantyGST;

                        int noPayments = 0;
                        int.TryParse(txtLeaseNumberofPayments.Text, NumberStyles.Currency, null, out noPayments);
                        currentLease.NoPayments = noPayments;

                        double paymentAmount = 0;
                        Double.TryParse(txtLeasePaymentAmount.Text, NumberStyles.Currency, null, out paymentAmount);
                        currentLease.PaymentAmount = paymentAmount;

                        double rate = 0;
                        Double.TryParse(txtLeaseInterestRate.Text, NumberStyles.Currency, null, out rate);
                        currentLease.Rate = rate;

                        double totalCost = 0;
                        Double.TryParse(txtLeaseSubtotal.Text, NumberStyles.Currency, null, out totalCost);
                        currentLease.TotalCostofVehicle = totalCost;

                        currentLease.notes = txtLeaseNotes.Text;

                        Vehicle vehicle = (from v in context.Vehicles
                                           where v.Id == currentLease.VehicleId
                                           select v).FirstOrDefault();

                        vehicle.InStock = 0;

                        currentLease.Current = (dtpLeaseDate.Value < dtpLeaseDate.Value.AddMonths(noPayments));
                        currentLease.InterestCalculated = 1;
                        currentLease.Milage = 0;
                        MessageBox.Show("Lease Saved Successfully");
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error Saving: " + ex.Message);
                    }

                }

                try
                {
                    if (currentLease.Warranty > 0)
                    {
                        String warrantyValue = Microsoft.VisualBasic.Interaction.InputBox("What is the cost of the warranty?");
                        String warrantyLength = Microsoft.VisualBasic.Interaction.InputBox("How many months does the warranty last?");

                        Cost newCost = new Cost();
                        int expense;
                        int.TryParse(warrantyValue, out expense);
                        newCost.Cost1 = expense;
                        newCost.Date = currentLease.Date;
                        newCost.Description = "Warranty";
                        newCost.VIN = currentLease.VehicleId;
                        newCost.WO = "0";
                        using (DealershipManagerEntities context = new DealershipManagerEntities())
                        {
                            context.Costs.AddObject(newCost);
                            context.SaveChanges();
                            context.Connection.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Saving: " + ex.Message);
                }
                DealForm_Load(this, null);
            }
        }

        private void btnDeleteLease_Click(object sender, EventArgs e)
        {
            int id = (int)cboLeases.SelectedValue;
            if (MessageBox.Show("Are you sure you want to delete this Lease?", "ALERT", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (DealershipManagerEntities context = new DealershipManagerEntities())
                {
                    var lease = (from l in context.Leases
                                 where l.Id == id
                                 select l).First();

                    context.Leases.DeleteObject(lease);
                    context.SaveChanges();
                }

            };
            foreach (Control c in this.Controls)
            {
                UnbindForm(c);
            }

            DealForm_Load(this, null);
        }

        private void btnCalculateLease_Click(object sender, EventArgs e)
        {
            this.Hide();
            LeaseForm leaseform = new LeaseForm();
            leaseform.MdiParent = this.MdiParent;
            leaseform.txtCapitalizedCost.Text = txtLeaseSubtotal.Text;
            leaseform.txtInterestRate.Text = txtLeaseInterestRate.Text;
            leaseform.txtNumberOfMonths.Text = txtLeaseNumberofPayments.Text;
            leaseform.txtMonthlyPayment.Text = txtLeasePaymentAmount.Text;
            if (rdbCalculateMonthly.Checked)
            {
                leaseform.rdbMonthly.Checked = true;
            }
            if (rdbLeaseCalculateAnnually.Checked)
            {
                leaseform.rdbAnnually.Checked = true;
            }
            leaseform.currentLease = currentLease;
            leaseform.dealForm = this;
            leaseform.Show();
        }


        private void btnFinanceCalculate_Click(object sender, EventArgs e)
        {
            this.Hide();
            LeaseForm leaseform = new LeaseForm();
            leaseform.MdiParent = this.MdiParent;
            leaseform.txtCapitalizedCost.Text = txtFinanceSubtotal.Text;
            leaseform.txtInterestRate.Text = txtFinanceInterestRate.Text;
            leaseform.txtNumberOfMonths.Text = txtFinanceNumberofPayments.Text;
            leaseform.txtMonthlyPayment.Text = txtFinancePaymentAmount.Text;
            if (rdbCalculateMonthly.Checked)
            {
                leaseform.rdbMonthly.Checked = true;
            }
            if (rdbLeaseCalculateAnnually.Checked)
            {
                leaseform.rdbAnnually.Checked = true;
            }
            leaseform.currentLease = currentLease;
            leaseform.dealForm = this;
            leaseform.Show();
        }

        private void LoadVehicleForm(int id)
        {
            using (DealershipManagerEntities context = new DealershipManagerEntities())
            {
                IQueryable<Vehicle> vehicleQuery = from vehicle in context.Vehicles
                                                   where vehicle.Id == id
                                                   select vehicle;

                Vehicle v = vehicleQuery.FirstOrDefault();
                if (null == v)
                {
                    v = new Vehicle();
                }
                VehicleForm vehicleForm = new VehicleForm(v);

                vehicleForm.rdbAllVehicles.Checked = true;
                vehicleForm.dealHolder = this;
                vehicleForm.MdiParent = this.MdiParent;
                vehicleForm.Show();
                this.Hide();
            }
        }

        private void btnSaleEditVehicle_Click(object sender, EventArgs e)
        {
            int id = (int)cboSaleVehicle.SelectedValue;
            LoadVehicleForm(id);
        }

        private void btnSaleEditTradeIn_Click(object sender, EventArgs e)
        {
            int id = (int)cboSaleTradeInVehicle.SelectedValue;
            LoadVehicleForm(id);
        }

        private void btnLeaseEditVehicle_Click(object sender, EventArgs e)
        {
            int id = (int)cboLeaseVehicle.SelectedValue;
            LoadVehicleForm(id);
        }

        private void btnLeaseEditTradeIn_Click(object sender, EventArgs e)
        {
            int id = (int)cboLeaseTradeIn.SelectedValue;
            LoadVehicleForm(id);
        }

        private void btnFinanceEditVehicle_Click(object sender, EventArgs e)
        {
            int id = (int)cboFinanceVehicle.SelectedValue;
            LoadVehicleForm(id);
        }

        private void btnFinanceEditTradeIn_Click(object sender, EventArgs e)
        {
            int id = (int)cboFinanceTradeInVehicle.SelectedValue;
            LoadVehicleForm(id);
        }

        private void btnFinanceSaveSale_Click(object sender, EventArgs e)
        {
            //Need to figure out how this works

            bool isNewSale = cboFinances.SelectedIndex == -1;
            if (isNewSale)
            {
                using (DealershipManagerEntities context = new DealershipManagerEntities())
                {
                    Sale newSale = new Sale();
                    try
                    {

                        newSale.CustomerId = (int)cboFinanceCustomer.SelectedValue;
                        newSale.VehicleId = (int)cboFinanceVehicle.SelectedValue;
                        if (cboFinanceTradeInVehicle.SelectedIndex != -1)
                        {
                            newSale.TradeInVehicle = cboFinanceTradeInVehicle.SelectedValue.ToString();
                        }
                        else
                        {
                            newSale.TradeInVehicle = "0";
                        }

                        newSale.SalesPerson = (int)cboFinanceSalesPerson.SelectedValue;
                        newSale.Date = dtpFinanceDate.Value;


                        double price = 0;
                        Double.TryParse(txtFinanceSalePrice.Text, NumberStyles.Currency, null, out price);
                        newSale.Price = price;

                        double tradein = 0;
                        Double.TryParse(txtFinanceTradeInAllowance.Text, NumberStyles.Currency, null, out tradein);
                        newSale.TradeAllowance = tradein;

                        double adminFee = 0;
                        Double.TryParse(txtFinanceAdministrationFee.Text, NumberStyles.Currency, null, out adminFee);
                        newSale.AdminFee = adminFee;

                        double gst = 0;
                        Double.TryParse(txtFinanceGST.Text, NumberStyles.Currency, null, out gst);
                        newSale.GST = gst;

                        double deposit = 0;
                        Double.TryParse(txtFinanceDeposit.Text, NumberStyles.Currency, null, out deposit);
                        newSale.Deposit = deposit;

                        double warranty = 0;
                        Double.TryParse(txtFinanceWarranty.Text, NumberStyles.Currency, null, out warranty);
                        newSale.Warranty = warranty;

                        double warrantyGST = 0;
                        Double.TryParse(txtFinanceGSTOnWarranty.Text, NumberStyles.Currency, null, out warrantyGST);
                        newSale.WarrantyGST = warrantyGST;

                        double lienpayout = 0;
                        Double.TryParse(txtPayoutOnLien.Text, NumberStyles.Currency, null, out lienpayout);
                        newSale.PayoutonLien = lienpayout;

                        double totalBalance = 0;
                        Double.TryParse(txtFinanceTotalBalanceDue.Text, NumberStyles.Currency, null, out totalBalance);
                        newSale.TotalBalance = totalBalance;

                        newSale.Notes = txtFinanceNotes.Text;
                        newSale.current = true;

                        // HOLY SNAP I FIGURED OUT WHAT THESE ARE FOR. This is ONLY for Financing!

                        double interestAmount = 0;
                        Double.TryParse(txtFinanceInterestAmount.Text, NumberStyles.Currency, null, out interestAmount);
                        newSale.Interest = interestAmount;

                        int payments = 0;
                        Int32.TryParse(txtFinanceNumberofPayments.Text, out payments);
                        newSale.noPayments = payments;

                        double paymentDue = 0;
                        Double.TryParse(txtFinancePaymentAmount.Text, NumberStyles.Currency, null, out paymentDue);
                        newSale.PaymentDue = paymentDue;

                        newSale.paymentFrequency = 0;

                        double rate = 0;
                        Double.TryParse(txtFinanceInterestRate.Text, NumberStyles.AllowDecimalPoint, null, out rate);
                        newSale.Rate = rate;

                        newSale.TotalCostofVehicle = totalBalance;
                        newSale.VehicleMilage = "0";
                        newSale.GrossCommission = 0;
                        newSale.FlatCommission = 0;
                        newSale.CurrentBalance = totalBalance;


                        double totalCost = price - tradein + adminFee + gst - deposit + warranty + warrantyGST + lienpayout + interestAmount;

                        context.Sales.AddObject(newSale);
                        context.SaveChanges();

                        Finance finance = new Finance();
                        finance.Balance = totalBalance;
                        finance.DatePaid = dtpFinanceDate.Value;
                        finance.Description = "Total Balance";
                        finance.Open = 1;
                        finance.Payment = 0;
                        finance.Addin = totalBalance;
                        finance.saleId = newSale.id;
                        finance.Short = 0;

                        Vehicle vehicle = (from v in context.Vehicles
                                           where v.Id == newSale.VehicleId
                                           select v).FirstOrDefault();

                        vehicle.InStock = 0;

                        context.Finances.AddObject(finance);
                        context.SaveChanges();
                        try
                        {
                            if (newSale.Warranty > 0)
                            {
                                String warrantyValue = Microsoft.VisualBasic.Interaction.InputBox("What is the cost of the warranty?");
                                String warrantyLength = Microsoft.VisualBasic.Interaction.InputBox("How many months does the warranty last?");

                                Cost newCost = new Cost();
                                int expense;
                                int.TryParse(warrantyValue, out expense);
                                newCost.Cost1 = expense;
                                newCost.Date = newSale.Date;
                                newCost.Description = "Warranty";
                                newCost.VIN = newSale.VehicleId;
                                newCost.WO = "0";
                                context.Costs.AddObject(newCost);
                                context.SaveChanges();
                                context.Connection.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error Saving: " + ex.Message);
                        }

                        MessageBox.Show("Financing Saved Successfully");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error Saving: " + ex.Message);
                    }

                }
            }
            else
            {
                using (DealershipManagerEntities context = new DealershipManagerEntities())
                {

                    DataRow r = ((DataRowView)cboFinances.SelectedItem).Row;

                    int id = (int)r["Id"];
                    IQueryable<Sale> saleQuery = from sale in context.Sales
                                                 where sale.id == id
                                                 select sale;

                    Sale s = saleQuery.FirstOrDefault();

                    currentSale = s;
                    try
                    {
                        currentSale.CustomerId = (int)cboFinanceCustomer.SelectedValue;
                        currentSale.VehicleId = (int)cboFinanceVehicle.SelectedValue;
                        if (cboSaleTradeInVehicle.SelectedIndex != -1)
                        {
                            currentSale.TradeInVehicle = cboFinanceTradeInVehicle.SelectedValue.ToString();
                        }
                        else
                        {
                            currentSale.TradeInVehicle = "0";
                        }


                        currentSale.SalesPerson = (int)cboFinanceSalesPerson.SelectedValue;
                        currentSale.Date = dtpFinanceDate.Value;


                        double price = 0;
                        Double.TryParse(txtFinanceSalePrice.Text, NumberStyles.Currency, null, out price);
                        currentSale.Price = price;

                        double tradein = 0;
                        Double.TryParse(txtFinanceTradeInAllowance.Text, NumberStyles.Currency, null, out tradein);
                        currentSale.TradeAllowance = tradein;

                        double adminFee = 0;
                        Double.TryParse(txtFinanceAdministrationFee.Text, NumberStyles.Currency, null, out adminFee);
                        currentSale.AdminFee = adminFee;

                        double gst = 0;
                        Double.TryParse(txtFinanceGST.Text, NumberStyles.Currency, null, out gst);
                        currentSale.GST = gst;

                        double deposit = 0;
                        Double.TryParse(txtFinanceDeposit.Text, NumberStyles.Currency, null, out deposit);
                        currentSale.Deposit = deposit;

                        double warranty = 0;
                        Double.TryParse(txtFinanceWarranty.Text, NumberStyles.Currency, null, out warranty);
                        currentSale.Warranty = warranty;

                        double warrantyGST = 0;
                        Double.TryParse(txtFinanceGSTOnWarranty.Text, NumberStyles.Currency, null, out warrantyGST);
                        currentSale.WarrantyGST = warrantyGST;

                        double lienpayout = 0;
                        Double.TryParse(txtPayoutOnLien.Text, NumberStyles.Currency, null, out lienpayout);
                        currentSale.PayoutonLien = lienpayout;

                        double totalBalance = 0;
                        Double.TryParse(txtFinanceTotalBalanceDue.Text, NumberStyles.Currency, null, out totalBalance);
                        currentSale.TotalBalance = totalBalance;

                        currentSale.Notes = txtFinanceNotes.Text;
                        currentSale.current = true;

                        // HOLY SNAP I FIGURED OUT WHAT THESE ARE FOR. This is ONLY for Financing!

                        double interestAmount = 0;
                        Double.TryParse(txtFinanceInterestAmount.Text, NumberStyles.Currency, null, out totalBalance);
                        currentSale.Interest = interestAmount;

                        int payments = 0;
                        Int32.TryParse(txtFinanceNumberofPayments.Text, out payments);
                        currentSale.noPayments = payments;

                        double paymentDue = 0;
                        Double.TryParse(txtFinancePaymentAmount.Text, NumberStyles.Currency, null, out paymentDue);
                        currentSale.PaymentDue = paymentDue;

                        currentSale.paymentFrequency = 0;

                        double rate = 0;
                        Double.TryParse(txtFinanceInterestRate.Text, NumberStyles.AllowDecimalPoint, null, out rate);
                        currentSale.Rate = rate;

                        currentSale.TotalCostofVehicle = totalBalance;
                        currentSale.VehicleMilage = "0";
                        currentSale.VehicleMilage = "0";
                        currentSale.GrossCommission = 0;
                        currentSale.FlatCommission = 0;

                        double totalCost = price - tradein + adminFee + gst - deposit + warranty + warrantyGST + lienpayout + interestAmount;

                        Vehicle vehicle = (from v in context.Vehicles
                                           where v.Id == currentSale.VehicleId
                                           select v).FirstOrDefault();

                        vehicle.InStock = 0;

                        context.SaveChanges();
                        try
                        {
                            if (currentSale.Warranty > 0)
                            {
                                String warrantyValue = Microsoft.VisualBasic.Interaction.InputBox("What is the cost of the warranty?");
                                String warrantyLength = Microsoft.VisualBasic.Interaction.InputBox("How many months does the warranty last?");

                                Cost newCost = new Cost();
                                int expense;
                                int.TryParse(warrantyValue, out expense);
                                newCost.Cost1 = expense;
                                newCost.Date = currentSale.Date;
                                newCost.Description = "Warranty";
                                newCost.VIN = currentSale.VehicleId;
                                newCost.WO = "0";
                                context.Costs.AddObject(newCost);
                                context.SaveChanges();
                                context.Connection.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error Saving: " + ex.Message);
                        }
                        MessageBox.Show("Financing Saved Successfully");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error Saving: " + ex.Message);
                    }
                }
            }
            DealForm_Load(this, null);
        }

        #region format textboxes as currency and calculate values
        private void txtSalePrice_Leave(object sender, EventArgs e)
        {
            decimal saleprice;
            decimal.TryParse(txtSalePrice.Text, NumberStyles.Currency, null, out saleprice);
            txtSalePrice.Text = saleprice.ToString("C");
            txtSaleSubtotal.Text = CalculateSubtotal("Sale").ToString("C");
            txtSaleTotalBalance.Text = CalculateTotal("Sale").ToString("C");
        }

        private void txtSaleSubtotal_Leave(object sender, EventArgs e)
        {
            decimal gst = decimal.Parse(Properties.Settings.Default["GST"].ToString()) * decimal.Parse(txtSaleSubtotal.Text, NumberStyles.Currency);
            txtSaleGST.Text = gst.ToString("C");
        }

        private void txtSaleTradeInAllowance_Leave(object sender, EventArgs e)
        {
            decimal saleTradeIn;
            decimal.TryParse(txtSaleTradeInAllowance.Text, NumberStyles.Currency, null, out saleTradeIn);
            txtSaleTradeInAllowance.Text = saleTradeIn.ToString("C");

            txtSaleSubtotal.Text = CalculateSubtotal("Sale").ToString("C");
            txtSaleTotalBalance.Text = CalculateTotal("Sale").ToString("C");
        }

        private void txtSaleAdministrationFee_Leave(object sender, EventArgs e)
        {
            decimal saleAdminFee;
            decimal.TryParse(txtSaleAdministrationFee.Text, NumberStyles.Currency, null, out saleAdminFee);
            txtSaleAdministrationFee.Text = saleAdminFee.ToString("C");

            txtSaleSubtotal.Text = CalculateSubtotal("Sale").ToString("C");
            txtSaleTotalBalance.Text = CalculateTotal("Sale").ToString("C");
        }

        private void txtSaleDeposit_Leave(object sender, EventArgs e)
        {
            decimal saleDeposit;
            decimal.TryParse(txtSaleDeposit.Text, NumberStyles.Currency, null, out saleDeposit);
            txtSaleDeposit.Text = saleDeposit.ToString("C");

            txtSaleSubtotal.Text = CalculateSubtotal("Sale").ToString("C");
            txtSaleTotalBalance.Text = CalculateTotal("Sale").ToString("C");
        }

        private void txtSaleWarranty_Leave(object sender, EventArgs e)
        {
            decimal saleWarranty;
            decimal.TryParse(txtSaleWarranty.Text, NumberStyles.Currency, null, out saleWarranty);
            txtSaleWarranty.Text = saleWarranty.ToString("C");

            decimal gst = decimal.Parse(Properties.Settings.Default["GST"].ToString()) * decimal.Parse(txtSaleWarranty.Text, NumberStyles.Currency);
            txtSaleWarrantyGST.Text = gst.ToString("C");

            txtSaleSubtotal.Text = CalculateSubtotal("Sale").ToString("C");
            txtSaleTotalBalance.Text = CalculateTotal("Sale").ToString("C");
        }

        private void txtSalePayoutOnLien_Leave(object sender, EventArgs e)
        {
            decimal salePayoutOnLien;
            decimal.TryParse(txtSalePayoutOnLien.Text, NumberStyles.Currency, null, out salePayoutOnLien);
            txtSalePayoutOnLien.Text = salePayoutOnLien.ToString("C");

            txtSaleSubtotal.Text = CalculateSubtotal("Sale").ToString("C");
            txtSaleTotalBalance.Text = CalculateTotal("Sale").ToString("C");
        }

        private void txtLeaseSalePrice_Leave(object sender, EventArgs e)
        {
            decimal saleprice;
            decimal.TryParse(txtLeaseSalePrice.Text, NumberStyles.Currency, null, out saleprice);
            txtLeaseSalePrice.Text = saleprice.ToString("C");

            txtLeaseSubtotal.Text = CalculateSubtotal("Lease").ToString("C");
        }

        private void txtLeaseTradeInAllowance_Leave(object sender, EventArgs e)
        {
            decimal tradein;
            decimal.TryParse(txtLeaseTradeInAllowance.Text, NumberStyles.Currency, null, out tradein);
            txtLeaseTradeInAllowance.Text = tradein.ToString("C");

            txtLeaseSubtotal.Text = CalculateSubtotal("Lease").ToString("C");
        }

        private void txtLeaseAdministrationFee_Leave(object sender, EventArgs e)
        {
            decimal adminFee;
            decimal.TryParse(txtLeaseAdministrationFee.Text, NumberStyles.Currency, null, out adminFee);
            txtLeaseAdministrationFee.Text = adminFee.ToString("C");

            txtLeaseSubtotal.Text = CalculateSubtotal("Lease").ToString("C");
        }

        private void txtLeaseWarranty_Leave(object sender, EventArgs e)
        {
            decimal warranty;
            decimal.TryParse(txtLeaseWarranty.Text, NumberStyles.Currency, null, out warranty);
            txtLeaseWarranty.Text = warranty.ToString("C");

            txtLeaseSubtotal.Text = CalculateSubtotal("Lease").ToString("C");
        }

        private void txtLeaseCashDownPayment_Leave(object sender, EventArgs e)
        {
            decimal downPayment;
            decimal.TryParse(txtLeaseCashDownPayment.Text, NumberStyles.Currency, null, out downPayment);
            txtLeaseCashDownPayment.Text = downPayment.ToString("C");

            decimal gst = decimal.Parse(Properties.Settings.Default["GST"].ToString()) * decimal.Parse(txtLeaseCashDownPayment.Text, NumberStyles.Currency);
            txtLeaseGSTDownPayment.Text = gst.ToString("C");

            txtLeaseSubtotal.Text = CalculateSubtotal("Lease").ToString("C");
        }

        private void txtLeaseGSTDownPayment_Leave(object sender, EventArgs e)
        {
            decimal gst;
            decimal.TryParse(txtLeaseGSTDownPayment.Text, NumberStyles.Currency, null, out gst);
            txtLeaseGSTDownPayment.Text = gst.ToString("C");

            txtLeaseSubtotal.Text = CalculateSubtotal("Lease").ToString("C");
        }

        private void txtFinanceSalePrice_Leave(object sender, EventArgs e)
        {
            decimal saleprice;
            decimal.TryParse(txtFinanceSalePrice.Text, NumberStyles.Currency, null, out saleprice);
            txtFinanceSalePrice.Text = saleprice.ToString("C");

            txtFinanceSubtotal.Text = CalculateSubtotal("Finance").ToString("C");
        }

        private void txtFinanceTradeInAllowance_Leave(object sender, EventArgs e)
        {
            decimal saleTradeIn;
            decimal.TryParse(txtFinanceTradeInAllowance.Text, NumberStyles.Currency, null, out saleTradeIn);
            txtFinanceTradeInAllowance.Text = saleTradeIn.ToString("C");

            txtFinanceSubtotal.Text = CalculateSubtotal("Finance").ToString("C");
            txtFinanceTotalBalanceDue.Text = CalculateTotal("Finance").ToString("C");
        }

        private void txtFinanceAdministrationFee_Leave(object sender, EventArgs e)
        {
            decimal saleAdminFee;
            decimal.TryParse(txtFinanceAdministrationFee.Text, NumberStyles.Currency, null, out saleAdminFee);
            txtFinanceAdministrationFee.Text = saleAdminFee.ToString("C");

            txtFinanceSubtotal.Text = CalculateSubtotal("Finance").ToString("C");
        }

        private void txtFinanceSubtotal_Leave(object sender, EventArgs e)
        {
            decimal gst = decimal.Parse(Properties.Settings.Default["GST"].ToString()) * decimal.Parse(txtFinanceSubtotal.Text, NumberStyles.Currency);
            txtFinanceGST.Text = gst.ToString("C");
        }

        private void txtFinanceWarranty_Leave(object sender, EventArgs e)
        {
            decimal warranty;
            decimal.TryParse(txtFinanceWarranty.Text, NumberStyles.Currency, null, out warranty);
            txtFinanceWarranty.Text = warranty.ToString("C");

            decimal gst = decimal.Parse(Properties.Settings.Default["GST"].ToString()) * decimal.Parse(txtFinanceWarranty.Text, NumberStyles.Currency);
            txtFinanceGSTOnWarranty.Text = gst.ToString("C");


            txtFinanceTotalBalanceDue.Text = CalculateTotal("Finance").ToString("C");
        }

        private void txtFinanceDeposit_Leave(object sender, EventArgs e)
        {
            decimal downPayment;
            decimal.TryParse(txtFinanceDeposit.Text, NumberStyles.Currency, null, out downPayment);
            txtFinanceDeposit.Text = downPayment.ToString("C");

            txtFinanceTotalBalanceDue.Text = CalculateTotal("Finance").ToString("C");
        }

        private void txtPayoutOnLien_Leave(object sender, EventArgs e)
        {
            decimal lienPayout;
            decimal.TryParse(txtPayoutOnLien.Text, NumberStyles.Currency, null, out lienPayout);
            txtPayoutOnLien.Text = lienPayout.ToString("C");

            txtFinanceTotalBalanceDue.Text = CalculateTotal("Finance").ToString("C");
        }

        private void txtFinanceInterestAmount_Leave(object sender, EventArgs e)
        {
            decimal interest;
            decimal.TryParse(txtFinanceInterestAmount.Text, NumberStyles.Currency, null, out interest);
            txtFinanceInterestAmount.Text = interest.ToString("C");

            txtFinanceTotalBalanceDue.Text = CalculateTotal("Finance").ToString("C");
        }


        private void txtFinancePaymentAmount_Leave(object sender, EventArgs e)
        {
            decimal paymentAmount;
            decimal.TryParse(txtFinancePaymentAmount.Text, NumberStyles.Currency, null, out paymentAmount);
            txtFinancePaymentAmount.Text = paymentAmount.ToString("C");
        }

        private void txtFinanceGSTOnWarranty_Leave(object sender, EventArgs e)
        {
            decimal GSTonWarranty;
            decimal.TryParse(txtFinanceGSTOnWarranty.Text, NumberStyles.Currency, null, out GSTonWarranty);
            txtFinanceTotalBalanceDue.Text = CalculateTotal("Finance").ToString("C");
        }
        #endregion

        private void cboFinances_Format(object sender, ListControlConvertEventArgs e)
        {
            DataRow r = ((DataRowView)e.ListItem).Row;
            DateTime dt = (DateTime)r["SaleDate"];
            e.Value = dt.ToShortDateString() + " - " + r["FirstName"] + " " + r["LastName"];
        }

        private void btnPayoutLease_Click(object sender, EventArgs e)
        {

        }

        private void btnFinanceDeleteSale_Click(object sender, EventArgs e)
        {
            int id = (int)cboSales.SelectedValue;
            if (MessageBox.Show("Are you sure you want to delete this Finance?", "ALERT", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (DealershipManagerEntities context = new DealershipManagerEntities())
                {
                    var sale = (from s in context.Sales
                                where s.id == id
                                select s).First();

                    context.Sales.DeleteObject(sale);
                    context.SaveChanges();
                }

            };
            foreach (Control c in this.Controls)
            {
                UnbindForm(c);
            }

            DealForm_Load(this, null);
        }

        private void btnLeaseViewReport_Click(object sender, EventArgs e)
        {
            ReportViewerForm form = new ReportViewerForm();
            form.rptviewer.LocalReport.ReportPath = "LeasePaymentReport.rdlc";
            using (DealershipManagerEntities context = new DealershipManagerEntities())
            {
                var lease = (from l in context.Leases
                             where l.Id == (int)cboLeases.SelectedValue
                             select l).FirstOrDefault();
                form.SetLeaseDataSource(lease);

            }
            form.rptviewer.Show();
            form.Show();
            form.rptviewer.RefreshReport();
            form.rptviewer.LocalReport.Refresh();
        }

        private void btnFinanceViewReport_Click(object sender, EventArgs e)
        {
            try
            {
                ReportViewerForm form = new ReportViewerForm();
                form.rptviewer.LocalReport.ReportPath = "FinanceReport.rdlc";
                using (DealershipManagerEntities context = new DealershipManagerEntities())
                {
                    var sale = (from s in context.Sales
                                where s.id == (int)cboFinances.SelectedValue
                                select s).FirstOrDefault();
                    form.SetFinanceDataSource(sale);


                }
                form.rptviewer.Show();
                form.Show();
                form.rptviewer.RefreshReport();
                form.rptviewer.LocalReport.Refresh();
            }
            catch
            {
                MessageBox.Show("Error: Cannot generate a report");
            }
        }
    }
}
