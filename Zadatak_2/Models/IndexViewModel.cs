using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zadatak_1;
using Zadatak_2.Models;


namespace Zadatak_2.Models
{
    public class IndexViewModel
    {
        public List<TodoViewModel> IndexTodoModels { get; set; }

        public IndexViewModel(List<TodoViewModel> todoViewModels)
        {
            IndexTodoModels = todoViewModels;

        }
    }
}
