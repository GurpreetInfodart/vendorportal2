using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseOrder
{
    class Program
    {
        static string PurchaseOrderPath = "C:\\FTP\\PO_UPLOAD.csv";
        public static SimpleLogger logger = new SimpleLogger("PurchaseOrderlog" + DateTime.Now.ToString("yyyyMMdd").ToLower() + ".log", true);
        static VendorPortalEntities db = new VendorPortalEntities();

        static void Main(string[] args)
        {
            if (!File.Exists(PurchaseOrderPath))
            {
                Console.WriteLine("PO Uploaded file not found!!!!!.");
                System.Threading.Thread.Sleep(3000); //Delay fo

            }
            else
            {
                readCSV();
            }
        }

        public static void readCSV()
        {
            List<PO_MASTER> values = File.ReadAllLines(PurchaseOrderPath)
                                           .Skip(1)
                                           .Select(v => Program.FromCsv(v))
                                           .ToList();

            Console.WriteLine("Read Lines - " + values.Count + " from csv.");
            Console.WriteLine("Starting Database insert");
            logger.Info("Read Lines - " + values.Count + " from csv.");

            List<PO_MASTER> distinctSchdMaster = values.GroupBy(p => p.PO_NUM).Select(g => g.First()).ToList();
            Console.Out.WriteLine("Found " + distinctSchdMaster.Count() + " Distinct PO Numbers.");
            logger.Info("Found " + distinctSchdMaster.Count() + " Distinct PO Numbers.");

            foreach (PO_MASTER item in distinctSchdMaster)
            {
                try
                {
                    int pocount = (from pom in db.POes where pom.PO_NUM == item.PO_NUM select pom).Count();
                    if (pocount == 0)
                    {
                        PO po = new PO();
                        po.PO_NUM = item.PO_NUM;
                        po.PO_DATE = Convert.ToDateTime( item.PO_DATE);
                        po.SUPP_CODE = item.SUPP_CODE;

                        db.POes.Add(po);
                        logger.Info("New Purchase Order Created. Number-" + po.PO_NUM);

                    }
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    logger.Error(ex.Message);
                }
            }
            db.SaveChanges();
            //Console.Read();
            int rcount = 0;
            foreach (PO_MASTER item in values)
            {
                rcount++;
                //var poid = (from x in db.POes
                //            where x.PO_NUM == item.PO_NUM && x.SUPP_CODE == item.SUPP_CODE
                //            select x.ID).FirstOrDefault();

                int count = (from po in db.PO_DETAILS
                             where po.PO_NUM == item.PO_NUM  && po.MaterialCode == item.ITEM_CODE
                             select po).Count();
                PO_DETAILS pod = new PO_DETAILS();
                pod.PO_NUM = item.PO_NUM;
                pod.MaterialCode = item.ITEM_CODE;
                pod.CESS = Convert.ToDecimal(item.CESS);
                pod.CGST = Convert.ToDecimal(item.CGST);
                pod.DISCOUNT = Convert.ToDecimal(item.DISCOUNT);
                pod.FREIGHT = Convert.ToDecimal(item.FREIGHT);
                pod.GL_ACC = item.GL_ACC;
                pod.HSN_SAC = item.HSN_SAC;
                pod.IGST = Convert.ToDecimal(item.IGST);
                pod.NET_AMOUNT = Convert.ToDecimal(item.NET_AMOUNT);
                pod.PF = Convert.ToDecimal(item.PF);
                pod.PLANT_CODE = item.PLANT_CODE;
                pod.QUANTITY = Convert.ToDecimal(item.QUANTITY);
                pod.SGST = Convert.ToDecimal(item.SGST);
                pod.TOTAL_VALUE_EXC = Convert.ToDecimal(item.TOTAL_VALUE_EXC);
                pod.TOTAL_VALUE_INC = Convert.ToDecimal(item.TOTAL_VALUE_INC);
                pod.UNIT_RATE = Convert.ToDecimal(item.UNIT_RATE);
                pod.UOM = item.UOM;
                pod.STATUS = 0;
                pod.CREATEDON = item.CREATED_ON;
                if (count == 0)
                    db.PO_DETAILS.Add(pod);
                else
                {
                    var poupd = (from po in db.PO_DETAILS
                                 where po.PO_NUM == item.PO_NUM && po.MaterialCode == item.ITEM_CODE
                                 select po).FirstOrDefault();
                    poupd.CESS = Convert.ToDecimal(item.CESS);
                    poupd.CGST = Convert.ToDecimal(item.CGST);
                    poupd.DISCOUNT = Convert.ToDecimal(item.DISCOUNT);
                    poupd.FREIGHT = Convert.ToDecimal(item.FREIGHT);
                    poupd.GL_ACC = item.GL_ACC;
                    poupd.HSN_SAC = item.HSN_SAC;
                    poupd.IGST = Convert.ToDecimal(item.IGST);
                    poupd.NET_AMOUNT = Convert.ToDecimal(item.NET_AMOUNT);
                    poupd.PF = Convert.ToDecimal(item.PF);
                    poupd.PLANT_CODE = item.PLANT_CODE;
                    poupd.QUANTITY = Convert.ToDecimal(item.QUANTITY);
                    poupd.SGST = Convert.ToDecimal(item.SGST);
                    poupd.TOTAL_VALUE_EXC = Convert.ToDecimal(item.TOTAL_VALUE_EXC);
                    poupd.TOTAL_VALUE_INC = Convert.ToDecimal(item.TOTAL_VALUE_INC);
                    poupd.UNIT_RATE = Convert.ToDecimal(item.UNIT_RATE);
                    poupd.UOM = item.UOM;
                   db.Entry(poupd).State = System.Data.Entity.EntityState.Modified;
                    logger.Info("PO Line Item Queued for modification." + item.PO_NUM+ "," +item.ITEM_CODE);
                }
                Console.WriteLine("Parsed " + rcount + " Records");
                logger.Info("Parsed " + rcount + " Records");
                db.SaveChanges();

            }
            Console.WriteLine("Wrote " + values.Count + " records");
            string sourceFilePath = @"C:\\FTP\\PO_UPLOAD.csv";
            string destinationFilePath = @"C:\\FTP\\DailyBackup\\PO_UPLOAD_" + DateTime.Now.ToString("ddMMyyyy hhmmss") + ".csv";
            System.IO.File.Move(sourceFilePath, destinationFilePath);
            Console.WriteLine("PO Uploaded process has been completed.Thank You!!!!!.");
            System.Threading.Thread.Sleep(5000); //Delay for 5 seconds  
        }

        public static PO_MASTER FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            PO_MASTER po_obj = new PO_MASTER();
            po_obj.PO_NUM = values[0];
            po_obj.PO_DATE =  values[1];
            po_obj.SUPP_CODE = values[2];
            po_obj.ITEM_CODE = values[3];
            po_obj.MAT_CODE = po_obj.ITEM_CODE; //
            po_obj.HSN_SAC = values[4];
            po_obj.GL_ACC = values[2];
            po_obj.QUANTITY = values[5];
            po_obj.UOM = values[6];
            po_obj.UNIT_RATE = values[7];
            po_obj.DISCOUNT = values[9]; //
            po_obj.NET_AMOUNT = values[8];
            po_obj.FREIGHT = values[10];
            po_obj.PF = values[11];
            po_obj.SGST = values[12];
            po_obj.CGST = values[13];
            po_obj.IGST = values[14];
            po_obj.CESS = values[15];
            po_obj.TOTAL_VALUE_INC = values[16];
            po_obj.TOTAL_VALUE_EXC = values[17]; //
            po_obj.PLANT_CODE = values[18];
            po_obj.CREATED_BY = 0;
            po_obj.CREATED_ON = DateTime.UtcNow;
            po_obj.STATUS = "Pending";
            po_obj.ACC_REJ_BY = 0;
            po_obj.ACC_REJ_ON = DateTime.UtcNow;
            po_obj.refslno = 0;

            return po_obj;
        }
    }
}
