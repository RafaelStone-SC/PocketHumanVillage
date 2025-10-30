using UnityEngine;
using UnityEngine.AI;

public class Ferreiro : MonoBehaviour
{
    public Character dadosPersonagem;
    private FerreiroBT btFerreiro;
    public Transform localItem;
    public Transform localFerramenta;

    public GameObject itemCarregado { get; set; } = null;
    public GameObject ferroPrefab;
    public GameObject picaretaReforcadaPrefab;

    [HideInInspector]
    public GameObject targetFonteFerro;
    public GameObject ferramentaInstanciada;

    public NavMeshAgent agentFerreiro;
    public Vector3 destinoAtual { get; private set; }


    void Start()
    {
        agentFerreiro = GetComponent<NavMeshAgent>();
        dadosPersonagem = new Character(gameObject.name, this);
        btFerreiro = new FerreiroBT(dadosPersonagem);

        SelecionarPersonagem seletor = GetComponent<SelecionarPersonagem>();
        if (seletor != null) seletor.dadosDoPersonagem = dadosPersonagem;
    }


    void Update()
    {
        if (dadosPersonagem == null || btFerreiro == null) return;

        if (agentFerreiro != null && agentFerreiro.velocity.sqrMagnitude > 0.01f)
        {
            dadosPersonagem.Fome += Time.deltaTime * 0.5f;
            dadosPersonagem.Energia -= Time.deltaTime * 2.0f;
        }

        dadosPersonagem.Energia = Mathf.Max(0, dadosPersonagem.Energia);
        dadosPersonagem.Fome = Mathf.Max(0, dadosPersonagem.Fome);
        dadosPersonagem.Energia = Mathf.Min(100f, dadosPersonagem.Energia);

        btFerreiro.Updade();
    }

    public void Novodestino(Vector3 novoDestino)
    {
        if (agentFerreiro == null || !agentFerreiro.isActiveAndEnabled)
        {
            
            return;
        }

        destinoAtual = novoDestino;
        agentFerreiro.isStopped = false;

        if (!agentFerreiro.SetDestination(novoDestino))
        {
            Debug.Log("Sem Rota");
        }
    }
}