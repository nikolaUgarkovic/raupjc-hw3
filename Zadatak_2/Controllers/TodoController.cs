using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Zadatak_1;
using Zadatak_2.Data;
using Zadatak_2.Models;

namespace Zadatak_2.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {
        private readonly ITodoRepository _repository;
        private readonly UserManager<ApplicationUser> _user;

        public TodoController(ITodoRepository repository, UserManager<ApplicationUser> user)
        {
            _repository = repository;
            _user = user;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _user.GetUserAsync(HttpContext.User);

            List<TodoViewModel> todoViewModels = new List<TodoViewModel>();

            List<TodoItem> golub = _repository.GetActive(new Guid(user.Id));

            foreach (var item in golub)
            {
                todoViewModels.Add(new TodoViewModel(item));
            }
            IndexViewModel indexTodoModels = new IndexViewModel(todoViewModels);
            //List<TodoViewModel> todoViewModels = Mapper.Map<List<TodoItem>, List<TodoViewModel>>(golub);


            return View(indexTodoModels);
        }

        public async Task<IActionResult> Completed()
        {
            var user = await _user.GetUserAsync(HttpContext.User);
            var golub = _repository.GetCompleted(new Guid(user.Id));
            //List<TodoViewModel> todoViewModels = Mapper.Map<List<TodoItem>, List<TodoViewModel>>(golub);
            List<TodoViewModel> todoViewModels = new List<TodoViewModel>();
            foreach (var item in golub)
            {
                todoViewModels.Add(new TodoViewModel(item));
            }
            CompletedViewModel completedTodoModels = new CompletedViewModel(todoViewModels);
            return View(completedTodoModels);
        }

        public async Task<IActionResult> RemoveFromCompleted(TodoViewModel item)
        {
            var user = await _user.GetUserAsync(HttpContext.User);

            _repository.Remove(item.Id, new Guid(user.Id));
            return RedirectToAction("Completed");
        }

        public async Task<IActionResult> MarkAsCompleted(Guid id)
        {
            var user = await _user.GetUserAsync(HttpContext.User);
            _repository.MarkAsCompleted(id, new Guid(user.Id));
            return RedirectToAction("Index");
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTodoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _user.GetUserAsync(HttpContext.User);
                var todoItem = new TodoItem(model.Text, new Guid(user.Id))
                {
                    DateDue = model.DateDue
                };
                _repository.Add(todoItem);

                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}