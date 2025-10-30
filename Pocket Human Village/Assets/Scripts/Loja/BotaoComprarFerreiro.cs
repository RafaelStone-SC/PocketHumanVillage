using UnityEngine;

public class BotaoComprarFerreiro : MonoBehaviour
{
    [Header("Config de Compra")]
    private const string TIPO_MOEDA = "Ouro";
    private const int CUSTO_OURO = 20;
    public GameObject ferreiroPrefab;

    [Header("Config UI")]
    public GameObject painelDetalhes;

    public void ComprarFerreiro()
    {
        if (RecursosManager.Instance.RemoverRecurso(TIPO_MOEDA, CUSTO_OURO))
        {
            SpawnarFerreiro();
        }
        else
        {
            Debug.Log("Sem recursos");
        }
    }
    private void SpawnarFerreiro()
    {
        Transform spawnPoint = VilaEstruturasBasicas.Instance.LocalizacaoSpawLenhador();
        if (spawnPoint != null)
        {
            GameObject novoFerreiro = Instantiate(ferreiroPrefab, spawnPoint.position, Quaternion.identity);
            Debug.Log("Novo Ferreiro");
        }
        else
        {
            Debug.Log("Sem ponto de spawn");
        }
    }
}
