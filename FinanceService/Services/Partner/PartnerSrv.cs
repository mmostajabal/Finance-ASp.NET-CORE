using FinanceModels;
using FinanceService.Contracts.Partner;
using FinanceShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceService.Services.Partner
{
    public class PartnerSrv : IPartner
    {
        /// <summary>
        /// CreatePartner
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<PartnersDTO> CreatePartner(int numberPartner, int numberChild, int minMemberInSet = 3, int minFeePercent = 1, int maxFeePercent = 20)
        {
            PartnersDTO partnersDTO;
            List<PartnersDTO> partners = new List<PartnersDTO>();
            List<Decimal> partnerId = new List<Decimal>();
            List<Decimal> FeePercent = new List<Decimal>();

            for (int partnerInd = 1; partnerInd <= numberPartner; partnerInd++)
            {
                partnersDTO = new PartnersDTO();

                partnersDTO.Partner_Id = CreatePartnerId(partners);
                partnersDTO.Parent_Partner_Id = 0;
                partnersDTO.Fee_Percent = GlobalMethod.GetRandom(minFeePercent, maxFeePercent, maxFeePercent);

                partners.Add(partnersDTO);
            }

            //  GeneratePartnerId
            GeneratePartnerId(partnerId, partners, numberChild);

            //  GenerateFeePercent
            GenerateFeePercent(FeePercent, numberChild);

            List<List<decimal>> setOfPaernetId = CreateUniqueRandomSets(partnerId, minMemberInSet, numberPartner);
            int rearFeePercent = 0;
            for (int ind = 0; ind < numberPartner; ind++)
            {
                for (int childInd = 0; childInd < setOfPaernetId[ind].Count; childInd++)
                {
                    partnersDTO = new PartnersDTO();

                    partnersDTO.Partner_Id = setOfPaernetId[ind][childInd];
                    partnersDTO.Parent_Partner_Id = partners[ind].Partner_Id;
                    partnersDTO.Fee_Percent = FeePercent[rearFeePercent];

                    partners.Add(partnersDTO);

                    rearFeePercent++;
                }
            }

            GlobalVariables.PARTNER_LIST = partners;
            return partners;
        }


        /// <summary>
        /// GeneratePartnerId
        /// </summary>
        /// <param name="partnerId"></param>
        /// <param name="partners"></param>
        /// <param name="numberPartnerId"></param>
        private void GeneratePartnerId(List<Decimal> partnerId, List<PartnersDTO> partners, int numberPartnerId)
        {
            Random rand = new Random();
            decimal randomPartnerId;
            for (int i = 0; i < numberPartnerId; i++)
            {
                while (true)
                {
                    randomPartnerId = rand.Next();
                    if (!partners.Any(c => c.Partner_Id == randomPartnerId) && !partnerId.Any(o => o == randomPartnerId)) break;
                }

                partnerId.Add(randomPartnerId);
            }

            return;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="feePercent"></param>
        /// <param name="numberFeePercent"></param>
        private void GenerateFeePercent(List<decimal> feePercent, int numberFeePercent)
        {

            for (int i = 0; i < numberFeePercent; i++)
            {
                feePercent.Add(GlobalMethod.GetRandom(1, 20, 20));
            }

            return;
        }

        /// <summary>
        /// CreatePartnerId
        /// </summary>
        /// <param name="partners"></param>
        /// <returns></returns>
        private decimal CreatePartnerId(List<PartnersDTO> partners)
        {
            Random rand = new Random();
            decimal partnerId;
            while (true)
            {
                partnerId = rand.Next();
                if (!partners.Any(c => c.Partner_Id == partnerId)) break;
            }

            return partnerId;
        }
        /// <summary>
        /// CreateUniqueRandomSets
        /// </summary>
        /// <param name="list"></param>
        /// <param name="minCount"></param>
        /// <param name="numberOfSets"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static List<List<decimal>> CreateUniqueRandomSets(List<decimal> list, int minCount, int numberOfSets)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (list.Count < minCount * numberOfSets)
                throw new ArgumentException($"The list does not contain enough elements to create {numberOfSets} unique sets with at least {minCount} elements each.", nameof(list));

            List<List<decimal>> randomSets = new List<List<decimal>>();
            Random rand = new Random();
            var availableNumbers = new List<decimal>(list);

            for (int i = 0; i < numberOfSets; i++)
            {
                // Shuffle the available numbers
                availableNumbers = availableNumbers.OrderBy(x => rand.Next()).ToList();

                // Select a random number of elements between minCount and the remaining available numbers count / remaining sets
                int maxCount = availableNumbers.Count / (numberOfSets - i);
                int count = rand.Next(minCount, maxCount + 1);

                // Add the selected elements to the random sets list
                var selectedSet = availableNumbers.Take(count).ToList();
                randomSets.Add(selectedSet);

                // Remove the selected elements from the available numbers
                availableNumbers = availableNumbers.Skip(count).ToList();
            }

            return randomSets;
        }

        public List<PartnersDTO> GetPartnerMember()
        {
            return GlobalVariables.PARTNER_LIST.Where(o => o.Parent_Partner_Id != 0).ToList();
        }

        public List<PartnersDTO> GetAllPartner()
        {
            return GlobalVariables.PARTNER_LIST.ToList();
        }

        public List<PartnersDTO> GetPartnerParents()
        {
            return GlobalVariables.PARTNER_LIST.Where(o => o.Parent_Partner_Id == 0).ToList();
        }

    }
}
