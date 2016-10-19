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
    public partial class ContactsForm : Form
    {
        Contact currentContact = new Contact();
        public ContactsForm()
        {
            InitializeComponent();
        }

        private void ContactsForm_Load(object sender, EventArgs e)
        {            
            // TODO: This line of code loads data into the 'dealershipManagerDataSet.Contacts' table. You can move, or remove it, as needed.
            this.contactsTableAdapter.Fill(this.dealershipManagerDataSet.Contacts);
            this.Size = new Size(Parent.Size.Width * 2, Parent.Size.Height * 2);

            BindContactsListBox();
        }

        private void lbxContacts_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentContact = (Contact)lbxContacts.SelectedItem;
            if (null != currentContact)
            {
                BindContactToForm();
            }
        }

        private void BindContactToForm()
        {
            txtCompanyName.Text = currentContact.CompanyName;

            txtFirstName.Text = currentContact.FirstName;
            txtLastName.Text = currentContact.LastName;
            txtStreetAddress.Text = currentContact.Address;
            txtPhone.Text = currentContact.Phone;
            txtCity.Text = currentContact.City;

            txtFax.Text = currentContact.Fax;

            if (currentContact.Province != "")
            {
                cboProvince.SelectedItem = currentContact.Province;
            }
            else
            {
                cboProvince.SelectedIndex = -1;
            }
            if (currentContact.Country != "")
            {
                cboCountry.SelectedItem = currentContact.Country;
            }
            else
            {
                cboCountry.SelectedIndex = -1;
            }

            txtPostalCode.Text = currentContact.Postal;
            txtEmail.Text = currentContact.Email;
            cboCountry.SelectedItem = currentContact.Country;
            txtNotes.Text = currentContact.notes;
        }

        private void BindContactsListBox()
        {
            lbxContacts.Items.Clear();
            using (DealershipManagerEntities context = new DealershipManagerEntities())
            {
                List<Contact> contactQuery = (from contact in context.Contacts
                                              select contact).ToList<Contact>();

                contactQuery = contactQuery.OrderBy(x => x.CompanyName).ToList();
                try
                {
                    foreach (var contact in contactQuery)
                    {
                        lbxContacts.Items.Add(contact);
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void CreateContactFromForm()
        {
            currentContact.CompanyName = txtCompanyName.Text;
            currentContact.FirstName = txtFirstName.Text;
            currentContact.LastName = txtLastName.Text;
            currentContact.Address = txtStreetAddress.Text;
            currentContact.Phone = txtPhone.Text;
            currentContact.City = txtCity.Text;
            currentContact.Fax = txtFax.Text;
            currentContact.Province = cboProvince.SelectedText;
            currentContact.Postal = txtPostalCode.Text;
            currentContact.Email = txtEmail.Text;
            currentContact.Country = cboCountry.SelectedText;
            currentContact.notes = txtNotes.Text;
        }

        private void lbxContacts_Format(object sender, ListControlConvertEventArgs e)
        {
            string firstName = ((Contact)e.ListItem).FirstName.ToString();
            string lastName = ((Contact)e.ListItem).LastName.ToString();
            string companyName = ((Contact)e.ListItem).CompanyName.ToString();

            e.Value = companyName;
        }

        private void DisableContactFields()
        {
            txtCompanyName.Enabled = false;
            txtFirstName.Enabled = false;
            txtLastName.Enabled = false;
            txtNotes.Enabled = false;
            txtPhone.Enabled = false;
            txtFax.Enabled = false;
            txtEmail.Enabled = false;
            txtStreetAddress.Enabled = false;
            txtCity.Enabled = false;
            txtPostalCode.Enabled = false;
            cboProvince.Enabled = false;
            cboCountry.Enabled = false;

        }

        private void EnableContactFields()
        {
            txtCompanyName.Enabled = true;
            txtFirstName.Enabled = true;
            txtLastName.Enabled = true;
            txtNotes.Enabled = true;
            txtPhone.Enabled = true;
            txtFax.Enabled = true;
            txtEmail.Enabled = true;
            txtStreetAddress.Enabled = true;
            txtCity.Enabled = true;
            txtPostalCode.Enabled = true;
            cboProvince.Enabled = true;
            cboCountry.Enabled = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.Controls)
            {
                UnbindForm(c);
            }

            currentContact = new Contact();
            EnableContactFields();
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

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EnableContactFields();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this contact?", "ALERT", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (DealershipManagerEntities context = new DealershipManagerEntities())
                {
                    var contact = (from c in context.Contacts
                                   where c.Id == currentContact.Id
                                   select c).First();

                    context.Contacts.DeleteObject(contact);
                    context.SaveChanges();
                }

            };
            BindContactsListBox();
            DisableContactFields();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            BindContactToForm();
            DisableContactFields();
        }

        private void bindContact(Contact c)
        {
            c.Address = currentContact.Address;
            c.City = currentContact.City;
            c.CompanyName = currentContact.CompanyName;
            c.Country = currentContact.Country;
            c.Email = currentContact.Email;
            c.Fax = currentContact.Fax;
            c.FirstName = currentContact.FirstName;
            c.LastName = currentContact.LastName;
            c.notes = currentContact.notes;
            c.Phone = currentContact.Phone;
            c.Postal = currentContact.Postal;
            c.Province = currentContact.Province;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //CreateContactFromForm();
            //using (DealershipManagerEntities context = new DealershipManagerEntities())
            //{
            //    var contact = (from c in context.Contacts
            //                    where c.Id == currentContact.Id
            //                    select c).First();

            //    context.SaveChanges();
            //}          
            CreateContactFromForm();
            using (DealershipManagerEntities context = new DealershipManagerEntities())
            {
                try
                {
                    var contact = (from c in context.Contacts
                                   where c.CompanyName == currentContact.CompanyName
                                   select c).First();

                    if (contact != null)
                    {
                        bindContact(contact);
                    }
                }
                catch
                {
                    context.Contacts.AddObject(currentContact);
                }
                finally
                {
                    context.SaveChanges();
                }

                BindContactsListBox();
                lbxContacts.SelectedItem = currentContact;
                DisableContactFields();
            }
        }
    }
}
