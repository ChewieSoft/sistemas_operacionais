using System;
using System.Threading;

namespace ConcorrenciaThreads
{
    class Program
    {

        private const int TOTAL_ITERACOES = 15;
        private const string ALFA = "ALFA";
        private const string BRAVO = "BRAVO";
        private const string CHARLIE = "CHARLIE";
        
        /**
         * Este programa tem por finalidade demonstrar uma simulação da concorrência entre 3 instâncias de uma thread em C#.         * 
         */

        static void Main(string[] args)
        {

            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Simulando cenário de concorrência
            // Cria três threads
            Thread threadAlfa = new (Programa);
            Thread threadBravo = new (Programa);
            Thread threadCharlie = new (Programa);

            string simulacaoInicio = ImprimirMensagemColorida("Simulação INICIALIZADA! \U0001F680", "verde");
            Console.WriteLine(simulacaoInicio);


            // Inicia as três threads
            threadAlfa.Start(ALFA);
            threadBravo.Start(BRAVO);
            threadCharlie.Start(CHARLIE);

            // Aguarda até que todas as threads terminem
            threadAlfa.Join();
            threadBravo.Join();
            threadCharlie.Join();

            string simulacaoFim = ImprimirMensagemColorida("Simulação FINALIZADA! \U0001F3C1", "verde");
            Console.WriteLine(simulacaoFim);
        }

        static void Programa(object identificador)
        {
            // Thread atual
            Thread threadAtual = Thread.CurrentThread;
            int contador = 0;

            
            while (contador <= TOTAL_ITERACOES)
            {
                string threadId = String.Empty;

                if(identificador.Equals(ALFA))
                {
                    threadId = ImprimirMensagemColorida($"Thread {identificador} => ID: {threadAtual.ManagedThreadId}", "vermelho");
                }
                else if(identificador.Equals(BRAVO))
                {
                    threadId = ImprimirMensagemColorida($"Thread {identificador} => ID: {threadAtual.ManagedThreadId}", "azul");
                }
                else if(identificador.Equals(CHARLIE))
                {
                    threadId = ImprimirMensagemColorida($"Thread {identificador} => ID: {threadAtual.ManagedThreadId}", "magenta");
                }

                Console.WriteLine(threadId);
                contador++;
            }
        }

        #region => Colorir Console

        static Dictionary<string, string> Cores = new Dictionary<string, string>
        {
            { "vermelho", "\u001b[91m" },
            { "verde", "\u001b[92m" },
            { "amarelo", "\u001b[93m" },
            { "azul", "\u001b[94m" },
            { "magenta", "\u001b[95m" },
            { "ciano", "\u001b[96m" },
            { "reset", "\u001b[0m" }
        };

        static string ImprimirMensagemColorida(string mensagem, string cor)
        {
            if (!Cores.ContainsKey(cor))
            {
                return mensagem;
            }
            else
            {
                string mensagemOriginal = $"{Cores[cor]}{mensagem}{Cores["reset"]}";
                return mensagemOriginal;
            }
        }

        #endregion => Colorir Console
    }
}
