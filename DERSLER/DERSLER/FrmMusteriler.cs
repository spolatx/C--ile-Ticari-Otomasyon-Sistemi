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
    public partial class FrmMusteriler : DevExpress.XtraEditors.XtraForm
    {
        public FrmMusteriler()
        {
            InitializeComponent();
        }
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("insert into TBL_MUSTERILER(AD,SOYAD,TELEFON,TELEFON2," +
                "TC,MAIL,IL,ILCE,ADRES,VERGIDAIRE)" +
                "values(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10)",dataAccess.baglanti());
            cmd.Parameters.AddWithValue("@p1",txtAd.Text);
            cmd.Parameters.AddWithValue("@p2", txtSoyad.Text);
            cmd.Parameters.AddWithValue("@p3", mskTel1.Text);
            cmd.Parameters.AddWithValue("@p4", mskTel2.Text);
            cmd.Parameters.AddWithValue("@p5", mskTc.Text);
            cmd.Parameters.AddWithValue("@p6", txtMail.Text);
            cmd.Parameters.AddWithValue("@p7", cmbIl.Text);
            cmd.Parameters.AddWithValue("@p8", cmbIlce.Text);
            cmd.Parameters.AddWithValue("@p9", rchAdres.Text);
            cmd.Parameters.AddWithValue("@p10", txtVergi.Text);
            cmd.ExecuteNonQuery();
            dataAccess.baglanti().Close();
            MessageBox.Show("Musteri Sisteme Eklendi", "Bilg,", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();

        }

        DataAccess dataAccess = new DataAccess();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from TBL_MUSTERILER", dataAccess.baglanti());
            da.Fill(dt);
            gridControl1.DataSource=dt;
        }

        void sehirListesi() {

            SqlCommand cmd = new SqlCommand("Select Sehir from TBL_ILLER", dataAccess.baglanti());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read()) {
                cmbIl.Properties.Items.Add(dr[0]);
            }
            dataAccess.baglanti().Close();
        }

        private void FrmMusteriler_Load(object sender, EventArgs e)
        {
            listele();
            sehirListesi();
        }

        private void cmbIl_SelectedIndexChanged(object sender, EventArgs e)
        {   
            //ıl değiştiğinde ilçeleri temizle.
            cmbIlce.Properties.Items.Clear();
            //sql komutuyla seçili indexin ilçelerini getir. veri tabanı bağlantısını kur.
            SqlCommand cmd = new SqlCommand("Select ilce from TBL_ILCELER where sehir=@P1",dataAccess.baglanti());

            cmd.Parameters.AddWithValue("@P1", cmbIl.SelectedIndex+1);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmbIlce.Properties.Items.Add(dr[0]);
            }
            dataAccess.baglanti().Close();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtId.Text = dr["ID"].ToString();
                txtAd.Text = dr["AD"].ToString();
                txtSoyad.Text = dr["SOYAD"].ToString();
                mskTel1.Text = dr["TELEFON"].ToString();
                mskTel2.Text = dr["TELEFON2"].ToString();
                mskTc.Text = dr["TC"].ToString();
                txtMail.Text = dr["MAIL"].ToString();
                cmbIl.Text = dr["IL"].ToString();
                cmbIlce.Text = dr["ILCE"].ToString();
                rchAdres.Text = dr["ADRES"].ToString();
                txtVergi.Text = dr["VERGIDAIRE"].ToString();

            }

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Delete from TBL_MUSTERILER where ID=@p1", dataAccess.baglanti());
            cmd.Parameters.AddWithValue("@p1", txtId.Text);
            cmd.ExecuteNonQuery();
            dataAccess.baglanti().Close();
            MessageBox.Show("Musteri Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Update TBL_MUSTERILER set AD=@P1,SOYAD=@P2,TELEFON=@P3,TELEFON2=@P4," +
                "TC=@P5,MAIL=@P6,IL=@P7,ILCE=@P8,ADRES=@P9,VERGIDAIRE=@P10" +
                " where ID=@P11", dataAccess.baglanti());
            cmd.Parameters.AddWithValue("@P1", txtAd.Text);
            cmd.Parameters.AddWithValue("@P2", txtSoyad.Text);
            cmd.Parameters.AddWithValue("@P3", mskTel1.Text);
            cmd.Parameters.AddWithValue("@P4", mskTel2.Text);
            cmd.Parameters.AddWithValue("@P5", mskTc.Text);
            cmd.Parameters.AddWithValue("@P6", txtMail.Text);
            cmd.Parameters.AddWithValue("@P7", cmbIl.Text);
            cmd.Parameters.AddWithValue("@P8", cmbIlce.Text);
            cmd.Parameters.AddWithValue("@P9", rchAdres.Text);
            cmd.Parameters.AddWithValue("@P10", txtVergi.Text);
            cmd.Parameters.AddWithValue("@P11", txtId.Text);
            cmd.ExecuteNonQuery();
            dataAccess.baglanti().Close();
            MessageBox.Show("Musteri Guncellendi!!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();


        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}