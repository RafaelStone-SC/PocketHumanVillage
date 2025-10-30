using UnityEngine;
using UnityEngine.AI;

public class Soldado : MonoBehaviour
{
    public Character dadosPersonagem;
    private SoldadoBT btSoldado;
    public NavMeshAgent agentSoldado;
   

    [Header("Status de Combate")]
    public int vida = 100;
    public int danoAtaque = 10;
    public float alcanceAtaque = 2.0f;
    public float tempoEntreAtaques = 1.0f;
    public float tempoDoCooldown = 0f;

    [Header("Ferramenta/Visual")]
    public GameObject armaPrefab;
    public Transform localArma;
    public GameObject armaInstanciada;

    
    [HideInInspector]
    public GameObject targetInimigo = null;

    public Vector3 destinoAtual { get; private set; }

    void Start()
    {
        agentSoldado = GetComponent<NavMeshAgent>();
       

       
        dadosPersonagem = new Character(gameObject.name, this);
        btSoldado = new SoldadoBT(dadosPersonagem);

        if (GetComponent<SelecionarPersonagem>() is SelecionarPersonagem seletor)
        {
            seletor.dadosDoPersonagem = dadosPersonagem;
            seletor.characterName = "Soldado";
        }

        if (armaInstanciada != null) armaInstanciada.SetActive(false);
    }

    void Update()
    {
        if (dadosPersonagem == null || btSoldado == null) return;

      
        if (agentSoldado != null && agentSoldado.velocity.sqrMagnitude > 0.01f)
        {
            dadosPersonagem.Fome += Time.deltaTime * 0.05f;
            dadosPersonagem.Energia -= Time.deltaTime * 0.05f;
        }

        tempoDoCooldown -= Time.deltaTime;

        btSoldado.Update();

        dadosPersonagem.Vida = vida;
    }

    public void NovoDestino(Vector3 novoDestino)
    {
        if (agentSoldado == null || !agentSoldado.isActiveAndEnabled) return;
        destinoAtual = novoDestino;
        agentSoldado.isStopped = false;
        agentSoldado.SetDestination(novoDestino);
    }

    public void ReceberDano(int dano)
    {
        vida -= dano;
        vida = Mathf.Max(0, vida);

        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void AtivarArma(bool ativo)
    {
       
        if (armaInstanciada == null && ativo && armaPrefab != null && localArma != null)
        {
            armaInstanciada = Instantiate(armaPrefab, localArma.position, localArma.rotation, localArma);
            
        }

        if (armaInstanciada != null)
        {
           
            armaInstanciada.SetActive(ativo);

           
            if (armaInstanciada.TryGetComponent<BalancarMachado>(out var balanco))
            {
                balanco.enabled = ativo;
            }
            
        }
    }

    public bool ProntoParaAtacar() => tempoDoCooldown <= 0;
}