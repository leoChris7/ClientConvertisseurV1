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

        public override bool Equals(object? obj)
        {
            Devise nouvelleDevise = ((Devise)obj);
            return this.Taux == nouvelleDevise.Taux && this.NomDevise == nouvelleDevise.NomDevise;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string? ToString()
        {
            return this.Id + "\n" + this.NomDevise + "\n" + this.Taux + "\n";
        }
    }
}
