using PokemonLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.Data;

namespace PokemonCatch
{
    /// <summary>
    /// Interaction logic for PokemonWindow.xaml
    /// </summary>
    public partial class PokemonWindow : Window
    {
        private Timer timer;
        private Random random = new Random();
        private string folderPath = @"C:\Users\12002009\Documents\Cadvanced\PokemonCatch\PokemonCatch\pokemon\"; // Replace with your folder path
        public int randomNumber;
        private int previousRandomNumber = -1;
        public string trainerName;

        public PokemonWindow(string Trainernaam)
        {
            InitializeComponent();
            DataBewerking.InitializeDataBewerking();

            trainerName = Trainernaam;
            timer = new Timer(5000);
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;

            DgdPokemon.ItemsSource = PokemonData.GetPokemonDataView();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            DataBewerking.SaveTrainerDataSet();
        }

        private void TxtGok_SelectionChanged(object sender, RoutedEventArgs e)
        {
            DgdPokemon.ItemsSource = PokemonData.GetPokemonDataViewByNameFilter(TxtGok.Text);
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            // Ensure this runs on the UI thread
            Dispatcher.Invoke(() => LoadRandomImage());
        }

        private int GetFileCount(string path)
        {
            if (Directory.Exists(path))
            {
                return Directory.GetFiles(path, "*.png").Length;
            }
            else
            {
                MessageBox.Show("Path niet gevonden");
                return 0;
            }
        }

        private void LoadRandomImage()
        {
            int maxNumber = GetFileCount(folderPath);
            if (maxNumber > 0)
            {
                int newRandomNumber;
                do
                {
                    newRandomNumber = random.Next(1, maxNumber + 1);
                } while (newRandomNumber == previousRandomNumber);
                previousRandomNumber = newRandomNumber; 

                string imagePath = System.IO.Path.Combine(folderPath, $"{newRandomNumber}.png");

                if (File.Exists(imagePath))
                {
                    ImgPokemon.Source = new BitmapImage(new Uri(imagePath, UriKind.Absolute));
                }
                else
                {
                    Console.WriteLine($"Image {newRandomNumber}.png does not exist.");
                }
            }
            else
            {
                Console.WriteLine("No files found in the directory.");
            }
        }

        private void DgdPokemon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DgdPokemon.SelectedItem is DataRowView selectedRow)
            {
                int selectedNumber = Convert.ToInt32(selectedRow["Number"]);
                int comparisonValue = previousRandomNumber;
                if (comparisonValue == selectedNumber)
                {
                    CaugthPokemonData.CatchPokemon(TrainerData.GetTrainerIdByTrainerName(trainerName), selectedNumber);
                    TxtGok.Text = string.Empty;

                    string imagePath = System.IO.Path.Combine(folderPath, $"{selectedNumber}.png");
                    if (File.Exists(imagePath))
                    {
                        BitmapImage bitmap = new BitmapImage(new Uri(imagePath));
                        bitmap.DecodePixelWidth = 5;
                        bitmap.DecodePixelHeight = 5;
                        Image imageControl = new Image();
                        imageControl.Source = bitmap;
                        imageControl.Width = 50;
                        imageControl.Height = 50;
                        LstPokemon.Items.Add(imageControl);
                    }
                }
            }
        }
    }
}
