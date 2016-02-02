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
            if (MoveSelected != null)
            {
                Button btn = (Button)sender;
                MoveSelected(this, new MoveSelectedArgs((Move)btn.DataContext, GetRow(btn) * 2 + GetColumn(btn)));
            }
        }
    }

    public class MoveSelectedArgs : EventArgs
    {
        public Move SelectedMove { get; private set; }
        public int SelectedIndex { get; private set; }

        public MoveSelectedArgs(Move move, int idx)
        {
            SelectedMove = move;
            SelectedIndex = idx;
        }
    }
}
