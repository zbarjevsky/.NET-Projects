using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashCamGPSView.Tools
{
    public class DashCamFileTree
    {
        public List<List<DashCamFileInfo>> fileGroups = new List<List<DashCamFileInfo>>();

        /// <summary>
        /// Sort and group files by date inside specific interval
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="deltaMinutesBetweenGroups"></param>
        public DashCamFileTree(string fileName, double deltaMinutesBetweenGroups = 10.0)
        {
            string dirParent = Path.GetDirectoryName(fileName);
            string [] fileList = Directory.GetFiles(dirParent, "*.MP4");

            Array.Sort(fileList, StringComparer.InvariantCultureIgnoreCase);

            List<DashCamFileInfo> infoList = new List<DashCamFileInfo>();
            foreach (string file in fileList)
            {
                infoList.Add(new DashCamFileInfo(file));
            }

            List<DashCamFileInfo> group = new List<DashCamFileInfo>();
            //group files by date
            //if date difference more than 'groupMinutes' minutes - start new group
            foreach (DashCamFileInfo info in infoList)
            {
                if (group.Count == 0 || (info.FileDate - group.Last().FileDate).TotalMinutes < deltaMinutesBetweenGroups)
                {
                    group.Add(info);
                }
                else
                {
                    fileGroups.Add(group);
                    group = new List<DashCamFileInfo>();
                    group.Add(info);
                }
            }
            if (group.Count > 0)
                fileGroups.Add(group);
        }
    }
}
