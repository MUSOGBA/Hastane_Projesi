using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Hastane_Projesi
{
    class sqlbaglantisi
    {
        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection("Data Source = MUSOGBA; Initial Catalog = Hastane Projesi; Integrated Security = True");
            baglan.Open();
            return baglan;
        }
    }
}
