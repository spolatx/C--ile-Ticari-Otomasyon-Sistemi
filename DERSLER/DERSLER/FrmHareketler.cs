using DevExpress.XtraEditors;
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

namespace DERSLER
{
    public partial class FrmHareketler : DevExpress.XtraEditors.XtraForm
    {
        public FrmHareketler()
        {
            InitializeComponent();
        }
        DataAccess dataAccess = new DataAccess();

        void firmaHareketleri()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Exec FirmaHareketler",dataAccess.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void musteriHareketler() {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Exec MusteriHareketler", dataAccess.baglanti());
            da.Fill(dt);
            gridControl2.DataSource = dt;
        }

        private void FrmHareketler_Load(object sender, EventArgs e)
        {
            firmaHareketleri();
            musteriHareketler();
        }

       
    }
}