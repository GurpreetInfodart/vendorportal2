using ExcelDataReader;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Xml;
using System.Xml.Serialization;
using VendorPortal.Common;
using VendorPortal.Models;

namespace VendorPortal.Controllers
{
    [Authorize(Roles = "Supplier,Buyer,Admin,Administrator,BSLUser,Quality,R&D,Visitor")]
    public class InventoryController : Controller
    {
        VendorPortalEntities db = new VendorPortalEntities();
        // GET: Inventory
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult StockManual()
        {
            string Supp_code = User.Identity.Name;
            //ViewBag.UserID = Convert.ToInt32(Session["User_ID"]);
            //var USERID = Convert.ToInt32(Session["User_ID"]);
 

            ViewBag.DDLPO = (from po in db.POes
                             join User in db.AspNetUsers on po.SUPP_CODE equals User.UserName                            
                             where User.UserName == Supp_code
                             select new { po.PO_NUM }).Distinct();

            ViewBag.DDLUOM = (from Unit in db.BASIC_MST
                              where Unit.IDENTIFIER == "UNIT_UOM" && Unit.ISACTIVE == "Y"
                              select new { Unit.CODE }).Distinct();
            ViewBag.DDLWIPUOM = (from Unit in db.BASIC_MST
                                 where Unit.IDENTIFIER == "UNIT_UOM" && Unit.ISACTIVE == "Y"
                                 select new { Unit.CODE }).Distinct();
            ViewBag.DDLFGUOM = (from Unit in db.BASIC_MST
                                where Unit.IDENTIFIER == "UNIT_UOM" && Unit.ISACTIVE == "Y"
                                select new { Unit.CODE }).Distinct();

            return View();
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetUOM()
        {
            var classesList = (from Unit in db.BASIC_MST
                               where Unit.IDENTIFIER == "UNIT_UOM" && Unit.ISACTIVE == "Y"
                               select new { Unit.CODE }).Distinct();
            var classesData = classesList.Select(m => new SelectListItem()
            {
                Text = m.CODE,
                Value = m.CODE.ToString(),
            });
            return Json(classesData, JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetWIPUOM()
        {
            var classesList = (from Unit in db.BASIC_MST
                               where Unit.IDENTIFIER == "UNIT_UOM" && Unit.ISACTIVE == "Y"
                               select new { Unit.CODE }).Distinct();
            var classesData = classesList.Select(m => new SelectListItem()
            {
                Text = m.CODE,
                Value = m.CODE.ToString(),
            });
            return Json(classesData, JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetFGUOM()
        {

            var classesList = (from Unit in db.BASIC_MST
                               where Unit.IDENTIFIER == "UNIT_UOM" && Unit.ISACTIVE == "Y"
                               select new { Unit.CODE }).Distinct();
            var classesData = classesList.Select(m => new SelectListItem()
            {
                Text = m.CODE,
                Value = m.CODE.ToString(),
            });
            return Json(classesData, JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult MaterialManualList(string PONO)
        {
            string Supp_code = User.Identity.Name;
            List<StockDeclaration> list = db.Database.SqlQuery<StockDeclaration>("exec Sp_GetMaterialDataManual @USERID={0},@PONO={1}", Supp_code, PONO).ToList();
            var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
            //System.Data.DataTable dt = ConvertToDataTable(list);
            //Excelread(dt);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GenerateStock(string PONO, string SUPPLIERCODE, string SUPPLIERNAME, string MATERIALCODE, string MATERIALDESCRIPTION, string RAWMATERIAL, string UOM, string WIP, string WIPUOM, string FINISHGOODS, string FINISHGOODSUOM, string REMARK, string PODATE, string CREATEDBY)
        {
            CREATEDBY = User.Identity.Name;
            var list = db.Database.SqlQuery<TableStock>("exec Sp_GenerateStock  @PONO  ={0},@SUPPLIERCODE ={1},@SUPPLIERNAME  ={2},@MATERIALCODE ={3},@MATERIALDESCRIPTION  ={4},@RAWMATERIAL ={5},@UOM={6},@WIP={7},@WIPUOM={8}, @FINISHGOODS ={9},@FINISHGOODSUOM ={10} ,@REMARK ={11},@PODATE={12},@CREATEDBY={13}", PONO, SUPPLIERCODE, SUPPLIERNAME, MATERIALCODE, MATERIALDESCRIPTION, RAWMATERIAL, UOM, WIP, WIPUOM, FINISHGOODS, FINISHGOODSUOM, REMARK, Convert.ToDateTime(PODATE), CREATEDBY).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Stock()
        {
            string Supp_code = User.Identity.Name;
            // BharatSEntities db = new BharatSEntities();
            var USERID = Convert.ToInt32(Session["User_ID"]);
            ViewBag.UserID = Convert.ToInt32(Session["User_ID"]);
            ViewBag.DDLPO = (from po in db.POes
                             join User in db.AspNetUsers on po.SUPP_CODE equals User.UserName                             
                             where User.UserName == Supp_code
                             select new { po.PO_NUM }).Distinct();
            //ViewBag.DDLPO = db.Database.SqlQuery<POLIST>("exec Sp_GetPOListData @USERID={0}", USERID).ToList();

            return View();
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public void MaterialsaveList(string PONO)
        {
            string Supp_code = User.Identity.Name;
            List<StockDeclaration2> list = db.Database.SqlQuery<StockDeclaration2>("exec Sp_GetMaterialData @USERID={0},@PONO={1}", Supp_code, PONO).ToList();

            System.Data.DataTable dt = ConvertToDataTable(list);
            try
            {
                CSVread(dt);
            }
            catch (Exception e)
            {

            }

        }
        public void CSVread(System.Data.DataTable table)
        {
            string Supp_code = User.Identity.Name;
            string csv = string.Empty;
            foreach (DataColumn column in table.Columns)
            {
                //Add the Header row for CSV file.
                csv += column.ColumnName + ',';
            }
            csv = csv.Remove(csv.Length - 1, 1);
            //Add new line.
            csv += "\r\n";
            foreach (DataRow row in table.Rows)
            {
                foreach (DataColumn column in table.Columns)
                {
                    //Add the Data rows.
                    csv += row[column.ColumnName].ToString().Replace(",", ";") + ',';
                }
                csv = csv.Remove(csv.Length - 1, 1);
                //Add new line.
                csv += "\r\n";
            }
            //Download the CSV file.


            string path = System.Web.Configuration.WebConfigurationManager.AppSettings["myFilePath"].ToString();


            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=StockDeclaration.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(csv);

            // // Adding Header Or Column in the First Row of CSV
            string csvPath = path + "StockDeclaration_" + Supp_code + ".csv";
            // Save or upload CSV format File (.csv)
            System.IO.File.WriteAllText(csvPath, csv.ToString());
            //System.IO.File.AppendAllText(csvPath, csv.ToString());


        }
        public System.Data.DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            System.Data.DataTable table = new System.Data.DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult MaterialList(string PONO)
        {
            string Supp_code = User.Identity.Name;
            List<StockDeclaration2> list = db.Database.SqlQuery<StockDeclaration2>("exec Sp_GetMaterialData @USERID={0},@PONO={1}", Supp_code, PONO).ToList();


            //System.Data.DataTable dt = ConvertToDataTable(list);
            

            var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            //CSVread(dt);
            //System.Data.DataTable dt = ConvertToDataTable(list);
            //try
            //{
            //    CSVread(dt);
            //}
            //catch (Exception e)
            //{

            //}

            return jsonResult;

        }
        public ActionResult DownloadFile(string DDLPO)
        {
            string Supp_code = User.Identity.Name; 
            
            string path = System.Web.Configuration.WebConfigurationManager.AppSettings["myFilePath"].ToString();
            byte[] fileBytes = System.IO.File.ReadAllBytes(path + "\\StockDeclaration_" + Supp_code + ".csv");
           // int UID = Convert.ToInt16(Session["User_ID"].ToString().Trim());
            var UserName = (from User in db.AspNetUsers
                            where User.UserName == Supp_code
                            select new { User.UserName }).FirstOrDefault();
            string fileName = "StockDeclaration_" + UserName.UserName + ".csv";
            //string fileName = "StockDeclaration_" + Session["User_ID"].ToString().Trim() + ".csv";                    
            System.IO.File.Delete(path + "\\StockDeclaration_" + Supp_code + ".csv");
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);

        }

        public ActionResult ExportInventory(StockDeclaration2 obj)
        {
             
            string po = obj.PONO;
            string Supp_code = User.Identity.Name;
            //Load Data
            var dataInventory = db.Database.SqlQuery<StockDeclaration2>("exec Sp_GetMaterialData @USERID={0},@PONO={1}", Supp_code, po).ToList();
            string xml = String.Empty;
            XmlDocument xmlDoc = new XmlDocument();

            XmlSerializer xmlSerializer = new XmlSerializer(dataInventory.GetType());

            using (MemoryStream xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, dataInventory);
                xmlStream.Position = 0;
                xmlDoc.Load(xmlStream);
                xml = xmlDoc.InnerXml;
            }

            var fName = string.Format("Inventory-{0}", DateTime.Now.ToString("s"));

            byte[] fileContents = Encoding.UTF8.GetBytes(xml);

            return File(fileContents, "application/vnd.ms-excel", fName);
        }
        [HttpGet]
        public ActionResult StockBulk()
        {
            string Supp_code = User.Identity.Name;
            ViewBag.UserID = Supp_code;
            return View();
        }

        [HttpPost]        
        public ActionResult StockBulk(HttpPostedFileBase upload)
        {
            string Supp_code = User.Identity.Name;
            System.Data.DataTable result1 = new System.Data.DataTable();

            upload.SaveAs(Server.MapPath("..\\ExcelFile\\") + upload.FileName);
            string path = Server.MapPath("..\\ExcelFile\\") + upload.FileName;

            if (ModelState.IsValid)
            {
                ViewBag.UserID = Convert.ToString(Session["User_ID"]); ;

                if (upload != null && upload.ContentLength > 0)
                {

                    Stream stream = upload.InputStream;
                    // string csvData = System.IO.File.ReadAllText(path);



                    //  var lines = System.IO.File.ReadAllLines(stream).Skip(1).Select(a => a.Split('\n'));
                    // We return the interface, so that

                    IExcelDataReader reader = null;


                    //if (upload.FileName.EndsWith(".xls"))
                    //{
                    //    reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    //     //reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    //}
                    //else
                    //if (upload.FileName.EndsWith(".xlsx"))
                    //{
                    //    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    //    //reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    //}
                    //else
                    if (upload.FileName.EndsWith(".csv"))
                    {
                        result1 = ConvertCSVtoDataTable(path);
                        // reader = ExcelReaderFactory.CreateOpenXmlReader(stream);

                    }
                    else
                    {
                        ModelState.AddModelError("File", "This file format is not supported");
                        return View();
                    }
                    //reader.IsFirstRowAsColumnNames = true;
                    // DataSet result1 = reader.AsDataSet();
                    int ColCount = result1.Columns.Count;
                    // int ColCount = reader.FieldCount;

                    if (ColCount != 13)
                    {
                        ModelState.AddModelError("File", "This file format is not supported");
                        return View();
                    }



                    string PoNum = ""; string SupplierCode = ""; string SupplierName = ""; string MaterialCode = "";
                    string MaterialDescription = ""; string RawMaterial = ""; string UOM = ""; string WIP = "";
                    string WIPUOM = ""; string FinishGoods = ""; string FinishGoodsUOM = "";
                    string Remark = ""; string Date = "";

                    int rowcount1 = result1.Rows.Count;
                    if (rowcount1 > 0)
                    {
                        for (int i = 0; i < rowcount1; i++)
                        {


                            PoNum = result1.Rows[i][0].ToString().Trim();
                            SupplierCode = result1.Rows[i][1].ToString().Trim();
                            SupplierName = result1.Rows[i][2].ToString().Trim();
                            MaterialCode = result1.Rows[i][3].ToString().Trim();
                            MaterialDescription = result1.Rows[i][4].ToString().Trim();
                            RawMaterial = result1.Rows[i][5].ToString().Trim();
                            
                            UOM = result1.Rows[i][6].ToString().Trim();
                            WIP = result1.Rows[i][7].ToString().Trim();
                            WIPUOM = result1.Rows[i][8].ToString().Trim();
                            FinishGoods = result1.Rows[i][9].ToString().Trim();
                            FinishGoodsUOM = result1.Rows[i][10].ToString().Trim();
                            Remark = result1.Rows[i][11].ToString().Trim();
                            Date = result1.Rows[i][12].ToString().Trim();


                            if (PoNum == "" || SupplierCode == "" || SupplierName == "" || MaterialCode == "" || MaterialDescription == "")
                            {
                                ModelState.AddModelError("File", "PoNum, SupplierCode, SupplierName, MaterialCode, MaterialDes Should Not Empty, Please check file.");
                                return View();
                            }
                            else
                            {

                                if (RawMaterial == "") { RawMaterial = "0"; }
                                else
                                {
                                    var RawMaterial1 = new Regex(@"\d");
                                    if (!RawMaterial1.IsMatch(RawMaterial))
                                    {
                                        ModelState.AddModelError("File", "RawMaterial should be Numeric.");
                                        return View();
                                    }
                                }
                                if (WIP == "")
                                { WIP = "0"; }
                                else
                                {
                                    var WIP1 = new Regex(@"\d");
                                    if (!WIP1.IsMatch(WIP))
                                    {
                                        ModelState.AddModelError("File", "WIP should be Numeric.");
                                        return View();
                                    }
                                }
                                if (FinishGoods == "") { FinishGoods = "0"; }
                                else
                                {
                                    var FinishGoods1 = new Regex(@"\d");
                                    if (!FinishGoods1.IsMatch(FinishGoods))
                                    {
                                        ModelState.AddModelError("File", "FinishGoods should be Numeric.");
                                        return View();
                                    }
                                }
                            }
                        }
                    }


                    int rowcount = result1.Rows.Count;

                    //if (rowcount > 0)
                    //{
                    //    for (int i = 0; i < rowcount; i++)
                    //    {
                    //        if (RawMaterial == "")
                    //        {
                    //            RawMaterial = "0";
                    //        }

                    //        if (UOM == "")
                    //        {
                    //            UOM = "0";
                    //        }
                    //        if (WIP == "")
                    //        {
                    //            WIP = "0";
                    //        }
                    //        if (WIPUOM == "")
                    //        {
                    //            WIPUOM = "0";
                    //        }
                    //        if (FinishGoods == "")
                    //        {
                    //            FinishGoods = "0";
                    //        }
                    //        if (FinishGoods == "")
                    //        {
                    //            FinishGoods = "0";
                    //        }

                    //        if (PoNum == "" || SupplierCode == "" || SupplierName == "" || MaterialCode == "" || MaterialDescription == "" || RawMaterial == "" || UOM == "" || WIP == "" || WIPUOM == "" || FinishGoods == "" || FinishGoodsUOM == "" || Date == "")
                    //        {
                    //            ModelState.AddModelError("File", "Empty field is not allowed ,Please check file.");
                    //            return View();
                    //        }
                    //    }
                    //}


                }

            }
            return View(result1);
        }
        public static System.Data.DataTable ConvertCSVtoDataTable(string strFilePath)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            using (StreamReader sr = new StreamReader(strFilePath))
            {
                string[] headers = sr.ReadLine().Split(',');
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }

                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');
                    if (rows.Length > 1)
                    {
                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < headers.Length; i++)
                        {
                            dr[i] = rows[i].Trim();
                        }
                        dt.Rows.Add(dr);
                    }
                }

            }


            return dt;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GenerateStockdeclarationUpload(string PoNum, string SupplierCode, string SupplierName, string MaterialCode, string MaterialDescription, string RawMaterial, string UOM, string WIP, string WIPUOM, string FinishGoods, string FinishGoodsUOM, string Remark, string PoDate, string EMPID)
        {
            string Supp_code = User.Identity.Name;

            //string PDate = Convert.ToDateTime(PoDate).ToString("dd/MM/yyyy");
            DateTime PDate = Convert.ToDateTime(PoDate);
            DateTime _CreatedOn = DateTime.Now;            
            var list = db.Database.SqlQuery<StockDeclaration>("exec SPStockUpload  @PONO  ={0},@SUPPLIERCODE ={1},@SUPPLIERNAME ={2},@MATERIALCODE  ={3},@MATERIALDESCRIPTION ={4},@RAWMATERIAL={5},@UOM={6}, @WIP ={7},@WIPUOM ={8},@FINISHGOODS ={9},@FINISHGOODSUOM ={10},@REMARK ={11},@PODATE ={12},@CREATEDBY ={13}", PoNum, SupplierCode, SupplierName, MaterialCode, MaterialDescription, RawMaterial, UOM, WIP, WIPUOM, FinishGoods, FinishGoodsUOM, Remark, PDate, Supp_code).ToList();
            return Json("SUCCESS", JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult DuplicatePo(string PONO)
        {
             
            var result = db.Database.SqlQuery<string>("exec SP_PODUPLICATE @PONO={0}", PONO).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);

        }
      //  [Authorize(Roles = "Supplier,Buyer")]
        public ActionResult StockDeclarationRpt()
        {
            //var user = new User_Master();
            //user.USERNAME = Convert.ToString(Session["User_ID"]);
            ViewBag.UserID =  User.Identity.Name;
            return View();
        }

      //  [Authorize(Roles = "Supplier,Buyer")]
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult StockListRptList()
        {
            string UserID = User.Identity.Name;
            var jsonResult = Json(db.Database.SqlQuery<StockDeclaration>("exec Sp_StockRpt @USERID={0}", UserID).ToList(), JsonRequestBehavior.AllowGet);

            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
       // [Authorize(Roles = "Supplier,Buyer")]
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult StockListReportList(string CreateOn)
        {
            string UserID = User.Identity.Name;
            DateTime CreatedOn = Convert.ToDateTime(CreateOn);            
            var jsonResult = Json(db.Database.SqlQuery<tblStock>("exec Sp_StockReport @USERID={0},@CREATEDON={1}", UserID, CreatedOn).ToList(), JsonRequestBehavior.AllowGet);

            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public class StockDeclaration
        {

            public Int32 SLNO { get; set; }
            public string PONO { get; set; }
            public string SUPPLIERCODE { get; set; }
            public string SUPPLIERNAME { get; set; }
            public string MATERIALCODE { get; set; }
            public string MATERIALDESCRIPTION { get; set; }
            public string RAWMATERIAL { get; set; }
            public string UOM { get; set; }
            public string WIP { get; set; }
            public string WIPUOM { get; set; }
            public string FINISHGOODS { get; set; }
            public string FINISHGOODSUOM { get; set; }
            public string REMARK { get; set; }
            public DateTime CREATEDON { get; set; }
        }
        public partial class TableStock
        {
            public string PONO { get; set; }
            public string SUPPLIERCODE { get; set; }
            public string SUPPLIERNAME { get; set; }
            public string MATERIALCODE { get; set; }
            public string MATERIALDESCRIPTION { get; set; }
            public string RAWMATERIAL { get; set; }
            public string UOM { get; set; }
            public string WIP { get; set; }
            public string WIPUOM { get; set; }
            public string FINISHGOODS { get; set; }
            public string FINISHGOODSUOM { get; set; }
            public string REMARK { get; set; }
            public System.DateTime PODATE { get; set; }
            public int CREATEDBY { get; set; }
            public System.DateTime CREATEDON { get; set; }
        }
        public class StockDeclaration2
        {
            public string PONO { get; set; }
            public string SUPPLIERCODE { get; set; }
            public string SUPPLIERNAME { get; set; }
            public string MATERIALCODE { get; set; }
            public string MATERIALDESCRIPTION { get; set; }
            public string RAWMATERIAL { get; set; }
            public string UOM { get; set; }
            public string WIP { get; set; }
            public string WIPUOM { get; set; }
            public string FINISHGOODS { get; set; }
            public string FINISHGOODSUOM { get; set; }
            public string REMARK { get; set; }
            public string DATE { get; set; }



        }
    }
}