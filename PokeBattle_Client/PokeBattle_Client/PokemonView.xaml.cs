using System.Windows;
using System.Windows.Controls;

namespace PokeBattle_Client
{
    public partial class PokemonView : Grid
    {
        Side _imageSide = Side.Right;

        public PokemonView()
        {
            InitializeComponent();
        }

        private void UpdateGridHPMargin() =>
            gridHP.Margin = new Thickness(_imageSide == Side.Left ? img.ActualWidth + 10 : 10, 0, _imageSide == Side.Right ? img.ActualWidth + 10 : 10, 0);

        public Side ImageSide
        {
            set
            {
                _imageSide = value;
                img.HorizontalAlignment = value.ToAlignment();
                gridHP.HorizontalAlignment = value.ToReflectedAlignment();
                UpdateGridHPMargin();
            }
            get { return _imageSide; }
        }

        private void img_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateGridHPMargin();
        }

    }

    public enum Side { Left, Right }

    public static class SideExtension
    {
        public static HorizontalAlignment ToAlignment(this Side s) =>
            s == Side.Right ? HorizontalAlignment.Right : HorizontalAlignment.Left;

        public static HorizontalAlignment ToReflectedAlignment(this Side s) => 
            s != Side.Right ? HorizontalAlignment.Right : HorizontalAlignment.Left;
    }
}
