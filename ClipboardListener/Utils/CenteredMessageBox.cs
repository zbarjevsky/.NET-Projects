using System; 
using System.Windows.Forms; 
using System.Text; 
using System.Drawing; 
using System.Runtime.InteropServices;
using MZ.WPF.MessageBox;

namespace Utils
{
    //Centered MessageBox - relatively to owner window
	public static class CenteredMessageBox
	{
        static CenteredMessageBox()
        {
            PopUp.IconType = PopUp.IconStyle.RegularImages;
        }

        public static void MsgBoxErr(this Form owner, string msg, string title = "Clipboard Manager")
        {
            owner.MessageError(msg, title);
        }//end MsgBoxErr

        public static void MsgBoxErr(string msg, string title = "Clipboard Manager")
        {
            PopUp.Error(msg, title);
        }//end MsgBoxErr

        public static void MsgBoxIfo(this Form owner, string msg, string title = "Clipboard Manager")
        {
            owner.MessageInfo(msg, title);
        }//end MsgBoxIfo

        public static PopUp.PopUpResult MsgBoxQst(this Form owner, string msg, string title = "Clipboard Manager")
        {
            return owner.MessageQuestion(msg, title);
        }//end MsgBoxErr
    }
}

