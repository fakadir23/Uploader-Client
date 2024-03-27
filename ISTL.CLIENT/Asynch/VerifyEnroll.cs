using ISTL.MODELS.Response.New.Enrollment;
using ISTL.RAB.ApiManager;
using ISTL.RAB.Controllers;
using ISTL.RAB.DbManager;
using ISTL.RAB.Entity;
using ISTL.RAB.View;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.RAB.Asynch
{
    public class VerifyEnroll
    {
        private Logger logger = LogManager.GetCurrentClassLogger();

        public bool EnrollVerify()
        {
            if (Configuration.VerifyDayCount == 0)
            {
                return false;
            }

            if (string.IsNullOrEmpty(Users.AccessToken))
            {
                return false;
            }

            DbEnrollClientManager dbEnrollClientManager = new DbEnrollClientManager();
            List<string> uploadedHashList = dbEnrollClientManager.GetUploadedHashList(Configuration.VerifyDayCount);

            logger.Debug("UPLOADER: Start Verify All Uploaded Data.");

            logger.Debug("Total Uploaded Hash Found in Local Database: " + uploadedHashList.Count.ToString());

            if (uploadedHashList != null && uploadedHashList.Count > 0)
            {
                try
                {
                    EnrollmentApiManager enrollmentApiManager = new EnrollmentApiManager();
                    NotVerifiedHashResponse notVerifiedHashObj = enrollmentApiManager.GetNotVerifiedHashList(uploadedHashList);

                    if(notVerifiedHashObj != null && !(notVerifiedHashObj.code== 200))
                    {
                        CustomMessageBox.ShowMessage("SNSOP TOOLS","There was an unexpected error during getting not verified list by API call.");
                        return false;
                    }

                    if (notVerifiedHashObj == null || notVerifiedHashObj?.hashList == null 
                        || notVerifiedHashObj?.hashList?.Count <= 0)
                    {
                        foreach (string uploadedHash in uploadedHashList)
                        {
                            dbEnrollClientManager.UpdateEnrollStatusToVerified(uploadedHash);
                            logger.Info("Successfully Updated Enroll Status to VERIFIED: " + uploadedHash);
                        }
                        return true;
                    }

                    foreach (string uploadedHash in uploadedHashList)
                    {
                        if (notVerifiedHashObj.hashList.Contains(uploadedHash))
                        {
                            dbEnrollClientManager.UpdateEnrollStatusToNew(uploadedHash);
                            logger.Info("Successfully Updated Enroll Status to NEW: " + uploadedHash);
                        }
                        else
                        {
                            dbEnrollClientManager.UpdateEnrollStatusToVerified(uploadedHash);
                            logger.Info("Successfully Updated Enroll Status to VERIFIED: " + uploadedHash);
                        }
                    }
                    return true;
                }
                catch (System.Net.WebException x)
                {
                    // Connection problems. This is not serious
                    logger.Debug("Enroll Verify Operation: Connection problems. This is not serious." + x.Message);
                    ErrorMessageBox.ShowError("The web exception during upload verification process.", x);
                }
                catch (TimeoutException x)
                {
                    logger.Error("Enroll Verify Operation: Connection timed out!\n" + x.ToString());
                    ErrorMessageBox.ShowError("The connection timed out during upload verification process.", x);
                }
                catch (System.ServiceModel.EndpointNotFoundException x)
                {
                    logger.Debug("Enroll Verify Operation: EndPointNotFound during enroll verify operation!" + x.Message);
                    //ErrorMessageBox.ShowError("There was a problem during upload verification process.", x);
                }
                catch (Exception x)
                {
                    // Is Online but got unexpected exception
                    logger.Error("There was an unexpected error during enroll verify operation. " + "\n" + x.ToString());
                    ErrorMessageBox.ShowError("There was an unexpected error during enroll verify operation.", x);
                }
            }
            else
            {
                return true;
            }

            return false;
        }

        public bool SpecialEnrollVerify()
        {
            if (Configuration.VerifyDayCount == 0)
            {
                return false;
            }

            if (string.IsNullOrEmpty(Users.AccessToken))
            {
                return false;
            }

            DbSpecialEnrollManager dbSpecialEnrollManager = new DbSpecialEnrollManager();
            List<string> uploadedHashList = dbSpecialEnrollManager.GetUploadedHashList(Configuration.VerifyDayCount);

            logger.Debug("UPLOADER: Start Verify All Uploaded Data.");

            logger.Debug("Total Uploaded Hash Found in Local Database: " + uploadedHashList.Count.ToString());

            if (uploadedHashList != null && uploadedHashList.Count > 0)
            {
                try
                {
                    SpecialEnrollApiManager enrollmentApiManager = new SpecialEnrollApiManager();
                    NotVerifiedHashResponse notVerifiedHashObj = enrollmentApiManager.GetSpecialNotVerifiedHashList(uploadedHashList);

                    if (notVerifiedHashObj != null && !(notVerifiedHashObj.code == 200))
                    {
                        CustomMessageBox.ShowMessage("SNSOP TOOLS", "There was an unexpected error during getting not verified list by API call.");
                        return false;
                    }

                    if (notVerifiedHashObj == null || notVerifiedHashObj?.hashList == null
                        || notVerifiedHashObj?.hashList?.Count <= 0)
                    {
                        foreach (string uploadedHash in uploadedHashList)
                        {
                            dbSpecialEnrollManager.UpdateEnrollStatusToVerified(uploadedHash);
                            logger.Info("Successfully Updated Enroll Status to VERIFIED: " + uploadedHash);
                        }
                        return true;
                    }

                    foreach (string uploadedHash in uploadedHashList)
                    {
                        if (notVerifiedHashObj.hashList.Contains(uploadedHash))
                        {
                            dbSpecialEnrollManager.UpdateEnrollStatusToNew(uploadedHash);
                            logger.Info("Successfully Updated Enroll Status to NEW: " + uploadedHash);
                        }
                        else
                        {
                            dbSpecialEnrollManager.UpdateEnrollStatusToVerified(uploadedHash);
                            logger.Info("Successfully Updated Enroll Status to VERIFIED: " + uploadedHash);
                        }
                    }
                    return true;
                }
                catch (System.Net.WebException x)
                {
                    // Connection problems. This is not serious
                    logger.Debug("Enroll Verify Operation: Connection problems. This is not serious." + x.Message);
                    ErrorMessageBox.ShowError("The web exception during upload verification process.", x);
                }
                catch (TimeoutException x)
                {
                    logger.Error("Enroll Verify Operation: Connection timed out!\n" + x.ToString());
                    ErrorMessageBox.ShowError("The connection timed out during upload verification process.", x);
                }
                catch (System.ServiceModel.EndpointNotFoundException x)
                {
                    logger.Debug("Enroll Verify Operation: EndPointNotFound during enroll verify operation!" + x.Message);
                    //ErrorMessageBox.ShowError("There was a problem during upload verification process.", x);
                }
                catch (Exception x)
                {
                    // Is Online but got unexpected exception
                    logger.Error("There was an unexpected error during enroll verify operation. " + "\n" + x.ToString());
                    ErrorMessageBox.ShowError("There was an unexpected error during enroll verify operation.", x);
                }
            }
            else
            {
                return true;
            }

            return false;
        }
    }
}
