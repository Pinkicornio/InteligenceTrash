using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace PapeleraInteligente
{
    public partial class FormInterface : Form
    {

        private static DataManagement management = new DataManagement();
        private static MySqlDataReader rdr = null;
        private static int second = 1;
        public FormInterface()
        {
            InitializeComponent();
            initConfig();
            management.startConnection();
            timerInterface.Start();
        }

        private void initConfig()
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
          
        }

        private void timerInterface_Tick(object sender, EventArgs e)
        {
            
            StorageItem item = new StorageItem();

            labelTitle.Text = getTime() + DataManagement.currentUser;
            labelStor.Text = "Storage " + DataManagement.currentStorage;

            textBoxReader.Select();
            item = management.selectProduct(textBoxReader.Text);
            management.updateStock(item);

            if (item.Name == null)
            {
                scanTextGif(second);
            }
            else
            {
                labelInfo.Text = item.Name + "\n" + item.Brand;
                second = -15;
            }

            textBoxReader.Clear();

            if (second == 15)
            {
                second = 1;
            }
            else
            {
                second++;
            }
            
        }

        private void scanTextGif(int second)
        {
           
            switch (second)
            {
                case 1:
                    labelInfo.Text = "Scann";
                    break;
                case 4:
                    labelInfo.Text = "Scann.";
                    break;
                case 8:
                    labelInfo.Text = "Scann..";
                    break;
                case 11:
                    labelInfo.Text = "Scann...";
                    break;
            }
        }

        private string getTime()
        {
            string aux = "";
            int hour = Int32.Parse(DateTime.Now.ToString("HH"));

            if (hour > 6 && hour < 12)
            {
                aux = "Good Morning ";
            }else if (hour == 12)
            {
                aux = "High Noon ";
            }
            else if (hour > 12 && hour <= 17)
            {
                aux = "Good Afternoon ";
            }
            else if (hour > 17 && hour <= 20)
            {
                aux = "Good Evening ";
            }
            else if ((hour > 20 && hour <= 23) || (hour >= 0 && hour <= 6) )
            {
                aux = "Good Night ";
            }

            return aux;
        }

        private void FormInterface_Load(object sender, EventArgs e)
        {

        }
    }
}
