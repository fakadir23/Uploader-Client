using ISTL.COMMON;
using ISTL.MODELS.DTO.Auth;
using ISTL.MODELS.DTO.Common;
using ISTL.MODELS.Response;
using ISTL.PERSOGlobals;
using ISTL.RAB.ApiManager;
using ISTL.RAB.DbManager;
using ISTL.RAB.Entity;
using ISTL.RAB.View;
using NLog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.RAB.AppUtils
{
    public class AppUtils
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static void SetTokenByAdminUser()
        {
            if (Users.AccessToken == null)
            {
                DbUserManager dbUserManager = new DbUserManager();
                LoginApiManager loginApiManager = new LoginApiManager();

                var userName = "clientadmin";
                var pass = dbUserManager.GetUserPassword(userName);

                LoginResponse loginResponse = null;

                //ProcessingDialog.Run(delegate ()
                //{
                    try
                    {
                        loginResponse = loginApiManager.Login(userName, pass);
                        if (loginResponse?.operationResult == true)
                        {
                            Users.AccessToken = loginResponse.token;
                        }
                    }
                    catch (WebException x)
                    {
                        logger.Debug("Known error, when server not found.\n" + x.Message);
                    }
                    catch (TimeoutException x)
                    {
                        logger.Debug("Connection timedout! Should attempt to login offline.\n" + x.Message);
                    }
                    catch (Exception x)
                    {
                        logger.Error("There was an unexpected error during login.\n" + x.ToString());
                    }
                //});
            }
        }

        public static DateTime GetDateTimeByStrDate(string strDate)
        {
            DateTime myDate = new DateTime();
            try
            {
                var formatStrings = new string[] { "yyyy-MM-dd", "yyyy-MM-ddTHH:mm:ss.fffZ", "yyyy-MM-ddTHH:mm:ss.fffK" };
                DateTime.TryParseExact(strDate, formatStrings, CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out myDate);
            }
            catch (Exception) { }
            return myDate;
        }

        public static string GetRecordStatus(int? id)
        {
            string status = "N/A";

            if (id == null) return status;

            if (Globals.RecordState.NEW == id)
            {
                status = "NEW";
            }
            else if (Globals.RecordState.DRAFT == id)
            {
                status = "DRAFT";
            }
            else if (Globals.RecordState.EXPORTED == id)
            {
                status = "EXPORTED";
            }
            else if (Globals.RecordState.UPLOADED == id)
            {
                status = "UPLOADED";
            }
            else if (Globals.RecordState.VERIFIED == id)
            {
                status = "VERIFIED";
            }
            return status;
        }

        public static void SetTokenByLoggedInUser()
        {
            if (Users.AccessToken == null)
            {
                DbUserManager dbUserManager = new DbUserManager();
                LoginApiManager loginApiManager = new LoginApiManager();

                var userName = Users.Username;
                var pass = Users.Password;

                LoginResponse loginResponse = null;

                //ProcessingDialog.Run(delegate ()
                //{
                try
                {
                    loginResponse = loginApiManager.Login(userName, pass);
                    if (loginResponse?.operationResult == true)
                    {
                        Users.AccessToken = loginResponse.token;
                    }
                }
                catch (WebException x)
                {
                    logger.Debug("Known error, when server not found.\n" + x.Message);
                }
                catch (TimeoutException x)
                {
                    logger.Debug("Connection timedout! Should attempt to login offline.\n" + x.Message);
                }
                catch (Exception x)
                {
                    logger.Error("There was an unexpected error during login.\n" + x.ToString());
                }
                //});
            }
        }

        public static void SetTokenByLoggedInUserForHeartbeat()
        {
            if (Users.AccessTokenForWorkStation == null)
            {
                DbUserManager dbUserManager = new DbUserManager();
                LoginApiManager loginApiManager = new LoginApiManager();

                var userName = Users.Username;
                var pass = Users.Password;

                LoginResponse loginResponse = null;

                //ProcessingDialog.Run(delegate ()
                //{
                try
                {
                    loginResponse = loginApiManager.Login(userName, pass);
                    if (loginResponse?.operationResult == true)
                    {
                        Users.AccessTokenForWorkStation = loginResponse.token;
                    }
                }
                catch (WebException x)
                {
                    logger.Debug("Known error, when server not found.\n" + x.Message);
                }
                catch (TimeoutException x)
                {
                    logger.Debug("Connection timedout! Should attempt to login offline.\n" + x.Message);
                }
                catch (Exception x)
                {
                    logger.Error("There was an unexpected error during getting token by logged in user for sending heartbeat to server.\n" + x.ToString());
                }
                //});
            }
        }

        public static Image WsqToImage(byte[] wsq)
        {
            if (wsq == null)
            {
                return null;
            }

            Utils.ByteToFile("finger.wsq", wsq);
            Wsqm.WSQ wsqCodec = new Wsqm.WSQ();
            try
            {
                wsqCodec.DecoderFile("finger.wsq", "finger.bmp");
                Bitmap bm = (Bitmap)Bitmap.FromFile("finger.bmp");
                Bitmap newBm = new Bitmap(bm);
                bm.Dispose();
                return newBm;
            }
            catch (Exception ex) 
            {
                logger.Error("Error converting wsq to image. " + ex.ToString());
                return Utils.ByteToImage(wsq);
            }
            finally
            {
                try
                {
                    File.Delete("finger.bmp");
                    File.Delete("finger.wsq");
                }catch(Exception ex) 
                {
                    logger.Error("Error deleting finger.bmp and finger.wsq " + ex.ToString());
                }
                
            }
            /*

            
            try
            {
                int width = 0;
                int height = 0;
                int depth = 0;
                int dpi = 0;
                byte[] rawData = new byte[800 * 800];
                Image finalImage;
                CustomFpEngine.WsqToBmp(wsq, wsq.Length, rawData, ref width, ref height, ref depth, ref dpi);
                finalImage = Utils.ByteToImage(rawData);
                return finalImage;
            }
            catch (Exception ex)
            {
                return null;
            }
            */
        }

        public static List<CodeNameDto> GetRoleList()
        {
            List<CodeNameDto> list = new List<CodeNameDto>();
            list.Add(new CodeNameDto() { Id = Globals.RoleId.Admin, Name = Globals.RoleName.Admin });
            list.Add(new CodeNameDto() { Id = Globals.RoleId.Users, Name = Globals.RoleName.Users });
            list.Add(new CodeNameDto() { Id = Globals.RoleId.Others, Name = Globals.RoleName.Others });
            return list;
        }

        public static bool HasUserAppAccess(List<PermissionDto> list)
        {
            foreach (var obj in list)
            {
                if (obj.permissionId == Globals.PermissionsId.NewEntry)
                {
                    return true;
                }
                if (obj.permissionId == Globals.PermissionsId.SpecialEntry)
                {
                    return true;
                }
                if (obj.permissionId == Globals.PermissionsId.DraftRecords)
                {
                    return true;
                }
                if (obj.permissionId == Globals.PermissionsId.FailedUploads)
                {
                    return true;
                }
                if (obj.permissionId == Globals.PermissionsId.SearchProfile)
                {
                    return true;
                }
                if (obj.permissionId == Globals.PermissionsId.BiometricSearch)
                {
                    return true;
                }

                if (obj.permissionId == Globals.PermissionsId.UserManagement)
                {
                    return true;
                }
                if (obj.permissionId == Globals.PermissionsId.Report)
                {
                    return true;
                }
                if (obj.permissionId == Globals.PermissionsId.Settings)
                {
                    return true;
                }
                if (obj.permissionId == Globals.PermissionsId.Update)
                {
                    return true;
                }
                if (obj.permissionId == Globals.PermissionsId.ProfileEnrollment)
                {
                    return true;
                }
                if (obj.permissionId == Globals.PermissionsId.NotEntry)
                {
                    return true;
                }
            }
            return false;
        }

        public static void SetUserAppAccess(List<PermissionDto> list)
        {
            foreach (var obj in list)
            {
                if (obj.permissionId == Globals.PermissionsId.NewEntry)
                {
                    Users.UserPermission.NewEntry = true;
                }
                if (obj.permissionId == Globals.PermissionsId.SpecialEntry)
                {
                    Users.UserPermission.SpecialEntry = true;
                }
                if (obj.permissionId == Globals.PermissionsId.DraftRecords)
                {
                    Users.UserPermission.DraftRecords = true;
                }
                if (obj.permissionId == Globals.PermissionsId.FailedUploads)
                {
                    Users.UserPermission.FailedUploads = true;
                }
                if (obj.permissionId == Globals.PermissionsId.SearchProfile)
                {
                    Users.UserPermission.SearchProfile = true;
                }
                if (obj.permissionId == Globals.PermissionsId.BiometricSearch)
                {
                    Users.UserPermission.BiometricSearch = true;
                }
                if (obj.permissionId == Globals.PermissionsId.UserManagement)
                {
                    Users.UserPermission.UserManagement = true;
                }
                if (obj.permissionId == Globals.PermissionsId.Settings)
                {
                    Users.UserPermission.Settings = true;
                }
                if (obj.permissionId == Globals.PermissionsId.Report)
                {
                    Users.UserPermission.Report = true;
                }
                if (obj.permissionId == Globals.PermissionsId.Update)
                {
                    Users.UserPermission.Update = true;
                }
                if (obj.permissionId == Globals.PermissionsId.ProfileEnrollment)
                {
                    Users.UserPermission.ProfileManagement = true;
                }
                if (obj.permissionId == Globals.PermissionsId.NotEntry)
                {
                    Users.UserPermission.NotEntry = true;
                }
            }
        }
    }
}
