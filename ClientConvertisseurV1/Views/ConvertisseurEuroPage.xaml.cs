using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using WSConvertisseur.Models;
using ClientConvertisseurV1.Services;
using System.Collections.ObjectModel;
using Windows.UI.Popups;
using System;
using System.ComponentModel;
using System.Linq;
using Microsoft.UI.Xaml;
using System.Runtime.CompilerServices;

namespace ClientConvertisseurV1.Views
{
    public sealed partial class ConvertisseurEuroPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Devise> listeDevises = new ObservableCollection<Devise>();

        private Devise monnaieChoisi;
        private double euroAConvertir;
        private double resultatConverti;

        /// <summary>
        /// Constructeur de la window: initialisation des composants, définition du datacontext, récupération des données.
        /// </summary>
        public ConvertisseurEuroPage()
        {
            this.InitializeComponent();
            this.DataContext = this;
            GetDataOnLoadAsync();
        }

        public ObservableCollection<Devise> ListeDevises
        {
            get
            {
                return this.listeDevises;
            }

            set
            {
                this.listeDevises = value;
                OnPropertyChanged(nameof(ListeDevises));
            }
        }

        public Devise MonnaieChoisi
        {
            get
            {
                return monnaieChoisi;
            }

            set
            {
                monnaieChoisi = value;
                OnPropertyChanged(nameof(MonnaieChoisi));
            }
        }

        public double EuroAConvertir
        {
            get
            {
                return euroAConvertir;
            }

            set
            {
                euroAConvertir = value; 
                OnPropertyChanged(nameof(EuroAConvertir));
            }
        }

        public double ResultatConverti
        {
            get
            {
                return resultatConverti;
            }

            set
            {
                resultatConverti = value;
                OnPropertyChanged(nameof(ResultatConverti));
            }
        }

        /// <summary>
        /// Permet de notifier qu'une propriété a changé
        /// </summary>
        /// <param name="name">Nom de la propriété</param>
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        /// <summary>
        /// Fait apparaître un message dans une boîte de dialogue de manière asynchrone.
        /// </summary>
        /// <param name="v1">Contenu de la boîte de dialogue</param>
        /// <param name="v2">Titre de la boîte de dialogue</param>
        private async void MessageAsync(string v1, string v2)
        {
            ContentDialog message = new ContentDialog
            {
                Title = v2,
                Content = v1,
                CloseButtonText = "Ok"
            };
            // on dit qu'on doit montrer la boîte de dialogue dans la fenêtre actuelle.
            message.XamlRoot = this.Content.XamlRoot;
            ContentDialogResult result = await message.ShowAsync();
        }

        /// <summary>
        /// Permet de récupérer les données de l'API de manière asynchrone.
        /// </summary>
        private async void GetDataOnLoadAsync()
        {
            // connexion à l'API
            WSService service = new WSService("http://localhost:44366/api/");
            List<Devise> result = await service.GetDevisesAsync("devises");
            if (result == null) { 
                // erreur déclarée (API non dispo)
                MessageAsync("Api non disponible !", "Erreur");
            }
            else
            { 
                // on fait le binding
                foreach(Devise devise in result)
                {
                    this.ListeDevises.Add(devise);
                }
                this.DataContext = this;
            }
        }

        /// <summary>
        /// Gestion de l'évènement de clic sur  "Cliquer".
        /// </summary>
        /// <param name="sender">Bouton</param>
        /// <param name="e">Evenement</param>
        /// <exception cref="ArgumentNullException">Aucune monnaie n'a été choisie</exception>
        private void bt1_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            if (this.MonnaieChoisi != null)
            {
                this.ResultatConverti = this.EuroAConvertir * this.MonnaieChoisi.Taux;
            }
            else 
            {
                throw new ArgumentNullException("Le monnaie doit être choisie!");
            }
        }
    }
}