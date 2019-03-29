using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace RadexOneDemo
{
    public partial class RadiationConverterControl : UserControl
    {
        public enum RadiationUnits
        {
            [Description("Sievert (Sv/h)")]
            Sv = 1000000,     
            [Description("milliSievert (mSv/h)")]
            mvSv = 1000,   
            [Description("microSievert (µSv/h)")]
            µSv = 1,     
            [Description("Röntgen (Rem/h)")]
            Rem = 10000,  
            [Description("milliRöntgen (mRem/h)")]
            mRem = 10 
        }

        public double ValueFrom
        {
            set { m_numFrom.Value = (decimal)value; m_cmbFrom.SelectedIndex = 2; } //µSv 
            get { return (double)m_numFrom.Value; }
        }

        public RadiationConverterControl()
        {
            InitializeComponent();
        }

        private void RadiationConverterControl_Load(object sender, EventArgs e)
        {
            BindToEnumDescription(m_cmbFrom, 2);
            BindToEnumDescription(m_cmbTo, 4);
        }

        private static void BindToEnumDescription(ComboBox cmb, int selectedIndex)
        {
            List<RadiationUnits> list = GetEnumValuesInDeclarationOrder<RadiationUnits>();

            cmb.Items.Clear();
            cmb.DisplayMember = "Description";
            cmb.ValueMember = "Value";
            cmb.DataSource = list.Cast<Enum>()
                .Select(value => new
                {
                    (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), 
                    typeof(DescriptionAttribute)) as DescriptionAttribute).Description,
                    value
                })
                //.OrderBy(item => item.value)
                .ToList();
            cmb.SelectedIndex = selectedIndex;
        }

        private static List<T> GetEnumValuesInDeclarationOrder<T>()
        {
            var t = typeof(T);
            // first type in this array is the data-type of the enum, int32 if not defined
            var members = t.GetFields();

            var result = new List<T>(members.Length - 1);
            foreach (FieldInfo mem in members.Skip(1))
                result.Add((T)mem.GetValue(null));

            return result;
        }

        private void m_numFrom_KeyUp(object sender, KeyEventArgs e)
        {
            OnChange(sender, e);
        }

        private bool _inOnChange = false;
        private void OnChange(object sender, EventArgs e)
        {
            if (m_cmbFrom.Items.Count < 5 || m_cmbTo.Items.Count < 5)
                return;
            if (m_cmbFrom.SelectedIndex < 0 || m_cmbTo.SelectedIndex < 0)
                return;

            if (_inOnChange)
                return;
            _inOnChange = true;

            List<RadiationUnits> list = GetEnumValuesInDeclarationOrder<RadiationUnits>();
            RadiationUnits valFrom = list[m_cmbFrom.SelectedIndex];
            RadiationUnits valTo = list[m_cmbTo.SelectedIndex];
            double ratio = (double)valFrom / (double)valTo;

            if (sender.Equals(m_numFrom) || sender.Equals(m_cmbFrom) || sender.Equals(m_cmbTo))
            {
                m_numTo.Value = (decimal)((double)m_numFrom.Value * ratio);
            }
            else if(sender.Equals(m_numTo))
            {
                m_numFrom.Value = (decimal)((double)m_numTo.Value * ratio);
            }

            _inOnChange = false;
        }
    }

    public partial class SpecialNumericUpDown : NumericUpDown
    {
        private const int _decimalPlaces = 6;

        protected override void UpdateEditText()
        {
            UpdateDecimalPlaces();
            base.UpdateEditText();
        }

        private void UpdateDecimalPlaces()
        {
            int decimalPlaces = GetDecimalPlaces(this.Value);
            if (decimalPlaces > 6)
                decimalPlaces = 6;

            if (decimalPlaces < 1) //integer value
            {
                if (DecimalPlaces != 1)
                    DecimalPlaces = 1;
            }
            else if (DecimalPlaces != decimalPlaces)
            {
                DecimalPlaces = decimalPlaces;
            }
        }

        private static int GetDecimalPlaces(decimal d)
        {
            int count = BitConverter.GetBytes(decimal.GetBits(d)[3])[2];
            return count;
        }
    }
}
