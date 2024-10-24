using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VendorPortal.Models;

namespace VendorPortal.Controllers
{
    public class MaterialRejectionController : Controller
    {
        List<MaterialRejectionViewModel> objMaterialRejectionViewModelist = null;
        VendorPortalEntities db = new VendorPortalEntities();
        BasicPaging objBasicPaging = null;
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult GetMaterialRejectedData(int CurrentPage = 1, string fromDate = "", string toDate = "", string SearchBy = "", string SearchValue = "")
        {
            string SupplierCode = User.Identity.Name;

            if (SearchBy == "" || SearchValue == "")
            {
                SearchValue = "";
                SearchBy = "";
            }
            string Supp_code = User.Identity.Name; 

            objMaterialRejectionViewModelist = db.Database.SqlQuery<MaterialRejectionViewModel>("exec Usp_GetMaterialRejectedData @CurrentPage={0},@Supp_code={1},@FromDate={2}, @ToDate={3},@SearchBy={4},@SearchValue={5}", CurrentPage,Supp_code,fromDate,toDate,SearchBy,SearchValue).ToList();

            var totalCount = db.Database.SqlQuery<int>("exec Usp_GetMaterialRejectedData_TotalCount @Supp_code={0},@FromDate={1}, @ToDate={2},@SearchBy={3},@SearchValue={4}", SupplierCode, fromDate, toDate, SearchBy, SearchValue).ToList();

            DateTime dtFrmDt = new DateTime();
            DateTime dtToDt = new DateTime();
            if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                dtFrmDt = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);
                dtToDt = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
            }

            

            objBasicPaging = new BasicPaging()
            {
                TotalItem = totalCount.FirstOrDefault(),
                RowParPage = Convert.ToInt32(CommanResource.RecordsPerPage),
                CurrentPage = CurrentPage
            };
            if (objBasicPaging.TotalItem % objBasicPaging.RowParPage == 0)
            {
                objBasicPaging.TotalPage = objBasicPaging.TotalItem / objBasicPaging.RowParPage;
            }
            else
                objBasicPaging.TotalPage = objBasicPaging.TotalItem / objBasicPaging.RowParPage + 1;
            ViewBag.paging = objBasicPaging;
            return PartialView("_MaterialRejection", objMaterialRejectionViewModelist);
        }
    }
}