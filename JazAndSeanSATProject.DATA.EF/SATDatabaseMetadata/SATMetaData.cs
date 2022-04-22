using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JazAndSeanSATProject.DATA.EF/*.SATDatabaseMetadata*/
{
    public class EnrollmentMetadata
    {
        //public int EnrollmentId { get; set; }
        [Required(ErrorMessage = "*")]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "*")]
        public int ScheduledClassId { get; set; }

        [Required(ErrorMessage = "*")]
        [DisplayFormat(DataFormatString ="{0:d}")]
        [Display(Name = "Enrollment Date")]
        public System.DateTime EnrollmentDate { get; set; }



    }//end class
        [MetadataType(typeof(EnrollmentMetadata))]
        public partial class Enrollment { }

    public class StudentMetadata
    {
        //public int StudentId { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "First Name")]
        [StringLength(20, ErrorMessage = "* Field must be 20 characters or less")]
        public string FirstName { get; set; }

        [StringLength(20, ErrorMessage = "* Field must be 20 characters or less")]
        [Required(ErrorMessage = "*")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DisplayFormat(NullDisplayText = "-N/A-")]
        [StringLength(15, ErrorMessage = "* Field must be 15 characters or less")]
        public string Major { get; set; }

        [DisplayFormat(NullDisplayText = "-N/A-")]
        [StringLength(50, ErrorMessage = "* Field must be 50 characters or less")]
        public string Address { get; set; }

        [DisplayFormat(NullDisplayText = "-N/A-")]
        [StringLength(25, ErrorMessage = "* Field must be 25 characters or less")]
        public string City { get; set; }

        [DisplayFormat(NullDisplayText = "-N/A-")]
        [StringLength(2, ErrorMessage = "* Field must be 2 characters or less")]
        public string State { get; set; }

        [DisplayFormat(NullDisplayText = "-N/A-")]
        [StringLength(10, ErrorMessage = "* Field must be 10 characters or less")]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

        [DisplayFormat(NullDisplayText = "-N/A-")]
        [StringLength(13, ErrorMessage = "* Field must be 13 characters or less")]
        public string Phone { get; set; }

        [StringLength(60, ErrorMessage = "* Field must be 60 characters or less")]
        [Required(ErrorMessage = "*")]
        public string Email { get; set; }

        [DisplayFormat(NullDisplayText = "-N/A-")]
        [StringLength(100, ErrorMessage = "* Field must be 100 characters or less")]
        public string PhotoUrl { get; set; }

        [Required(ErrorMessage = "*")]
        public int SSID { get; set; }

    }

    [MetadataType(typeof(StudentMetadata))]
    public partial class Student {
        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

    }

    public class StudentStatusMetadata
    {
        //public int SSID { get; set; }

        [StringLength(30, ErrorMessage = "* Field must be 30 characters or less")]
        [Required(ErrorMessage = "*")]
        public string SSName { get; set; }

        [DisplayFormat(NullDisplayText = "-N/A-")]
        [StringLength(250, ErrorMessage = "* Field must be 250 characters or less")]
        public string SSDescription { get; set; }
    }

    [MetadataType(typeof(StudentStatusMetadata))]
    public partial class StudentStatus { }

    public class ScheduledClassMetadata
    {
        //public int ScheduledClassId { get; set; }
        public int CourseId { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]

        [Required(ErrorMessage = "*")]
        public System.DateTime StartDate { get; set; }

        [Required(ErrorMessage = "*")]
        public System.DateTime EndDate { get; set; }

        [Required(ErrorMessage = "*")]
        public string InstructorName { get; set; }

        [Required(ErrorMessage = "*")]
        public string Location { get; set; }

        [Required(ErrorMessage = "*")]
        public int SCSID { get; set; }

    }
        [MetadataType(typeof(ScheduledClass))]
        public partial class ScheduledClass {
        [Display(Name = "Emrollment Info")]
        public string EnrollmentInfo
        {
            get { return Cours.CourseName + " " + StartDate + " " + Location; }
        }
    }

    public class CoursMetadata
    {
        //public int CourseId { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(50, ErrorMessage = "* Field must be 50 characters or less")]
        public string CourseName { get; set; }

        public string CourseDescription { get; set; }
        public byte CreditHours { get; set; }
        public string Curriculum { get; set; }
        public string Notes { get; set; }
        public bool IsActive { get; set; }
    }

        [MetadataType(typeof(CoursMetadata))]
        public partial class Cours { }
}//end namespace
