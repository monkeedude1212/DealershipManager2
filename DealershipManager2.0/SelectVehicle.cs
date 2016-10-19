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
    public partial class SelectVehicle : Form
    {
        List<Vehicle> vehicleHistory;
        Insurance currentInsurance;
        public CustomerForm customerForm;
        public SelectVehicle()
        {
            InitializeComponent();
        }

        public SelectVehicle(Customer c, Insurance i)
        {
            InitializeComponent();
            currentInsurance = i;
            vehicleHistory = new List<Vehicle>();
            using (DealershipManagerEntities context = new DealershipManagerEntities())
            {
                IQueryable<Sale> saleQuery = from sale in context.Sales
                                             where (sale.CustomerId == c.Id)
                                             select sale;
                IQueryable<Lease> leaseQuery = from lease in context.Leases
                                               where (lease.CustomerId == c.Id)
                                               select lease;

                foreach (Sale s in saleQuery)
                {
                    Vehicle veh = (from vehicle in context.Vehicles
                                   where (vehicle.Id == s.VehicleId)
                                   select vehicle).FirstOrDefault();
                    vehicleHistory.Add(veh);
                }

                foreach (Lease l in leaseQuery)
                {
                    Vehicle veh = (from vehicle in context.Vehicles
                                   where (vehicle.Id == l.VehicleId)
                                   select vehicle).FirstOrDefault();
                    vehicleHistory.Add(veh);
                }

                foreach (Vehicle v in vehicleHistory)
                {
                    lbxVehicleHistory.Items.Add(v);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddVehicle_Click(object sender, EventArgs e)
        {
            InsuranceVehicle newInsuranceVehicle = new InsuranceVehicle();
            newInsuranceVehicle.VehicleId = (lbxVehicleHistory.SelectedItem as Vehicle).Id;
            newInsuranceVehicle.insuranceid = currentInsurance.id;
            customerForm.allInsuranceVehicles.Add(newInsuranceVehicle);
            customerForm.BindInsuranceVehiclesToForm();
            customerForm.Refresh();
            this.Close();
        }

        private void lbxVehicleHistory_Format(object sender, ListControlConvertEventArgs e)
        {
            string year = ((Vehicle)e.ListItem).Year.ToString();
            string make = ((Vehicle)e.ListItem).Make ?? "".ToString() ?? "";
            string model = ((Vehicle)e.ListItem).Model ?? "".ToString() ?? "";

            e.Value = year + " " + make + " " + model;
        }

        private void lbxVehicleHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnAddVehicle.Enabled = true;
        }
    }
}
