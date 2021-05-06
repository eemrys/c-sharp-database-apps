using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            this.Font = DataContainer.enter_font;
            this.FormInit();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            List<Employee> staff = DataContainer.repos.GetAll().Result;
            dataGridView1.RowsAdded += (obj, arg) => DataContainer.sizeDGV(dataGridView1);
            dataGridView1.RowsRemoved += (obj, arg) => DataContainer.sizeDGV(dataGridView1);
            PopulateDataGridView(staff);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Employee> staff = DataContainer.repos.GetAll().Result;
            PopulateDataGridView(staff);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int id;
            textBox2.Clear();
            try
            {
                id = Int32.Parse(textBox1.Text);
                Employee emp = DataContainer.repos.GetById(id).Result;
                List<Employee> staff = new List<Employee>();
                staff.Add(emp);
                dataGridView1.Rows.Clear();
                PopulateDataGridView(staff);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.InnerException.Message, "Ошибка");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            string name = textBox2.Text;
            try
            {
               List<Employee> staff = DataContainer.repos.GetByName(name).Result;
                if (staff.Count == 0)
                {
                    MessageBox.Show("Не найдено");
                }
                else
                {
                    dataGridView1.Rows.Clear();
                    PopulateDataGridView(staff);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.InnerException.Message, "Ошибка");
            }
        }

        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged == DataGridViewElementStates.Selected)
            {
                if (dataGridView1.SelectedRows.Count == 1)
                {
                    int id = Int32.Parse(e.Row.Cells[0].Value.ToString());
                    DataContainer.currentEmployee = DataContainer.repos.GetById(id).Result;
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

        private void button4_Click(object sender, EventArgs e)
        {
            Form5 detailed = new Form5();
            detailed.Show();
        }

        private void PopulateDataGridView (List<Employee> staff)
        {
            this.dataGridView1.Rows.Clear();

            foreach (Employee emp in staff)
            {
                var table = DataContainer.repos.MaritalStatusTable().Result;
                dataGridView1.Rows.Add(emp.PersonID, emp.LastName, emp.FirstName, emp.Gender, emp.BirthDate, table[emp.MaritalStatusID], emp.Passport);
            }
            for (int i = 0; i < 6; i++)
            {
                dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DataContainer.sizeDGV(dataGridView1);
            int height = dataGridView1.Height;
            this.Height = dataGridView1.Height + 150;
            dataGridView1.Height = height;
            dataGridView1.ClearSelection();
            dataGridView1.Update();
            dataGridView1.Refresh();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!(string.IsNullOrWhiteSpace(textBox1.Text)))
            {
                button2.Enabled = true;
            }
            else
            {
                button2.Enabled = false;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (!(string.IsNullOrWhiteSpace(textBox2.Text)))
            {
                button3.Enabled = true;
            }
            else
            {
                button3.Enabled = false;
            }
        }
    }
}
