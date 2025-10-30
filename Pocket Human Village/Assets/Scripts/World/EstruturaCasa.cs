using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;


public class EstruturaCasa : MonoBehaviour
{
    public int capacidadeMaxima = 1;
    private int ocupacaoAtual = 0;

    public int OcuapacaoAtual => ocupacaoAtual;

    public float tempoPorParte = 5.0f;
    public int totalDePartes = 9;

    public List<GameObject> partesDaCasa;

    public bool pronta = false;

    private float progressoDaParteAtual = 0f;
    private int indiceParteAtual = 0;

    public bool EstaCompleta => indiceParteAtual >= totalDePartes;

    private void Awake()
    {
        pronta = false;
        progressoDaParteAtual = 0f;
        indiceParteAtual = 0;
        for(int i = 0; i< partesDaCasa.Count; i++)
        {
            if(partesDaCasa[i] != null)
            {
                partesDaCasa[i].SetActive(i == 0);
            }
        }
    }

    public bool PrecisaDeConstrucao()
    {
        return !pronta && indiceParteAtual < totalDePartes;
    }
    public void ReceberTrabalho(float trabalho)
    {
        if (pronta)
        {
            return;
        }
        progressoDaParteAtual += Time.deltaTime;

        if(progressoDaParteAtual >= tempoPorParte)
        {
            progressoDaParteAtual = 0f;
            indiceParteAtual++;

            if(indiceParteAtual < totalDePartes)
            {
                if(partesDaCasa.Count > indiceParteAtual && partesDaCasa[indiceParteAtual] != null)
                {
                    partesDaCasa[indiceParteAtual].SetActive(true);

                }
            }
            else
            {
                FinalizarContrucao();
            }
        }
    }

    private void FinalizarContrucao()
    {
       pronta = true;
    }

    public bool TentarEntrar()
    {
        if(ocupacaoAtual < capacidadeMaxima)
        {
            ocupacaoAtual++;
            Debug.Log("Alguem veio descansar");
            return true;
        }
        Debug.Log("Alguem tentou entrar e nao conseguiu");
        return false;
    } 
    public void Sair()
    {
        ocupacaoAtual = Mathf.Max(0, ocupacaoAtual - 1);
        Debug.Log("Alguem saiu da casa");
    }
}
