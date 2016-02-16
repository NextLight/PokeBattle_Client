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
    public partial class PokemonView : Grid
    {
        public PokemonView()
        {
            InitializeComponent();
        }

        Side imageSide = Side.Right;

        private void UpdateGridHPMargin()
        {
            gridHP.Margin = new Thickness(imageSide == Side.Left ? img.ActualWidth + 10 : 10, 0, imageSide == Side.Right ? img.ActualWidth + 10 : 10, 0);
        }

        public Side ImageSide
        {
            set
            {
                imageSide = value;
                img.HorizontalAlignment = value.ToAlignment();
                gridHP.HorizontalAlignment = value.ToReflectedAlignment();
                UpdateGridHPMargin();
            }
            get { return imageSide; }
        }

        private void img_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateGridHPMargin();
        }

    }
    public enum Side
    {
        Left, Right
    }

    public static class SideExtension
    {
        public static HorizontalAlignment ToAlignment(this Side s)
        {
            return s == Side.Right ? HorizontalAlignment.Right : HorizontalAlignment.Left;
        }

        public static HorizontalAlignment ToReflectedAlignment(this Side s)
        {
            return s != Side.Right ? HorizontalAlignment.Right : HorizontalAlignment.Left;
        }
    }
}
