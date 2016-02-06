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
            pokev1.DataContext = pokeTeam[0];
            pokev2.DataContext = opponent;
            movePick.DataContext = pokeTeam[0].Moves;
        }

        private async void movePick_MoveSelected(object sender, MoveSelectedArgs e)
        {
            await server.SendMove((byte)e.SelectedIndex);
            pokeTeam[0].InBattle = await server.ReadInBattle();
            opponent.InBattle = await server.ReadInBattle();
        }
    }
}
