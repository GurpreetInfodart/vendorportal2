using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VendorPortal.Models;

namespace VendorPortal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Unit()
        {
            return View();
        }
        public ActionResult City()
        {
            return View();
        }
        public ActionResult Material()
        {
            return View();
        }
        VendorPortalEntities db = new VendorPortalEntities();
        //https://www.c-sharpcorner.com/article/asp-net-mvc5-full-calendar-jquery-plugin/
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetCalendarData()
        {
            List<PublicHoliday> list = db.Database.SqlQuery<PublicHoliday>("exec GetHoliday").ToList();
            var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        #region Unit definition  
        /// < Create crude Unit Opration>  
        public JsonResult Get_AllUnit()
        {
            using (VendorPortalEntities Obj = new VendorPortalEntities())
            {
                List<tblUnit> Unit = Obj.tblUnits.ToList();
                return Json(Unit, JsonRequestBehavior.AllowGet);
            }
        }       
        public JsonResult Get_UnitById(string Id)
        {
            using (VendorPortalEntities Obj = new VendorPortalEntities())
            {
                int UnitID = int.Parse(Id);
                return Json(Obj.tblUnits.Find(UnitID), JsonRequestBehavior.AllowGet);

            }
        }             
        public string Insert_Unit(tblUnit  Unit)
        {


            if (Unit.UnitCode  != null && Unit.UnitDescription != null )
            {
                using (VendorPortalEntities Obj = new VendorPortalEntities())
                {
                    int DC = Obj.tblUnits.Where(x => x.UnitCode == Unit.UnitCode).Count();
                    if (DC > 0)
                    {
                        return "Enter unit already exits!!!";
                    }
                    else
                    { 
                    Obj.tblUnits.Add(Unit);
                    Unit.CreatedBy = 1;
                    Unit.CreatedOn = DateTime.Now ;
                    Obj.SaveChanges();
                    return "Unit Added Successfully";
                    }
                }
            }
            else
            {
                return "Unit Not Inserted! Try Again";

            }
        }       
        public string Delete_Unit(tblUnit  Unit)
        {
            if (Unit != null)
            {
                using (VendorPortalEntities Obj = new VendorPortalEntities())
                {
                    var Unit_ = Obj.Entry(Unit);
                    if (Unit_.State == System.Data.Entity.EntityState.Detached)
                    {
                        Obj.tblUnits.Attach(Unit);
                        Obj.tblUnits.Remove(Unit);
                    }
                    Obj.SaveChanges();
                    return "Unit Deleted Successfully";
                }
            }
            else
            {
                return "Unit Not Deleted! Try Again";
            }
        }       
        public string Update_Unit(tblUnit Unit)
        {
            if (Unit != null)
            {
                using (VendorPortalEntities Obj = new VendorPortalEntities())
                {
                    var Unit_ = Obj.Entry(Unit);
                    tblUnit Units = Obj.tblUnits.Where(x => x.UnitId  == Unit.UnitId).FirstOrDefault();

                    Units.UnitCode  = Unit.UnitCode;
                    Units.UnitDescription  = Unit.UnitDescription;
                    Units.Active = Unit.Active;
                    // Obj.Entry.State = EntityState.Modified;
                    
                    Obj.SaveChanges();
                    return "Unit Updated Successfully";
                }
            }
            else
            {
                return "Unit Not Updated! Try Again";
            }
        }
        #endregion
        #region City definition  
        /// < Create crude Unit Opration>  
        public JsonResult Get_AllCity()
        {
            using (VendorPortalEntities Obj = new VendorPortalEntities())
            {
                List<tblCity> City = Obj.tblCities.ToList();
                return Json(City, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Get_CityById(string Id)
        {
            using (VendorPortalEntities Obj = new VendorPortalEntities())
            {
                int CityID = int.Parse(Id);
                return Json(Obj.tblCities.Find(CityID), JsonRequestBehavior.AllowGet);

            }
        }
        public string Insert_City(tblCity City)
        {


            if (City.CityName != null && City.StateName != null)
            {
                using (VendorPortalEntities Obj = new VendorPortalEntities())
                {
                    int DC = Obj.tblCities.Where(x => x.CityName == City.CityName).Count();
                    if (DC > 0)
                    {
                        return "Enter City already exits!!!";
                    }
                    else
                    {
                        Obj.tblCities.Add(City);
                        City.CreatedBy = 1;
                        City.CreatedOn = DateTime.Now;
                        Obj.SaveChanges();
                        return "City Added Successfully";
                    }
                }
            }
            else
            {
                return "City Not Inserted! Try Again";

            }
        }
        public string Delete_City(tblCity City)
        {
            if (City != null)
            {
                using (VendorPortalEntities Obj = new VendorPortalEntities())
                {
                    var City_ = Obj.Entry(City);
                    if (City_.State == System.Data.Entity.EntityState.Detached)
                    {
                        Obj.tblCities.Attach(City);
                        Obj.tblCities.Remove(City);
                    }
                    Obj.SaveChanges();
                    return "City Deleted Successfully";
                }
            }
            else
            {
                return "City Not Deleted! Try Again";
            }
        }
        public string Update_City(tblCity City)
        {
            if (City != null)
            {
                using (VendorPortalEntities Obj = new VendorPortalEntities())
                {
                    var City_ = Obj.Entry(City);
                    tblCity Cities = Obj.tblCities.Where(x => x.CityId == City.CityId).FirstOrDefault();

                    Cities.CityName = City.CityName;
                    Cities.StateName = City.StateName;
                    Cities.Active = City.Active;
                    Obj.SaveChanges();
                    return "Unit Updated Successfully";
                }
            }
            else
            {
                return "Unit Not Updated! Try Again";
            }
        }
        #endregion
        #region Material Defination
        /// < Create crude Unit Opration>  
        public JsonResult Get_AllMaterial()
        {
            using (VendorPortalEntities Obj = new VendorPortalEntities())
            {
                List<tblMaterial> Material = Obj.tblMaterials.ToList();
                return Json(Material, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Get_MaterialById(string Id)
        {
            using (VendorPortalEntities Obj = new VendorPortalEntities())
            {
                int MaterialID = int.Parse(Id);
                return Json(Obj.tblMaterials.Find(MaterialID), JsonRequestBehavior.AllowGet);

            }
        }
        public string Insert_Material(tblMaterial Material)
        {


            if (Material.MaterialCode != null && Material.MaterialDescription != null)
            {
                using (VendorPortalEntities Obj = new VendorPortalEntities())
                {
                    int DC = Obj.tblMaterials.Where(x => x.MaterialCode == Material.MaterialCode).Count();
                    if (DC > 0)
                    {
                        return "Enter Material already exits!!!";
                    }
                    else
                    {
                        Obj.tblMaterials.Add(Material);
                        Material.CreatedBy = 1;
                        Material.CreatedOn = DateTime.Now;
                        Obj.SaveChanges();
                        return "Material Added Successfully";
                    }
                }
            }
            else
            {
                return "Material Not Inserted! Try Again";

            }
        }
        public string Delete_Material(tblMaterial Material)
        {
            if (Material != null)
            {
                using (VendorPortalEntities Obj = new VendorPortalEntities())
                {
                    var Material_ = Obj.Entry(Material);
                    if (Material_.State == System.Data.Entity.EntityState.Detached)
                    {
                        Obj.tblMaterials.Attach(Material);
                        Obj.tblMaterials.Remove(Material);
                    }
                    Obj.SaveChanges();
                    return "Material Deleted Successfully";
                }
            }
            else
            {
                return "Material Not Deleted! Try Again";
            }
        }
        public string Update_Material(tblMaterial Material)
        {
            if (Material != null)
            {
                using (VendorPortalEntities Obj = new VendorPortalEntities())
                {
                    var Material_ = Obj.Entry(Material);
                    tblMaterial Materials = Obj.tblMaterials.Where(x => x.MaterialID  == Material.MaterialID).FirstOrDefault();

                    Materials.MaterialCode  = Material.MaterialCode;
                    Materials.MaterialDescription = Material.MaterialDescription;
                    Materials.Unit = Material.Unit;
                    Materials.MaterialGroup = Material.MaterialGroup;
                    Materials.SNP = Material.SNP;
                    Materials.Active = Material.Active;
                    Obj.SaveChanges();
                    return "Unit Updated Successfully";
                }
            }
            else
            {
                return "Unit Not Updated! Try Again";
            }
        }

        #endregion
    }
}