using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
namespace MiniFtpServer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public string siteAdi = "ftp.sanalkod.net"; //örn. csharptr.com
        public string kullaniciAdi = "sanalkod"; //k.adi
        public string sifre = "mgnfcnt"; //sifre
        //yukarıda FTP bilgilerinizi giriniz

        private void Upload(string FtpServer, string Username, string Password, string filename)
        {
            FileInfo fileInf = new FileInfo(filename);
            string uri = "ftp://" + FtpServer + "//foto//" + fileInf.Name;//Burada uplaod ediceğiniz dizini tam oalrak belirtmelisiniz.
            FtpWebRequest reqFTP;
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
            reqFTP.Credentials = new NetworkCredential(Username, Password);
            reqFTP.KeepAlive = false;
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
            reqFTP.UseBinary = true;
            reqFTP.ContentLength = fileInf.Length;
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;
            FileStream fs = fileInf.OpenRead();

            try
            {
                Stream strm = reqFTP.GetRequestStream();
                contentLen = fs.Read(buff, 0, buffLength);
                while (contentLen != 0)
                {
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }
                strm.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Upload Error");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Upload("ftp.sanalkod.net","sanalkod","mgnfcnt","C:\\Users\\Mgnfcnt\\Desktop\\oyunresim\\DSC_0091.JPG");
        }
    }
}
