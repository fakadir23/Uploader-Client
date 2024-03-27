﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.DTO.Enrollment
{
    public class PersonEnrollmentDto
    {
        public long Id { get; set; }
        public string FirstNameEn { get; set; }
        public string MiddleNameEn { get; set; }
        public string LastNameEn { get; set; }
        public string FirstNameLocal { get; set; }
        public string MiddleNameLocal { get; set; }
        public string LastNameLocal { get; set; }
        public Nullable<sbyte> PlaceOfBirth { get; set; }
        public Nullable<sbyte> NationalityId { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public Nullable<sbyte> Gender { get; set; }
        public string MotherName { get; set; }
        public string FatherName { get; set; }
        public string SpouseName { get; set; }
        public Nullable<sbyte> MaritalStatus { get; set; }
        public Nullable<sbyte> Occupation { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public Nullable<sbyte> PermanentDivisionId { get; set; }
        public Nullable<sbyte> PermanentDistrictId { get; set; }
        public Nullable<short> PermanentStationId { get; set; }
        public Nullable<short> PermanentUpazilaId { get; set; }
        public Nullable<int> PermanentUnionId { get; set; }
        public Nullable<short> PermanentPostCode { get; set; }
        public string PermanentAddress { get; set; }
        public Nullable<sbyte> PresentDivisionId { get; set; }
        public Nullable<sbyte> PresentDistrictId { get; set; }
        public Nullable<short> PresentStationId { get; set; }
        public Nullable<short> PresentPostCode { get; set; }
        public string PresentAddress { get; set; }
        public string Status { get; set; }
        public Nullable<sbyte> ApplicationStatus { get; set; }
        public Nullable<short> BloodGroupId { get; set; }
        public string CategoriesId { get; set; }
        public string OrganizationId { get; set; }
        public string Designation { get; set; }
        public string Remarks { get; set; }
        public byte[] Photo { get; set; }
        public string PhotoUrl { get; set; }
    }
}
