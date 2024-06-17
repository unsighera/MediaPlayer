using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
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

        public static List<Song> GetAllSongs()
        {
            return _dataBase.Song.ToList();
        }

        public static List<Playlist> GetAllPlayLists()
        {
            return _dataBase.Playlist.ToList();
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
            SaveChanges();
        }

        public static void UpdatePlayList(Playlist playlist)
        {
            _dataBase.Playlist.AddOrUpdate(playlist);
            SaveChanges();
        }
        public static void RemoveSong(Song song)
        {
            _dataBase.Song.Remove(song);
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
