using System;
using System.Windows;
using System.Windows.Controls;

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
            MoveSelected?.Invoke(this, new MoveSelectedArgs((Move)btn.DataContext, GetRow(btn) * 2 + GetColumn(btn)));
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
