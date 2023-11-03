public class Program
{
    public enum Estado
    {
        READY,
        RUNNING,
        BLOCKED,
        FINISHED
    }

    public class Processo
    {
        public const int QUANTIDADE_PROCESSOS = 7;
        public int PID { get; }
        public Estado Estado { get; set; }

        public Processo(int pid)
        {
            PID = pid;
            Estado = Estado.READY;
        }

        public override string ToString()
        {
            if(Estado == Estado.FINISHED)
            {
                return $"-------------------------------------\n" +
                $" Processo PID: {PID} | Estado: \u001b[92m{Estado}\u001b[0m" +
                "\n-------------------------------------";
            }
            else if(Estado == Estado.RUNNING)
            {
                return $"-------------------------------------\n" +
                $" Processo PID: {PID} | Estado: \u001b[93m{Estado}\u001b[0m" +
                "\n-------------------------------------";
            }
            else if(Estado == Estado.BLOCKED)
            {
                return $"-------------------------------------\n" +
                $" Processo PID: {PID} | Estado: \u001b[91m{Estado}\u001b[0m" +
                "\n-------------------------------------";
            }
            else if(Estado == Estado.READY)
            {
                return $"-------------------------------------\n" +
                $" Processo PID: {PID} | Estado: \u001b[94m{Estado}\u001b[0m" +
                "\n-------------------------------------";
            }
            else
            {
                return $"-------------------------------------\n" +
                $" Processo PID: {PID} | Estado: {Estado}" +
                "\n-------------------------------------";
            }
                                      
        }
    }

    public static void SimularEstadosTransicao(Processo processo)
    {
        Random random = new Random();
        switch (processo.Estado)
        {
            case Estado.READY:
                if (random.NextDouble() < 0.6)
                    processo.Estado = Estado.RUNNING;                
                break;
            case Estado.RUNNING:
                if (random.NextDouble() < 0.4)
                    processo.Estado = Estado.BLOCKED;         
                else
                {
                    if (random.NextDouble() < 0.5)
                    {
                        processo.Estado = Estado.READY;
                    }
                    else 
                        processo.Estado = Estado.FINISHED;                    
                }
                break;
            case Estado.BLOCKED:
                processo.Estado = Estado.READY;
                break;
        }
    }

    public static void Main()
    {

        Console.OutputEncoding = System.Text.Encoding.UTF8;

        List<Processo> processos = new List<Processo>();

        Console.WriteLine("\n\u001b[92mColocando os processos na Fila... \u001b[0m\U0001F680🚀");

        for (int contador = 1; contador <= Processo.QUANTIDADE_PROCESSOS; contador++)
        {
            processos.Add(new Processo(contador));
        }        

        while (!processos.All(processo => processo.Estado == Estado.FINISHED))
        {
            Console.WriteLine("\n✨ \u001b[93mSimulando transições de estados...\u001b[0m");
            foreach (Processo processo in processos)
            {
                SimularEstadosTransicao(processo);
            }

            Console.WriteLine("\n        ESTADO DOS PROCESSOS:");
            foreach (Processo processo in processos)
            {
                Console.WriteLine(processo);         
            }
            Thread.Sleep(1000);

            if(!processos.All(processo => processo.Estado == Estado.FINISHED))
            Console.Clear();

        }

        if (processos.All(processo => processo.Estado == Estado.FINISHED))
        {
            Console.WriteLine("\n\u001b[92mTodos processos FINALIZADOS!\u001b[0m \U0001F3C1");
        }
    }
}
