using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace escalonador_processos
{
    public class Processo
    {
        public int TarefaId { get; set; }
        public int TempoExecucao { get; set; }
        public int TempoRemanescente { get; set; }
        public int Ingresso { get; set; }
        public int PrioridadeEstatica { get; set; }
        public int PrioridadeDinamica { get; set; }
    }

}
