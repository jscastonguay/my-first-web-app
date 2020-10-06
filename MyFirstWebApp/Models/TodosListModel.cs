using System;
using System.Collections.Generic;
using MyFirstWebApp.Business;

namespace MyFirstWebApp.Models
{
    public class TodosListModel
    {
        public Counter counter { get; set;}
        public List<Business.Todo> todosList { get; set; }
    }
}
