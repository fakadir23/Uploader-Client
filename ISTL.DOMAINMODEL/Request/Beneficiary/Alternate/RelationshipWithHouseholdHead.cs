using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.Request.Beneficiary.Alternate
{
    public enum RelationshipWithHouseholdHead
    {
        HOUSEHOLD_HEAD = 2, 
        SPOUSE = 3, 
        SON_DAUGHTER = 4, 
        SON_DAUGHTER_IN_LAW = 5, 
        GRANDCHILD = 6,
        PARENT = 7, 
        PARENT_IN_LAW = 8, 
        SIBLING = 9, 
        SIBLING_IN_LAW = 10, 
        OTHER = 11, 
        DOMESTIC_WORKER = 12,
        NO_RELATION = 13, 
        UNKNOWN = 14
    }
}
