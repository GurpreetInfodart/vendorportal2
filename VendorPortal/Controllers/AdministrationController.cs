using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VendorPortal.Models;
using PagedList;

#region Includes

using System.Net;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Globalization;
using System.Threading.Tasks;
using System.Data.Entity;
using VendorPortal.Common;

#endregion Includes
namespace VendorPortal.Controllers
{
    public class AdministrationController : Controller
    {
        VendorPortalEntities db = new VendorPortalEntities();
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        BasicPaging objBasicPaging = null;

        // GET: Administration
        [Authorize(Roles = "Admin,Buyer,Administrator")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateUser()
        {
            List<SelectListItem> roles = (from x in db.AspNetRoles
                                          select new SelectListItem
                                          {
                                              Text = x.Name,
                                              Value = x.Name
                                          }).ToList();
            roles.Insert(0, new SelectListItem { Text = "--Select User Type--", Value = "" });

            List<SelectListItem> supps = (from x in db.SUPPLIER_MASTER
                                          select new SelectListItem
                                          {
                                              Text = x.SUPP_CODE + "-" + x.SUPP_NAME,
                                              Value = x.SUPP_CODE
                                          }).ToList();
            supps.Insert(0, new SelectListItem { Text = "--Select Supplier--", Value = "" });

            List<SelectListItem> states = (from x in db.STATE_MST
                                           select new SelectListItem
                                           {
                                               Text = x.STATE_CODE,
                                               Value = x.StateId.ToString()
                                           }).ToList();
            states.Insert(0, new SelectListItem { Text = "--Select State--", Value = "0" });
            List<SelectListItem> cities = (from x in db.tblCities
                                           select new SelectListItem
                                           {
                                               Text = x.CityName,
                                               Value = x.CityName
                                           }).ToList();
            cities.Insert(0, new SelectListItem { Text = "--Select City--", Value = "" });
            ViewData["UserType"] = roles;
            ViewData["SuppCode"] = supps;
            ViewData["State"] = states;
            ViewData["City"] = cities;

            return View();
        }
        public JsonResult BindCity(string StateId)
        {
            Int64 _stateId = Convert.ToInt64(StateId);
            var cityList = (from x in db.tblCities
                            where x.StateId == _stateId
                            select new
                            {
                                x.CityName,
                                x.CityId
                            }).ToList();
            return Json(cityList, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(UserViewModel viewModel)
        {
            List<SelectListItem> roles = (from x in db.AspNetRoles
                                          select new SelectListItem
                                          {
                                              Text = x.Name,
                                              Value = x.Name
                                          }).ToList();
            roles.Insert(0, new SelectListItem { Text = "--Select User Type--", Value = "" });

            List<SelectListItem> supps = (from x in db.SUPPLIER_MASTER
                                          select new SelectListItem
                                          {
                                              Text = x.SUPP_CODE + "-" + x.SUPP_NAME,
                                              Value = x.SUPP_CODE
                                          }).ToList();
            supps.Insert(0, new SelectListItem { Text = "--Select Supplier--", Value = "" });
            List<SelectListItem> states = (from x in db.STATE_MST
                                           select new SelectListItem
                                           {
                                               Text = x.STATE_CODE,
                                               Value = x.StateId.ToString()
                                           }).ToList();
            states.Insert(0, new SelectListItem { Text = "--Select State--", Value = "0" });
            List<SelectListItem> cities = (from x in db.tblCities
                                           select new SelectListItem
                                           {
                                               Text = x.CityName,
                                               Value = x.CityName
                                           }).ToList();
            cities.Insert(0, new SelectListItem { Text = "--Select City--", Value = "" });
            ViewData["UserType"] = roles;
            ViewData["SuppCode"] = supps;
            ViewData["State"] = states;
            ViewData["City"] = cities;
            var user = new ApplicationUser { UserName = viewModel.UserName }; 
            user.PhoneNumber = viewModel.MobileNumber;
            user.Email = viewModel.Email;

            var result = await UserManager.CreateAsync(user, viewModel.Password);
            if (result.Succeeded)
            {
                var usr = (from x in db.AspNetUsers
                           where x.UserName == viewModel.UserName
                           select x).FirstOrDefault();
                usr.Address = viewModel.Address;
                usr.City = viewModel.City;
                usr.ContactNumber = viewModel.ContactNumber;
                usr.Pin = viewModel.Pin;
                usr.State = viewModel.Pin;
                usr.UserType = viewModel.UserType;

                db.Entry(usr).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                UserManager.AddToRole(user.Id, viewModel.UserType);
                return RedirectToAction("ViewAllUsers");
            }
            else
            {
                string _error = result.Errors.FirstOrDefault();
                Messages msg = new Messages();
                msg.Message_Id = 2;
                msg.Message = _error;
                ViewBag.msg = msg;

                //string ss = resul;
                 
            }
            return View();
        }

        public ActionResult ViewAllUsers(string searchStringUserNameOrEmail, string currentFilter, int? page, int? intPageSize = 25)
        {
            int intPage = 1;
            //intPageSize = 25;
            int intTotalPageCount = 0;

            if (searchStringUserNameOrEmail != null)
            {
                intPage = 1;
            }
            else
            {
                if (currentFilter != null)
                {
                    searchStringUserNameOrEmail = currentFilter;
                    intPage = page ?? 1;
                }
                else
                {
                    searchStringUserNameOrEmail = "";
                    intPage = page ?? 1;
                }
            }

            ViewBag.CurrentFilter = searchStringUserNameOrEmail;

            List<ExpandedUserDTO> col_UserDTO = new List<ExpandedUserDTO>();
            int intSkip = (intPage - 1) * (int)intPageSize;

            intTotalPageCount = (from x in db.AspNetUsers
                                 where x.UserName.Contains(searchStringUserNameOrEmail) || x.Email.Contains(searchStringUserNameOrEmail)
                                 select x)
                      .Count();



            var signups = (from x in db.AspNetUsers
                           where x.UserName.Contains(searchStringUserNameOrEmail) || x.Email.Contains(searchStringUserNameOrEmail)
                           select x).
                       OrderBy(x => x.UserName)
                .Skip(intSkip)
                .Take((int)intPageSize)
                .ToList();

            var signuplist =
    new StaticPagedList<AspNetUser>
    (
        signups, intPage, (int)intPageSize, intTotalPageCount
        );

            return View(signuplist);

        }

        public ActionResult Vendors(int page = 1, int intPageSize = 25)
        {
            var mats = (from x in db.SUPPLIER_MASTER
                        where x.ISACTIVE == "Y" || x.ISACTIVE == null
                        select x).OrderBy(m => m.SUPP_CODE).ToList().Skip((page - 1) * intPageSize).Take(intPageSize);
            var totmats = (from m in db.SUPPLIER_MASTER
                           where m.ISACTIVE == "Y" || m.ISACTIVE == null
                           select m).ToList();
            int intTotalPageCount = totmats.Count();
            var matList =
             new StaticPagedList<SUPPLIER_MASTER>
            (
                mats, page, (int)intPageSize, intTotalPageCount
            );
            return View(matList);
        }
        public ActionResult Materials()
        {
            return View();
        }

        public PartialViewResult MaterialsList(int CurrentPage = 1, string SearchBy = "", string SearchValue = "")
        {
            List<tblMaterial> objBuyerMaterialList = null;

            if (SearchBy == "" || SearchValue == "")
            {
                SearchValue = "";
                SearchBy = "";
            }

            objBuyerMaterialList = db.Database.SqlQuery<tblMaterial>("exec usp_GetMaterialData @CurrentPage={0},@SearchBy={1},@SearchValue={2}", CurrentPage, SearchBy, SearchValue).ToList();

            var totalCount = db.Database.SqlQuery<int>("exec usp_GetMaterialData_TotalCount @SearchBy={0},@SearchValue={1}", SearchBy, SearchValue).ToList();



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
            return PartialView("_MaterialList", objBuyerMaterialList);
        }




        // GET: /Admin/EditRoles/TestUser 
        //[Authorize(Roles = "Administrator")]
        #region ActionResult EditRoles(string UserName)
        public ActionResult EditRoles(string UserName)
        {
            if (UserName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UserName = UserName.ToLower();

            // Check that we have an actual user
            ExpandedUserDTO objExpandedUserDTO = GetUser(UserName);

            if (objExpandedUserDTO == null)
            {
                return HttpNotFound();
            }

            UserAndRolesDTO objUserAndRolesDTO =
                GetUserAndRoles(UserName);

            return View(objUserAndRolesDTO);
        }
        #endregion

        // PUT: /Admin/EditRoles/TestUser 
        //  [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        #region public ActionResult EditRoles(UserAndRolesDTO paramUserAndRolesDTO)
        public ActionResult EditRoles(UserAndRolesDTO paramUserAndRolesDTO)
        {
            try
            {
                if (paramUserAndRolesDTO == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                string UserName = paramUserAndRolesDTO.UserName;
                string strNewRole = Convert.ToString(Request.Form["AddRole"]);

                if (strNewRole != "No Roles Found")
                {
                    // Go get the User
                    ApplicationUser user = UserManager.FindByName(UserName);

                    // Put user in role
                    UserManager.AddToRole(user.Id, strNewRole);
                }

                ViewBag.AddRole = new SelectList(RolesUserIsNotIn(UserName));

                UserAndRolesDTO objUserAndRolesDTO =
                    GetUserAndRoles(UserName);

                return View(objUserAndRolesDTO);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error: " + ex);
                return View("EditRoles");
            }
        }
        #endregion

        // DELETE: /Admin/DeleteRole?UserName="TestUser&RoleName=Administrator
        //[Authorize(Roles = "Administrator")]
        #region public ActionResult DeleteRole(string UserName, string RoleName)
        public ActionResult DeleteRole(string UserName, string RoleName)
        {
            try
            {
                if ((UserName == null) || (RoleName == null))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                UserName = UserName.ToLower();

                // Check that we have an actual user
                ExpandedUserDTO objExpandedUserDTO = GetUser(UserName);

                if (objExpandedUserDTO == null)
                {
                    return HttpNotFound();
                }

                if (UserName.ToLower() ==
                    this.User.Identity.Name.ToLower() && RoleName == "Administrator")
                {
                    ModelState.AddModelError(string.Empty,
                        "Error: Cannot delete Administrator Role for the current user");
                }

                // Go get the User
                ApplicationUser user = UserManager.FindByName(UserName);
                // Remove User from role
                UserManager.RemoveFromRoles(user.Id, RoleName);
                UserManager.Update(user);

                ViewBag.AddRole = new SelectList(RolesUserIsNotIn(UserName));

                return RedirectToAction("EditRoles", new { UserName = UserName });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error: " + ex);

                ViewBag.AddRole = new SelectList(RolesUserIsNotIn(UserName));

                UserAndRolesDTO objUserAndRolesDTO =
                    GetUserAndRoles(UserName);

                return View("EditRoles", objUserAndRolesDTO);
            }
        }
        #endregion

        // Roles *****************************

        // GET: /Admin/ViewAllRoles
        //[Authorize(Roles = "Administrator")]
        #region public ActionResult ViewAllRoles()
        public ActionResult ViewAllRoles()
        {
            var roleManager =
                new RoleManager<IdentityRole>
                (
                    new RoleStore<IdentityRole>(new ApplicationDbContext())
                    );

            List<RoleDTO> colRoleDTO = (from objRole in roleManager.Roles
                                        select new RoleDTO
                                        {
                                            Id = objRole.Id,
                                            RoleName = objRole.Name
                                        }).ToList();

            return View(colRoleDTO);
        }
        #endregion

        // GET: /Admin/AddRole
        // [Authorize(Roles = "Administrator")]
        #region public ActionResult AddRole()
        public ActionResult AddRole()
        {
            RoleDTO objRoleDTO = new RoleDTO();

            return View(objRoleDTO);
        }
        #endregion

        // PUT: /Admin/AddRole
        // [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        #region public ActionResult AddRole(RoleDTO paramRoleDTO)
        public ActionResult AddRole(RoleDTO paramRoleDTO)
        {
            try
            {
                if (paramRoleDTO == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var RoleName = paramRoleDTO.RoleName.Trim();

                if (RoleName == "")
                {
                    throw new Exception("No RoleName");
                }

                // Create Role
                var roleManager =
                    new RoleManager<IdentityRole>(
                        new RoleStore<IdentityRole>(new ApplicationDbContext())
                        );

                if (!roleManager.RoleExists(RoleName))
                {
                    roleManager.Create(new IdentityRole(RoleName));
                }

                return Redirect("~/Administration/ViewAllRoles");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error: " + ex);
                return View("AddRole");
            }
        }
        #endregion

        // DELETE: /Admin/DeleteUserRole?RoleName=TestRole
        // [Authorize(Roles = "Administrator")]
        #region public ActionResult DeleteUserRole(string RoleName)
        public ActionResult DeleteUserRole(string RoleName)
        {
            try
            {
                if (RoleName == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                if (RoleName.ToLower() == "administrator")
                {
                    throw new Exception(String.Format("Cannot delete {0} Role.", RoleName));
                }

                var roleManager =
                    new RoleManager<IdentityRole>(
                        new RoleStore<IdentityRole>(new ApplicationDbContext()));

                var UsersInRole = roleManager.FindByName(RoleName).Users.Count();
                if (UsersInRole > 0)
                {
                    throw new Exception(
                        String.Format(
                            "Canot delete {0} Role because it still has users.",
                            RoleName)
                            );
                }

                var objRoleToDelete = (from objRole in roleManager.Roles
                                       where objRole.Name == RoleName
                                       select objRole).FirstOrDefault();
                if (objRoleToDelete != null)
                {
                    roleManager.Delete(objRoleToDelete);
                }
                else
                {
                    throw new Exception(
                        String.Format(
                            "Canot delete {0} Role does not exist.",
                            RoleName)
                            );
                }

                List<RoleDTO> colRoleDTO = (from objRole in roleManager.Roles
                                            select new RoleDTO
                                            {
                                                Id = objRole.Id,
                                                RoleName = objRole.Name
                                            }).ToList();

                return View("ViewAllRoles", colRoleDTO);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error: " + ex);

                var roleManager =
                    new RoleManager<IdentityRole>(
                        new RoleStore<IdentityRole>(new ApplicationDbContext()));

                List<RoleDTO> colRoleDTO = (from objRole in roleManager.Roles
                                            select new RoleDTO
                                            {
                                                Id = objRole.Id,
                                                RoleName = objRole.Name
                                            }).ToList();

                return View("ViewAllRoles", colRoleDTO);
            }
        }
        #endregion

        #region public ApplicationUserManager UserManager
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ??
                    HttpContext.GetOwinContext()
                    .GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        #endregion

        #region public ApplicationRoleManager RoleManager
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ??
                    HttpContext.GetOwinContext()
                    .GetUserManager<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        #endregion

        #region private List<SelectListItem> GetAllRolesAsSelectList()
        private List<SelectListItem> GetAllRolesAsSelectList()
        {
            List<SelectListItem> SelectRoleListItems =
                new List<SelectListItem>();

            var roleManager =
                new RoleManager<IdentityRole>(
                    new RoleStore<IdentityRole>(new ApplicationDbContext()));

            var colRoleSelectList = roleManager.Roles.OrderBy(x => x.Name).ToList();

            SelectRoleListItems.Add(
                new SelectListItem
                {
                    Text = "Select",
                    Value = "0"
                });

            foreach (var item in colRoleSelectList)
            {
                SelectRoleListItems.Add(
                    new SelectListItem
                    {
                        Text = item.Name.ToString(),
                        Value = item.Name.ToString()
                    });
            }

            return SelectRoleListItems;
        }
        #endregion

        #region private ExpandedUserDTO GetUser(string paramUserName)
        private ExpandedUserDTO GetUser(string paramUserName)
        {
            ExpandedUserDTO objExpandedUserDTO = new ExpandedUserDTO();

            var result = UserManager.FindByName(paramUserName);

            // If we could not find the user, throw an exception
            if (result == null) throw new Exception("Could not find the User");

            objExpandedUserDTO.UserName = result.UserName;
            objExpandedUserDTO.Email = result.Email;
            objExpandedUserDTO.LockoutEndDateUtc = result.LockoutEndDateUtc;
            objExpandedUserDTO.AccessFailedCount = result.AccessFailedCount;
            objExpandedUserDTO.PhoneNumber = result.PhoneNumber;

            return objExpandedUserDTO;
        }
        #endregion

        #region private ExpandedUserDTO UpdateDTOUser(ExpandedUserDTO objExpandedUserDTO)
        private ExpandedUserDTO UpdateDTOUser(ExpandedUserDTO paramExpandedUserDTO)
        {
            ApplicationUser result =
                UserManager.FindByName(paramExpandedUserDTO.UserName);

            // If we could not find the user, throw an exception
            if (result == null)
            {
                throw new Exception("Could not find the User");
            }

            result.Email = paramExpandedUserDTO.Email;

            // Lets check if the account needs to be unlocked
            if (UserManager.IsLockedOut(result.Id))
            {
                // Unlock user
                UserManager.ResetAccessFailedCountAsync(result.Id);
            }

            UserManager.Update(result);

            // Was a password sent across?
            if (!string.IsNullOrEmpty(paramExpandedUserDTO.Password))
            {
                // Remove current password
                var removePassword = UserManager.RemovePassword(result.Id);
                if (removePassword.Succeeded)
                {
                    // Add new password
                    var AddPassword =
                        UserManager.AddPassword(
                            result.Id,
                            paramExpandedUserDTO.Password
                            );

                    if (AddPassword.Errors.Count() > 0)
                    {
                        throw new Exception(AddPassword.Errors.FirstOrDefault());
                    }
                }
            }

            return paramExpandedUserDTO;
        }
        #endregion

        // DELETE: /Admin/DeleteUser
        // [Authorize(Roles = "Administrator")]
        #region public ActionResult DeleteUser(string UserName)
        public ActionResult DeleteUser(string UserName)
        {
            try
            {
                if (UserName == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                if (UserName.ToLower() == this.User.Identity.Name.ToLower())
                {
                    ModelState.AddModelError(
                        string.Empty, "Error: Cannot delete the current user");

                    return View("EditUser");
                }

                ExpandedUserDTO objExpandedUserDTO = GetUser(UserName);

                if (objExpandedUserDTO == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    DeleteUser(objExpandedUserDTO);
                }

                return Redirect("~/Administration/ViewAllUsers");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error: " + ex);
                return View("EditUser", GetUser(UserName));
            }
        }
        #endregion

        #region private void DeleteUser(ExpandedUserDTO paramExpandedUserDTO)
        private void DeleteUser(ExpandedUserDTO paramExpandedUserDTO)
        {
            ApplicationUser user =
                UserManager.FindByName(paramExpandedUserDTO.UserName);

            // If we could not find the user, throw an exception
            if (user == null)
            {
                throw new Exception("Could not find the User");
            }

            UserManager.RemoveFromRoles(user.Id, UserManager.GetRoles(user.Id).ToArray());
            UserManager.Update(user);
            UserManager.Delete(user);

        }
        #endregion

        [Authorize(Roles = "Administrator")]
        #region public ActionResult EditUser(string UserName)
        public ActionResult EditUser(string UserName)
        {
            if (UserName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpandedUserDTO objExpandedUserDTO = GetUser(UserName);
            if (objExpandedUserDTO == null)
            {
                return HttpNotFound();
            }
            return View(objExpandedUserDTO);
        }
        #endregion

        // PUT: /Admin/EditUser
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        #region public ActionResult EditUser(ExpandedUserDTO paramExpandedUserDTO)
        public ActionResult EditUser(ExpandedUserDTO paramExpandedUserDTO)
        {
            try
            {
                if (paramExpandedUserDTO == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                ExpandedUserDTO objExpandedUserDTO = UpdateDTOUser(paramExpandedUserDTO);

                if (objExpandedUserDTO == null)
                {
                    return HttpNotFound();
                }

                return RedirectToAction("ViewAllUsers", "Administration");

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error: " + ex);
                return View("EditUser", GetUser(paramExpandedUserDTO.UserName));
            }
        }
        #endregion

        #region private UserAndRolesDTO GetUserAndRoles(string UserName)
        private UserAndRolesDTO GetUserAndRoles(string UserName)
        {
            // Go get the User
            ApplicationUser user = UserManager.FindByName(UserName);

            List<UserRoleDTO> colUserRoleDTO =
                (from objRole in UserManager.GetRoles(user.Id)
                 select new UserRoleDTO
                 {
                     RoleName = objRole,
                     UserName = UserName
                 }).ToList();

            if (colUserRoleDTO.Count() == 0)
            {
                colUserRoleDTO.Add(new UserRoleDTO { RoleName = "No Roles Found" });
            }

            ViewBag.AddRole = new SelectList(RolesUserIsNotIn(UserName));

            // Create UserRolesAndPermissionsDTO
            UserAndRolesDTO objUserAndRolesDTO =
                new UserAndRolesDTO();
            objUserAndRolesDTO.UserName = UserName;
            objUserAndRolesDTO.colUserRoleDTO = colUserRoleDTO;
            return objUserAndRolesDTO;
        }
        #endregion

        #region private List<string> RolesUserIsNotIn(string UserName)
        private List<string> RolesUserIsNotIn(string UserName)
        {
            // Get roles the user is not in
            var colAllRoles = RoleManager.Roles.Select(x => x.Name).ToList();

            // Go get the roles for an individual
            ApplicationUser user = UserManager.FindByName(UserName);

            // If we could not find the user, throw an exception
            if (user == null)
            {
                throw new Exception("Could not find the User");
            }

            var colRolesForUser = UserManager.GetRoles(user.Id).ToList();
            var colRolesUserInNotIn = (from objRole in colAllRoles
                                       where !colRolesForUser.Contains(objRole)
                                       select objRole).ToList();

            if (colRolesUserInNotIn.Count() == 0)
            {
                colRolesUserInNotIn.Add("No Roles Found");
            }

            return colRolesUserInNotIn;
        }
        #endregion

        public ActionResult AddVENDOR_ASSOC(int? id)
        {


            List<SelectListItem> _supplierList = (from s in db.SUPPLIER_MASTER.AsEnumerable()
                                                  select new SelectListItem
                                                  {
                                                      Text = s.SUPP_CODE,
                                                      Value = s.SUPP_CODE
                                                  }).ToList();
            //_stateList.Insert(0, new SelectListItem { Text = "--Select--", Value = "0" });
            ViewData["supplierList"] = _supplierList;

            if (id != 0 && id != null)
            {
                var _data = db.VENDOR_ASSOC.Where(a => a.ID == id).FirstOrDefault();
                if (_data != null)
                {
                    return View(_data);
                }
                else
                {
                    return View();
                }

            }
            else
            {
                return View();
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddVENDOR_ASSOC(VENDOR_ASSOC objVENDOR_ASSOC)
        {
            List<SelectListItem> _supplierList = (from s in db.SUPPLIER_MASTER.AsEnumerable()
                                                  select new SelectListItem
                                                  {
                                                      Text = s.SUPP_CODE,
                                                      Value = s.SUPP_CODE
                                                  }).ToList();
            //_stateList.Insert(0, new SelectListItem { Text = "--Select--", Value = "0" });
            ViewData["supplierList"] = _supplierList;

            if (objVENDOR_ASSOC.ID != 0)
            {
                var _data = db.VENDOR_ASSOC.Where(a => a.ID == objVENDOR_ASSOC.ID).FirstOrDefault();
                if (_data != null)
                {
                    _data.USER_CODE = objVENDOR_ASSOC.USER_CODE;
                    _data.VENDOR_CODE = objVENDOR_ASSOC.VENDOR_CODE;
                    db.Entry(_data).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            else
            {
                db.VENDOR_ASSOC.Add(objVENDOR_ASSOC);
                db.SaveChanges();
            }
            return View("Vendor_AssocData");
        }
        public ActionResult DeleteVENDOR_ASSOC(int id)
        {
            if (id > 0)
            {
                var del_data = db.VENDOR_ASSOC.Where(x => x.ID == id).FirstOrDefault();
                if (del_data != null)
                {
                    del_data.IsActive = 1;
                    db.Entry(del_data).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return View("Vendor_AssocData");
        }
        public ActionResult Vendor_AssocData()
        {
            return View();
        }

        public PartialViewResult GetVendor_AssocData(int CurrentPage = 1)
        {
            List<GetVendor_AssociationViewModel> objVendorAssoList = null;
            objVendorAssoList = db.Database.SqlQuery<GetVendor_AssociationViewModel>("exec usp_GetVendorAssocaitionData @CurrentPage={0}", CurrentPage).ToList();

            var totalData = (from va in db.VENDOR_ASSOC
                             join sm in db.SUPPLIER_MASTER on va.VENDOR_CODE equals sm.SUPP_CODE where va.IsActive != 1
                             select new { }
              ).ToList();


            objBasicPaging = new BasicPaging()
            {
                TotalItem = totalData.Count(),
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
            return PartialView("_GetVendor_AssociationData", objVendorAssoList);

        }
        [HttpGet]
        public ActionResult AddEditMaterials(int? Id)
        {
            List<SelectListItem> _unitList = (from s in db.tblUnits.AsEnumerable()
                                              select new SelectListItem
                                              {
                                                  Text = s.UnitDescription,
                                                  Value = s.UnitCode
                                              }).ToList();

            ViewData["unitList"] = _unitList;
            if (Id != null && Id != 0)
            {

                var _data = db.tblMaterials.Where(i => i.MaterialID == Id).FirstOrDefault();

                if (_data != null)
                {
                    return View("AddEditMaterials", _data);
                }
                else
                {
                    return View("AddEditMaterials");
                }
            }
            else
            {
                return View("AddEditMaterials");
            }
        }

        [HttpPost]
        public ActionResult AddEditMaterials(tblMaterial objtblMat)
        {

            List<SelectListItem> _unitList = (from s in db.tblUnits.AsEnumerable()
                                              select new SelectListItem
                                              {
                                                  Text = s.UnitDescription,
                                                  Value = s.UnitCode
                                              }).ToList();

            ViewData["unitList"] = _unitList;

            if (objtblMat.MaterialID != 0)
            {
                var _data = db.tblMaterials.Where(a => a.MaterialID == objtblMat.MaterialID).FirstOrDefault();
                if (_data != null)
                {
                    _data.MaterialCode = objtblMat.MaterialCode;
                    _data.MaterialDescription = objtblMat.MaterialDescription;
                    _data.Unit = objtblMat.Unit;
                    _data.MaterialGroup = objtblMat.MaterialGroup;
                    _data.SNP = objtblMat.SNP;
                    _data.UpdatedOn = DateTime.Now;
                    db.Entry(_data).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            else
            {
                objtblMat.CreatedBy = 1;
                objtblMat.CreatedOn = DateTime.Now;
                objtblMat.Active = true;
                db.tblMaterials.Add(objtblMat);
                db.SaveChanges();
            }
            return RedirectToAction("Materials");
        }

        public ActionResult DeleteMaterial(int id)
        {
            if (id > 0)
            {
                var del_data = db.tblMaterials.Where(x => x.MaterialID == id).FirstOrDefault();
                if (del_data != null)
                {
                    del_data.Active = false;
                    del_data.UpdatedOn = DateTime.Now;
                    db.Entry(del_data).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Materials");
        }


        [HttpGet]
        public ActionResult AddEditVendor(int? Id)
        {


            List<SelectListItem> _bankCurrencyList1 = (from s in db.BASIC_MST.AsEnumerable()
                                                       where s.ISACTIVE == "Y" && s.IDENTIFIER == "BANK_CURRENCY1"
                                                       orderby s.DESCRIPTION
                                                       select new SelectListItem
                                                       {
                                                           Text = s.CODE,
                                                           Value = s.DESCRIPTION
                                                       }).ToList();

            ViewBag.Currency1 = _bankCurrencyList1;

            List<SelectListItem> _bankCurrencyList2 = (from s in db.BASIC_MST.AsEnumerable()
                                                       where s.ISACTIVE == "Y" && s.IDENTIFIER == "BANK_CURRENCY2"
                                                       orderby s.DESCRIPTION
                                                       select new SelectListItem
                                                       {
                                                           Text = s.CODE,
                                                           Value = s.DESCRIPTION
                                                       }).ToList();

            ViewBag.Currency2 = _bankCurrencyList2;

            if (Id != null && Id != 0)
            {

                var _data = db.SUPPLIER_MASTER.Where(i => i.Id == Id).FirstOrDefault();

                if (_data != null)
                {
                    return View("AddEditSupplier", _data);
                }
                else
                {
                    return View("AddEditSupplier");
                }
            }
            else
            {
                return View("AddEditSupplier");
            }
        }

        [HttpPost]
        public ActionResult AddEditVendor(SUPPLIER_MASTER objtblSupp)
        {
            if (objtblSupp.Id != 0)
            {
                var _data = db.SUPPLIER_MASTER.Where(a => a.Id == objtblSupp.Id).FirstOrDefault();
                if (_data != null)
                {
                    _data.SUPP_CODE = objtblSupp.SUPP_CODE;
                    _data.SUPP_NAME = objtblSupp.SUPP_NAME;
                    _data.ADDRESS1 = objtblSupp.ADDRESS1;
                    _data.ADDRESS2 = objtblSupp.ADDRESS2;
                    _data.ADDRESS3 = objtblSupp.ADDRESS3;
                    _data.CITY = objtblSupp.CITY;
                    _data.PIN = objtblSupp.PIN;
                    _data.DISTRICT = objtblSupp.DISTRICT;
                    _data.STATE = objtblSupp.STATE;
                    _data.COUNTRY = objtblSupp.COUNTRY;
                    _data.VATREG_NO1 = objtblSupp.VATREG_NO1;
                    _data.VATREG_NO2 = objtblSupp.VATREG_NO2;
                    _data.TIN2_1 = objtblSupp.TIN2_1;
                    _data.TIN2_2 = objtblSupp.TIN2_2;
                    _data.GSTIN = objtblSupp.GSTIN;
                    _data.COMM_TEL1 = objtblSupp.COMM_TEL1;
                    _data.COMM_TEL2 = objtblSupp.COMM_TEL2;
                    _data.COMM_TEL3 = objtblSupp.COMM_TEL3;
                    _data.COMM_FAX = objtblSupp.COMM_FAX;
                    _data.COMM_EMAIL = objtblSupp.COMM_EMAIL;
                    _data.PAYMENT_TERM = objtblSupp.PAYMENT_TERM;
                    _data.PAYMENT_METHOD = objtblSupp.PAYMENT_METHOD;
                    _data.BANK_NAME = objtblSupp.BANK_NAME;
                    _data.BANK_ADDRESS1 = objtblSupp.BANK_ADDRESS1;
                    _data.BANK_ADDRESS2 = objtblSupp.BANK_ADDRESS2;
                    _data.BANK_ACC_NO = objtblSupp.BANK_ACC_NO;
                    _data.BANK_SWIFT_CODE1 = objtblSupp.BANK_SWIFT_CODE1;
                    _data.BANK_SWIFT_CODE2 = objtblSupp.BANK_SWIFT_CODE2;
                    _data.BANK_SWIFT_CODE3 = objtblSupp.BANK_SWIFT_CODE3;
                    _data.BANK_CURRENCY1 = objtblSupp.BANK_CURRENCY1;
                    _data.BANK_CURRENCY2 = objtblSupp.BANK_CURRENCY2;
                    _data.ADDITIONAL_INFO1 = objtblSupp.ADDITIONAL_INFO1;
                    _data.ADDITIONAL_INFO2 = objtblSupp.ADDITIONAL_INFO2;
                    _data.ADDITIONAL_INFO3 = objtblSupp.ADDITIONAL_INFO3;
                    _data.ADDITIONAL_INFO4 = objtblSupp.ADDITIONAL_INFO4;
                    _data.ISACTIVE = "Y";
                    _data.EMAIL_ID = objtblSupp.EMAIL_ID;
                    //_data.CreatedBy = 1;
                    //_data.CreatedOn = DateTime.Now;
                    _data.UpdatedOn = DateTime.Now;

                    db.Entry(_data).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            else
            {
                db.SUPPLIER_MASTER.Add(objtblSupp);
                db.SaveChanges();
            }
            return RedirectToAction("Vendors");
        }
        public ActionResult NotificationsSupp()
        {
            return View();
        }
        public ActionResult DeleteVendor(int id)
        {
            if (id > 0)
            {
                var del_data = db.SUPPLIER_MASTER.Where(x => x.Id == id).FirstOrDefault();
                if (del_data != null)
                {
                    del_data.ISACTIVE = "N";
                    del_data.UpdatedOn = DateTime.Now;
                    db.Entry(del_data).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Vendors");
        }



    }
}
