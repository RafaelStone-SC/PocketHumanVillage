using UnityEngine;
using UnityEngine.AI;

public class Construtor : MonoBehaviour
{
    public Character dadosPersonagem;
    private ConstrutorBT construtorBT;
    public NavMeshAgent agentConstrutor;
    public Transform localFerramenta;      
    public GameObject marteloPrefab;        
    public GameObject ferramentaInstanciada;
    public EstruturaCasa targetConstrucao;
    public Vector3 destinoAtual { get; private set; }

     void Start()
    {
        agentConstrutor = GetComponent<NavMeshAgent>();
        dadosPersonagem = new Character(gameObject.name, this);
        construtorBT = new ConstrutorBT(dadosPersonagem);
        SelecionarPersonagem seletor = GetComponent<SelecionarPersonagem>();
        if (seletor != null) seletor.dadosDoPersonagem = dadosPersonagem;
    }
     void Update()
    {
        if(dadosPersonagem == null || construtorBT == null)
        {
            return;
        }
        if(agentConstrutor != null && agentConstrutor.velocity.sqrMagnitude > 0.01f)
        {
            dadosPersonagem.Fome += Time.deltaTime * 0.1f;
            dadosPersonagem.Energia -= Time.deltaTime * 0.1f;
        }
        construtorBT.Update();
    }

    public void NovoDestino(Vector3 novoDestino)
    {
        if(agentConstrutor == null || !agentConstrutor.isActiveAndEnabled)
        {
            return;
        }
        destinoAtual = novoDestino;
        agentConstrutor.isStopped = false;
        agentConstrutor.SetDestination(novoDestino);
    }
}
