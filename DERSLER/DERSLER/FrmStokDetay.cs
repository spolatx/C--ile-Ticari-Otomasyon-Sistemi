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
    public partial class FrmStokDetay : DevExpress.XtraEditors.XtraForm
    {
        public FrmStokDetay()
        {
            InitializeComponent();
        }

        public string ad;
        DataAccess dataAccess = new DataAccess();
        private void FrmStokDetay_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_URUNLER WHERE URUNAD='"+ad+"'",dataAccess.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
    }
}