using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DERSLER
{
    public partial class FrmNotlar : DevExpress.XtraEditors.XtraForm
    {
        public FrmNotlar()
        {
            InitializeComponent();
        }
        DataAccess dataAccess = new DataAccess();
        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_NOTLAR",dataAccess.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            txtId.Text = dr["NOTID"].ToString();
            mskTarih.Text = dr["NOTTARIH"].ToString();
            mskSaat.Text = dr["NOTSAAT"].ToString();
            txtBaslik.Text = dr["NOTBASLIK"].ToString();
            rchDetay.Text = dr["NOTDETAY"].ToString();
            txtOlusturan.Text = dr["NOTOLUSTURAN"].ToString();
            txtHitap.Text = dr["NOTHITAP"].ToString();

        }

        private void FrmNotlar_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("insert into TBL_NOTLAR(NOTTARIH,NOTSAAT," +
                "NOTBASLIK,NOTDETAY,NOTOLUSTURAN,NOTHITAP)VALUES(@P1,@P2,@P3,@P4,@P5,@P6)", dataAccess.baglanti());          
            cmd.Parameters.AddWithValue("@P1", mskTarih.Text);
            cmd.Parameters.AddWithValue("@P2", mskSaat.Text);
            cmd.Parameters.AddWithValue("@P3", txtBaslik.Text);
            cmd.Parameters.AddWithValue("@P4", rchDetay.Text);
            cmd.Parameters.AddWithValue("@P5", txtOlusturan.Text);
            cmd.Parameters.AddWithValue("@P6", txtHitap.Text);
            cmd.ExecuteNonQuery();
            dataAccess.baglanti().Close();
            MessageBox.Show("Not bilgisi sisteme kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd =new SqlCommand("update TBL_NOTLAR set NOTTARIH=@P1,NOTSAAT=@P2," +
                "NOTBASLIK=@P3,NOTDETAY=@P4,NOTOLUSTURAN=@P5,NOTHITAP=@P6 WHERE NOTID=@P7", dataAccess.baglanti());            
            cmd.Parameters.AddWithValue("@P1", mskTarih.Text);
            cmd.Parameters.AddWithValue("@P2", mskSaat.Text);
            cmd.Parameters.AddWithValue("@P3", txtBaslik.Text);
            cmd.Parameters.AddWithValue("@P4", rchDetay.Text);
            cmd.Parameters.AddWithValue("@P5", txtOlusturan.Text);
            cmd.Parameters.AddWithValue("@P6", txtHitap.Text);
            cmd.Parameters.AddWithValue("@P7", txtId.Text);
            cmd.ExecuteNonQuery();
            dataAccess.baglanti().Close();
            MessageBox.Show("Not bilgisi güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Delete from TBL_NOTLAR WHERE NOTID=@P1", dataAccess.baglanti());
            cmd.Parameters.AddWithValue("@P1", txtId.Text);
            cmd.ExecuteNonQuery();
            dataAccess.baglanti().Close();
            MessageBox.Show("Not bilgisi sistemden silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmNotDetay frmNotDetay = new FrmNotDetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr!=null)
            {
                frmNotDetay.metin = dr["NOTDETAY"].ToString();
            }
            frmNotDetay.Show();
        }
    }
}