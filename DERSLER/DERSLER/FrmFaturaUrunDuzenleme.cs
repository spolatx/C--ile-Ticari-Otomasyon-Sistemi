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
    public partial class FrmFaturaUrunDuzenleme : DevExpress.XtraEditors.XtraForm
    {
        public FrmFaturaUrunDuzenleme()
        {
            InitializeComponent();
        }

        public string urunId;
        DataAccess dataAccess = new DataAccess();
        private void FrmFaturaUrunDuzenleme_Load(object sender, EventArgs e)
        {
            txtUrunId.Text = urunId;
            SqlCommand cmd = new SqlCommand("Select * from TBL_FATURADETAY where FATURAURUNID=@P1", dataAccess.baglanti());
            cmd.Parameters.AddWithValue("@P1", urunId);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtFiyat.Text = dr[3].ToString();
                txtMiktar.Text = dr[2].ToString();
                txtTutar.Text = dr[4].ToString();
                txtUrunAd.Text = dr[1].ToString();
            }
            dataAccess.baglanti().Close();
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("update TBL_FATURADETAY set URUNAD=@P1,MIKTAR=@P2,FIYAT=@P3,TUTAR=@P4 WHERE FATURAURUNID=@P5", dataAccess.baglanti());
            cmd.Parameters.AddWithValue("@P1", txtUrunAd.Text);
            cmd.Parameters.AddWithValue("@P2", txtMiktar.Text);
            cmd.Parameters.AddWithValue("@P3", decimal.Parse(txtFiyat.Text));
            cmd.Parameters.AddWithValue("@P4", decimal.Parse(txtTutar.Text));
            cmd.Parameters.AddWithValue("@P5", txtUrunId.Text);
            cmd.ExecuteNonQuery();
            dataAccess.baglanti().Close();
            MessageBox.Show("Değişiklikler kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Delete from TBL_FATURADETAY where FATURAURUNID=@P1", dataAccess.baglanti());
            cmd.Parameters.AddWithValue("@P1", txtUrunId.Text);
            cmd.ExecuteNonQuery();
            dataAccess.baglanti().Close();
            MessageBox.Show("Ürün silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}