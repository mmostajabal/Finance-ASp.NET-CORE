using FinanceShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceService.Contracts.Partner
{
    public interface IPartner
    {
        public List<PartnersDTO> CreatePartner(int numberPartner, int numberChild, int minMemberInSet, int minFeePercent, int maxFeePercent);

        public List<PartnersDTO> GetPartnerMember();

        public List<PartnersDTO> GetAllPartner();

        public List<PartnersDTO> GetPartnerParents();
    }
}
