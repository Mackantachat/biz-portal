using OfficeOpenXml;
using OfficeOpenXml.DataValidation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace BizPortal.Utils
{
    public static class Epplus
    {
        public static IEnumerable<T> ConvertSheetToObjects<T>(this ExcelWorksheet worksheet, int startRow = 2) where T : new()
        {

            //Get the properties of T
            var tprops = (new T())
                .GetType()
                .GetProperties()
                .ToList();

            //Cells only contains references to cells with actual data
            var groups = worksheet.Cells
                .GroupBy(cell => cell.Start.Row)
                .ToList();

            //Assume first row has the column names
            var colnames = groups
                .First()
                .Select((hcell, idx) => new { Name = hcell.Value, Index = idx })
                .ToList();

            //Everything after the header is data
            var rowvalues = new List<List<string>>();
            for (int row = startRow; row <= worksheet.Dimension.End.Row; row++)
            {
                if (string.IsNullOrEmpty(worksheet.Cells[row, 1].Text))
                {
                    break;
                }

                var columns = new List<string>();
                for (int column = 1; column <= worksheet.Dimension.End.Column; column++)
                {
                    columns.Add(worksheet.Cells[row, column].Text);
                }
                rowvalues.Add(columns);
            }

            //Create the collection container
            var collection = rowvalues
                .Select(row =>
                {
                    var tnew = new T();
                    colnames.ForEach(colname =>
                    {
                        //This is the real wrinkle to using reflection - Excel stores all numbers as double including int

                        //var prop = tprops.First(p => p.Name == colname.Name);

                        var prop = tprops.Where(w => w.GetCustomAttributes<Column>().FirstOrDefault() != null && w.GetCustomAttributes<Column>().FirstOrDefault().ColumnIndex == colname.Index).FirstOrDefault();
                        if (prop != null)
                        {
                            var val = row[colname.Index];
                            var type = prop.PropertyType;

                            if (type == typeof(double))
                            {
                                double convertVal = 0;
                                double.TryParse(val, out convertVal);
                                prop.SetValue(tnew, convertVal);
                            }
                            else if (type == typeof(DateTime) || type == typeof(DateTime?))
                            {
                                DateTime valDate;
                                int year = 0;

                                string date = val.ToString().Replace("-", "/");
                                string[] dateSplit = date.Split('/');

                                if (dateSplit.Count() == 3 && int.TryParse(dateSplit[2], out year))
                                {
                                    if (year > 2500)
                                    {
                                        if (year > 3000)
                                        {
                                            date = dateSplit[0] + "/" + dateSplit[1] + "/" + (year - 543);
                                        }

                                        if (DateTime.TryParse(date, new CultureInfo("th"), DateTimeStyles.None, out valDate))
                                        {
                                            prop.SetValue(tnew, valDate);
                                        }
                                        else
                                        {
                                            prop.SetValue(tnew, null);
                                        }
                                    }
                                    else
                                    {
                                        if (DateTime.TryParse(date, new CultureInfo("en"), DateTimeStyles.None, out valDate))
                                        {
                                            prop.SetValue(tnew, valDate);

                                        }
                                        else
                                        {
                                            prop.SetValue(tnew, null);
                                        }
                                    }
                                }
                                else
                                {
                                    prop.SetValue(tnew, null);
                                }
                            }
                            else if (type == typeof(int) || type == typeof(int?))
                            {
                                int convertVal = 0;
                                int.TryParse(val, out convertVal);
                                prop.SetValue(tnew, convertVal);
                            }
                            else if (type == typeof(bool) || type == typeof(bool?))
                            {
                                bool convertVal = false;
                                bool.TryParse(val, out convertVal);
                                prop.SetValue(tnew, convertVal);
                            }
                            else if (type == typeof(decimal) || type == typeof(decimal?))
                            {
                                decimal convertVal = 0;
                                decimal.TryParse(val, out convertVal);
                                prop.SetValue(tnew, convertVal);
                            }
                            else if (type == typeof(string))
                            {
                                prop.SetValue(tnew, val);
                            }
                            else
                            {
                                throw new NotImplementedException(String.Format("Type '{0}' not implemented yet!", prop.PropertyType.Name));
                            }
                        }
                    });

                    return tnew;
                });


            //Send it back
            return collection;
        }

        public static T ConvertRowToObjects<T>(this ExcelRange row) where T : new()
        {
            int index = 0;
            var tnew = new T();
            var tprops = (new T()).GetType()
                                  .GetProperties()
                                  .ToList();

            foreach (object val in (object[,])row.Value)
            {
                var prop = tprops.Where(w => w.GetCustomAttributes<Column>().FirstOrDefault() != null && w.GetCustomAttributes<Column>().FirstOrDefault().ColumnIndex == index).FirstOrDefault();

                if (prop != null)
                {
                    var type = prop.PropertyType;

                    if (type == typeof(double))
                    {
                        double convertVal = 0;
                        if (val != null && double.TryParse(val.ToString(), out convertVal))
                        {
                            prop.SetValue(tnew, convertVal);
                        }
                    }
                    else if (type == typeof(DateTime) || type == typeof(DateTime?))
                    {
                        if (val != null)
                        {
                            DateTime valDate;
                            int year = 0;

                            string date = val.ToString().Replace("-", "/");
                            string[] dateSplit = date.Split('/');

                            if (dateSplit.Count() == 3 && int.TryParse(dateSplit[2], out year))
                            {
                                if (year > 2500)
                                {
                                    if (year > 3000)
                                    {
                                        date = dateSplit[0] + "/" + dateSplit[1] + "/" + (year - 543);
                                    }

                                    if (DateTime.TryParse(date, new CultureInfo("th"), DateTimeStyles.None, out valDate))
                                    {
                                        prop.SetValue(tnew, valDate);
                                    }
                                    else
                                    {
                                        prop.SetValue(tnew, null);
                                    }
                                }
                                else
                                {
                                    if (DateTime.TryParse(date, new CultureInfo("en"), DateTimeStyles.None, out valDate))
                                    {
                                        prop.SetValue(tnew, valDate);
                                    }
                                    else
                                    {
                                        prop.SetValue(tnew, null);
                                    }
                                }
                            }
                            else
                            {
                                prop.SetValue(tnew, null);
                            }
                        }
                        else
                        {
                            prop.SetValue(tnew, null);
                        }
                    }
                    else if (type == typeof(int) || type == typeof(int?))
                    {
                        int convertVal = 0;
                        if (val != null && int.TryParse(val.ToString(), out convertVal))
                        {
                            prop.SetValue(tnew, convertVal);
                        }
                        else
                        {
                            prop.SetValue(tnew, null);
                        }
                    }
                    else if (type == typeof(bool) || type == typeof(bool?))
                    {
                        bool convertVal = false;
                        if (val != null && bool.TryParse(val.ToString(), out convertVal))
                        {
                            prop.SetValue(tnew, convertVal);
                        }
                        else
                        {
                            prop.SetValue(tnew, null);
                        }
                    }
                    else if (type == typeof(decimal) || type == typeof(decimal?))
                    {
                        decimal convertVal = 0;
                        if (val != null && decimal.TryParse(val.ToString(), out convertVal))
                        {
                            prop.SetValue(tnew, convertVal);
                        }
                        else
                        {
                            prop.SetValue(tnew, null);
                        }
                    }
                    else if (type == typeof(TimeSpan) || type == typeof(TimeSpan?))
                    {
                        TimeSpan convertVal;
                        if (val != null && TimeSpan.TryParse(val.ToString(), out convertVal))
                        {
                            prop.SetValue(tnew, convertVal);
                        }
                        else
                        {
                            prop.SetValue(tnew, null);
                        }
                    }
                    else if (type == typeof(string))
                    {
                        prop.SetValue(tnew, val);
                    }
                    else
                    {
                        throw new NotImplementedException(String.Format("Type '{0}' not implemented yet!", prop.PropertyType.Name));
                    }
                }

                index++;
            }

            return tnew;
        }

        public static bool IsNullOrEmpty(this ExcelRange row)
        {
            foreach (object val in (object[,])row.Value)
            {
                if (val != null)
                {
                    return false;
                }
            }

            return true;
        }

        public static int GetAmountOfImportRow(this ExcelWorksheet worksheet, List<string> columns = null)
        {
            int amountOfRow = 0;

            if (columns.Count() > 0)
            {
                foreach (var column in columns)
                {
                    var endRow = 0;
                    var endOfRow = worksheet.Cells[column + ":" + column].LastOrDefault();

                    if (endOfRow != null)
                    {
                        endRow = endOfRow.End.Row;
                    }
                    else
                    {
                        endRow = worksheet.Dimension.End.Row;
                    }

                    if (endRow > amountOfRow)
                    {
                        amountOfRow = endRow;
                    }
                }
            }
            else
            {
                return worksheet.Dimension.End.Row;
            }

            return amountOfRow;
        }

        public static string DbField<T>(this T obj, Expression<Func<T, string>> value)
        {
            var memberExpression = value.Body as MemberExpression;
            var attr = memberExpression.Member.GetCustomAttributes(typeof(Column), true);
            return ((Column)attr[0]).ColumnIndex.ToString();
        }

        public static MemoryStream GenerateImportTemplate(string templatePath, List<ImportTemplateViewModel> templateDatas, int startRowIndex = 1, List<List<string>> data = null, Dictionary<string, string> extenseData = null)
        {
            var ms = new MemoryStream();
            var file = File.Open(templatePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using (ExcelPackage pck = new ExcelPackage(ms, file))
            {
                // set template data
                var templateSheet = pck.Workbook.Worksheets["ข้อมูลที่จะนำเข้า"];
                var dataSheet = pck.Workbook.Worksheets["ข้อมูลพื้นฐาน"];
                int rowIndex = 0;
                int colIndex = 0;

                // set data
                if (data != null)
                {
                    foreach (var row in data)
                    {
                        foreach (var cell in row)
                        {
                            colIndex++;
                            templateSheet.Cells[(rowIndex + startRowIndex), colIndex].Value = cell;
                        }
                        colIndex = 0;
                        rowIndex++;
                    }
                }

                // set extense data
                if (extenseData != null)
                {
                    foreach (var value in extenseData)
                    {
                        templateSheet.Cells[value.Key].Value = value.Value;
                    }
                }

                // set validation
                foreach (var templateData in templateDatas)
                {
                    rowIndex = templateData.RowIndex;
                    colIndex = templateData.ColIndex;

                    var templateCells = templateSheet.Cells[rowIndex, colIndex, 1000, colIndex];
                    var listValidation = templateSheet.DataValidations.AddListValidation(templateCells.Address);

                    if (!string.IsNullOrEmpty(templateData.InputMessage))
                    {
                        listValidation.Prompt = templateData.InputMessage;
                    }

                    if (!string.IsNullOrEmpty(templateData.InputTitle))
                    {
                        listValidation.PromptTitle = templateData.InputTitle;
                    }

                    listValidation.AllowBlank = true;
                    listValidation.ShowInputMessage = true;
                    listValidation.ShowErrorMessage = true;
                    listValidation.ErrorStyle = ExcelDataValidationWarningStyle.stop;
                    listValidation.Formula.ExcelFormula = string.Format("={0}", dataSheet.Cells[rowIndex, colIndex, templateData.Data.Count > 0 ? (templateData.Data.Count + (rowIndex - 1)) : rowIndex, colIndex].FullAddressAbsolute);

                    foreach (string value in templateData.Data)
                    {
                        var dataCell = dataSheet.Cells[rowIndex, colIndex];
                        dataCell.Value = value;
                        rowIndex++;
                    }
                }

                // save file
                pck.Save();
                ms.Position = 0;
            }

            return ms;
        }

        public class ImportTemplateViewModel
        {
            public string InputTitle { get; set; }

            public string InputMessage { get; set; }

            public int RowIndex { get; set; }

            public int ColIndex { get; set; }

            public List<string> Data { get; set; }
        }

        public class ImportException : Exception
        {
            public ImportException()
            {

            }

            public ImportException(string message)
                : base(message)
            {

            }
        }
    }
}
