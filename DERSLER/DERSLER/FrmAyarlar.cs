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
    public partial class FrmAyarlar : DevExpress.XtraEditors.XtraForm
    {
        public FrmAyarlar()
        {
            InitializeComponent();
        }
        DataAccess dataAccess = new DataAccess();
        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_ADMIN", dataAccess.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        private void FrmAyarlar_Load(object sender, EventArgs e)
        {
            listele();
            txtKullaniciAdi.Text = "";
            txtSifre.Text = "";
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {

            if (btnKaydet.Text == "KAYDET")
            {
                SqlCommand cmd = new SqlCommand("insert into TBL_ADMIN(kullaniciAdi,sifre)values(@p1,@p2)", dataAccess.baglanti());
                cmd.Parameters.AddWithValue("@p1", txtKullaniciAdi.Text);
                cmd.Parameters.AddWithValue("@p2", txtSifre.Text);
                cmd.ExecuteNonQuery();
                dataAccess.baglanti().Close();
                MessageBox.Show("Kullanıcı Kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
            if (btnKaydet.Text == "GUNCELLE") 
            {
                SqlCommand cmd = new SqlCommand("update TBL_ADMIN set sifre=@p2 where kullaniciAdi=@p1 ", dataAccess.baglanti());
                cmd.Parameters.AddWithValue("@p1", txtKullaniciAdi.Text);
                cmd.Parameters.AddWithValue("@p2", txtSifre.Text);
                cmd.ExecuteNonQuery();
                dataAccess.baglanti().Close();
                MessageBox.Show("Kullanici Güncellendi!!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();

            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr!=null)
            {
                txtKullaniciAdi.Text = dr["kullaniciAdi"].ToString();
                txtSifre.Text = dr["sifre"].ToString();
            }
            
        }

        private void txtKullaniciAdi_TextChanged(object sender, EventArgs e)
        {
            if (txtKullaniciAdi.Text != "")
            {
                btnKaydet.Text = "GUNCELLE";
                btnKaydet.BackColor = Color.GreenYellow;
            }
            else
            {
                btnKaydet.Text = "KAYDET";
                btnKaydet.BackColor = Color.SkyBlue;

            }
        }
    }
}