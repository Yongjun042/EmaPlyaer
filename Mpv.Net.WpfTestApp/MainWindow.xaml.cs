using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;
using System;
using Mpv.Net.Wpf;
using System.Windows.Threading;
using EmaPlayer;
using System.Windows.Input;

namespace EmaPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            for (int i = 0; i < totalnum; i++)
            {
                fileNames[i] = i.ToString().PadLeft(5, '0') + ".m2ts";
            }
            Player.MediaUnloaded += new EventHandler(Play_Ema);
            CreditSkip.IsChecked = EmaPlayer.Settings.Default.CreditSkip;
            StoneSkip.IsChecked = EmaPlayer.Settings.Default.StoneSkip;
            SelectStone.Content = EmaPlayer.Properties.Resources.SelectStone;
            CreditSkip.Content = EmaPlayer.Properties.Resources.CreditSkip;
            StoneSkip.Content = EmaPlayer.Properties.Resources.StoneSkip;
            MenuFile.Header = EmaPlayer.Properties.Resources.File;
            OpenFolder.Header = EmaPlayer.Properties.Resources.OpenFolder;
            MenuAbout.Header = EmaPlayer.Properties.Resources.About;
            MenuAbout2.Header = EmaPlayer.Properties.Resources.About;
            title = EmaPlayer.Properties.Resources.Title;
            this.Title = title;
        }
        private const int totalnum = 66;
        private string[] fileNames = new string[totalnum];
        private string[] filePaths = new string[totalnum];
        private int[] stoneNum = { 13, 19, 23, 45, 49 };
        private int videoNum = 0;
        private int choice1 = 0;
        private string title = "EmaPlayer";
        bool? isCreditChecked = null;
        bool? isStoneChecked = null;
        private Random r = new Random();


        //Show a file select dialog and load the selected file
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //var dialog = new OpenFileDialog();
            //if (dialog.ShowDialog() == true)
            //{
            //    Player.LoadFile(dialog.FileName);
            //}
            var dialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true
            };
            CommonFileDialogResult result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
            {
                for (int i = 0; i < totalnum; i++)
                {
                    filePaths[i] = Path.Combine(dialog.FileName, fileNames[i]);
                }
                //file check 만들기
                videoNum = IsHidden(5);
                Player.LoadFile(filePaths[videoNum]);
                this.Title = title + " - " + filePaths[videoNum];
                SeekbarTime();
            }
        }

        // When closing the window stop played video
        // and Dispose native dll resources
        // A wait is necessary after stop, because
        // it doesn't happen instantainously
        private async void Window_Closing(object sender, CancelEventArgs e)
        {
            if (Player != null)
            {
                Player.Stop();
                await Task.Delay(1000);
                Player.Dispose();
            }
        }

        private void CreditSkipChanged(object sender, RoutedEventArgs e)
        {
            EmaPlayer.Settings.Default.CreditSkip = (CreditSkip.IsChecked == true);
            EmaPlayer.Settings.Default.Save();
        }
        private void StoneSkipChanged(object sender, RoutedEventArgs e)
        {
            EmaPlayer.Settings.Default.StoneSkip = (StoneSkip.IsChecked == true);
            EmaPlayer.Settings.Default.Save();
        }
        private void CheckCheckBox()
        {
            CreditSkip.Dispatcher.Invoke(new Action(() =>
            {
                isCreditChecked = CreditSkip.IsChecked;
            }),
                DispatcherPriority.Normal);
            StoneSkip.Dispatcher.Invoke(new Action(() =>
            {
                isStoneChecked = StoneSkip.IsChecked;
            }),
                DispatcherPriority.Normal);
        }

        private void Play_Ema(object sender, EventArgs e)
        {
            if (!((EventBoolArgs)e).ebool)
            {
                return;
            }
            CheckCheckBox();
            switch (videoNum)
            {
                case 5:
                    videoNum = r.Next(7, 11);
                    break;
                case 7:
                case 8:
                case 9:
                case 10:
                    //로랑신사 
                    videoNum = IsHidden(11);
                    break;
                case 11:
                    //인업
                    if (isStoneChecked == true)
                    {
                        videoNum = 17 + WhichStone(1);
                    }
                    else
                    {
                        videoNum = 13;
                    }
                    break;
                case 13:
                    videoNum = 15 + WhichStone(1);
                    break;
                case 15:
                    videoNum = 17;
                    break;
                case 16:
                    videoNum = 18;
                    break;
                case 17:
                    if (isStoneChecked == true)
                    {
                        choice1 = WhichStone(2);
                        videoNum = ShowMemory();
                    }
                    else
                    {
                        videoNum = 19;
                    }
                    break;
                case 18:
                    if (isStoneChecked == true)
                    {
                        choice1 = 2+ WhichStone(2);
                        videoNum = ShowMemory();
                    }
                    else
                    {
                        videoNum = 23;
                    }
                    break;
                case 19:
                    choice1 = WhichStone(2);
                    videoNum = 21 + choice1;
                    break;
                case 23:
                    choice1 = 2+ WhichStone(2);
                    videoNum = 23 + choice1;
                    break;
                case 21:
                case 22:
                case 25:
                case 26:
                    //2번째 비석 석택
                    videoNum = ShowMemory();
                    break;
                case 27:
                case 28:
                case 29:
                case 30:
                    //소녀의 기억
                    videoNum = 35;
                    break;
                case 31:
                case 32:
                case 33:
                case 34:
                    //소년의 기억
                    videoNum = 36;
                    break;
                case 35:
                case 36:
                    videoNum = 37 + choice1;
                    break;
                case 37:
                case 38:
                    videoNum = IsHidden(41);
                    break;
                case 39:
                case 40:
                    videoNum = IsHidden(43);
                    break;
                case 41:
                    if (isStoneChecked == true)
                    {
                        if (isCreditChecked == true)
                        {
                            videoNum = IsHidden(5);
                        }
                        else
                        {
                            videoNum = 53;
                        }
                    }
                    else
                    {
                        videoNum = 45;
                    }
                    break;
                case 45:
                    videoNum = 47 + WhichStone(3);
                    break;
                case 43:
                    if (isStoneChecked == true)
                    {
                        if (isCreditChecked == true)
                        {
                            videoNum = IsHidden(5);
                        }
                        else
                        {
                            videoNum = 53;
                        }
                    }
                    else
                    {
                        videoNum = 49;
                    }
                    break;
                case 49:
                    videoNum = 51 + WhichStone(1);
                    break;
                case 47:
                case 48:
                case 51:
                case 52:
                    if (isCreditChecked == true)
                    {
                        videoNum = IsHidden(5);
                    }
                    else
                    {
                        videoNum = 53;
                    }
                    break;
                default:
                    videoNum = IsHidden(5);
                    break;

            }
            Player.LoadFile(filePaths[videoNum]);
            this.Dispatcher.Invoke(() =>
            {
                Title = title + " - " + filePaths[videoNum];
            });
            CheckStone();
            SeekbarTime();
        }
        private async void CheckStone()
        {
            if (Array.Exists(stoneNum, i => i.Equals(videoNum)))
            {
                await Task.Delay(10000);
                Player.PlayNext();
            }

        }

        private async void SeekbarTime()
        {
            int count = 0;
            while (Player.GetTotalTime() == 0)
            {
                await Task.Delay(500);
                count++;
            }
        }

        private int ShowMemory()
        {
            int result = 27;
            //decide he, she
            if (r.Next(0, 6) == 3)
            {
                result += 4;
            }
            result += r.Next(0, 4);
            return result;
        }

        private int IsHidden(int n)
        {
            int a = r.Next(0, 1000);
            if (a<Settings.Default.Hidden)
            {
                return n + 1;
            }
            else
            {
                return n;
            }
        }

        private int WhichStone(int a)
        {
            int n;
            if (Settings.Default.Random)
            {
                n = r.Next(0, 2);
            }
            else
            {
                switch(a)
                {
                    case 1:
                        n = Settings.Default.Stone1? 0 : 1;
                        break;
                    case 2:
                        n = Settings.Default.Stone2 ? 0 : 1;
                        break;
                    case 3:
                        n = Settings.Default.Stone3 ? 0 : 1;
                        break;
                    default:
                        n = 0;
                        break;
                }
            }
            return n;
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            About aboutWindow = new About();
            aboutWindow.ShowDialog();
        }

        private void SelectStone_Click(object sender, RoutedEventArgs e)
        {
            Choice choiceWindow = new Choice();
            choiceWindow.ShowDialog();
        }

        private void Window_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                if (this.WindowState != WindowState.Maximized)
                {
                    this.WindowStyle = WindowStyle.None;
                    this.WindowState = WindowState.Maximized;
                    MainMenu.Visibility = Visibility.Collapsed;
                    option.Visibility = Visibility.Collapsed;
                    Player.Visibility = Visibility.Collapsed;
                }
                else
                {
                    this.WindowStyle = WindowStyle.SingleBorderWindow;
                    this.WindowState = WindowState.Normal;
                    MainMenu.Visibility = Visibility.Visible;
                    option.Visibility = Visibility.Visible;
                    Player.Visibility = Visibility.Visible;
                }
            }

            if (e.Key == System.Windows.Input.Key.H)
            {
                if (MainMenu.Visibility == Visibility.Visible)
                {
                    MainMenu.Visibility = Visibility.Collapsed;
                    option.Visibility = Visibility.Collapsed;
                    Player.Visibility = Visibility.Collapsed;
                }
                else
                {
                    MainMenu.Visibility = Visibility.Visible;
                    option.Visibility = Visibility.Visible;
                    Player.Visibility = Visibility.Visible;
                }
            }
        }



    }

}
