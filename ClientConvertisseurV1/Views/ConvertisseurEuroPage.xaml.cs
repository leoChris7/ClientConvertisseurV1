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

namespace ClientConvertisseurV1.Views
{
    public sealed partial class ConvertisseurEuroPage : Page
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Devise> listeDevises = new ObservableCollection<Devise>();

        private Devise monnaieChoisi;
        private double euroAConvertir;
        private double resultatConverti;

        public ConvertisseurEuroPage()
        {
            this.InitializeComponent();
            GetDataOnLoadAsync();
            this.DataContext = this;

            
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
                return this.resultatConverti;
            }

            set
            {
                this.resultatConverti = value;
                OnPropertyChanged(nameof(ResultatConverti)); // Notification de changement de propriété
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private async void MessageAsync(string v1, string v2)
        {
            ContentDialog message = new ContentDialog
            {
                Title = v2,
                Content = v1,
                CloseButtonText = "Ok"
            };
            message.XamlRoot = this.Content.XamlRoot;
            ContentDialogResult result = await message.ShowAsync();
        }

        private async void GetDataOnLoadAsync()
        {
            WSService service = new WSService("http://localhost:44366/api/");
            List<Devise> result = await service.GetDevisesAsync("devises");
            if (result == null) { 
                MessageAsync("Api non disponible !", "Erreur");
            }
            else
            { 
                foreach(Devise devise in result)
                {
                    this.ListeDevises.Add(devise);
                }
                this.DataContext = this;
            }
        }

        private void bt1_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            if (this.MonnaieChoisi != null) { 
                this.ResultatConverti = EuroAConvertir * this.MonnaieChoisi.Taux;
            } 
            else
            {
                throw new ArgumentNullException("Le monnaie doit être choisie!");
            }
        }
    }
}