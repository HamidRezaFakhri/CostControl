namespace CostControl.Entity.Models.Base
{
	using System.ComponentModel.DataAnnotations;

	public class SupperNameEntity<T> : SuperEntity<T>
	{
		[Required]
		[StringLength(250, MinimumLength = 2,
			ErrorMessage = "Please enter a unique Name, it must be greater than {2} characters and less than {1} characters.")]
		public string Name { get; set; }
	}
}