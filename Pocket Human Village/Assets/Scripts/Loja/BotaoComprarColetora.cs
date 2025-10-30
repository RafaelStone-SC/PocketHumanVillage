using UnityEngine;

public class BotaoComprarColetora : MonoBehaviour
{
    [Header("Config de Compra")]
    private const string TIPO_MOEDA = "Ouro";
    private const int CUSTO_OURO = 15;

    
    public GameObject coletoraPrefab;

    [Header("Config UI")]
    public GameObject painelDetalhes;

    public void ComprarColetora()
    {
       
        if (RecursosManager.Instance.RemoverRecurso(TIPO_MOEDA, CUSTO_OURO))
        {
            SpawnarColetora();
        }
        else
        {
            Debug.Log("Sem recursos para comprar coletora.");
        }
    }

    private void SpawnarColetora()
    {
       
        Transform spawnPoint = VilaEstruturasBasicas.Instance.LocalizacaoSpawLenhador();

        if (spawnPoint != null)
        {
           
            GameObject novaColetora = Instantiate(coletoraPrefab, spawnPoint.position, Quaternion.identity);

            
            novaColetora.name = "Coletora " + Time.time;

            Debug.Log("Nova Coletora spawnada!");
        }
        else
        {
            Debug.Log("Sem ponto de spawn para a coletora.");
        }
    }
}