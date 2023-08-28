using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
         
        public CompanyController(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
          
        }   


        public IActionResult Index()
        {
            // retrieve all data  , convert to that list , list of categories will be retreived using this command 
            // it will go the database run the command and lets select from categories and retrieve that and assigning to the object write here
            
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();


            return View(objCompanyList);
        }

        public IActionResult Upsert(int? id)
        {

           
            if(id==null || id == 0)
            {
                //create
                return View(new Company());


            } else
            {

                //update

                Company companyObj = _unitOfWork.Company.Get(u => u.Id == id);

                return View(companyObj);
            }
        }
        //this action method used for to add Company 
        [HttpPost]
        public IActionResult Upsert(Company CompanyObj)
        {
           
            //server side validation
            if (ModelState.IsValid)
            {
                              
                if (CompanyObj.Id == 0)
                {
                    _unitOfWork.Company.Add(CompanyObj);
                   
                }
                else
                {
                    _unitOfWork.Company.Update(CompanyObj);
                    
                }


               // _unitOfWork.Company.Add(CompanyVM.Company);
                _unitOfWork.Save();

                TempData["success"] = "Company Created Successfully";

                return RedirectToAction("Index");
            }
            else
            {
                return View(CompanyObj);
            }

        }


        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();

        //    }
        //    Company? CompanyFromDb = _unitOfWork.Company.Get(u => u.Id == id);
        //    //Company? CompanyFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);

        //    //Company? CompanyFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();

        //    if (CompanyFromDb == null)
        //    {

        //        return NotFound();
        //    }

        //    return View(CompanyFromDb);
        //}
        ////this action method used for to add Company 
        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeletePost(int? id)
        //{

        //    Company? obj = _unitOfWork.Company.Get(u => u.Id == id);

        //    if (obj == null)
        //    {

        //        return NotFound();

        //    }

        //    //else
        //    _unitOfWork.Company.Remove(obj);
        //    _unitOfWork.Save();
        //    TempData["success"] = "Company Deleted Successfully";
        //    return RedirectToAction("Index");


        //}

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll() {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();

            return Json(new { data = objCompanyList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var CompanyToBeDeleted = _unitOfWork.Company.Get(u => u.Id == id);
            if (CompanyToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
                      _unitOfWork.Company.Remove(CompanyToBeDeleted);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete successful" });
        }


        #endregion

    }
}
