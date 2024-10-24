using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
//using ClosedXML;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;



namespace VendorPortal
{
    /// <summary>
    /// Created By:Naresh Kumar
    /// Created Date:11-10-2018
    /// purpose:Import Data in Excel & Convert List To Table & Convert List To table with specific Coloumn
    /// </summary>
    public class ExcelExportHelper: Controller
    {
        public static string ExcelContentType
        {
            get
            { return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; }
        }
        public static DataTable ListToDataTable<T>(List<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable dataTable = new DataTable();

            for (int i = 0; i < properties.Count; i++)
            {
                PropertyDescriptor property = properties[i];
                dataTable.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
            }

            object[] values = new object[properties.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = properties[i].GetValue(item);
                }

                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
        // public static byte[] ExportExcel(DataTable dataTable, string heading = "", bool showSrNo = false, params string[] columnsToTake)  
        //{  
  
        //    byte[] result = null;  
        //    using (ExcelPackage package = new ExcelPackage())  
        //    {  
        //        ExcelWorksheet workSheet = package.Workbook.Worksheets.Add(String.Format("{0} Data",heading));  
        //        int startRowFrom = String.IsNullOrEmpty(heading) ? 1 : 3;  
  
        //        if (showSrNo)  
        //        {  
        //            DataColumn dataColumn = dataTable.Columns.Add("#", typeof(int));  
        //            dataColumn.SetOrdinal(0);  
        //            int index = 1;  
        //            foreach (DataRow item in dataTable.Rows)  
        //            {  
        //                item[0] = index;  
        //                index++;  
        //            }  
        //        }  
  
  
        //        // add the content into the Excel file  
        //        workSheet.Cells["A" + startRowFrom].LoadFromDataTable(dataTable, true);  
  
        //        // autofit width of cells with small content  
        //        int columnIndex = 1;  
        //        //foreach (DataColumn column in dataTable.Columns)  
        //        //{  
        //        //    ExcelRange columnCells = workSheet.Cells[workSheet.Dimension.Start.Row, columnIndex, workSheet.Dimension.End.Row, columnIndex];  
        //        //    int maxLength = columnCells.Max(cell => cell.Value.ToString().Count());  
        //        //    if (maxLength < 150)  
        //        //    {  
        //        //        workSheet.Column(columnIndex).AutoFit();  
        //        //    }  
  
  
        //        //    columnIndex++;  
        //        //}  
  
        //        // format header - bold, yellow on black  
        //        using (ExcelRange r = workSheet.Cells[startRowFrom, 1, startRowFrom, dataTable.Columns.Count])  
        //        {  
        //            r.Style.Font.Color.SetColor(System.Drawing.Color.White);  
        //            r.Style.Font.Bold = true;  
        //            r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;  
        //            r.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#1fb5ad"));  
        //        }  
  
        //        // format cells - add borders  
        //        using (ExcelRange r = workSheet.Cells[startRowFrom + 1, 1, startRowFrom + dataTable.Rows.Count, dataTable.Columns.Count])  
        //        {  
        //            r.Style.Border.Top.Style = ExcelBorderStyle.Thin;  
        //            r.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;  
        //            r.Style.Border.Left.Style = ExcelBorderStyle.Thin;  
        //            r.Style.Border.Right.Style = ExcelBorderStyle.Thin;  
  
        //            r.Style.Border.Top.Color.SetColor(System.Drawing.Color.Black);  
        //            r.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.Black);  
        //            r.Style.Border.Left.Color.SetColor(System.Drawing.Color.Black);  
        //            r.Style.Border.Right.Color.SetColor(System.Drawing.Color.Black);  
        //        }  
  
        //        // removed ignored columns  
        //        for (int i = dataTable.Columns.Count - 1; i >= 0; i--)  
        //        {  
        //            if (i == 0 && showSrNo)  
        //            {  
        //                continue;  
        //            }  
        //            if (!columnsToTake.Contains(dataTable.Columns[i].ColumnName))  
        //            {  
        //                workSheet.DeleteColumn(i + 1);  
        //            }  
        //        }  
  
        //        if (!String.IsNullOrEmpty(heading))  
        //        {  
        //            workSheet.Cells["A1"].Value = heading;  
        //            workSheet.Cells["A1"].Style.Font.Size = 20;  
  
        //            workSheet.InsertColumn(1, 1);  
        //            workSheet.InsertRow(1, 1);  
        //            workSheet.Column(1).Width = 5;  
        //        }  
  
        //        result = package.GetAsByteArray();  
        //    }  
  
        //    return result;  
        //}

        public FileResult ExportExcel<T>(List<T> data, string FileName = "", string ExportFormat= ".xls", string MDLAttrName="", params string[] ColumnsToTake)
        {
            // return ExportExcel(ListToDataTable<T>(data), Heading, showSlno, ColumnsToTake);
             DataTable dt=  ConvertListToDataTable(ListToDataTable<T>(data), MDLAttrName, ColumnsToTake);
            return ExportToFormats(dt, FileName, ExportFormat);

        }
        public FileResult ExportToFormats(DataTable tableToExport, string ReportType, string ExportFormat)
        {
            GridView DataGrid = new GridView();
            DataGrid.AllowPaging = false;
            DataGrid.DataSource = tableToExport;
            DataGrid.DataBind();
           

            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            DataGrid.RenderControl(hw);
            byte[] content = Encoding.UTF8.GetBytes(sw.ToString());

            string FileName = ReportType + "_" + DateTime.Now.ToString("dd.MM.yyyy_HH.mm") + ExportFormat;

            if (ExportFormat == ".xls")
            {
                return File(content, "application/vnd.ms-excel", FileName); //"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            }

            else if (ExportFormat == ".xlsx")
            {
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", FileName);
            }

            else if (ExportFormat == ".doc")
            {
                return File(content, "application/msword", FileName); // "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
            }

            else if (ExportFormat == ".docx")
            {
                return File(content, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", FileName);
            }

            else
            {
                //Response.Clear();
                //Response.ContentType = "application/pdf";                
                //Response.AddHeader("content-disposition", "attachment;filename=" +FileName+ "");
                //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //DataGrid.RenderControl(hw);
                //StringReader sr = new StringReader(sw.ToString());
                //Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                //PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                //pdfDoc.Open();
                //htmlparser.Parse(sr);
                //pdfDoc.Close();
                //Response.Write(pdfDoc);
                //Response.End();
                ////ExportToPDF(tableToExport, FileName);
                return null;
            }
        }
        public DataTable ConvertListToDataTable(DataTable datadt, string MDLAttrName = "", params string[] ColumnsToTake)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Sr. No.", typeof(string));
            foreach (string s in ColumnsToTake)
            {
                dt.Columns.Add(s, typeof(string));
            }
            int index = 1;
            foreach (DataRow lstdr in datadt.Rows)
            {
                DataRow dr = dt.NewRow();
                dr["Sr. No."] = index.ToString();
                var stringArray = MDLAttrName.Split(',');
                int i = 0;
                foreach (string s in ColumnsToTake)
                {
                    dr[s] = lstdr[stringArray[i]].ToString();
                    i = i + 1;
                }
                index = index + 1;
                dt.Rows.Add(dr);
          }
                return dt;
        }


        #region region for export excel common methods for area

        public FileResult ExportExcelArea<T>(List<T> data, string FileName = "", string ExportFormat = ".xls", string MDLAttrName = "", params string[] ColumnsToTake)
        {
            // return ExportExcel(ListToDataTable<T>(data), Heading, showSlno, ColumnsToTake);
            DataTable dt = ConvertListToDataTableArea(ListToDataTable<T>(data), MDLAttrName, ColumnsToTake);
            return ExportToFormatsArea(dt, FileName, ExportFormat);

        }

        public FileResult ExportToFormatsArea(DataTable tableToExport, string ReportType, string ExportFormat)
        {
            GridView DataGrid = new GridView();
            DataGrid.AllowPaging = false;
            DataGrid.DataSource = tableToExport;
            DataGrid.DataBind();


            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            DataGrid.RenderControl(hw);
            byte[] content = Encoding.UTF8.GetBytes(sw.ToString());

            string FileName = ReportType + "_" + DateTime.Now.ToString("dd.MM.yyyy_HH.mm") + ExportFormat;

            if (ExportFormat == ".xls")
            {
                return File(content, "application/vnd.ms-excel", FileName); //"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            }

            else if (ExportFormat == ".xlsx")
            {
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", FileName);
            }

            else if (ExportFormat == ".doc")
            {
                return File(content, "application/msword", FileName); // "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
            }

            else if (ExportFormat == ".docx")
            {
                return File(content, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", FileName);
            }

            else
            {
                //Response.Clear();
                //Response.ContentType = "application/pdf";                
                //Response.AddHeader("content-disposition", "attachment;filename=" +FileName+ "");
                //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //DataGrid.RenderControl(hw);
                //StringReader sr = new StringReader(sw.ToString());
                //Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                //PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                //pdfDoc.Open();
                //htmlparser.Parse(sr);
                //pdfDoc.Close();
                //Response.Write(pdfDoc);
                //Response.End();
                ////ExportToPDF(tableToExport, FileName);
                return null;
            }
        }


        public DataTable ConvertListToDataTableArea(DataTable datadt, string MDLAttrName = "", params string[] ColumnsToTake)
        {
            DataTable dt = new DataTable();
           // dt.Columns.Add("Sr. No.", typeof(string));
            foreach (string s in ColumnsToTake)
            {
                dt.Columns.Add(s, typeof(string));
            }
           // int index = 1;
            foreach (DataRow lstdr in datadt.Rows)
            {
                DataRow dr = dt.NewRow();
               // dr["Sr. No."] = index.ToString();
                var stringArray = MDLAttrName.Split(',');
                int i = 0;
                foreach (string s in ColumnsToTake)
                {
                    dr[s] = lstdr[stringArray[i]].ToString();
                    i = i + 1;
                }
               // index = index + 1;
                dt.Rows.Add(dr);
            }
            return dt;
        }
        #endregion

    }
}