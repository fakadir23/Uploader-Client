using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ISTL.COMMON;
using NLog;
using ISTL.RAB.DbManager;
using ISTL.MODELS.DTO.New.Lookup;
using ISTL.MODELS.DTO.Common;
using ISTL.MODELS.Request.User;
using ISTL.MODELS.DTO.Auth;
using ISTL.RAB.Entity;
using ISTL.MODELS.Common;
using System.Net;
using ISTL.RAB.ApiManager;
using ISTL.RAB.Controllers;
using ISTL.MODELS.Response.User;
using ISTL.PERSOGlobals;
using ISTL.RAB.Controllers.New.Home;

namespace ISTL.RAB.View.New
{
    public partial class UserManagementUserControl : ViewUserControl
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private int? selStationId;
        private int? selSubStationId;
        private int? selRoleId;
        private DbLookupManager dbLookupManager;
        private UserApiManager userApiManager;
        private int position = 0;
        private int total = 0;
        private int limit = 10;
        private UserSearchCriteriaRequest request;

        public UserManagementUserControl()
        {
            InitializeComponent();
            dbLookupManager = new DbLookupManager();
            userApiManager = new UserApiManager();
            request = new UserSearchCriteriaRequest();
        }

        protected override void OnLoad()
        {
            request.startIndex = position;
            request.limit = limit;

            base.OnLoad();
            InitUserControl();
            SetPermissionIds();
            LoadUnit();
            LoadRole();
            LoadUsers();
        }

        private void InitUserControl()
        {
            tbUserId.Tag = 0;
        }

        private void SetPermissionIds()
        {
            chkNewEntry.Tag = Globals.PermissionsId.NewEntry;
            chkSpecialEntry.Tag = Globals.PermissionsId.SpecialEntry;
            chkDraftRecords.Tag = Globals.PermissionsId.DraftRecords;
            chkFailedUpload.Tag = Globals.PermissionsId.FailedUploads;
            chkSearchProfile.Tag = Globals.PermissionsId.SearchProfile;
            chkBiometricSearch.Tag = Globals.PermissionsId.BiometricSearch;
            chkUserManagement.Tag = Globals.PermissionsId.UserManagement;
            chkReport.Tag = Globals.PermissionsId.Report;
            chkSettings.Tag = Globals.PermissionsId.Settings;
            chkUpdate.Tag = Globals.PermissionsId.Update;
            chkProfileEnrollment.Tag = Globals.PermissionsId.ProfileEnrollment;
            chkNotEntry.Tag = Globals.PermissionsId.NotEntry;
        }

        public object Unit
        {
            get { return this.cmbUnit.SelectedItem; }
            set
            {
                try
                {
                    if (value != null)
                    {
                        this.cmbUnit.DataSource = value;
                        this.cmbUnit.DisplayMember = "nameEn";
                        this.cmbUnit.SelectedIndex = -1;

                        var obj = (value as IList<StationDto>).Where(p => p.id.Equals(selStationId)).SingleOrDefault();
                        if (obj != null)
                        {
                            this.cmbUnit.SelectedItem = obj;
                        }
                    }
                }
                catch (Exception x)
                {
                    logger.Error("An error has occurred during setting unit value of Camera in combobox.\n", x.ToString());
                }
            }
        }

        public object SubUnit
        {
            get { return this.cmbSubUnit.SelectedItem; }
            set
            {
                try
                {
                    if (value != null)
                    {
                        this.cmbSubUnit.DataSource = value;
                        this.cmbSubUnit.DisplayMember = "nameEn";
                        this.cmbSubUnit.SelectedIndex = -1;

                        var obj = (value as IList<SubStationDto>).Where(p => p.id.Equals(selSubStationId)).SingleOrDefault();
                        if (obj != null)
                        {
                            this.cmbSubUnit.SelectedItem = obj;
                        }
                    }
                }
                catch (Exception x)
                {
                    logger.Error("An error has occurred during setting device value of sub-unit in combobox.\n", x.ToString());
                }
            }
        }

        public object Role
        {
            get { return this.cmbRole.SelectedItem; }
            set
            {
                try
                {
                    if (value != null)
                    {
                        this.cmbRole.DataSource = value;
                        this.cmbRole.DisplayMember = "Name";
                        this.cmbRole.SelectedIndex = -1;

                        var obj = (value as IList<CodeNameDto>).Where(p => p.Id.Equals(selRoleId)).SingleOrDefault();
                        if (obj != null)
                        {
                            this.cmbRole.SelectedItem = obj;
                        }
                    }
                }
                catch (Exception x)
                {
                    logger.Error("An error has occurred during setting device value of role in combobox.\n", x.ToString());
                }
            }
        }

        private bool userAccountStatus = true;

        private void ResetData()
        {
            ClearData();
            userAccountStatus = true;
            EnableDisableSelectAll(true);
            EnableDisablePrivileges(true);
            CheckUncheckPrivileges(false);
            dgUserList.Rows.Clear();

            UserEmail = null;
            UserPhone = null;
            //if (dgUserList.Rows.Count > 0)
            //{
            //    dgUserList.Rows[0].Selected = true;
            //}
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ResetData();
        }

        private void LoadUnit()
        {
            this.Unit = dbLookupManager.GetStation();
        }

        private void LoadSubUnit(int id)
        {
            this.SubUnit = dbLookupManager.GetSubStationByStationId(id);
        }

        private void LoadRole()
        {
            this.Role = AppUtils.AppUtils.GetRoleList();
        }

        private void ClearData()
        {
            tbUserId.Tag = 0;
            tbUserId.Enabled = true;
            tbPassword.Enabled = true;
            tbConfirmPassword.Enabled = true;
            cmbUnit.SelectedIndex = -1;
            cmbUnit.Hint = "Unit";
            cmbSubUnit.SelectedIndex = -1;
            cmbSubUnit.Hint = "Sub Unit";
            SubUnit = new List<SubStationDto>();
            tbUserId.Text = "";
            tbName.Text = "";
            tbPassword.Text = "";
            tbConfirmPassword.Text = "";
            cmbRole.SelectedIndex = -1;
            cmbRole.Hint = "Role";
            chkSelectAll.Checked = false;
        }

        private void CheckUncheckSelectAll(bool flag)
        {
            chkSelectAll.Checked = flag;
        }

        private void CheckUncheckPrivileges(bool flag)
        {
            chkNewEntry.Checked = flag;
            chkSpecialEntry.Checked = flag;
            chkDraftRecords.Checked = flag;
            chkFailedUpload.Checked = flag;
            chkSearchProfile.Checked = flag;
            chkBiometricSearch.Checked = flag;
            chkUserManagement.Checked = flag;
            chkReport.Checked = flag;
            chkSettings.Checked = flag;
            chkUpdate.Checked = flag;
            chkProfileEnrollment.Checked = flag;
            chkNotEntry.Checked = flag;
        }

        private void EnableDisablePrivileges(bool flag)
        {
            chkNewEntry.Enabled = flag;
            chkFailedUpload.Enabled = flag;
            chkSpecialEntry.Enabled = flag;
            chkSpecialEntry.Enabled = flag;
            chkSearchProfile.Enabled = flag;
            chkDraftRecords.Enabled = flag;
            chkBiometricSearch.Enabled = flag;
            chkUserManagement.Enabled = flag;
            chkReport.Enabled = flag;
            chkSettings.Enabled = flag;
            chkUpdate.Enabled = flag;
            chkProfileEnrollment.Enabled = flag;
            chkNotEntry.Enabled = flag;
        }

        private void EnableDisableSelectAll(bool flag)
        {
            chkSelectAll.Enabled = flag;
        }

        private bool IsPrivilegeSelected()
        {
            if (!chkNewEntry.Checked
                && !chkSpecialEntry.Checked
                && !chkFailedUpload.Checked
                && !chkSearchProfile.Checked
                && !chkDraftRecords.Checked
                && !chkBiometricSearch.Checked
                && !chkUserManagement.Checked
                && !chkReport.Checked
                && !chkSettings.Checked
                && !chkUpdate.Checked
                && !chkProfileEnrollment.Checked
                && !chkNotEntry.Checked
                ) return false;
            else return true;
        }

        public bool Validatedata()
        {
            string errorMessage = "";

            if (Unit == null)
            {
                errorMessage += "Unit is required." + "\n";
            }

            if (string.IsNullOrWhiteSpace(tbUserId.Text))
            {
                errorMessage += "User Id is required." + "\n";
            }

            if (string.IsNullOrWhiteSpace(tbName.Text))
            {
                errorMessage += "Name is required." + "\n";
            }

            if (string.IsNullOrWhiteSpace(tbPassword.Text))
            {
                errorMessage += "Password is required." + "\n";
            }

            if (string.IsNullOrWhiteSpace(tbConfirmPassword.Text))
            {
                errorMessage += "Confirm Password is required." + "\n";
            }

            if (!string.IsNullOrWhiteSpace(tbPassword.Text)
                && !string.IsNullOrWhiteSpace(tbConfirmPassword.Text)
                && !string.Equals(tbPassword.Text.Trim(), tbConfirmPassword.Text.Trim(),
                StringComparison.CurrentCulture))
            {
                errorMessage += "Password and Confirm Password is mismatched." + "\n";
            }

            if (Role == null)
            {
                errorMessage += "Role is required." + "\n";
            }

            if (Role != null
                && !((CodeNameDto)Role).Name.Equals("Admin")
                && !IsPrivilegeSelected())
            {
                errorMessage += "At least one Privilege is required." + "\n";
            }

            if (errorMessage != "")
            {
                //MessageBox.Show("Please correct the following error(s):\n\n" + errorMessage,
                //    "RAB CDMS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please correct the following error(s):\n" + errorMessage);

                return false;
            }
            return true;
        }

        private void chkSelectAll_Click(object sender, EventArgs e)
        {
            if (chkSelectAll.Checked) CheckUncheckPrivileges(true);
            else CheckUncheckPrivileges(false);
        }

        private void cmbUnit_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cmbSubUnit.SelectedIndex = -1;

            if (Unit != null)
            {
                LoadSubUnit(((StationDto)Unit).id.Value);
            }
        }

        private void cmbRole_SelectionChangeCommitted(object sender, EventArgs e)
        {
            EnableDisableSelectAll(true);
            EnableDisablePrivileges(true);

            if (Role == null) return;

            if (((CodeNameDto)Role).Id.Equals(Globals.RoleId.Admin))
            {
                CheckUncheckSelectAll(true);
                CheckUncheckPrivileges(true);
                EnableDisableSelectAll(false);
                EnableDisablePrivileges(false);
            }
            else
            {
                EnableDisableSelectAll(true);
                EnableDisablePrivileges(true);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveUser();            
        }

        private List<PermissionDto> GetPermissions()
        {
            List<PermissionDto> list = new List<MODELS.DTO.Auth.PermissionDto>();

            if (chkNewEntry.Checked)
            {
                list.Add(new PermissionDto() { id = (int)chkNewEntry.Tag, type = 2 });
            }
            if (chkSpecialEntry.Checked)
            {
                list.Add(new PermissionDto() { id = (int)chkSpecialEntry.Tag, type = 2 });
            }
            if (chkDraftRecords.Checked)
            {
                list.Add(new PermissionDto() { id = (int)chkDraftRecords.Tag, type = 2 });
            }
            if (chkFailedUpload.Checked)
            {
                list.Add(new PermissionDto() { id = (int)chkFailedUpload.Tag, type = 2 });
            }
            if (chkSearchProfile.Checked)
            {
                list.Add(new PermissionDto() { id = (int)chkSearchProfile.Tag, type = 2 });
            }
            if (chkBiometricSearch.Checked)
            {
                list.Add(new PermissionDto() { id = (int)chkBiometricSearch.Tag, type = 2 });
            }
            if (chkUserManagement.Checked)
            {
                list.Add(new PermissionDto() { id = (int)chkUserManagement.Tag, type = 2 });
            }
            if (chkReport.Checked)
            {
                list.Add(new PermissionDto() { id = (int)chkReport.Tag, type = 2 });
            }
            if (chkSettings.Checked)
            {
                list.Add(new PermissionDto() { id = (int)chkSettings.Tag, type = 2 });
            }
            if (chkUpdate.Checked)
            {
                list.Add(new PermissionDto() { id = (int)chkUpdate.Tag, type = 2 });
            }
            if (chkProfileEnrollment.Checked)
            {
                list.Add(new PermissionDto() { id = (int)chkProfileEnrollment.Tag, type = 2 });
            }
            if (chkNotEntry.Checked)
            {
                list.Add(new PermissionDto() { id = (int)chkNotEntry.Tag, type = 2 });
            }
            
            return list;
        }

        private void RemoveUser()
        {
            //if (MessageBox.Show("Are you sure you want to remove/inactive this User?", "RAB CDMS", MessageBoxButtons.YesNo)
            //         == DialogResult.No)
            DialogResult dr = YesNoMessageBox.YesNoMessageBoxResult("RAB CDMS", "Are you sure you want to remove/inactivate this user?");
            if (dr == DialogResult.No)
            {
                return;
            }
            
            var request = new UserActivationRequest()
            {
                id = tbUserId.Tag != null ? Convert.ToInt32(tbUserId.Tag) : 0               
            };

            var response = OnRemoveUser(request);

            if (response != null && response.code == (int)HttpResponseStatus.OK)
            {
                ResetData();
                LoadUsers();
                InfoMessageBox.ShowMessage("SNSOP TOOLS", "User is removed/inactivated successfully.");
            }
            else if (response != null && response.code != (int)HttpResponseStatus.OK
                && !string.IsNullOrWhiteSpace(response.message))
            {
                InfoMessageBox.ShowMessage("SNSOP TOOLS", response.message);
            }
        }
        private void SaveUser()
        {
            if (!Validatedata()) return;

            var request = new AddUserRequest()
            {
                id = tbUserId.Tag != null ? Convert.ToInt32(tbUserId.Tag) : 0,
                unit = Convert.ToInt32(((StationDto)Unit).id),
                subunit = SubUnit != null ? Convert.ToInt32(((SubStationDto)SubUnit).id) : 0,
                username = tbUserId.Text.Trim(),
                nameEn = tbName.Text.Trim(),
                password = tbPassword.Text.Trim(),
                type = Globals.UserType.NotAdmin,
                permissions = GetPermissions(),
                workStationCode = Users.WorkStationCode,
                role = ((CodeNameDto)Role).Id,
                status = userAccountStatus,
                email = UserEmail,
                phone = UserPhone
            };

            if (tbUserId.Tag != null)
            {
                if (Convert.ToInt32(tbUserId.Tag) > 0)
                {
                    UserSearchCriteriaRequest NewRequest = new UserSearchCriteriaRequest();
                    NewRequest.id = Convert.ToInt32(tbUserId.Tag);

                    var NewResponse = new UserSearchResponse();

                    string errorMsg = string.Empty;

                    ProcessingDialog.Run(delegate ()
                    {
                        try
                        {
                            NewResponse = userApiManager.GetUserList(NewRequest);
                        }
                        catch (WebException x)
                        {
                            logger.Debug("Known error, when server not found when getting user list. Error Message: " + x.Message);
                            errorMsg = "Seems you are Offline while getting permission List. Please contact with your Administrator.";
                        }
                        catch (TimeoutException x)
                        {
                            logger.Debug("Connection timedout when getting user list. Error Message: " + x.Message);
                            errorMsg = "Seems you are Offline while getting permission List. Please contact with your Administrator.";
                        }
                        catch (Exception x)
                        {
                            logger.Error("There was an unexpected error when getting user list.\n" + x.ToString());
                            errorMsg = "There was an unexpected error when getting permission List. Please contact with your Administrator.";
                        }
                    });

                    if (!string.IsNullOrEmpty(errorMsg))
                    {
                        CustomMessageBox.ShowMessage("SNSOP TOOLS", errorMsg);
                        return;
                    }

                    if (NewResponse != null && NewResponse.code == (int)HttpResponseStatus.OK)
                    {
                        if (NewResponse.userList?.Count == 1)
                        {
                            if (request.permissions == null)
                            {
                                request.permissions = new List<PermissionDto>();
                            }
                            for (int i = 0; i < NewResponse.userList[0].permissions?.Count; i++)
                            {
                                if (NewResponse.userList[0].permissions[i].type == 1)
                                {
                                    request.permissions.Add(new PermissionDto() { id = NewResponse.userList[0].permissions[i].id, type = 1 });
                                }
                            }
                        }
                    }
                }
            }

            var response = OnSaveUser(request);

            if (response != null && response.code == (int)HttpResponseStatus.OK)
            {
                ResetData();
                LoadUsers();
                InfoMessageBox.ShowMessage("SNSOP TOOLS", "User is saved successfully.");
            }
            else if (response != null && response.code != (int)HttpResponseStatus.OK
                && !string.IsNullOrWhiteSpace(response.message))
            {
                InfoMessageBox.ShowMessage("SNSOP TOOLS", response.message);
            }
        }

        private ApiResponse OnSaveUser(AddUserRequest request)
        {
            var response = new ApiResponse();

            ProcessingDialog.Run(delegate ()
            {
                try
                {
                    response = userApiManager.SaveUser(request);
                }
                catch (WebException x)
                {
                    logger.Debug("Known error, when server not found when saving user. Error Message: " + x.Message);
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Seems you are Offline while saving a User. Please contact with your Administrator.");
                }
                catch (TimeoutException x)
                {
                    logger.Debug("Connection timedout when saving user. Error Message: " + x.Message);
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Seems you are Offline while saving a User. Please contact with your Administrator.");
                }
                catch (Exception x)
                {
                    logger.Error("There was an unexpected error when saving user.\n" + x.ToString());
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "There was an unexpected error when saving a User. Please contact with your Administrator.");
                }
            });

            return response;
        }

        private ApiResponse OnRemoveUser(UserActivationRequest request)
        {
            var response = new ApiResponse();

            ProcessingDialog.Run(delegate ()
            {
                try
                {
                    response = userApiManager.DeactivateUser(request);
                }
                catch (WebException x)
                {
                    logger.Debug("Known error, when server not found when removing user. Error Message: " + x.Message);
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Seems you are Offline while removing a User. Please contact with your Administrator.");
                }
                catch (TimeoutException x)
                {
                    logger.Debug("Connection timedout when removing user. Error Message: " + x.Message);
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Seems you are Offline while removing a User. Please contact with your Administrator.");
                }
                catch (Exception x)
                {
                    logger.Error("There was an unexpected error when removing user.\n" + x.ToString());
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "There was an unexpected error when removing a User. Please contact with your Administrator.");
                }
            });

            return response;
        }

        private void LoadUsers()
        {            
            request.type = Globals.AppType.Client;

            var response = new UserSearchResponse();

            ProcessingDialog.Run(delegate ()
            {
                try
                {
                    response = userApiManager.GetUserList(request);
                }
                catch (WebException x)
                {
                    logger.Debug("Known error, when server not found when getting user list. Error Message: " + x.Message);
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Seems you are Offline while getting User List. Please contact with your Administrator.");
                }
                catch (TimeoutException x)
                {
                    logger.Debug("Connection timedout when getting user list. Error Message: " + x.Message);
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Seems you are Offline while getting User List. Please contact with your Administrator.");
                }
                catch (Exception x)
                {
                    logger.Error("There was an unexpected error when getting user list.\n" + x.ToString());
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "There was an unexpected error when getting User List. Please contact with your Administrator.");
                }
            });

            total = Convert.ToInt32(response?.totalCount);

            if (response != null && response.code == (int)HttpResponseStatus.OK)
            {
                labelTotalRecords.Text = total.ToString();
                PrepareUsers(response);
            }
        }

        private string GetRoleNamebyId(int id)
        {
            string role = "Unknown Role";

            if (id == 1)
            {
                return "Admin";
            }
            if (id == 2)
            {
                return "Users";
            }
            if (id == 99)
            {
                return "Others";
            }

            return role;
        }

        private int GetPermissionStatus(int permissionId, List<PermissionDto> list)
        {
            int status = 0;
            if (list == null || list?.Count <= 0) return status;

            var data = list.Where(x => x.id == permissionId).FirstOrDefault();            
            if(data != null)
            {
                status = 1;
            } 
            return status;
        }

        private void PrepareUsers(UserSearchResponse response)
        {
            try
            {
                dgUserList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

                if (response == null || response.userList == null || response.userList.Count <= 0) return;

                dgUserList.Rows.Clear();
                
                //foreach (var obj in response.userList)
                for (int i = 0; i < response.userList.Count; i++)
                {
                    var obj = response.userList[i];

                    int Serial = (position * 10) + i + 1;

                    dgUserList.Rows.Add(new object[]
                    {
                        Serial,
                        obj.id,
                        obj.unit,
                        obj.subunit,
                        obj.role,
                        obj.unitName,
                        obj.subUnitName,
                        obj.username,
                        obj.nameEn,
                        obj.roleName,
                        obj.status ? "Active" : "Inactive",
                        GetPermissionStatus(Globals.PermissionsId.NewEntry, obj.permissions),
                        GetPermissionStatus(Globals.PermissionsId.SpecialEntry, obj.permissions),
                        GetPermissionStatus(Globals.PermissionsId.DraftRecords, obj.permissions),
                        GetPermissionStatus(Globals.PermissionsId.FailedUploads, obj.permissions),
                        GetPermissionStatus(Globals.PermissionsId.SearchProfile, obj.permissions),
                        GetPermissionStatus(Globals.PermissionsId.BiometricSearch, obj.permissions),
                        GetPermissionStatus(Globals.PermissionsId.UserManagement, obj.permissions),
                        GetPermissionStatus(Globals.PermissionsId.Report, obj.permissions),
                        GetPermissionStatus(Globals.PermissionsId.Settings, obj.permissions),
                        GetPermissionStatus(Globals.PermissionsId.Update, obj.permissions),
                        GetPermissionStatus(Globals.PermissionsId.ProfileEnrollment, obj.permissions),
                        GetPermissionStatus(Globals.PermissionsId.NotEntry, obj.permissions),
                        obj.email,
                        obj.phone
                    });
                }
                dgUserList.ClearSelection();

                //if (dgUserList.Rows.Count > 0)
                //{
                //    dgUserList.Rows[0].Selected = true;
                //}
            }
            catch (Exception ex)
            {
                logger.Error("There was an error when preparing list. Error Message: " + ex.ToString());
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "There was an error when preparing user list. Please contact with your Administrator.");
            }
        }

        private void dgUserList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (dgUserList.Rows.Count <= 0) return;

            //var index = e.RowIndex;

            //if (index < 0) return;

            //ShowUser(index);
        }

        private void ShowUser(int index)
        {
            try
            {
                CheckUncheckPrivileges(false);

                tbPassword.Text = "Nopassword4now";
                tbConfirmPassword.Text = "Nopassword4now";
                tbUserId.Enabled = false;
                tbPassword.Enabled = false;
                tbConfirmPassword.Enabled = false;

                tbUserId.Tag = 0;

                tbUserId.Tag = dgUserList.Rows[index].Cells["Id"].Value.ToString();
                tbUserId.Text = dgUserList.Rows[index].Cells["UserId"].Value.ToString();
                tbName.Text = dgUserList.Rows[index].Cells["UserName"].Value.ToString();

                selStationId = !string.IsNullOrWhiteSpace(dgUserList.Rows[index].Cells["UnitId"].Value?.ToString()) ?
                    Convert.ToInt32(dgUserList.Rows[index].Cells["UnitId"].Value?.ToString()) : 0;
                LoadUnit();

                selSubStationId = !string.IsNullOrWhiteSpace(dgUserList.Rows[index].Cells["SubUnitId"].Value?.ToString()) ?
                    Convert.ToInt32(dgUserList.Rows[index].Cells["SubUnitId"].Value?.ToString()) : 0;
                LoadSubUnit(selStationId.Value);

                selRoleId = !string.IsNullOrWhiteSpace(dgUserList.Rows[index].Cells["RoleId"].Value?.ToString()) ?
                   Convert.ToInt32(dgUserList.Rows[index].Cells["RoleId"].Value?.ToString()) : 0;
                LoadRole();

                string accountStatus = !string.IsNullOrWhiteSpace(dgUserList.Rows[index].Cells["AccountStatus"].Value?.ToString()) ?
                   dgUserList.Rows[index].Cells["AccountStatus"].Value.ToString() : "";

                if(accountStatus == "Active")
                {
                    userAccountStatus = true;
                }
                else
                {
                    userAccountStatus = false;
                }

                var NewEntry = !string.IsNullOrWhiteSpace(dgUserList.Rows[index].Cells["NewEntry"].Value?.ToString()) ?
                   Convert.ToBoolean(dgUserList.Rows[index].Cells["NewEntry"].Value) : false;

                var SpecialEntry = !string.IsNullOrWhiteSpace(dgUserList.Rows[index].Cells["SpecialEntry"].Value?.ToString()) ?
                   Convert.ToBoolean(dgUserList.Rows[index].Cells["SpecialEntry"].Value) : false;

                var DraftRecords = !string.IsNullOrWhiteSpace(dgUserList.Rows[index].Cells["DraftRecords"].Value?.ToString()) ?
                    Convert.ToBoolean(dgUserList.Rows[index].Cells["DraftRecords"].Value) : false;

                var FailedUploads = !string.IsNullOrWhiteSpace(dgUserList.Rows[index].Cells["FailedUploads"].Value?.ToString()) ?
                    Convert.ToBoolean(dgUserList.Rows[index].Cells["FailedUploads"].Value) : false;

                var SearchProfile = !string.IsNullOrWhiteSpace(dgUserList.Rows[index].Cells["SearchProfile"].Value?.ToString()) ?
                    Convert.ToBoolean(dgUserList.Rows[index].Cells["SearchProfile"].Value) : false;

                var BiometricSearch = !string.IsNullOrWhiteSpace(dgUserList.Rows[index].Cells["BiometricSearch"].Value?.ToString()) ?
                    Convert.ToBoolean(dgUserList.Rows[index].Cells["BiometricSearch"].Value) : false;


                var UserManagement = !string.IsNullOrWhiteSpace(dgUserList.Rows[index].Cells["UserManagement"].Value?.ToString()) ?
                    Convert.ToBoolean(dgUserList.Rows[index].Cells["UserManagement"].Value) : false;
                var Report = !string.IsNullOrWhiteSpace(dgUserList.Rows[index].Cells["Report"].Value?.ToString()) ?
                    Convert.ToBoolean(dgUserList.Rows[index].Cells["Report"].Value) : false;
                var Settings = !string.IsNullOrWhiteSpace(dgUserList.Rows[index].Cells["Settings"].Value?.ToString()) ?
                    Convert.ToBoolean(dgUserList.Rows[index].Cells["Settings"].Value) : false;
                var Update = !string.IsNullOrWhiteSpace(dgUserList.Rows[index].Cells["Update"].Value?.ToString()) ?
                    Convert.ToBoolean(dgUserList.Rows[index].Cells["Update"].Value) : false;
                var ProfileEnrollment = !string.IsNullOrWhiteSpace(dgUserList.Rows[index].Cells["ProfileEnrollment"].Value?.ToString()) ?
                    Convert.ToBoolean(dgUserList.Rows[index].Cells["ProfileEnrollment"].Value) : false;
                var NotEntry = !string.IsNullOrWhiteSpace(dgUserList.Rows[index].Cells["NotEntry"].Value?.ToString()) ?
                    Convert.ToBoolean(dgUserList.Rows[index].Cells["NotEntry"].Value) : false;

                UserEmail = !string.IsNullOrWhiteSpace(dgUserList.Rows[index].Cells["dgvEmail"].Value?.ToString()) ?
                   dgUserList.Rows[index].Cells["dgvEmail"].Value.ToString() : null;
                UserPhone = !string.IsNullOrWhiteSpace(dgUserList.Rows[index].Cells["dgvPhone"].Value?.ToString()) ?
                   dgUserList.Rows[index].Cells["dgvPhone"].Value.ToString() : null;

                chkNewEntry.Checked = NewEntry;
                chkSpecialEntry.Checked = SpecialEntry;
                chkDraftRecords.Checked = DraftRecords;
                chkFailedUpload.Checked = FailedUploads;
                chkSearchProfile.Checked = SearchProfile;
                chkBiometricSearch.Checked = BiometricSearch;
                chkUserManagement.Checked = UserManagement;
                chkReport.Checked = Report;
                chkSettings.Checked = Settings;
                chkUpdate.Checked = Update;
                chkProfileEnrollment.Checked = ProfileEnrollment;
                chkNotEntry.Checked = NotEntry;

                cmbRole_SelectionChangeCommitted(null, null);
            }
            catch (Exception ex)
            {
                logger.Error("There was an error when showing user data. Error Message: " + ex.ToString());
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "There was an error when showing user data. Please contact with your Administrator.");
            }
        }

        private string UserEmail { get; set; }
        private string UserPhone { get; set; }

        private void dgUserList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgUserList.Rows.Count <= 0) return;

            var index = e.RowIndex;

            if (index < 0) return;

            ShowUser(index);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            RemoveUser();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            position = 0;
            request.startIndex = position;
            LoadUsers();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (position < (total / limit)) position += 1;
            request.startIndex = position;
            LoadUsers();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (position > 0) position -= 1;
            request.startIndex = position;
            LoadUsers();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            position = total / limit;
            request.startIndex = position;
            LoadUsers();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            ((UserManagementController)controller).GoBacktoDashboard();
        }
    }
}
