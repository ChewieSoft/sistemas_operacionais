using escalonador;
using System;
using System.Collections.Generic;
using System;
using System.IO;
using System.Threading;

class Program
{
    private const int CYCLE_TIME = 200;
    public static int cycle = 0;

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
                string[] data = lines[contador].Split(' ');

                int ingresso = int.Parse(data[0]);
                int tempoExecucao = int.Parse(data[1]);
                int prioridadeEstatica = int.Parse(data[2]);

                Processo processo = new Processo(contador, tempoExecucao, ingresso, prioridadeEstatica);
                listaProcessoRaw.InserirProcesso(processo);
            }
        }
        catch (IOException e)
        {
            Console.WriteLine("Erro ao ler o arquivo: " + e.Message);
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

    static void ImprimirInfos(int cycle, ListaProcesso listaProcesso, Processo processo)
    {
        Console.WriteLine($"Ciclo: {cycle}");
        Console.WriteLine($"Processo ID: {processo.ProcessoId}");
        Console.WriteLine($"Processo Qtd.: {listaProcesso.Processos.Count}\n");
    }

    static void Imprimir(ListaProcesso listaProcesso)
    {
        Console.WriteLine("Ingresso | PID   | Tempo Restante | Prioridade Dinamica");
        foreach (Processo processo in listaProcesso.Processos)
        {
            Console.WriteLine($"{processo.Ingresso}        | {processo.ProcessoId}     | {processo.TempoRemanescente.ToString("00")}             | {processo.PrioridadeDinamica.ToString("00")}");
        }
        Console.WriteLine("--------------------------------------------------------\n");
    }
}

