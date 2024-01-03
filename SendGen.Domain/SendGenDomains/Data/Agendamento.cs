using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendGen.Domain.SendGenDomains.Data
{
	public class Agendamento
	{
		[Key]
		public int ID { get; set; }
		public int FiltroID { get; set; }
		public string TemplateID { get; set; }
		public string CanalID { get; set; }
		public DateTime UltimaExecucao {  get; set; }
		public int IntervaloExecucao { get; set; }
		public string Tipo {  get; set; }
	}
}