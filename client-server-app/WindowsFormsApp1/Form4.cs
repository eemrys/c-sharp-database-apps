using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form4 : Form
    {
        int index;
        Font f_def;
        Color c_def;
        string[] t_def = new string[12];
        CultureInfo MyCultureInfo = new System.Globalization.CultureInfo("ru-RU");

        public Form4()
        {
            InitializeComponent();
            this.FormInit();
            foreach (TextBox t in this.Controls.OfType<TextBox>())
            {
                t.GotFocus += Box_GotFocus;
                t.LostFocus += Box_LostFocus;
                t_def[t.TabIndex-1] = t.Text;
            }
            c_def = textBox1.ForeColor;
            f_def = textBox1.Font;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            string format = "{0}, {1}";            
            foreach (var row in DataContainer.AvailableJobs())
            {
                var data = (IDictionary<string, object>)row;
                listBox1.Items.Add(string.Format(format, data.Values.ElementAt(1), data.Values.ElementAt(3)));
            }           
            Dictionary<int, string> MaritalStatus = DataContainer.repos.MaritalStatusTable().Result;
            foreach (var item in MaritalStatus)
            {
                listBox3.Items.Add(item.Value);
            }
            listBox2.Items.Insert(0, 'М');
            listBox2.Items.Insert(1, 'Ж');
            button2.Enabled = false;
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker2.Value = DateTime.Today;           
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count == 1)
            {
                button2.Enabled = true;
                index = listBox1.SelectedIndex;
            }
            else
            {
                button2.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(DataContainer.JobInfo(index), "Selected Job Information");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool boxEmpty = false;
            foreach (var box in this.Controls.OfType<TextBox>())
            {
                if (box.Font.ValueEquals(f_def)) boxEmpty = true;
            }
            foreach (var box in this.Controls.OfType<ListBox>())
            {
                if (box.SelectedItems.Count == 0) boxEmpty = true;
            }
            if (boxEmpty)
            {
                MessageBox.Show("All fields must be filled.", "Invalid Format Error");
            }
            else
            {
                Dictionary<int, string> MaritalStatus = DataContainer.repos.MaritalStatusTable().Result;
                var stat = MaritalStatus.ElementAt(listBox3.SelectedIndex);
                try
                {
                    Address address = new Address
                    {
                        City = textBox11.Text,
                        Street = textBox12.Text,
                        Building = textBox13.Text,
                        Appartment = Int32.Parse(textBox14.Text)
                    };
                    EmploymentContract contract = new EmploymentContract
                    {
                        JobTitleID = DataContainer.JobID(listBox1.SelectedIndex),
                        HiringDate = dateTimePicker1.Value.ToShortDateString()
                    };
                    
                    bool isNumeric = true;
                    foreach (char c in textBox17.Text)
                    {
                        if (!char.IsDigit(c)) isNumeric = false;
                    }
                    if (!isNumeric || (textBox17.Text.Length != 10))
                    {
                        throw new InvalidOperationException();
                    }
                    EmergencyContact contact = new EmergencyContact
                    {
                        LastName = textBox15.Text,
                        FirstName = textBox16.Text,
                        CellPhone = string.Concat(label11.Text, textBox17.Text)
                    };
                    Employee employee = new Employee
                    {
                        Gender = listBox2.SelectedItem.ToString(),
                        MaritalStatusID = stat.Key,
                        BirthDate = dateTimePicker2.Value.ToShortDateString(),
                        LastName = textBox1.Text,
                        FirstName = textBox2.Text,
                        Passport = textBox6.Text
                    };
                    KeyValue email = new KeyValue
                    {
                        ID = -1,
                        Value = textBox10.Text
                    };
                    isNumeric = true;
                    foreach (char c in textBox9.Text)
                    {
                        if (!char.IsDigit(c)) isNumeric = false;
                    }
                    if (!isNumeric || (textBox9.Text.Length != 10))
                    {
                        throw new InvalidOperationException();
                    }
                    KeyValue cellphone = new KeyValue
                    {
                        ID = -1,
                        Value = string.Concat(label10.Text, textBox9.Text)
                    };
                    employee.Email = new List<KeyValue> { email };
                    employee.CellPhone = new List<KeyValue> { cellphone };
                    employee.Address = new List<Address> { address };
                    employee.EmergencyContact = new List<EmergencyContact> { contact };
                    employee.EmploymentContract = new List<EmploymentContract> { contract };
                    int res = DataContainer.repos.AddEmployee(employee).Result;
                    this.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid Format Error", "Error");
                }
            }
        }

        private void Box_GotFocus(object sender, EventArgs e)
        {            
            TextBox box = sender as TextBox;
            if (box.Font != DataContainer.enter_font)
            {               
                box.Clear();
                box.Font = DataContainer.enter_font;
                box.ForeColor = DataContainer.enter_color;
            }
        }

        private void Box_LostFocus(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (string.IsNullOrWhiteSpace(box.Text))
            {
                box.Clear();
                box.Text = t_def[box.TabIndex-1];
                box.Font = f_def;
                box.ForeColor = c_def;
            }
        }
    }
}
