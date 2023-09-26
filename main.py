from time import sleep
from random import random
from threading import Thread
from queue import Queue

def produtor(buffer):
    print('Produtor: Rodando')
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
        print(f'>produtor adicionado {item}')
    # exibe que não há mais itens
    buffer.put(None)
    print('Produtor: finalizado')

# consumidor
def consumidor(buffer):
    print('Consumidor: Rodando')
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
        print(f'>consumidor pegou {item}')
    # concluido
    print('Consumidor: finalizado')

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