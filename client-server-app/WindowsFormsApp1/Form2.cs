using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            comboBox1.DataSource = new List<string> { "Staff Schedule", "Address Catalog", "Veterans List", "Employees With Several Contracts", "Employees Of Retirement Age", "Termination History", "Parent-Child Relation", "Emergency Contacts" };
            this.FormInit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 records = new Form3();
            records.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool f = false;
            var list = DataContainer.repos.GetPermissions("p_AddStaff").Result;
            foreach (var row in list)
            {
                var data = (IDictionary<string, object>)row;
                if (data["permission_name"].ToString() == "EXECUTE")
                { f = true; }
            }

            if (f)
            {
                Form4 addRecord = new Form4();
                addRecord.Show();
            }
            else
            {
                MessageBox.Show("Permission denied", "Error");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    DataContainer.temlpateName = "select JobTitleID,JobTitle,CategoryName,DepartmentName,NumOfStakes,WageRate from v_GetStaffSchedule";
                    break;
                case 1:
                    DataContainer.temlpateName = "select PersonID, Country,City,Street,Building,Appartment from dbo.v_Address";
                    break;
                case 2:
                    DataContainer.temlpateName = "select PersonID, LastName, FirstName, Experience, CONVERT(VARCHAR, HiringDate, 104) as HiringDate from dbo.v_GetVeterans";
                    break;
                case 3:
                    DataContainer.temlpateName = "exec dbo.p_GetMultStakePeople";
                    break;
                case 4:
                    DataContainer.temlpateName = "select * from dbo.v_Retirement";
                    break;
                case 5:
                    DataContainer.temlpateName = "select ID, LastName, FirstName , CONVERT(VARCHAR, TerminationDate, 104) as TerminationDate from Archive";
                    break;
                case 6:
                    DataContainer.temlpateName = "select ParentID, a.ChildID, a.LastName, a.FirstName, a.Gender, CONVERT(VARCHAR, a.BirthDate, 104) as BirthDate from ParentChildRelation join Children as a on a.ChildID = ParentChildRelation.ChildID";
                    break;
                case 7:
                    DataContainer.temlpateName = "select a.PersonID, b.EmergencyContactID, b.LastName, b.FirstName , b.CellPhone from EmergencyContactRelation as a join EmergencyContacts as b on a.EmergencyContactID = b.EmergencyContactID";
                    break;
            }
            Form6 views = new Form6();
            views.Text = comboBox1.SelectedItem.ToString();
            views.Show();
        }
    }
}
