using Microsoft.AspNetCore.Mvc;
using PaginationLearningFietkau.Models;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using System.Drawing.Printing;
using PaginationLearningFietkau.Models.ViewModels;

namespace PaginationLearningFietkau.Controllers
{
    public class HomeController : Controller
    {
        private ItemCtx _context;
        private readonly IItemRepository _repo;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ItemCtx tempItemCtx, IItemRepository  repo, ILogger<HomeController> logger)
        {
            _context = tempItemCtx;
            _repo = repo;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Produce(int pageNum)
        {
            int pageSize = 8;
            var blah = new ProduceListViewModel
            {
                Perishables = _repo.Perishables
                    .OrderBy(x => x.Name)
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize)
                    .ToList(),
                PaginationInfo = new PaginationInfo
                {
                    CurrentPage = pageNum,
                    ItemsPerPage = pageSize,
                    TotalItems = _repo.Perishables.Count()
                }
            };

            
            return View(blah);
        }

        [HttpGet]
        public IActionResult AddProduceItem()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProduceItem(Perishable item)
        {
            _repo.Add(item);
            _repo.SaveChanges();
            return RedirectToAction("Produce");
        }

        public IActionResult ManageProduceItems(int id)
        {
            var perishable = _repo.Perishables.FirstOrDefault(x => x.PerishableID == id);
            return View(perishable);
        }

        [HttpPost]
        public IActionResult EditProduceItem(Perishable item)
        {
            
            _logger.LogInformation($"Item {item.Name} was found with an id of {item.PerishableID}");
            _repo.Update(item);
            _repo.SaveChanges();
            return RedirectToAction("Produce");
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var perishable = _repo.Perishables.FirstOrDefault(x => x.PerishableID == id);
            return View("Delete",perishable);
        }

        [HttpPost]
        public IActionResult Delete(Perishable item)
        {
            if (item == null)
            {
                _logger.LogInformation("Null at HTTPPOST Delete");
                return View("Error");
            }

            else
            {
                _repo.Remove(item);
                _repo.SaveChanges();
            }
            return RedirectToAction("Produce");

        }

        public IActionResult Analytics()
        {
            var perishables = _repo.Perishables
                .OrderBy(x => x.Price)
                .ToList();
            double totalPrice = 0;
            foreach(var item in perishables)
            {
                totalPrice += item.Price;
            }
            var averagePrice = totalPrice / perishables.Count;
            ViewBag.AveragePrice = averagePrice;
            return View(perishables);
        }
    }
}
