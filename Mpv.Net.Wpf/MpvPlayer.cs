﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Mpv.Net.Wpf
{
    /// <summary>
    /// Mpv Player controller. Main Component
    /// </summary>
    public class EventBoolArgs : System.EventArgs
    {
        public bool ebool;
        public EventBoolArgs(bool ebool)
        {
            this.ebool = ebool;
        }
    }
    public sealed class MpvPlayer : Control, IDisposable
    {
        private Mpv.NET.Player.MpvPlayer _player;
        private readonly Locker _locker;

        private EventBoolArgs cont = new EventBoolArgs(true);
        public MpvDisplay Display
        {
            get { return (MpvDisplay)GetValue(DisplayProperty); }
            set { SetValue(DisplayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Display.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplayProperty =
            DependencyProperty.Register("Display", typeof(MpvDisplay), typeof(MpvPlayer), new PropertyMetadata(null));

        static MpvPlayer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MpvPlayer), new FrameworkPropertyMetadata(typeof(MpvPlayer)));
        }

        public MpvPlayer()
        {
            _locker = new Locker();
        }

        public void Dispose()
        {
            if (_player != null)
            {
                _player.PositionChanged -= _player_PositionChanged;
                cont.ebool = false;
                _player.Stop();
                _player.Dispose();
                _player = null;
            }
        }

        public override void OnApplyTemplate()
        {
            var playpause = GetTemplateChild("PART_PlayPause") as Button;
            playpause.Click += Playpause_Click;

            var stop = GetTemplateChild("PART_Stop") as Button;
            stop.Click += Stop_Click;

            var volume = GetTemplateChild("PART_Volume") as Slider;
            volume.ValueChanged += Volume_ValueChanged;

            var seek = GetTemplateChild("PART_Seek") as Slider;
            seek.ValueChanged += Seek_ValueChanged;

            Loaded += MpvPlayer_Loaded;
        }

        private void MpvPlayer_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _player = new NET.Player.MpvPlayer(Display.DisplayHandle);
                _player.AutoPlay = true;
                _player.PositionChanged += _player_PositionChanged;
                _player.MediaUnloaded += WhenUnloaded;
                cont.ebool = true;
            }
            catch (NET.Player.MpvPlayerException)
            {
                _player = null;
            }
        }

        public void LoadFile(string fileName)
        {
            if (_player != null)
            {
                cont.ebool = true;
                _player?.Stop();
                _locker.PerformLockAction(() =>
                {
                    if (GetTemplateChild("PART_Seek") is Slider seek)
                    {
                        seek.Value = 0;
                    }

                    if (GetTemplateChild("PART_Volume") is Slider volume)
                        _player.Volume = Convert.ToInt32(volume.Value);
                });
                System.Threading.Thread.Sleep(100);
                _player.Load(fileName);
                System.Threading.Thread.Sleep(100);
                //GetChapters();
                GetAudioAndSubtitles();
            }
        }

        public double GetTotalTime()
        {
            double ret = 0;
            _locker.PerformLockAction(() =>
            {
                if (GetTemplateChild("PART_Seek") is Slider seek)
                {
                    seek.Maximum = _player.Duration.TotalSeconds;
                    ret = seek.Maximum;
                }
            });
            return ret;
        }

        private void GetAudioAndSubtitles()
        {
            List<long> aid = new List<long>();
            List<long> sid = new List<long>();
            long tracks = _player.API.GetPropertyLong("track-list/count");
            for (long track = 0; track < tracks; ++track)
            {
                string type = _player.API.GetPropertyString($"track-list/{track}/type");
                long id = _player.API.GetPropertyLong(($"track-list/{track}/id"));
                if (type == "audio") aid.Add(id);
                else if (type == "sub") sid.Add(id);
            }
            if (GetTemplateChild("PART_Audio") is Button audio)
            {
                CreateMenu(audio, aid, (sender, e) =>
                {
                    if (_player != null
                    && sender is MenuItem item
                    && item.Tag is long id)
                    {
                        _player.API.SetPropertyLong("aid", id);
                    }
                });
            }
            if (GetTemplateChild("PART_Subtitle") is Button subtitle)
            {
                if (sid.Count > 0) sid.Insert(0, 0);
                CreateMenu(subtitle, sid, (sender, e) =>
                {
                    if (_player != null
                    && sender is MenuItem item
                    && item.Tag is long id)
                    {
                        _player.API.SetPropertyLong("sid", id);
                    }
                });
            }
        }

        private void CreateMenu(Button target, List<long> ids, Action<object, RoutedEventArgs> clickAction)
        {
            if (ids.Count < 1) return;
            ContextMenu menu = new ContextMenu();
            foreach (var id in ids)
            {
                var menuitem = new MenuItem();
                menuitem.Header = id.ToString();
                menuitem.Tag = id;
                menuitem.Click += (sender, e) => clickAction(sender, e);
                menu.Items.Add(menuitem);
            }
            target.ContextMenu = menu;
            target.Click += (sender, e) =>
            {
                target.ContextMenu.IsOpen = true;
            };
        }

        private void GetChapters()
        {
            if (_player == null) return;
            long chapters = _player.API.GetPropertyLong("chapters"); //set -> chapter
            ContextMenu chaptersMenu = new ContextMenu();
            for (long i = 0; i < chapters; i++)
            {
                var menuitem = new MenuItem();
                menuitem.Header = $"Chapter {i}";
                menuitem.Tag = i;
                menuitem.Click += (sender, args) =>
                {
                    if (_player != null
                    && sender is MenuItem item
                    && item.Tag is long chapter)
                    {
                        _player.API.SetPropertyLong("chapter", chapter);
                    }
                };
                chaptersMenu.Items.Add(menuitem);
            }
            if (GetTemplateChild("PART_Chapters") is Button btnChapters)
            {
                btnChapters.ContextMenu = null;
                btnChapters.ContextMenu = chaptersMenu;
                btnChapters.Click += (sender, e) =>
                {
                    btnChapters.ContextMenu.IsOpen = true;
                };
            }
        }

        public void Stop()
        {
            cont.ebool = false;
            _player?.Stop();
        }

        public void PlayNext()
        {
            cont.ebool = true;
            _player?.Stop();
        }

        public void PlayPause()
        {
            Playpause_Click(null, null);
        }

        private void Seek_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!_locker.IsLocked && _player != null)
            {
                _player.Position = TimeSpan.FromSeconds(e.NewValue);
            }
        }

        private void Volume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_player != null)
            {
                _player.Volume = Convert.ToInt32(e.NewValue);
            }
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            cont.ebool = false;
            _player?.Stop();
        }

        private void Playpause_Click(object sender, RoutedEventArgs e)
        {
            if (_player != null)
            {
                if (_player.IsPlaying)
                {
                    _player.Pause();
                }
                else
                {
                    _locker.PerformLockAction(() =>
                    {
                        if (GetTemplateChild("PART_Seek") is Slider seek)
                        {
                            seek.Maximum = _player.Duration.TotalSeconds;
                        }
                    });
                    _player.Resume();
                }
            }
        }

        private void _player_PositionChanged(object sender, NET.Player.MpvPlayerPositionChangedEventArgs e)
        {
            _locker.PerformLockAction(() =>
            {
                if (GetTemplateChild("PART_Seek") is Slider seek &&
                    _player.IsMediaLoaded)
                {

                    seek.Value = e.NewPosition.TotalSeconds;
                }
            });
        }


        public event EventHandler MediaUnloaded;
        public void WhenUnloaded(object sender, EventArgs e)
        {
            MediaUnloaded?.Invoke(this, cont);
        }

    }
}
