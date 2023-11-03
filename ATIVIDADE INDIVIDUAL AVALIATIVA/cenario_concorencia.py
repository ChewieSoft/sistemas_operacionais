import threading


def imprimir_id(identificador):
    print(f"Thread {identificador} - ID: {threading.get_ident()}")


primeira_thread = threading.Thread(target=imprimir_id, args=(1,))
segunda_thread = threading.Thread(target=imprimir_id, args=(2,))
terceira_thread = threading.Thread(target=imprimir_id, args=(3,))


primeira_thread.start()
segunda_thread.start()
terceira_thread.start()

# Espera todas as threads terminarem
primeira_thread.join()
segunda_thread.join()
terceira_thread.join()

print("Programa principal finalizado.")
