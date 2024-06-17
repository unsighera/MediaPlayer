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

namespace MediaPlayer.Windows
{
    /// <summary>
    /// Логика взаимодействия для PlaylistsWindow.xaml
    /// </summary>
    public partial class PlaylistsWindow : Window
    {
        public PlaylistsWindow()
        {
            InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var addPlaylistWindow = new AddPlayListWindow();
            addPlaylistWindow.ShowDialog();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PlaylistsListView_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var listView = sender as ListView;
            var clickedItem = listView.SelectedItem;

            if (clickedItem != null)
            {
                listView.ContextMenu.IsOpen = true;
            }
        }

        private void ShowPlaylist_Click(object sender, RoutedEventArgs e)
        {
            // Обработка события "Показать плейлист"
            var playlist = PlaylistsListView.SelectedItem as Playlist;
            if (playlist != null)
            {
                var playlistWindow = new PlaylistsWindow(playlist);
                playlistWindow.ShowDialog();
            }
        }
    }
}
