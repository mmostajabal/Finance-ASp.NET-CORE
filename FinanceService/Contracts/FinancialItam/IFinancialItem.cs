
using FinanceShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceService.Contracts.FinancialItam
{
    public interface IFinancialItem
    {

        IList<FinancialItemsDTO> GetFinancialItemsFromFile(string path, string fileName);
        FinancialItemsDTO Insert(FinancialItemsDTO financialItem);

        bool Update(FinancialItemsDTO financialItem);

        bool Delete(Guid guid);

        IList<FinancialItemsDTO> GetAll();

        FinancialItemsDTO Get(Guid guid);


    }
}
