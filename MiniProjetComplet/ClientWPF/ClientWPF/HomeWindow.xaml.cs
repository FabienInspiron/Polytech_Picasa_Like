using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ClientWPF.WebService;

namespace ClientWPF
{
    /// <summary>
    /// Logique d'interaction pour HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        ServiceClient service = new ServiceClient();

        public HomeWindow()
        {
            InitializeComponent();
        }

        private void connexion_Click(object sender, RoutedEventArgs e)
        {
            Utilisateur user = service.Connexion(login.Text, password.Text);

            if (user == null)
            {
                string messageBoxText = "Connexion impossible";
                string caption = "Word Processor";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show(messageBoxText, caption, button, icon);
            }
            else
            {
                MainWindow m = new MainWindow(user.Id);
                m.Show();
                this.Close();
            }

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Utilisateur u = new Utilisateur();
            u.Mdp = textpassword.Text;
            u.Nom = textlogin.Text;
            u.Prenom = textprenom.Text;
            Utilisateur ret = service.Inscription(u);

            if (ret == null)
            {
                string messageBoxText = "Inscription impossible";
                string caption = "Word Processor";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show(messageBoxText, caption, button, icon);
            }
            else
            {
                string messageBoxText = "Inscription reussite";
                string caption = "Word Processor";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Information;
                MessageBox.Show(messageBoxText, caption, button, icon);
                textprenom.Text = "";
                textpassword.Text = "";
                textmail.Text = "";
                textlogin.Text = "";
            }
        }
    }
}
