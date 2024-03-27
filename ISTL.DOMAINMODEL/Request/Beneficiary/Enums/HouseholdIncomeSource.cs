using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.Request.Beneficiary.Enums
{
    public enum HouseholdIncomeSource
    {
        NONE = 2, 
        SELLING_OF_FARM = 3, 
        SELLING_OF_NON_FARM = 4, 
        CAUSAL_LABOUR = 5, 
        FORMAL_EMPLOYMENT = 6,
        REMITTANCES = 7, 
        GIFT = 8, 
        FROM_GOVT = 9, 
        FROM_NGO = 10, 
        PENSION = 11, 
        OTHER = 12
    }
}
