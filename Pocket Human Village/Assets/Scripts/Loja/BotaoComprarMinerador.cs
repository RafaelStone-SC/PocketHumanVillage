using UnityEngine;

public class BotaoComprarMinerador : MonoBehaviour
{
    [Header("Config de Compra")]
    private const string TIPO_MOEDA = "Ouro";
    private const int CUSTO_OURO = 30;
    public GameObject mineradorPrefab;

    [Header("Config UI")]
    public GameObject painelDetalhes;

    public void ComprarMinerador()
    {
        if (RecursosManager.Instance.RemoverRecurso(TIPO_MOEDA, CUSTO_OURO))
        {
            SpawnarMinerador();
        }
        else
        {
            Debug.Log("Sem recursos");
        }
    }
    private void SpawnarMinerador()
    {
        Transform spawnPoint = VilaEstruturasBasicas.Instance.LocalizacaoSpawLenhador();
        if (spawnPoint != null)
        {
            GameObject novoMinerador = Instantiate(mineradorPrefab, spawnPoint.position, Quaternion.identity);
            Debug.Log("Novo Minerador");
        }
        else
        {
            Debug.Log("Sem ponto de spawn");
        }
    }
}
