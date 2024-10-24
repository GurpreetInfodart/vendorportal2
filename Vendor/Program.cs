using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Vendor
{
    class Program
    {
        static string MaterialPath = "C:\\FTP\\Supplier_Upload.csv";
        public static SimpleLogger logger = new SimpleLogger("vendorimportlog" + DateTime.Now.ToString("yyyyMMdd").ToLower() + ".log", true);
        static void Main(string[] args)
        {
            if (!File.Exists(MaterialPath))
            {
                Console.WriteLine("Supplier Uploaded file not found!!!!!.");
                System.Threading.Thread.Sleep(3000);

            }
            else
            {
                var watch = new System.Diagnostics.Stopwatch();
                watch.Start();
                VendorPortalEntities db = new VendorPortalEntities();
                var lines = File.ReadAllLines(MaterialPath).Skip(1).Select(a => a.Split('\n'));
                int count = 0;
                foreach (var line in lines)
                {
                    Console.Out.WriteLine((Convert.ToString(count++)));
                    try
                    {
                        var record = line[0].Split(',');
                        SUPPLIER_MASTER supplier = new SUPPLIER_MASTER();
                        supplier.SUPP_CODE = record[0];
                        supplier.SUPP_NAME = record[1];
                        supplier.EMAIL_ID = record[2];
                        supplier.CONTACT_NO = record[3];
                        supplier.ADDRESS1 = record[4];
                        supplier.CITY = record[5];
                        supplier.PIN = record[6];
                        supplier.STATE = record[7];
                        supplier.COUNTRY = record[8];
                        supplier.GSTIN = record[9];
                        supplier.CreatedOn = DateTime.UtcNow;
                        supplier.CreatedBy = 0;
                        supplier.UpdatedOn = DateTime.UtcNow;
                        supplier.UpdatedBy = DateTime.UtcNow;
                        var supp = (from x in db.SUPPLIER_MASTER
                                    where x.SUPP_CODE == supplier.SUPP_CODE
                                    select x).FirstOrDefault();
                        if (supp == null)
                        {
                            db.SUPPLIER_MASTER.Add(supplier);
                            logger.Info("Supplier Code Queued for Insert: " + line[0]);
                        }
                        else
                        {
                            supp.SUPP_CODE = supplier.SUPP_CODE;
                            supp.SUPP_NAME = supplier.SUPP_NAME;
                            supp.EMAIL_ID = supplier.EMAIL_ID;
                            supp.CONTACT_NO = supplier.CONTACT_NO;
                            supp.ADDRESS1 = supplier.ADDRESS1;
                            supp.CITY = supplier.CITY;
                            supp.STATE = supplier.STATE;
                            supp.COUNTRY = supplier.COUNTRY;
                            supp.GSTIN = supplier.GSTIN;
                            supp.UpdatedOn = DateTime.UtcNow;
                            supp.UpdatedBy = DateTime.UtcNow;
                            db.Entry(supp).State = System.Data.Entity.EntityState.Modified;
                            logger.Info("Supplier Code Queued for Update: " + line[0]);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        logger.Error("Supplier code parsing error :" + line[0]);
                        logger.Error(e.Message);
                    }

                }
                db.SaveChanges();
                Console.WriteLine(Convert.ToString(count) + " Records Parsed.");
                string sourceFilePath = @"C:\\FTP\\Supplier_Upload.csv";
                string destinationFilePath = @"C:\\FTP\\DailyBackup\\Supplier_Upload_" + DateTime.Now.ToString("ddMMyyyy hhmmss") + ".csv";
                System.IO.File.Move(sourceFilePath, destinationFilePath);
                watch.Stop();
                Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
                //Console.In.ReadLine();
                System.Threading.Thread.Sleep(10000); //Delay for 5 seconds  
            }
        }
    }
}
