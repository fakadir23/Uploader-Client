using ISTL.MODELS.DTO.Enrollment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.RAB.Entity
{
    public class SelectedEntity
    {
        private static PersonEnrollmentDto person;

        public static PersonEnrollmentDto Person
        {
            get { return person; }
            set { person = value; }
        }
    }
}
