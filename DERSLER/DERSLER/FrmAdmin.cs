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
    public partial class FrmAdmin : DevExpress.XtraEditors.XtraForm
    {
        public FrmAdmin()
        {
            InitializeComponent();
        }
        DataAccess dataAccess = new DataAccess();
        private void btnGirisYap_Click(object sender, EventArgs e)
        {

            girisYap();
        }

        bool loginSuccess = false;
        void girisYap()
        {
            SqlCommand cmd = new SqlCommand("Select * from TBL_ADMIN where kullaniciAdi=@p1 and sifre=@p2", dataAccess.baglanti());
            cmd.Parameters.AddWithValue("@p1", txtKullaniciAdi.Text);
            cmd.Parameters.AddWithValue("@p2", txtSifre.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                loginSuccess = true;
                //FrmAnaModul fr = new FrmAnaModul(); Yeni algoritmada anamodul load olurken giriş panelini çağırdığımız için burada oluşturmuyoruz. Giriş paneli kapandığında ana modül çalışır durumda olacak. Yani parent form = AnaModül
                //KASA MODULUNUN ICINDEKİ LABELA KULLANICI ADINI ATAMA.
                //fr.kullanici = txtKullaniciAdi.Text;
                FrmAnaModul.kullanici = txtKullaniciAdi.Text;
                //fr.Show();
                this.Close();
            }
            else
            {
                loginSuccess = false;
                MessageBox.Show("Hatalı kullanıcı adı ya da şifre", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            dataAccess.baglanti().Close();
        }

        private void btnGirisYap_MouseHover(object sender, EventArgs e)
        {
            btnGirisYap.BackColor = Color.Aquamarine;
        }

        private void btnGirisYap_MouseLeave(object sender, EventArgs e)
        {
            btnGirisYap.BackColor = Color.RoyalBlue;

        }

        private void txtSifre_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                girisYap();
            }
        }

        private void FrmAdmin_FormClosing(object sender, FormClosingEventArgs e)//FrmAdmin kapanırken bu event çalışacak. (alt+f4 ile kapatma güvenlik açığını aşmak için)
        {
            if (!loginSuccess)
            {
                //Giriş başarısız. Programı kapatıyoruz.
                Application.Exit();
            }
            else
            {
                //giriş başarılı hiç bir şey yapmıyouz
            }
        }
    }
}