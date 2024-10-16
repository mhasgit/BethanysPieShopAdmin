﻿using BethanysPieShopAdmin.Models;
using BethanysPieShopAdmin.Models.Repositories;
using BethanysPieShopAdmin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BethanysPieShopAdmin.Controllers
{
    public class PieController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;

        public PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository)
        {
            _pieRepository = pieRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var pies = await _pieRepository.GetAllPiesAsync();
            return View(pies);
        }

        public async Task<IActionResult> Details(int id)
        {
            var pie = await _pieRepository.GetPieByIdAsync(id);
            return View(pie);
        }

        public async Task<IActionResult> Add()
        {
            var allCategories = await _categoryRepository.GetAllCategoriesAsync();
            IEnumerable<SelectListItem> selectListItems = new SelectList(allCategories, "CategoryId", "Name", null);

            PieAddViewModel pieAddViewModel = new() { Categories = selectListItems };
            return View(pieAddViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(PieAddViewModel pieAddViewModel)
        {

            Pie pie = new Pie
            {
                CategoryId = pieAddViewModel.Pie.CategoryId,
                Name = pieAddViewModel.Pie.Name,
                ShortDescription = pieAddViewModel.Pie.ShortDescription,
                LongDescription = pieAddViewModel.Pie.LongDescription,
                AllergyInformation = pieAddViewModel.Pie.AllergyInformation,
                ImageThumbnailUrl = pieAddViewModel.Pie.ImageThumbnailUrl,
                ImageUrl = pieAddViewModel.Pie.ImageUrl,
                Price = pieAddViewModel.Pie.Price,
                InStock = pieAddViewModel.Pie.InStock,
                IsPieOfTheWeek = pieAddViewModel.Pie.IsPieOfTheWeek
            };

            await _pieRepository.AddPieAsync(pie);
            return RedirectToAction(nameof(Index));
        }
    }
}
