using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace DUMeterMZ
{
    public class SendEmail
    {
        private const string GMAIL_HOST = "smtp.gmail.com";
        private const string GMAIL_FROM = "zbarjevsky@gmail.com";
        private const string GMAIL_TO = "nata.rz@gmail.com";
        private const string GMAIL_DUMMY = "Big.Brother@gmail.com";
        private const string GMAIL_PWD = "";
        private const string IP_URL = @"http://www.canyouseeme.org/";
        private const string ERROR_PREFIX = "<html>Error: ";

		private Options m_Options;
		private string m_sIP = String.Empty;

		public SendEmail(Options opt)
		{
			m_Options = opt;
		}

        public void SendIPEmailMessageThread()
        {
			if (string.IsNullOrEmpty(m_Options.EMailForIP) || string.IsNullOrEmpty(m_Options.EMailFrom))
				return;

            new Thread(new ThreadStart(SendIPEmailMessage)).Start();
        }

        private void SendIPEmailMessage()
        {
            try
            {
                string sIPPage = DownloadUrl(IP_URL);
				string sIP = FindIP(sIPPage);
				if (m_sIP.CompareTo(sIP) != 0)
				{
					m_sIP = sIP;
					SendEmailMessage(m_Options.EMailForIP, "My Current IP", sIPPage);
				}
            }
            catch
            {
            }
        }

		private string FindIP(string sIPPage)
		{
			int idx = sIPPage.IndexOf("name=\"IP\" value=\"");
			if ( idx<0 ) return String.Empty;
			int end = sIPPage.IndexOf("\"/>", idx+1);
			if ( end<0 ) return String.Empty;

			return sIPPage.Substring(idx + 17, end - idx - 17);
		}

        private void SendEmailMessage(string sMailTo, string sSubject, string sBody)
        {
            try
            {
				EventLogWrap.Info("My IP: "+m_sIP);
                
				SmtpClient MySMTPClient = new SmtpClient(GMAIL_HOST, 25);
                MySMTPClient.EnableSsl = true;

                //Enter your gmail username and password:
                //NetworkCredential( string username, string password);
                NetworkCredential MyCredentials = new NetworkCredential(m_Options.EMailFrom, m_Options.EMailFromPwd);
                MySMTPClient.Credentials = MyCredentials;

                //MailMessage( string from, string to, string subject, string body);
                MailMessage MyMessage = new MailMessage(GMAIL_DUMMY, sMailTo, sSubject, sBody);
                MyMessage.IsBodyHtml = true;

                //Note the double back-slashes:
                //MyMessage.Attachments.Add(new Attachment("C:\\TextFile.txt"));

                MySMTPClient.Send(MyMessage);
            }//end try
            catch (Exception err)
            {
				string sErrMsg = "Cannot send mail: " + err.Message;
				EventLogWrap.Error(sErrMsg);
				MessageBox.Show(sErrMsg, "DUMeterMZ", 
					MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end catch
        }

        private static string DownloadUrl(String url)
        {
            try
            {
                WebRequest wrq = WebRequest.Create(url);
                HttpWebResponse hwr = (HttpWebResponse)(wrq.GetResponse());
                using (Stream strm = hwr.GetResponseStream())
                {
                    int count = 0;
                    byte[] b = new byte[4092];
                    StringBuilder sb = new StringBuilder(5000);
                    while ((count = strm.Read(b, 0, b.Length)) > 0)
                    {
                        for (int i = 0; i < count; i++)
                            sb.Append(Convert.ToChar(b[i]));
                    }

                    sb.AppendLine();
                    return sb.ToString();
                }
            }//end try
            catch (Exception err)
            {
                return ERROR_PREFIX + err.Message + "</html>";
            }//end catch
        }
    }//end class SendEmail
}
