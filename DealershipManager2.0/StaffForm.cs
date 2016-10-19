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
    public partial class StaffForm : Form
    {
        Staff currentStaff;
        public StaffForm()
        {
            InitializeComponent();
        }

        private void StaffForm_Load(object sender, EventArgs e)
        {
            BindStaffListBox();
            cboProvince.SelectedIndex = 1;
            cboJob.SelectedIndex = 1;
            
            this.Size = new Size(Parent.Size.Width * 2, Parent.Size.Height * 2);
        }

        private void lbxStaff_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentStaff = (Staff)lbxStaff.SelectedItem;
            if (null != currentStaff)
            {
                BindStaffToForm();
            }
        }

        private void BindStaffListBox()
        {
            lbxStaff.Items.Clear();
            
            using (DealershipManagerEntities context = new DealershipManagerEntities())
            {
                List<Staff> staffQuery = (from staff in context.Staffs
                                             select staff).ToList<Staff>();

                staffQuery = staffQuery.OrderBy(x => x.LastName).ToList();
                try
                {
                    foreach (var staff in staffQuery)
                    {
                        lbxStaff.Items.Add(staff);
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void BindStaffToForm()
        {
            txtFirstName.Text = currentStaff.FirstName;
            txtLastName.Text = currentStaff.LastName;
            txtSIN.Text = (currentStaff.SIN ?? 0).ToString();
            txtAddress.Text = currentStaff.Street;
            txtCity.Text = currentStaff.City;
            if (currentStaff.Province != null)
            {
                cboProvince.SelectedValue = currentStaff.Province;
            }
            txtPostalCode.Text = currentStaff.Postal;
            txtHomePhone.Text = currentStaff.HomePhone;
            dtpBirthday.Value = currentStaff.Birthdate ?? DateTime.Now; txtOtherPhone.Text = currentStaff.OtherPhone;
            txtEmail.Text = currentStaff.Email;

            if (currentStaff.Job != null)
            {
                cboJob.SelectedValue = currentStaff.Job;
            }
            txtCommission.Text = (currentStaff.Commission ?? 0).ToString();
        }

        private void EnableStaffFields()
        {
            txtFirstName.Enabled = true;
            txtLastName.Enabled = true;
            txtSIN.Enabled = true;
            txtAddress.Enabled = true;
            txtCity.Enabled = true;
            cboProvince.Enabled = true;
            txtPostalCode.Enabled = true;
            txtHomePhone.Enabled = true;
            txtOtherPhone.Enabled = true;
            txtEmail.Enabled = true;
            dtpBirthday.Enabled = true;
            cboJob.Enabled = true;
        }
        private void DisableStaffFields()
        {
            txtFirstName.Enabled = false;
            txtLastName.Enabled = false;
            txtSIN.Enabled = false;
            txtAddress.Enabled = false;
            txtCity.Enabled = false;
            cboProvince.Enabled = false;
            txtPostalCode.Enabled = false;
            txtHomePhone.Enabled = false;
            txtOtherPhone.Enabled = false;
            txtEmail.Enabled = false;
            dtpBirthday.Enabled = false;
            cboJob.Enabled = false;
        }

        private void lbxStaff_Format(object sender, ListControlConvertEventArgs e)
        {
            string firstName = ((Staff)e.ListItem).FirstName.ToString();
            string lastName = ((Staff)e.ListItem).LastName.ToString();

            e.Value = lastName + ", " + firstName;
        }

        private void btnAddNewStaff_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.Controls)
            {
                UnbindForm(c);
            }
            currentStaff = new Staff();
            lbxStaff.Enabled = false;
            EnableStaffFields();
            txtFirstName.Focus();
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

        private void btnStaffCancel_Click(object sender, EventArgs e)
        {
            lbxStaff.Enabled = true;
            DisableStaffFields();
            BindStaffToForm();
        }

        private void btnEditStaff_Click(object sender, EventArgs e)
        {
            lbxStaff.Enabled = false;
            EnableStaffFields();
        }

        private void btnDeleteStaff_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this Staff Member?", "ALERT", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (DealershipManagerEntities context = new DealershipManagerEntities())
                {
                    var staff = (from s in context.Staffs
                                    where s.Id == currentStaff.Id
                                    select s).First();

                    context.Staffs.DeleteObject(staff);
                    context.SaveChanges();
                }

            };
            BindStaffListBox();
            DisableStaffFields();
        }

        private void CreateStaffFromForm()
        {
            currentStaff.Birthdate = dtpBirthday.Value;
            currentStaff.City = txtCity.Text;            
            currentStaff.Email = txtEmail.Text;
            currentStaff.FirstName = txtFirstName.Text;
            currentStaff.HomePhone = txtHomePhone.Text;
            currentStaff.Job = cboJob.SelectedIndex;
            currentStaff.LastName = txtLastName.Text;
            currentStaff.OtherPhone = txtOtherPhone.Text;
            currentStaff.Postal = txtPostalCode.Text;
            currentStaff.Province = cboProvince.SelectedItem.ToString();
            int sin = 0;
            int.TryParse(txtSIN.Text, out sin);
            currentStaff.SIN = sin;
            currentStaff.Street = txtAddress.Text;
        }

        private void BindStaff(Staff s)
        {
            s.FirstName = txtFirstName.Text;
            s.LastName = txtLastName.Text;
            int sin = 0;
            int.TryParse(txtSIN.Text, out sin);
            s.SIN = sin;
            s.Street = txtAddress.Text;
            s.City = txtCity.Text;
            s.Province = cboProvince.SelectedText;
            s.Postal = txtPostalCode.Text;
            s.HomePhone = txtHomePhone.Text;
            s.OtherPhone = txtOtherPhone.Text;
            s.Email = txtEmail.Text;
            s.Birthdate = dtpBirthday.Value;
            //s.Job = cboJob.SelectedValue;            
        }

        private void btnSaveStaff_Click(object sender, EventArgs e)
        {
            CreateStaffFromForm();
            using (DealershipManagerEntities context = new DealershipManagerEntities())
            {
                try
                {
                    var staff = (from s in context.Staffs
                                   where (s.FirstName == currentStaff.FirstName && s.LastName == currentStaff.LastName)
                                   select s).First();

                    if (staff != null)
                    {
                        BindStaff(staff);
                    }
                }
                catch
                {
                    context.Staffs.AddObject(currentStaff);
                }
                finally
                {
                    context.SaveChanges();
                }

                BindStaffListBox();
                lbxStaff.SelectedItem = currentStaff;
                lbxStaff.Enabled = true;
                DisableStaffFields();                
            }
        }
    }
}
