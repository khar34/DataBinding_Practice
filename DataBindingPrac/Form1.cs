using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBindingPrac
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'schoolDBDataSet.Students' table. You can move, or remove it, as needed.
            this.studentsTableAdapter.Fill(this.schoolDBDataSet.Students);
            

            cmbMajor.Items.Add("Công nghệ thông tin");
            cmbMajor.Items.Add("Ngôn ngữ Anh");
            cmbMajor.Items.Add("Quản trị kinh doanh");
            cmbMajor.Items.Add("Kế toán");
            

            if (cmbMajor.Items.Count > 0)
            {
                cmbMajor.SelectedIndex = 0;
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var newRow = schoolDBDataSet.Students.NewStudentsRow();
            newRow.FullName = txtFullName.Text;
            newRow.Age = int.Parse(txtAge.Text);
            newRow.Major = cmbMajor.Text;

            schoolDBDataSet.Students.AddStudentsRow(newRow);

            studentsTableAdapter.Update(schoolDBDataSet.Students);

            studentsTableAdapter.Fill(schoolDBDataSet.Students);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewStudents.CurrentRow != null)
            {
                var currentRow = (SchoolDBDataSet.StudentsRow)((DataRowView)dataGridViewStudents.CurrentRow.DataBoundItem).Row;

                currentRow.FullName = txtFullName.Text;
                if (int.TryParse(txtAge.Text, out int newAge))
                {
                    currentRow.Age = newAge;  
                }
                currentRow.Major = cmbMajor.Text;

                studentsTableAdapter.Update(schoolDBDataSet.Students);

                studentsTableAdapter.Fill(schoolDBDataSet.Students);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một sinh viên để chỉnh sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewStudents.CurrentRow != null)
            {
                dataGridViewStudents.Rows.RemoveAt(dataGridViewStudents.CurrentRow.Index);
                studentsTableAdapter.Update(schoolDBDataSet.Students);
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (dataGridViewStudents.CurrentRow.Index > 0)
            {
                dataGridViewStudents.CurrentCell = dataGridViewStudents.Rows[dataGridViewStudents.CurrentRow.Index - 1].Cells[0];
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (dataGridViewStudents.CurrentRow.Index < dataGridViewStudents.Rows.Count - 1)
            {
                dataGridViewStudents.CurrentCell = dataGridViewStudents.Rows[dataGridViewStudents.CurrentRow.Index + 1].Cells[0];
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc muốn thoát", "Đồng ý thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
