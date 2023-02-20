using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


using MkZ.Physics;

namespace MkZ.RadexOneLib
{
    public class RadiationDataPoint : MkZ.Physics.IDataPoint
    {
        [XmlIgnore]
        public override bool IsValid => RATE != 0.0;

        public override DateTime Date { get; set; } = DateTime.Now;

        [XmlIgnore]
        public override double[] Values { get; protected set; } = new double[4] { 0, 0, 0, 80 };

        public double CPM { get { return Values[0]; } set { Values[0] = value; } }
        public double RATE { get { return Values[1]; } set { Values[1] = value; } }
        public double DOSE { get { return Values[2]; } set { Values[2] = value; } }
        public double Threshold { get { return Values[3]; } set { Values[3] = value; } }

        public override double GetValue<T>(IUnitBase<T> measurementType) //where T : struct, IConvertible
        {
            return measurementType.Convert(RATE);
        }

        public override string ToString()
        {
            return string.Format("{0} - Rate: {1:0.00} µSv/h, CPM: {2}, Dose: {3:##0.00} µSv, Threshold: {4} CPM",
                Date.ToString("s"), RATE, CPM, DOSE, Threshold);
        }
    }
}
