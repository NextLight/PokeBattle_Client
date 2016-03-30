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
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Server server;
        Pokemon[] pokeTeam;
        int activeIndex;
        int ActiveIndex
        {
            get { return activeIndex; }
            set
            {
                if (value < pokeTeam.Length)
                {
                    activeIndex = value;
                    pokev1.DataContext = ActivePokemon;
                    movePick.DataContext = ActivePokemon.Moves;
                }
            }
        }
        Pokemon ActivePokemon
        {
            get { return pokeTeam[activeIndex]; }
        }
        Pokemon opponent;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            gridConnection.Visibility = Visibility.Hidden;
            server = new Server(txtIP.Text);
            await server.Connect();
            pokeTeam = await server.ReadPokeTeam();
            opponent = await server.ReadPokemon();
            ActiveIndex = 0;
            pokev2.DataContext = opponent;
            pokePicker.PokeTeam = pokeTeam;
        }

        private async void movePick_MoveSelected(object sender, MoveSelectedArgs e)
        {
            await server.SendMove((byte)e.SelectedIndex);
            ReadInBattles();
        }

        private async void pokePicker_Picked(object sender, PickedPokemonArgs e)
        {
            ActiveIndex = e.PickedIndex;
            await server.SendSwitchPokemon((byte)e.PickedIndex);
            ReadInBattles();
        }

        private async void ReadInBattles()
        {
            ActivePokemon.InBattle = await server.ReadInBattle();
            opponent.InBattle = await server.ReadInBattle();
        }

        private void btnSwitch_Click(object sender, RoutedEventArgs e)
        {
            pokePicker.Show(activeIndex);
        }
    }
}
