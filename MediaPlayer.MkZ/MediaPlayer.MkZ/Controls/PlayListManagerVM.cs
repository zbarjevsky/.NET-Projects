using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using MkZ.MediaPlayer.Utils;
using MkZ.Windows;

namespace MkZ.MediaPlayer.Controls
{
    public class PlayListManagerVM : NotifyPropertyChangedImpl
    {
        public MediaDatabaseInfo DB => VideoPlayerContext.Instance.Config.MediaDatabaseInfo;

        public ObservableCollection<PlayList> PlayListRoot => DB.RootList.PlayLists;

        public PlayListManagerVM()
        {
        }

        public void RemovePlayList(PlayList list)
        {
            DB.RemovePlayList(list);
        }
    }
}
