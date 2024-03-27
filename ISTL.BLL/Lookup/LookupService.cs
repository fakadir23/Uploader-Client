using AutoMapper;
using ISTL.DAL.DataContext;
using ISTL.DAL.Lookup;
using ISTL.MODELS.DTO.Lookup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.BLL.Lookup
{
    public interface ILookupService
    {
        List<LookupDto> LookupDataList(LookupDto request);
    }
    public class LookupService : ILookupService
    {
        private readonly LookupRepository _lookupRepository;
        private MapperConfiguration _mapperConfig;

        public LookupService()
        {
            _lookupRepository = new LookupRepository();
            InitializeMapper();
        }
        public List<LookupDto> LookupDataList(LookupDto request)
        {
            var list = _mapperConfig.CreateMapper().Map<List<lookup>, List<LookupDto>>(_lookupRepository.LookupDataList(request));
            return list;
        }
        private void InitializeMapper()
        {
            _mapperConfig = new MapperConfiguration(
            cfg =>
            {
                cfg.CreateMap<lookup, LookupDto>();
            });
        }
    }
}
