using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using WSConvertisseur.Models;
using ClientConvertisseurV1.Services;
using System.Collections.ObjectModel;
using Windows.UI.Popups;
using System;

namespace ClientConvertisseurV1.Views
{
    public sealed partial class ConvertisseurEuroPage : Page
    {
        public ObservableCollection<Devise> ListeDevises;

        public string EuroChoisi
        {
            get
            {
                return euroChoisi;
            }

            set
            {
                euroChoisi = value;
            }
        }

        public double TauxChange
        {
            get
            {
                return tauxChange;
            }

            set
            {
                tauxChange = value;
            }
        }

        public int Id
        {
            get
            {
                return this.id;
            }

            set
            {
                this.id = value;
            }
        }

        private string euroChoisi;
        private double tauxChange;
        private int id;
        

        public ConvertisseurEuroPage()
        {
            this.InitializeComponent();
                
               
            // Définissez le DataContext sur cette page pour permettre le binding
            this.DataContext = this;
            GetDataOnLoadAsync();
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
            WSService service = new WSService("https://localhost:44366/api/");
            List<Devise> result = await service.GetDevisesAsync("devises");
            if (result == null)
                MessageAsync("Api non disponible !", "Erreur");
            else
                ListeDevises = new ObservableCollection<Devise>(result);
        }
    }
}