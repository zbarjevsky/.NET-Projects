using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MkZ.Weather.Utils
{
    public class ListUtils<T> where T : class
    {
        public static List<T> Merge(List<T> l1, List<T> l2, Func<T,T,int> compare)
        {
            List<T> newRadiationDataPoints = new List<T>();

            int i = 0, j = 0;
            while (i < l1.Count && j < l2.Count)
            {
                T pt1 = l1[i];
                T pt2 = l2[j];

                int res = compare(pt1, pt2);
                if (res == 0)
                {
                    newRadiationDataPoints.Add(pt1);
                    i++;
                    j++;
                }
                else if (res < 0)
                {
                    newRadiationDataPoints.Add(pt1);
                    i++;
                }
                else
                {
                    newRadiationDataPoints.Add(pt2);
                    j++;
                }
            }

            //only one of these will work
            while (i < l1.Count)
            {
                newRadiationDataPoints.Add(l1[i]);
                i++;
            }

            while (j < l2.Count)
            {
                newRadiationDataPoints.Add(l2[j]);
                j++;
            }

            return newRadiationDataPoints;
        }
    }
}
