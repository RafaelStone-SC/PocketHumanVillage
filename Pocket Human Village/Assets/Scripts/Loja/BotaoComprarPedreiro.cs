using UnityEngine;

public class BotaoComprarPedreiro : MonoBehaviour
{
    [Header("Config de Compra")]
    private const string TIPO_MOEDA = "Ouro";
    private const int CUSTO_OURO = 20;
    public GameObject pedreiroPrefab;

    [Header("Config UI")]
    public GameObject painelDetalhes;

    public void ComprarPedreiro()
    {
        if (RecursosManager.Instance.RemoverRecurso(TIPO_MOEDA, CUSTO_OURO))
        {
            SpawnarPedreiro();
        }
        else
        {
            Debug.Log("Sem recursos");
        }
    }
    private void SpawnarPedreiro()
    {
        Transform spawnPoint = VilaEstruturasBasicas.Instance.LocalizacaoSpawLenhador();
        if (spawnPoint != null)
        {
            GameObject novoPedreiro = Instantiate(pedreiroPrefab, spawnPoint.position, Quaternion.identity);
            Debug.Log("Novo Pedreiro");
        }
        else
        {
            Debug.Log("Sem ponto de spawn");
        }
    }
}
