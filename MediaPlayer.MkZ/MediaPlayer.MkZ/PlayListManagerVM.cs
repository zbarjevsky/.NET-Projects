using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using MkZ.MediaPlayer.Utils;
using MkZ.Windows;

namespace MkZ.MediaPlayer
{
    public class PlayListManagerVM : NotifyPropertyChangedImpl
    {
        public MediaDatabaseInfo DB => MediaPlayerContext.Instance.AppConfig.MediaDatabaseInfo;

        public ObservableCollection<PlayList> PlayListRoot => DB.RootList.PlayLists;

        public bool IsPlayingSelectedFile { get { return GetSelectedFile()?.MediaState == System.Windows.Controls.MediaState.Play; } }

        public PlayListManagerVM()
        {
        }

        public MediaFileInfo GetSelectedFile()
        {
            return DB.SelectedPlayList.GetSelectedInfo();
        }

        public void RemovePlayList(PlayList list)
        {
            DB.RemovePlayList(list);
        }
    }
}
