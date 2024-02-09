using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WSConvertisseur.Models;

namespace ClientConvertisseurV1.Services
{
    internal class WSService : IService
    {
        private HttpClient client;

        /// <summary>
        /// Permet de se connecter à une API dont l'URL est précisé en arguments
        /// </summary>
        /// <param name="urlAPI">URL de l'API</param>
        public WSService(string urlAPI)
        {
            // création du client
            this.Client = new HttpClient();
            // chemin vers la ressource url
            this.Client.BaseAddress = new Uri(urlAPI);
            this.Client.DefaultRequestHeaders.Accept.Clear();
            this.Client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public HttpClient Client
        {
            get
            {
                return this.client;
            }

            set
            {
                this.client = value;
            }
        }

        /// <summary>
        /// Récupération des devises depuis l'API
        /// </summary>
        /// <param name="nomControleur">nom du controller de l'API (devise)</param>
        /// <returns>Une liste de devises sauf si exception: null</returns>
        public async Task<List<Devise>> GetDevisesAsync(string nomControleur)
        {
            try
            {
                // récupération de manière asynchrone, sous format JSON, de toutes les devises
                return await this.Client.GetFromJsonAsync<List<Devise>>(nomControleur);
            }
            catch (Exception e)
            {
                // on affiche l'erreur et on retourne null
                Console.WriteLine($"Erreur:{e}");
                return null;
            }
        }
    }
}
