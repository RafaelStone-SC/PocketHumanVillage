using UnityEngine;

public class BotaoComprarConstrutor : MonoBehaviour
{
    [Header("Config de Compra")]
    private const string TIPO_MOEDA = "Ouro";
    private const int CUSTO_OURO = 15;


    public GameObject construtorPrefab;

    [Header("Config UI")]
    public GameObject painelDetalhes;

    public void ComprarConstrutor()
    {

        if (RecursosManager.Instance.RemoverRecurso(TIPO_MOEDA, CUSTO_OURO))
        {
            SpawnarConstrutor();
        }
        else
        {
            Debug.Log("Sem recursos para comprar Construtor.");
        }
    }

    private void SpawnarConstrutor()
    {

        Transform spawnPoint = VilaEstruturasBasicas.Instance.LocalizacaoSpawLenhador();

        if (spawnPoint != null)
        {

            GameObject novoConstrutor = Instantiate(construtorPrefab, spawnPoint.position, Quaternion.identity);


            novoConstrutor.name = "Construtor " + Time.time;

            Debug.Log("Novo Construtor spawnado!");
        }
        else
        {
            Debug.Log("Sem ponto de spawn para a Construtor.");
        }
    }
}
