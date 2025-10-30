using UnityEngine;
using System.Collections.Generic;

public class VilaEstruturasBasicas : MonoBehaviour
{
    public static VilaEstruturasBasicas Instance { get; private set; }

    [Header("Estuturas Centrais")]
    public Transform refeitorio;
    public Transform casa;
    public Transform deposito;
    public Transform castelo;

    [Header("FontesColeta")]
    public Transform fonteMadeira;
    public Transform fonteOuro;
    public Transform fontePedra;
    public Transform fonteFerro;
    

    [Header("Pontos de Spawn")]
    public Transform spawnLenhador;

    private void Awake()
    {
       if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void RegistrarEstrutura(string key, Transform estruturaTransform)
    {
        switch (key)
        {
            case "Refeitorio":
                refeitorio = estruturaTransform;
                break;
            case "Casa":
                casa = estruturaTransform;
                break;
            case "Deposito":
                deposito = estruturaTransform;
                break;
            case "Castelo":
                castelo = estruturaTransform;
                break;
            case "Madeira":
                fonteMadeira = estruturaTransform;
                break;
            case "Ouro":
                fonteOuro = estruturaTransform;
                break;
            case "Pedra":
                fontePedra = estruturaTransform;
                break;
            case "Ferro":
                fonteFerro = estruturaTransform;
                break;
               
        }
    }
    public Vector3 Localizacao(string key)
    {
        switch (key)
        {
            case "Refeitorio": return refeitorio != null ? refeitorio.position : Vector3.zero;
            case "Casa": return casa != null ? casa.position : Vector3.zero;
            case "Deposito": return deposito != null ? deposito.position : Vector3.zero;
            case "Castelo": return castelo != null ? castelo.position : Vector3.zero;
            case "Madeira": return fonteMadeira != null ? fonteMadeira.position : Vector3.zero;
            case "Ouro": return fonteOuro != null ? fonteOuro.position : Vector3.zero;
            case "Pedra": return fontePedra != null ? fontePedra.position : Vector3.zero;
            case "Ferro": return fonteFerro != null ? fonteFerro.position : Vector3.zero;
            case "Frutas": return Vector3.zero;

            default: return Vector3.zero;
        }
    }
    public Transform LocalizacaoSpawLenhador()
    {
        if(spawnLenhador != null)
        {
            return spawnLenhador;
        }
        return null;
    }
}