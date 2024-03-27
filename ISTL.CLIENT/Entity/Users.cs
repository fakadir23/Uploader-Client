using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISTL.RAB.Entity
{
    public class Users
    {
        private static int id;
        private static string username;
        private static string password;
        private static string fullName;
        private static string accessToken;
        private static int? unit;
        private static int? subunit;
        public static int UserType { get; set; }
        public static int RoleId { get; set; }
        public static string WorkStationCode { get; set; }
        public static string AccessTokenForWorkStation { get; set; }
        public static string deviceId { get; set; }
        public struct UserPermission
        {
            public static bool NewEntry = false;
            public static bool SpecialEntry = false;
            public static bool DraftRecords = false;
            public static bool FailedUploads = false;
            public static bool SearchProfile = false;
            public static bool BiometricSearch = false;

            public static bool UserManagement = false;
            public static bool Settings = false;
            public static bool Report = false;
            public static bool Update = false;
            public static bool ProfileManagement = false;
            public static bool NotEntry = false;
            public static void Init()
            {
                NewEntry = false;
                SpecialEntry = false;
                DraftRecords = false;
                FailedUploads = false;
                SearchProfile = false;
                BiometricSearch = false;
                UserManagement = false;
                Settings = false;
                Report = false;
                Update = false;
                ProfileManagement = false;
                NotEntry = false;
                AccessTokenForWorkStation = null;
            }
        }

        public static void ClearAll()
        {
            username = null;
            password = null;
            fullName = null;
            accessToken = null;
            id = 0;
            WorkStationCode = null;
            UserType = 0;
            RoleId = 0;
            UserPermission.Init();
        }

        public static string Username
        {
            get { return Users.username; }
            set { Users.username = value; }
        }

        public static string Password
        {
            get { return Users.password; }
            set { Users.password = value; }
        }

        public static string FullName
        {
            get { return Users.fullName; }
            set { Users.fullName = value; }
        }

        public static string AccessToken
        {
            get { return accessToken; }
            set { accessToken = value; }
        }

        public static int Id
        {
            get { return id; }
            set { id = value; }
        }

        public static int Unit
        {
            get { return Convert.ToInt32(unit); }
            set { unit = value; }
        }
        public static int SubUnit
        {
            get { return Convert.ToInt32(subunit); }
            set { subunit = value; }
        }
        public static string DeviceId
        {
            get { return deviceId; }
            set { deviceId = value; }
        }
    }
}
