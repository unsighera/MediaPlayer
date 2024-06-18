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
    /// Логика взаимодействия для AddPlayListWindow.xaml
    /// </summary>
    public partial class AddPlayListWindow : Window
    {
        public AddPlayListWindow()
        {
            InitializeComponent();
        }

        private void AddPlaylist_Click(object sender, RoutedEventArgs e)
        {
            string playlistName = PlaylistNameTextBox.Text;
            var selectedMusic = MusicSelectionListBox.SelectedItems;

            this.Close();
        }
    }
}
