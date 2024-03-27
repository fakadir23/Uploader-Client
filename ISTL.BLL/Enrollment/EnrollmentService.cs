using AutoMapper;
using ISTL.DAL.DataContext;
using ISTL.DAL.Enrollment;
using ISTL.MODELS.Common;
using ISTL.MODELS.DTO.Enrollment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.BLL.Enrollment
{
    public interface IEnrollmentService
    {
        ApiResponse EnrollPerson(PersonEnrollmentDto dto);
        PersonEnrollmentDto PersonDetails(long id);
        ApiResponse DeletePerson(long id);
        List<PersonEnrollmentDto> PersonList();
    }

    public class EnrollmentService : IEnrollmentService
    {
        private readonly EnrollmentRepository _enrollmentRepository;
        private MapperConfiguration _mapperConfig;

        //Should use dependency injection container such as unity or ninject
        public EnrollmentService()
        {
            _enrollmentRepository = new EnrollmentRepository();
            InitializeMapper();
        }

        public ApiResponse DeletePerson(long id)
        {
            var response = _enrollmentRepository.DeletePerson(id);
            if (response.Status == 500)
            {
                return response;
            }
            response.Message = "Person's enrollment is deleted successfully.";
            return response;
        }

        public ApiResponse EnrollPerson(PersonEnrollmentDto dto)
        {
            var personEntity = _mapperConfig.CreateMapper().Map<PersonEnrollmentDto, personenrollment>(dto);
            var response = _enrollmentRepository.EnrollPerson(personEntity);
            if (response.Status == 500)
            {
                return response;
            }
            response.Message = dto?.Id > 0 ? "Person's enrollment is updated successfully."
                : "Person is enrolled successfully.";
            return response;
        }

        public PersonEnrollmentDto PersonDetails(long id)
        {
            var personEntity = _enrollmentRepository.PersonDetails(id);
            var personDto = _mapperConfig.CreateMapper().Map<personenrollment, PersonEnrollmentDto>(personEntity);
            return personDto;
        }

        public List<PersonEnrollmentDto> PersonList()
        {
            var personEntityList = _enrollmentRepository.PersonList();
            var personDtoList = _mapperConfig.CreateMapper().Map<List<personenrollment>, List<PersonEnrollmentDto>>(personEntityList);
            return personDtoList;
        }

        //Should place in a global config class and initialize when app starts
        private void InitializeMapper()
        {
            _mapperConfig = new MapperConfiguration(
            cfg =>
            {
                cfg.CreateMap<PersonEnrollmentDto, personenrollment>();
                cfg.CreateMap<personenrollment, PersonEnrollmentDto>();
            });
        }
    }
}
