using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PokeBattle_Client
{
    public partial class MovePicker : Grid
    {
        public MovePicker()
        {
            InitializeComponent();
        }

        public event EventHandler<MoveSelectedArgs> MoveSelected;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            MoveSelected?.Invoke(this, new MoveSelectedArgs((Move)btn.DataContext, GetRow(btn) * 2 + GetColumn(btn) - 1));
        }

        private void Button_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            gridDescr.DataContext = ((Button)sender).DataContext;
        }
    }

    public class MoveSelectedArgs : EventArgs
    {
        public Move SelectedMove { get; }
        public int SelectedIndex { get; }

        public MoveSelectedArgs(Move move, int idx)
        {
            SelectedMove = move;
            SelectedIndex = idx;
        }
    }
}
