using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace escalonador
{
    internal class ListaProcesso
    {

        public List<Processo> Processos { get; }

        public ListaProcesso()
        {
            Processos = new List<Processo>();
        }

        public void InserirProcesso(Processo processo)
        {
            Processos.Add(processo);
        }

        public bool Vazio => Processos.Count == 0;
    }
}
