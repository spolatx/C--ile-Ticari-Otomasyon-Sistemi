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

namespace DERSLER
{
    public partial class FrmNotDetay : DevExpress.XtraEditors.XtraForm
    {
        public FrmNotDetay()
        {
            InitializeComponent();
        }
        public string metin;

        private void FrmNotDetay_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = metin;
        }
    }
}