using System;
using SQLite;

namespace RecetteMaster.Models
{
    public class Recette
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        
        public string Nom { get; set; }
        
        public DateTime Date { get; set; }
        
        public string Preparation { get; set; }
        [Indexed]
        public int AlimentsId { get; set; }
    }

    public class AlimentPossible
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public bool Important { get; set; }
        public string Nom { get; set; }
    }

    public class Aliments
    {
        [Indexed]
        public int RecetteId{ get; set; }
        [Indexed]
        public int AlimentId { get; set; }
        public string Quantite { get; set; }
    }
}