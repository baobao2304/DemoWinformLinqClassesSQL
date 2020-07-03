using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTBuoi8_PhamVanLeBao
{
    public partial class Form2 : Form
    {
        DataClassesDataContext db = new DataClassesDataContext();
        SinhVien sv;
        int isCheck = -1;
        public Form2()
        {
            InitializeComponent();
            var khoas = from khoa in db.Khoas select khoa;
            cboMaKhoa.DataSource = khoas;
            cboMaKhoa.DisplayMember = "TenKhoa";
            cboMaKhoa.ValueMember = "MaKhoa";

            var lops = from lop in db.Lops select lop;
            cboMaLop.DataSource = lops;
            cboMaLop.DisplayMember = "TenLop";
            cboMaLop.ValueMember = "MaLop";

            var sinhvienLOP = from svl in db.SinhViens join k in db.Lops on svl.MaLop equals k.MaLop select new { svl.MaSinhVien, svl.HoTen, svl.NgaySinh, k.TenLop };
           
            dataGridView1.DataSource = sinhvienLOP;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isCheck == -1)
            {
                if (txtHoTen.Text == "")
                    MessageBox.Show("Trong");
                else
                {


                    var sinhvienLOP = (from svl in db.SinhViens
                                       join k in db.Lops on svl.MaLop equals k.MaLop
                                       select new { svl.MaSinhVien, svl.HoTen, svl.NgaySinh, k.TenLop, k.MaLop }).Where(t => t.MaLop == cboMaLop.SelectedValue.ToString() && t.HoTen == txtHoTen.Text);

                    dataGridView1.DataSource = sinhvienLOP;
                }
            }
            else
                MessageBox.Show("Dang thuc hien lenh , vui long bam luu de tiep tuc");
        }

        private void themToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isCheck == -1)
            {
                if (txtMaSinhVien.Text == "" || txtHoTen.Text == "")
                {
                    MessageBox.Show("Bạn chua nhạp đủ");
                }
                else
                {
                   
                    SinhVien sv11 = db.SinhViens.Where(t => t.MaSinhVien == txtMaSinhVien.Text).FirstOrDefault();
                    if (sv11 != null)
                    {
                        MessageBox.Show("Trung Khoa Chin");
                        return;
                    }
                    
                    sv = new SinhVien();
                    Lop lp = new Lop();

                    sv.MaSinhVien = txtMaSinhVien.Text;
                    sv.MaLop = cboMaLop.SelectedValue.ToString();
                    sv.HoTen = txtHoTen.Text;
                    sv.NgaySinh = dateTimePicker1.Value;
                    isCheck = 1;
                    MessageBox.Show("Bam Luu De  Luu vao database ");
                }
            }
            else
                MessageBox.Show("Dang thuc hien lenh , vui long bam luu de tiep tuc");

            

        }

        private void xoaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isCheck == -1)
            {
                string sv1 = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sv = db.SinhViens.Where(t => t.MaSinhVien == sv1).FirstOrDefault();

                db.SinhViens.DeleteOnSubmit(sv);
                isCheck = 2;
                MessageBox.Show("Bam Luu De  Luu vao database ");
            }
            else
                MessageBox.Show("Dang thuc hien lenh , vui long bam luu de tiep tuc");

          

        }

        private void suaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isCheck == 1)
            {
                string sv1 = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sv = db.SinhViens.Where(t => t.MaSinhVien == sv1).FirstOrDefault();
                if (sv != null)
                {
                    sv.NgaySinh = dateTimePicker1.Value;
                    sv.HoTen = txtHoTen.Text;
                    sv.MaLop = cboMaLop.SelectedValue.ToString();
                    isCheck = 3;
                    MessageBox.Show("Bam Luu De  Luu vao database ");
                }
                else
                {
                    MessageBox.Show("Khong ton tai");
                }
            }
           else
                MessageBox.Show("Dang thuc hien lenh , vui long bam luu de tiep tuc");

        }

        private void dongToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void luuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isCheck == 1 || isCheck == 2 || isCheck == 3 )
            {

                db.SinhViens.InsertOnSubmit(sv);
                db.SubmitChanges();
                var sinhvienLOP = from svl in db.SinhViens join k in db.Lops on svl.MaLop equals k.MaLop select new { svl.MaSinhVien, svl.HoTen, svl.NgaySinh, k.TenLop };

                dataGridView1.DataSource = sinhvienLOP;
                isCheck = -1;
                MessageBox.Show("Thanh Cong");
            }
            else
                MessageBox.Show("Chua Co gi Duoc Luu");
        }
    }
}
