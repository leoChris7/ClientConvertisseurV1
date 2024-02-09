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
        /// Constructeur de la window: initialisation des composants, d�finition du datacontext, r�cup�ration des donn�es.
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
        /// Permet de notifier qu'une propri�t� a chang�
        /// </summary>
        /// <param name="name">Nom de la propri�t�</param>
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        /// <summary>
        /// Fait appara�tre un message dans une bo�te de dialogue de mani�re asynchrone.
        /// </summary>
        /// <param name="v1">Contenu de la bo�te de dialogue</param>
        /// <param name="v2">Titre de la bo�te de dialogue</param>
        private async void MessageAsync(string v1, string v2)
        {
            ContentDialog message = new ContentDialog
            {
                Title = v2,
                Content = v1,
                CloseButtonText = "Ok"
            };
            // on dit qu'on doit montrer la bo�te de dialogue dans la fen�tre actuelle.
            message.XamlRoot = this.Content.XamlRoot;
            ContentDialogResult result = await message.ShowAsync();
        }

        /// <summary>
        /// Permet de r�cup�rer les donn�es de l'API de mani�re asynchrone.
        /// </summary>
        private async void GetDataOnLoadAsync()
        {
            // connexion � l'API
            WSService service = new WSService("http://localhost:44366/api/");
            List<Devise> result = await service.GetDevisesAsync("devises");
            if (result == null) { 
                // erreur d�clar�e (API non dispo)
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
        /// Gestion de l'�v�nement de clic sur  "Cliquer".
        /// </summary>
        /// <param name="sender">Bouton</param>
        /// <param name="e">Evenement</param>
        /// <exception cref="ArgumentNullException">Aucune monnaie n'a �t� choisie</exception>
        private void bt1_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            if (this.MonnaieChoisi != null)
            {
                this.ResultatConverti = this.EuroAConvertir * this.MonnaieChoisi.Taux;
            }
            else 
            {
                throw new ArgumentNullException("Le monnaie doit �tre choisie!");
            }
        }
    }
}