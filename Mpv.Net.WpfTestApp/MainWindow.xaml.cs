using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;
using System;
using Mpv.Net.Wpf;
using System.Windows.Threading;

namespace Mpv.Net.WpfTestApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Title = title;
            for (int i = 0; i < totalnum; i++)
            {
                fileNames[i] = i.ToString().PadLeft(5, '0') + ".m2ts";
            }
            Player.MediaUnloaded += new EventHandler(Play_Ema);

            CreditSkip.IsChecked = EmaPlayer.Settings.Default.CreditSkip;
            StoneSkip.IsChecked = EmaPlayer.Settings.Default.StoneSkip;
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
                    //밤의인업 히든
                    videoNum = IsHidden(11);
                    break;
                case 11:
                    if (isStoneChecked == true)
                    {
                        videoNum = r.Next(17, 19);
                    }
                    else
                    {
                        videoNum = 13;
                    }
                    break;
                case 13:
                    videoNum = r.Next(15, 17);
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
                        choice1 = r.Next(0, 2);
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
                        choice1 = r.Next(2, 4);
                        videoNum = ShowMemory();
                    }
                    else
                    {
                        videoNum = 23;
                    }
                    break;
                case 19:
                    choice1 = r.Next(0, 2);
                    videoNum = 21 + choice1;
                    break;
                case 23:
                    choice1 = r.Next(2, 4);
                    videoNum = 23 + choice1;
                    break;
                case 21:
                case 22:
                case 25:
                case 26:
                    videoNum = ShowMemory();
                    break;
                case 27:
                case 28:
                case 29:
                case 30:
                    videoNum = 35;
                    break;
                case 31:
                case 32:
                case 33:
                case 34:
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
                    videoNum = r.Next(47, 49);
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
                    videoNum = r.Next(51, 53);
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
        }
        private async void CheckStone()
        {
            if (Array.Exists(stoneNum, i => i.Equals(videoNum)))
            {
                await Task.Delay(10000);
                Player.PlayNext();
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
            if (r.Next(0, 1000) == 8)
            {
                return n + 1;
            }
            else
            {
                return n;
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            About aboutWindow = new About();
            aboutWindow.ShowDialog();
        }

    }

}
