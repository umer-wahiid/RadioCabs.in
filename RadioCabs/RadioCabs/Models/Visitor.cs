using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RadioCabs.Models
{
	public class Visitor
	{
        [Key]
        public int VisitorId { get; set; }
		public DateTime VisitDate { get; set; }
		public string VisitorName { get; set; }
		public string VisitorProfile { get; set; }
		public string VisitorEmail { get; set; }
		public string VisitorCity { get; set; }
		public string VisitorMobile { get; set; }
		public int Compid { get; set; }
		public int Driveid { get; set; }
		
		//public Visitor() { }
		//public Visitor(Registration registration)
		//{
		//	VisitorName = registration.Name;
		//	VisitorEmail = registration.Email;
		//	VisitorCity = registration.City;
		//	VisitorMobile = registration.Mobile;
		//}
	}
}
