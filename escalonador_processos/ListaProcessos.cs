using System;
using System.Collections.Generic;
using System.IO;

namespace escalonador_processos
{
    public class ListaProcessos
    {
        public List<Processo> Processos { get; private set; }

        public ListaProcessos()
        {
            Processos = new List<Processo>();
        }

        public void CarregarDados(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);

            if (lines.Length < 2 || !int.TryParse(lines[0], out int totalProcessos))
            {
                throw new FormatException("Formato inválido do arquivo.");
            }

            for (int i = 1; i <= totalProcessos; i++)
            {
                string[] dados = lines[i].Split(' ');
                if (dados.Length != 3) throw new FormatException("Formato inválido para os dados do processo.");

                Processo processo = new Processo
                {
                    TarefaId = new Random().Next(1000),
                    Ingresso = int.Parse(dados[0]),
                    TempoExecucao = int.Parse(dados[1]),
                    PrioridadeEstatica = int.Parse(dados[2])
                };

                processo.TempoRemanescente = processo.TempoExecucao;
                processo.PrioridadeDinamica = processo.PrioridadeEstatica;

                Processos.Add(processo);
            }
        }
    }
}
