using ExcelDataReader;
using FinanceService.Contracts.TransferExcel2List;
using FinanceShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace FinanceService.Services.TransferExcel2List
{

    public  class TransferExcel2ListSrv<T> : ITransferExcel2List<T> where T : class, new()
    {

        public static T GetObject() 
        {
            return new T();
        }
        /// <summary>
        /// TransferExcel2List
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public IList<T> TransferExcel2List(string path)
        {
            IList<T> values = new List<T>();
            T curObj;
            PropertyInfo prop;
            Type type;

            string[] columnsHeader;
            using (var stream = System.IO.File.Open(path, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    reader.Read();
                    columnsHeader = new string[reader.FieldCount];
                    for (int column = 0; column < reader.FieldCount; column++)
                    {                        
                        columnsHeader[column]= Convert.ToString(reader.GetValue(column));
                    }

                    do
                    {
                        while (reader.Read())
                        {
                            
                            curObj = GetObject();
                            type = curObj.GetType();

                            for (int column = 0; column < reader.FieldCount; column++)
                            {
                                prop = type.GetProperty(columnsHeader[column]);

                                prop.SetValue(curObj, Convert.ChangeType(reader.GetValue(column), Type.GetType(prop.PropertyType.FullName)));

                            }
                            values.Add(curObj);
                        }
                    } while (reader.NextResult());

                }
            }

            return values;
        }
    }

}
