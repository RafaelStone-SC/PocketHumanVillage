using UnityEngine;
using UnityEngine.AI;

public class Pedreiro : MonoBehaviour
{
    public Character dadosPersonagem;
    private PedreiroBT btPedreiro;
    public Transform localItem;
    public Transform localFerramenta; 

    public GameObject itemCarregado { get; set; } = null;
    public GameObject pedraPrefab;
    public GameObject marteloPrefab; 

    [HideInInspector]
    public GameObject targetFontePedra;
    public GameObject ferramentaInstanciada;

    public NavMeshAgent agentPedreiro;
    public Vector3 destinoAtual { get; private set; }


    void Start()
    {
        agentPedreiro = GetComponent<NavMeshAgent>();
        dadosPersonagem = new Character(gameObject.name, this);
        btPedreiro = new PedreiroBT(dadosPersonagem);

        SelecionarPersonagem seletor = GetComponent<SelecionarPersonagem>();
        if (seletor != null) seletor.dadosDoPersonagem = dadosPersonagem;
    }


    void Update()
    {
        if (dadosPersonagem == null || btPedreiro == null) return;

     
        if (agentPedreiro != null && agentPedreiro.velocity.sqrMagnitude > 0.01f)
        {
            dadosPersonagem.Fome += Time.deltaTime * 0.1f;
            dadosPersonagem.Energia -= Time.deltaTime * 0.1f;
        }

        dadosPersonagem.Energia = Mathf.Max(0, dadosPersonagem.Energia);
        dadosPersonagem.Fome = Mathf.Max(0, dadosPersonagem.Fome);
        dadosPersonagem.Energia = Mathf.Min(100f, dadosPersonagem.Energia);

        btPedreiro.Updade();
    }

    public void Novodestino(Vector3 novoDestino)
    {
        if (agentPedreiro == null || !agentPedreiro.isActiveAndEnabled)
        {
           
            return;
        }

        destinoAtual = novoDestino;
        agentPedreiro.isStopped = false;

        if (!agentPedreiro.SetDestination(novoDestino))
        {
           
        }
    }
}