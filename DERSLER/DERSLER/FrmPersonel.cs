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
    public partial class FrmPersonel : DevExpress.XtraEditors.XtraForm
    {
        public FrmPersonel()
        {
            InitializeComponent();
        }

        void listele() {
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_PERSONELLER",dataAccess.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource=dt;
        }
        void sehirListesi() {
            SqlCommand cmd = new SqlCommand("Select Sehir from TBL_ILLER", dataAccess.baglanti());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmbIl.Properties.Items.Add(dr[0]);
                dataAccess.baglanti().Close();
            }
        }
        void temizle()
        {
            txtId.Text = "";
            txtAd.Text = "";
            txtGorev.Text = "";
            txtMail.Text = "";
            txtSoyad.Text = "";
            mskTc.Text = "";
            mskTel1.Text = "";
            cmbIl.Text = "";
            cmbIlce.Text = "";
            rchAdres.Text = "";
            txtAd.Focus();
        }

       
      
        DataAccess dataAccess = new DataAccess();
        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Delete from TBL_PERSONELLER where ID=@P1", dataAccess.baglanti());
            cmd.Parameters.AddWithValue("@P1", txtId.Text);
            cmd.ExecuteNonQuery();
            dataAccess.baglanti().Close();
            MessageBox.Show("Personel silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void FrmPersonel_Load(object sender, EventArgs e)
        {
            listele();
            sehirListesi();
            temizle();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("insert into TBL_PERSONELLER (AD,SOYAD,TELEFON,TC,MAIL,IL,ILCE,ADRES,GOREV" +
                " )values (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9)", dataAccess.baglanti());
            cmd.Parameters.AddWithValue("@P1", txtAd.Text);
            cmd.Parameters.AddWithValue("@P2", txtSoyad.Text);
            cmd.Parameters.AddWithValue("@P3", mskTel1.Text);
            cmd.Parameters.AddWithValue("@P4", mskTc.Text);
            cmd.Parameters.AddWithValue("@P5", txtMail.Text);
            cmd.Parameters.AddWithValue("@P6", cmbIl.Text);
            cmd.Parameters.AddWithValue("@P7", cmbIlce.Text);
            cmd.Parameters.AddWithValue("@P8", rchAdres.Text);
            cmd.Parameters.AddWithValue("@P9", txtGorev.Text);
            cmd.ExecuteNonQuery();
            dataAccess.baglanti().Close();
            MessageBox.Show("Personel Eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();

        }

        private void cmbIl_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbIlce.Properties.Items.Clear();
            SqlCommand cmd = new SqlCommand("Select ilce from TBL_ILCELER where sehir=@P1", dataAccess.baglanti());
            cmd.Parameters.AddWithValue("@P1", cmbIl.SelectedIndex+1);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmbIlce.Properties.Items.Add(dr[0]);
            }
            dataAccess.baglanti().Close();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("update TBL_PERSONELLER set " +
                "AD=@P1,SOYAD=@P2,TELEFON=@P3,TC=@P4,MAIL=@P5,IL=@P6,ILCE=@P7," +
                "ADRES=@P8,GOREV=@P9 where ID =@P10", dataAccess.baglanti());
            cmd.Parameters.AddWithValue("@P1", txtAd.Text);
            cmd.Parameters.AddWithValue("@P2", txtSoyad.Text);
            cmd.Parameters.AddWithValue("@P3", mskTel1.Text);
            cmd.Parameters.AddWithValue("@P4", mskTc.Text);
            cmd.Parameters.AddWithValue("@P5", txtMail.Text);
            cmd.Parameters.AddWithValue("@P6", cmbIl.Text);
            cmd.Parameters.AddWithValue("@P7", cmbIlce.Text);
            cmd.Parameters.AddWithValue("@P8", rchAdres.Text);
            cmd.Parameters.AddWithValue("@P9", txtGorev.Text);
            cmd.Parameters.AddWithValue("@P10", txtId.Text);
            cmd.ExecuteNonQuery();
            dataAccess.baglanti().Close();
            MessageBox.Show("Personel Guncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr!=null)
            {
                txtId.Text = dr["ID"].ToString();
                txtAd.Text = dr["AD"].ToString();
                txtSoyad.Text = dr["SOYAD"].ToString();
                mskTel1.Text = dr["TELEFON"].ToString();
                mskTc.Text = dr["TC"].ToString();
                txtMail.Text = dr["MAIL"].ToString();
                cmbIl.Text = dr["IL"].ToString();
                cmbIlce.Text = dr["ILCE"].ToString();
                rchAdres.Text = dr["ADRES"].ToString();
                txtGorev.Text = dr["GOREV"].ToString();
            }




        }
    }
}