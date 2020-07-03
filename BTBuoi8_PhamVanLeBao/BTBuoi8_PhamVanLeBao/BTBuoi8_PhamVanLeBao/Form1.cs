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
    public partial class Form1 : Form
    {
        DataClassesDataContext db = new DataClassesDataContext();
        public Form1()
        {
            InitializeComponent();
            var monhocs = from mh in db.MonHocs select mh;
            dataGridView1.DataSource = monhocs;
        }

        private void themToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtMaMonHoc.Text == "" || txtTenMonHoc.Text == "")
            {
                MessageBox.Show("Bạn chua nhạp đủ");
            }
            MonHoc mh = new MonHoc();
            mh.MaMonHoc = txtMaMonHoc.Text;
            mh.TenMonHoc = txtTenMonHoc.Text;

            db.MonHocs.InsertOnSubmit(mh);
            db.SubmitChanges();
            var monhocss = from mh1 in db.MonHocs select mh1;
            dataGridView1.DataSource = monhocss;
        }

        private void xoaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MonHoc mh = new MonHoc();

            string mamh = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            mh = db.MonHocs.Where(t => t.MaMonHoc == mamh).FirstOrDefault();

            db.MonHocs.DeleteOnSubmit(mh);

            db.SubmitChanges();

            var monhocss = from mh1 in db.MonHocs select mh1;
            dataGridView1.DataSource = monhocss;


        }

        private void suaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MonHoc mh = new MonHoc();

            string mamh = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            mh = db.MonHocs.Where(t => t.MaMonHoc == mamh).FirstOrDefault();
            if(mh != null){
                mh.MaMonHoc = txtMaMonHoc.Text;
            mh.TenMonHoc = txtTenMonHoc.Text;

            db.SubmitChanges();
            }
            else
            {
                MessageBox.Show("Khong ton tai");
            }
          

            var monhocss = from mh1 in db.MonHocs select mh1;
            dataGridView1.DataSource = monhocss;
        }

        private void đóngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
