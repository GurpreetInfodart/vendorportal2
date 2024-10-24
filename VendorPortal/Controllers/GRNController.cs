using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VendorPortal.Models;

namespace VendorPortal.Controllers
{
    public class GRNController : Controller
    {
        List<GRNViewModel> objGRNList = null;
        VendorPortalEntities db = new VendorPortalEntities();
        BasicPaging objBasicPaging = null;
        // GET: GRN
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult GRNDetail(int CurrentPage = 1, string fromDate = "", string toDate = "", string SearchBy = "", string SearchValue = "")
        {
            string Supp_code = User.Identity.Name;
            objGRNList = db.Database.SqlQuery<GRNViewModel>("exec Usp_GetGRNData @CurrentPage={0},@Supp_code={1},@FromDate={2}, @ToDate={3},@SearchBy={4},@SearchValue={5}", CurrentPage, Supp_code, fromDate, toDate,SearchBy,SearchValue).ToList();

            var totalCount = db.Database.SqlQuery<int>("exec Usp_GetGRNData_TotalCount @Supp_code={0},@FromDate={1}, @ToDate={2},@SearchBy={3},@SearchValue={4}", Supp_code, fromDate, toDate, SearchBy, SearchValue).ToList();

            if (SearchBy == "" || SearchValue == "")
            {
                SearchValue = "";
                SearchBy = "";
            }

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
            return PartialView("_GRN", objGRNList);
        }
    }
}