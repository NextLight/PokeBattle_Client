using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PokeBattle_Client
{
    public partial class PokemonPicker : ListBox
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
                foreach (Pokemon p in _pokeTeam)
                    Items.Add(p);
            }
        }

        public void Show(int activePokemonIndex)
        {
            Visibility = Visibility.Visible;
            if (_pokeTeam != null && _pokeTeam.Length > activePokemonIndex)
            {
                Items.Insert(0, _pokeTeam[activePokemonIndex]);
                Items.Remove(_pokeTeam[activePokemonIndex]);
            }
        }

        public event EventHandler<PickedPokemonArgs> Picked;
        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {   
                if (SelectedIndex != -1)
                    Picked?.Invoke(this, new PickedPokemonArgs(_pokeTeam[SelectedIndex], SelectedIndex));
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
