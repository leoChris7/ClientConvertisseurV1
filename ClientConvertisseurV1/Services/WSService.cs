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

        public async Task<List<Devise>> GetDevisesAsync(string nomControleur)
        {
            try
            {
                return await this.Client.GetFromJsonAsync<List<Devise>>(nomControleur);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}
