
namespace Domain.Entities
{
	public class Student
	{
		public int Id { get; set; }

		public String? Name { get; set; }

		public String? Email { get; set; }

		public string? StudentId { get; set; }

		public String? Status { get; set; }

		public DateTime? EnrollmentDate { get; set; }

		public string? PhoneNumber { get; set; }

		public DateTime? CreatedAt { get; set; }

		public DateTime? UpdatedAt { get; set; }
	}
}

