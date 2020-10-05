using System;

namespace MyFirstWebApp.Models
{
    public enum Etiquette
    {
        Travail,
        Deco,
        Recette,
        Famille
    };

    public class TodoModel
    {
        public int Id { get; set; }
        public int Counter { get; set; }
        public string description { get; set; }
        public Etiquette etiquette { get; set; }
        public int priority { get; set; }
    }
}
