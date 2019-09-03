using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using ExcelDataReader;
using FileUploader.Contracts;


namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {

            string filePath = @"C:\Users\UMESH\Desktop\POC\CarData.xlsx";
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var excelReader = ExcelReaderFactory.CreateReader(stream))
                {

                    var sheets = excelReader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });

                    DataTable spreadSheet = sheets.Tables[0];
                    var carmodels = Map(spreadSheet);
                }
            }

        }

        public static List<CarModel> Map(DataTable dataTable)
        {
            var carModels = new List<CarModel>();

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {

                var carModel = new CarModel
                {
                    Ecs = Convert.ToString(dataTable.Rows[i].ItemArray[0]),
                    BrandCategory = Convert.ToString(dataTable.Rows[i].ItemArray[1]),
                    Model = Convert.ToString(dataTable.Rows[i].ItemArray[2]),
                    ModelCode = Convert.ToString(dataTable.Rows[i].ItemArray[3]),
                    Country = Convert.ToString(dataTable.Rows[i].ItemArray[4]),
                    Engine = Convert.ToString(dataTable.Rows[i].ItemArray[5]),
                    Display = Convert.ToString(dataTable.Rows[i].ItemArray[6]),
                    Steering = Convert.ToString(dataTable.Rows[i].ItemArray[7]),
                    Transaction = Convert.ToString(dataTable.Rows[i].ItemArray[8]),
                    Performance = Convert.ToString(dataTable.Rows[i].ItemArray[9]),
                    ModelCodeText = Convert.ToString(dataTable.Rows[i].ItemArray[10]),
                    Series_Ivp = Convert.ToString(dataTable.Rows[i].ItemArray[11]),
                    Cylinder = Convert.ToString(dataTable.Rows[i].ItemArray[12]),
                    SOP = Convert.ToString(dataTable.Rows[i].ItemArray[12]),
                    EOP = Convert.ToString(dataTable.Rows[i].ItemArray[14])
                };
                carModels.Add(carModel);
            }
            Console.ReadLine();


            return carModels;
        }
    }
}
