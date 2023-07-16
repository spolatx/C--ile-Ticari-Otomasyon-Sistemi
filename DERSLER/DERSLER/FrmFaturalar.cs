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
    public partial class FrmFaturalar : DevExpress.XtraEditors.XtraForm
    {
        public FrmFaturalar()
        {
            InitializeComponent();
        }
        DataAccess dataAccess = new DataAccess();
        void listele() {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_FATURABILGI",dataAccess.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void temizle()
        {
            txtAlici.Text = "";
            txtFaturaId.Text = "";
            txtFiyat.Text = "";
            txtId.Text = "";
            txtMiktar.Text = "";
            txtSeri.Text = "";
            txtSiraNo.Text = "";
            txtTeslimAlan.Text = "";
            txtTeslimEden.Text = "";
            txtTutar.Text = "";
            txtUrunAd.Text = "";
            txtUrunId.Text = "";
            txtVergiDaire.Text = "";
            mskSaat.Text = "";
            mskTarih.Text = "";
            
        }
        private void gridControl1_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (txtFaturaId.Text=="")
            {
                SqlCommand cmd = new SqlCommand("insert into TBL_FATURABILGI(SERI,SIRANO,TARIH," +
                    "SAAT,VERGIDAIRE,ALICI,TESLIMEDEN,TESLIMALAN)values" +
                    "(@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8)", dataAccess.baglanti());
                cmd.Parameters.AddWithValue("@P1", txtSeri.Text);
                cmd.Parameters.AddWithValue("@P2", txtSiraNo.Text);
                cmd.Parameters.AddWithValue("@P3", mskTarih.Text);
                cmd.Parameters.AddWithValue("@P4", mskSaat.Text);
                cmd.Parameters.AddWithValue("@P5", txtVergiDaire.Text);
                cmd.Parameters.AddWithValue("@P6", txtAlici.Text);
                cmd.Parameters.AddWithValue("@P7", txtTeslimEden.Text);
                cmd.Parameters.AddWithValue("@P8", txtTeslimAlan.Text);
                cmd.ExecuteNonQuery();
                dataAccess.baglanti().Close();
                MessageBox.Show("Fatura bilgisi sisteme kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
            if (txtFaturaId.Text!="")
            {
                double miktar, tutar, fiyat;
                fiyat = Convert.ToDouble(txtFiyat.Text);
                miktar = Convert.ToDouble(txtMiktar.Text);
                tutar = miktar * fiyat;
                txtTutar.Text = tutar.ToString();
                SqlCommand cmd = new SqlCommand("insert into TBL_FATURADETAY(URUNAD,MIKTAR,FIYAT,TUTAR,FATURAID)" +
                    "values(@p1,@p2,@p3,@p4,@p5)", dataAccess.baglanti());
                cmd.Parameters.AddWithValue("@p1", txtUrunAd.Text);
                cmd.Parameters.AddWithValue("@p2", txtMiktar.Text);
                cmd.Parameters.AddWithValue("@p3", txtFiyat.Text);
                cmd.Parameters.AddWithValue("@p4", txtTutar.Text);
                cmd.Parameters.AddWithValue("@p5", txtFaturaId.Text);
                cmd.ExecuteNonQuery();
                dataAccess.baglanti().Close();
                MessageBox.Show("Faturaya ait ürün sisteme kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtId.Text = dr["FATURABILGIID"].ToString();
                txtSiraNo.Text = dr["SIRANO"].ToString();
                txtSeri.Text = dr["SERI"].ToString();
                mskTarih.Text = dr["TARIH"].ToString();
                mskSaat.Text = dr["SAAT"].ToString();
                txtVergiDaire.Text = dr["VERGIDAIRE"].ToString();
                txtAlici.Text = dr["ALICI"].ToString();
                txtTeslimEden.Text = dr["TESLIMEDEN"].ToString();
                txtTeslimAlan.Text = dr["TESLIMALAN"].ToString();
            }
        }

    

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Delete from TBL_FATURABILGI where FATURABILGIID=@p1", dataAccess.baglanti());
            cmd.Parameters.AddWithValue("@p1", txtFaturaId.Text);
            cmd.ExecuteNonQuery();
            dataAccess.baglanti().Close();
            MessageBox.Show("Fatura Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
        }

        private void btnTemizle_Click_1(object sender, EventArgs e)
        {
            temizle();
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("update TBL_FATURABILGI set " +
                "SERI=@P1, SIRA=@P2,TARIH=@P3,SAAT=@P4,VERGIDAIRE=@P5,ALICI=@P6,TESLIMEDEN=@P7," +
                "TESLIMALAN=@P8 where FATURABILGIID=@P9",dataAccess.baglanti());
            cmd.Parameters.AddWithValue("@P1", txtSeri.Text);
            cmd.Parameters.AddWithValue("@P2", txtSiraNo.Text);
            cmd.Parameters.AddWithValue("@P3", mskTarih.Text);
            cmd.Parameters.AddWithValue("@P4", mskSaat.Text);
            cmd.Parameters.AddWithValue("@P5", txtVergiDaire.Text);
            cmd.Parameters.AddWithValue("@P6", txtAlici.Text);
            cmd.Parameters.AddWithValue("@P7", txtTeslimEden.Text);
            cmd.Parameters.AddWithValue("@P8", txtTeslimAlan.Text);
            cmd.Parameters.AddWithValue("@P9", txtFaturaId.Text);
            cmd.ExecuteNonQuery();
            dataAccess.baglanti().Close();
            MessageBox.Show("Fatura bilgisi güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }
        private void gridView1_DoubleClick_1(object sender, EventArgs e)
        {
            FrmFaturaUrunDetay fr = new FrmFaturaUrunDetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            //if (dr != null)
            {
                fr.id = dr["FATURABILGIID"].ToString();
            }
            fr.Show();
        }


        private void FrmFaturalar_Load(object sender, EventArgs e)
        {
            listele();
        }
    }
}