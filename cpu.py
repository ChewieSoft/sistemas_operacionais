import threading
import time

class GerenciaProcessos:
    def __init__(self):
        self.processos_prontos = []
        self.processos_bloqueados = []
        self.quantum = 10

    def main(self):
        # Inicializa o monitor de processos bloqueados em uma nova thread
        monitor_thread = threading.Thread(target=self.monitora_processos_bloqueados)
        monitor_thread.start()

        while True:
            if self.processos_prontos:
                processo = self.processos_prontos.pop(0)
                self.prioriza_processo(processo)
                self.processos_prontos.append(processo)  # Adiciona o processo novamente no fim da fila

    def prioriza_processo(self, processo):
        processo.status_processo = "Executando"
        tempo = 0

        while tempo < self.quantum:
            # Método que executa as próximas instruções do processo
            self.executa_instrucoes(processo)

            # Se a instrução atual não utilizar a CPU
            if self.instrucao_nao_utiliza_cpu(processo):
                processo.status_processo = "Bloqueado"
                self.processos_bloqueados.append(processo)  # Adiciona o processo na fila de bloqueados
                self.sair_processo()
                return

            tempo += 1

        processo.status_processo = "Pronto"
        self.processos_prontos.append(processo)  # Adiciona o processo novamente no fim da fila

    def monitora_processos_bloqueados(self):
        while True:
            processos_desbloqueados = []

            for processo in self.processos_bloqueados:
                if self.instrucao_requer_cpu(processo):
                    processo.status_processo = "Pronto"
                    processos_desbloqueados.append(processo)

            for processo in processos_desbloqueados:
                self.processos_bloqueados.remove(processo)
                self.processos_prontos.append(processo)  # Adiciona o processo novamente no fim da fila de prontos

    def executa_instrucoes(self, processo):
        print(f"Executando instruções para o processo {processo.Id}")
        # Simula a execução de instruções (substitua pelo código real)
        time.sleep(0.1)

    def instrucao_nao_utiliza_cpu(self, processo):
        print(f"Verificando se a instrução do processo {processo.Id} não utiliza a CPU")
        # Simula a verificação (substitua pelo código real)
        return False

    def instrucao_requer_cpu(self, processo):
        print(f"Verificando se a instrução do processo {processo.Id} requer a CPU")
        # Simula a verificação (substitua pelo código real)
        return True

    def sair_processo(self):
        print("Liberando a CPU para outro processo")
        # Simula a liberação da CPU (substitua pelo código real)
        time.sleep(0.1)

if __name__ == "__main__":
    gerenciador = GerenciaProcessos()
    gerenciador.main()
