from time import sleep
from random import random
from threading import Thread
from queue import Queue


# Função para imprimir mensagens formatadas
def imprimir_mensagem_colorida(mensagem, cor):
    cores = {
        "vermelho": "\033[91m",
        "verde": "\033[92m",
        "azul": "\033[94m",
        "reset": "\033[0m",
    }
    return f"{cores[cor]}{mensagem}{cores['reset']}"

def produtor(buffer):
    print(imprimir_mensagem_colorida('Produtor: INICIALIZADO! \U0001F680', 'verde'))
    # gera itens
    for i in range(10):
        # gera um valor
        valor = random()
        # bloqueia, para simular esforço
        sleep(valor)
        # cria uma tupla
        item = (i, valor)
        # adiciona ao buffer
        buffer.put(item)
        # exibe o progresso
        print(imprimir_mensagem_colorida(f'> Produtor adicionou => {item}', 'azul'))
    # exibe que não há mais itens
    buffer.put(None)
    print(imprimir_mensagem_colorida('Produtor: FINALIZADO! \U0001F3C1', 'verde'))

# consumidor
def consumidor(buffer):
    print(imprimir_mensagem_colorida('Consumidor: INICIALIZADO! \U0001F680', 'verde'))
    # consome items
    while True:
        # pega uma unidade de trabalho
        item = buffer.get()
        # condição de checagem para parar
        if item is None:
            break
       # bloqueia, para simular esforço
        sleep(item[1])
        # reporte
        print(imprimir_mensagem_colorida(f'> Consumidor removeu <= {item}', 'vermelho'))
    # concluido
    print(imprimir_mensagem_colorida('Consumidor: FINALIZADO! \U0001F3C1', 'verde'))

# cria a fila compartilhada
buffer = Queue()
# inicia o consumidor
consumidor = Thread(target=consumidor, args=(buffer,))
consumidor.start()
# inicia o produtor
produtor = Thread(target=produtor, args=(buffer,))
produtor.start()
# espera por todas as threads finalizarem
produtor.join()
consumidor.join()