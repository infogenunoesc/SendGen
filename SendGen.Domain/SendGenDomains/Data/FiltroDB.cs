using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendGen.Domain.SendGenDomains.Data
{
	public class FiltroDB
	{
		[Key]
		public int ID { get; set; }
		public string Condicao { get; set; }

	}
}
