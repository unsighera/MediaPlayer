using System;
using System.Collections.Generic;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Threading;
using MediaPlayer.DataBase;
using MediaPlayer.Windows;

namespace MediaPlayer
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        private Queue queueWindow;
        private Queue<Song> SongQueue;
        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            SongQueue = new Queue<Song>();
        }

        private void AddFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Media files (*.mp3)|*.mp3|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() != true) return;
            var fileName = openFileDialog.FileName;
            mediaElement.Source = new Uri(fileName);
            currentFileLabel.Content = System.IO.Path.GetFileName(fileName);
            SongQueue = new Queue<Song>();
        }

        private void OpenQueue_Click(object sender, RoutedEventArgs e)
        {
            queueWindow = new Queue(SongQueue);
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
            if (SongQueue.Count == 0) return;
            PlayFile(SongQueue.Dequeue().FilePath);
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

        private void Playlist_Click(object sender, RoutedEventArgs e)
        {
            PlaylistsWindow playlistsWindow = new PlaylistsWindow();
            playlistsWindow.Show();
        }

        public void QueueChanged()
        {
            if (queueWindow._queue.Count == 0)
            {
                SongQueue = new Queue<Song>();
                mediaElement.Source = null;
                currentFileLabel.Content = "";
                return;
            }
            SongQueue = queueWindow._queue;
            Song first = SongQueue.Peek();
            mediaElement.Source = new Uri(first.FilePath);
            currentFileLabel.Content = first.FileName;
        }
        public void QueueChanged(Queue<Song> newQueue)
        {
            if (newQueue.Count == 0)
            {
                SongQueue = new Queue<Song>();
                mediaElement.Source = null;
                currentFileLabel.Content = "";
                return;
            }
            SongQueue = newQueue;
            Song first = SongQueue.Peek();
            mediaElement.Source = new Uri(first.FilePath);
            currentFileLabel.Content = first.FileName;
        }

        public Queue<Song> GetQueue() => SongQueue;
    }
}
