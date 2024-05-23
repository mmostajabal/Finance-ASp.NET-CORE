using AutoMapper;
using FinanceModels;
using FinanceService.Contracts.FinancialItam;
using FinanceShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceService.Mappings
{
    public class Maps :Profile
    {
        public Maps()
        {
            CreateMap<FinancialItems, FinancialItemsDTO>().ReverseMap();
            CreateMap<FinancialItemsDTO, FinancialItems>().ReverseMap();

            CreateMap<Partners, PartnersDTO>().ReverseMap();
            CreateMap<PartnersDTO, Partners>().ReverseMap();

        }

    }
}
