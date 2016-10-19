using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.IO;

namespace DealershipManager2._0
{
    public partial class VehicleForm : Form
    {
        public DealForm dealHolder;
        public CustomerForm customerHolder;
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DealershipManagerConnectionString"].ToString());
        Vehicle currentVehicle;
        Specification currentSpecification;
        Feature currentFeature;
        Lien currentLien;
        List<Cost> currentCosts = new List<Cost>();
        Sale currentSaleHistory;
        bool isEdittingVehicle;
        bool isNewVehicle;


        public VehicleForm()
        {
            InitializeComponent();
        }

        public VehicleForm(Vehicle v)
        {
            currentVehicle = v;
            InitializeComponent();
            rdbAllVehicles.Checked = true;
        }

        private void VehicleForm_Load(object sender, EventArgs e)
        {
            BindVehiclesListBox();

            //var seatTypes = new BindingList<KeyValuePair<int, string>>();
            //seatTypes.Add(new KeyValuePair<int, string>(0, "N/A"));
            //seatTypes.Add(new KeyValuePair<int, string>(1, "Bucket"));
            //seatTypes.Add(new KeyValuePair<int, string>(2, "Bench"));
            //seatTypes.Add(new KeyValuePair<int, string>(3, "Split Bench"));
            //cboFrontSeat.DataSource = seatTypes;
            //cboFrontSeat.ValueMember = "Key";
            //cboFrontSeat.DisplayMember = "Value";
            //cboFrontSeat.SelectedIndex = 0;
            //cboBackSeat.DataSource = seatTypes;
            //cboBackSeat.ValueMember = "Key";
            //cboBackSeat.DisplayMember = "Value";
            //cboBackSeat.SelectedIndex = 0;

            //var driveTrains = new BindingList<KeyValuePair<int, string>>();
            //driveTrains.Add(new KeyValuePair<int, string>(0, "N/A"));
            //driveTrains.Add(new KeyValuePair<int, string>(1, "AWD"));
            //driveTrains.Add(new KeyValuePair<int, string>(2, "4WD"));
            //driveTrains.Add(new KeyValuePair<int, string>(3, "FrWD"));
            //driveTrains.Add(new KeyValuePair<int, string>(4, "RrWD"));
            //driveTrains.Add(new KeyValuePair<int, string>(5, "Belt"));
            //driveTrains.Add(new KeyValuePair<int, string>(6, "Chain"));
            //driveTrains.Add(new KeyValuePair<int, string>(7, "Shaft"));
            //cboDriveTrain.DataSource = driveTrains;
            //cboDriveTrain.ValueMember = "Key";
            //cboDriveTrain.DisplayMember = "Value";
            //cboDriveTrain.SelectedIndex = 0;

            //var transmissions = new BindingList<KeyValuePair<int, string>>();
            //driveTrains.Add(new KeyValuePair<int, string>(0, "Automatic"));
            //driveTrains.Add(new KeyValuePair<int, string>(1, "Manual"));
            //cboTransmission.DataSource = transmissions;
            //cboTransmission.ValueMember = "Key";
            //cboTransmission.DisplayMember = "Value";
            //cboTransmission.SelectedIndex = 0;

            //var fuelTypes = new BindingList<KeyValuePair<int, string>>();
            //fuelTypes.Add(new KeyValuePair<int, string>(0, "N/A"));
            //fuelTypes.Add(new KeyValuePair<int, string>(1, "Regular"));     
            //fuelTypes.Add(new KeyValuePair<int, string>(2, "Unleaded"));     
            //fuelTypes.Add(new KeyValuePair<int, string>(3, "Propane"));     
            //fuelTypes.Add(new KeyValuePair<int, string>(4, "Diesel"));
            //fuelTypes.Add(new KeyValuePair<int, string>(5, "Natural Gas"));            
            //cboFuelType.DataSource = fuelTypes;
            //cboFuelType.ValueMember = "Key";
            //cboFuelType.DisplayMember = "Value";
            //cboFuelType.SelectedIndex = 0;

            //var gears = new BindingList<KeyValuePair<int, string>>();
            //gears.Add(new KeyValuePair<int, string>(0, "N/A"));
            //gears.Add(new KeyValuePair<int, string>(1, "3 Speed"));     
            //gears.Add(new KeyValuePair<int, string>(2, "4 Speed"));     
            //gears.Add(new KeyValuePair<int, string>(3, "5 Speed"));
            //gears.Add(new KeyValuePair<int, string>(4, "6 Speed"));
            //cboGears.DataSource = gears;
            //cboGears.ValueMember = "Key";
            //cboGears.DisplayMember = "Value";
            //cboGears.SelectedIndex = 0;

            //var hitchTypes = new BindingList<KeyValuePair<int, string>>();
            //hitchTypes.Add(new KeyValuePair<int, string>(0, "None"));
            //hitchTypes.Add(new KeyValuePair<int, string>(1, "Fifth Wheel"));     
            //hitchTypes.Add(new KeyValuePair<int, string>(2, "Goose Neck"));
            //hitchTypes.Add(new KeyValuePair<int, string>(3, "Tongue"));
            //cboHitchType.DataSource = hitchTypes;
            //cboHitchType.ValueMember = "Key";
            //cboHitchType.DisplayMember = "Value";
            //cboHitchType.SelectedIndex = 0;

            //var cabs = new BindingList<KeyValuePair<int, string>>();
            //cabs.Add(new KeyValuePair<int, string>(0, "N/A"));
            //cabs.Add(new KeyValuePair<int, string>(1, "Regular"));     
            //cabs.Add(new KeyValuePair<int, string>(2, "Extended"));
            //cabs.Add(new KeyValuePair<int, string>(3, "Crew"));
            //cabs.Add(new KeyValuePair<int, string>(4, "Cab & Chassis"));
            //cboCab.DataSource = cabs;
            //cboCab.ValueMember = "Key";
            //cboCab.DisplayMember = "Value";
            //cboCab.SelectedIndex = 0;

            //var boxes = new BindingList<KeyValuePair<int, string>>();
            //boxes.Add(new KeyValuePair<int, string>(0, "N/A"));
            //boxes.Add(new KeyValuePair<int, string>(1, "Short"));     
            //boxes.Add(new KeyValuePair<int, string>(2, "Long"));
            //boxes.Add(new KeyValuePair<int, string>(3, "Step Side"));
            //boxes.Add(new KeyValuePair<int, string>(4, "None"));
            //cboBox.DataSource = boxes;
            //cboBox.ValueMember = "Key";
            //cboBox.DisplayMember = "Value";
            //cboBox.SelectedIndex = 0;

            //var tonnages = new BindingList<KeyValuePair<int, string>>();
            //tonnages.Add(new KeyValuePair<int, string>(0, "N/A"));
            //tonnages.Add(new KeyValuePair<int, string>(1, "1"));     
            //tonnages.Add(new KeyValuePair<int, string>(2, "1/2"));
            //tonnages.Add(new KeyValuePair<int, string>(3, "1/4"));
            //tonnages.Add(new KeyValuePair<int, string>(4, "2"));
            //tonnages.Add(new KeyValuePair<int, string>(1, "3"));     
            //tonnages.Add(new KeyValuePair<int, string>(2, "3/4"));
            //tonnages.Add(new KeyValuePair<int, string>(3, "5"));
            //tonnages.Add(new KeyValuePair<int, string>(4, "1 1/4"));
            //tonnages.Add(new KeyValuePair<int, string>(1, "1 1/2"));     
            //cboTonnage.DataSource = tonnages;
            //cboTonnage.ValueMember = "Key";
            //cboTonnage.DisplayMember = "Value";
            //cboTonnage.SelectedIndex = 0;

            //var axles = new BindingList<KeyValuePair<int, string>>();
            //axles.Add(new KeyValuePair<int, string>(0, "N/A"));
            //axles.Add(new KeyValuePair<int, string>(1, "Single"));     
            //axles.Add(new KeyValuePair<int, string>(2, "Dual"));   
            //cboAxle.DataSource = axles;
            //cboAxle.ValueMember = "Key";
            //cboAxle.DisplayMember = "Value";
            //cboAxle.SelectedIndex = 0;

            //var wheelTypes = new BindingList<KeyValuePair<int, string>>();
            //wheelTypes.Add(new KeyValuePair<int, string>(0, "N/A"));
            //wheelTypes.Add(new KeyValuePair<int, string>(1, "Chrome"));     
            //wheelTypes.Add(new KeyValuePair<int, string>(2, "Mag"));   
            //wheelTypes.Add(new KeyValuePair<int, string>(3, "Steel"));   
            //cboWheelType.DataSource = wheelTypes;
            //cboWheelType.ValueMember = "Key";
            //cboWheelType.DisplayMember = "Value";
            //cboWheelType.SelectedIndex = 0;

            if (currentVehicle != null)
            {
                try
                {
                    foreach (Vehicle v in lbxVehicles.Items)
                    {
                        if (v.Id == currentVehicle.Id)
                        {
                            lbxVehicles.SelectedItem = v;
                            lbxVehicles_SelectedIndexChanged(this, EventArgs.Empty);
                        }
                    }
                    SetVehicleNameLabels(currentVehicle.Year + " " + currentVehicle.Make + " " + currentVehicle.Model + " " + currentVehicle.VIN.GetLast(4) + " ");
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                }
            }

            this.Size = new Size(Parent.Size.Width * 2, Parent.Size.Height * 2);
            if (customerHolder != null)
            {
                btnReturnToCustomer.Visible = true;
            }
        }

        private void SetVehicleNameLabels(string YMMV)
        {
            lblCarNameCosts.Text = YMMV;
            lblCarNameHistory.Text = YMMV;
            lblCarNameSpecs.Text = YMMV;
        }

        private void BindVehiclesListBox()
        {
            lbxVehicles.Items.Clear();
            int type = 0;
            Vehicle cars = new Vehicle();
            Vehicle trucks = new Vehicle();
            Vehicle suvs = new Vehicle();
            Vehicle vans = new Vehicle();
            cars.Model = "------------CARS------------";
            trucks.Model = "-----------TRUCKS-----------";
            suvs.Model = "------------SUVS------------";
            vans.Model = "------------VANS------------";
            lbxVehicles.Items.Add(cars);
            if (rdbAllVehicles.Checked)
            {
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
                                    lbxVehicles.Items.Add(trucks);
                                }
                                else if (vehicle.Type == 2)
                                {
                                    type = 2;
                                    lbxVehicles.Items.Add(suvs);
                                }
                                else if (vehicle.Type == 3)
                                {
                                    type = 3;
                                    lbxVehicles.Items.Add(vans);
                                }
                                lbxVehicles.Items.Add(vehicle);
                            }
                            else
                            {
                                lbxVehicles.Items.Add(vehicle);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                    context.Connection.Close();
                }
            }
            else
            {
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
                                    lbxVehicles.Items.Add(trucks);
                                }
                                else if (vehicle.Type == 2)
                                {
                                    type = 2;
                                    lbxVehicles.Items.Add(suvs);
                                }
                                else if (vehicle.Type == 3)
                                {
                                    type = 3;
                                    lbxVehicles.Items.Add(vans);
                                }
                                lbxVehicles.Items.Add(vehicle);
                            }
                            else
                            {
                                lbxVehicles.Items.Add(vehicle);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                    context.Connection.Close();
                }
            }
        }

        private void BindCostStuff()
        {
            try
            {
                this.dgvExpenseHistory.DataSource = this.costBindingSource;
                costBindingSource.ResetBindings(false);
                currentCosts.Clear();
            }
            catch
            {
            }
            try
            {
                using (DealershipManagerEntities context = new DealershipManagerEntities())
                {
                    context.Connection.Open();
                    IQueryable<Cost> costQuery = from cost in context.Costs
                                                 where cost.VIN == currentVehicle.Id
                                                 select cost;

                    foreach (Cost c in costQuery)
                    {
                        currentCosts.Add(c);
                    }
                    context.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Costs");
            }

            try
            {
                dgvExpenseHistory.DataSource = currentCosts;
                dgvExpenseHistory.Refresh();
                BindCostInfo();
            }
            catch
            {
            }
        }

        private void lbxVehicles_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentVehicle = (Vehicle)lbxVehicles.SelectedItem;

            if (null != currentVehicle)
            {
                try
                {
                    SetVehicleNameLabels(currentVehicle.Year + " " + currentVehicle.Make + " " + currentVehicle.Model + " " + currentVehicle.VIN.GetLast(4) + " ");

                    using (DealershipManagerEntities context = new DealershipManagerEntities())
                    {
                        context.Connection.Open();
                        IQueryable<Specification> specificationQuery = from spec in context.Specifications
                                                                       where spec.VehicleId == currentVehicle.Id
                                                                       select spec;


                        Specification aSpec = specificationQuery.FirstOrDefault();
                        if (null == aSpec)
                        {
                            aSpec = new Specification();
                        }
                        currentSpecification = aSpec;
                        context.Connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Specs");
                }
                try
                {
                    using (DealershipManagerEntities context = new DealershipManagerEntities())
                    {
                        context.Connection.Open();
                        IQueryable<Feature> featureQuery = from feature in context.Features
                                                           where feature.VehicleId == currentVehicle.Id
                                                           select feature;


                        Feature aFeature = featureQuery.FirstOrDefault();
                        if (null == aFeature)
                        {
                            aFeature = new Feature();
                        }
                        currentFeature = aFeature;
                        context.Connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Features");
                }

                try
                {
                    using (DealershipManagerEntities context = new DealershipManagerEntities())
                    {
                        context.Connection.Open();
                        IQueryable<Lien> lienQuery = from lien in context.Liens
                                                     where lien.VehicleId == currentVehicle.Id
                                                     select lien;

                        Lien aLien = lienQuery.FirstOrDefault();
                        if (null == aLien)
                        {
                            aLien = new Lien();
                        }
                        else
                        {
                            dtpLienExpiryDate.Value = aLien.ExpiryDate ?? DateTime.Today;
                            txtRegistrationNumber.Text = aLien.RegNumber;
                            btnAddLien.Enabled = false;
                            btnSaveLien.Enabled = false;
                            btnEditLien.Enabled = true;
                            btnRemoveLien.Enabled = true;
                        }
                        currentLien = aLien;
                        context.Connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Liens");
                }

                //sales history
                lbxSalesHistory.Items.Clear();
                using (DealershipManagerEntities context = new DealershipManagerEntities())
                {
                    IQueryable<Sale> saleQuery = from sale in context.Sales
                                                 where sale.VehicleId == currentVehicle.Id
                                                 select sale;
                    foreach (Sale s in saleQuery)
                    {
                        lbxSalesHistory.Items.Add(s);
                    }
                }

                picImage.Image = null;
                try
                {
                    Image img = Image.FromFile(Properties.Settings.Default["ImageFilePath"].ToString() + currentVehicle.photoPath);
                    picImage.Image = img.GetThumbnailImage(picImage.Width, picImage.Height, null, IntPtr.Zero);
                    picImage.Refresh();
                    picImage.Visible = true;
                }
                catch (Exception ex)
                {
                    try
                    {
                        Image img = Image.FromFile(Properties.Settings.Default["ImageFilePath"].ToString() + "comingsoon.jpg");
                        picImage.Image = img.GetThumbnailImage(picImage.Width, picImage.Height, null, IntPtr.Zero);
                        picImage.Refresh();
                        picImage.Visible = true;
                    }
                    catch
                    {
                    }
                }

                BindCostStuff();
                BindVehicleForm();
            }
        }

        private void BindVehicleForm()
        {
            try
            {
                //Vehicle Tab
                txtVIN.Text = currentVehicle.VIN.ToString();
                txtOdometer.Text = currentSpecification.Milage.ToString();
                txtNotes.Text = currentVehicle.notes;
                if (currentSpecification.Safety == 1)
                {
                    chkSafetyInspection.Checked = true;
                }
                if (currentSpecification.FactoryWarranty == 1)
                {
                    chkFactoryWarranty.Checked = true;
                }
                if (currentSpecification.ServiceHistory == 1)
                {
                    chkServiceHistory.Checked = true;
                }
                if (currentSpecification.OneOwner == 1)
                {
                    chkOneOwner.Checked = true;
                }

                //Specifications Tab
                txtEngineSize.Text = currentSpecification.EngineSize;
                txtDoors.Text = (currentSpecification.Doors ?? 4).ToString();
                cboCylinders.SelectedItem = currentSpecification.Cylinders.ToString();
                cboDriveTrain.SelectedIndex = currentSpecification.DriveTrain ?? 0;
                cboFrontSeat.SelectedIndex = currentSpecification.SeatType ?? 0;
                cboBackSeat.SelectedIndex = currentSpecification.BackSeatType ?? 0;
                cboTransmission.SelectedIndex = currentSpecification.Transmission ?? 0;
                cboGears.SelectedIndex = currentSpecification.Gears ?? 0;
                cboFuelType.SelectedIndex = currentSpecification.FuelType ?? 0;
                cboHitchType.SelectedIndex = currentSpecification.HitchType ?? 0;

                chkWagon.Checked = currentFeature.Wagon == 1;
                chkConvertable.Checked = currentFeature.convertable == 1;
                chkHatchBack.Checked = currentFeature.hatchback == 1;

                cboCab.SelectedIndex = currentSpecification.Cab ?? 0;
                cboBox.SelectedIndex = currentSpecification.Box ?? 0;
                cboTonnage.SelectedIndex = currentSpecification.Tonnage ?? 0;
                cboAxle.SelectedIndex = currentSpecification.Axle ?? 0;
                cboWheelType.SelectedIndex = currentSpecification.WheelType ?? 0;

                chkBoxLiner.Checked = currentFeature.BoxLiner == 1;
                chkBoxCover.Checked = currentFeature.BoxCover == 1;
                chkWinch.Checked = currentFeature.Winch == 1;
                chkRunningBoards.Checked = currentFeature.runningboards == 1;
                chkTowPackage.Checked = currentSpecification.TowPackage == 1;
                ChkTopper.Checked = currentSpecification.Topper == 1;
                chkRearSlidingWindow.Checked = currentFeature.rearslidingwindow == 1;
                chkDualRearWheels.Checked = currentFeature.dualrearwheels == 1;

                /*Features
                 *
                 */
                chkPowerSteering.Checked = currentFeature.PowerSteering == 1;
                chkPowerWindows.Checked = currentFeature.PowerWindows == 1;
                chkPowerTrunk.Checked = currentFeature.PowerTrunk == 1;

                //Naviation & Aux input

                chkCDPlayer.Checked = currentFeature.CD == 1;
                chkRemoteStarter.Checked = currentFeature.RemoteStarter == 1;
                chkKeylessEntry.Checked = currentFeature.KeylessEntry == 1;
                chkAlarm.Checked = currentFeature.Alarm == 1;
                chkTractionControl.Checked = currentFeature.TractionControl == 1;
                chkABSBrakes.Checked = currentFeature.ABSBrakes == 1;
                //Leather Interior & Cloth Interior
                chkPowerMirrors.Checked = currentFeature.PowerMirrors == 1;
                chkPowerLocks.Checked = currentFeature.PowerLocks == 1;

                //Backup Camera and Sensor
                chkBlockHeater.Checked = currentFeature.BlockHeater == 1;
                chkHeatedSeats.Checked = currentFeature.HeatedSeats == 1;
                chkSunRoof.Checked = currentFeature.Sunroof == 1;

                // Power Sliding Doors
                chkRoofRack.Checked = currentFeature.RoofRack == 1;
                chkSpoiler.Checked = currentFeature.Spoilers != 0;

                chkAlloyWheels.Checked = currentFeature.AlloyWheels == 1;
                chkTurbo.Checked = currentFeature.Turbo == 1;
                chkPowerSeats.Checked = currentFeature.PowerSeats == 1;
                chkCruiseControl.Checked = currentFeature.Cruise == 1;
                chkTilt.Checked = currentFeature.Tilt == 1;
                chkAirConditioning.Checked = currentFeature.AirConditioning == 1;
                chkTintedWindows.Checked = currentFeature.TintedWindows == 1;
                chkClimateControl.Checked = currentFeature.ClimateControl == 1;

                //Car Proof Report

                chkRearWiper.Checked = currentFeature.RearWiper == 1;

                //Sport Shift
                chkDriverAirBag.Checked = currentFeature.DAirBag == 1;
                chkPassengerAirbag.Checked = currentFeature.pAirBag == 1;
                chkSideAirbag.Checked = currentFeature.SAirBag == 1;
                //End Features



                //if (currentSpecification.AlbertaActive == 1)
                //{
                //    chkAlbertaActive = true;
                //}
                txtInternetPrice.Text = currentSpecification.InternetPrice.ToString();
                txtWindowPrice.Text = currentSpecification.Windowprice.ToString();

                txtMake.Text = currentVehicle.Make;
                txtYear.Text = currentVehicle.Year.ToString();
                txtModel.Text = currentVehicle.Model;
                txtExtColour.Text = currentVehicle.Colour;
                txtSubModel.Text = currentVehicle.SubModel;
                txtIntColour.Text = currentVehicle.InteriorColor;
                chkInStock.Checked = currentVehicle.InStock == 1;


                switch (currentVehicle.Type)
                {
                    case 0:
                        rdbCar.Checked = true;
                        break;
                    case 1:
                        rdbTruck.Checked = true;
                        break;
                    case 2:
                        rdbSUV.Checked = true;
                        break;
                    case 3:
                        rdbVan.Checked = true;
                        break;
                    default:
                        break;
                }


                BindCostInfo();
            }
            catch
            {

            }

        }

        private void BindCostInfo()
        {
            //Costs Tab
            double totalExpenses = 0;
            foreach (Cost c in currentCosts)
            {
                totalExpenses += c.Cost1 ?? 0;
            }
            DateTime purchasedDate = DateTime.Today;
            CultureInfo enUS = new CultureInfo("en-US");
            if (DateTime.TryParseExact(currentVehicle.DatePurchased, "g", enUS, DateTimeStyles.None, out purchasedDate))
                dtpInventoryDate.Value = purchasedDate;
            else if ((DateTime.TryParseExact(currentVehicle.DatePurchased, "M/d/yyyy", enUS, DateTimeStyles.None, out purchasedDate)))
                dtpInventoryDate.Value = purchasedDate;
            else if ((DateTime.TryParseExact(currentVehicle.DatePurchased, "MM/d/yyyy", enUS, DateTimeStyles.None, out purchasedDate)))
                dtpInventoryDate.Value = purchasedDate;
            else if (DateTime.TryParseExact(currentVehicle.DatePurchased, "MM/d/yy", enUS, DateTimeStyles.None, out purchasedDate))
                dtpInventoryDate.Value = purchasedDate;
            else if (DateTime.TryParseExact(currentVehicle.DatePurchased, "MM-d-yyyy", enUS, DateTimeStyles.None, out purchasedDate))
                dtpInventoryDate.Value = purchasedDate;
            else if (DateTime.TryParseExact(currentVehicle.DatePurchased, "MM-d-yy", enUS, DateTimeStyles.None, out purchasedDate))
                dtpInventoryDate.Value = purchasedDate;

            txtPurchasedFrom.Text = currentVehicle.PurchasedFrom;
            txtInitialCost.Text = (currentVehicle.InitialCost ?? 0).ToString();
            txtExpenses.Text = (totalExpenses).ToString();
            txtTotalCost.Text = ((currentVehicle.InitialCost ?? 0) + (double)(totalExpenses)).ToString();
        }

        private void lbxVehicles_Format(object sender, ListControlConvertEventArgs e)
        {
            string lastvin = "";
            string year = "";
            string make = "";
            string model = "";
            try
            {
                year = ((Vehicle)e.ListItem).Year.ToString();
                make = ((Vehicle)e.ListItem).Make ?? "".ToString() ?? "";
                model = ((Vehicle)e.ListItem).Model ?? "".ToString() ?? "";
                lastvin = ((Vehicle)e.ListItem).VIN.GetLast(4);
            }
            catch
            {
            }
            finally
            {
                e.Value = year + " " + make + " " + model + " " + lastvin;
            }
        }

        private void rdbAllVehicles_CheckedChanged(object sender, EventArgs e)
        {
            lbxVehicles.Items.Clear();
            BindVehiclesListBox();
        }

        private void rdbVehiclesInStock_CheckedChanged(object sender, EventArgs e)
        {
            lbxVehicles.Items.Clear();
            BindVehiclesListBox();
        }

        public void btnNewVehicle_Click(object sender, EventArgs e)
        {
            isNewVehicle = true;
            foreach (Control c in this.Controls)
            {
                UnbindForm(c);
            }
            currentVehicle = new Vehicle();
            currentSpecification = new Specification();
            currentFeature = new Feature();
            currentCosts = new List<Cost>();
            chkInStock.Checked = true;

            BindCostStuff();
            EnableFormFields();
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
                (c as ComboBox).SelectedIndex = -1;
            }
            else if (c.GetType() == typeof(Label))
            {
                if ((c as Label).Name.ToString().Contains("lblCarName"))
                    (c as Label).Text = "";
            }
        }

        private void CreateVehicleFromForm()
        {
            currentVehicle.VIN = txtVIN.Text;
            //Vehicle Tab
            int milage = 0;
            try
            {
                int.TryParse(txtOdometer.Text, out milage);
            }
            catch { };
            currentSpecification.Milage = milage;
            currentVehicle.notes = txtNotes.Text;
            currentVehicle.photoPath = txtFileName.Text;
            currentSpecification.Safety = (chkSafetyInspection.Checked ? 1 : 0);
            currentSpecification.FactoryWarranty = (chkFactoryWarranty.Checked ? 1 : 0);
            currentSpecification.ServiceHistory = (chkServiceHistory.Checked ? 1 : 0);
            currentSpecification.OneOwner = (chkOneOwner.Checked ? 1 : 0);


            //Specifications Tab

            currentSpecification.EngineSize = txtEngineSize.Text;
            int doors = 4;
            try
            {
                int.TryParse(txtDoors.Text, out doors);
                currentSpecification.Doors = doors;
            }
            catch { };
            try
            {
                int cylinders;
                int.TryParse(cboCylinders.SelectedItem.ToString(), out cylinders);
                currentSpecification.Cylinders = cylinders;
            }
            catch
            { }

            try
            {
                currentSpecification.SeatType = cboFrontSeat.SelectedIndex;
                currentSpecification.BackSeatType = (int)cboBackSeat.SelectedIndex;
                currentSpecification.DriveTrain = (int)cboDriveTrain.SelectedIndex;

                currentSpecification.Transmission = (int)cboTransmission.SelectedIndex;
                currentSpecification.FuelType = (int)cboFuelType.SelectedIndex;
                currentSpecification.Gears = (int)cboGears.SelectedIndex;
                currentSpecification.HitchType = (int)cboHitchType.SelectedIndex;

                currentFeature.Wagon = (chkWagon.Checked ? 1 : 0);
                currentFeature.convertable = (chkConvertable.Checked ? 1 : 0);
                currentFeature.hatchback = (chkHatchBack.Checked ? 1 : 0);

                currentSpecification.Cab = (int)cboCab.SelectedIndex;
                currentSpecification.Box = (int)cboBox.SelectedIndex;
                currentSpecification.Tonnage = (int)cboTonnage.SelectedIndex;
                currentSpecification.Axle = (int)cboAxle.SelectedIndex;
                currentSpecification.WheelType = (int)cboWheelType.SelectedIndex;

                currentFeature.BoxLiner = (chkBoxLiner.Checked ? 1 : 0);
                currentFeature.BoxCover = (chkBoxCover.Checked ? 1 : 0);
                currentFeature.Winch = (chkWinch.Checked ? 1 : 0);
                currentFeature.runningboards = (chkRunningBoards.Checked ? 1 : 0);
                currentSpecification.TowPackage = (chkTowPackage.Checked ? 1 : 0);
                currentSpecification.Topper = (ChkTopper.Checked ? 1 : 0);
                currentFeature.rearslidingwindow = (chkRearSlidingWindow.Checked ? 1 : 0);
                currentFeature.dualrearwheels = (chkDualRearWheels.Checked ? 1 : 0);
            }
            catch
            {
            }
            //if (currentSpecification.Cab)

            /*Features
             *
             */
            try
            {
                currentFeature.PowerSteering = (chkPowerSteering.Checked ? 1 : 0);
                currentFeature.PowerWindows = (chkPowerWindows.Checked ? 1 : 0);
                currentFeature.PowerTrunk = (chkPowerTrunk.Checked ? 1 : 0);

                //Naviation & Aux input
                currentFeature.CD = (chkCDPlayer.Checked ? 1 : 0);
                currentFeature.RemoteStarter = (chkRemoteStarter.Checked ? 1 : 0);
                currentFeature.KeylessEntry = (chkKeylessEntry.Checked ? 1 : 0);
                currentFeature.Alarm = (chkAlarm.Checked ? 1 : 0);
                currentFeature.TractionControl = (chkTractionControl.Checked ? 1 : 0);
                currentFeature.ABSBrakes = (chkABSBrakes.Checked ? 1 : 0);

                //Leather Interior & Cloth Interior
                currentFeature.PowerMirrors = (chkPowerMirrors.Checked ? 1 : 0);
                currentFeature.PowerLocks = (chkPowerLocks.Checked ? 1 : 0);

                //Backup Camera and Sensor
                currentFeature.BlockHeater = (chkBlockHeater.Checked ? 1 : 0);
                currentFeature.HeatedSeats = (chkHeatedSeats.Checked ? 1 : 0);

                currentFeature.Sunroof = (chkSunRoof.Checked ? 1 : 0);

                // Power Sliding Doors
                currentFeature.RoofRack = (chkRoofRack.Checked ? 1 : 0);
                currentFeature.Spoilers = (chkSpoiler.Checked ? 1 : 0);
                currentFeature.AlloyWheels = (chkAlloyWheels.Checked ? 1 : 0);
                currentFeature.Turbo = (chkTurbo.Checked ? 1 : 0);
                currentFeature.PowerSeats = (chkPowerSeats.Checked ? 1 : 0);
                currentFeature.Cruise = (chkCruiseControl.Checked ? 1 : 0);
                currentFeature.Tilt = (chkTilt.Checked ? 1 : 0);
                currentFeature.AirConditioning = (chkAirConditioning.Checked ? 1 : 0);
                currentFeature.TintedWindows = (chkTintedWindows.Checked ? 1 : 0);

                currentFeature.ClimateControl = (chkClimateControl.Checked ? 1 : 0);

                //Car Proof Report
                currentFeature.RearWiper = (chkRearWiper.Checked ? 1 : 0);
                //Sport Shift
                currentFeature.DAirBag = (chkDriverAirBag.Checked ? 1 : 0);
                currentFeature.pAirBag = (chkPassengerAirbag.Checked ? 1 : 0);
                currentFeature.SAirBag = (chkSideAirbag.Checked ? 1 : 0);
                //End Features
            }
            catch
            {
            }

            int internetPrice = 0;
            try
            {
                //if (currentSpecification.AlbertaActive == 1)
                //{
                //    chkAlbertaActive = true;
                //}
                int.TryParse(txtInternetPrice.Text, out internetPrice);
            }
            catch { }
            currentSpecification.InternetPrice = internetPrice;
            int windowPrice = 0;
            try
            {

                int.TryParse(txtWindowPrice.Text, out windowPrice);
            }
            catch { };
            currentSpecification.Windowprice = windowPrice;
            currentVehicle.Make = txtMake.Text;

            int year = 0;
            try
            {
                int.TryParse(txtYear.Text, out year);
            }
            catch { };
            try
            {
                currentVehicle.Year = year;
                currentVehicle.Model = String.IsNullOrEmpty(txtModel.Text) ? " " : txtModel.Text;
                currentVehicle.Colour = String.IsNullOrEmpty(txtExtColour.Text) ? " " : txtExtColour.Text;
                currentVehicle.SubModel = String.IsNullOrEmpty(txtSubModel.Text) ? " " : txtSubModel.Text;
                currentVehicle.InteriorColor = String.IsNullOrEmpty(txtIntColour.Text) ? " " : txtIntColour.Text;

                currentVehicle.InStock = (chkInStock.Checked ? (short)1 : (short)0);

                if (rdbCar.Checked)
                {
                    currentVehicle.Type = 0;
                }
                else if (rdbTruck.Checked)
                {
                    currentVehicle.Type = 1;
                }
                else if (rdbSUV.Checked)
                {
                    currentVehicle.Type = 2;
                }
                else if (rdbVan.Checked)
                {
                    currentVehicle.Type = 3;
                }

                //Costs Tab
                double initalCost = 0;
                double.TryParse(txtInitialCost.Text, out initalCost);
                currentVehicle.InitialCost = initalCost;

                currentVehicle.DatePurchased = dtpInventoryDate.Value.ToShortDateString();
                currentVehicle.PurchasedFrom = txtPurchasedFrom.Text;
            }
            catch
            {
            }
        }

        private void BindVehicle(Vehicle v)
        {
            v.Colour = currentVehicle.Colour;
            v.Company = currentVehicle.Company;
            v.DatePurchased = currentVehicle.DatePurchased;
            v.InitialCost = currentVehicle.InitialCost;
            v.InStock = currentVehicle.InStock;
            v.InteriorColor = currentVehicle.InteriorColor;
            v.Make = currentVehicle.Make;
            v.Model = currentVehicle.Model;
            v.notes = currentVehicle.notes;
            v.photoPath = currentVehicle.photoPath;
            v.PurchasedFrom = currentVehicle.PurchasedFrom;
            v.SubModel = currentVehicle.SubModel;
            v.Type = currentVehicle.Type;
            v.VIN = currentVehicle.VIN;
            v.Year = currentVehicle.Year;
        }

        private void BindFeature(Feature f)
        {
            f.ABSBrakes = currentFeature.ABSBrakes;
            f.AirConditioning = currentFeature.AirConditioning;
            f.Alarm = currentFeature.Alarm;
            f.AlloyWheels = currentFeature.AlloyWheels;
            f.BlockHeater = currentFeature.BlockHeater;
            f.BoxCover = currentFeature.BoxCover;
            f.BoxLiner = currentFeature.BoxLiner;
            f.Cassette = currentFeature.Cassette;
            f.CD = currentFeature.CD;
            f.cdStacker = currentFeature.cdStacker;
            f.ChildProofLocks = currentFeature.ChildProofLocks;
            f.ClimateControl = currentFeature.ClimateControl;
            f.convertable = currentFeature.convertable;
            f.Cruise = currentFeature.Cruise;
            f.DAirBag = currentFeature.DAirBag;
            f.dualrearwheels = currentFeature.dualrearwheels;
            f.EngineImmobiliser = currentFeature.EngineImmobiliser;
            f.hatchback = currentFeature.hatchback;
            f.HeatedSeats = currentFeature.HeatedSeats;
            f.KeylessEntry = currentFeature.KeylessEntry;
            f.pAirBag = currentFeature.pAirBag;
            f.PowerBrakes = currentFeature.PowerBrakes;
            f.PowerLocks = currentFeature.PowerLocks;
            f.PowerMirrors = currentFeature.PowerMirrors;
            f.PowerSeats = currentFeature.PowerSeats;
            f.PowerSteering = currentFeature.PowerSteering;
            f.PowerTrunk = currentFeature.PowerTrunk;
            f.PowerWindows = currentFeature.PowerWindows;
            f.RearDefrost = currentFeature.RearDefrost;
            f.rearslidingwindow = currentFeature.rearslidingwindow;
            f.RearWiper = currentFeature.RearWiper;
            f.RemoteStarter = currentFeature.RemoteStarter;
            f.RoofRack = currentFeature.RoofRack;
            f.runningboards = currentFeature.runningboards;
            f.SAirBag = currentFeature.SAirBag;
            f.Spoilers = currentFeature.Spoilers;
            f.Stereo = currentFeature.Stereo;
            f.Sunroof = currentFeature.Sunroof;
            f.Tilt = currentFeature.Tilt;
            f.TintedWindows = currentFeature.TintedWindows;
            f.TractionControl = currentFeature.TractionControl;
            f.TRoof = currentFeature.TRoof;
            f.Turbo = currentFeature.Turbo;
            f.Wagon = currentFeature.Wagon;
            f.Winch = currentFeature.Winch;
        }

        private void BindSpecification(Specification s)
        {
            s.Axle = currentSpecification.Axle;
            s.BackSeatType = currentSpecification.BackSeatType;
            s.Box = currentSpecification.Box;
            s.Cab = currentSpecification.Cab;
            s.Cylinders = currentSpecification.Cylinders;
            s.DateIn = currentSpecification.DateIn;
            s.Doors = currentSpecification.Doors;
            s.DriveTrain = currentSpecification.DriveTrain;
            s.EngineSize = currentSpecification.EngineSize;
            s.FactoryWarranty = currentSpecification.FactoryWarranty;
            s.FuelType = currentSpecification.FuelType;
            s.Gears = currentSpecification.Gears;
            s.HitchType = currentSpecification.HitchType;
            s.InternetPrice = currentSpecification.InternetPrice;
            s.Milage = currentSpecification.Milage;
            s.OneOwner = currentSpecification.OneOwner;
            s.Safety = currentSpecification.Safety;
            s.SeatType = currentSpecification.SeatType;
            s.ServiceHistory = currentSpecification.ServiceHistory;
            s.Tonnage = currentSpecification.Tonnage;
            s.Topper = currentSpecification.Topper;
            s.TowPackage = currentSpecification.TowPackage;
            s.Transmission = currentSpecification.Transmission;
            s.WheelType = currentSpecification.WheelType;
            s.Windowprice = currentSpecification.Windowprice;
        }

        private void BindLien(Lien l)
        {
            l.VehicleId = currentVehicle.Id;
            l.ExpiryDate = dtpLienExpiryDate.Value;
            l.RegNumber = txtRegistrationNumber.Text;
        }

        private void btnSaveVehicle_Click(object sender, EventArgs e)
        {
            CreateVehicleFromForm();
            using (DealershipManagerEntities context = new DealershipManagerEntities())
            {
                context.Connection.Open();

                if (isNewVehicle)
                {
                    try
                    {
                        context.Vehicles.AddObject(currentVehicle);
                        context.SaveChanges();
                        var vehicle = (from v in context.Vehicles
                                       where v.VIN == currentVehicle.VIN
                                       select v).First();
                        currentSpecification.VehicleId = vehicle.Id;
                        currentFeature.VehicleId = vehicle.Id;
                        context.Specifications.AddObject(currentSpecification);
                        context.Features.AddObject(currentFeature);
                    }
                    catch
                    {

                    }                       
                    finally
                    {
                        try
                        {
                            context.SaveChanges();
                            context.Connection.Close();
                            MessageBox.Show("Vehicle Saved Successfully");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Vehicle did not save successfully");
                            string test = ex.Message;
                        }
                    }
                }
                else if (isEdittingVehicle)
                {
                    try
                    {
                        var vehicle = (from v in context.Vehicles
                                       where v.Id == currentVehicle.Id
                                       select v).First();

                        var feature = (from f in context.Features
                                       where f.VehicleId == currentVehicle.Id
                                       select f).First();

                        var specification = (from s in context.Specifications
                                             where s.VehicleId == currentVehicle.Id
                                             select s).First();

                        if (vehicle != null)
                        {
                            BindVehicle(vehicle);
                        }
                        if (feature != null)
                        {
                            BindFeature(feature);
                        }
                        if (specification != null)
                        {
                            BindSpecification(specification);
                        }
                    }
                    catch
                    {
                        try
                        {
                            context.Vehicles.AddObject(currentVehicle);
                            context.SaveChanges();
                            var vehicle = (from v in context.Vehicles
                                           where v.VIN == currentVehicle.VIN
                                           select v).First();
                            currentSpecification.VehicleId = vehicle.Id;
                            currentFeature.VehicleId = vehicle.Id;
                        }
                        catch
                        {

                        }
                        context.Specifications.AddObject(currentSpecification);
                        context.Features.AddObject(currentFeature);
                    }
                    finally
                    {
                        try
                        {
                            context.SaveChanges();
                            context.Connection.Close();
                            MessageBox.Show("Vehicle Saved Successfully");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Vehicle did not save successfully");
                            string test = ex.Message;
                        }
                    }
                }
            }
            BindVehiclesListBox();
            lbxVehicles.SelectedItem = currentVehicle;
            DisableFormFields();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            bool car = chkSearchCar.Checked;
            bool truck = chkSearchTruck.Checked;
            bool van = chkSearchVan.Checked;
            bool SUV = chkSearchSUV.Checked;

            int type = 0;


            bool inInventory = rdbSearchAvailableInventory.Checked;

            string make = txtSearchMake.Text;
            string model = txtSearchModel.Text;
            //string doors = cboSearchDoors.SelectedValue.ToString();
            //string transmission = cboSearchTransmission.SelectedValue.ToString();
            //string cab = cboSearchCab.SelectedValue.ToString();
            //string box = cboSearchBox.SelectedValue.ToString();

            string year1 = txtSearchYearsStart.Text;
            string year2 = txtSearchYearsEnd.Text;

            string price1 = txtSearchPriceFrom.Text;
            string price2 = txtSearchPriceTo.Text;

            using (DealershipManagerEntities context = new DealershipManagerEntities())
            {
                context.Connection.Open();
                IQueryable<Vehicle> vehicleQuery = from vehicle in context.Vehicles
                                                   orderby vehicle.Year
                                                   select vehicle;

                lbxVehicles.Items.Clear();
                foreach (Vehicle v in vehicleQuery)
                {
                    lbxVehicles.Items.Add(v);
                }

                context.Connection.Close();
            }
            tabControlVehicleForm.SelectedTab = tabControlVehicleForm.TabPages["Vehicle"];

        }

        private void btnEditVehicle_Click(object sender, EventArgs e)
        {
            EnableFormFields();
            isEdittingVehicle = true;
        }

        private void EnableFormFields()
        {
            txtVIN.Enabled = true;
            txtOdometer.Enabled = true;
            rdbCar.Enabled = true;
            rdbVan.Enabled = true;
            rdbTruck.Enabled = true;
            rdbSUV.Enabled = true;
            txtMake.Enabled = true;
            txtModel.Enabled = true;
            txtSubModel.Enabled = true;
            txtYear.Enabled = true;
            txtExtColour.Enabled = true;
            txtIntColour.Enabled = true;
            chkSafetyInspection.Enabled = true;
            chkFactoryWarranty.Enabled = true;
            chkServiceHistory.Enabled = true;
            chkOneOwner.Enabled = true;
            chkAlbertaActive.Enabled = true;
            txtWindowPrice.Enabled = true;
            txtInternetPrice.Enabled = true;
            chkInStock.Enabled = true;
            txtNotes.Enabled = true;
        }

        private void DisableFormFields()
        {
            txtVIN.Enabled = false;
            txtOdometer.Enabled = false;
            rdbCar.Enabled = false;
            rdbVan.Enabled = false;
            rdbTruck.Enabled = false;
            rdbSUV.Enabled = false;
            txtMake.Enabled = false;
            txtModel.Enabled = false;
            txtSubModel.Enabled = false;
            txtYear.Enabled = false;
            txtExtColour.Enabled = false;
            txtIntColour.Enabled = false;
            chkSafetyInspection.Enabled = false;
            chkFactoryWarranty.Enabled = false;
            chkServiceHistory.Enabled = false;
            chkOneOwner.Enabled = false;
            chkAlbertaActive.Enabled = false;
            txtWindowPrice.Enabled = false;
            txtInternetPrice.Enabled = false;
            chkInStock.Enabled = false;
            txtNotes.Enabled = false;
            isEdittingVehicle = false;
            isNewVehicle = false;
        }

        private void btnVehicleCancel_Click(object sender, EventArgs e)
        {
            DisableFormFields();

            BindVehicleForm();
        }

        private void btnAddLien_Click(object sender, EventArgs e)
        {
            btnSaveLien.Enabled = true;
            btnAddLien.Enabled = false;
            dtpLienExpiryDate.Enabled = true;
            txtRegistrationNumber.Enabled = true;
        }

        private void btnSaveLien_Click(object sender, EventArgs e)
        {
            try
            {
                using (DealershipManagerEntities context = new DealershipManagerEntities())
                {
                    context.Connection.Open();
                    IQueryable<Lien> lienQuery = from lien in context.Liens
                                                 where lien.ID == currentLien.ID
                                                 select lien;


                    Lien aLien = lienQuery.FirstOrDefault();
                    if (null == aLien)
                    {
                        aLien = new Lien();
                        BindLien(aLien);
                        context.Liens.AddObject(aLien);
                    }
                    else
                    {
                        BindLien(aLien);
                    }
                    context.SaveChanges();
                    context.Connection.Close();
                }
                MessageBox.Show("Lien was Saved Successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnEditLien_Click(object sender, EventArgs e)
        {
            btnSaveLien.Enabled = true;
            btnAddLien.Enabled = false;
            dtpLienExpiryDate.Enabled = true;
            txtRegistrationNumber.Enabled = true;
        }

        private void btnRemoveLien_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to remove this Lien?", "ALERT", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (DealershipManagerEntities context = new DealershipManagerEntities())
                    {
                        var lien = (from l in context.Liens
                                    where l.ID == currentLien.ID
                                    select l).First();

                        context.Liens.DeleteObject(lien);
                        context.SaveChanges();
                    }
                }
                btnEditLien.Enabled = false;
                btnRemoveLien.Enabled = false;
                btnAddLien.Enabled = true;
                btnSaveLien.Enabled = false;
                dtpLienExpiryDate.Enabled = false;
                txtRegistrationNumber.Enabled = false;
                txtRegistrationNumber.Text = "";
                dtpLienExpiryDate.Value = DateTime.Today;
            }
            catch (Exception)
            {
            }
        }

        private void lbxSalesHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                currentSaleHistory = (Sale)lbxSalesHistory.SelectedItem;


                using (DealershipManagerEntities context = new DealershipManagerEntities())
                {
                    IQueryable<Customer> customerQuery = from customer in context.Customers
                                                         where customer.Id == currentSaleHistory.CustomerId
                                                         select customer;

                    Customer cust = customerQuery.FirstOrDefault();
                    if (null == cust)
                    {
                        cust = new Customer();
                    }
                    txtFirstName.Text = cust.FirstName;
                    txtLastName.Text = cust.LastName;
                    txtHomePhone.Text = cust.HomePhone;
                    txtWorkPhone.Text = cust.BusPhone;
                    txtOtherPhone.Text = cust.OtherPhone;
                    btnViewCustomer.Enabled = true;
                }
            }
            catch
            {

            }
        }

        private void btnViewCustomer_Click(object sender, EventArgs e)
        {
            using (DealershipManagerEntities context = new DealershipManagerEntities())
            {
                IQueryable<Customer> customerQuery = from customer in context.Customers
                                                     where customer.Id == currentSaleHistory.CustomerId
                                                     select customer;

                Customer cust = customerQuery.FirstOrDefault();
                CustomerForm customerForm = new CustomerForm(cust);
                customerForm.MdiParent = this.MdiParent;
                customerForm.Show();
            }
        }

        private void lbxSalesHistory_Format(object sender, ListControlConvertEventArgs e)
        {
            string customerInfo = "";
            string date = ((Sale)e.ListItem).Date.ToShortDateString();
            int id = ((Sale)e.ListItem).CustomerId;
            using (DealershipManagerEntities context = new DealershipManagerEntities())
            {
                IQueryable<Customer> customerQuery = from customer in context.Customers
                                                     where customer.Id == id
                                                     select customer;

                Customer cust = customerQuery.FirstOrDefault();
                if (null == cust)
                {
                    cust = new Customer();
                }

                customerInfo = date + " - " + cust.FirstName + " " + cust.LastName;
            }

            e.Value = customerInfo;
        }

        private void btnReturnToStock_Click(object sender, EventArgs e)
        {
            CreateVehicleFromForm();
            using (DealershipManagerEntities context = new DealershipManagerEntities())
            {

                if (isNewVehicle)
                {
                    try
                    {
                        context.Vehicles.AddObject(currentVehicle);
                        context.SaveChanges();
                        var vehicle = (from v in context.Vehicles
                                       where v.VIN == currentVehicle.VIN
                                       select v).First();
                        currentSpecification.VehicleId = vehicle.Id;
                        currentFeature.VehicleId = vehicle.Id;
                        vehicle.InStock = 1;
                        context.Specifications.AddObject(currentSpecification);
                        context.Features.AddObject(currentFeature);
                    }
                    catch
                    {

                    }
                    finally
                    {
                        try
                        {
                            context.SaveChanges();
                            context.Connection.Close();
                            MessageBox.Show("Vehicle Saved Successfully");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Vehicle did not save successfully");
                            string test = ex.Message;
                        }
                    }
                }
                else if (isEdittingVehicle)
                {
                    try
                    {
                        var vehicle = (from v in context.Vehicles
                                       where v.Id == currentVehicle.Id
                                       select v).First();

                        vehicle.InStock = 1;

                        var feature = (from f in context.Features
                                       where f.VehicleId == currentVehicle.Id
                                       select f).First();

                        var specification = (from s in context.Specifications
                                             where s.VehicleId == currentVehicle.Id
                                             select s).First();

                        if (vehicle != null)
                        {
                            BindVehicle(vehicle);
                        }
                        if (feature != null)
                        {
                            BindFeature(feature);
                        }
                        if (specification != null)
                        {
                            BindSpecification(specification);
                        }
                    }
                    catch
                    {
                        try
                        {
                            context.Vehicles.AddObject(currentVehicle);
                            context.SaveChanges();
                            var vehicle = (from v in context.Vehicles
                                           where v.VIN == currentVehicle.VIN
                                           select v).First();
                            currentSpecification.VehicleId = vehicle.Id;
                            currentFeature.VehicleId = vehicle.Id;
                        }
                        catch
                        {

                        }
                        context.Specifications.AddObject(currentSpecification);
                        context.Features.AddObject(currentFeature);
                    }
                    finally
                    {
                        try
                        {
                            context.SaveChanges();
                            context.Connection.Close();
                            MessageBox.Show("Vehicle Saved Successfully");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Vehicle did not save successfully");
                            string test = ex.Message;
                        }
                    }
                }
                //normal start
                try
                {
                    var vehicle = (from v in context.Vehicles
                                   where v.VIN == currentVehicle.VIN
                                   select v).First();

                    vehicle.InStock = 1;
                    if (MessageBox.Show("Would you like to remove previous costs from this vehicle?", "ALERT", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {

                        IQueryable<Cost> costQuery = from cost in context.Costs
                                                     where cost.VIN == vehicle.Id 
                                                     select cost;

                        foreach (Cost c in costQuery)
                        {
                            context.DeleteObject(c);
                        }
                    }
                }
                catch
                {
                }
                finally
                {
                    context.SaveChanges();
                    context.Connection.Close();
                }
                //}
                //

                
                
            }

            DisableFormFields();
        }

        private void btnSaveExpense_Click(object sender, EventArgs e)
        {

            try
            {
                Cost newCost = new Cost();
                int expense;
                int.TryParse(txtExpenseCost.Text.ToString(), out expense);
                newCost.Cost1 = expense;
                newCost.Date = dtpExpense.Value;
                newCost.Description = txtExpenseDescription.Text;
                newCost.VIN = currentVehicle.Id;
                newCost.WO = txtExpenseWorkOrder.Text;
                using (DealershipManagerEntities context = new DealershipManagerEntities())
                {
                    context.Costs.AddObject(newCost);

                    context.SaveChanges();
                    context.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                string test = ex.Message;
            }
            finally
            {
                BindCostStuff();
            }

            dtpExpense.Value = DateTime.Today;
            txtExpenseCost.Text = "";
            txtExpenseDescription.Text = "";
            txtExpenseWorkOrder.Text = "";
        }

        private void btnReturnToCustomer_Click(object sender, EventArgs e)
        {
            customerHolder.Show();

        }

        private void btnAddPhoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.CheckFileExists = true;
            openFileDialog.AddExtension = true;
            openFileDialog.Multiselect = false;
            string filepath = Properties.Settings.Default["ImageFilePath"].ToString();
            //openFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
            try
            {
                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    foreach (string fileName in openFileDialog.FileNames)
                    {
                        File.Copy(fileName, filepath + Path.GetFileName(fileName));
                        txtFileName.Text = Path.GetFileName(fileName);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Error saving image to server");
            }
        }
    }
}

public static class StringExtension
{
    public static string GetLast(this string source, int tail_length)
    {
        if (tail_length >= source.Length)
            return source;
        return source.Substring(source.Length - tail_length);
    }
}