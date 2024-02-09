using System.ComponentModel.DataAnnotations;

namespace WSConvertisseur.Models
{
    /// <summary>
    /// La classe des objets devise. Contient l'ID, le nom de la devise, le taux.
    /// </summary>
    public class Devise
    {
        private int id;
        private string? nomDevise;
        private double taux;

        public Devise(int id, string? nomDevise, double taux)
        {
            this.Id = id;
            this.NomDevise = nomDevise;
            this.Taux = taux;
        }

        
        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string? NomDevise
        {
            get
            {
                return nomDevise;
            }

            set
            {
                nomDevise = value;
            }
        }

        public double Taux
        {
            get
            {
                return this.taux;
            }

            set
            {
                this.taux = value;
            }
        }

        /// <summary>
        /// Vérification si deux devises sont égales à partir de leur taux et de leur nom.
        /// </summary>
        /// <param name="obj">La devise à comparer avec this.</param>
        /// <returns>True si elles sont égales, False si elle ne le sont pas.</returns>
        public override bool Equals(object? obj)
        {
            Devise nouvelleDevise = ((Devise)obj);
            return this.Taux == nouvelleDevise.Taux && this.NomDevise == nouvelleDevise.NomDevise;
        }

        /// <summary>
        /// Affichage des devises avec l'ID, le nom et le taux dans la console.
        /// </summary>
        /// <returns>Une chaîne de caractère contenant toutes les devises.</returns>
        public override string? ToString()
        {
            return this.Id + "\n" + this.NomDevise + "\n" + this.Taux + "\n";
        }
    }
}
