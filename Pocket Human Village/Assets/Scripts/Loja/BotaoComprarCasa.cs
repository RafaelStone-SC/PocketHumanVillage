using UnityEngine;
using System.Collections;

public class BotaoComprarCasa : MonoBehaviour
{
    public GameObject casaPrefab;  
   
    
  

    public void ComprarCasa()
    {
        Debug.Log("Comprando casa");

       
        Transform casaAtualTransform = VilaEstruturasBasicas.Instance.casa;

        if (casaAtualTransform != null)
        {
            EstruturaCasa casaData = casaAtualTransform.GetComponent<EstruturaCasa>();

            
            if (casaData != null && casaData.PrecisaDeConstrucao())
            {
                RecursosManager.Instance.MensagemUI();
                return;
            }
        }

       
        if (RecursosManager.Instance.IniciarTransacaoCasa())
        {
          
            if (Posicionamento.Instance != null)
            {
                Posicionamento.Instance.EntrarModoPosicionamento(casaPrefab);
            }
          
        }
        else
        {
            RecursosManager.Instance.MensagemUI();
        }
        
    }

  
   
}