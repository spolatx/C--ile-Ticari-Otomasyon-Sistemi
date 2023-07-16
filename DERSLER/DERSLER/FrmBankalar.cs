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
    public partial class FrmBankalar : DevExpress.XtraEditors.XtraForm
    {
        public FrmBankalar()
        {
            InitializeComponent();
        }
        DataAccess dataAccess = new DataAccess();
        void listele() {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("execute BankaBilgileri", dataAccess.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void firmaListesi()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select ID,AD from TBL_FIRMALAR", dataAccess.baglanti());
            da.Fill(dt);
            lookUpFirma.Properties.ValueMember = "ID";
            lookUpFirma.Properties.DisplayMember = "AD";
            lookUpFirma.Properties.DataSource = dt;

        }
        void sehirListesi()
        {
            SqlCommand cmd = new SqlCommand("Select sehir from TBL_ILLER", dataAccess.baglanti());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmbIl.Properties.Items.Add(dr[0]);

            }
            dataAccess.baglanti().Close();
        }
        void temizle() {
           
            txtBankaAdi.Text = "";
            txtHesapTuru.Text = "";
            txtIban.Text = "";
            txtId.Text = "";
            txtSube.Text = "";
            txtYetkili.Text = "";
            mskHesapNo.Text = "";
            mskTarih.Text = "";
            mskTelefon.Text = "";
            cmbIl.Text = "";
            cmbIlce.Text = "";
            lookUpFirma.Text = "";
    
        }
        private void FrmBankalar_Load(object sender, EventArgs e)
        {
            listele();
            sehirListesi();
            firmaListesi();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null) {

                txtId.Text = dr["ID"].ToString();
                txtBankaAdi.Text = dr["BANKAADI"].ToString();
                cmbIl.Text = dr["IL"].ToString();
                cmbIlce.Text = dr["ILCE"].ToString();
                txtSube.Text = dr["SUBE"].ToString();
                txtIban.Text = dr["IBAN"].ToString();
                mskHesapNo.Text = dr["HESAPNO"].ToString();
                txtYetkili.Text = dr["YETKILI"].ToString();
                mskTelefon.Text = dr["TELEFON"].ToString();
                mskTarih.Text = dr["TARIH"].ToString();
                txtHesapTuru.Text = dr["HESAPTURU"].ToString();
        
          
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("insert into TBL_BANKALAR " +
                "(BANKAADI,IL,ILCE,SUBE,IBAN,HESAPNO,YETKILI,TELEFON,TARIH,HESAPTURU,FIRMAID)" +
                " VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11)", dataAccess.baglanti());
            cmd.Parameters.AddWithValue("@P1",txtBankaAdi.Text);
            cmd.Parameters.AddWithValue("@P2", cmbIl.Text);
            cmd.Parameters.AddWithValue("@P3", cmbIlce.Text);
            cmd.Parameters.AddWithValue("@P4", txtSube.Text);
            cmd.Parameters.AddWithValue("@P5", txtIban.Text);
            cmd.Parameters.AddWithValue("@P6", mskHesapNo.Text);
            cmd.Parameters.AddWithValue("@P7", txtYetkili.Text);
            cmd.Parameters.AddWithValue("@P8", mskTelefon.Text);
            cmd.Parameters.AddWithValue("@P9", mskTarih.Text);
            cmd.Parameters.AddWithValue("@P10", txtHesapTuru.Text);
            cmd.Parameters.AddWithValue("@P11", lookUpFirma.EditValue);
            cmd.ExecuteNonQuery();
            dataAccess.baglanti().Close();
            MessageBox.Show("Banka eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        //Secili ilin ilcelerini getiren metot
        private void cmbIl_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbIl.Properties.Items.Clear();
            SqlCommand cmd = new SqlCommand("Select ilce from TBL_ILCELER where sehir=@p1", dataAccess.baglanti());
            cmd.Parameters.AddWithValue("@p1", cmbIl.SelectedIndex+1);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmbIlce.Properties.Items.Add(dr[0]);
            }
            dataAccess.baglanti().Close();

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("update TBL_BANKALAR set BANKAADI=@P1,IL=@P2,ILCE=@P3,SUBE=@P4,IBAN=@P5" +
                ",HESAPNO=@P6,YETKILI=@P7,TELEFON=@P8,TARIH=@P9,HESAPTURU=@P10,FIRMAID=@P11 where ID=@P12",dataAccess.baglanti());
            cmd.Parameters.AddWithValue("@P1", txtBankaAdi.Text);
            cmd.Parameters.AddWithValue("@P2", cmbIl.Text);
            cmd.Parameters.AddWithValue("@P3", cmbIlce.Text);
            cmd.Parameters.AddWithValue("@P4", txtSube.Text);
            cmd.Parameters.AddWithValue("@P5", txtIban.Text);
            cmd.Parameters.AddWithValue("@P6", mskHesapNo.Text);
            cmd.Parameters.AddWithValue("@P7", txtYetkili.Text);
            cmd.Parameters.AddWithValue("@P8", mskTelefon.Text);
            cmd.Parameters.AddWithValue("@P9", mskTarih.Text);
            cmd.Parameters.AddWithValue("@P10", txtHesapTuru.Text);
            cmd.Parameters.AddWithValue("@P11", lookUpFirma.EditValue);
            cmd.Parameters.AddWithValue("@P12", txtId.Text);    
            cmd.ExecuteNonQuery();
            dataAccess.baglanti().Close();
            MessageBox.Show("Banka Güncellendi!!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Delete from TBL_BANKALAR where ID=@p1", dataAccess.baglanti());
            cmd.Parameters.AddWithValue("@p1", txtId.Text);
            cmd.ExecuteNonQuery();
            dataAccess.baglanti().Close();
            MessageBox.Show("Banka bilgileri sistemden silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            listele();
        }
    }
}