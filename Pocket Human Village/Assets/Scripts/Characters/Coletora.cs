using UnityEngine;
using UnityEngine.AI;

public class Coletora : MonoBehaviour
{
    public Character dadosPersonagem;
    private ColetoraBT btColetora;
    public Transform localItem;

    public GameObject itemCarregado { get; set; } = null;
    public GameObject comidaPrefab; 

    [HideInInspector]
    public GameObject targetFonteFrutas;

    public NavMeshAgent agentColetora;
    public Vector3 destinoAtual { get; private set; }


    void Start()
    {
        agentColetora = GetComponent<NavMeshAgent>();
        dadosPersonagem = new Character(gameObject.name, this);
        btColetora = new ColetoraBT(dadosPersonagem);

        
        SelecionarPersonagem seletor = GetComponent<SelecionarPersonagem>();
        if (seletor != null) seletor.dadosDoPersonagem = dadosPersonagem;
    }


    void Update()
    {
        if (dadosPersonagem == null || btColetora == null) return;

        
        if (agentColetora != null && agentColetora.velocity.sqrMagnitude > 0.01f)
        {
            dadosPersonagem.Fome += Time.deltaTime * 0.1f;
            dadosPersonagem.Energia -= Time.deltaTime * 0.1f;
        }

        dadosPersonagem.Energia = Mathf.Max(0, dadosPersonagem.Energia);
        dadosPersonagem.Fome = Mathf.Max(0, dadosPersonagem.Fome);
        dadosPersonagem.Energia = Mathf.Min(100f, dadosPersonagem.Energia);

        btColetora.Updade();
    }

    public void Novodestino(Vector3 novoDestino)
    {
        if (agentColetora == null || !agentColetora.isActiveAndEnabled)
        {
            Debug.LogWarning($"[NavMesh] Coletora {gameObject.name} inativa.");
            return;
        }

        destinoAtual = novoDestino;
        agentColetora.isStopped = false;

        if (!agentColetora.SetDestination(novoDestino))
        {
            Debug.Log("Sem rota");
        }
    }
}