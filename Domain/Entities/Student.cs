using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
	public class Student
	{
		public int Id { get; set; }

		[Required]
		public String? Name { get; set; }

		[Required]
		[EmailAddress]
		public String? Email { get; set; }

		[Required]
		public String? HashedPassword { get; set; }
	}
}

