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

namespace Hastane_Projesi
{
    public partial class FrmDoktorBilgiDuzenle : Form
    {
        public FrmDoktorBilgiDuzenle()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        public string tcno;

        private void FrmDoktorBilgiDuzenle_Load(object sender, EventArgs e)
        {
            MskTC.Text = tcno;
            SqlCommand komut = new SqlCommand("select * from Tbl_doktorlar where doktortc=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", MskTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                TxtAd.Text = dr[1].ToString();
                TxtSoyad.Text = dr[2].ToString();
                CmbBrans.Text = dr[3].ToString();
                TxtSifre.Text = dr[5].ToString();
            }
            bgl.baglanti().Close();


        }

        private void BtnBilgileriGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut1 = new SqlCommand("update Tbl_doktorlar set doktoradi=@d1,doktorsoyadi=@d2,doktorbrans=@d3,doktorsifre=@d4  where doktortc=@d5", bgl.baglanti());
            komut1.Parameters.AddWithValue("@d1",TxtAd.Text);
            komut1.Parameters.AddWithValue("@d2", TxtSoyad.Text);
            komut1.Parameters.AddWithValue("@d3", CmbBrans.Text);
            komut1.Parameters.AddWithValue("@d4", TxtSifre.Text);
            komut1.Parameters.AddWithValue("@d5", MskTC.Text);
            komut1.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Bilgileriniz Günllenmiştir.");
        }
    }
}
