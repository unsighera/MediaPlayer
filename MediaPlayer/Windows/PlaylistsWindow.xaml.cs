using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MediaPlayer.DataBase;

namespace MediaPlayer.Windows
{
    /// <summary>
    /// Логика взаимодействия для PlaylistsWindow.xaml
    /// </summary>
    public partial class PlaylistsWindow : Window
    {
        private List<Playlist> _playlists;
        public PlaylistsWindow()
        {
            InitializeComponent();
            _playlists = DataBaseManager.GetAllPlayLists();
            PlaylistsListView.ItemsSource = _playlists;
            PlaylistsListView.Items.Refresh();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var addPlaylistWindow = new AddPlayListWindow();
            addPlaylistWindow.ShowDialog();
            if (DialogResult == true) return;
            PlaylistsListView.ItemsSource = DataBaseManager.GetAllPlayLists();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (PlaylistsListView.SelectedItem is Playlist item)
            {
                DataBaseManager.RemovePlayList(item);
                PlaylistsListView.ItemsSource = DataBaseManager.GetAllPlayLists();
            }
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            if (PlaylistsListView.SelectedItem is Playlist item)
            {
                ((MainWindow)Application.Current.MainWindow).QueueChanged(new Queue<Song>(DataBaseManager.GetPlaylistSongs(item)));
            }
        }


        private void Add_to_Queue_Click(object sender, RoutedEventArgs e)
        {
            if (PlaylistsListView.SelectedItem is Playlist item)
            {
                var q = ((MainWindow)Application.Current.MainWindow).GetQueue();
                foreach (var s in new Queue<Song>(DataBaseManager.GetPlaylistSongs(item)))
                {
                    q.Enqueue(s);
                }
                ((MainWindow)Application.Current.MainWindow).QueueChanged(q);
            }
        }

        private void ShowPlaylist_Click(object sender, RoutedEventArgs e)
        {
            // Обработка события "Показать плейлист"
            if (!(PlaylistsListView.SelectedItem is Playlist playlist)) return;
            var playlistWindow = new PlaylistWindow(playlist);
            playlistWindow.ShowDialog();
        }
    }
}
