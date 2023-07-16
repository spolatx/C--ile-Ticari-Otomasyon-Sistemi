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
    public partial class FrmFirmalar : DevExpress.XtraEditors.XtraForm
    {
        public FrmFirmalar()
        {
            InitializeComponent();
        }

        DataAccess dataAccess = new DataAccess();

        void firmaListesi() {
            SqlDataAdapter da = new SqlDataAdapter("Select * From TBL_FIRMALAR", dataAccess.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource=dt;
        }
        void temizle()
        {
            txtAd.Text = "";
            txtId.Text = "";
            txtKod1.Text = "";
            txtKod2.Text = "";
            txtKod3.Text = "";
            txtMail.Text = "";
            txtSektor.Text = "";
            txtVergi.Text = "";
            txtYetkili.Text = "";
            txtYetkiliGorev.Text = "";
            rchAdres.Text = "";
            rchKod1.Text = "";
            rchKod2.Text = "";
            rchKod3.Text = "";
            mskFax.Text = "";
            mskTel1.Text = "";
            mskTel2.Text = "";
            mskTel3.Text = "";
            mskYetkiliTc.Text = "";
            txtAd.Focus();
        }
        void sehirListesi()
        {

            SqlCommand cmd = new SqlCommand("Select Sehir from TBL_ILLER", dataAccess.baglanti());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmbIl.Properties.Items.Add(dr[0]);
            }
            dataAccess.baglanti().Close();
        }

        void cariKodAciklamalar() {
            SqlCommand cmd = new SqlCommand("Select FIRMAKOD1 from TBL_KODLAR",dataAccess.baglanti());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                rchKod1.Text = dr[0].ToString();
            }
            dataAccess.baglanti().Close();

        }
   

        private void FrmFirmalar_Load(object sender, EventArgs e)
        {
            firmaListesi();
            temizle();
            sehirListesi();
            cariKodAciklamalar();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //seçili satırı fieldlara getir.
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                if (dr!=null)
                {
                    txtId.Text = dr["ID"].ToString();
                    txtAd.Text = dr["AD"].ToString();
                    txtYetkiliGorev.Text = dr["YETKILISTATU"].ToString();
                    txtYetkili.Text = dr["YETKILIADSOYAD"].ToString();
                    mskYetkiliTc.Text = dr["YETKILITC"].ToString();
                    txtSektor.Text = dr["SEKTOR"].ToString();
                    mskTel1.Text = dr["TELEFON1"].ToString();
                    mskTel2.Text=dr["TELEFON2"].ToString();
                    mskTel3.Text=dr["TELEFON3"].ToString();
                    txtMail.Text = dr["MAIL"].ToString();
                    mskFax.Text = dr["FAX"].ToString();
                    cmbIl.Text = dr["IL"].ToString();
                    cmbIlce.Text = dr["ILCE"].ToString();
                    txtVergi.Text = dr["VERGIDAIRE"].ToString();
                    rchAdres.Text = dr["ADRES"].ToString();
                    txtKod1.Text = dr["OZELKOD1"].ToString();
                    txtKod2.Text=dr["OZELKOD2"].ToString();
                    txtKod3.Text = dr["OZELKOD3"].ToString();
                  
                }

            
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("insert into TBL_FIRMALAR(AD,YETKILISTATU" +
                ",YETKILIADSOYAD,YETKILITC,SEKTOR," +
                "TELEFON1,TELEFON2,TELEFON3,MAIL," +
                "FAX,IL,ILCE,VERGIDAIRE,ADRES,OZELKOD1" +
                ",OZELKOD2,OZELKOD3)Values(@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11," +
                "@P12,@P13,@P14,@P15,@P16,@P17)", dataAccess.baglanti());

            cmd.Parameters.AddWithValue("@P1", txtAd.Text);
            cmd.Parameters.AddWithValue("@P2", txtYetkiliGorev.Text);
            cmd.Parameters.AddWithValue("@P3", txtYetkili.Text);
            cmd.Parameters.AddWithValue("@P4", mskYetkiliTc.Text);
            cmd.Parameters.AddWithValue("@P5", txtSektor.Text);
            cmd.Parameters.AddWithValue("@P6", mskTel1.Text);
            cmd.Parameters.AddWithValue("@P7", mskTel2.Text);
            cmd.Parameters.AddWithValue("@P8", mskTel3.Text);
            cmd.Parameters.AddWithValue("@P9", txtMail.Text);
            cmd.Parameters.AddWithValue("@P10", mskFax.Text);
            cmd.Parameters.AddWithValue("@P11", cmbIl.Text);
            cmd.Parameters.AddWithValue("@P12", cmbIlce.Text);
            cmd.Parameters.AddWithValue("@P13", txtVergi.Text);
            cmd.Parameters.AddWithValue("@P14", rchAdres.Text);
            cmd.Parameters.AddWithValue("@P15", txtKod1.Text);
            cmd.Parameters.AddWithValue("@P16", txtKod2.Text);
            cmd.Parameters.AddWithValue("@P17", txtKod3.Text);
            cmd.ExecuteNonQuery();
            dataAccess.baglanti().Close();
            MessageBox.Show("Firmalar Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            firmaListesi();
            temizle();
        }

        private void cmbIl_SelectedIndexChanged(object sender, EventArgs e)
        {

            //ıl değiştiğinde ilçeleri temizle.
            cmbIlce.Properties.Items.Clear();
            //sql komutuyla seçili indexin ilçelerini getir. veri tabanı bağlantısını kur.
            SqlCommand cmd = new SqlCommand("Select ilce from TBL_ILCELER where sehir=@P1", dataAccess.baglanti());

            cmd.Parameters.AddWithValue("@P1", cmbIl.SelectedIndex + 1);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmbIlce.Properties.Items.Add(dr[0]);
            }
            dataAccess.baglanti().Close();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Delete from TBL_FIRMALAR where ID=@P1", dataAccess.baglanti());
            cmd.Parameters.AddWithValue("@P1", txtId.Text);
            cmd.ExecuteNonQuery();
            dataAccess.baglanti().Close();
            MessageBox.Show("Firma Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            firmaListesi();
            temizle();
            
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Update TBL_FIRMALAR set AD=@P1,YETKILISTATU=@P2" +
                ",YETKILIADSOYAD=@P3,YETKILITC=@P4,SEKTOR=@P5," +
                "TELEFON1=@P6,TELEFON2=@P7,TELEFON3=@P8,MAIL=@P9," +
                "FAX=@P10,IL=@P11,ILCE=@P12,VERGIDAIRE=@P13,ADRES=@P14,OZELKOD1=@P15" +
                ",OZELKOD2=@P16,OZELKOD3=@P17 where ID=@P18", dataAccess.baglanti());
            cmd.Parameters.AddWithValue("@P1", txtAd.Text);
            cmd.Parameters.AddWithValue("@P2", txtYetkiliGorev.Text);
            cmd.Parameters.AddWithValue("@P3", txtYetkili.Text);
            cmd.Parameters.AddWithValue("@P4", mskYetkiliTc.Text);
            cmd.Parameters.AddWithValue("@P5", txtSektor.Text);
            cmd.Parameters.AddWithValue("@P6", mskTel1.Text);
            cmd.Parameters.AddWithValue("@P7", mskTel2.Text);
            cmd.Parameters.AddWithValue("@P8", mskTel3.Text);
            cmd.Parameters.AddWithValue("@P9", txtMail.Text);
            cmd.Parameters.AddWithValue("@P10", mskFax.Text);
            cmd.Parameters.AddWithValue("@P11", cmbIl.Text);
            cmd.Parameters.AddWithValue("@P12", cmbIlce.Text);
            cmd.Parameters.AddWithValue("@P13", txtVergi.Text);
            cmd.Parameters.AddWithValue("@P14", rchAdres.Text);
            cmd.Parameters.AddWithValue("@P15", txtKod1.Text);
            cmd.Parameters.AddWithValue("@P16", txtKod2.Text);
            cmd.Parameters.AddWithValue("@P17", txtKod3.Text);
            cmd.Parameters.AddWithValue("@P18", txtId.Text);
            cmd.ExecuteNonQuery();
            dataAccess.baglanti().Close();
            MessageBox.Show("Firma güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            firmaListesi();
            temizle();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            temizle();

        } 
    }
}