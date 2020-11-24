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
    public partial class FrmRandevuListesi : Form
    {
        public FrmRandevuListesi()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        private void FrmRandevuListesi_Load(object sender, EventArgs e)
        {

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_randevular", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;



        }

        public FrmSekreterDetay frmsd = new FrmSekreterDetay();

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {


            frmsd.Show();

            int secim = dataGridView1.SelectedCells[0].RowIndex;
            frmsd.Txtid.Text = dataGridView1.Rows[secim].Cells[0].Value.ToString();
            frmsd.MskTarih.Text = dataGridView1.Rows[secim].Cells[1].Value.ToString();
            frmsd.MskSaat.Text = dataGridView1.Rows[secim].Cells[2].Value.ToString();
            frmsd.CmbBrans.Text = dataGridView1.Rows[secim].Cells[3].Value.ToString();
            frmsd.CmbDoktorlar.Text = dataGridView1.Rows[secim].Cells[4].Value.ToString();
            frmsd.ChkDurum.Text = dataGridView1.Rows[secim].Cells[5].Value.ToString();
            frmsd.MskTC.Text = dataGridView1.Rows[secim].Cells[6].Value.ToString();




        }
    }
}
