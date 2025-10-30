using UnityEngine;

public class BotaoComprarLenhador : MonoBehaviour
{
    [Header("Config de Compra")]
    private const string TIPO_MOEDA = "Ouro";
    private const int CUSTO_OURO = 20;
    public GameObject lenhadorPrefab;

    [Header("Config UI")]
    public GameObject painelDetalhes;

    public void ComprarLenhador()
    {
        if (RecursosManager.Instance.RemoverRecurso(TIPO_MOEDA, CUSTO_OURO))
        {
            SpawnarLenhador();
        }
        else
        {
            Debug.Log("Sem recursos");
        }
    }
    private void SpawnarLenhador()
    {
        Transform spawnPoint = VilaEstruturasBasicas.Instance.LocalizacaoSpawLenhador();
        if(spawnPoint != null)
        {
            GameObject novoLenhador = Instantiate(lenhadorPrefab, spawnPoint.position, Quaternion.identity);
            Debug.Log("Novo Lenhador");
        }
        else
        {
            Debug.Log("Sem ponto de spawn");
        }
    }
}
