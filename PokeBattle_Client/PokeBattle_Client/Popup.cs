using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PokeBattle_Client
{
    public class Popup : Grid
    {
        private Button btnClose;

        public event EventHandler Closed;

        // I can't use xaml because someone decided that xaml-defined controls can't be used as base for other controls (i.e. PokemonPicker)
        public Popup()
        {
            Loaded += Popup_Loaded;
            btnClose = new Button { Content = 'X', HorizontalAlignment = HorizontalAlignment.Right,  VerticalAlignment = VerticalAlignment.Top, Width = Height = 25, FontSize = 16 };
            btnClose.Click += btnClose_Click;
            Children.Add(new Rectangle { Fill = Brushes.White, Opacity = 0.8 });
            Children.Add(btnClose);
        }

        private void Popup_Loaded(object sender, RoutedEventArgs e)
        {
            Height = double.NaN; // auto
            if (ColumnDefinitions.Count > 0)
            {
                SetColumnSpan(Children[0], ColumnDefinitions.Count);
                SetColumnSpan(Children[1], ColumnDefinitions.Count);
            }
        }

        public bool CanClose
        {
            set
            {
                btnClose.Visibility = value ? Visibility.Visible : Visibility.Hidden;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Hidden;
            Closed?.Invoke(this, null);
        }
    }
}
