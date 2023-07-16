using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DERSLER
{
    public partial class FrmAnaModul : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public FrmAnaModul()
        {
            InitializeComponent();
        }

        URUNLER fr;
        private void btnUrunler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr==null)
            {
                fr = new URUNLER();
                fr.MdiParent = this;
                fr.Show();
            }
            
        }

        FrmMusteriler fr2;
        private void btnMusteriler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr2 == null)
            {
                fr2 = new FrmMusteriler();
                fr2.MdiParent = this;
                fr2.Show();
            }
        }

        FrmFirmalar fr3;
        private void btnFirmalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr3 == null) {
                fr3 = new FrmFirmalar();
                fr3.MdiParent = this;
                fr3.Show();
            }
        }

        FrmPersonel fr4;
        private void btnPersoneller_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr4 == null) {
                fr4 = new FrmPersonel();
                fr4.MdiParent = this;
                fr4.Show();
            }
        }

        FrmRehber fr5;
        private void btnRehber_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr5 == null)
            {
                fr5 = new FrmRehber();
                fr5.MdiParent = this;
                fr5.Show();
            }
        }

        FrmGiderler fr6;
        private void btnGıderler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr6==null)
            {
                fr6 = new FrmGiderler();
                fr6.MdiParent = this;
                fr6.Show();
            }
        }

        FrmBankalar fr7;
        private void btnBankalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr7==null)
            {
                fr7 = new FrmBankalar();
                fr7.MdiParent = this;
                fr7.Show();
            }

        }


        FrmFaturalar fr8;
        private void btnFaturalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            fr8 = new FrmFaturalar();
            fr8.MdiParent = this;
            fr8.Show();
        }

        FrmNotlar fr9;
        private void btnNotlar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            fr9 = new FrmNotlar();
            fr9.MdiParent = this;
            fr9.Show();
        }
        FrmHareketler fr10;
        private void btnHareketler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            fr10 = new FrmHareketler();
            fr10.MdiParent = this;
            fr10.Show();
        }

        FrmStoklar fr12;
        private void btnStoklar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            fr12 = new FrmStoklar();
            fr12.MdiParent = this;
            fr12.Show();
        }

        FrmRaporlar fr11;
        private void btnRaporlar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            fr11 = new FrmRaporlar();
            fr11.MdiParent = this;
            fr11.Show();
        }

        FrmAyarlar fr13;
        private void btnAyarlar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            fr13 = new FrmAyarlar();

            fr13.Show();
        }

        FrmKasa fr14;
        private void btnKasa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            fr14 = new FrmKasa();
            fr14.MdiParent = this;
            fr14.ad = kullanici;
            fr14.Show();
        }
        public static string kullanici;
        private void FrmAnaModul_Load(object sender, EventArgs e)
        {
            FrmAdmin frmAdmin = new FrmAdmin();
            frmAdmin.ShowDialog();

            if (fr15 == null)
            {
                fr15 = new FrmAnaSayfa();
                fr15.MdiParent = this;
                fr15.Show();
            }
        }

        FrmAnaSayfa fr15;
        private void btnAnaSayfa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr15==null)
            {
                fr15 = new FrmAnaSayfa();
                fr15.MdiParent = this;
                fr15.Show();
            }
           
        }

       
        private void xtraTabbedMdiManager1_PageRemoved(object sender, DevExpress.XtraTabbedMdi.MdiTabPageEventArgs e)
        {
            fr = null;
            fr2 = null; 
            fr3= null; 
            fr4 = null; 
            fr5 = null; 
            fr6 = null; 
            fr7 = null; 
            fr8 = null; 
            fr9 = null; 
            fr10 = null; 
            fr11 = null; 
            fr12 = null; 
            fr13 = null; 
            fr14 = null; 
            fr15 = null; 
            
        }
    }
}
