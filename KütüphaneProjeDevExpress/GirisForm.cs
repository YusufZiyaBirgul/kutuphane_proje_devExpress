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
    public partial class GirisForm : Form
    {
        public GirisForm()
        {
            InitializeComponent();
        }
        SqlConnection baglantı = new SqlConnection("Data Source=DESKTOP-EPA8GDH\\SQLEXPRESS;Initial Catalog=KUTUPHANE_PROJE_VERITABANIV2;Integrated Security=True");

        private void btnGiris_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            KullaniciIslem frm2 = new KullaniciIslem();
            if (txtKullaniciAd.Text == "" && txtSifre.Text == "")
            {
                frm.ShowDialog();
            }

            try
            {
                baglantı.Open();
                SqlCommand komut = new SqlCommand("select * from Kullanici_GirisV2 where Ogr_Sifre=@p1 and Ogr_KullaniciAd=@p2 ", baglantı);
                SqlParameter p1 = new SqlParameter("@p1", txtSifre.Text.Trim());
                SqlParameter p2 = new SqlParameter("@p2", txtKullaniciAd.Text.Trim());
                komut.Parameters.Add(p1);
                komut.Parameters.Add(p2);

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(komut);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    frm2.ShowDialog();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Kullanıcı Adı veya Şifre Hatalı");
            }
            baglantı.Close();
        }
       public void ESAK(Label lbl)
        {
            baglantı.Open();
            
            SqlCommand komut = new SqlCommand("select et.Kitap_ad from Emanet_TabloV3 et " +
                "right join Kullanici_GirisV2 kg on et.Ogr_Tc=kg.Ogr_Sifre " +
                "where et.Ogr_Tc like '%"+txtSifre.Text+"%' and kg.Ogr_Sifre like '"+txtSifre.Text+"%'",baglantı);

            /*
            SqlCommand komut = new SqlCommand("select et.Kitap_ad from Emanet_TabloV3 et , Kullanici_GirisV2 kg" +
                " where kg.Ogr_Sifre like '%" + txtSifre.Text + "%'", baglantı);
            */
            /*SqlCommand komut = new SqlCommand("select et.Kitap_ad from Kullanici_GirisV2 kg inner join Emanet_TabloV3 et on et.Ogr_Tc=kg.Ogr_Sifre  " +
                " where kg.Ogr_Sifre like '%"+txtSifre.Text+"%'",baglantı);*/
            
            SqlDataReader read = komut.ExecuteReader();
           while (read.Read())
           {
                lbl.Text = read["Kitap_ad"].ToString();
           }
            
         
            baglantı.Close();
        }
    
    
    
    }
}
