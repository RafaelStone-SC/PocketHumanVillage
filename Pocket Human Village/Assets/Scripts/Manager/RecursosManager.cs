using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecursosManager : MonoBehaviour
{
    public GameObject mensagemUI;
    public float tempoDeExibicaoUI = 2.0f;
    private Coroutine coroutineUI;
    public static RecursosManager Instance { get; private set; }

    private Dictionary<string, int> recursos = new Dictionary<string, int>();

    private Dictionary<string, int> custoCasa = new Dictionary<string, int>
    {
        {"Madeira",50},
        {"Ouro", 30 }
    };

    private void Awake()
    {
        if(mensagemUI != null)
        {
            mensagemUI.SetActive(false);
        }
        if (Instance == null)
        {
            Instance = this;
            RecursosStatusIniciais();
            
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void RecursosStatusIniciais()
    {
        recursos.Add("Madeira", 100);
        recursos.Add("Pedra", 0);
        recursos.Add("Ferro", 0);
        recursos.Add("Ouro", 300);
        recursos.Add("Comida", 5);

    }

    public void AddRecurso(string tipo, int quantidade)
    {
        if (recursos.ContainsKey(tipo))
        {
            recursos[tipo] += quantidade;
        }
        if (UIRecursos.Instance != null)
        {
            UIRecursos.Instance.AtualizarHUD();
        }
        Debug.Log("Add recurso");
    }
    public bool RemoverRecurso(string tipo, int quantidade)
    {
        if(recursos.ContainsKey(tipo) && recursos[tipo] >= quantidade)
        {
            recursos[tipo] -= quantidade;
            return true;
        }
        return false;
    }
    public int Valorrecurso(string tipo)
    {
        return recursos.ContainsKey(tipo) ? recursos[tipo] : 0;
    }

    public bool PodePagar(Dictionary<string, int> custos)
    {
        foreach (var custo in custos)
        {
            if(!recursos.ContainsKey(custo.Key) || recursos[custo.Key] < custo.Value)
            {
                return false;
            }

            
        }
        return true;
    }

    public bool RemoverRecursos(Dictionary<string, int> custos)
    {
        if (PodePagar(custos))
        {
            foreach (var custo in custos)
            {
                recursos[custo.Key] -= custo.Value;
            }
            if (UIRecursos.Instance != null)
            {
                UIRecursos.Instance.AtualizarHUD();
            }
            return true;
        }
        return false;
    }
    public bool IniciarTransacaoCasa()
    {
        return PodePagar(custoCasa);
    }
    public void FinalizarTransacaoCasa(Vector3 posicaoFinal)
    {
        if (RemoverRecursos(custoCasa))
        {
            Debug.Log("Casa Comprada");
        }
        else
        {
            Debug.Log("Casa nao comprada");
        }
    }
    public void MensagemUI()
    {
        if (mensagemUI == null)
        {
            return;
        }
        if (coroutineUI != null)
        {
            StopCoroutine(coroutineUI);
        }
        mensagemUI.SetActive(true);
        coroutineUI = StartCoroutine(DesligarMensagemUI());
    }
    private IEnumerator DesligarMensagemUI()
    {
        yield return new WaitForSeconds(tempoDeExibicaoUI);
        if (mensagemUI != null)
        {
            mensagemUI.SetActive(false);
        }
        coroutineUI = null;
    }
}