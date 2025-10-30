using UnityEngine;

public class BotaoComprarSoldado : MonoBehaviour
{
    [Header("Config de Compra")]
    private const string TIPO_MOEDA = "Ouro";
    private const int CUSTO_OURO = 50;
   


    public GameObject soldadoPrefab;

    [Header("Config UI")]
    public GameObject painelDetalhes;

    public void ComprarSoldado()
    {

        if (RecursosManager.Instance.RemoverRecurso(TIPO_MOEDA, CUSTO_OURO))
        {
            SpawnarSoldado();
        }
        else
        {
            Debug.Log("Sem recursos para comprar coletora.");
        }
    }

    private void SpawnarSoldado()
    {

        Transform spawnPoint = VilaEstruturasBasicas.Instance.LocalizacaoSpawLenhador();

        if (spawnPoint != null)
        {

            GameObject novaColetora = Instantiate(soldadoPrefab, spawnPoint.position, Quaternion.identity);


            novaColetora.name = "Soldado " + Time.time;

            Debug.Log("Novo Soldado spawnado!");
        }
        else
        {
            Debug.Log("Sem ponto de spawn para o soldado.");
        }
    }
}