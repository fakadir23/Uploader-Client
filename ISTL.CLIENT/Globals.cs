using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ISTL.RAB.Entity;

namespace ISTL.PERSOGlobals
{
    public struct Globals
    {
        public struct Logging
        {
            public static NLog.LogLevel DEFAULT_LEVEL = NLog.LogLevel.Debug;
            public const string FILE = "rab_cdms.log";
        }

        public struct Assembly
        {
            public static string EXE_RAB_CDMS;
            public static string COMMON_DLL_VER;
            public const string UPDATE_URL = "http://istldl/istl/rab-cdms-client/";
        }
        public struct Database
        {
            public const string NAME = "snsop_tools.db";
            public const string EXTERNAL_NAME = NAME;
            public const string PASSWORD = "snsop_tools";
            public static string NAME_MIG = "migscript.db";
            public const string PASSWORD_MIG = "snsop_tools";
        }

        public struct CustomMessage
        {
            public const string ErrorTitle = "RAB CDMS Error";
            public const string ErrorMessage = "You are not authorized to access this feature. Please contact with your Administrator.";
        }

        public struct Commands
        {
            public const string SAVE = "Save";
            public const string CANCEL = "Cancel";
            public const string TAKE = "Take";
        }
        public struct UserType
        {
            public const int Admin = 1;
            public const int NotAdmin = 2;
        }

        public struct RoleName
        {
            public const string Admin = "Admin";
            public const string Users = "Users";
            public const string Others = "Others";
        }

        public struct RoleId
        {
            public const int Admin = 1;
            public const int Users = 2;
            public const int Others = 99;
        }

        public struct PermissionsId
        {
            public const int NewEntry = 11;
            public const int SpecialEntry = 12;
            public const int DraftRecords = 13;
            public const int FailedUploads = 14;
            public const int SearchProfile = 15;
            public const int BiometricSearch = 16;
            public const int UserManagement = 17;
            public const int Report = 18;
            public const int Settings = 19;
            public const int Update = 20;
            public const int ProfileEnrollment = 23;
            public const int NotEntry = 24;
        }

        public struct AppType
        {
            public const int Web = 1;
            public const int Client = 2;
        }

        public struct UserStatus
        {
            public const bool Active = true;
            public const bool Inactive = false;
        }

        public struct UserActivationStatus
        {
            public const bool Activated = true;
            public const bool NotActivated = false;
        }

        public struct SearchCriteriaBeforeEnrollment
        {
            public const int CDMS_Search = 1;
            public const int BEC_Search = 2;
            public const int JailDB_Search = 3;
        }

        public struct ReportExtension
        {
            public const string PDF = "pdf";
            public const string EXCEL = "xlsx";
        }

        public struct Device
        {
            public struct Cam
            {
                public const string NAME = "Cam_Status";
                public static string[] Type = { "Fingerprint Reader" };
                public static string[] SupportedModels = { "M31x" };
            }
            public struct FP
            {
                public const string NAME = "FP_Status";
                public static string[] Type = { "Iris" };
                public static string[] SupportedModels = { "C910", "C920" };
            }
            public struct Iris
            {
                //
                /// <summary>
                /// Unfortunately the Wacom signature pad does not show up with a brand name or model,
                /// so using a generic name "HID-compliant device". This can give false detections.
                /// </summary>
                public const string NAME = "Iris_Status";
                public static string[] Type = null;
                public static string[] SupportedModels = null;
            }
        }

        public struct DeviceCategory
        {
            public const string CAM = "CAM";
            public const string FP = "FP";
            public const string IRIS = "IRIS";
        }

        public struct DeviceSubject
        {
            public const string CAM = "CAM_SUBJECT";
            public const string FP = "FP_SUBJECT";
            public const string IRIS = "IRIS_SUBJECT";
        }

        public struct ChildControllers
        {
            public const string DEFAULT = "DEFAULT";
            public const string ENROLL = "ENROLL";
            public const string OTHER_INFO = "OTHER_INFO";
            public const string MANAGE = "MANAGE";
            public const string MATCH = "MATCH";
            public const string ENROLL_MATCH = "ENROLL_MATCH";
            public const string BIOMETRIC = "BIOMETRIC";
            public const string DIAGNOSE = "DIAGNOSE";
            public const string SEARCH_CRIMINAL = "SEARCH_CRIMINAL";
            public const string BIOMETRIC_SEARCH = "BIOMETRIC_SEARCH";
            public const string REPORT = "REPORT";
            public const string DAILY_ENROLLMENT_REPORT = "DAILY_ENROLLMENT_REPORT";
            public const string SUMMARY_REPORT = "SUMMARY_REPORT";
            public const string BATTALION_SETTINGS = "BATTALION_SETTINGS";
            public const string SERVICE_SETTINGS = "SERVICE_SETTINGS";
            public const string USER_MANAGEMENT = "USER_MANAGEMENT";
            public const string PREVIEW_SUBMIT = "PREVIEW_SUBMIT";
            public const string DRAFT_LIST = "DRAFT_LIST";
            public const string FAILED_UPLOAD = "FAILED_UPLOAD";
            public const string UPLOAD_PENDING = "UPLOAD_PENDING";
            public const string ENROLLED_TODAY = "ENROLLED_TODAY";
            public const string MATCH_RESULTS = "MATCH_RESULTS";
            public const string SPECIAL_ENTRY = "SPECIAL_ENTRY";
            public const string SPECIAL_COUNT = "SPECIAL_COUNT";
            public const string SPECIAL_SEARCH_PROFILE = "SPECIAL_SEARCH_PROFILE";
            public const string SPECIAL_DRAFT = "SPECIAL_DRAFT";
            public const string FAILED_UPLOAD_SPECIAL = "FAILED_UPLOAD_SPECIAL";
            public const string UPLOAD_PENDING_SPECIAL = "UPLOAD_PENDING_SPECIAL";
            public const string NOT_ENTRY = "NOT_ENTRY";
            public const string NOT_ENTRY_BIOMETRIC = "NOT_ENTRY_BIOMETRIC";
            public const string NOT_ENTRY_SUBMIT_PREVIEW = "NOT_ENTRY_SUBMIT_PREVIEW";
            public const string SEARCH_NOT_ENTRY_PROFILE = "SEARCH_NOT_ENTRY_PROFILE";
            public const string NOT_ENTRY_UPLOAD_PENDING = "NOT_ENTRY_UPLOAD_PENDING";
            public const string NOT_ENTRY_FAILED_UPLOAD = "NOT_ENTRY_FAILED_UPLOAD";
            public const string PROFILE_MANAGEMENT = "PROFILE_MANAGEMENT";
        }
        public struct Common
        {
            public const int RETURN_CARRIAGE_CODE = 13;
            public const string COUNTER_NAME = "counter_name";
            public const string COUNTER_PENDING_NAME = "counter_pending_name";
            public const string COUNTER_ERROR_NAME = "counter_error_name";
            public const string COUNTER_DRAFT_NAME = "counter_draft_name";
        }

        public struct Biometric
        {
            public const sbyte RIGHT_THUMB = 1;
            public const sbyte RIGHT_INDEX = 2;
            public const sbyte RIGHT_MIDDLE = 3;
            public const sbyte RIGHT_RING = 4;
            public const sbyte RIGHT_SMALL = 5;
            public const sbyte LEFT_THUMB = 6;
            public const sbyte LEFT_INDEX = 7;
            public const sbyte LEFT_MIDDLE = 8;
            public const sbyte LEFT_RING = 9;
            public const sbyte LEFT_SMALL = 10;
        }

        public struct RecordState
        {
            public const int DRAFT = 0;
            public const int NEW = 1;
            public const int EXPORTED = 2;
            public const int UPLOADED = 3;
            public const int VERIFIED = 4;
        }

        public struct ErrorState
        {
            public const int NOT_ERROR = 0;
            public const int ERROR = 1;
        }

        public struct AttachmentType
        {
            public const int COMPLAIN = 0;
            public const int FIR = 1;
            public const int SEIZURE = 2;
        }

        public struct LookupTable
        {
            public const string Nationality = "nationality";
            public const string District = "district";
            public const string Upazilla = "upazilla";
            public const string Eunion = "eunion";
            public const string Station = "station";
            public const string Sub_Station = "sub_station";
            public const string RabGeoMap = "rab_geo_map";
            public const string RabDistrict = "rab_district";
            public const string RabUpazila = "rab_upazila";
            public const string RecoveryLookup = "recovery_lookup";
            public const string CrimeType = "crime_type";
        }

        public struct ApiExceptionCode
        {
            public const String AUTH_FAILED = "AUTH-FAILED";
            public const String AUTH_DENIED = "AUTH-DENIED";
            public const String NO_PRIVILEGE = "NO-PRIVILEGE";
            public const String ERROR = "RAB-CAS-ERROR";
            public const String REGION_TRESPASS = "REGION-TRESPASS";
            public const String USER_TRESPASS = "USER-TRESPASS";
            public const String INCOMPLETE_REQUEST = "INCOMPLETE-REQUEST";
        }

        public struct MethodRole
        {
            public const long ENROLLMENT = 2;
        }
    }
}
