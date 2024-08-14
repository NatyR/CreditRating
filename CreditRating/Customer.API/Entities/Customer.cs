using System.ComponentModel.DataAnnotations;

namespace Customer.API.Entities
{
    public class CustomerData
    {
        public Guid CustomerId { get; set; }

        [Required(ErrorMessage = "Name is mandatory")]
        [StringLength(150, ErrorMessage = "The name cannot be longer than 100 characters")]
        public string Name { get; set; }


        [Required(ErrorMessage = "CPF is mandatory")]
        [RegularExpression(@"\d{3}\.\d{3}\.\d{3}-\d{2}", ErrorMessage = "Invalid CPF")]
        public string Cpf { get; set; }


        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Email is mandatory")]
        public string Email { get; set; }


        [Phone(ErrorMessage = "Invalid phone number")]
        public string Telephone { get; set; }


        [Required(ErrorMessage = "Date of birth is mandatory")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date of birth")]
        public DateTime DateBirth { get; set; }


        [Required(ErrorMessage = "Address is mandatory")]
        public string Address { get; set; }


        [Required(ErrorMessage = "Registration Date is mandatory")]
        public DateTime RegistrationDate { get; set; }


        [Required(ErrorMessage = "Salary is mandatory")]
        public decimal Salary { get; set; }

        public char Gender { get; set; }
        public string CivilStatus { get; set; }

        public CustomerData()
        {
            CustomerId = Guid.NewGuid();
        }
    }
}
