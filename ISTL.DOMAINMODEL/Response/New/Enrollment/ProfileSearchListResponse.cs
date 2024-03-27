using System.Collections.Generic;
using ISTL.MODELS.Common;
using ISTL.MODELS.DTO.New.Enrollment;

namespace ISTL.MODELS.Response.New.Enrollment
{
    public class ProfileSearchListResponse : ApiResponse
    {     
        //public List<ProfileDto> profileList { get; set; }
        public List<ProfileResponseDto> profileList { get; set; }
        public int? total { get; set; }
    }
}