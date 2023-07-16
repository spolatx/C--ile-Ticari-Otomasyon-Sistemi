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
    public partial class FrmGiderler : DevExpress.XtraEditors.XtraForm
    {
        public FrmGiderler()
        {
            InitializeComponent();
        }
        DataAccess dataAccess = new DataAccess();
        void listele() {
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_GIDERLER",dataAccess.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void giderListesi()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_GIDERLER Order By ID ASC", dataAccess.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void temizle()
        {
            txtId.Text = "";
            txtAy.Text = "";
            txtDogalgaz.Text = "";
            txtEkstra.Text = "";
            txtElektrik.Text = "";
            txtInternet.Text = "";
            txtMaaslar.Text = "";
            txtSu.Text = "";
            txtYıl.Text = "";
            rchNotlar.Text = "";
        }

        private void FrmGiderler_Load(object sender, EventArgs e)
        {
            giderListesi();
            listele();
            temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr!=null)
            { 
                txtId.Text = dr["ID"].ToString();
                txtAy.Text = dr["AY"].ToString();
                txtYıl.Text = dr["YIL"].ToString();
                txtElektrik.Text=dr["ELEKTRIK"].ToString();
                txtSu.Text=dr["SU"].ToString();
                txtDogalgaz.Text=dr["DOGALGAZ"].ToString();
                txtInternet.Text=dr["INTERNET"].ToString();
                txtMaaslar.Text=dr["MAASLAR"].ToString();
                txtEkstra.Text = dr["EKSTRA"].ToString();
                rchNotlar.Text = dr["NOTLAR"].ToString();

            }
           
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("insert into TBL_GIDERLER(AY,YIL,ELEKTRIK,SU,DOGALGAZ,INTERNET,MAASLAR,EKSTRA,NOTLAR)" +
                "values(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)", dataAccess.baglanti());
            cmd.Parameters.AddWithValue("@p1",txtAy.Text);
            cmd.Parameters.AddWithValue("@p2", txtYıl.Text);
            cmd.Parameters.AddWithValue("@p3", decimal.Parse(txtElektrik.Text));
            cmd.Parameters.AddWithValue("@p4", decimal.Parse(txtSu.Text));
            cmd.Parameters.AddWithValue("@p5", decimal.Parse(txtDogalgaz.Text));
            cmd.Parameters.AddWithValue("@p6", decimal.Parse(txtInternet.Text));
            cmd.Parameters.AddWithValue("@p7", decimal.Parse(txtMaaslar.Text));
            cmd.Parameters.AddWithValue("@p8", decimal.Parse(txtEkstra.Text));
            cmd.Parameters.AddWithValue("@p9", rchNotlar.Text);
            cmd.ExecuteNonQuery();
            dataAccess.baglanti().Close();
            MessageBox.Show("Giderler eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Delete from TBL_GIDERLER where ID=@p1",dataAccess.baglanti());
            cmd.Parameters.AddWithValue("@p1", txtId.Text);
            cmd.ExecuteNonQuery();
            dataAccess.baglanti().Close();
            MessageBox.Show("Giderler Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
            temizle();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("update TBL_GIDERLER set AY=@p1,YIL=@p2,ELEKTRIK=@p3,SU=@p4,DOGALGAZ=@p5," +
                "INTERNET=@p6,MAASLAR=@p7,EKSTRA=@p8,NOTLAR=@p9 where ID=@p10", dataAccess.baglanti());
            cmd.Parameters.AddWithValue("@p1", txtAy.Text);
            cmd.Parameters.AddWithValue("@p2", txtYıl.Text);
            cmd.Parameters.AddWithValue("@p3", decimal.Parse(txtElektrik.Text));
            cmd.Parameters.AddWithValue("@p4", decimal.Parse(txtSu.Text));
            cmd.Parameters.AddWithValue("@p5", decimal.Parse(txtDogalgaz.Text));
            cmd.Parameters.AddWithValue("@p6", decimal.Parse(txtInternet.Text));
            cmd.Parameters.AddWithValue("@p7", decimal.Parse(txtMaaslar.Text));
            cmd.Parameters.AddWithValue("@p8", decimal.Parse(txtEkstra.Text));
            cmd.Parameters.AddWithValue("@p9", rchNotlar.Text);
            cmd.Parameters.AddWithValue("@p10", txtId.Text);
            cmd.ExecuteNonQuery();
            dataAccess.baglanti().Close();
            MessageBox.Show("Giderler Guncellendi!!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }
    }
}