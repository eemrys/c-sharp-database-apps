using System;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp1.DAL;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        bool text1 = false;
        bool text2 = false;

        public Form1()
        {
            InitializeComponent();
            this.Font = DataContainer.enter_font;
            this.FormInit();
            label3.HeadInit();
            label3.Location = new Point((this.Width - label3.Width) / 2 - 10, label3.Location.Y);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox2.Text;
            string pass = textBox3.Text;
            try
            {
                DataContainer.repos = new EmployeeRepository(login, pass);
                Form2 menu = new Form2();
                this.Hide();
                menu.Show();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error");
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Length > 0)
            {
                text1 = true;
            }
            else
            {
                text1 = false;
            }

            if ((text1 == true) && (text2 == true))
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text.Length > 0)
            {
                text2 = true;
            }
            else
            {
                text2 = false;
            }

            if ((text1 == true) && (text2 == true))
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
        }
    }
}
