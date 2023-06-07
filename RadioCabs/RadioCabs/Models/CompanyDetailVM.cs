using Microsoft.EntityFrameworkCore;

namespace RadioCabs.Models
{
	[Keyless]
	public class CompanyDetailVM
	{
		public Registration RegData { get; set; }
		public CompanyRegistration CompData { get; set; }
		public DriversRegistration DriverData { get; set; }
		public Services ServData { get; set; }
	}
}
