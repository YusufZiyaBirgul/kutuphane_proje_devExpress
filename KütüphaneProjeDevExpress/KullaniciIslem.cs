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
    public partial class KullaniciIslem : Form
    {
        public KullaniciIslem()
        {
            InitializeComponent();
            KitapListe();
            GirisForm gf = new GirisForm();
            gf.ESAK(label1);
        }
        SqlConnection baglantı = new SqlConnection("Data Source=DESKTOP-EPA8GDH\\SQLEXPRESS;Initial Catalog=KUTUPHANE_PROJE_VERITABANIV2;Integrated Security=True");

       
        public void KitapListe()
        {
            baglantı.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select kt.ISBN,kt.Kitap_Ad as KitapAd,rt.Raf_Ad,yt.Yazar_ad,kt.Durum from Kitap_TabloV2 kt " +
                "inner join Raf_TabloV2 rt on kt.Kitap_RafId=rt.Raf_id " +
                "inner join Yazar_TabloV2 yt on kt.Kitap_YazarId=yt.Yazar_id ", baglantı);
            da.Fill(dt);
            gridControl1.DataSource = dt;
            baglantı.Close();
        }
        /*
        public void ESAK(string lbl)
        {
            baglantı.Open();
            SqlCommand komut = new SqlCommand("select et.Kitap_ad from Emanet_TabloV3 et " +
                "inner join Kullanici_GirisV2 kg on et.Ogr_Tc=kg.Ogr_Sifre",baglantı);

            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                labelControl3.Text = read["Kitap_ad"].ToString();
            }
            baglantı.Close();
        }*/
    }
}
