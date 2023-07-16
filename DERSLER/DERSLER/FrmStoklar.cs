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
    public partial class FrmStoklar : DevExpress.XtraEditors.XtraForm
    {
        public FrmStoklar()
        {
            InitializeComponent();
        }
        DataAccess dataAccess = new DataAccess();
        private void FrmStoklar_Load(object sender, EventArgs e)
        {
            //chartControl1.Series["Series 1"].Points.AddPoint("İstanbul", 4);
            //chartControl1.Series["Series 1"].Points.AddPoint("İzmir", 6);
            //chartControl1.Series["Series 1"].Points.AddPoint("Adana", 5);

            SqlDataAdapter da = new SqlDataAdapter("Select UrunAd,Sum(Adet) as 'Miktar'" +
                " from TBL_URUNLER group by UrunAd", dataAccess.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;

            //Charta Stok miktarı listeleme
            SqlCommand cmd = new SqlCommand("Select UrunAd, Sum(Adet) as 'Miktar'" +
                " from TBL_URUNLER group by UrunAd", dataAccess.baglanti());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                chartControl1.Series["Series 1"].Points.AddPoint(Convert.ToString(dr[0]),int.Parse(dr[1].ToString()));

            }
            dataAccess.baglanti().Close();

            //Charta firma-şehir sayısı çekme
            SqlCommand cmd1 = new SqlCommand("Select IL,Count(*) from TBL_FIRMALAR group by IL", dataAccess.baglanti());
            SqlDataReader dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                chartControl2.Series["Series 1"].Points.AddPoint(Convert.ToString(dr1[0]), int.Parse(dr1[1].ToString()));
            }
            dataAccess.baglanti().Close();

        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmStokDetay fr = new FrmStokDetay();
            //BİR NOKTAYA TIKLAYIP SEÇTİĞİM ZAMAN ALAN BOŞ DEĞİL İSE STOK DETAY FORMUNU AÇ.
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr!=null)
            {
                fr.ad = dr["URUNAD"].ToString();
            }
            fr.Show();



        }
    }
}