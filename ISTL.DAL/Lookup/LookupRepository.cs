using ISTL.DAL.DataContext;
using ISTL.MODELS.DTO.Lookup;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.DAL.Lookup
{
    public interface ILookupRepository
    {
        List<lookup> LookupDataList(LookupDto request);
    }
    public class LookupRepository : ILookupRepository
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        public List<lookup> LookupDataList(LookupDto request)
        {
            logger.Trace("Entry");
            List<lookup> list = new List<lookup>();
            try
            {
                using (var db = new ISTLEntities())
                {
                    list = db.lookups.Where(x => x.Type.Equals(request.Type)).ToList();
                    
                    if(request.HasParent)
                    {
                        list = list.Where(x => x.ParentId == request.ParentId).ToList();
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
            return list;
        }
    }
}
