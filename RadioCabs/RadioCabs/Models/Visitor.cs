using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RadioCabs.Models
{
	public class Visitor
	{	
		public int VisitorId { get; set; }
		public string VisitorName { get; set; }
		public string VisitorEmail { get; set; }
		public string VisitorCity { get; set; }
		public string VisitorMobile { get; set; }
		
		public Visitor() { }
		public Visitor(Registration registration)
		{
			VisitorName = registration.Name;
			VisitorEmail = registration.Email;
			VisitorCity = registration.City;
			VisitorMobile = registration.Mobile;
		}
	}
}
