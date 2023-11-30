using escalonador;
using System;
using System.Collections.Generic;
using System;
using System.IO;
using System.Threading;
using System.Diagnostics;

class Program
{
    private const int CYCLE_TIME = 200;
    public static int cycle = 0;

    public static String ANSI_PURPLE = "\u001B[35m";
    public static String ANSI_CYAN = "\u001B[36m";
    public static String ANSI_RESET = "\u001B[0m";

    static void Main()
    {        

        ListaProcesso listaProcessoRaw = CarregarDados();
        ListaProcesso listaProcesso = new ListaProcesso();
        Processo processo = null;

        while (true)
        {
            InserirProcessos(listaProcessoRaw, listaProcesso);

            if (FinishHim(listaProcesso))
            {
                break;
            }

            processo = Selecao(listaProcesso);
            Executar(processo);
            Manutencao(processo, listaProcesso);

            Console.Clear();
            ImprimirInfos(cycle, listaProcesso, processo);
            Imprimir(listaProcesso);

            Thread.Sleep(CYCLE_TIME);
            cycle++;
        }

        ExecutarFCFS(listaProcessoRaw.Processos);        
    }

    static ListaProcesso CarregarDados()
    {
        ListaProcesso listaProcessoRaw = new ListaProcesso();

        try
        {
            string[] lines = File.ReadAllLines("Processos.txt");

            int totalProcessos = int.Parse(lines[0]);
            for (int contador = 1; contador <= totalProcessos; contador++)
            {
                string[] data = lines[contador].Split(';');

                int ingresso = int.Parse(data[0]);
                int tempoExecucao = int.Parse(data[1]);
                int prioridadeEstatica = int.Parse(data[2]);

                Processo processo = new Processo(contador, tempoExecucao, ingresso, prioridadeEstatica);
                listaProcessoRaw.InserirProcesso(processo);
            }
        }
        catch (IOException e)
        {
            Console.WriteLine($"\u001b[91mErro ao ler o arquivo: {e.Message}\u001b[0m" );
        }

        return listaProcessoRaw;
    }

    static void InserirProcessos(ListaProcesso listaProcessoRaw, ListaProcesso listaProcesso)
    {
        foreach (Processo processo in listaProcessoRaw.Processos)
        {
            if (processo.Ingresso == cycle)
            {
                listaProcesso.InserirProcesso(processo);
            }
        }
    }

    static bool FinishHim(ListaProcesso listaProcesso)
    {
        return listaProcesso.Vazio;
    }

    static Processo Selecao(ListaProcesso listaProcesso)
    {
        Processo selectedProcess = listaProcesso.Processos[0];
        foreach (Processo processo in listaProcesso.Processos)
        {
            if (processo.PrioridadeDinamica > selectedProcess.PrioridadeDinamica)
            {
                selectedProcess = processo;
            }
            else if (processo.PrioridadeDinamica == selectedProcess.PrioridadeDinamica)
            {
                selectedProcess = processo.TempoRemanescente < selectedProcess.TempoRemanescente ? processo : selectedProcess;
            }
        }
        return selectedProcess;
    }

    static void Executar(Processo processo)
    {
        processo.TempoRemanescente--;
    }

    static void Manutencao(Processo processo, ListaProcesso listaProcesso)
    {
        foreach (Processo p in listaProcesso.Processos)
        {
            if (p != processo)
            {
                p.PrioridadeDinamica++;
            }
        }

        listaProcesso.Processos.RemoveAll(p => p.TempoRemanescente == 0);
    }

    static void RunFCFS(List<Processo> processos)
    {
        int currentTime = 0;
        foreach (var processo in processos)
        {
            if (processo.Ingresso > currentTime)
            {
                currentTime = processo.Ingresso;
            }
            Console.WriteLine($"Executando processo {processo.ProcessoId} de {currentTime} até {currentTime + processo.TempoRemanescente}");
            currentTime += processo.TempoRemanescente;
        }
    }

    static void ExecutarFCFS(List<Processo> processos)
    {
        int tempoTotal = 0;

        Console.WriteLine("\u001b[93mIngresso\u001b[0m | \u001b[93mPID\u001b[0m   | \u001b[93mTempo Restante\u001b[0m | \u001b[93mPrioridade Dinamica\u001b[0m");
        Console.WriteLine("--------------------------------------------------------");
        foreach (var processo in processos)
        {
            
            Console.WriteLine($"Executando o Processo {processo.ProcessoId}");
            Console.WriteLine($"Tempo de Chegada: {processo.Ingresso }, Tempo de Execução: {processo.TempoExecucao}");

            // Aguarde o tempo de chegada, se necessário
            if (tempoTotal < processo.Ingresso)
            {
                Console.WriteLine($"Aguardando {processo.Ingresso - tempoTotal} unidades de tempo");
                tempoTotal = processo.Ingresso;
            }

            // Execute o processo
            tempoTotal += processo.TempoExecucao;
            Console.WriteLine($"Tempo Total até agora: {tempoTotal}");
            Console.WriteLine();
        }
    }


    static void ImprimirInfos(int cycle, ListaProcesso listaProcesso, Processo processo)
    {
        Console.WriteLine($"\u001b[93mCiclo:\u001b[0m \u001b[92m{cycle}\u001b[0m");
        Console.WriteLine($"\u001b[93mProcesso ID:\u001b[0m {ANSI_CYAN}{processo.ProcessoId}{ANSI_RESET}");
        Console.WriteLine($"\u001b[93mProcesso Qtd.:\u001b[0m {listaProcesso.Processos.Count}\n");
    }

    static void Imprimir(ListaProcesso listaProcesso)
    {
        Console.WriteLine("\u001b[93mIngresso\u001b[0m | \u001b[93mPID\u001b[0m   | \u001b[93mTempo Restante\u001b[0m | \u001b[93mPrioridade Dinamica\u001b[0m");
        Console.WriteLine("--------------------------------------------------------");
        foreach (Processo processo in listaProcesso.Processos)
        {
            Console.WriteLine($"{processo.Ingresso}        | {ANSI_CYAN}{processo.ProcessoId}{ANSI_RESET}     | {processo.TempoRemanescente.ToString("00")}             | {processo.PrioridadeDinamica.ToString("00")}");
        }
        Console.WriteLine("--------------------------------------------------------\n");
    }
}

