using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WSConvertisseur.Models;

namespace ClientConvertisseurV1.Services
{
    public interface IService
    {
        Task<List<Devise>> GetDevisesAsync(string nomControleur);
    }
}
