using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace KütüphaneProjeDevExpress
{
    public partial class Üyeİşlemleri : Form
    {
        public Üyeİşlemleri()
        {
            InitializeComponent();
            ÖğrenciListe();
        }
        SqlConnection baglantı = new SqlConnection("Data Source=DESKTOP-EPA8GDH\\SQLEXPRESS;Initial Catalog=KUTUPHANE_PROJE_VERITABANIV2;Integrated Security=True");
        public void ÖğrenciListe()
        {
            baglantı.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Uye_TabloV2", baglantı);
            da.Fill(dt);
            gridControl1.DataSource = dt;
            baglantı.Close();
        }
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            try
            {
                
                SqlCommand komut = new SqlCommand("insert into Uye_TabloV2 (Ogr_No,Ogr_Tc,Ogr_Ad,Ogr_Soyad,Ogr_Tel,Ogr_KayitT,Ogr_EMail,Ogr_Adres) values ('"
                    + txtÖğrenciNo.Text + "','" + txtTc.Text + "','" + txtAd.Text + "','" + txtSoyad.Text + "','" + txtTel.Text + "','" +
                    dtpKayitT.Value.ToString("yyyy-MM-dd") + "','" + txtMail.Text + "','" + rtAdres.Text + "')", baglantı);

                komut.ExecuteNonQuery();
                MessageBox.Show("İşlem Başarılı");
            }
            catch (Exception)
            {
                MessageBox.Show("İşlem Başarısız");
            }
            
            baglantı.Close();
            txtTc.Text = "";
            txtAd.Text = "";
            txtÖğrenciNo.Text = "";
            txtSoyad.Text = "";
            txtTel.Text = "";
            rtAdres.Text = "";
            txtMail.Text = "";
            dtpKayitT.Text = "";
            ÖğrenciListe();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            try
            {
                SqlCommand komut = new SqlCommand("delete from Uye_TabloV2 where Ogr_Tc=@p1", baglantı);
                SqlCommand komut2 = new SqlCommand("delete from Kullanici_GirisV2  where Ogr_Sifre=@p2", baglantı);
                komut.Parameters.AddWithValue("@p1", txtTc.Text);
                komut2.Parameters.AddWithValue("@p2", txtTc.Text);
                komut2.ExecuteNonQuery();
                komut.ExecuteNonQuery();
                MessageBox.Show("İşlem Başarılı");
            }
            catch (Exception)
            {

                MessageBox.Show("İşlem Başarısız");
            }
            

            baglantı.Close();
            txtTc.Text = "";
            txtAd.Text = "";
            txtÖğrenciNo.Text = "";
            txtSoyad.Text = "";
            txtTel.Text = "";
            rtAdres.Text = "";
            txtMail.Text = "";
            dtpKayitT.Text = "";
            ÖğrenciListe();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            try
            {
                SqlCommand komut = new SqlCommand("update Uye_TabloV2 set " +
                "Ogr_No=@p2,Ogr_Ad=@p3,Ogr_Soyad=@p4,Ogr_Tel=@p5,Ogr_KayitT=@p6,Ogr_EMail=@p7,Ogr_Adres=@p8 where Ogr_Tc=@p1", baglantı);
                komut.Parameters.AddWithValue("@p1", txtTc.Text);
                komut.Parameters.AddWithValue("@p2", txtÖğrenciNo.Text);
                komut.Parameters.AddWithValue("@p3", txtAd.Text);
                komut.Parameters.AddWithValue("@p4", txtSoyad.Text);
                komut.Parameters.AddWithValue("@p5", txtTel.Text);
                komut.Parameters.AddWithValue("@p6", dtpKayitT.Value.ToString());
                komut.Parameters.AddWithValue("@p7", txtMail.Text);
                komut.Parameters.AddWithValue("@p8", rtAdres.Text);

                SqlCommand komut2 = new SqlCommand("update Kullanici_GirisV2 set Ogr_KullaniciAd=@p9  where Ogr_Sifre=@p10", baglantı);

                komut2.Parameters.AddWithValue("@p9", txtAd.Text);
                komut2.Parameters.AddWithValue("@p10", txtTc.Text);

                komut2.ExecuteNonQuery();
                komut.ExecuteNonQuery();
                MessageBox.Show("İşlem Başarılı");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //MessageBox.Show("İşlem Başarısız");
            }
            txtTc.Text = "";
            txtAd.Text = "";
            txtÖğrenciNo.Text = "";
            txtSoyad.Text = "";
            txtTel.Text = "";
            rtAdres.Text = "";
            txtMail.Text = "";
            dtpKayitT.Text = "";




            baglantı.Close();
            ÖğrenciListe();
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            /*"Ogr_No=@p2,Ogr_Ad=@p3,Ogr_Soyad=@p4,Ogr_Tel=@p5,Ogr_KayitT=@p6,Ogr_EMail=@p7,Ogr_Adres=@p8 where Ogr_Tc=@p1"*/
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr!=null)
            { 
                txtAd.Text = dr["Ogr_Ad"].ToString();
                txtTc.Text = dr["Ogr_Tc"].ToString();
                txtÖğrenciNo.Text = dr["Ogr_No"].ToString();
                txtSoyad.Text = dr["Ogr_Soyad"].ToString();
                txtTel.Text = dr["Ogr_Tel"].ToString();
                dtpKayitT.Text = dr["Ogr_KayitT"].ToString();
                txtMail.Text = dr["Ogr_EMail"].ToString();
                rtAdres.Text = dr["Ogr_Adres"].ToString();
            }
        }
    }
}
