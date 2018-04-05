using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace VCFEdit
{
	public class Contact
	{
		public const string BEGIN_VCARD = "BEGIN:VCARD";
		public const string END_VCARD = "END:VCARD";
		public const string PHOTO = "PHOTO;";

		public string Name = string.Empty;
		public string FullName = string.Empty;
		public List<PhoneNumber> Numbers = new List<PhoneNumber>();
		public List<EMail> Mails = new List<EMail>();
		public List<string> OriginalText = new List<string>();
		public Image Photo;

		public Contact()
		{
			string s =
				"BEGIN:VCARD\n"+
				"VERSION:3.0\n"+
				"N:;;;;\n"+
				"FN:\n"+
				"TEL;TYPE=CELL:\n"+
				"EMAIL;TYPE=HOME:\n"+
				"END:VCARD";

			TextLoad(s);
		}

		public Contact(ref int idx, string [] lines)
		{
			string s = lines[idx];
			OriginalText.Add(s);
			while (s != END_VCARD && idx < lines.Length - 1)
			{
				idx++;
				s = lines[idx];
				OriginalText.Add(s);
			}
			Reload();
		}

		public void Reload()
		{
			for (int i = 0; i < OriginalText.Count; i++ )
			{
				string s = OriginalText[i];

				if (s.StartsWith("N:"))
				{
					Name = s.Substring(2);
				}
				else if (s.StartsWith("FN:"))
				{
					FullName = s.Substring(3);
				}
				else if (s.StartsWith(PHOTO))
				{
					s = s.Substring(s.IndexOf(':')+1);
					for (int j = i + 1; j < OriginalText.Count; j++)
					{
						if (OriginalText[j].StartsWith(END_VCARD) || string.IsNullOrEmpty(OriginalText[j].Trim()))
							break;

						s += OriginalText[j];
					}

					try { Photo = Image.FromStream(new MemoryStream(Convert.FromBase64String(s))); }
					catch (Exception err) { System.Diagnostics.Debug.WriteLine("Error: "+err.Message);}
				}
				else if (s.StartsWith("TEL;"))
				{
					Numbers.Add(new PhoneNumber(s));
				}
				else if (s.StartsWith("EMAIL;TYPE="))
				{
					Mails.Add(new EMail(s));
				}
			}
		}

		public bool Validate(List<string> list)
		{
			if (list.Count < 2)
				return false;
			if (!list[0].StartsWith(BEGIN_VCARD))
				return false;
			if (!list[list.Count - 1].StartsWith(END_VCARD) && !list[list.Count - 2].StartsWith(END_VCARD))
				return false;
			return true;
		}

		public override string ToString()
		{
			if (HasData())
			{

				if (!string.IsNullOrEmpty(FullName))
				{
                    if (Numbers.Count > 0)
                    {
                        return FullName + " (" + Numbers[0].Number + ")";
                    }
					return FullName;
				}
				else if (!string.IsNullOrEmpty(Name))
				{
					return Name;
				}
				else if (Numbers.Count > 0)
				{
					return Numbers[0].Number;
				}
				else if (Mails.Count > 0)
				{
					return Mails[0].Mail;
				}
			}
			return "N/A";
		}

		public override bool Equals(object obj)
		{
			Contact c = obj as Contact;

			//compare original text
			for (int i = 0; i < OriginalText.Count; i++)
			{
				if (OriginalText[i] != c.OriginalText[i])
					return false;
			}
			return true;
		}

		public bool HasData()
		{
			return !string.IsNullOrEmpty(Name) || !string.IsNullOrEmpty(FullName)
				|| Numbers.Count > 0 || Mails.Count > 0;
		}

		public bool TextLoad(string text)
		{
			List<string> list = new List<string>(text.Split('\n'));
			for (int i = 0; i < list.Count; i++)
			{
				list[i] = list[i].Trim();
			}
			for (int i = list.Count - 1; i >= 0; i--)
			{
				if (string.IsNullOrEmpty(list[i].Trim()))
					list.RemoveAt(i);
			}
			if (!Validate(list))
				return false;

			OriginalText = list;
			Reload();
			return true;
		}

		public string TextExport()
		{
			StringBuilder sb = new StringBuilder();
			foreach (string s in OriginalText)
			{
				sb.AppendLine(s);
			}
			return sb.ToString();
		}
	}

	public class PhoneNumber
	{
		public string Type = string.Empty;
		public string Number = string.Empty;

		public PhoneNumber(string line)
		{
			string [] sv = line.Substring("TEL;".Length).Split(':');
			Type = sv[0];
			Number = sv[1];
		}
	}

	public class EMail
	{
		public string Type = string.Empty;
		public string Mail = string.Empty;

		public EMail(string line)
		{
			string[] sv = line.Substring(11).Split(':');
			Type = sv[0];
			Mail = sv[1];
		}
	}
}
