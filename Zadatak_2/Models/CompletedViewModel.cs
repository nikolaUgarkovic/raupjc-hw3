using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zadatak_1;

namespace Zadatak_2.Models
{
    public class CompletedViewModel
    {
        public List<TodoViewModel> CompletedTodoModels { get; set; }

        public CompletedViewModel(List<TodoViewModel> completedTodoItemViews)
        {
            CompletedTodoModels = completedTodoItemViews;
        }

    }
}
