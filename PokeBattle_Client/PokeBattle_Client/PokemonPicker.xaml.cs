using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PokeBattle_Client
{
    public partial class PokemonPicker : Popup
    {
        public PokemonPicker()
        {
            InitializeComponent();
        }

        Pokemon[] _pokeTeam;
        public Pokemon[] PokeTeam
        {
            set
            {
                _pokeTeam = value;
            }
        }

        public void Show(int activePokemonIndex, bool canCLose)
        {
            CanClose = canCLose;
            Visibility = Visibility.Visible;
            if (_pokeTeam != null && activePokemonIndex < _pokeTeam.Length)
                listBox.ItemsSource = _pokeTeam.Where((p, i) => !p.Fainted && i != activePokemonIndex);
        }

        public event EventHandler<PickedPokemonArgs> Picked;
        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listBox.SelectedIndex != -1)
            {
                var poke = (Pokemon)listBox.SelectedItem;
                Picked?.Invoke(this, new PickedPokemonArgs(poke, Array.IndexOf(_pokeTeam, poke)));
            }
            Visibility = Visibility.Hidden;
        }
    }

    public class PickedPokemonArgs : EventArgs
    {
        public Pokemon PickedPokemon { get; }
        public int PickedIndex { get; }

        public PickedPokemonArgs(Pokemon poke, int idx)
        {
            PickedPokemon = poke;
            PickedIndex = idx;
        }
    }
}
