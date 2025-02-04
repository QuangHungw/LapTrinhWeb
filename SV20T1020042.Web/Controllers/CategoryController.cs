﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SV20T1020042.BusinessLayers;
using SV20T1020042.DomainModels;
using SV20T1020042.Web.AppCodes;
using SV20T1020042.Web.Models;
using System.Reflection;

namespace SV20T1020042.Web.Controllers
{
    [Authorize(Roles = $"{WebUserRoles.Administrator},{WebUserRoles.Employee}")]
    public class CategoryController : Controller
    {
        const int PAGE_SIZE = 20;
        const string CREATE_TITLE = "Bổ sung loại hàng";
        const string CATEGORY_SEARCH = "category_search";
        public IActionResult Index()
        {
            //Kiểm tra xem trong session có lưu điều kiện tìm kiếm không.
            Models.PaginationSearchInput? input = ApplicationContext.GetSessionData<PaginationSearchInput>(CATEGORY_SEARCH);
            if (input == null)
            {
                input = new PaginationSearchInput()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    SearchValue = ""
                };
            }
            return View(input);
        }
        public IActionResult Search(PaginationSearchInput input)
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfCategories(out rowCount, input.Page, input.PageSize, input.SearchValue ?? "");
            var model = new CategorySearchResult()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue ?? "",
                RowCount = rowCount,
                Data = data
            };
            //Lưu lại điều kiện tìm kiếm
            ApplicationContext.SetSessionData(CATEGORY_SEARCH, input);


            return View(model);
        }
        public IActionResult Create()
        {
            ViewBag.Title = "Bổ sung loại hàng";
            var model = new Category()
            {
                CategoryID = 0
            };

            return View("Edit", model);
        }


        public IActionResult Edit(int id = 0)
        {
            ViewBag.Title = "Cập nhật thông tin loại hàng";

            var model = CommonDataService.GetCategory(id);
            if (model == null)
                return RedirectToAction("Index");

            return View(model);
        }

        [HttpPost] //Attribute
        public IActionResult Save(Category model)
        {
            if (string.IsNullOrWhiteSpace(model.CategoryName))
                ModelState.AddModelError("CategoryName","Tên loại hàng không được để trống");
            if (string.IsNullOrWhiteSpace(model.Description))
                ModelState.AddModelError("Description", "Mô tả không được để trống");
            if (!ModelState.IsValid)
            {
                ViewBag.Title = model.CategoryID == 0 ? CREATE_TITLE : "Cập nhật thông tin loại hàng";
                return View("Edit", model);
            }
            if (model.CategoryID == 0)
            {
                int id = CommonDataService.AddCategory(model);
                if (id <= 0)
                {
                    ModelState.AddModelError("Description", "Description bị trùng");
                    ViewBag.Title = CREATE_TITLE;
                    return View("Edit", model);
                }
            }
            else
            {
                bool result = CommonDataService.UpdateCategory(model);
                if (!result)
                {
                    ModelState.AddModelError("Error", "Không cập nhập được loại hàng.Có thể mô tả bị trùng ");
                    ViewBag.Title = "Cập nhập thông tin nhà cung cấp";
                    return View("Edit", model);
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id = 0)
        {
            if (Request.Method == "POST")
            {
                bool result = CommonDataService.DeleteCategory(id);
                return RedirectToAction("Index");
            }

            var model = CommonDataService.GetCategory(id);
            if (model == null)
                return RedirectToAction("Index");


            return View(model);
        }
    }
}
