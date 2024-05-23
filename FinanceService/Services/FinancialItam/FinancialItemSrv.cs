using FinanceModels;
using FinanceService.Contracts.FinancialItam;
using FinanceShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace FinanceService.Services.FinanceItem
{
    public class FinancialItemSrv : IFinancialItem
    {
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public bool Delete(Guid guid)
        {
            FinancialItemsDTO item = Get(guid);
            if (item != null)
            {
                return GlobalVariables.FINANCIAL_ITEMS_LIST.Remove(Get(guid) ?? new FinancialItemsDTO());
            }

            return false;
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public FinancialItemsDTO Get(Guid guid)
        {
            return GlobalVariables.FINANCIAL_ITEMS_LIST.FirstOrDefault(o=>o.FinancialId == guid);
        }

        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns></returns>
        public IList<FinancialItemsDTO> GetAll()
        {
            return GlobalVariables.FINANCIAL_ITEMS_LIST.ToList();
        }

        /// <summary>
        /// GetFinancialItemsFromFile
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public IList<FinancialItemsDTO> GetFinancialItemsFromFile(string path, string fileName)
        {
            IList<FinancialItemsDTO> financialItemsDTOs = new List<FinancialItemsDTO>();
                        
            return financialItemsDTOs;
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="financialItem"></param>
        /// <returns></returns>
        public FinancialItemsDTO Insert(FinancialItemsDTO financialItem)
        {
            financialItem.FinancialId = Guid.NewGuid();
            GlobalVariables.FINANCIAL_ITEMS_LIST.Add(financialItem);

            return financialItem;
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="financialItem"></param>
        /// <returns></returns>
        public bool Update(FinancialItemsDTO financialItem)
        {
            FinancialItemsDTO item = Get(financialItem.FinancialId);
            if (financialItem != null)
            {
                item.PARTNER_ID = financialItem.PARTNER_ID;
                item.Amount = financialItem.Amount;
                item.Date   = financialItem.Date;
            }

            return true;
        }
    }
}
