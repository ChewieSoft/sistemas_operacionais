using System;
using System.Threading;

namespace escalonador_processos
{
    public class Escalonador
    {
        public ListaProcessos ListaProcesso { get; private set; }
        public int Cycle { get; private set; }

        public Escalonador(ListaProcessos listaProcesso)
        {
            ListaProcesso = listaProcesso;
            Cycle = 0;
        }

        public void Executar()
        {
            while (true)
            {
                // Implemente a lógica do escalonador aqui
                // Use métodos da ListaProcessos e da classe Processo
                // para simular o escalonamento

                // Exemplo: 
                Processo processo = SelecionarProcesso();
                if (processo != null)
                {
                    // Execute o processo
                    ExecutarProcesso(processo);

                    // Atualize a lista de processos
                    AtualizarListaProcessos(processo);

                    // Imprima as informações
                    ImprimirInformacoes();
                }

                // Simulação de ciclo de execução
                Thread.Sleep(200); // Aguarda 200ms para desacelerar os ciclos
                Cycle++;

                // Condição de parada - Implemente sua própria lógica aqui
                if (SuaCondicaoDeParada()) break;
            }
        }

        private Processo SelecionarProcesso()
        {
            // Implemente a lógica de seleção do processo a ser executado
            // com base na prioridade dinâmica, tempo remanescente, etc.
            return null; // Substitua isso com a lógica de seleção apropriada
        }

        private void ExecutarProcesso(Processo processo)
        {
            // Execute o processo
            processo.TempoRemanescente--;
        }

        private void AtualizarListaProcessos(Processo processo)
        {
            // Atualize a lista de processos, ajustando a prioridade dinâmica, etc.
            foreach (var p in ListaProcesso.Processos)
            {
                if (p != processo)
                {
                    p.PrioridadeDinamica++;
                }

                if (p.TempoRemanescente == 0)
                {
                    ListaProcesso.Processos.Remove(p);
                }
            }
        }

        private void ImprimirInformacoes()
        {
            Console.WriteLine($"Ciclo: {Cycle}");
            // Implemente a impressão das informações dos processos
            foreach (var processo in ListaProcesso.Processos)
            {
                Console.WriteLine($"ID: {processo.TarefaId}, Tempo restante: {processo.TempoRemanescente}, Prioridade dinâmica: {processo.PrioridadeDinamica}");
            }
            Console.WriteLine("----------------------------");
        }

        private bool SuaCondicaoDeParada()
        {
            // Implemente a sua condição de parada do escalonador
            return false; // Substitua isso com a sua própria condição de parada
        }
    }
}
