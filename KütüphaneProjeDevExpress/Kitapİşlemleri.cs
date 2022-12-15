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
    public partial class Kitapİşlemleri : Form
    {
        public Kitapİşlemleri()
        {
            InitializeComponent();

            liste();
            rafSeç();
            kks();
           


        }
        SqlConnection baglantı = new SqlConnection("Data Source=DESKTOP-EPA8GDH\\SQLEXPRESS;Initial Catalog=KUTUPHANE_PROJE_VERITABANIV2;Integrated Security=True");
      
        public void liste() 
        {
            baglantı.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select ISBN,Kitap_Ad as KitapAd,Kitap_YazarId as Yazarİd,Kitap_YayineviId as Yayineviİd,Kitap_BasimT as BasımTarihi,Kitap_TurId as Türİd,Kitap_RafId as Rafİd,Durum from Kitap_TabloV2", baglantı);
            da.Fill(dt);
            gridControl1.DataSource = dt;
            baglantı.Close();


        }

        
        private void simpleButton1_Click(object sender, EventArgs e)
        {

            baglantı.Open();
            try
            {
                SqlCommand komut = new SqlCommand("insert into Kitap_TabloV2 (ISBN,Kitap_Ad,Kitap_BasimT,Kitap_TurId,Kitap_YayineviId,Kitap_YazarId,Kitap_RafId,Durum) " +
                                "values('" + txtISBN.Text + "','" + txtKitapAd.Text + "','" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "','" +
                                 txtKitapYazar.Text + "','" + txtKitapTur.Text + "','" + txtKitapYayinevi.Text + "','" + cbKitapRaf.Text + "','" + "Rafta" + "')", baglantı);

                komut.ExecuteNonQuery();
                MessageBox.Show("Başarılı İşlem");
            }
            catch (Exception)
            {

                MessageBox.Show("Hatalı İşlem");
            }

            baglantı.Close();
            
            
            liste();
        }

        private void cbKitapRaf_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        
        }

        public void rafSeç()
        {
            baglantı.Open();
            SqlCommand komut = new SqlCommand("select Raf_id from Raf_TabloV2", baglantı);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cbKitapRaf.Properties.Items.Add(dr[0]);
            }
            baglantı.Close();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
           
            DialogResult dr = MessageBox.Show("Kitap Silinsinmi", "Uyarı", MessageBoxButtons.YesNo);
           try 
            { 
            if (dr == DialogResult.Yes)
            {
                baglantı.Open();
                SqlCommand komut = new SqlCommand("delete from Kitap_TabloV2 where ISBN=@p1", baglantı);
                komut.Parameters.AddWithValue("@p1", txtISBN.Text);
                komut.ExecuteNonQuery();
                MessageBox.Show("İşlem Başarılı");

            }
            }
           
           
            catch (Exception)
            {
                MessageBox.Show("İşlem Başarısız");
            }
           
            
            baglantı.Close();
            foreach (Control item in Controls)
            {
                if (item is DataGridViewTextBoxEditingControl)
                {
                    item.Text = "";
                }
            }
            liste();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            try
            {
                SqlCommand komut = new SqlCommand("update Kitap_TabloV2 set " +
                "Kitap_Ad=@p2,Kitap_YazarId=@p3,Kitap_YayineviId=@p4,Kitap_BasimT=@p5,Kitap_TurId=@p6,Kitap_RafId=@p7 where ISBN=@p1", baglantı);

                komut.Parameters.AddWithValue("@p1", txtISBN.Text);
                komut.Parameters.AddWithValue("@p2", txtKitapAd.Text);
                komut.Parameters.AddWithValue("@p3", txtKitapYazar.Text);
                komut.Parameters.AddWithValue("@p4", txtKitapYayinevi.Text);
                komut.Parameters.AddWithValue("@p5", dateTimePicker1.Value.ToString());
                komut.Parameters.AddWithValue("@p6", txtKitapTur.Text);
                komut.Parameters.AddWithValue("@p7", cbKitapRaf.Text);

                komut.ExecuteNonQuery();
                MessageBox.Show("İşlem Başarılı");
            }
            catch (Exception)
            {
                MessageBox.Show("İşlem Başarısız");
            }
            
            baglantı.Close();
            foreach (Control item in Controls)
            {
                if (item is DataGridViewTextBoxEditingControl)
                {
                    item.Text = "";
                }
            }
            liste();
        }

        private void gridView1_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
          
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr!=null)
            {
                txtISBN.Text = dr["ISBN"].ToString();
                txtKitapAd.Text = dr["KitapAd"].ToString();
                txtKitapYazar.Text = dr["Yazarİd"].ToString();
                txtKitapYayinevi.Text = dr["Yayineviİd"].ToString();
                dateTimePicker1.Text = dr["BasımTarihi"].ToString();
                txtKitapTur.Text = dr["Türİd"].ToString();
                cbKitapRaf.Text = dr["Rafİd"].ToString();
            }
            foreach (Control item in Controls)
            {
                if (item is DataGridViewTextBoxEditingControl)
                {
                    item.Text = "";
                }
            }
        
        }

        public void kks()
        {
            baglantı.Open();
            SqlCommand komut = new SqlCommand("select count(ISBN) from Kitap_TabloV2",baglantı);

            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                lblkks.Text = read.ToString();
            }
            baglantı.Close();
        }
    }
}
