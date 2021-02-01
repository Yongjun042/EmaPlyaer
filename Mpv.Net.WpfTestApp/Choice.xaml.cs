using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EmaPlayer
{
    /// <summary>
    /// Interaction logic for Choice.xaml
    /// </summary>
    public partial class Choice : Window
    {
        public Choice()
        {
            InitializeComponent();
            Hidden.Text = Settings.Default.Hidden.ToString();
            if(Settings.Default.Stone1)
            {
                stone1L.IsChecked = true;
            }
            else
            {
                stone1R.IsChecked = true;
            }

            if (Settings.Default.Stone2)
            {
                stone2L.IsChecked = true;
            }
            else
            {
                stone2R.IsChecked = true;
            }

            if (Settings.Default.Stone3)
            {
                stone3L.IsChecked = true;
            }
            else
            {
                stone3R.IsChecked = true;
            }
            if (Settings.Default.Random)
            {
                isRandom.IsChecked = true;
            }
            else
            {
                isRandom.IsChecked = false;
                stoneList.IsEnabled = false;
            }
            isRandom.IsChecked = Settings.Default.Random;
            Title = EmaPlayer.Properties.Resources.SelectStone;
            isRandom.Content = EmaPlayer.Properties.Resources.isRandom;
            stonelabel1.Content= EmaPlayer.Properties.Resources.Stone1;
            stonelabel2.Content = EmaPlayer.Properties.Resources.Stone2;
            stonelabel3.Content = EmaPlayer.Properties.Resources.Stone3;
            stone1L.Content = EmaPlayer.Properties.Resources.L;
            stone1R.Content = EmaPlayer.Properties.Resources.R;
            stone2L.Content = EmaPlayer.Properties.Resources.L;
            stone2R.Content = EmaPlayer.Properties.Resources.R;
            stone3L.Content = EmaPlayer.Properties.Resources.L;
            stone3R.Content = EmaPlayer.Properties.Resources.R;
            Hiddengroup.Header = EmaPlayer.Properties.Resources.HiddenProb;
            HiddenText.Text = EmaPlayer.Properties.Resources.HiddenDesc;
            SaveButton.Content = EmaPlayer.Properties.Resources.Save;

        }

        private void Hidden_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValid(((TextBox)sender).Text + e.Text);
        }
        public static bool IsValid(string str)
        {
            int i;
            return int.TryParse(str, out i) && i >= 0 && i <= 1000;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.Hidden = Int32.Parse(Hidden.Text);
            Settings.Default.Stone1 = (stone1L.IsChecked ==true);
            Settings.Default.Stone2 = (stone2L.IsChecked == true);
            Settings.Default.Stone3 = (stone3L.IsChecked == true);
            Settings.Default.Random = (isRandom.IsChecked == true);
            Settings.Default.Save();
            Close();
        }

        private void isRandom_Changed(object sender, RoutedEventArgs e)
        {
            CheckBox li = sender as CheckBox;
            if(li.IsChecked == true)
            {
                stoneList.IsEnabled = true;
            }
            else
            {
                stoneList.IsEnabled = false;
            }
        }
    }
}
