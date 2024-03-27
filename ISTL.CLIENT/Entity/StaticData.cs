using ISTL.MODELS.DTO.New.Enrollment;
using ISTL.MODELS.DTO.New.Enrollment.BECverify;
using ISTL.MODELS.DTO.New.NotEntry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.RAB.Entity
{
    public class StaticData
    {
        public static EnrollmentDto Enrollment = new EnrollmentDto();
        public static EnrollmentDto PreviewEnrollment = new EnrollmentDto();
        public static SpecialEnrollmentDto specialEnrollment = new SpecialEnrollmentDto();
        public static bool ModifiableNormalEnrollment = false;
        public static bool ModifiableSpecialEnrollment = false;
        public static bool firPending = false;
        public static bool dataWithBio = false;
        public static int? StaticFIRUploadPendingCount = -1;
        public static int? StaticEnrolledWithBiometicCount = -1;
        public static bool MoveFromBiometricEntryPageWithoutCDMSsearch = false;
        public static List<BECvoterInfoDto> VoterInfos = null;
        public static int SearchByFpPhotoIris = -1;
        public static NotEntryDto NotEntry = new NotEntryDto();
        public static bool ModifiableNotEntry = false;
    }
}
