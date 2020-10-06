using System;

namespace MyFirstWebApp.Business
{
    public enum Etiquette
    {
        Travail,
        Deco,
        Recette,
        Famille
    };

    public class Todo
    {
        public int Id { get; set; }
        public string description { get; set; }
        public Etiquette etiquette { get; set; }
        public int priority { get; set; }
    }
}