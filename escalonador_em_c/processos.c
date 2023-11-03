#include "processos.h"

cycle = 0;
run = 1;

//Descritivo
struct listaProcesso {
	int qtd;
	struct processo* processos;
};

struct processo {
	struct processo* prox;
	//Conteudo util
	int tarefaId;
	int tempoExecucao;
	int tempoRemanescente;
	int ingresso;
	int proridadeEstatica;
	int proridadeDinamica;
};

//vai finalizar o programa
bool finishHim(ListaProcesso* lista) {
	if (lista->qtd == 0) {
		return true;
	}
	return false;
}

ListaProcesso* inicializaListaProcesso() {
	ListaProcesso* lista = malloc(sizeof(ListaProcesso));
	lista->qtd = 0;
	lista->processos = NULL;
	return lista;
}

void carregaDados(ListaProcesso* lista) {

	//Leitura do arquivo txt
	FILE* arquivo = fopen("Processos.txt", "r");
	if (arquivo == NULL) {
		exit(1);
	}

	//Primeira linha define a quantidade de processos a serem listados
	fscanf_s(arquivo, "%d\n", &lista->qtd);
	int i;

	Processo* processo = NULL;
	for (i = 0; i < lista->qtd; i++) {
		Processo* processo_aux = (Processo*)malloc(sizeof(Processo));

		//LER VALORES => ENTRADA - TEMPO EXECUCAO - PRIORIDADE
		fscanf(arquivo, "%d %d %d\n", &processo_aux->ingresso, &processo_aux->tempoExecucao, &processo_aux->proridadeEstatica);

		//ATRIBUICAO DOS VALORES RESTANTES
		processo_aux->tarefaId = rand() % 1000;
		processo_aux->proridadeDinamica = processo_aux->proridadeEstatica;
		processo_aux->tempoRemanescente = processo_aux->tempoExecucao;

		if (i == 0) {
			lista->processos = processo_aux;
		}
		else {
			processo->prox = processo_aux;
		}

		if (i == lista->qtd - 1)
			processo_aux->prox = NULL;

		processo = processo_aux;
	}
	fclose(arquivo);
}

void inserirProcessos(ListaProcesso* listaRaw, ListaProcesso* lista) {
	Processo* processo = listaRaw->processos;
	while (processo != NULL) {
		if (processo->ingresso == cycle) {
			Processo* aux = malloc(sizeof(Processo));
			aux->prox = NULL;
			aux->tarefaId = processo->tarefaId;
			aux->tempoExecucao = processo->tempoExecucao;
			aux->tempoRemanescente = processo->tempoRemanescente;
			aux->ingresso = processo->ingresso;
			aux->proridadeEstatica = processo->proridadeEstatica;
			aux->proridadeDinamica = processo->proridadeDinamica;

			if (lista->processos == NULL) {
				lista->processos = aux;
				lista->qtd++;
			}
			else {
				Processo* aux2 = lista->processos;
				while (aux2->prox != NULL) {
					aux2 = aux2->prox;
				}
				aux2->prox = aux;
				lista->qtd++;
			}

		}
		processo = processo->prox;
	}
}

Processo* selecao(ListaProcesso* lista) {
	//DEFINIR
	Processo* aux = lista->processos;
	Processo* retorno = NULL;
	int prioridade = -1;

	if (lista->qtd == 1) {
		return lista->processos;
	}

	while (aux != NULL) {
		if (aux->proridadeDinamica > prioridade) {
			prioridade = aux->proridadeDinamica;
			retorno = aux;
		}
		else if (aux->proridadeDinamica == prioridade) {
			if (retorno->tempoRemanescente == aux->tempoRemanescente) {
				retorno = retorno->tarefaId < aux->tarefaId ? retorno : aux;
				break;
			}
			else {
				retorno = retorno->tempoRemanescente < aux->tempoRemanescente ? retorno : aux;
				break;
			}
		}
		aux = aux->prox;
	}
	return retorno;
}

void executar(Processo* processo) {
	processo->tempoRemanescente--;
}

void excluirTerminado(ListaProcesso* lista, Processo* processo) {
	Processo* aux = lista->processos;
	if (aux == processo) {
		lista->processos = processo->prox;
	}
	else {
		while (aux->prox != processo) {
			aux = aux->prox;
		}
		aux->prox = aux->prox->prox;
	}
	lista->qtd--;
}

//Alterar o tempo restante do processo atual e prioridade dinamica dos itens que n foram executados nesse ciclo
void manutencao(Processo* processo, ListaProcesso* lista) {
	Processo* aux = lista->processos;
	while (aux != NULL) {
		if (aux != processo) {
			aux->proridadeDinamica++;
		}
		if (aux->tempoRemanescente == 0) {
			excluirTerminado(lista, aux);
		}
		aux = aux->prox;
	}
}

//imprimir ciclo e processo atual
void imprimirInfos(int ciclo, ListaProcesso* lista, Processo* processo) {
	printf("Ciclo: %d\n", cycle);
	printf("Processo ID: %d\n", processo->tarefaId);
	printf("Processo Qtd.: %d\n\n", lista->qtd);
}

void imprimir(ListaProcesso* lista) {
	Processo* processo = lista->processos;
	printf("Ingresso | PID	| Tempo Restante | Prioridade Dinamica\n");
	while (processo != NULL) {
		printf("%d	 | %d	| %d		 | %d\n", processo->ingresso, processo->tarefaId, processo->tempoRemanescente, processo->proridadeDinamica);
		processo = processo->prox;
	}
	printf("-----------------------------------------------------------------------------\n\n");
}
