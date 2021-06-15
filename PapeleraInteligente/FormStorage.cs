using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PapeleraInteligente
{
    public partial class FormStorage : Form
    {
        DataManagement dm = new DataManagement();
        public FormStorage()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            
            DataManagement.currentStorage = storagecombobox.Text;
            changeForm();
        }

        private void changeForm()
        {
            FormInterface nextForm = new FormInterface();
            this.Hide();
            nextForm.ShowDialog();
            this.Close();
        }

        private void FormStorage_Load(object sender, EventArgs e)
        {
            storagecombobox.DataSource = dm.Storage_Selection(Int32.Parse(DataManagement.currentUser_Id));
            storagecombobox.DisplayMember = "HOUSE_ID";
            storagecombobox.ValueMember = "HOUSE_ID";
        }
    }
}
