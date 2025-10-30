using UnityEngine;
using UnityEngine.AI;

public class Minerador : MonoBehaviour
{
    public Character dadosPersonagem;
    private MineradorBT btMinerador;
    public Transform localItem;
    public Transform localPicareta;

    public GameObject itemCarregado { get; set; } = null;
    public GameObject ouroPrefab;
    public GameObject picaretaPrefab;

    [HideInInspector]
    public GameObject targetMina;
    public GameObject picaretaInstanciada;

    public NavMeshAgent agentMinerador;
    public Vector3 destinoAtual { get; private set; }


    void Start()
    {
        agentMinerador = GetComponent<NavMeshAgent>();
        dadosPersonagem = new Character(gameObject.name, this);
        btMinerador = new MineradorBT(dadosPersonagem);
        SelecionarPersonagem seletor = GetComponent<SelecionarPersonagem>();
        if (seletor != null)
        {
            
            seletor.dadosDoPersonagem = dadosPersonagem;
        }
    }


    void Update()
    {
        if (dadosPersonagem == null || btMinerador == null) return;

        if (agentMinerador != null && agentMinerador.velocity.sqrMagnitude > 0.01f)
        {
            dadosPersonagem.Fome += Time.deltaTime * 0.1f;
            dadosPersonagem.Energia -= Time.deltaTime * 0.1f;
        }

        dadosPersonagem.Energia = Mathf.Max(0, dadosPersonagem.Energia);
        dadosPersonagem.Fome = Mathf.Max(0, dadosPersonagem.Fome);
        dadosPersonagem.Energia = Mathf.Min(100f, dadosPersonagem.Energia);

        btMinerador.Updade();
    }

    public void Novodestino(Vector3 novoDestino)
    {
        if (agentMinerador == null || !agentMinerador.isActiveAndEnabled)
        {
            
            return;
        }

        destinoAtual = novoDestino;
        agentMinerador.isStopped = false;

        if (!agentMinerador.SetDestination(novoDestino))
        {
            Debug.Log("Sem rota");
        }
    }
}