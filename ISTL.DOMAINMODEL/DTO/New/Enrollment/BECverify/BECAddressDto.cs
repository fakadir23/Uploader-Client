using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.DTO.New.Enrollment.BECverify
{
    public class BECAddressDto
    {
        public string additionalMouzaOrMoholla { get; set; }
        public string additionalVillageOrRoad { get; set; }
        public string cityCorporationOrMunicipality { get; set; }
        public string district { get; set; }
        public string division { get; set; }
        public string homeOrHoldingNo { get; set; }
        public string mouzaOrMoholla { get; set; }
        public string postOffice { get; set; }
        public string postalCode { get; set; }
        public string rmo { get; set; }
        public string unionOrWard { get; set; }
        public string upozila { get; set; }
        public string villageOrRoad { get; set; }
        public string voterArea { get; set; }
        public int? wardForUnionPorishod { get; set; }
    }
}
