using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Xml;
using System.IO;
using ClipboardManager.Zip;

namespace ClipboardManager
{
	public class ClipboardList
	{
		public const string OWNER_TYPE_MISMATCH = "Owner Type Mismatch";
		
		public class ClipboardEntry
		{
			private static RichTextBox m_RtfBox = new RichTextBox();
			public static ImageList m_ImageList = null;

			public class DropEffect
			{
				public static int DROPEFFECT_NONE { get { return 0; } }
				public static int DROPEFFECT_COPY { get { return 1; } }
				public static int DROPEFFECT_MOVE { get { return 2; } }
				public static int DROPEFFECT_LINK { get { return 4; } }
				public static int DROPEFFECT_SCROLL { get { return -2147483648; } }
			}; //end enum DrpEffect

			public ClipboardEntry(Image ico)
			{
				_icoAppFrom = (Image)ico.Clone();

				IDataObject objData = Clipboard.GetDataObject();
				string[] svFormats = objData.GetFormats(true);

				_desc = "[Clipboard data is not RTF or ASCII Text]";
				if ( Clipboard.ContainsText(TextDataFormat.Rtf) )
				{
					_dataType = DataFormats.Rtf;
					_data = Clipboard.GetText(TextDataFormat.Rtf);
					_icoItemType = 2;

					m_RtfBox.Rtf = (string)_data;
					_desc = m_RtfBox.Text;
					if (_desc.Trim().Length == 0)
						_desc = "--Rtf Binary--";

					//if there is 'international' string that cannot be translated
					if ( _desc.IndexOf("???") >= 0 && Clipboard.ContainsText(TextDataFormat.UnicodeText) )
						UnicodeTextInit();
				}//end if
				else if ( Clipboard.ContainsText(TextDataFormat.UnicodeText) )
				{
					UnicodeTextInit();
				}//end else if
				else if ( Clipboard.ContainsText(TextDataFormat.Text) )
				{
					_dataType = DataFormats.Text;
					_data = Clipboard.GetText(TextDataFormat.Text);
					_icoItemType = 0;

					_desc = (string)_data;
				}//end else if
				else if ( Clipboard.ContainsImage() )
				{
					_dataType = DataFormats.Bitmap;
					_data = Clipboard.GetImage();
					_icoItemType = 3;

					Image bmp = (Image)_data;
					_desc = "Bitmap: " + bmp.Width + "x" + bmp.Height;
				}//end else if
				else if ( Clipboard.ContainsFileDropList() )
				{
					int flag = 0;
					MemoryStream streamType = (MemoryStream)Clipboard.GetData("Preferred DropEffect");
					if ( streamType != null )
						flag = streamType.ReadByte();

					bool move = (flag & DropEffect.DROPEFFECT_MOVE) > 0 ;
					bool copy = (flag & DropEffect.DROPEFFECT_COPY) > 0 ;

					_dataType = DataFormats.FileDrop;
					_data = Clipboard.GetData(DataFormats.FileDrop);
					
					string[] svFiles = (string[])_data;
					_icoItemType = 4;
					if ( copy )
						_icoItemType = svFiles.Length == 1 ? 6 : 8;
					else if ( move )
						_icoItemType = svFiles.Length == 1 ? 7 : 9;

					_desc = FileDropListDesc(svFiles);
				}//end else if
				else //Unsupported format == empty
				{
					_data			= null;
					_icoItemType	= 5;

					if ( svFormats.Length > 0 )
						_desc = "--Unsupported format: "+svFormats[0]+"--";
					else
						_desc = "--Empty--";
					
					System.Diagnostics.Trace.WriteLine(
						"ClipboardEntry::constructor - Unsupported clipboard format",
						"ClipboardEntry");
				}//end else
			}//end constructor

			//for load from Xml
			public ClipboardEntry(XmlNode nd, string sOwnerType, Image icoDefault)
			{
				_dataType = XmlUtil.GetStrAtt(nd, "Type", DataFormats.Text);
				_ownerType = XmlUtil.GetStrAtt(nd, "OwnerType", "");
				if (_ownerType != sOwnerType)
					throw new Exception(OWNER_TYPE_MISMATCH);

				string Value = nd.InnerText;
				string sImageFileName = XmlUtil.GetStrAtt(nd, "Ico", "Image001");
				Image ico = LoadImage(sImageFileName, icoDefault);

				_icoAppFrom = ico;
				if ( ico == null )
					_icoAppFrom = new Bitmap(16, 16);

				if (_dataType == DataFormats.Rtf)
				{
					_icoItemType = 2;
					_data = Value;
					m_RtfBox.Rtf = Value;
					_desc = m_RtfBox.Text;
					if (_desc.Trim().Length == 0)
						_desc = "--Rtf Binary--";
				}//end if
				else if (_dataType == DataFormats.UnicodeText)
				{
					_icoItemType = 1;
					_data = Value;
					_desc = Value;
				}//end else if
				else if (_dataType == DataFormats.Text)
				{
					_icoItemType = 0;
					_data = Value;
					_desc = Value;
				}//end else if
				else if (_dataType == DataFormats.FileDrop)
				{
					_icoItemType = 4;
					String [] sv = Value.Split('|');
					_data = sv;
					_desc = FileDropListDesc(sv);
				}//end else if
				else
				{
					throw new Exception("Unknown clipboard format");
				}//end else

				System.Diagnostics.Trace.WriteLine(
					"File: " + Path.GetFileName(sImageFileName) +
					" Entry: " + ShortDesc());
			}//end constructor

			private ClipboardEntry(string sOwnerType)
			{
				_ownerType = sOwnerType;
				_icoAppFrom = new Bitmap(16, 16);
			}//end constructor

            internal void Clear()
            {
                _icoAppFrom.Dispose();
                _icoAppFrom = null;
            }

            //constructor for unicode text
            private void UnicodeTextInit()
			{
				if ( !Clipboard.ContainsText(TextDataFormat.UnicodeText) )
					return;

				_dataType = DataFormats.UnicodeText;
				_data = Clipboard.GetText(TextDataFormat.UnicodeText);
				_icoItemType = 1;
				
				_desc = (string)_data;
			}//end UnicodeTextInit

			public bool IsEmpty { get { return _data == null; } }

			public static ClipboardEntry CreateEmpty(string sOwnerType)
			{
				return new ClipboardEntry(sOwnerType);
			}//end CreateEmpty

			public void Save(XmlNode ndParent, IZip zip, string sImageFileName)
			{
				if (_data == null)
					return;

				string Value = "";
				string Type = "";
				if (_dataType == DataFormats.Text ||
					_dataType == DataFormats.UnicodeText ||
					_dataType == DataFormats.Rtf)
				{
					Type = _dataType;
					Value = (string)_data;
				}//end if
				else if (_dataType == DataFormats.FileDrop)
				{
					Type = _dataType;
					String [] sv = (String[])_data;
					StringBuilder sb = new StringBuilder(12);
					for (int i = 0; i < sv.Length; i++)
					{
						sb.Append(sv[i]);
						if ( i != sv.Length-1 )
							sb.Append('|');
					}//end for
					Value = sb.ToString();
				}//end if
				else //do not know how to save
					return; 

				try
				{
					File.Delete(sImageFileName);
					_icoAppFrom.Save(sImageFileName);
					if ( zip != null )
					{
						zip.Add(sImageFileName);
						File.Delete(sImageFileName);
					}//end if

					System.Diagnostics.Trace.WriteLine(
						"File: " + Path.GetFileName(sImageFileName) +
						" Entry: " + ShortDesc());
				}//end try
				catch (Exception err) 
				{ 
					System.Diagnostics.Trace.WriteLine("Archiving Error: "+err.ToString());
					FormClipboard.TraceLn(true, "ClipboardEntry", "Save",
						"{0} Error: {1}", sImageFileName, err.Message);
				}//end catch

				XmlNode ndEntry = XmlUtil.AddNewNode(ndParent, "ClipboardEntry", Value);
				XmlUtil.UpdStrAtt(ndEntry, "OwnerType", _ownerType);
				XmlUtil.UpdStrAtt(ndEntry, "Type", Type);
				XmlUtil.UpdStrAtt(ndEntry, "Ico", sImageFileName);
			}//end Save

			public void Put()
			{
				if (_data == null)
					return;

				try
				{
					if ( _dataType == DataFormats.Rtf )
					{
						m_RtfBox.Rtf = (string)_data;
						m_RtfBox.SelectAll();
						m_RtfBox.Copy();
						//Clipboard.SetText(m_RtfBox.Rtf, TextDataFormat.Rtf);
					}//end if
					else
						Clipboard.SetData(_dataType, _data);
				}//end try
				catch ( Exception err)
				{
					throw new Exception("ClipboardList("+_ownerType+")::Put::Error: "+err.Message, err);
				}//end catch
			}//end Put

			public void SetRichText(RichTextBox box, PictureBox lblApp, Label lblType)
			{
				box.SuspendLayout();
				bool bReadOnly = box.ReadOnly;
				box.ReadOnly = false;

				lblApp.Image = _icoAppFrom;
				lblType.ImageIndex = _icoItemType;

				box.Clear();
				if (_dataType == DataFormats.Rtf)
					box.Rtf = (string)_data;
				else if (_dataType == DataFormats.Bitmap || _dataType == DataFormats.Dib)
					box.Paste();
				else //text, picture, file etc.
					box.Text = _desc;
				//box.SelectAll();

				box.ReadOnly = bReadOnly;
				box.ResumeLayout();
			}//end SetRichText

			private string FileDropListDesc(String[] sv)
			{
				if (sv.Length == 1)
					return sv[0];

				StringBuilder desc = new StringBuilder(200);
				desc.Append(sv.Length + " files: ");
				for (int i = 0; i < sv.Length; i++)
				{
					desc.Append("\n\t");
					desc.Append(sv[i]);
				}//end for
				return desc.ToString();
			}//end FileDropListDesc

            public string ShortDesc() { return ShortDesc(60, true); }
            public string ShortDesc(int iMaxChars, bool bSingleLine)
            {
                string s = bSingleLine ? _desc.Trim(' ', '\n', '\r', '\t') : _desc;
				//s = s.Replace("---------------------------", "--"); //for message boxes copy
                if ( bSingleLine ) s = s.Replace("\n", " ");
                if ( bSingleLine ) s = s.Replace("\r", "");
				s = s.Replace("&", "&&");
                if (s.Length > iMaxChars)
				{
                    if (bSingleLine) //insert elipsis in the middle
                        s = s.Substring(0, iMaxChars / 2) + " ... " + s.Substring(s.Length - (iMaxChars / 2));
                    else //cat string at the end
                        s = s.Substring(0, iMaxChars) + " ... ";
				}//end if
				return s;
			}//end ShortDesc

			public string FileFilter()
			{
				switch ( _icoItemType )
				{
					case 0: //plain text
					case 1: //unicode text
					case 2: //rtf
					case 4: //file drop list
						return "Text File(*.txt)|*.txt|Rtf File(*.rtf)|*.rtf||";
					case 3: //bitmap
						return "Picture File(*.png)|*.png||";
					default:
						return "Unknown(Binary) File|*.bin||";
				}//end switch
			}//end FileFilter

			public bool SavePicture(string sFileName)
			{
				if ( _icoItemType != 3 )
					return false;
				
				Image bmp = (Image)_data;
				bmp.Save(sFileName);
				
				return true;
			}//end SavePicture

			public override string ToString()
			{
				return _desc;
			}//end ToString

			public Image GetCombinedIcon(bool bTwinImage)
			{
				if (bTwinImage)
					return ImagesUtil.Combine(_icoAppFrom, m_ImageList.Images[_icoItemType]);
				else
					return m_ImageList.Images[_icoItemType];
			}//end GetCombinedIcon

			public override bool Equals(object obj)
			{
				ClipboardEntry clp = (ClipboardEntry)obj;
				if (clp == null)
					return false;
				return clp._desc.Equals(_desc) && clp._dataType.Equals(_dataType);
			}//end Equals

			public override int GetHashCode()
			{
				return _desc.GetHashCode() + _dataType.GetHashCode();
			}//end GetHashCode

			internal ClipboardEntry Clone()
			{
				ClipboardEntry clp = new ClipboardEntry(_ownerType);

				clp._icoItemType	= _icoItemType;
				clp._icoAppFrom		= (Image)_icoAppFrom.Clone();
				clp._dataType		= _dataType;
				clp._data			= _data;
				clp._desc			= _desc;

				return clp;
			}//end Clone

			public int _icoItemType		= 5;
			public Image _icoAppFrom { get; private set; } = null;
			public string _dataType		= DataFormats.Text;
			public object _data			= null;
			public string _ownerType	= "";
			private string _desc		= "--empty--";

            internal void SetAppIcon(Image icoAppFrom)
            {
                if (_icoAppFrom != null)
                    _icoAppFrom.Dispose();

                _icoAppFrom = (Image)icoAppFrom.Clone();
            }
        }//end class ClipboardEntry

		public bool m_bClipboardIsEmpty = true;
		private List<ClipboardEntry> m_vData = new List<ClipboardEntry>();
		public string m_sListType		= "";
		private ClipboardEntry m_EmptyEntry = null;

		public ClipboardList(string sListType, ImageList list)
		{
			m_sListType = sListType;
			ClipboardEntry.m_ImageList = list;
			m_EmptyEntry = ClipboardEntry.CreateEmpty(sListType);
		}//end constructor

		internal void Clear()
		{
            while (m_vData.Count > 1)
            {
                m_vData[1].Clear();
                m_vData.RemoveAt(1);
            }
		}//end Clear

		public int Count
		{
			get { return m_vData.Count; }
		}//end count

		public int Load(XmlNode nd, Image icoDefault)
		{
			m_vData = new List<ClipboardEntry>();
			try
			{
				XmlNodeList list = nd.SelectNodes("Settings/ClipBoardList/ClipboardEntry");
				for (int i = 0; i < list.Count; i++)
				{
					try 
					{
						m_vData.Add(new ClipboardEntry(list[i], m_sListType, icoDefault)); 
					}//end try
					catch (Exception err) 
					{
						if ( err.Message != OWNER_TYPE_MISMATCH )
						{
							System.Diagnostics.Trace.WriteLine("Clipboard entry ignored: "+err.Message);
							FormClipboard.TraceLn(false, "ClipboardList", "Load",
								"{0} Error: {1}", list[i].InnerXml, err.Message);
						}//end if
					}//end catch
				}//end for
			}//end try
			catch ( Exception err ) 
			{
				System.Diagnostics.Trace.WriteLine("ClipboardList: " + err.Message);
				FormClipboard.TraceLn(true, "ClipboardList", "Load",
					"{0} Error: {1}", m_sListType, err.Message);
			}//end catch
			return Count;
		}//end Load

		public void Save(XmlNode nd, IZip zip, string sFolderName)
		{
			nd = XmlUtil.UpdSubNode(nd, "ClipBoardList", "");
			for (int i = 0; i < Count; i++)
			{
				string sImageFileName = GetImageFileName(sFolderName, i);
				GetEntry(i).Save(nd, zip, sImageFileName);
			}//end for
		}//end Save

		private string GetImageFileName(string sFolderName, int idx)
		{
			return sFolderName + "\\" + string.Format("{0}_Image{1:D3}.png", m_sListType, idx);
		}//end GetImageFileName

		protected static Image LoadImage(string sImageFileName, Image icoDefault)
		{
			try { 
				Bitmap bmp = new Bitmap(sImageFileName);
				Image ico =  new Bitmap(bmp);
				bmp.Dispose(); //to release the file
				File.Delete(sImageFileName);
				return ico; 
			}//end try
			catch { return (Image)icoDefault.Clone(); }
		}//end LoadImage

		public bool AddEntry(Image ico)
		{
			m_bClipboardIsEmpty = true;
			return AddEntry(new ClipboardEntry(ico));
		}//end AddEntry

		public bool AddEntry(ClipboardEntry clp)
		{
			if ( clp.IsEmpty )
			{
				m_EmptyEntry = clp; 
				m_EmptyEntry._ownerType = m_sListType;

                return false; //not added
			}//end if

            m_bClipboardIsEmpty = false;
            
            int idx = FindEntry(clp);
            if ( idx == 0 ) //latest entry - already in
                return false; //not added

			if ( idx > 0 ) //if exist - remove earlier
			{
				//restore first app ico - problems with first loading when clipboard not empty
				clp.SetAppIcon(GetEntry(idx)._icoAppFrom);
                m_vData[idx].Clear();
                m_vData.RemoveAt(idx);
			}//end if

			ClipboardEntry c = clp.Clone();
			c._ownerType = this.m_sListType;
			m_vData.Insert(0, c);

			int max = FormClipboard.m_Settings.m_iHistoryLen;
            while (m_vData.Count > max) //if too big - remove from the end
            {
                m_vData[max].Clear();
                m_vData.RemoveAt(max);
            }
			m_vData.TrimExcess();

            return true;
		}//end AddEntry

		public ClipboardEntry GetEntry(int idx)
		{
			return m_vData[idx];
		}//end GetEntry

		public ClipboardEntry GetCurrentEntry()
		{
		    if (m_bClipboardIsEmpty || m_vData == null || m_vData.Count == 0)
		        return m_EmptyEntry;

			return m_vData[0];
		}//end GetCurrentEntry

		public int FindEntry(ClipboardEntry clp)
		{
			for (int i = 0; i < m_vData.Count; i++)
			{
				if (GetEntry(i).Equals(clp))
					return i;
			}//end for
			return -1;
		}//end FindEntry

		internal void RemoveAt(int idx)
		{
			m_vData.RemoveAt(idx);
		}//end RemoveAt

		internal void Insert(int idx, ClipboardEntry entry)
		{
			m_vData.Insert(idx, entry);
		}//end Insert
	}//end class ClipboardList
}//end namespace ClipboardListener
