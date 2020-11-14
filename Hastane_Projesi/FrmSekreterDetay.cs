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
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }

        public string tc,adsoyad;

        sqlbaglantisi bgl = new sqlbaglantisi();

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("insert into Tbl_Randevular (RandevuTarih,RandevuSaat,RandevuBrans,RandevuDoktor,RandevuDurum) values (@r1,@r2,@r3,@r4,@r5)", bgl.baglanti());
            komut2.Parameters.AddWithValue("@r1", MskTarih.Text);
            komut2.Parameters.AddWithValue("@r2", MskSaat.Text);
            komut2.Parameters.AddWithValue("@r3", CmbBrans.Text);
            komut2.Parameters.AddWithValue("@r4", CmbDoktorlar.Text);
            komut2.Parameters.AddWithValue("@r5", ChkDurum.Checked);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevunuz Başarıyla Oluşturulmuştur.","Randevu Bilgisi",MessageBoxButtons.OK,MessageBoxIcon.Information);
          
           
        }

        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbDoktorlar.Items.Clear();

            SqlCommand komut = new SqlCommand("Select Doktoradi,DoktorSoyadi from Tbl_Doktorlar where DoktorBrans = @p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",CmbBrans.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                CmbDoktorlar.Items.Add(dr[0]+" "+ dr[1]);
            }
            bgl.baglanti().Close();

        }

        private void BtnDuyuruOlustur_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_duyurular (duyuru) values (@p1)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", RchDuyuru.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Duyurunuz başarıyla oluşturulmuştur.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnDoktorPaneli_Click(object sender, EventArgs e)
        {
            FrmDoktorPaneli fr = new FrmDoktorPaneli();
            fr.Show();
        }

        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {
            LblTC.Text = tc;

            //Ad Soyad

            SqlCommand komut1 = new SqlCommand("Select * from Tbl_Sekreterler where SekreterTC=@p1", bgl.baglanti());
            komut1.Parameters.AddWithValue("@p1",LblTC.Text);
            SqlDataReader dr = komut1.ExecuteReader();
            while (dr.Read())
            {
                LblAdSoyad.Text = dr[1].ToString();
            }
            bgl.baglanti().Close();

            //Branş DataGrid Çekme

            DataTable dt1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select Bransadi from Tbl_Branslar", bgl.baglanti());
            da.Fill(dt1);
            dataGridView1.DataSource = dt1;

            //Branş Combo Çekme

            SqlCommand komut3 = new SqlCommand("Select Bransadi from Tbl_Branslar", bgl.baglanti());
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                CmbBrans.Items.Add(dr3[0]);
            }
            bgl.baglanti().Close();

            //Doktor DataGrid  Çekme

            DataTable dt2 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("Select (Doktoradi +' '+ Doktorsoyadi) as Doktorlar,DoktorBrans from Tbl_Doktorlar", bgl.baglanti());
            da1.Fill(dt2);
            dataGridView2.DataSource = dt2;

            //Doktor Combo Çekme

            SqlCommand komut4 = new SqlCommand("Select (Doktoradi +' '+ Doktorsoyadi) as Doktorlar,DoktorBrans from Tbl_Doktorlar", bgl.baglanti());
            SqlDataReader dr4 = komut4.ExecuteReader();
            while (dr.Read())
            {
                CmbDoktorlar.Items.Add(dr4[0]);
            }
            bgl.baglanti().Close();
        }
    }
}
