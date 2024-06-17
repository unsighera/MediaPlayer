using System;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Threading;
using MediaPlayer.Windows;

namespace MediaPlayer
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        private Queue queueWindow;

        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;

            queueWindow = new Queue();
        }

        private void AddFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Media files (*.mp4;*.mp3)|*.mp4;*.mp3|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string fileName = openFileDialog.FileName;
                mediaElement.Source = new Uri(fileName);
                currentFileLabel.Content = System.IO.Path.GetFileName(fileName);
                queueWindow.QueueList.Add(fileName);
                queueWindow.queueListBox.Items.Refresh();
            }
        }

        private void OpenPlaylits_Click(object sender, RoutedEventArgs e)
        {
            var playlists = new PlaylistsWindow();
            playlists.ShowDialog();
        }

        private void OpenQueue_Click(object sender, RoutedEventArgs e)
        {
            queueWindow.Show();
        }

        public void PlayFile(string fileName)
        {
            mediaElement.Source = new Uri(fileName);
            mediaElement.Play();
            currentFileLabel.Content = System.IO.Path.GetFileName(fileName);
            timer.Start();
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Play();
            timer.Start();
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Pause();
            timer.Stop();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Stop();
            timer.Stop();
            timelineSlider.Value = 0;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            timelineSlider.Maximum = mediaElement.NaturalDuration.TimeSpan.TotalSeconds;
        }

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            timelineSlider.Value = 0;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (mediaElement.NaturalDuration.HasTimeSpan)
            {
                timelineSlider.Value = mediaElement.Position.TotalSeconds;
            }
        }

        private void TimelineSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Math.Abs(mediaElement.Position.TotalSeconds - timelineSlider.Value) > 1)
            {
                mediaElement.Position = TimeSpan.FromSeconds(timelineSlider.Value);
            }
        }
    }
}
