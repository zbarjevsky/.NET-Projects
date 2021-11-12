using DashCamGPSView.Properties;
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
        public DashCamFileTree(string selectFile, string speedUnits, double deltaMinutesBetweenGroups = 10.0)
        {
            string dirParent = Path.GetDirectoryName(selectFile);
            if(dirParent.EndsWith(@"\RO") || dirParent.EndsWith(@"Parking"))
                dirParent = Path.GetDirectoryName(dirParent);

            string dirParking = Path.Combine(dirParent, "Parking");
            string dirReadOnly = Path.Combine(dirParent, "RO");

            string ext = Path.GetExtension(selectFile);

            List<string> fileList = Directory.GetFiles(dirParent, "*"+ext).ToList();
            if (Directory.Exists(dirParking))
            {
                string[] parkingFiles = Directory.GetFiles(dirParking, "*" + ext);
                fileList.AddRange(parkingFiles);
            }

            if (Directory.Exists(dirReadOnly))
            {
                string[] readOnlyFiles = Directory.GetFiles(dirReadOnly, "*" + ext);
                fileList.AddRange(readOnlyFiles);
            }

            List<FileInfoWithDateFromFileName> allInfos = fileList.Select(f => new FileInfoWithDateFromFileName(f)).ToList();

            //sorting by LastWriteTime - seems to be more accurate
            allInfos.Sort((f1, f2) => f1.Info.LastWriteTime.CompareTo(f2.Info.LastWriteTime));

            List<DashCamFileInfo> infoList = new List<DashCamFileInfo>();
            for (int idx = 0; idx < allInfos.Count; idx++)
            {
                FileInfoWithDateFromFileName currentInfo = allInfos[idx];
                DashCamFileInfo info = new DashCamFileInfo(allInfos, currentInfo, speedUnits);
                DashCamFileInfo existing = (infoList.FirstOrDefault(i => i.FileNameFront == info.FileNameFront));
                if(existing == null)
                    infoList.Add(info);
            }

            List<DashCamFileInfo> group = new List<DashCamFileInfo>();
            //group files by date
            //if date difference more than 'groupMinutes' minutes - start new group
            foreach (DashCamFileInfo info in infoList)
            {
                if (group.Count == 0 || (info.FileDateStart - group.Last().FileDateEnd).TotalMinutes < deltaMinutesBetweenGroups)
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
