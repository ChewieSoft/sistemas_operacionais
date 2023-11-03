#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <stdlib.h>
#include <stdlib.h>
#include <stdbool.h>
#include <Windows.h>
#define CYCLE_TIME Sleep(200)

int cycle;
int run;

typedef struct processo Processo;
typedef struct listaProcesso ListaProcesso;

bool finishHim(ListaProcesso* lista);
ListaProcesso* inicializaListaProcesso();
void carregaDados(ListaProcesso* lista);
Processo* selecao(ListaProcesso* lista);
void executar(Processo* processo);
void excluirTerminado(ListaProcesso* lista, Processo* processo);
void manutencao(Processo* processo, ListaProcesso* lista);
void imprimirInfos(int ciclo, ListaProcesso* lista, Processo* processo);
void imprimir(ListaProcesso* lista);