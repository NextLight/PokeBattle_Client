using System.Linq;
using System.Windows;

namespace PokeBattle_Client
{
    public partial class PokemonInfo : Popup
    {
        public PokemonInfo()
        {
            InitializeComponent();
            Visibility = Visibility.Hidden;
        }

        public void Show(Pokemon p)
        {
            lblName.Content = p.Name;
            img.Source = p.Image;
            listView.ItemsSource = typeof(StatsClass).GetProperties()
                .Where(pr => !pr.Name.EndsWith("Hp"))
                .Select(pr => new { Name = pr.Name, Value = pr.GetValue(p.InBattle.Stats) });
            moves.DataContext = p.Moves;
            Visibility = Visibility.Visible;
        }

        private void MovePicker_MoveSelected(object sender, MoveSelectedArgs e)
        {
            // TODO
        }
    }
}
