using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Xml.Linq;
using static HR.Shared.Enums;

namespace HR.PresentationModels.Dto.Users
{
	public class LogsDto
	{


		public int ID { get; set; }
		public CheckType Type { get; set; }
		public DateTime Date { get; set; }
		public string Message { get; set; }
	}
}
