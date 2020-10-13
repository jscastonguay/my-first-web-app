using System;
using System.Collections.Generic;
using Todo.Core.Entities;

namespace Todo.Web.Models
{
    public class TodosListModel
    {
        public Counter counter { get; set;}
        public List<Todo.Core.Entities.Todo> todosList { get; set; }
    }
}
