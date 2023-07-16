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
using DevExpress.Charts;

namespace DERSLER
{
    public partial class FrmKasa : DevExpress.XtraEditors.XtraForm
    {
        public FrmKasa()
        {
            InitializeComponent();
        }
        DataAccess dataAccess = new DataAccess();

        void musteriHareket()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("exec MusteriHareketler", dataAccess.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        public string ad;


        void firmaHareketleri()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Exec FirmaHareketler", dataAccess.baglanti());
            da.Fill(dt);
            gridControl3.DataSource = dt;
        }
        void giderler()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_GIDERLER", dataAccess.baglanti());
            da.Fill(dt);
            gridControl2.DataSource = dt;
        }

        private void FrmKasa_Load(object sender, EventArgs e)
        {
            lblAktifKullanici.Text = ad;

            firmaHareketleri();
            musteriHareket();
            giderler();

            //Toplam tutarı Hesaplama
            SqlCommand cmd = new SqlCommand("Select Sum(TUTAR) from TBL_FATURADETAY",dataAccess.baglanti());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblToplamTutar.Text = dr[0].ToString()+" TL";
            }
            dataAccess.baglanti().Close();

            //SON AYIN FATURALARI
            SqlCommand cmd2 = new SqlCommand("Select (ELEKTRIK+SU+DOGALGAZ+INTERNET+EKSTRA) from TBL_GIDERLER order by ID asc", dataAccess.baglanti());
            SqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                lblOdemeler.Text = dr2[0].ToString()+" TL";
            }
            dataAccess.baglanti().Close();

            //SON AYIN PERSONEL MAASLARI
            SqlCommand cmd3 = new SqlCommand("Select MAASLAR from TBL_GIDERLER order by ID asc", dataAccess.baglanti());
            SqlDataReader dr3 = cmd3.ExecuteReader();
            while (dr3.Read())
            {
                lblPersonelMaaslar.Text = dr3[0].ToString();
            }
            dataAccess.baglanti().Close();

            //TOPLAM MUSTERI SAYISI
            SqlCommand cmd4 = new SqlCommand("Select Count(*) from TBL_MUSTERILER", dataAccess.baglanti());
            SqlDataReader dr4 = cmd4.ExecuteReader();
            while (dr4.Read())
            {
                lblMusteriSayisi.Text = dr4[0].ToString();
            }
            dataAccess.baglanti().Close();

            //TOPLAM FIRMA SAYISI
            SqlCommand cmd5 = new SqlCommand("Select Count(*)from TBL_FIRMALAR", dataAccess.baglanti());
            SqlDataReader dr5 = cmd5.ExecuteReader();
            while (dr5.Read())
            {
                lblFirmaSayisi.Text = dr5[0].ToString();
            }
            dataAccess.baglanti().Close();

            //FIRMA SEHIR SAYISI 
            SqlCommand cmd6 = new SqlCommand("Select Count(Distinct(IL)) from TBL_FIRMALAR", dataAccess.baglanti());
            SqlDataReader dr6 = cmd6.ExecuteReader();
            while (dr6.Read())
            {
                lblSehirSayisi.Text = dr6[0].ToString();
            }
            dataAccess.baglanti().Close();
            //MUSTERI SEHIR SAYISI
            SqlCommand cmd7 = new SqlCommand("Select Count(Distinct(IL)) from TBL_MUSTERILER", dataAccess.baglanti());
            SqlDataReader dr7 = cmd7.ExecuteReader();
            while (dr7.Read())
            {
                lblSehirSayisi2.Text = dr7[0].ToString();
            }
            dataAccess.baglanti().Close();

            //PERSONEL SAYISI 
            SqlCommand cmd8 = new SqlCommand("Select Count(*) from TBL_PERSONELLER", dataAccess.baglanti());
            SqlDataReader dr8 = cmd8.ExecuteReader();
            while (dr8.Read()) 
            {
                lblPersonelSayisi.Text = dr8[0].ToString();
            }
            dataAccess.baglanti().Close();

            //STOK SAYISI
            SqlCommand cmd9 = new SqlCommand("Select sum(ADET) from TBL_URUNLER", dataAccess.baglanti());
            SqlDataReader dr9 = cmd9.ExecuteReader();
            while (dr9.Read())
            {
                lblStokSayisi.Text = dr9[0].ToString();
            }
            dataAccess.baglanti().Close();

        }

        private void chartControl1_Click(object sender, EventArgs e)
        {

        }

        int sayac = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            sayac++;
            //SON 4 AYIN ELEKTRIK FATURASINI LISTELEME
            if (sayac>0&&sayac<=5)
            {
               
                groupControl10.Text = "Elektrik";
                SqlCommand cmd10 = new SqlCommand("Select top 4 Ay,ELEKTRIK from TBL_GIDERLER order by ID DESC", dataAccess.baglanti());
                SqlDataReader dr10 = cmd10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl1.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));

                }
                dataAccess.baglanti().Close();
            }
            //SON 4 AYIN SU FATURASINI LISTELEME
            if (sayac>5 && sayac<=10)
            {
                groupControl10.Text = "Su";
                chartControl1.Series["AYLAR"].Points.Clear();
                //2. CHART CONTROLE SU FATURASI SON 4 AY LISTELEME
                SqlCommand cmd11 = new SqlCommand("Select Top 4 Ay,Su from TBL_GIDERLER order by ID desc", dataAccess.baglanti());
                SqlDataReader dr11 = cmd11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl1.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                dataAccess.baglanti().Close();
            }
            //SON 4 AYIN DOGALGAZ FATURASINI LISTELEME
            if (sayac>11&&sayac<=15)
            {
                groupControl10.Text = "Dogalgaz";
                SqlCommand cmd12 = new SqlCommand("Select top 4 Ay,DOGALGAZ from TBL_GIDERLER order by ID desc", dataAccess.baglanti());
                SqlDataReader dr12 = cmd12.ExecuteReader();
                while (dr12.Read())
                {
                    chartControl1.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr12[0], dr12[1]));
                }
                dataAccess.baglanti().Close();
            }

            //SON 4 AYIN INTERNET FATURASINI LISTELEME
            if (sayac > 15 && sayac <= 20)
            {
                groupControl10.Text = "İnternet";
                SqlCommand cmd12 = new SqlCommand("Select top 4 Ay,INTERNET from TBL_GIDERLER order by ID desc", dataAccess.baglanti());
                SqlDataReader dr12 = cmd12.ExecuteReader();
                while (dr12.Read())
                {
                    chartControl1.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr12[0], dr12[1]));
                }
                dataAccess.baglanti().Close();
            }
            if (sayac > 20 && sayac <= 25)
            {
                groupControl10.Text = "Ekstra";
                SqlCommand cmd12 = new SqlCommand("Select top 4 Ay,Ekstra from TBL_GIDERLER order by ID desc", dataAccess.baglanti());
                SqlDataReader dr12 = cmd12.ExecuteReader();
                while (dr12.Read())
                {
                    chartControl1.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr12[0], dr12[1]));
                }
                dataAccess.baglanti().Close();
            }
            if (sayac==26)
            {
                sayac = 0;
            }
        }
        int sayac2;
        private void timer2_Tick(object sender, EventArgs e)
        {
            sayac2++;
            //SON 4 AYIN ELEKTRIK FATURASINI LISTELEME
            if (sayac > 0 && sayac <= 5)
            {

                groupControl11.Text = "Elektrik";
                SqlCommand cmd10 = new SqlCommand("Select top 4 Ay,ELEKTRIK from TBL_GIDERLER order by ID DESC", dataAccess.baglanti());
                SqlDataReader dr10 = cmd10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl2.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));

                }
                dataAccess.baglanti().Close();
            }
            //SON 4 AYIN SU FATURASINI LISTELEME
            if (sayac2 > 5 && sayac2 <= 10)
            {
                groupControl11.Text = "Su";
                chartControl2.Series["AYLAR"].Points.Clear();
                //2. CHART CONTROLE SU FATURASI SON 4 AY LISTELEME
                SqlCommand cmd11 = new SqlCommand("Select Top 4 Ay,Su from TBL_GIDERLER order by ID desc", dataAccess.baglanti());
                SqlDataReader dr11 = cmd11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl2.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                dataAccess.baglanti().Close();
            }
            //SON 4 AYIN DOGALGAZ FATURASINI LISTELEME
            if (sayac2 > 11 && sayac2 <= 15)
            {
                groupControl11.Text = "Dogalgaz";
                SqlCommand cmd12 = new SqlCommand("Select top 4 Ay,DOGALGAZ from TBL_GIDERLER order by ID desc", dataAccess.baglanti());
                SqlDataReader dr12 = cmd12.ExecuteReader();
                while (dr12.Read())
                {
                    chartControl2.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr12[0], dr12[1]));
                }
                dataAccess.baglanti().Close();
            }

            //SON 4 AYIN INTERNET FATURASINI LISTELEME
            if (sayac2 > 15 && sayac2 <= 20)
            {
                groupControl11.Text = "İnternet";
                SqlCommand cmd12 = new SqlCommand("Select top 4 Ay,INTERNET from TBL_GIDERLER order by ID desc", dataAccess.baglanti());
                SqlDataReader dr12 = cmd12.ExecuteReader();
                while (dr12.Read())
                {
                    chartControl2.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr12[0], dr12[1]));
                }
                dataAccess.baglanti().Close();
            }
            if (sayac2 > 20 && sayac2 <= 25)
            {
                groupControl11.Text = "Ekstra";
                SqlCommand cmd12 = new SqlCommand("Select top 4 Ay,Ekstra from TBL_GIDERLER order by ID desc", dataAccess.baglanti());
                SqlDataReader dr12 = cmd12.ExecuteReader();
                while (dr12.Read())
                {
                    chartControl2.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr12[0], dr12[1]));
                }
                dataAccess.baglanti().Close();
            }
            if (sayac2 == 26)
            {
                sayac2 = 0;
            }
        }

       
    }
}