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
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            gridConnection.Visibility = Visibility.Hidden;
            server = new Server(txtIP.Text);
            await server.Connect();
            Pokemon[] pokeTeam = await server.ReadPokeTeam();
            try
            {
                img1.Source = GetPokemonImage(pokeTeam[0].Id);
            }
            catch { }
            hpb1.DataContext = pokeTeam[0];
        }

        private BitmapImage GetPokemonImage(int id)
        {
            return new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "\\sprites\\" + id + ".png", UriKind.Absolute));
        }
    }
}
