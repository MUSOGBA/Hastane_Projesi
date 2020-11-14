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
    public partial class FrmSekreterGiris : Form
    {
        public FrmSekreterGiris()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        private void BtnGirisYap_Click(object sender, EventArgs e)
        {


            SqlCommand komut3 = new SqlCommand("Select * from Tbl_Sekreterler where SekreterTc=@p1 and SekreterSifre=@p2", bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1",MskTC.Text);
            komut3.Parameters.AddWithValue("@p2",TxtSifre.Text);
            SqlDataReader dr = komut3.ExecuteReader();
            if (dr.Read())
            {
                FrmSekreterDetay fr = new FrmSekreterDetay();
                fr.tc = MskTC.Text;
               
                fr.Show();
                this.Hide();
                
            }
            else
            {
                MessageBox.Show("Kimlik Numaranız veya Şifreniz Yanlıştır.\n Lütfen Tekrar Giriş Yapın ","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            bgl.baglanti().Close();



        }

        private void FrmSekreterGiris_Load(object sender, EventArgs e)
        {

        }
    }
}
