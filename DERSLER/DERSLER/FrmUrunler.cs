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
    public partial class URUNLER : DevExpress.XtraEditors.XtraForm
    {
        public URUNLER()
        {
            InitializeComponent();
        }

        private void labelControl6_Click(object sender, EventArgs e)
        {

        }

        private void labelControl5_Click(object sender, EventArgs e)
        {

        }

        DataAccess dtAccess = new DataAccess();

        void listele()
        {
            //tabloyu sql komutuyla getir ve grid içerisine ata.
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_URUNLER",dtAccess.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }


        private void URUNLER_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            //Verileri Kaydetme
            SqlCommand cmd = new SqlCommand("insert into TBL_URUNLER(URUNAD,MARKA,MODEL," +
                "YIL,ADET,ALISFIYAT,SATISFIYAT,DETAY) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)",dtAccess.baglanti());
            cmd.Parameters.AddWithValue("@p1", txtAd.Text);
            cmd.Parameters.AddWithValue("@p2", txtMarka.Text);
            cmd.Parameters.AddWithValue("@p3", txtModel.Text);
            cmd.Parameters.AddWithValue("@p4", mskYil.Text);
            //veritabanında decimal olduğu için veritabanına uygun şekilde donuşturduk.
            cmd.Parameters.AddWithValue("@p5", int.Parse((NudAdet.Value).ToString()));
            cmd.Parameters.AddWithValue("@p6", decimal.Parse((txtAlis.Text)));
            cmd.Parameters.AddWithValue("@p7" , decimal.Parse((txtSatis.Text)));
            cmd.Parameters.AddWithValue("@p8", rchDetay.Text);
            //veritabanı komutlarını çalıştır bağlantiyi kapat.
            cmd.ExecuteNonQuery();
            dtAccess.baglanti().Close();
            MessageBox.Show("Urun sisteme eklendi.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            listele();

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komutSil = new SqlCommand("Delete from TBL_URUNLER where ID=@p1"
                , dtAccess.baglanti());
            komutSil.Parameters.AddWithValue("@p1", txtId.Text);
            komutSil.ExecuteNonQuery();
            dtAccess.baglanti().Close();
            MessageBox.Show("Ürün Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //gridview içerisinde satırın verisini al. focuslandiğimiz indexi al.
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            txtId.Text = dr["ID"].ToString();
            txtAd.Text = dr["URUNAD"].ToString();
            txtMarka.Text = dr["MARKA"].ToString();
            txtModel.Text = dr["MODEL"].ToString();
            mskYil.Text = dr["YIL"].ToString();
            NudAdet.Value = decimal.Parse(dr["ADET"].ToString());
            txtAlis.Text = dr["ALISFIYAT"].ToString();
            txtSatis.Text = dr["SATISFIYAT"].ToString();
            rchDetay.Text = dr["DETAY"].ToString();

        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komutGuncelle = new SqlCommand("update TBL_URUNLER set " +
                "URUNAD=@P1,MARKA=@P2,MODEL=@P3,YIL=@P4,ADET=@P5,ALISFIYAT=@P6,SATISFIYAT" +
                "=@P7,DETAY=@P8 where ID=@P9", dtAccess.baglanti());
            komutGuncelle.Parameters.AddWithValue("@P1", txtAd.Text);
            komutGuncelle.Parameters.AddWithValue("@P2", txtMarka.Text);
            komutGuncelle.Parameters.AddWithValue("@P3", txtModel.Text);
            komutGuncelle.Parameters.AddWithValue("@P4", mskYil.Text);
            komutGuncelle.Parameters.AddWithValue("@P5", int.Parse((NudAdet.Value).ToString()));
            komutGuncelle.Parameters.AddWithValue("@P6", decimal.Parse(txtAlis.Text));
            komutGuncelle.Parameters.AddWithValue("@P7", decimal.Parse(txtSatis.Text));
            komutGuncelle.Parameters.AddWithValue("@P8", rchDetay.Text);
            komutGuncelle.Parameters.AddWithValue("@P9", txtId.Text);
            komutGuncelle.ExecuteNonQuery();
            dtAccess.baglanti().Close();
            MessageBox.Show("Urun bilgisi guncellendi", "bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();

        }
    }
}