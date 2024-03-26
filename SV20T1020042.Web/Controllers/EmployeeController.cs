using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SV20T1020042.BusinessLayers;
using SV20T1020042.DomainModels;
using SV20T1020042.Web.AppCodes;
using SV20T1020042.Web.Models;
using System.Reflection;

namespace SV20T1020042.Web.Controllers
{
    [Authorize(Roles = $"{WebUserRoles.Administrator}")]
    public class EmployeeController : Controller
    {
        const int PAGE_SIZE = 20;
        const string CREATE_TITLE = "Bổ sung nhân viên";
        const string EMPLOYEE_SEARCH = "employee_search";
        public IActionResult Index()
        {
            //Kiểm tra xem trong session có lưu điều kiện tìm kiếm không.
            PaginationSearchInput? input = ApplicationContext.GetSessionData<PaginationSearchInput>(EMPLOYEE_SEARCH);
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
            var data = CommonDataService.ListOfEmployees(out rowCount, input.Page, input.PageSize, input.SearchValue ?? "");
            var model = new EmployeeSearchResult()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue ?? "",
                RowCount = rowCount,
                Data = data
            };
            //Lưu lại điều kiện tìm kiếm
            ApplicationContext.SetSessionData(EMPLOYEE_SEARCH, input);


            return View(model);
        }
        public IActionResult Create()
        {
            ViewBag.Title = "Bổ sung nhân viên";
            var model = new Employee()
            {
                EmployeeID = 0,
                Photo = "nophoto.png",
                BirthDate = new DateTime(1990, 1, 1)
            };
            return View("Edit", model);
        }
        public IActionResult Edit(int id)
        {
            ViewBag.Title = "Cập nhật thông tin nhân viên";
            var model = CommonDataService.GetEmployee(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            if (string.IsNullOrWhiteSpace(model.Photo))
                model.Photo = "nophoto.png";
            return View(model);
        }

        [HttpPost]
        public IActionResult Save(Employee model, string birthDateInput = "", IFormFile? uploadPhoto = null)
        {
            DateTime? d = birthDateInput.ToDateTime();
            if (d.HasValue)
            {
                model.BirthDate = d.Value;

                //Xử lý ảnh upload: Nếu có ảnh được upload thì lưu ảnh lên server, gán tên file ảnh đã lưu vào cho model.Photo


            }
            if (uploadPhoto != null)
            {
                string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";// Tên file lưu trên server
                                                                                 //Đường dẫn vật lý đến file sẽ lưu lên server
                string filePath = Path.Combine(ApplicationContext.HostEnviroment.WebRootPath, @"images\employees", fileName);

                //Lưu file lên server
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    uploadPhoto.CopyTo(stream);
                }
                //Gán tên file ảnh cho model.Photo
                model.Photo = fileName;
            }
            //Yêu cầu : Tên khách hàng, tên giao dịch, Email và tỉnh thành không được để trống
            if (string.IsNullOrWhiteSpace(model.FullName))
                ModelState.AddModelError("FullName", "Tên không được để trống");  
            if (string.IsNullOrWhiteSpace(model.Email))
                ModelState.AddModelError("Email", "Email không được để trống");
            if (string.IsNullOrWhiteSpace(model.Password))
                ModelState.AddModelError("Password", "Password không được để trống");
            if (string.IsNullOrWhiteSpace(model.RoleNames))
                ModelState.AddModelError("RoleNames", "Chức vụ không được để trống");
            if (!ModelState.IsValid)
            {
                ViewBag.Title = model.EmployeeID == 0 ? CREATE_TITLE : "Cập nhật thông tin nhân viên";
                return View("Edit", model);
            }
            if (model.EmployeeID == 0)
            {
                int id = CommonDataService.AddEmployee(model);
                if (id <= 0)
                {
                    ModelState.AddModelError("Email", "Email bị trùng");
                    ViewBag.Title = CREATE_TITLE;
                    return View("Edit", model);
                }
            }
            else
            {
                bool result = CommonDataService.UpdateEmployee(model);
                if (!result)
                {
                    ModelState.AddModelError("Error", "Không cập nhập được nhân viên.Có thể email bị trùng ");
                    ViewBag.Title = "Cập nhập thông tin nhân viên";
                    return View("Edit", model);
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            if (Request.Method == "POST")
            {
                bool result = CommonDataService.DeleteEmployee(id);
                return RedirectToAction("Index");
            }
            var model = CommonDataService.GetEmployee(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            if (string.IsNullOrWhiteSpace(model.Photo))
                model.Photo = "nophoto.png";
            return View(model);
        }

    }
}