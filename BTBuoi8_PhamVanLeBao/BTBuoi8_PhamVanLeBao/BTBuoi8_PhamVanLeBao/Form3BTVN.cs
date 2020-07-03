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
    public partial class Form3BTVN : Form
    {
        int isCheck = -1;
        DataClassesBTVNDataContext db = new DataClassesBTVNDataContext();
        Sach sacH;
        NXB nXB;
            
        public Form3BTVN()
        {
            InitializeComponent();


            //var khoas = from khoa in db.Khoas select khoa;
            //cboMaKhoa.DataSource = khoas;
            //cboMaKhoa.DisplayMember = "TenKhoa";
            //cboMaKhoa.ValueMember = "MaKhoa";

            var nxbs = from nxb in db.NXBs select nxb ;
            cboNXB.DataSource = nxbs;
            cboNXB.DisplayMember = "tennxb";
            cboNXB.ValueMember = "manxb";

            var sachNXB = from snxb in db.Saches join k in db.NXBs on snxb.manxb equals k.manxb select new { snxb.masach, snxb.tensach, snxb.tacgia, k.tennxb, snxb.soluong };

            dataGridView1.DataSource = sachNXB;
        }

        private void thêmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isCheck == -1)
            {
                if (txtMaSach.Text == "" || txtSoLuong.Text == "" || txtTacGia.Text =="" || txtTenSach.Text == "")
                {
                    MessageBox.Show("Bạn chua nhạp đủ");
                }
                else
                {


                    //var sachNXB = (from snxb in db.Saches join k in db.NXBs on snxb.manxb equals k.manxb 
                    //              select new { snxb.masach, snxb.tensach, snxb.tacgia, k.tennxb, snxb.soluong }).Where(t => t.masach == txtMaSach.Text 
                    //              && t.tensach == txtTenSach.Text && t.tacgia == txtTacGia.Text && t.soluong == int.Parse(txtSoLuong.Text));

                    //Sach sAHHH = db.Saches.Where(t => t.masach == txtMaSach.Text && t.tensach == txtTenSach.Text && t.tacgia == txtTacGia.Text 
                    //&& t.soluong == int.Parse(txtSoLuong.Text) && t.manxb == cboNXB.SelectedValue.ToString()).FirstOrDefault();

                    Sach sAHHH = db.Saches.Where(t => t.masach == txtMaSach.Text ).FirstOrDefault();
                    if (sAHHH != null)
                    {
                        MessageBox.Show("Trung Khoa Chin");
                        return;
                    }

                    //sv = new SinhVien();
                    //Lop lp = new Lop();
                    sacH.masach = txtMaSach.Text;
                    sacH.tensach = txtTenSach.Text;
                    sacH.tacgia = txtTacGia.Text;
                    sacH.soluong = int.Parse(txtSoLuong.Text);
                    sacH.manxb = cboNXB.SelectedValue.ToString();
                    //sv.MaSinhVien = txtMaSinhVien.Text;
                    //sv.MaLop = cboMaLop.SelectedValue.ToString();
                    //sv.HoTen = txtHoTen.Text;
                    //sv.NgaySinh = dateTimePicker1.Value;
                    isCheck = 1;
                    MessageBox.Show("Bam Luu De  Luu vao database ");
                }
            }
            else
                MessageBox.Show("Dang thuc hien lenh , vui long bam luu de tiep tuc");
        }

        private void xóaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isCheck == -1)
            {
                string sachNXB = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sacH = db.Saches.Where(t => t.masach == sachNXB).FirstOrDefault();

                db.Saches.DeleteOnSubmit(sacH);
                isCheck = 2;
                MessageBox.Show("Bam Luu De  Luu vao database ");
            }
            else
                MessageBox.Show("Dang thuc hien lenh , vui long bam luu de tiep tuc");


        }

        private void sưarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isCheck == 1)
            {
                string sachNXB = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sacH = db.Saches.Where(t => t.masach == sachNXB).FirstOrDefault();
                if (sacH != null)
                {
                    sacH.masach = txtMaSach.Text;
                    sacH.tensach = txtTenSach.Text;
                    sacH.tacgia = txtTacGia.Text;
                    sacH.manxb = cboNXB.SelectedValue.ToString();
                    sacH.soluong = int.Parse(txtSoLuong.Text);
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

        private void lưuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isCheck == 1 || isCheck == 2 || isCheck == 3)
            {

                db.Saches.InsertOnSubmit(sacH);
                db.SubmitChanges();

                var sachNXB = from snxb in db.Saches join k in db.NXBs on snxb.manxb equals k.manxb select new { snxb.masach, snxb.tensach, snxb.tacgia, k.tennxb, snxb.soluong };

                dataGridView1.DataSource = sachNXB;
                isCheck = -1;
                MessageBox.Show("Thanh Cong");
            }
            else
                MessageBox.Show("Chua Co gi Duoc Luu");
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }
    }
}
