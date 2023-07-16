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
using System.Net;
using System.Net.Mail;

namespace DERSLER
{
    public partial class FrmMail : DevExpress.XtraEditors.XtraForm
    {
        public FrmMail()
        {
            InitializeComponent();
        }
        public string mail;
        private void FrmMail_Load(object sender, EventArgs e)
        {
            txtMail.Text = mail;
        }

       

        private void btnGonder_Click(object sender, EventArgs e)
        {
            MailMessage message = new MailMessage();
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("spolat953@gmail.com", "******");
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            message.To.Add(txtMail.Text);
            message.From = new MailAddress("spolat953@gmail.com");
            message.Subject = txtKonu.Text;
            message.Body = rchMesaj.Text;
            message.IsBodyHtml = true;
            client.Send(message);
            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                throw new Exception("Mail gönderimi başarısız oldu: " + ex.Message);
            }
        }
    }
}