using System;
using System.Collections.Generic;
using System.Windows;

namespace MediaPlayer
{
    public partial class Queue : Window
    {
        public List<string> QueueList { get; set; } = new List<string>();

        public Queue()
        {
            InitializeComponent();
            queueListBox.ItemsSource = QueueList;
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            if (queueListBox.SelectedItem != null)
            {
                string selectedFile = queueListBox.SelectedItem as string;

                ((MainWindow)Application.Current.MainWindow).PlayFile(selectedFile);
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            if (queueListBox.SelectedItem != null)
            {
                string selectedFile = queueListBox.SelectedItem as string;
                QueueList.Remove(selectedFile);
                queueListBox.Items.Refresh();
            }
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (queueListBox.Items.Count > 0)
            {
                int currentIndex = queueListBox.SelectedIndex;
                int nextIndex = (currentIndex + 1) % queueListBox.Items.Count;
                queueListBox.SelectedIndex = nextIndex;
                queueListBox.ScrollIntoView(queueListBox.SelectedItem);
            }
        }
    }
}
