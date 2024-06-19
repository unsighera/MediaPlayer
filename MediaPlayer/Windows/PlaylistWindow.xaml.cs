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
using Microsoft.Win32;

namespace MediaPlayer.Windows
{
    /// <summary>
    /// Логика взаимодействия для PlaylistWindow.xaml
    /// </summary>
    public partial class PlaylistWindow : Window
    {
        private Playlist _playlist;
        public PlaylistWindow(Playlist playlist)
        {
            InitializeComponent();
            _playlist = playlist;
            Title = $"Плейлист <{_playlist.Name}>";
            PlaylistListView.ItemsSource = DataBaseManager.GetPlaylistSongs(playlist);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if(PlaylistListView.SelectedItem is Song song)
                DataBaseManager.RemoveSongFromPlaylist(_playlist, song);
            PlaylistListView.ItemsSource = DataBaseManager.GetPlaylistSongs(_playlist);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Media files (*.mp3)|*.mp3|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() != true) return;
            var fileName = openFileDialog.FileName;
            DataBaseManager.AddSongToPlayList(_playlist, DataBaseManager.GetSong(fileName));
            PlaylistListView.ItemsSource = DataBaseManager.GetPlaylistSongs(_playlist);
        }
    }
}
