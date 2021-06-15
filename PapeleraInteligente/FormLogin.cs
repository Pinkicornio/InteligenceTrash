using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PapeleraInteligente
{
    public partial class FormLogin : Form
    {
        private DataManagement datamanagement = new DataManagement();
        public FormLogin()
        {
            InitializeComponent();
            conectionDb();   
        }

        private void conectionDb()
        {
            datamanagement.startConnection();
        }

        private void changeForm()
        {

            string userInput = userfield.Text;
            string pwdInput = passwordfield.Text;


            if (!string.IsNullOrWhiteSpace(userInput) && !string.IsNullOrWhiteSpace(pwdInput))
            {
                try
                {
                    List<string> datainfo = datamanagement.getLoginInfo(userInput);

                    if (datainfo[1].Equals(datamanagement.hashpwd(pwdInput, datainfo[2])))
                    {
                        if (datainfo[3].Equals("APP"))
                        {
                            DataManagement.currentUser_Id = datainfo[4];
                            DataManagement.currentUser = userInput;
                            FormStorage nextForm = new FormStorage();
                            this.Hide();
                            nextForm.ShowDialog();
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("WRONG PASSWORD");
                    }
                }
                catch
                {
                    MessageBox.Show("LOG FAILED, USERNAME NOT EXIST");
                }
            }
            else MessageBox.Show("ALL FIELDS MUST BE FULL");
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            changeForm();
        }
    }
}
