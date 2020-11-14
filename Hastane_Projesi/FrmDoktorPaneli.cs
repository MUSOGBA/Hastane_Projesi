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
    public partial class FrmDoktorPaneli : Form
    {
        public FrmDoktorPaneli()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        private void FrmDoktorPaneli_Load(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from Tbl_Doktorlar", bgl.baglanti());
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1;

            //Branş Combo Çekme

            SqlCommand komut3 = new SqlCommand("Select Bransadi from Tbl_Branslar", bgl.baglanti());
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                CmbBrans.Items.Add(dr3[0]);
            }
            bgl.baglanti().Close();
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Doktorlar (doktoradi,doktorsoyadi,doktorbrans,doktortc,doktorsifre) values(@d1,@d2,@d3,@d4,@d5)", bgl.baglanti());
            komut.Parameters.AddWithValue("@d1", TxtAd.Text);
            komut.Parameters.AddWithValue("@d2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@d3", CmbBrans.Text);
            komut.Parameters.AddWithValue("@d4", MskTC.Text);
            komut.Parameters.AddWithValue("@d5", TxtSifre.Text);
            komut.ExecuteNonQuery();
            MessageBox.Show("Kayıt Başarıyla Oluşturulmuştur");
            bgl.baglanti().Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secim = dataGridView1.SelectedCells[0].RowIndex;
            TxtAd.Text = dataGridView1.Rows[secim].Cells[1].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[secim].Cells[2].Value.ToString();
            CmbBrans.Text = dataGridView1.Rows[secim].Cells[3].Value.ToString();
            MskTC.Text = dataGridView1.Rows[secim].Cells[4].Value.ToString();
            TxtSifre.Text = dataGridView1.Rows[secim].Cells[5].Value.ToString();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from tbl_doktorlar where doktortc=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",MskTC.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Silme işlemi başarıyla gerçekleştirilmiştir.");
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut1 = new SqlCommand("update tbl_doktorlar set doktoradi=@d1,doktorsoyadi=@d2,doktorbrans=@d3,doktorsifre=@d5 where doktortc=@d4 ", bgl.baglanti());
            komut1.Parameters.AddWithValue("@d1", TxtAd.Text);
            komut1.Parameters.AddWithValue("@d2", TxtSoyad.Text);
            komut1.Parameters.AddWithValue("@d3", CmbBrans.Text);
            komut1.Parameters.AddWithValue("@d4", MskTC.Text);
            komut1.Parameters.AddWithValue("@d5", TxtSifre.Text);
            komut1.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Bilgiler Güncellenmiştir.");


        }
    }
}
