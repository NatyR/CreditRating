using System.ComponentModel.DataAnnotations;

namespace Proposal.API.Common.Entities
{
    public class Customer
    {
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public DateTime DateBirth { get; set; }
        public string Address { get; set; }
        public DateTime RegistrationDate { get; set; }
        public decimal Salary { get; set; }
        public string Gender { get; set; }
        public string CivilStatus { get; set; }
        
    }
}
