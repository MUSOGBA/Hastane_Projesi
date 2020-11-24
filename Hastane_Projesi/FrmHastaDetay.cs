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
    public partial class FrmHastaDetay : Form
    {
        public FrmHastaDetay()
        {
            InitializeComponent();
        }

        public string tc,ad,soyad;

        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            // DOKTORLARIN ÇEKİLMESİ
            CmbDoktor.Items.Clear();

            SqlCommand komut3 = new SqlCommand("Select Doktoradi,DoktorSoyadi from Tbl_Doktorlar where DoktorBrans=@p1", bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1", CmbBrans.Text);

            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                CmbDoktor.Items.Add(dr3[0]+" "+dr3[1]);
            }
            bgl.baglanti().Close();
        }

        private void CmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Tbl_Randevular where RandevuBrans='" + CmbBrans.Text + "'" +"and Randevudoktor='"+CmbDoktor.Text+ "'"+"and randevudurum=0", bgl.baglanti());
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        private void LnkBilgiDuzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmBilgiDuzenle fr = new FrmBilgiDuzenle();
            fr.TCno = LblTC.Text;
            fr.Show();
            
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            Txtid.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();
     
        }

        private void BtnRandevuAl_Click(object sender, EventArgs e)
        {
            RchSikayet.Clear();

            SqlCommand komut2 = new SqlCommand("update Tbl_Randevular set randevudurum=1,HastaTc=@p1,HastaSikayet=@p2 where randevuid=@p3",bgl.baglanti());
            komut2.Parameters.AddWithValue("@p1",LblTC.Text);
            komut2.Parameters.AddWithValue("@p2",RchSikayet.Text);
            komut2.Parameters.AddWithValue("@p3", Txtid.Text);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevunuz Oluşturuldu","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        private void FrmHastaDetay_Load(object sender, EventArgs e)
        {
            LblTC.Text = tc;

            //VERİ TABANINDAN AD SOYAD ÇEKME

            SqlCommand komut = new SqlCommand("select Hastaadi,HastaSoyadi from Tbl_Hastalar where HastaTC=@p1", bgl.baglanti());

            komut.Parameters.AddWithValue("@p1", LblTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblAdSoyad.Text = dr[0] + " " + dr[1];
            }
            bgl.baglanti().Close();

            //RANDEVU GEÇMİŞİ
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Tbl_Randevular where HastaTC=" + LblTC.Text, bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            //BRANŞLARIN ÇEKİLMESİ
            SqlCommand komut2 = new SqlCommand("select Bransadi from  Tbl_Branslar", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                CmbBrans.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();

           
        }
    }
}
