using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using ExcelDataReader;
using FileUploader.Contracts;

namespace FileUploader.Services
{
    public class Mapper
    {
        public List<CarModel> ConvertData(string filePath)
        {
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var excelReader = ExcelReaderFactory.CreateReader(stream))
                {
                    var sheets = excelReader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });
                    var spreadSheet = sheets.Tables[0];
                    var excelDataRows = Map(spreadSheet);
                    var rows = FormatDataRow(excelDataRows);
                    SaveRowsToDB(rows);
                    return excelDataRows;
                }
            }
        }

        private void SaveRowsToDB(List<List<CarModel>> rows)
        {
            // DB Code 
        }

        /// <summary>
        /// Converting Excel Data Rows in to Objects
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public List<CarModel> Map(DataTable dataTable)
        {
            var carModels = new List<CarModel>();
           
            for (var i = 0; i < dataTable.Rows.Count; i++)
            {
                var carModel = new CarModel
                {
                    Ecs = Convert.ToString(dataTable.Rows[i].ItemArray[0]) ,
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
                    Series_Ivp = Convert.ToString(dataTable.Rows[i].ItemArray[12]),
                    Cylinder = Convert.ToString(dataTable.Rows[i].ItemArray[14]),
                    SOP = Convert.ToString(dataTable.Rows[i].ItemArray[15]),
                    EOP = Convert.ToString(dataTable.Rows[i].ItemArray[16])
                };
                carModels.Add(carModel);
            }
            return carModels;
        }

        public List<List<CarModel>> FormatDataRow(List<CarModel> carModel)
        {
            var rows =new List<List<CarModel>>();
            var validRows = new List<CarModel>();
            var inValidRows = new List<CarModel>();
           for(var i =0 ;i < carModel.Count ;i++)
            {
                //Console.WriteLine($"[E-CS:{carModel[i].Ecs} B. Cat:{carModel[i].BrandCategory} Model:{carModel[i].Model}" +
                //                  $"ModelCode:{carModel[i].ModelCode} Country:{carModel[i].Country}  Engine:{carModel[i].Engine}" +
                //                  $"Display:{carModel[i].Display}Steering:{carModel[i].Steering} Transaction:{carModel[i].Transaction}" +
                //                  $"Performance:{carModel[i].Performance} ModelCodeText:{carModel[i].ModelCodeText} Series_Ivp:{carModel[i].Series_Ivp}" +
                //                  $"Cylinder:{carModel[i].Cylinder}SOP:{carModel[i].SOP}EOP:{carModel[i].EOP}]\n");
                if (!string.IsNullOrEmpty(carModel[i].Ecs) && verifyCells(carModel[i]))
                {
                    validRows.Add(carModel[i]);
                }
                else if(string.IsNullOrEmpty(carModel[i].Ecs) && verifyCells(carModel[i]))
                {
                    carModel[i].EOP = carModel[i].SOP;
                    carModel[i].SOP = carModel[i].Cylinder;
                    carModel[i].Cylinder = carModel[i].Series_Ivp;
                    carModel[i].Series_Ivp = carModel[i].ModelCodeText;
                    carModel[i].ModelCodeText = carModel[i].Performance;
                    carModel[i].Performance = carModel[i].Transaction;
                    carModel[i].Transaction = carModel[i].Steering;
                    carModel[i].Steering = carModel[i].Display;
                    carModel[i].Display = carModel[i].Engine;
                    carModel[i].Engine = carModel[i].Country;
                    carModel[i].Country = carModel[i].ModelCode;
                    carModel[i].ModelCode = carModel[i].Model;
                    carModel[i].Model = carModel[i].BrandCategory;
                    carModel[i].BrandCategory = carModel[i-1].BrandCategory;
                    carModel[i].Ecs = carModel[i-1].Ecs;
                    validRows.Add(carModel[i]);
                }
                else
                {
                    Console.WriteLine($"InValid Row :[ECS:{carModel[i].Ecs} , B.Cat:{carModel[i].BrandCategory}]");
                    inValidRows.Add(carModel[i]);
                }
            }
           rows.Add(validRows);
           rows.Add(inValidRows);
            return rows;
        }

        private static bool verifyCells(CarModel model)
        {
            var status =  !string.IsNullOrEmpty(model.BrandCategory) || !string.IsNullOrEmpty(model.Model) || !string.IsNullOrEmpty(model.ModelCode) ||
                          !string.IsNullOrEmpty(model.Country) || !string.IsNullOrEmpty(model.Engine) || !string.IsNullOrEmpty(model.Display) || !string.IsNullOrEmpty(model.Steering) ||
                          !string.IsNullOrEmpty(model.Transaction) || !string.IsNullOrEmpty(model.Performance) || !string.IsNullOrEmpty(model.ModelCode) || !string.IsNullOrEmpty(model.Series_Ivp) ||
                          !string.IsNullOrEmpty(model.Cylinder) || !string.IsNullOrEmpty(model.SOP) || !string.IsNullOrEmpty(model.EOP);
            return status;
        }
    }
}
