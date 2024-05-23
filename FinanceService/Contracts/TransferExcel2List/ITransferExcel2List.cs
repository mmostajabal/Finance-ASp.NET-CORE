using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceService.Contracts.TransferExcel2List
{
    public  interface ITransferExcel2List<T> where T : class
    {
        IList<T> TransferExcel2List(string path);
    }
}
