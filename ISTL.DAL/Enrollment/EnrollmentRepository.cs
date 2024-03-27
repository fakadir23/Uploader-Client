using ISTL.DAL.DataContext;
using ISTL.MODELS.Common;
using ISTL.MODELS.DTO.Enrollment;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.DAL.Enrollment
{
    public interface IEnrollmentRepository
    {
        ApiResponse EnrollPerson(personenrollment entity);
        personenrollment PersonDetails(long id);
        ApiResponse DeletePerson(long id);
        List<personenrollment> PersonList();
    }
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        public ApiResponse DeletePerson(long id)
        {
            logger.Trace("Entry");
            ApiResponse response = new ApiResponse() { Status = 500 };
            try
            {
                using (var db = new ISTLEntities())
                {
                    var personEntity = db.personenrollments.Where(x => x.Id == id).FirstOrDefault();
                    db.Entry(personEntity).State = EntityState.Deleted;
                    db.SaveChanges();
                    response.Status = 200;
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}", validationErrors.Entry.Entity.ToString(), validationError.ErrorMessage);
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                logger.ErrorException("There was an critial error. Error Messate: ", raise);
                throw raise;
            }
            catch (Exception ex)
            {
                logger.ErrorException("There was an critial error. Error Messate: ", ex);
                throw ex;
            }
            finally
            {
                logger.Trace("Exit");
            }
            return response;
        }
        public ApiResponse EnrollPerson(personenrollment entity)
        {
            logger.Trace("Entry");
            ApiResponse response = new ApiResponse() { Status = 500 };
            try
            {
                using (var db = new ISTLEntities())
                {
                    // Transaction is not applicable below because we are 
                    // saving data in one table or performing one operation. 
                    // This is just an example.
                    // Transaction is applicable only when we will perform 
                    // muliple operations to maintain atomicity. 
                    using (DbContextTransaction transaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            //Update operation
                            if (entity?.Id > 0)
                            {
                                var dataToValidate = db.personenrollments.Where(x => x.Id != entity.Id
                                && x.MobileNumber.Trim().Equals(entity.MobileNumber.Trim())).FirstOrDefault();
                                if (dataToValidate != null)
                                {
                                    return new ApiResponse(500, "enroll_updated", "The mobile number you have entered is already exist.");
                                }
                                db.Entry(entity).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                            //Insert operation
                            else
                            {
                                var dataToValidate = db.personenrollments.Where(x => x.MobileNumber.Trim().Equals(entity.MobileNumber.Trim())).FirstOrDefault();
                                if (dataToValidate != null)
                                {
                                    return new ApiResponse(500, "enroll_saved", "The mobile number you have entered is already exist.");
                                }
                                db.personenrollments.Add(entity);
                                db.SaveChanges();
                            }
                            transaction.Commit();
                            response.Status = 200;
                            response.IsSuccess = true;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            logger.ErrorException("There was an critial error. Error Messate: ", ex);
                        }
                    }
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}", validationErrors.Entry.Entity.ToString(), validationError.ErrorMessage);
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                logger.ErrorException("There was an critial error. Error Messate: ", raise);
                throw raise;
            }
            catch (Exception ex)
            {
                logger.ErrorException("There was an critial error. Error Messate: ", ex);
                throw ex;
            }
            finally
            {
                logger.Trace("Exit");
            }
            return response;
        }
        public personenrollment PersonDetails(long id)
        {
            logger.Trace("Entry");
            personenrollment response = null;
            try
            {
                using (var db = new ISTLEntities())
                {
                    response = db.personenrollments.Where(x => x.Id == id).FirstOrDefault();
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}", validationErrors.Entry.Entity.ToString(), validationError.ErrorMessage);
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                logger.ErrorException("There was an critial error. Error Messate: ", raise);
                throw raise;
            }
            catch (Exception ex)
            {
                logger.ErrorException("There was an critial error. Error Messate: ", ex);
                throw ex;
            }
            finally
            {
                logger.Trace("Exit");
            }
            return response;
        }
        public List<personenrollment> PersonList()
        {
            logger.Trace("Entry");
            List<personenrollment> response = new List<personenrollment>();
            try
            {
                using (var db = new ISTLEntities())
                {
                    response = db.personenrollments.ToList();
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}", validationErrors.Entry.Entity.ToString(), validationError.ErrorMessage);
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                logger.ErrorException("There was an critial error. Error Messate: ", raise);
                throw raise;
            }
            catch (Exception ex)
            {
                logger.ErrorException("There was an critial error. Error Messate: ", ex);
                throw ex;
            }
            finally
            {
                logger.Trace("Exit");
            }
            return response;
        }
    }
}
