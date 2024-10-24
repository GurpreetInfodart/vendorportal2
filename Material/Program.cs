using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Material
{
    class Program
    {
        static string MaterialPath = "C:\\FTP\\Material_Upload.csv";
        public static SimpleLogger logger = new SimpleLogger( "matimportlog" + DateTime.Now.ToString("yyyyMMdd").ToLower() + ".log", true);
        static void Main(string[] args)
        {
            if (!File.Exists(MaterialPath))
            {
                Console.WriteLine("Materials Uploaded file not found!!!!!.");
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
                    tblMaterial material = new tblMaterial();
                    try
                    {
                        var record = line[0].Split(',');
                        material.MaterialCode = record[0];
                        material.MaterialDescription = record[1];
                        material.Unit = record[2];
                        material.MaterialGroup = record[3];
                        string SNP1 = Convert.ToString(record[4]);
                        string SNP;
                        //string SNP = String.Format("{0:0.##}", SNP1);
                        if (SNP1.Contains("."))
                        {
                            SNP = SNP1.Remove(SNP1.IndexOf("."));
                            material.SNP = Convert.ToInt32(SNP);
                        }
                        else
                        {
                            SNP = SNP1;
                            material.SNP = Convert.ToInt32(SNP);
                        }
                        material.CreatedBy = 0;
                        material.CreatedOn = DateTime.UtcNow;
                        material.UpdatedBy = 0;
                        material.UpdatedOn = DateTime.UtcNow;
                        material.Active = true;

                        //db.tblMaterials.Add(material);
                        //logger.Info("Mat Code Queued for Insert: " + line);
                        //db.SaveChanges();
                        var mat = (from x in db.tblMaterials
                                   where x.MaterialCode == material.MaterialCode
                        select x).FirstOrDefault();
                        if (mat == null)
                        {
                            db.tblMaterials.Add(material);
                            logger.Info("Mat Code Queued for Insert: " + line[0]);
                        }
                        else
                        {
                            mat.MaterialDescription = material.MaterialDescription;
                            mat.Unit = material.Unit;
                            mat.MaterialGroup = material.MaterialGroup;
                            mat.SNP = material.SNP;
                            mat.UpdatedOn = DateTime.UtcNow;
                            mat.Active = true;
                            db.Entry(mat).State = System.Data.Entity.EntityState.Modified;
                            logger.Info("Mat Code Queued for Update: " + line[0]);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        logger.Error("Mat code parsing error :" + line[0]);
                        logger.Error(e.Message);
                    }

                }
                db.SaveChanges();
                Console.WriteLine(Convert.ToString(count) + " Records Parsed.");
                string sourceFilePath = @"C:\\FTP\\Material_Upload.csv";
                string destinationFilePath = @"C:\\FTP\\DailyBackup\\Material_Upload_" + DateTime.Now.ToString("ddMMyyyy hhmmss") + ".csv";
                System.IO.File.Move(sourceFilePath, destinationFilePath);
                watch.Stop();
                Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
                Console.WriteLine("Materials Uploaded process has been completed.Thank You!!!!!.");
                //Console.In.ReadLine();
                System.Threading.Thread.Sleep(10000); //Delay for 5 seconds  
            }
        }
    }
}
