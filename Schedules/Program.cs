using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Schedules
{
    class Program
    {
        public static SimpleLogger logger = new SimpleLogger("scheduleimportlog" + DateTime.Now.ToString("yyyyMMdd").ToLower() + ".log", true);
        static VendorPortalEntities db = new VendorPortalEntities();
        static void Main(string[] args)
        {
            if (!File.Exists("C:\\FTP\\SCHEDULE_UPLOAD.csv"))
            {
                Console.WriteLine("schedule Uploaded file not found!!!!!.");
                System.Threading.Thread.Sleep(3000); //Delay fo

            }
            else
            {
                var watch = new System.Diagnostics.Stopwatch();

                watch.Start();

                ReadCSV();

                watch.Stop();

                Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
                //Console.In.ReadLine();
            }
        }

        public static void ReadCSV()
        {
            try
            {
                List<PO_SCHEDULE_DTLS> values = File.ReadAllLines("C:\\FTP\\SCHEDULE_UPLOAD.csv")
                                           .Skip(1)
                                           .Select(v => Program.ScheduleLineFromCsv(v))
                                           .ToList();

                Console.WriteLine("Read Lines - " + values.Count + " from csv.");
                Console.WriteLine("Starting Database insert");
                logger.Info("Read Lines - " + values.Count + " from csv.");
                //Console.Read();


                //List<PO_SCHEDULE_DTLS> distinctSchdMaster = values
                //    .GroupBy(p => new { p.SCHEDULE_NO, p.SUPPLIER_CODE, p.DEL_DATE })
                //    .Select(g => g.First())
                //    .ToList();


                //Console.WriteLine(distinctSchdMaster.Count + " Schedules found.");
                ////Console.ReadLine();
                //logger.Info("Found " + distinctSchdMaster.Count() + " Distinct Schedules.");

                var polist = (from x in db.POes
                              select x).ToList();

                foreach (PO_SCHEDULE_DTLS item in values)
                {
                    Schedule schedule = new Schedule();
                    schedule.DEL_DATE = Convert.ToDateTime(item.DEL_DATE);
                    schedule.MAT_CODE = item.MAT_CODE;
                    schedule.PLANT_CODE = item.PLANT_CODE;
                    schedule.Qty = Convert.ToDecimal(item.QUANTITY);
                    schedule.Position = item.POSITION_NO;
                    schedule.Status = 0;
                    //schedule.IS_BILL_GENERATED = "1";
                    schedule.IS_BILL_GENERATED = item.Is_BillGenerated;
                    schedule.REMARKS = "";
                    schedule.CONTRACT_NO = item.CONTRACT_NO;
                    schedule.POID = (from po in polist
                                     where po.PO_NUM == item.SCHEDULE_NO
                                     select po.ID
                                     ).FirstOrDefault();
                    schedule.SCHEDULE_NO = item.SCHEDULE_NO;
                    schedule.CreatedBy = 0;
                    schedule.CreatedOn = DateTime.UtcNow;
                    schedule.UpdatedBy = 0;
                    schedule.UpdatedOn = DateTime.UtcNow;
                    schedule.UploadDate = DateTime.Today;

                    var ExistingSchedule = (from x in db.Schedules
                                            where x.POID == schedule.POID
                                            && x.DEL_DATE == schedule.DEL_DATE
                                            && x.MAT_CODE == schedule.MAT_CODE
                                            select x).FirstOrDefault();
                    if (ExistingSchedule == null)
                        db.Schedules.Add(schedule);
                    else
                    {
                        ExistingSchedule.UpdatedBy = 0;
                        ExistingSchedule.UpdatedOn = DateTime.UtcNow;
                        ExistingSchedule.Qty = schedule.Qty;
                        ExistingSchedule.Position = schedule.Position;
                        ExistingSchedule.PLANT_CODE = schedule.PLANT_CODE;
                        ExistingSchedule.CONTRACT_NO = schedule.CONTRACT_NO;
                        db.Entry(ExistingSchedule).State = System.Data.Entity.EntityState.Modified;

                    }

                    //  logger.Info("Schedule Line Queued for insert on " + schedule.MAT_CODE + item.SUPPLIER_CODE +item.DEL_DATE);
                }

                db.SaveChanges();
                Console.WriteLine("Wrote " + values.Count + " records");
                //Console.ReadLine();
                logger.Info("Wrote  - " + values.Count + "  records.");
                string sourceFilePath = @"C:\\FTP\\SCHEDULE_UPLOAD.csv";
                string destinationFilePath = @"C:\\FTP\\DailyBackup\\SCHEDULE_UPLOAD_" + DateTime.Now.ToString("ddMMyyyy hhmmss") + ".csv";
                System.IO.File.Move(sourceFilePath, destinationFilePath);
                Console.WriteLine("SCHEDULE Uploaded process has been completed.Thank You!!!!!.");
                System.Threading.Thread.Sleep(5000); //Delay for 5 seconds 
            }
            catch (Exception ex)
            {
                ex.ToString();
                logger.Error(ex.Message);
            }
        }

        public static PO_SCHEDULE_DTLS ScheduleLineFromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            PO_SCHEDULE_DTLS schd = new PO_SCHEDULE_DTLS();
            schd.SUPPLIER_CODE = values[0];
            schd.MAT_CODE = values[1];
            schd.POSITION_NO = values[2];
            schd.CONTRACT_NO = "";
            schd.QUANTITY = values[4];
            schd.DEL_DATE = values[5];
            schd.PLANT_CODE = values[6];
            schd.SCHEDULE_NO = values[3];
            schd.STATUS = "Pending";
            schd.Is_BillGenerated = "N";
            schd.REFNO = 0;
            return schd;
        }


    }
}
