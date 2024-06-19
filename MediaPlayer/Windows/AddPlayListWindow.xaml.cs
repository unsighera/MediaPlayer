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
    /// Логика взаимодействия для AddPlayListWindow.xaml
    /// </summary>
    public partial class AddPlayListWindow : Window
    {
        public AddPlayListWindow()
        {
            InitializeComponent();
            MusicSelectionListBox.ItemsSource = DataBaseManager.GetAllSongs();
        }

        private void AddPlaylist_Click(object sender, RoutedEventArgs e)
        {
            var p = new Playlist() { ID = Guid.NewGuid().ToString(), Name = PlaylistNameTextBox.Text };
            DataBaseManager.AddPlayList(p);
            var lst = new List<Song>();
            foreach (Song s in MusicSelectionListBox.SelectedItems)
            {
                lst.Add(s);
            }
            DataBaseManager.AddSongToPlayList(p, lst);
            DialogResult = true;
            this.Close();
        }

        private void PlaylistNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PlaylistNameTextBox.Text = "";
        }
    }
}
