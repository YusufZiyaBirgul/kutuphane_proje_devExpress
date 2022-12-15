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
    public partial class Emanetİşlemleri : Form
    {
        public Emanetİşlemleri()
        {
            InitializeComponent();
            EmanetListe();
            KitapListe();
        }
        SqlConnection baglantı = new SqlConnection("Data Source=DESKTOP-EPA8GDH\\SQLEXPRESS;Initial Catalog=KUTUPHANE_PROJE_VERITABANIV2;Integrated Security=True");

        public void EmanetListe()
        {
            baglantı.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select Ogr_Tc as ÖğrencTc,ISBN,Kitap_Ad as KitapAd,EmanetVerilis,EmanetIade from Emanet_TabloV3", baglantı); //Kitap_Raf as Rafİd,
            da.Fill(dt);
            gridControl2.DataSource = dt;
            baglantı.Close();
        }
        public void KitapListe()
        {
            baglantı.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select ISBN,Kitap_Ad as KitapAd,Durum from Kitap_TabloV2", baglantı);
            da.Fill(dt);
            gridControl1.DataSource = dt;
            baglantı.Close();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            try
            {
                //,Kitap_Raf "','" + cbKitapRaf.Text +
                SqlCommand komut = new SqlCommand("insert into Emanet_TabloV3  (ISBN,Ogr_Tc,Kitap_Ad,EmanetVerilis,EmanetIade) values ('"
                   + txtISBN.Text + "','" + txtTc.Text +  "','" + txtKitapAd.Text + "','" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "','" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "')", baglantı);

                SqlCommand komut2 = new SqlCommand("update Kitap_TabloV2 set Durum='Emanet' where ISBN=@p1", baglantı);
                komut2.Parameters.AddWithValue("@p1", txtISBN.Text);



                komut.ExecuteNonQuery();
                komut2.ExecuteNonQuery();
                MessageBox.Show("İşlem Başarılı");
                
            }
            catch (Exception ex)
            {

               // MessageBox.Show("İşlem Başarısız");
                MessageBox.Show(ex.Message);
            }


            baglantı.Close();

            txtISBN.Text = "";
            txtKitapAd.Text = "";
            txtTc.Text = "";
            dateTimePicker1.Text = "";
            dateTimePicker2.Text = "";



            EmanetListe();
            KitapListe();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            try
            {
                SqlCommand komut = new SqlCommand("delete from Emanet_TabloV3 where ISBN=@p1", baglantı);
                SqlCommand komut2 = new SqlCommand("update Kitap_TabloV2 set Durum='Rafta' where ISBN=@p1", baglantı);
                komut2.Parameters.AddWithValue("@p1", txtISBN.Text);
                komut.Parameters.AddWithValue("@p1", txtISBN.Text);
                komut.ExecuteNonQuery();
                komut2.ExecuteNonQuery();
                MessageBox.Show("İşlem Başarılı");
            }
            catch (Exception)
            {

                MessageBox.Show("İşlem Başarısız");
            }
            baglantı.Close();

            txtISBN.Text = "";
            txtKitapAd.Text = "";
            txtTc.Text = "";
            dateTimePicker1.Text = "";
            dateTimePicker2.Text = "";

            EmanetListe();
            KitapListe();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            try
            {

                SqlCommand komut = new SqlCommand("update Emanet_TabloV3 set " +
                    "EmanetVerilis=@p4,EmanetIade=@p5 where ISBN=@p1 ", baglantı);

                komut.Parameters.AddWithValue("@p1", txtISBN.Text);
                komut.Parameters.AddWithValue("@p4", dateTimePicker1.Value.Date);
                komut.Parameters.AddWithValue("@p5", dateTimePicker2.Value.Date);

                komut.ExecuteNonQuery();
                MessageBox.Show("İşlem Başarılı");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
               // MessageBox.Show("İşlem Başarısız");

            }
            baglantı.Close();

            txtISBN.Text = "";
            txtKitapAd.Text = "";
            txtTc.Text = "";
            dateTimePicker1.Text = "";
            dateTimePicker2.Text = "";

            EmanetListe();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtISBN.Text = dr["ISBN"].ToString();
                txtKitapAd.Text = dr["KitapAd"].ToString();
                //dateTimePicker1.Text = dr["EmanetVerilis"].ToString();
               // dateTimePicker2.Text=dr["EmanetIade"].ToString();
               // cbKitapRaf.Text = dr["Rafİd"].ToString();
                
            }
        }

        private void gridControl2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DataRow dr = gridView2.GetDataRow(gridView2.FocusedRowHandle);
            if (dr != null)
            {
                txtISBN.Text = dr["ISBN"].ToString();
                txtKitapAd.Text = dr["KitapAd"].ToString();
                txtTc.Text = dr["ÖğrencTc"].ToString();
                dateTimePicker1.Text = dr["EmanetVerilis"].ToString();
                 dateTimePicker2.Text=dr["EmanetIade"].ToString();
            }
        }

        private void btnÖğrenciBul_Click(object sender, EventArgs e)
        {
            //"select Ogr_Tc as ÖğrencTc,ISBN,Kitap_Ad as KitapAd,EmanetVerilis,EmanetIade from Emanet_TabloV3"
            baglantı.Open();
            SqlCommand komut = new SqlCommand("select Ogr_Tc,ISBN,Kitap_Ad,EmanetVerilis,EmanetIade from Emanet_TabloV3 " +
                "where Ogr_Tc like '%" + txtOgrBul.Text + "%'", baglantı);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataSet ds = new DataSet();
            da.Fill(ds);
            gridControl2.DataSource = ds.Tables[0];
            baglantı.Close();
        }

        private void Emanetİşlemleri_Load(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand komut = new SqlCommand("select count(ISBN) from Kitap_TabloV2", baglantı);

            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                txttks.Text = read.ToString();
            }
            baglantı.Close();
        }
    }
}
