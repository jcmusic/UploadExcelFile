using ExcelDataReader;
using System.Data;
using System.Text;

namespace UploadExcelFile
{
    public static class ExcelDeserializer
    {
        public static List<T> Deserialize<T>(IFormFile formFile) where T : new()
        {
            var excelData = new List<T>();
            using (var stream = formFile.OpenReadStream())
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream, new ExcelReaderConfiguration()
                {
                    FallbackEncoding = Encoding.GetEncoding(1252),
                }))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });
                    var table = result.Tables[0];
                    foreach (DataRow row in table.Rows)
                    {
                        var item = new T();
                        foreach (var prop in typeof(T).GetProperties())
                        {
                            if (table.Columns.Contains(prop.Name))
                            {
                                var value = row[prop.Name];
                                if (value != DBNull.Value)
                                {
                                    switch (prop.PropertyType.Name)
                                    {
                                        case "String":
                                            prop.SetValue(item, value.ToString());
                                            break;
                                        case "Decimal":
                                            prop.SetValue(item, Convert.ToDecimal(value));
                                            break;
                                        case "Int32":
                                            prop.SetValue(item, Convert.ToInt32(value));
                                            break;
                                        case "DateTime":
                                            prop.SetValue(item, Convert.ToDateTime(value));
                                            break;
                                        case "Boolean":
                                            var boolVal = (value.ToString() == "true" || value.ToString() == "yes");
                                            prop.SetValue(item, boolVal);
                                            break;
                                        default:
                                            prop.SetValue(item, value);
                                            break;

                                            // Add switch cases for any additional property types in your model(s)
                                            // Additional data types for special cases
                                            // byte
                                            // sbyte
                                            // char
                                            // double
                                            // float
                                            // long
                                            // uint
                                            // nint
                                            // nuint
                                            // ulong
                                            // short
                                            // ushort
                                    }
                                }
                            }
                        }
                        excelData.Add(item);
                    }
                }
            }
            return excelData;
        }
    }



}

