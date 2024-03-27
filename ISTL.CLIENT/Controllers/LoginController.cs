using System;
using System.Net;
using System.Windows.Forms;
using NLog;
using ISTL.COMMON;
using ISTL.COMMON.Common;
using ISTL.COMMON.Subscription;
using ISTL.COMMON.Threads;
using ISTL.RAB.Entity;
using ISTL.RAB.View;
using System.Text.RegularExpressions;
using ISTL.PERSOGlobals;
using System.Text;
using GlobalsCommon;
using System.Collections.Generic;
using ISTL.RAB.DbManager;
using ISTL.RAB.ApiManager;
using ISTL.MODELS.Response;
using System.Threading.Tasks;
using ISTL.MODELS.DTO.New.Lookup;
using ISTL.RAB.View.New.Home;
using ISTL.MODELS.Request.User;
using ISTL.MODELS.Response.New.Lookup;

namespace ISTL.RAB.Controllers
{
    public class LoginController : ViewController
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private LoginForm loginForm;
        private OnlineSubject onlineStatus;
        private DbUserManager dbUserManager;
        private LoginApiManager loginApiManager;
        private DbLookupManager dbLookupManager;
        private LookupApiManager lookupApiManager;

        public LoginController()
        {
            loginForm = new LoginForm();
            base.SetView((IView)loginForm);
            loginForm.SetController(this);

            loginApiManager = new LoginApiManager();
            dbUserManager = new DbUserManager();

            onlineStatus = (OnlineSubject)SubjectFactory.GetInstance().GetSubject(OnlineSubject.Name);

            dbLookupManager = new DbLookupManager();
            lookupApiManager = new LookupApiManager();
        }

        ~LoginController()
        {
            logger.Debug("Releases unmanaged resources and performs other cleanup operations before the ["
                        + LogManager.GetCurrentClassLogger().Name
                        + "] is reclaimed by garbage collection.");
        }

        public override void OnLoad()
        {
            base.OnLoad();
            InitializeController();
            Users.ClearAll();

            //Test code
            //loginForm.Username = "admin";
            //loginForm.Password = "Secr3t";
        }

        private void InitializeController()
        {
            onlineStatus = (OnlineSubject)SubjectFactory.GetInstance().GetSubject(OnlineSubject.Name);
            loginForm.OnFocus();
        }

        private IList<Language> Cultures
        {
            get
            {
                var cultures = new List<Language>();
                foreach (var obj in GlobalCultures.Cultures)
                {
                    cultures.Add(new Language() { Culture = obj.Key, CultureName = obj.Value });
                }
                return cultures;
            }
        }

        public void OnLogin()
        {
            bool isLoggedin = false;
            bool tryOfflineLogin = false;

            if ((loginForm.Username == null || loginForm.Username.Trim() == "") && (loginForm.Password == null || loginForm.Password.Trim() == ""))
            {
                //MessageBox.Show("Login failed. Please enter username and password.", "RAB CAS Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Login failed. Please enter username and password.");
                return;
            }

            if ((loginForm.Username == null || loginForm.Username.Trim() == "") && (loginForm.Password != null && loginForm.Password.Trim() != ""))
            {
                //MessageBox.Show("Login failed. Please enter username.", "RAB CAS Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Login failed. Please enter username.");
                return;
            }

            if ((loginForm.Password == null || loginForm.Password.Trim() == "") && (loginForm.Username != null || loginForm.Username.Trim() != ""))
            {
                //MessageBox.Show("Login failed. Please enter password.", "RAB CAS Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Login failed. Please enter password.");
                return;
            }

            if (loginForm.DeviceId == null || loginForm.DeviceId.Trim() == "")
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please enter Device Id.");
                return;
            }

            string username = loginForm.Username;
            //string password = GenerateSecureHash.CreateSha1Hash(Utils.StringToByteArray(loginForm.Password));
            string password = loginForm.Password;
            string deviceId = loginForm.DeviceId;
            string fullName = "";
            LoginResponse loginResponse = null;

            Exception handleAuthException = null;
            bool hasUserAppAccess = false;
            bool isUserActivated = true;

            //var un = AesCryptography.EncryptToString("clientadmin");
            //var pass = AesCryptography.EncryptToString("admin135!");

            Users.UserPermission.Init();

            ProcessingDialog.Run(delegate ()
            {
                try
                {
                    loginResponse = loginApiManager.Login(username, password, deviceId);
                    if (loginResponse != null && loginResponse.operationResult == true)
                    {
                        Users.Username = username;
                        Users.Id = loginResponse.id.Value;
                        Users.DeviceId = deviceId;
                        Users.AccessToken = loginResponse.token;

                        ClearUserCashData();

                        try
                        {
                            if (loginResponse.status != null && loginResponse.status == UserStatus.PENDING)
                            {
                                var form = new UserActivationForm();
                                form.request = new UserActivationRequest()
                                {
                                    id = loginResponse.id.Value,
                                    username = username,                                    
                                };

                                DialogResult dr = form.ShowDialog();

                                if(dr == DialogResult.Cancel)
                                {
                                    isUserActivated = false;
                                    return;
                                }
                            }
                        }
                        catch(Exception ex)
                        {
                            logger.Error("There was an unexpected error during user activation.\n" + ex.ToString());
                            isUserActivated = false;
                            return;
                        }

                        try
                        {                           
                            hasUserAppAccess = true;
                        }
                        catch (Exception x)
                        {
                            logger.Error("There was an unexpected error during user role.\n" + x.ToString());
                        }

                        if (hasUserAppAccess)
                        {
                            onlineStatus.IsOnline = true;
                            onlineStatus.Notify();
                            isLoggedin = true;

                            // Set user's permission in the user's static permission properties
                            // AppUtils.AppUtils.SetUserAppAccess(loginResponse?.permissionList);

                            fullName = loginResponse.userName;


                            logger.Info(username + " Has Successfully Logged In Via Web API.");
                            //bool isAddedUser = dbUserManager.AddUserInfo(loginResponse);
                            bool isAddedUser = true;
                            if (isAddedUser == true)
                            {
                                // Add user's permissions
                                // dbUserManager.AddUserPermission(Users.Id, loginResponse?.permissionList);

                                // Add configurations
                                // dbUserManager.AddConfiguration(
                                //    Configuration.DeleteDayCount.ToString(),
                                //    Configuration.MaxUploadPending.ToString(),
                                //    Configuration.NotifyDayCount.ToString(),
                                //    Configuration.VerifyDayCount.ToString(),
                                //    Configuration.ReportDeleteDayCount.ToString(),
                                //    Configuration.NidDeleteDayCount.ToString()
                                //    );

                                logger.Debug("Successfuly Inserted User Info To Local Database.");
                            }
                            else
                            {
                                logger.Info("Failed To Insert User Info To Local Database.");
                                //MessageBox.Show("There was an error saving user info. Please contact your " +
                                //    "administrator.", "RAB CAS Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                CustomMessageBox.ShowMessage("SNSOP TOOLS", "There was an error saving user info. Please contact your administrator");
                            }
                        }

                        //Load lookups in a static property asynchronously
                        //Task.Run(() => LoadLookupList());
                        // LoadLookupList(loginResponse);
                    }
                }
                catch (WebException x)
                {
                    logger.Debug("Known error, when server not found.\n" + x.Message);
                    tryOfflineLogin = true;
                }
                catch (TimeoutException x)
                {
                    logger.Debug("Connection timedout! Should attempt to login offline.\n" + x.Message);
                    tryOfflineLogin = true;
                }
                catch (Exception x)
                {
                    logger.Error("There was an unexpected error during login.\n" + x.ToString());
                    handleAuthException = x;
                }
            });
            
            if (handleAuthException != null)
            {
                ErrorMessageBox.ShowErrorWithWaitDialog("There was an unexpected error when communicating with the server.", handleAuthException);
                logger.Error("There was an unexpected error. ERROR: " + handleAuthException.ToString());
                loginForm.Password = "";
                return;

                //OnAuthExceptions(handleAuthException, username, password);
            }

            if (loginResponse != null && loginResponse.status == UserStatus.INACTIVE)
            {
                //MessageBox.Show("Login failed. This user is Inactive. Please contact with your Administrator.", "RAB CDMS Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Login failed. This user is Inactive. Please contact with your Administrator.");
                logger.Info("Login failed. This user is inactive. Please contact with your Administrator. Username: " + username);
                loginForm.Password = "";
                return;
            }

            if (loginResponse != null && loginResponse.operationResult == false)
            {
                //MessageBox.Show("Login failed. Please enter the correct username and password.", "RAB CDMS Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (!string.IsNullOrEmpty(loginResponse.errorMsg))
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", loginResponse.errorMsg);
                }
                else
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Login failed. Please enter the correct username and password or contact your Administrator.");
                }
                logger.Info("Login failed. Wrong username and password. "+loginResponse.errorMsg);
                loginForm.Password = "";
                return;
            }

            if(!isUserActivated)
            {
                //MessageBox.Show("There was an error during User Activation. Please contact with your Administrator.", "RAB CDMS Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "There was an error during User Activation. Please contact with your Administrator.");
                loginForm.Password = "";
                return;
            }

            // TODO: server should send access-denied code and then show error message and delete this user info on database and return

            if (tryOfflineLogin)
            {
                isLoggedin = OfflineLogin(username, password);
                if (isLoggedin)
                {
                    fullName = dbUserManager.GetUserFullName(username);
                    Users.Id = dbUserManager.GetUserId(username);
                    Users.Unit = dbUserManager.GetOfflineUserUnit(username);
                    Users.SubUnit = dbUserManager.GetOfflineUserSubUnit(username);
                }
            }
            else if (!hasUserAppAccess && handleAuthException == null)
            {
                //MessageBox.Show("You are not authorized to use this application.", "RAB CAS Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "You are not authorized to use this application.");
                loginForm.Password = "";
                return;
            }

            if (isLoggedin == true)
            {
                Users.Username = username;
                Users.Password = password;
                Users.FullName = fullName;
                logger.Info("User: " + username + " is Logged Successfully.");
                logger.Info("Logged In Credentials: Username: " + username);
                // dbUserManager.DeletePreviousAddressData(Configuration.DeleteDayCount);
                loginForm.DialogResult = DialogResult.OK;
            }
        }

        private void ClearUserCashData()
        {
            dbUserManager.DeleteUserPermission(Users.Id);
            dbUserManager.DeleteUser(Users.Username);
        }

        public void LoadLookupList(LoginResponse response)
        {
        }

        public void LoadRabGeoMap(LoginResponse loginResponse)
        {
            //int localVersion = dbLookupManager.GetTableVersion(Globals.LookupTable.RabGeoMap);

            //if (localVersion == loginResponse?.lookupVersion?.rabGeoMap)
            //{
            //    if (dbLookupManager.GetRabGeoMap().Count > 0) return;
            //}

            //if (Convert.ToInt32(loginResponse?.lookupVersion?.rabGeoMap) > 0) 
            //    dbLookupManager.UpdateTableVersion(Globals.LookupTable.RabGeoMap, Convert.ToInt32(loginResponse?.lookupVersion?.rabGeoMap));

            if (dbLookupManager.GetRabGeoMap()?.Count > 0) return;

            List<RabGeoMapDto> response = new List<RabGeoMapDto>();

            ProcessingDialog.Run(delegate ()
            {
                try
                {
                    response = lookupApiManager.GetRabGeoMap();

                    if (response == null) return;

                    if (response.Count > 0)
                    {
                        dbLookupManager.AddRabGeoMap(response);
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex.ToString());
                }
            });
        }

        public void LoadRabDistrict(LoginResponse loginResponse)
        {
            //int localVersion = dbLookupManager.GetTableVersion(Globals.LookupTable.RabDistrict);

            //if (localVersion == loginResponse?.lookupVersion?.rabDistrict)
            //{
            //    if (dbLookupManager.GetRabDistrict().Count > 0) return;
            //}

            //if (Convert.ToInt32(loginResponse?.lookupVersion?.rabDistrict) > 0) 
            //    dbLookupManager.UpdateTableVersion(Globals.LookupTable.RabDistrict, Convert.ToInt32(loginResponse?.lookupVersion?.rabDistrict));

            if (dbLookupManager.GetRabDistrict()?.Count > 0) return;

            List<RabDistrictDto> response = new List<RabDistrictDto>();

            ProcessingDialog.Run(delegate ()
            {
                try
                {
                    response = lookupApiManager.GetAllRabDistrict();

                    if (response == null) return;

                    if (response.Count > 0)
                    {
                        dbLookupManager.AddRabDistrict(response);
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex.ToString());
                }
            });
        }

        public void LoadRabUpazila(LoginResponse loginResponse)
        {
            //int localVersion = dbLookupManager.GetTableVersion(Globals.LookupTable.RabUpazila);

            //if (localVersion == loginResponse?.lookupVersion?.rabUpazila)
            //{
            //    if (dbLookupManager.GetRabUpazila().Count > 0) return;
            //}

            //if (Convert.ToInt32(loginResponse?.lookupVersion?.rabUpazila) > 0)
            //    dbLookupManager.UpdateTableVersion(Globals.LookupTable.RabUpazila, Convert.ToInt32(loginResponse?.lookupVersion?.rabUpazila));

            if (dbLookupManager.GetRabUpazila()?.Count > 0) return;

            List<RabUpazilaDto> response = new List<RabUpazilaDto>();

            ProcessingDialog.Run(delegate ()
            {
                try
                {
                    response = lookupApiManager.GetAllRabUpazila();

                    if (response == null) return;

                    if (response.Count > 0)
                    {
                        dbLookupManager.AddRabUpazila(response);
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex.ToString());
                }
            });
        }

        public bool OfflineLogin(string username, string password)
        {
            onlineStatus.IsOnline = false;
            onlineStatus.Notify();
            Users.Username = username;

            try
            {
                string userNameCapitalized = username?.ToUpper();
                bool authenticated = dbUserManager.CheckUser(AesCryptography.EncryptToString(userNameCapitalized), AesCryptography.EncryptToString(password));
                //bool authenticated = dbUserManager.CheckUser(AesCryptography.EncryptToString(username), AesCryptography.EncryptToString(password));
                if (authenticated == true)
                {
                    var userInfo = dbUserManager.GetUserInfo(userNameCapitalized);
                    //var userInfo = dbUserManager.GetUserInfo(username);
                    var userPermissionList = dbUserManager.GetUserPermissions(userInfo.id.Value, userNameCapitalized);
                    //var userPermissionList = dbUserManager.GetUserPermissions(userInfo.id.Value, username);
                    bool hasUserAppAccess = AppUtils.AppUtils.HasUserAppAccess(userPermissionList);

                    // Check user activated flag and show modal if user is not activated

                    // Check user status and show error message if user is disabled

                    if (!hasUserAppAccess)
                    {
                        //MessageBox.Show("You are not authorized to use this application.", "RAB CAS Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        CustomMessageBox.ShowMessage("SNSOP TOOLS", "You are not authorized to access this application.");
                        loginForm.Password = "";
                        return false;
                    }

                    // Set user's permission in the user's static permission properties
                    AppUtils.AppUtils.SetUserAppAccess(userPermissionList);

                    // Set configurations in the configuration's static properties
                    dbUserManager.GetConfiguration();

                    logger.Info(username + " is Authenticated at Offline Mode.");
                    return true;
                }
                else
                {
                    logger.Info(username + " is not Authenticated at Offline Mode.");
                    //MessageBox.Show("You are not online. Tried to login with offline mode, " +
                    //    "but authentication failed.\n\nIf this is the first time you are attempting " +
                    //    "to login, then you cannot continue with offline mode. If this is not your " +
                    //    "first time, please enter the correct user name and password.", "RAB CAS Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Login failed. You appear to be offline." +
                        "\nPlease make sure you have logged in on online mode at first time or" +
                        " provide correct username and password.");

                    loginForm.Password = "";
                }
            }
            catch (Exception x)
            {
                logger.Error("Local Db Error: " + x.ToString());
                ErrorMessageBox.ShowErrorWithWaitDialog("There was a database error during offline authentication.", x);
            }

            return false;
        }

        public void OnAuthExceptions(Exception x, string username, string password)
        {
            switch (x.Message)
            {
                case Globals.ApiExceptionCode.AUTH_FAILED:
                    MessageBox.Show("Login failed. Please enter the correct username and password.", "RAB CAS Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    logger.Info("Login failed. Wrong username and password.");

                    // Reset password field
                    loginForm.Password = "";
                    break;
                case Globals.ApiExceptionCode.AUTH_DENIED:
                    dbUserManager.DeleteUser(username);
                    dbUserManager.DeleteConfiguration();
                    dbUserManager.DeleteRegions(username);
                    MessageBox.Show("Sorry, your access has been denied.", "RAB CAS Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    logger.Info(username + "'s access has been denied.");
                    loginForm.Password = "";
                    break;
                case Globals.ApiExceptionCode.NO_PRIVILEGE:
                case Globals.ApiExceptionCode.USER_TRESPASS:
                case Globals.ApiExceptionCode.REGION_TRESPASS:
                    MessageBox.Show("Login failed. You do not have sufficient priviledges.", "RAB CAS Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    logger.Info("Login failed. ERROR: " + x.Message);
                    loginForm.Password = "";
                    break;
                case Globals.ApiExceptionCode.INCOMPLETE_REQUEST:
                    ErrorMessageBox.ShowErrorWithWaitDialog("There was an error when communicating with the server.", x);
                    logger.Error("Login failed. ERROR: " + x.ToString());
                    loginForm.Password = "";
                    break;
                default:
                    ErrorMessageBox.ShowErrorWithWaitDialog("There was an unexpected error when communicating with the server.", x);
                    logger.Error("There was an unexpected error. ERROR: " + x.ToString());
                    loginForm.Password = "";
                    break;
            }
        }

        private void GetUserMethodRoles()
        {
            DbUserManager dbUserManager = new DbUserManager();
            List<long> roleList = dbUserManager.GetUserRoles(Users.Username);
            foreach (long roleId in roleList)
            {
                // For setting User Methods ROles
                switch (roleId)
                {
                    case Globals.MethodRole.ENROLLMENT:
                        Users.UserPermission.NewEntry = true;
                        break;
                    default:
                        logger.Debug("Specified Role Not Found.");
                        break;
                }
            }
        }

        public void OnExit()
        {
            loginForm.DialogResult = DialogResult.Cancel;
        }
    }
}
