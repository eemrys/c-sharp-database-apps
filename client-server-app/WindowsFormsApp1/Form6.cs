using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
            this.FormInit();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.AutoGenerateColumns = false;
                List<object> view = DataContainer.repos.Views(DataContainer.temlpateName).Result;
                var data = (IDictionary<string, object>)view.First();
                List<string> columns = data.Keys.ToList();
                foreach (string item in columns)
                {
                    dataGridView1.Columns.Add(item, item);
                }
                foreach (object item in view)
                {
                    data = (IDictionary<string, object>)item;
                    List<string> row = new List<string>();
                    foreach (var val in data.Values)
                    {
                        row.Add(val.ToString());
                    }
                    dataGridView1.Rows.Add(row.ToArray());
                }
                dataGridView1.ClearSelection();
                DataContainer.sizeDGV(dataGridView1);
                this.sizeForm(dataGridView1);

            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error");
                this.Close();
            }
        }
    }
}
