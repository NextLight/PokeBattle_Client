using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PokeBattle_Client
{
    public partial class MovePicker : Grid
    {
        public MovePicker()
        {
            InitializeComponent();
        }

        public event EventHandler MoveSelected;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MoveSelected != null)
            {
                MoveSelected(this, );
            }
        }
    }

    class MoveSelectedArgs : EventArgs
    {

    }
}
