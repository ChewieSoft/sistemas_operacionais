import random
import time

ESTADOS = ["Pronto", "Executando", "Bloqueado", "Terminado"]

class Processo:
    def __init__(self, pid):
        self.pid = pid
        self.estado = random.choice(ESTADOS)

    def __str__(self):
        return f"Processo {self.pid} - Status: {self.estado}"

def simular_transicao(processo):
    if processo.estado == "Pronto":
        if random.random() < 0.5:
            processo.estado = "Executando"
        else:
            processo.estado = "Bloqueado"
    elif processo.estado == "Executando":
        if random.random() < 0.3:
            processo.estado = "Bloqueado"
        else:
            processo.estado = "Terminado"
    elif processo.estado == "Bloqueado":
        processo.estado = "Pronto"

def main():
    num_processos = 5
    processos = [Processo(i) for i in range(1, num_processos + 1)]

    while any(processo.estado != "Terminado" for processo in processos):
        print("\nEstado dos processos:")
        for processo in processos:
            print(processo)

        print("\nSimulando transições de estados...\n")
        for processo in processos:
            simular_transicao(processo)

        time.sleep(1)

if __name__ == "__main__":
    main()
