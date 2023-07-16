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
using System.Xml;

namespace DERSLER
{
    public partial class FrmAnaSayfa : DevExpress.XtraEditors.XtraForm
    {
        public FrmAnaSayfa()
        {
            InitializeComponent();
        }
        DataAccess dataAccess = new DataAccess();
        void stokListele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select Urunad,Sum(Adet) as 'Adet' From TBL_URUNLER" +
                " group by Urunad having Sum(Adet)<=20 order by Sum(Adet)",dataAccess.baglanti());
            da.Fill(dt);
            gridControlStoklar.DataSource = dt;
        }
        void ajandaListele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select TOP 5 NOTTARIH,NOTSAAT,NOTBASLIK FROM TBL_NOTLAR ORDER BY NOTID DESC", dataAccess.baglanti());
            da.Fill(dt);
            gridControlAjanda.DataSource = dt;
        }
        void firmaHareket()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Exec FirmaHareket2", dataAccess.baglanti());
            da.Fill(dt);
            gridControlFirmaHareket.DataSource = dt;
        }
        //FIRMALARIN TELEFON NUMARALARINI GETIREN METHOD
        void fihristListele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select Ad,Telefon1 From TBL_FIRMALAR", dataAccess.baglanti());
            da.Fill(dt);
            gridControlFihrist.DataSource = dt;
        }
        void haberler()
        {
            //xmldeki dosyaları okur.
            XmlTextReader xmlOku = new XmlTextReader("http://www.hurriyet.com.tr/rss/anasayfa");
            while (xmlOku.Read())
            {
                if (xmlOku.Name=="title")
                {
                    //okuduğu methodu string formatta yazdır.
                    listBox1.Items.Add(xmlOku.ReadString());
                }
            }

        }

        private void FrmAnaSayfa_Load(object sender, EventArgs e)
        {
            stokListele();
            ajandaListele();
            firmaHareket();
            fihristListele();
            haberler();
            //web browser yönlendirme
            webBrowser1.Navigate("http://www.tcmb.gov.tr/kurlar/today.xml");
        }
    }
}