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
    public partial class FrmFaturaUrunDetay : DevExpress.XtraEditors.XtraForm
    {
        public FrmFaturaUrunDetay()
        {
            InitializeComponent();
        }
        public string id;
        DataAccess dataAccess = new DataAccess();
        void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_FATURADETAY where FATURAID='"+id+"'",dataAccess.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource=dt;
        }
        private void FrmFaturaUrunDetay_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmFaturaUrunDuzenleme frmFaturaUrunDuzenleme = new FrmFaturaUrunDuzenleme();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr!=null)
            {
                frmFaturaUrunDuzenleme.urunId = dr["FATURAURUNID"].ToString();
            }
            frmFaturaUrunDuzenleme.Show();
        }
    }
}