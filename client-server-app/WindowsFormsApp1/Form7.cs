using MongoDB.Bson;
using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form7 : Form
    {
        CultureInfo culture = CultureInfo.CreateSpecificCulture("ru-RU");

        public Form7()
        {
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            InitializeComponent();
            this.FormInit();
        }

        public void VisibilityOn()
        {
            foreach (Control c in this.Controls)
            {
                c.Visible = true;
            }
            label11.Visible = false;
            button3.Visible = false;
        }

        public void VisibilityOff()
        {
            foreach (Control c in this.Controls)
            {
                c.Visible = false;
            }
            label11.Visible = true;
            button3.Visible = true;
        }

        public void LoadRecord()
        {
            VisibilityOn();

            button2.Enabled = false;
            button1.Enabled = true;
            button4.Enabled = true;

            listBox1.Enabled = false;
            listBox2.Enabled = false;
            listBox4.Enabled = false;
            listBox5.Enabled = false;
            listBox6.Enabled = false;
            checkBox1.Enabled = false;
            numericUpDown1.Enabled = false;
            numericUpDown2.Enabled = false;
            numericUpDown3.Enabled = false;
            numericUpDown1.ReadOnly = true;
            numericUpDown2.ReadOnly = true;
            numericUpDown3.ReadOnly = true;

            listBox6.Items.Clear();
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox4.Items.Clear();
            listBox5.Items.Clear();

            MedicalRecord record = DataContainer.mongoRepos.Get(DataContainer.currentEmployee.PersonID);

            numericUpDown1.Value = record.height;
            numericUpDown2.Value = record.weight;

            int type = int.Parse(record.blood_type.Substring(1));
            listBox6.Items.Add(record.blood_type[0]);
            listBox1.Items.Add(type);

            numericUpDown3.Value = decimal.Parse(record.eyesight.Substring(1));
            listBox2.Items.Add(record.eyesight[0]);
            if (record.eyesight == "+1,0")
            {
                checkBox1.Checked = true;
            }
            listBox2.SelectedIndex = -1;
            if (record.psychoneurological_dispensary == true)
            {
                listBox4.Items.Add("Состоит");
            }
            else
            {
                listBox4.Items.Add("Не состоит");
            }

            if (record.drug_dispensary == true)
            {
                listBox5.Items.Add("Состоит");
            }
            else
            {
                listBox5.Items.Add("Не состоит");
            }

        }

        public void EditRecord()
        {
            CreateRecord();

            MedicalRecord record = DataContainer.mongoRepos.Get(DataContainer.currentEmployee.PersonID);

            if (checkBox1.Checked == true)
            {
                numericUpDown3.Enabled = false;
                numericUpDown3.ReadOnly = true;
                listBox2.Enabled = false;
            }

            int type = int.Parse(record.blood_type.Substring(1));
            listBox1.SelectedIndex = type - 1;

            if (record.blood_type[0] == '+')
            {
                listBox6.SelectedIndex = 0;
            }
            else
            {
                listBox6.SelectedIndex = 1;
            }

            if (record.eyesight[0] == '+')
            {
                listBox2.SelectedIndex = 0;
            }
            else
            {
                listBox2.SelectedIndex = 1;
            }

            if (record.psychoneurological_dispensary == true)
            {
                listBox4.SelectedIndex = 0;
            }
            else
            {
                listBox4.SelectedIndex = 1;
            }

            if (record.drug_dispensary == true)
            {
                listBox5.SelectedIndex = 0;
            }
            else
            {
                listBox5.SelectedIndex = 1;
            }
        }

        public void CreateRecord()
        {
            VisibilityOn();
            button1.Enabled = false;
            button4.Enabled = false;
            button2.Enabled = true;

            listBox1.Enabled = true;
            listBox2.Enabled = true;
            listBox4.Enabled = true;
            listBox5.Enabled = true;
            listBox6.Enabled = true;
            checkBox1.Enabled = true;
            numericUpDown1.Enabled = true;
            numericUpDown2.Enabled = true;
            numericUpDown3.Enabled = true;
            numericUpDown1.ReadOnly = false;
            numericUpDown2.ReadOnly = false;
            numericUpDown3.ReadOnly = false;

            listBox1.Items.Clear();
            listBox6.Items.Clear();
            listBox2.Items.Clear();
            listBox4.Items.Clear();
            listBox5.Items.Clear();

            listBox1.Items.Add(1);
            listBox1.Items.Add(2);
            listBox1.Items.Add(3);
            listBox1.Items.Add(4);
            listBox6.Items.Add('+');
            listBox6.Items.Add('-');
            listBox2.Items.Add('+');
            listBox2.Items.Add('-');
            listBox4.Items.Add("Состоит");
            listBox4.Items.Add("Не состоит");
            listBox5.Items.Add("Состоит");
            listBox5.Items.Add("Не состоит");
        }

        public void SaveRecord()
        {
            bool ok = SanityCheck();
            if (!ok)
            {
                MessageBox.Show("Все поля должны быть заполнены.", "Ошибка");
            }
            else
            {
                string blood_type = listBox1.SelectedItem.ToString();
                string sign = listBox6.SelectedItem.ToString();
                string eyesight = numericUpDown3.Value.ToString();
                string sign_eye = listBox2.SelectedItem.ToString();

                MedicalRecord record = DataContainer.mongoRepos.Get(DataContainer.currentEmployee.PersonID);
                if (record != null)
                {
                    record.height = (int)numericUpDown1.Value;
                    record.weight = (int)numericUpDown2.Value;
                    record.blood_type = sign + blood_type;
                    record.eyesight = sign_eye + eyesight;

                    if (listBox4.SelectedIndex == 0)
                    {
                        record.psychoneurological_dispensary = true;
                    }
                    else
                    {
                        record.psychoneurological_dispensary = false;
                    }
                    if (listBox5.SelectedIndex == 0)
                    {
                        record.drug_dispensary = true;
                    }
                    else
                    {
                        record.drug_dispensary = false;
                    }

                    try
                    {
                        DataContainer.mongoRepos.Update(record);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка");
                    }
                }
                else
                {
                    record = new MedicalRecord
                    {
                        _id = ObjectId.GenerateNewId(),
                        person_id = DataContainer.currentEmployee.PersonID,
                        height = (int)numericUpDown1.Value,
                        weight = (int)numericUpDown2.Value,
                        blood_type = sign + blood_type,
                        eyesight = sign_eye + eyesight
                    };

                    if (listBox4.SelectedIndex == 0)
                    {
                        record.psychoneurological_dispensary = true;
                    }
                    else
                    {
                        record.psychoneurological_dispensary = false;
                    }
                    if (listBox5.SelectedIndex == 0)
                    {
                        record.drug_dispensary = true;
                    }
                    else
                    {
                        record.drug_dispensary = false;
                    }

                    try
                    {
                        DataContainer.mongoRepos.Insert(record);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка");
                    }
                }
                LoadRecord();
            }
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            try
            {
                MedicalRecord record = DataContainer.mongoRepos.Get(DataContainer.currentEmployee.PersonID);
                if (record != null)
                {
                    LoadRecord();
                } else
                {
                    VisibilityOff();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EditRecord();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CreateRecord();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveRecord();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                DataContainer.mongoRepos.Delete(DataContainer.currentEmployee.PersonID);
                MessageBox.Show("Медицинская карта удалена");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        public bool SanityCheck()
        {
            if (listBox1.SelectedIndex == -1 || listBox2.SelectedIndex == -1 ||
                listBox4.SelectedIndex == -1 || listBox5.SelectedIndex == -1 ||
                listBox6.SelectedIndex == -1)
            {
                return false;
            } else
            {
                return true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                numericUpDown3.Value = decimal.Parse("+1,0");
                listBox2.SelectedIndex = 0;
                numericUpDown3.Enabled = false;
                numericUpDown3.ReadOnly = true;
                listBox2.Enabled = false;
            }
            else
            {
                numericUpDown3.Enabled = true;
                numericUpDown3.ReadOnly = false;
                listBox2.Enabled = true;
            }
        }
    }
}
