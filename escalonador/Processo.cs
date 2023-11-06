using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace escalonador
{
    internal class Processo
    {
        public int ProcessoId { get; set; }
        public int TempoExecucao { get; set; }
        public int TempoRemanescente { get; set; }
        public int Ingresso { get; set; }
        public int PrioridadeEstatica { get; set; }
        public int PrioridadeDinamica { get; set; }

        public Processo(int processoId, int tempoExecucao, int ingresso, int prioridadeEstatica)
        {
            ProcessoId = processoId;
            TempoExecucao = tempoExecucao;
            TempoRemanescente = tempoExecucao;
            Ingresso = ingresso;
            PrioridadeEstatica = prioridadeEstatica;
            PrioridadeDinamica = prioridadeEstatica;
        }
    }
}
