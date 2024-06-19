using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Markup;
using MediaPlayer.DataBase;
using Microsoft.Win32;

namespace MediaPlayer
{
    public partial class Queue : Window
    {
        public Queue<Song> _queue { get; set; }
        public Queue(Queue<Song> queue)
        {
            InitializeComponent();
            _queue = queue;
            queueListBox.ItemsSource = _queue;
        }
       
        private void Done_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).QueueChanged();
            Close();
        }
        private void Shuffle_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            var data = _queue.ToArray();
            for (int i = data.Length - 1; i >= 1; i--)
            {
                int j = random.Next(i + 1);
                (data[j], data[i]) = (data[i], data[j]);
            }

            _queue = new Queue<Song>(data);
            queueListBox.ItemsSource = _queue;
            queueListBox.Items.Refresh();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Media files (*.mp3)|*.mp3|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() != true) return;
            var fileName = openFileDialog.FileName;
            _queue.Enqueue(DataBaseManager.GetSong(fileName));
            queueListBox.Items.Refresh();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (queueListBox.Items.Count <= 0) return;
            _queue.Dequeue();
            queueListBox.Items.Refresh();

        }
    }
}
