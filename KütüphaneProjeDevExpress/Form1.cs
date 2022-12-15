using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KütüphaneProjeDevExpress
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Kitapİşlemleri frm;
        Üyeİşlemleri frm2;
        Emanetİşlemleri frm3;
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frm3 == null || frm3.IsDisposed)
            {
                frm3 = new Emanetİşlemleri();
                frm3.MdiParent = this;
                frm3.Show();
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
            if (frm2 == null || frm2.IsDisposed)
            {
                frm2 = new Üyeİşlemleri();
                frm2.MdiParent = this;
                frm2.Show();
            }

        }
        
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
            if (frm==null || frm.IsDisposed)
            {
                frm = new Kitapİşlemleri();
                frm.MdiParent = this;
                frm.Show();
            }
          
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
