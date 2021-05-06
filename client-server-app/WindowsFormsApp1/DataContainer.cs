using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp1.DAL;

namespace WindowsFormsApp1
{
    public static class DataContainer
    {

        public static EmployeeRepository repos = null;
        public static MongoRepository mongoRepos = null;
        public static Employee currentEmployee = null;
        public static string temlpateName = null;
        public static Font enter_font = new Font("Segoe UI", 8, GraphicsUnit.Point);
        public static Color enter_color = Color.Black;
        public static Color buttonColor = SystemColors.ControlLight;
        public static Color panelColor = Color.WhiteSmoke;
        public static Color formColor = panelColor;
        public static Font labelFont = new Font("Segoe UI", 9, GraphicsUnit.Point);
        public static Font buttonFont = labelFont;
        public static Font headingFont = new Font("Segoe UI Semibold", 10, GraphicsUnit.Point);
        public static Color gridbackColor = buttonColor;
        public static Color gridColor = SystemColors.ActiveBorder;


        public static List<object> AvailableJobs()
        {
            temlpateName = "select JobTitleID,JobTitle,CategoryName,DepartmentName,WageRate,RankByCategory,RankByDepartment from v_GetStaffSchedule where JobTitleID in (select distinct JobTitleID from Vacancies)";
            return repos.Views(temlpateName).Result;
        }

        public static string JobInfo(int index)
        {
            string result = null;
            string format = "{0}: {1}\n";
            var row = AvailableJobs()[index];
            var data = (IDictionary<string, object>)row;
            foreach (var t in data)
            {
                result = string.Concat(result, string.Format(format, t.Key, t.Value));
            }
            return result;
        }

        public static int JobID(int index)
        {
            var row = AvailableJobs()[index];
            var data = (IDictionary<string, object>)row;
            int id = Int32.Parse(data.Values.First().ToString());
            return id;
        }

        public static string ProfInfo(int id)
        {
            temlpateName = "Select b.JobTitleID,a.JobTitle,a.CategoryName,a.DepartmentName,a.WageRate,a.RankByCategory,a.RankByDepartment from ProfInfo as b join dbo.v_GetStaffSchedule as a on b.JobTitleID = a.JobTitleID where b.ProfID = {0}";
            temlpateName = string.Format(temlpateName, id.ToString());
            string result = null;
            string format = "{0}: {1}\n";
            var row = repos.Views(temlpateName).Result.First();
            var data = (IDictionary<string, object>)row;
            foreach (var t in data)
            {
                result = string.Concat(result, string.Format(format, t.Key, t.Value));
            }
            return result;
        }

        public static bool ValueEquals(this Font font, Font other)
        {
            if (font.Name != other.Name) return false;
            if (font.SizeInPoints != other.SizeInPoints) return false;
            if (font.Style != other.Style) return false;
            return true;
        }

        public static void sizeDGV(DataGridView dgv)
        {
            DataGridViewElementStates states = DataGridViewElementStates.None;
            var totalHeight = dgv.Rows.GetRowsHeight(states) + dgv.ColumnHeadersHeight;
            var totalWidth = dgv.Columns.GetColumnsWidth(states) + dgv.RowHeadersWidth;
            Size size  = new Size(totalWidth, totalHeight + 2);
            dgv.ClientSize = size;
            if (size.Height <= dgv.MaximumSize.Height)
            {
                dgv.ScrollBars = ScrollBars.None;
            }
            else
            {
                dgv.ScrollBars = ScrollBars.Vertical;
            }
        }

        public static void sizeForm(this Form form, DataGridView dgv)
        {
            Size size = dgv.Size;
            form.Size = new Size(dgv.Width + 40, dgv.Height + 63);
            dgv.Size = size;
            dgv.Location = new Point((form.Width - dgv.Width) / 2 - 8, (form.Height - dgv.Height) / 2 - 19); 
        }

        public static void FormInit(this Control c)
        {
            if (c.GetType() == typeof(Form))
            {
                Form form = c as Form;
                form.BackColor = formColor;
                form.ForeColor = enter_color;
                form.ShowIcon = false;
                form.FormBorderStyle = FormBorderStyle.FixedSingle;
            }

            foreach (Button b in c.Controls.OfType<Button>())
            {
                b.Font = buttonFont;
                b.BackColor = buttonColor;
                b.ForeColor = enter_color;
                b.FlatStyle = FlatStyle.Standard;
            }
            foreach (Label b in c.Controls.OfType<Label>())
            {
                b.Font = labelFont;
                b.ForeColor = enter_color;
                b.BorderStyle = BorderStyle.None;
            }
            foreach (Panel b in c.Controls.OfType<Panel>())
            {
                b.BackColor = panelColor;
                b.BorderStyle = BorderStyle.FixedSingle;
                b.FormInit();
            }
            foreach (DataGridView b in c.Controls.OfType<DataGridView>())
            {
                b.BackgroundColor = gridbackColor;
                b.GridColor = gridColor;
                b.Font = enter_font;
                b.ForeColor = enter_color;
                b.RowHeadersWidth = 25;
            }
            foreach (ListBox b in c.Controls.OfType<ListBox>())
            {
                b.Font = enter_font;
                b.ForeColor = enter_color;
            }
            foreach (ComboBox b in c.Controls.OfType<ComboBox>())
            {
                b.Font = enter_font;
                b.ForeColor = enter_color;
            }
            foreach (DateTimePicker b in c.Controls.OfType<DateTimePicker>())
            {
                b.CalendarFont = enter_font;
                b.Font = enter_font;
            }
        }

        public static void HeadInit(this Label head)
        {
            head.Font = headingFont;
            head.ForeColor = enter_color;
        }
    }
} 
