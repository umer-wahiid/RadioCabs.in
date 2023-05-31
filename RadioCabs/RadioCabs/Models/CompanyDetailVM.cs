using Microsoft.EntityFrameworkCore;

namespace RadioCabs.Models
{
	[Keyless]
	public class CompanyDetailVM
	{
		public Registration RegData { get; set; }
		public CompanyRegistration CompData { get; set; }
	}
}
