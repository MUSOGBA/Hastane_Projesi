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
    public partial class FrmBransPaneli : Form
    {
        public FrmBransPaneli()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        private void FrmBransPaneli_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Tbl_branslar", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_branslar (bransadi) values (@b1)", bgl.baglanti());
            komut.Parameters.AddWithValue("@b1", TxtBrans.Text);
            komut.ExecuteNonQuery();
            MessageBox.Show("Branş kaydınız oluşturulmuştur.");
            bgl.baglanti().Close();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut1 = new SqlCommand("delete from Tbl_branslar where bransadi=@c1", bgl.baglanti());
            komut1.Parameters.AddWithValue("@c1", TxtBrans.Text);
            komut1.ExecuteNonQuery();
            MessageBox.Show("Branş Silinmiştir.");
            bgl.baglanti().Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secim = dataGridView1.SelectedCells[0].RowIndex;
            Txtid.Text = dataGridView1.Rows[secim].Cells[0].Value.ToString();
            TxtBrans.Text = dataGridView1.Rows[secim].Cells[1].Value.ToString();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("update Tbl_branslar set bransadi=@d1 where bransid=@d2",bgl.baglanti());
            komut2.Parameters.AddWithValue("@d1",TxtBrans.Text);
            komut2.Parameters.AddWithValue("@d2", Txtid.Text);
            komut2.ExecuteNonQuery();
            MessageBox.Show("Branş başarıyla güncellenmiştir.");
        }
    }
}
