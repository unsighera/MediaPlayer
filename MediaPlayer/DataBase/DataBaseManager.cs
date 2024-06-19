using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace MediaPlayer.DataBase
{
    public static class DataBaseManager
    {
        private static MediaPlayerEntities _dataBase = new MediaPlayerEntities();

        public static Song GetSong(string FilePath)
        {
            var songs = _dataBase.Song.ToList();
            var song = songs.FirstOrDefault(x => x.FilePath == FilePath);
            if (song != null) return song;
            song = new Song()
                { ID = Guid.NewGuid().ToString(), FilePath = FilePath, FileName = System.IO.Path.GetFileName(FilePath) };
            AddSong(song);
            return song;
        }

        public static void AddSongToPlayList(Playlist playlist, Song song)
        {
            _dataBase.Song_playlist.Add(new Song_playlist() {ID=Guid.NewGuid().ToString(), Playlist = playlist, Song = song});
            SaveChanges();
        }

        public static void AddSongToPlayList(Playlist playlist, List<Song> song)
        {
            foreach (var s in song)
            {
                _dataBase.Song_playlist.Add(new Song_playlist() { ID = Guid.NewGuid().ToString(), Playlist = playlist, Song = s });
            }
            SaveChanges();
        }

        public static void RemoveSongFromPlaylist(Playlist playlist, Song song)
        {
            var lst = _dataBase.Song_playlist.ToList();
            _dataBase.Song_playlist.Remove(lst.First(x => x.Playlist == playlist && x.Song == song));
            SaveChanges();
        }

        public static List<Playlist> GetAllPlayLists()
        {
            return _dataBase.Playlist.ToList();
        }

        public static List<Song> GetPlaylistSongs(Playlist playlist)
        {
            var lst = _dataBase.Song_playlist.ToList();
            var n = lst.Where(x => x.Playlist == playlist);
            return n.Select(ps => ps.Song).ToList();
        }

        public static List<Song> GetAllSongs()
        {
            return _dataBase.Song.ToList();
        }

        public static void AddSong(Song song)
        {
            _dataBase.Song.Add(song);
            SaveChanges();
        }

        public static void AddPlayList(Playlist playlist)
        {
            _dataBase.Playlist.Add(playlist);
            SaveChanges();
        }

        public static void RemovePlayList(Playlist playlist)
        {
            _dataBase.Playlist.Remove(playlist);
            var lst = _dataBase.Song_playlist.ToList();
            while (lst.FirstOrDefault(x => x.Playlist == playlist) != null)
            {
                var p = _dataBase.Song_playlist.FirstOrDefault(x => x.Playlist == playlist);
                lst.Remove(p);
                _dataBase.Song_playlist.Remove(p);
            }
            SaveChanges();
        }

        public static void SaveChanges()
        {
            try
            {
                _dataBase.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();

                foreach (var f in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} fail validation\n", f.Entry.Entity.GetType());
                    foreach (var err in f.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", err.PropertyName, err.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException("Fail: " + sb.ToString(), ex);
            }
        }
    }
}
