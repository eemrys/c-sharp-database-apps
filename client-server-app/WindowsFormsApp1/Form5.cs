using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form5 : Form
    {
        int id;
        Font f_def;
        Color c_def;
        string[] t_def = new string[11];
        CultureInfo culture = CultureInfo.CreateSpecificCulture("ru-RU");
        

        public Form5()
        {
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            InitializeComponent();
            this.FormInit();
            label11.HeadInit(); label11.Location = new Point((label11.Parent.Width - label11.Width) / 2, label11.Location.Y);
            label12.HeadInit(); label12.Location = new Point((label12.Parent.Width - label12.Width) / 2, label12.Location.Y);
            label9.HeadInit(); label9.Location = new Point((label9.Parent.Width - label9.Width) / 2, label9.Location.Y);
            label6.HeadInit(); label6.Location = new Point((label6.Parent.Width - label6.Width) / 2, label6.Location.Y);

            label1.HeadInit();
            label14.HeadInit();
            label15.HeadInit();
            label16.HeadInit();
            label17.HeadInit();
            label18.HeadInit();

            foreach (TextBox t in this.Controls.OfType<TextBox>())
            {
                t.GotFocus += Box_GotFocus;
                t.LostFocus += Box_LostFocus;
                t_def[t.TabIndex - 1] = t.Text;
            }
            foreach (Panel p in this.Controls.OfType<Panel>())
            {
                foreach (TextBox t in p.Controls.OfType<TextBox>())
                {
                    t.GotFocus += Box_GotFocus;
                    t.LostFocus += Box_LostFocus;
                    t_def[t.TabIndex - 1] = t.Text;
                }
            }
            c_def = textBox1.ForeColor;
            f_def = textBox1.Font;
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            dataGridView2.ReadOnly = true;
            dataGridView3.ReadOnly = true;
            dataGridView4.ReadOnly = true;
            dataGridView5.ReadOnly = true;
            dataGridView6.ReadOnly = true;
            dataGridView7.ReadOnly = true;
            dateTimePicker1.Enabled = false;
            listBox1.Enabled = false;
            listBox2.Enabled = false;
            button10.Enabled = false;
            button8.Enabled = false;
            button6.Enabled = false;
            button4.Enabled = false;
            button13.Enabled = false;
            button15.Enabled = false;
            button12.Enabled = false;
            button14.Enabled = false;
            button1.Enabled = false;
            button3.Enabled = false;
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;

            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker2.Value = DateTime.Today;

            dataGridView1.Columns.Add("ID", "ID");
            dataGridView1.Columns.Add("Value", "Value");

            dataGridView2.Columns.Add("ID", "ID");
            dataGridView2.Columns.Add("Value", "Value");

            dataGridView3.Columns.Add("Property", "Property");
            dataGridView3.Columns.Add("Value", "Value");

            LoadData();
        }

        private void dataGridView4_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged == DataGridViewElementStates.Selected)
            {
                if (dataGridView4.SelectedRows.Count == 1)
                {
                    id = Int32.Parse(e.Row.Cells[0].Value.ToString());
                    button10.Enabled = true;
                }
                else
                {
                    button10.Enabled = false;
                }
            }
            else
            {
                button10.Enabled = false;
            }
        }

        private void dataGridView5_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged == DataGridViewElementStates.Selected)
            {
                if (dataGridView5.SelectedRows.Count == 1)
                {
                    id = Int32.Parse(e.Row.Cells[0].Value.ToString());
                    button8.Enabled = true;
                }
                else
                {
                    button8.Enabled = false;
                }
            }
            else
            {
                button8.Enabled = false;
            }
        }

        private void dataGridView6_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged == DataGridViewElementStates.Selected)
            {
                if (dataGridView6.SelectedRows.Count == 1)
                {
                    id = Int32.Parse(e.Row.Cells[0].Value.ToString());
                    button1.Enabled = true;
                    button6.Enabled = true;
                }
                else
                {
                    button6.Enabled = false;
                    button1.Enabled = false;
                }
            }
            else
            {
                button6.Enabled = false;
                button1.Enabled = false;
            }
        }

        private void dataGridView7_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged == DataGridViewElementStates.Selected)
            {
                if (dataGridView7.SelectedRows.Count == 1)
                {
                    id = Int32.Parse(e.Row.Cells[0].Value.ToString());
                    button4.Enabled = true;
                }
                else
                {
                    button4.Enabled = false;
                }
            }
            else
            {
                button4.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool f = false;
            var list = DataContainer.repos.GetPermissions("AllStaff").Result;
            foreach (var row in list)
            {
                var data = (IDictionary<string, object>)row;
                if (data["permission_name"].ToString() == "UPDATE")
                { f = true; }
            }
            if (f)
            {


                button2.Enabled = false;
                button3.Enabled = true;
                dataGridView3.ReadOnly = false;
                dataGridView3.Rows[0].ReadOnly = true;
                dataGridView3.Columns[0].ReadOnly = true;
                dataGridView1.ReadOnly = false;
                dataGridView1.Columns[0].ReadOnly = true;
                dataGridView2.ReadOnly = false;
                dataGridView2.Columns[0].ReadOnly = true;
                dataGridView4.ReadOnly = false;
                dataGridView4.Columns[0].ReadOnly = true;
                dataGridView5.ReadOnly = false;
                dataGridView5.Columns[0].ReadOnly = true;
                dateTimePicker1.Enabled = true;
                listBox1.Enabled = true;
                listBox2.Enabled = true;
                listBox1.Items.Clear();
                listBox2.Items.Clear();
                Dictionary<int, string> MaritalStatus = DataContainer.repos.MaritalStatusTable().Result;
                foreach (var item in MaritalStatus)
                {
                    listBox1.Items.Add(item.Value);
                }
                listBox2.Items.Insert(0, 'М');
                listBox2.Items.Insert(1, 'Ж');
                listBox1.SelectedItem = MaritalStatus[DataContainer.currentEmployee.MaritalStatusID];
                listBox2.SelectedItem = DataContainer.currentEmployee.Gender.ToCharArray()[0];
            }
            else  
            {
                MessageBox.Show("Нет прав доступа.", "Ошибка");
            }
        }


        private void button11_Click(object sender, EventArgs e)
        {
            bool f = false;
            var list = DataContainer.repos.GetPermissions("p_AddChildren").Result;
            foreach (var row in list)
            {
                var data = (IDictionary<string, object>)row;
                if (data["permission_name"].ToString() == "EXECUTE")
                { f = true; }
            }
            if (f)
            {
                foreach (Panel p in this.Controls.OfType<Panel>())
                {
                    p.Hide();
                    ClearPanel(p);
                }
                CalculateLocation(panel1);
                panel1.Show();
            }
            else
            {
                MessageBox.Show("Нет прав доступа.", "Ошибка");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            bool f = false;           
            var list = DataContainer.repos.GetPermissions("p_AddEmergencyContact").Result;
            foreach (var row in list)
            {
                var data = (IDictionary<string, object>)row;
                if (data["permission_name"].ToString() == "EXECUTE")
                { f = true;
                }
            }
            if (f)
            {
                foreach (Panel p in this.Controls.OfType<Panel>())
                {
                    p.Hide();
                    ClearPanel(p);
                }
                CalculateLocation(panel2);
                panel2.Show();
            }
            else
            {
                MessageBox.Show("Нет прав доступа.", "Ошибка");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            bool f = false;
            var list = DataContainer.repos.GetPermissions("p_AddEmploymentContract").Result;
            foreach (var row in list)
            {
                var data = (IDictionary<string, object>)row;
                if (data["permission_name"].ToString() == "EXECUTE")
                { f = true;
                }
            }
            if (f)
            {
                foreach (Panel p in this.Controls.OfType<Panel>())
                {
                    p.Hide();
                    ClearPanel(p);
                }
                CalculateLocation(panel4);
                panel4.Show();
                button19.Enabled = false;
            }
            else
            {
                MessageBox.Show("Нет прав доступа.", "Ошибка");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {          
            bool f = false;
            var list = DataContainer.repos.GetPermissions("p_AddAddress").Result;
            foreach (var row in list)
            {
                var data = (IDictionary<string, object>)row;
                if (data["permission_name"].ToString() == "EXECUTE")
                { f = true;
                }
            }
            if (f)
            {
                foreach (Panel p in this.Controls.OfType<Panel>())
                {
                    p.Hide();
                    ClearPanel(p);
                }
                CalculateLocation(panel3);
                panel3.Show();
            }
            else
            {
                MessageBox.Show("Нет прав доступа.", "Ошибка");
            }
        }


        private void button18_Click(object sender, EventArgs e)
        {
            bool boxEmpty = false;
            foreach (var box in panel3.Controls.OfType<TextBox>())
            {
                if (box.Font.ValueEquals(f_def)) boxEmpty = true;
            }
            if (boxEmpty)
            {
                MessageBox.Show("Все поля должны быть заполнены.", "Ошибка");
            }
            else
            {
                try
                {
                    Address address = new Address
                    {
                        AddressID = -1,
                        City = textBox10.Text,
                        Street = textBox9.Text,
                        Building = textBox8.Text,
                        Appartment = Int32.Parse(textBox11.Text)
                    };
                    DataContainer.currentEmployee.Address.Add(address);
                    int res = DataContainer.repos.AddAddress(DataContainer.currentEmployee).Result;
                    panel3.Hide();
                    ClearPanel(panel3);
                    LoadData();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "Ошибка");
                }
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            bool boxEmpty = false;
            foreach (var box in panel4.Controls.OfType<TextBox>())
            {
                if (box.Font.ValueEquals(f_def)) boxEmpty = true;
            }
            foreach (var box in panel4.Controls.OfType<ListBox>())
            {
                if (box.SelectedItems.Count == 0) boxEmpty = true;
            }
            if (boxEmpty)
            {
                MessageBox.Show("Все поля должны быть заполнены.", "Ошибка");
            }
            else
            {
                try
                {
                    EmploymentContract contract = new EmploymentContract
                    {
                        ProfID = -1,
                        JobTitleID = DataContainer.JobID(listBox4.SelectedIndex),
                        HiringDate = DateTime.Today.ToShortDateString(),
                        TerminationDate = null,
                        LeaveEndDate = null,
                        LeaveStartDate = null
                    };
                    DataContainer.currentEmployee.EmploymentContract.Add(contract);
                    int res = DataContainer.repos.AddJob(DataContainer.currentEmployee).Result;
                    panel4.Hide();
                    ClearPanel(panel4);
                    LoadData();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.InnerException.Message, "Ошибка");
                }
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            bool boxEmpty = false;
            foreach (var box in panel2.Controls.OfType<TextBox>())
            {
                if (box.Font.ValueEquals(f_def)) boxEmpty = true;
            }            
            if (boxEmpty)
            {
                MessageBox.Show("Все поля должны быть заполнены.", "Ошибка");
            }
            else
            {
                try
                {
                    bool isNumeric = true;
                    foreach (char c in textBox7.Text)
                    {
                        if (!char.IsDigit(c)) isNumeric = false;
                    }

                    if (!isNumeric || (textBox7.Text.Length != 10))
                    {
                        throw new InvalidOperationException();
                    }
                    EmergencyContact contact = new EmergencyContact
                    {
                        EmergencyContactID = -1,
                        LastName = textBox6.Text,
                        FirstName = textBox5.Text,
                        CellPhone = string.Concat(label10.Text, textBox7.Text)
                    };
                    DataContainer.currentEmployee.EmergencyContact.Add(contact);
                    int res = DataContainer.repos.AddEmerg(DataContainer.currentEmployee).Result;
                    panel2.Hide();
                    ClearPanel(panel2);
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка");
                }
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            bool boxEmpty = false;
            foreach (var box in panel1.Controls.OfType<TextBox>())
            {
                if (box.Font.ValueEquals(f_def)) boxEmpty = true;
            }
            foreach (var box in panel1.Controls.OfType<ListBox>())
            {
                if (box.SelectedItems.Count == 0) boxEmpty = true;
            }
            if (boxEmpty)
            {
                MessageBox.Show("Все поля должны быть заполнены.", "Ошибка");
            }
            else
            {
                try
                {
                    Child child = new Child
                    {
                        ChildID = -1,
                        LastName = textBox3.Text,
                        FirstName = textBox4.Text,
                        Gender = listBox3.SelectedItem.ToString(),
                        BirthDate = dateTimePicker2.Value.ToShortDateString()
                    };
                    DataContainer.currentEmployee.Children.Add(child);
                    int res = DataContainer.repos.AddChild(DataContainer.currentEmployee).Result;
                    panel1.Hide();
                    ClearPanel(panel1);
                    LoadData();
                }               
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка");
                }
            }           
        }

        private void button12_Click(object sender, EventArgs e)
        {
            bool boxEmpty = false;
            if (textBox1.Font.ValueEquals(f_def)) boxEmpty = true;
            if (boxEmpty)
            {
                MessageBox.Show("Все поля должны быть заполнены.", "Ошибка");
            }
            else
            {
                try
                {
                    bool isNumeric = true;
                    foreach  (char c in textBox1.Text)
                    {
                        if (!char.IsDigit(c)) isNumeric = false;
                    }

                    if (!isNumeric || (textBox1.Text.Length != 10))
                    {
                        throw new InvalidOperationException();
                    }
                    KeyValue cell = new KeyValue
                    {
                        ID = 100,
                        Value = string.Concat(label2.Text, textBox1.Text)
                    };
                    DataContainer.currentEmployee.CellPhone.Add(cell);
                    int res = DataContainer.repos.AddCellPhone(DataContainer.currentEmployee).Result;
                    textBox1.Clear();
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка");
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if ((!(textBox1.Font.ValueEquals(f_def))) && (!(string.IsNullOrWhiteSpace(textBox1.Text))))
            {
                button12.Enabled = true;
            }
            else
            {
                button12.Enabled = false;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if ((!(textBox2.Font.ValueEquals(f_def))) && (!(string.IsNullOrWhiteSpace(textBox2.Text))))
            {
                button14.Enabled = true;
            }
            else
            {
                button14.Enabled = false;
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            bool boxEmpty = false;
            if (textBox2.Font.ValueEquals(f_def)) boxEmpty = true;
            if (boxEmpty)
            {
                MessageBox.Show("Все поля должны быть заполнены.", "Ошибка");
            }
            else
            {
                try
                {
                    KeyValue email = new KeyValue
                    {
                        ID = 100,
                        Value = textBox2.Text
                    };
                    DataContainer.currentEmployee.Email.Add(email);
                    int res = DataContainer.repos.AddEmail(DataContainer.currentEmployee).Result;
                    textBox2.Clear();
                    LoadData();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.InnerException.Message, "Ошибка");
                }
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            MessageBox.Show(DataContainer.JobInfo(listBox4.SelectedIndex), "Информация о должности");
        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox4.SelectedItems.Count == 1)
            {
                button19.Enabled = true;
            }
            else
            {
                button19.Enabled = false;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                int res = DataContainer.repos.RemoveOptionalField(DataContainer.currentEmployee, "Children", id).Result;
                LoadData();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.InnerException.Message, "Ошибка");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                int res = DataContainer.repos.RemoveOptionalField(DataContainer.currentEmployee, "EmergencyContact", id).Result;
                LoadData();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.InnerException.Message, "Ошибка");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                int res = DataContainer.repos.Terminate(DataContainer.currentEmployee, id).Result;
                try
                {
                    LoadData();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.InnerException.Message, "Сотрудник уволен.");
                    this.Close();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.InnerException.Message, "Ошибка");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                int res = DataContainer.repos.RemoveOptionalField(DataContainer.currentEmployee, "Address", id).Result;
                LoadData();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.InnerException.Message, "Ошибка");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                bool f = true;
                foreach (DataGridViewRow row in dataGridView4.Rows)
                {
                    if ((row.Cells[3].Value.ToString() != "Ж") && (row.Cells[3].Value.ToString() != "М"))
                    {
                        f = false;
                    }
                }
                if (f)
                {
                    Dictionary<int, string> MaritalStatus = DataContainer.repos.MaritalStatusTable().Result;
                    var stat = MaritalStatus.ElementAt(listBox1.SelectedIndex);
                    DataContainer.currentEmployee.MaritalStatusID = stat.Key;
                    DataContainer.currentEmployee.Gender = listBox2.SelectedItem.ToString();
                    DataContainer.currentEmployee.BirthDate = dateTimePicker1.Value.ToShortDateString();
                    DataContainer.currentEmployee.LastName = dataGridView3.Rows[1].Cells[1].Value.ToString();
                    DataContainer.currentEmployee.FirstName = dataGridView3.Rows[2].Cells[1].Value.ToString();
                    DataContainer.currentEmployee.Passport = dataGridView3.Rows[3].Cells[1].Value.ToString();
                    List<KeyValue> Cells = new List<KeyValue>();
                    foreach (DataGridViewRow item in dataGridView1.Rows)
                    {
                        KeyValue cell = new KeyValue
                        {
                            ID = Int32.Parse(item.Cells[0].Value.ToString()),
                            Value = item.Cells[1].Value.ToString()
                        };
                        Cells.Add(cell);
                    }
                    List<KeyValue> Emails = new List<KeyValue>();
                    foreach (DataGridViewRow item in dataGridView2.Rows)
                    {
                        KeyValue email = new KeyValue
                        {
                            ID = Int32.Parse(item.Cells[0].Value.ToString()),
                            Value = item.Cells[1].Value.ToString()
                        };
                        Emails.Add(email);
                    }
                    DataContainer.currentEmployee.CellPhone.Clear();
                    DataContainer.currentEmployee.CellPhone = Cells;
                    DataContainer.currentEmployee.Email.Clear();
                    DataContainer.currentEmployee.Email = Emails;
                    foreach (DataGridViewRow row in dataGridView4.Rows)
                    {
                        foreach (Child c in DataContainer.currentEmployee.Children)
                        {
                            if (c.ChildID == Int32.Parse(row.Cells[0].Value.ToString()))
                            {
                                c.LastName = row.Cells[1].Value.ToString();
                                c.FirstName = row.Cells[2].Value.ToString();
                                c.Gender = row.Cells[3].Value.ToString();
                                c.BirthDate = row.Cells[4].Value.ToString();
                            }
                        }
                    }
                    foreach (DataGridViewRow row in dataGridView5.Rows)
                    {
                        foreach (EmergencyContact em in DataContainer.currentEmployee.EmergencyContact)
                        {
                            if (em.EmergencyContactID == Int32.Parse(row.Cells[0].Value.ToString()))
                            {
                                em.LastName = row.Cells[1].Value.ToString();
                                em.FirstName = row.Cells[2].Value.ToString();
                                em.CellPhone = row.Cells[3].Value.ToString();
                            }
                        }
                    }
                    int res = DataContainer.repos.Update(DataContainer.currentEmployee).Result;
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Пол должен быть 'М' или 'Ж'");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
                LoadData();
            }
            button3.Enabled = false;
            button2.Enabled = true;
            dataGridView1.ReadOnly = true;
            dataGridView2.ReadOnly = true;
            dataGridView3.ReadOnly = true;
            dataGridView4.ReadOnly = true;
            dataGridView5.ReadOnly = true;
            dateTimePicker1.Enabled = false;
            listBox1.Enabled = false;
            listBox2.Enabled = false;
        }
        public void LoadData()
        {
            foreach (DataGridView grid in this.Controls.OfType<DataGridView>())
            {
                grid.DataSource = null;
                grid.Rows.Clear();
            }
            DataContainer.currentEmployee = DataContainer.repos.GetById(DataContainer.currentEmployee.PersonID).Result;

            var data = DataContainer.currentEmployee.CellPhone;
            foreach (var item in data)
            {
                dataGridView1.Rows.Add(item.ID, item.Value);
            }

            data = DataContainer.currentEmployee.Email;
            foreach (var item in data)
            {
                dataGridView2.Rows.Add(item.ID, item.Value);
            }
            Type t = DataContainer.currentEmployee.GetType();
            PropertyInfo[] p = t.GetProperties();
            for (int i = 0; i < 4; i++)
            {
                dataGridView3.Rows.Add(p[i].Name, p[i].GetValue(DataContainer.currentEmployee).ToString());
            }
            Dictionary<int, string> MaritalStatus = DataContainer.repos.MaritalStatusTable().Result;
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox1.Items.Add(MaritalStatus[DataContainer.currentEmployee.MaritalStatusID]);
            listBox2.Items.Add(DataContainer.currentEmployee.Gender);
            CultureInfo MyCultureInfo = new System.Globalization.CultureInfo("ru-RU");
            dateTimePicker1.Value = DateTime.Parse(DataContainer.currentEmployee.BirthDate, MyCultureInfo);   
            dataGridView4.DataSource = DataContainer.currentEmployee.Children;
            dataGridView5.DataSource = DataContainer.currentEmployee.EmergencyContact;
            dataGridView6.DataSource = DataContainer.currentEmployee.EmploymentContract;
            dataGridView7.DataSource = DataContainer.currentEmployee.Address;

            string format = "{0}, {1}";
            foreach (var row in DataContainer.AvailableJobs())
            {
                var data2 = (IDictionary<string, object>)row;
                listBox4.Items.Add(string.Format(format, data2.Values.ElementAt(1), data2.Values.ElementAt(3)));
            }
            listBox3.Items.Insert(0, 'М');
            listBox3.Items.Insert(1, 'Ж');

            foreach (DataGridView grid in this.Controls.OfType<DataGridView>())
            {
                grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                grid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                DataContainer.sizeDGV(grid);
                grid.Width = grid.MaximumSize.Width;
                grid.ClearSelection();
                grid.Update();
                grid.Refresh();
            }

            this.Update();
            this.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(DataContainer.ProfInfo(id), "Информация о должности");
        }


        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged == DataGridViewElementStates.Selected)
            {
                if (dataGridView1.SelectedRows.Count == 1)
                {
                    id = Int32.Parse(e.Row.Cells[0].Value.ToString());
                    button13.Enabled = true;
                }
                else
                {
                    button13.Enabled = false;
                }
            }
            else
            {
                button13.Enabled = false;
            }
        }

        private void dataGridView2_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged == DataGridViewElementStates.Selected)
            {
                if (dataGridView2.SelectedRows.Count == 1)
                {
                    id = Int32.Parse(e.Row.Cells[0].Value.ToString());
                    button15.Enabled = true;
                }
                else
                {
                    button15.Enabled = false;
                }
            }
            else
            {
                button15.Enabled = false;
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                int res = DataContainer.repos.RemoveOptionalField(DataContainer.currentEmployee, "CellPhone", id).Result;
                LoadData();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.InnerException.Message, "Ошибка");
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                int res = DataContainer.repos.RemoveOptionalField(DataContainer.currentEmployee, "Email", id).Result;
                LoadData();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.InnerException.Message, "Ошибка");
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            panel2.Hide();
            ClearPanel(panel2);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            panel4.Hide();
            ClearPanel(panel4);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            panel3.Hide();
            ClearPanel(panel3);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            panel1.Hide();
            ClearPanel(panel1);
        }

        public void CalculateLocation(Control control)
        {
            int x = (this.Width - control.Width) / 2;
            int y = (this.Height - control.Height) / 2;
            Point point = new Point(x, y);
            control.Location = point;
        }
        public void ClearPanel(Panel panel)
        {
            foreach (TextBox box in panel.Controls.OfType<TextBox>())
            {
                box.Clear();
                box.Text = t_def[box.TabIndex - 1];
                box.Font = f_def;
                box.ForeColor = c_def;
            }
            foreach (ListBox t in panel.Controls.OfType<ListBox>())
            {
                t.SelectedIndex = -1;
            }
            foreach (DateTimePicker t in panel.Controls.OfType<DateTimePicker>())
            {
                t.Value = DateTime.Today;
                t.Format = DateTimePickerFormat.Custom;
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
                box.Text = t_def[box.TabIndex - 1];
                box.Font = f_def;
                box.ForeColor = c_def;
                if (box.Name[7] == '1') button12.Enabled = false;
                if (box.Name[7] == '2') button14.Enabled = false;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button25_Click(object sender, EventArgs e)
        {
            Form7 medical = new Form7();
            medical.Show();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
