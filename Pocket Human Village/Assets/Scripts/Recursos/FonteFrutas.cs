using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class FonteFrutas : MonoBehaviour
{
    [Header("Config Recurso")]
    public string tipoRecurso = "Frutas";
    public float tempoRegeneracao = 60f;

    [Header("Componentes")]
   
    public GameObject containerFrutas;

   
    private List<GameObject> frutasNaArvore = new List<GameObject>();

    private bool isAvailable = true;

   

    void Awake()
    {
       
        if (containerFrutas == null)
        {
            containerFrutas = gameObject;
        }

      
        foreach (Transform child in containerFrutas.transform)
        {
          
            frutasNaArvore.Add(child.gameObject);
        }

        if (frutasNaArvore.Count == 0)
        {
            Debug.Log("Sem frutas");
        }
    }

   
    public bool TemFrutasDisponiveis() => isAvailable;

  

    public void IniciarColeta()
    {
        if (!isAvailable) return;

        isAvailable = false;

        
        if (frutasNaArvore.Count > 0)
        {
            foreach (GameObject fruta in frutasNaArvore)
            {
                
                if (fruta != null)
                {
                    fruta.SetActive(false);
                }
            }
        }

        
        StartCoroutine(RegenerarFrutas());

        Debug.Log("Coletou frutas, regenerando");
    }

    private IEnumerator RegenerarFrutas()
    {
        yield return new WaitForSeconds(tempoRegeneracao);

      
        if (frutasNaArvore.Count > 0)
        {
            foreach (GameObject fruta in frutasNaArvore)
            {
                
                if (fruta != null)
                {
                    fruta.SetActive(true);
                }
            }
        }

        isAvailable = true;
        Debug.Log("frutas voltaram");
    }
}