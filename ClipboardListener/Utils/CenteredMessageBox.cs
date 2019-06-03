using System; 
using System.Windows.Forms; 
using System.Text; 
using System.Drawing; 
using System.Runtime.InteropServices;


namespace Utils
{
    //Centered MessageBox - relatively to owner window
	public class CenteredMessageBox
	{
        static CenteredMessageBox()
        {
            MZ.WPF.MessageBox.PopUp.IconType = MZ.WPF.MessageBox.PopUp.IconStyle.RegularImages;
        }

        public static void MsgBoxErr(string msg, string title= "Clipboard Manager")
        {
            MZ.WPF.MessageBox.PopUp.Error(msg, title);
        }//end MsgBoxErr

        public static void MsgBoxIfo(string msg, string title = "Clipboard Manager")
        {
            MZ.WPF.MessageBox.PopUp.Information(msg, title);
        }//end MsgBoxIfo

        public static MZ.WPF.MessageBox.PopUp.PopUpResult MsgBoxQst(string msg, string title = "Clipboard Manager")
        {
            return MZ.WPF.MessageBox.PopUp.Question(msg, title);
        }//end MsgBoxErr
    }
}

