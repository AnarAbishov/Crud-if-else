using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using crud_yeni.Models;

namespace crud_yeni
{
    public partial class Form1 : Form
    {
        private StudentInformationEntities db = new StudentInformationEntities();
        private StudentDetails selected_stdnt;


        public Form1()
        {
            InitializeComponent();
            Display();
            dgvStudent.Columns[0].Visible = false;
        }
        private void Display()
        {
            dgvStudent.DataSource = db.StudentDetails.Select(s => new {s.Id, s.Name, s.Age, s.City, s.Gender}).ToList();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            cmbxGender.Items.Add("Male");
            cmbxGender.Items.Add("Female");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            
            if (txtName.Text == "" && txtAge.Text == "" && txtCity.Text == "")
            {
                MessageBox.Show("Zehmet olmasa xanalari doldurun");
            }
            else
            {
                StudentDetails student = new StudentDetails();
                student.Name = txtName.Text;
                student.Age = Convert.ToInt32(txtAge.Text);
                student.City = txtCity.Text;
                student.Gender = cmbxGender.SelectedItem.ToString();

                db.StudentDetails.Add(student);
                db.SaveChanges();
                reset();
            }
        }


        private void reset()
        {
            Display();
            txtName.Clear();
            txtAge.Clear();
            txtCity.Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "" && txtAge.Text == "" && txtCity.Text == "")
            {
                MessageBox.Show("Zehmet olmasa deyishmek istediyiniz Row-u sechin");
            }
            else
            {
                selected_stdnt.Name = txtName.Text;
                selected_stdnt.Age = Convert.ToInt32(txtAge.Text);
                selected_stdnt.City = txtCity.Text;
                selected_stdnt.Gender = cmbxGender.SelectedItem.ToString();
                db.SaveChanges();
                reset();
            }
           
        }

        private void dgvStudent_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int id = Convert.ToInt32(dgvStudent.Rows[e.RowIndex].Cells[0].Value.ToString());
            selected_stdnt = db.StudentDetails.Find(id);
            txtName.Text = selected_stdnt.Name;
            txtAge.Text = Convert.ToString(selected_stdnt.Age);
            txtCity.Text = selected_stdnt.City;
            cmbxGender.SelectedItem = selected_stdnt.Gender;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Silmek istirsiz", "Silmek", MessageBoxButtons.YesNo);
            if (result==DialogResult.Yes)
            {
                if (txtName.Text == "" && txtAge.Text == "" && txtCity.Text == "")
                {
                    MessageBox.Show("Zehmet olmasa silmek uchun Row sechin");
                }
                else
                {
                    db.StudentDetails.Remove(selected_stdnt);
                    db.SaveChanges();
                    reset();
                }
               
            }
        }
    }
}
