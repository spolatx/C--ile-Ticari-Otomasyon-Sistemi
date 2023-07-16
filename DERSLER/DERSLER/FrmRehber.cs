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
    public partial class FrmRehber : DevExpress.XtraEditors.XtraForm
    {
        public FrmRehber()
        {
            InitializeComponent();
        }
        DataAccess dataAccess = new DataAccess();
        private void FrmRehber_Load(object sender, EventArgs e)
        {
            //MUSTERI BILGILERI
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select AD,SOYAD,TELEFON,TELEFON2,MAIL from TBL_MUSTERILER",dataAccess.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
         

            //FIRMA BILGILERI
            DataTable dataTable = new DataTable();
            SqlDataAdapter dtAdapter = new SqlDataAdapter("Select AD,YETKILIADSOYAD,TELEFON1," +
                "TELEFON2,TELEFON3,MAIL,FAX from TBL_FIRMALAR", dataAccess.baglanti());
            dtAdapter.Fill(dataTable);
            gridControl2.DataSource = dataTable;
            
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmMail frmMail = new FrmMail();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr!=null)
            {
                frmMail.mail = dr["MAIL"].ToString();
                frmMail.Show();
            }
        }

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            FrmMail frmMail = new FrmMail();
            DataRow dr = gridView2.GetDataRow(gridView2.FocusedRowHandle);
            if (dr != null)
            {
                frmMail.mail = dr["MAIL"].ToString();
                frmMail.Show();
            }
        }
    }
}