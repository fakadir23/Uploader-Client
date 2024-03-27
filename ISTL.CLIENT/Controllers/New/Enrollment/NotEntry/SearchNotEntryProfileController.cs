using ISTL.COMMON;
using ISTL.MODELS.DTO.New.Enrollment;
using ISTL.MODELS.DTO.New.NotEntry;
using ISTL.MODELS.Request.NotEntry;
using ISTL.MODELS.Response.New;
using ISTL.MODELS.Response.NotEntry;
using ISTL.PERSOGlobals;
using ISTL.RAB.ApiManager;
using ISTL.RAB.Entity;
using ISTL.RAB.View;
using ISTL.RAB.View.New.Enrollment.NotEntry;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.RAB.Controllers.New.Enrollment.NotEntry
{
    public class SearchNotEntryProfileController : ViewController
    {
        private SearchNotEntryProfileUserControl searchNotEntryProfileUserControl;
        private Logger logger = LogManager.GetCurrentClassLogger();
        private NotEntryApiManager notEntryApiManager;

        public SearchNotEntryProfileController()
        {
            searchNotEntryProfileUserControl = new SearchNotEntryProfileUserControl();
            base.SetView((IView)searchNotEntryProfileUserControl);
            searchNotEntryProfileUserControl.SetController(this);

            notEntryApiManager = new NotEntryApiManager();
        }

        public override string GetName()
        {
            return Globals.ChildControllers.SEARCH_NOT_ENTRY_PROFILE;
        }

        public void GoBackToDashboard()
        {
            ((MainController)parent).OnHome();
        }

        public void GetNotEntryProfile(string refNo, int EditOrPreview)
        {
            NotEntrySearchRequest request = new NotEntrySearchRequest();
            NotEntrySearchResponse response = new NotEntrySearchResponse();
            request.limit = 1;
            request.referenceNo = refNo;

            string errorMsg = string.Empty;

            ProcessingDialog.Run(delegate ()
            {
                try
                {
                    response = notEntryApiManager.NotEntrySearch(request);
                }
                catch (WebException x)
                {
                    logger.Debug("Known error, when server not found when searching criminal. Error Message: " + x.Message);
                    errorMsg = "Seems you are Offline while searching not_entry criminal. Please contact with your Administrator.";
                }
                catch (TimeoutException x)
                {
                    logger.Debug("Connection timedout when searching criminal. Error Message: " + x.Message);
                    errorMsg = "Seems you are Offline while searching not_entry criminal. Please contact with your Administrator.";
                }
                catch (Exception x)
                {
                    logger.Error("There was an unexpected error when searching criminal.\n" + x.ToString());
                    errorMsg = "There was an unexpected error when searching not_entry criminal. Please contact with your Administrator.";
                }
            });

            if (!string.IsNullOrEmpty(errorMsg))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", errorMsg);
                return;
            }

            if (response != null)
            {
                if (response.code == 200 && response.noEntryList?.Count > 0)
                {
                    StaticData.NotEntry = response.noEntryList[0];
                    StaticData.ModifiableNotEntry = false;

                    CopyAttachmentDataToStaticNotEntry(response.noEntryList[0]);
                    CopyBiometricDataToStaticNotEntry(response.noEntryList[0]);

                    if (EditOrPreview == 1)
                    {
                        parent.AddChild(Globals.ChildControllers.NOT_ENTRY);
                    }
                    else if (EditOrPreview == 2)
                    {
                        var form = new NotEntryPreviewForm();
                        form.ShowDialog();
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(response.message))
                    {
                        CustomMessageBox.ShowMessage("SNSOP TOOLS", response.message);
                    }
                    else
                    {
                        CustomMessageBox.ShowMessage("SNSOP TOOLS", "Could not fetch profile");
                    }
                }
            }
            else
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Could not fetch profile");
            }
        }

        private void CopyBiometricDataToStaticNotEntry(NotEntryDto dto)
        {
            try
            {
                if (StaticData.NotEntry.biometric == null) StaticData.NotEntry.biometric = new MODELS.DTO.New.Enrollment.Biometric.BiometricDto();

                if (dto.photo != null)
                {
                    StaticData.NotEntry.biometric.photo.photo = GetByteDataForStatic(dto.photo);
                }
                if (dto.fingerprint != null)
                {
                    if (!string.IsNullOrEmpty(dto.fingerprint.lt))
                        StaticData.NotEntry.biometric.fingerprint.lt = GetByteDataForStatic(dto.fingerprint.lt);
                    if (!string.IsNullOrEmpty(dto.fingerprint.li))
                        StaticData.NotEntry.biometric.fingerprint.li = GetByteDataForStatic(dto.fingerprint.li);
                    if (!string.IsNullOrEmpty(dto.fingerprint.lm))
                        StaticData.NotEntry.biometric.fingerprint.lm = GetByteDataForStatic(dto.fingerprint.lm);
                    if (!string.IsNullOrEmpty(dto.fingerprint.lr))
                        StaticData.NotEntry.biometric.fingerprint.lr = GetByteDataForStatic(dto.fingerprint.lr);
                    if (!string.IsNullOrEmpty(dto.fingerprint.ls))
                        StaticData.NotEntry.biometric.fingerprint.ls = GetByteDataForStatic(dto.fingerprint.ls);
                    if (!string.IsNullOrEmpty(dto.fingerprint.rt))
                        StaticData.NotEntry.biometric.fingerprint.rt = GetByteDataForStatic(dto.fingerprint.rt);
                    if (!string.IsNullOrEmpty(dto.fingerprint.ri))
                        StaticData.NotEntry.biometric.fingerprint.ri = GetByteDataForStatic(dto.fingerprint.ri);
                    if (!string.IsNullOrEmpty(dto.fingerprint.rm))
                        StaticData.NotEntry.biometric.fingerprint.rm = GetByteDataForStatic(dto.fingerprint.rm);
                    if (!string.IsNullOrEmpty(dto.fingerprint.rr))
                        StaticData.NotEntry.biometric.fingerprint.rr = GetByteDataForStatic(dto.fingerprint.rr);
                    if (!string.IsNullOrEmpty(dto.fingerprint.rs))
                        StaticData.NotEntry.biometric.fingerprint.rs = GetByteDataForStatic(dto.fingerprint.rs);
                }

                if (dto.iris != null)
                {
                    if (!string.IsNullOrEmpty(dto.iris.left)) StaticData.NotEntry.biometric.iris.left = GetByteDataForStatic(dto.iris.left);
                    if (!string.IsNullOrEmpty(dto.iris.right)) StaticData.NotEntry.biometric.iris.right = GetByteDataForStatic(dto.iris.right);
                }
            }
            catch { }
        }

        private void CopyAttachmentDataToStaticNotEntry(NotEntryDto dto)
        {
            try
            {
                if (StaticData.NotEntry.attachment == null) StaticData.NotEntry.attachment = new MODELS.DTO.New.Enrollment.Other.AttachmentDto();

                // Complain
                if (dto.complain != null)
                {
                    List<ComplainDto> complaintList = new List<ComplainDto>();
                    for (int j = 0; j < dto.complain?.files?.Count; j++)
                    {
                        ComplainDto complainDto = new ComplainDto();
                        complainDto.attachmentNumber = j;
                        string[] arr = dto.complain.files[j].Split('.');
                        complainDto.extension = "." + arr[1];
                        if (arr[1] == "pdf" || arr[1] == "PDF") complainDto.contentType = "application/" + arr[1];
                        else complainDto.contentType = "image/" + arr[1];
                        complainDto.complaint = GetByteDataForStatic(dto.complain.files[j]);
                        if (complainDto.complaint != null)
                        {
                            complaintList.Add(complainDto);
                        }
                    }
                    if (StaticData.NotEntry.attachment.complaintList == null) StaticData.NotEntry.attachment.complaintList = new List<ComplainDto>();
                    StaticData.NotEntry.attachment.complaintList = complaintList;
                }

                // Seizure
                if (dto.seizure != null)
                {
                    List<SeizureDto> seizureList = new List<SeizureDto>();
                    for (int j = 0; j < dto.seizure?.files?.Count; j++)
                    {
                        SeizureDto seizureDto = new SeizureDto();
                        seizureDto.attachmentNumber = j;
                        string[] arr = dto.seizure.files[j].Split('.');
                        seizureDto.extension = "." + arr[1];
                        if (arr[1] == "pdf" || arr[1] == "PDF") seizureDto.contentType = "application/" + arr[1];
                        else seizureDto.contentType = "image/" + arr[1];
                        seizureDto.seizure = GetByteDataForStatic(dto.seizure.files[j]);
                        if (seizureDto.seizure != null)
                        {
                            seizureList.Add(seizureDto);
                        }
                    }
                    if (StaticData.NotEntry.attachment.seizureList == null) StaticData.NotEntry.attachment.seizureList = new List<SeizureDto>();
                    StaticData.NotEntry.attachment.seizureList = seizureList;
                }

                // FIR
                if (dto.fir != null)
                {
                    List<FIRDto> firList = new List<FIRDto>();
                    for (int j = 0; j < dto.fir.files?.Count; j++)
                    {
                        FIRDto firDto = new FIRDto();
                        firDto.district = dto.fir.district;
                        firDto.upozilaOrThana = dto.fir.upozilaOrThana;
                        firDto.firNo = dto.fir.firNo;
                        firDto.firDate = dto.fir.firDate;
                        firDto.attachmentNumber = j;
                        string[] arr = dto.fir.files[j].Split('.');
                        firDto.extension = "." + arr[1];
                        if (arr[1] == "pdf" || arr[1] == "PDF") firDto.contentType = "application/" + arr[1];
                        else firDto.contentType = "image/" + arr[1];
                        firDto.fir = GetByteDataForStatic(dto.fir.files[j]);
                        if (firDto.fir != null)
                        {
                            firList.Add(firDto);
                        }
                    }
                    if (StaticData.NotEntry.attachment.firList == null) StaticData.NotEntry.attachment.firList = new List<FIRDto>();
                    StaticData.NotEntry.attachment.firList = firList;
                }
            }
            catch { }
        }

        private byte[] GetByteDataForStatic(string str)
        {
            try
            {
                GetProfileDataByteResponse response = new EnrollmentApiManager().GetByteDataByFilePath(str);
                if (response != null)
                {
                    if (response.code == 200)
                    {
                        return response.file;
                    }
                }
            }
            catch (Exception x)
            {
                logger.Error(x.ToString());
            }
            return null;
        }
    }
}
