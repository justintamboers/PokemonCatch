using Microsoft.Win32;
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

namespace PokemonCatch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnNiewgeb_Click(RoutedEventArgs e)
        {

        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (!PokemonLib.TrainerData.CheckTrainerLogin(TxtNaam.Text, PwdWachtwoord.Password))
            {
                MessageBox.Show("Foute login");
            }
            else
            {
                PokemonWindow pokemonWindow = new PokemonWindow(TxtNaam.Text);
                pokemonWindow.ShowDialog();
            }
            
            
        }

        private void BtnNiewgeb_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PokemonLib.TrainerData.CreateTrainer(TxtNaam.Text,PwdWachtwoord.Password);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
