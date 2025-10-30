using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Linq;

public class Inimigo : MonoBehaviour
{
   
    public NavMeshAgent agentInimigo;
    public Animator anim;
    
    public int vida = 50;
    public int danoAtaque = 5;
    public float alcanceAtaque = 2.5f;
    public float tempoEntreAtaques = 2.0f;
    private float cooldownTimer = 0f;

    
    [HideInInspector] public Castelo targetCastelo;
    [HideInInspector] public GameObject targetSoldado; 
    [HideInInspector] public bool podeMover = false; 

    
    private const string ANIM_ANDAR = "Andar";
    private const string ANIM_ATACAR = "Ataque";


    void Awake()
    {
        agentInimigo = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

        
        if (VilaEstruturasBasicas.Instance != null && VilaEstruturasBasicas.Instance.castelo != null)
        {
            VilaEstruturasBasicas.Instance.castelo.TryGetComponent<Castelo>(out targetCastelo);
        }
        
    }

    void Start()
    {
       
        if (agentInimigo != null && agentInimigo.enabled)
        {
            agentInimigo.Warp(transform.position);
            StartCoroutine(PermitirMovimentoAposDelay(0.1f));
        }
    }

    void Update()
    {
        if (targetCastelo == null || agentInimigo == null || !podeMover) return;

        cooldownTimer -= Time.deltaTime;

        
        LidarComAcoesDoInimigo();

        
        if (agentInimigo.velocity.sqrMagnitude > 0.01f)
        {
            SetAnimation(ANIM_ANDAR);
        }
    }

    
    private void LidarComAcoesDoInimigo()
    {
        
        targetSoldado = EncontrarSoldadoMaisProximo();

        if (targetSoldado != null)
        {
            
            MoverOuAtacar(targetSoldado.transform.position, true);
        }
        else
        {
           
            MoverOuAtacar(targetCastelo.transform.position, false);
        }
    }

    private void MoverOuAtacar(Vector3 destino, bool isTargetSoldier)
    {
        float distancia = Vector3.Distance(transform.position, destino);

       
        if (distancia <= alcanceAtaque)
        {
            agentInimigo.isStopped = true;

            if (cooldownTimer <= 0)
            {
                
                Vector3 direcao = (destino - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direcao.x, 0, direcao.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);

                SetAnimation(ANIM_ATACAR);

                if (isTargetSoldier)
                {
                    targetSoldado.TryGetComponent<Soldado>(out Soldado soldadoAlvo);
                    AtacarSoldado(soldadoAlvo);
                }
                else
                {
                    AtacarCastelo();
                }
            }
        }
        
        else
        {
            agentInimigo.isStopped = false;
            agentInimigo.SetDestination(destino);
        }
    }

    

    private GameObject EncontrarSoldadoMaisProximo()
    {
        GameObject[] soldados = GameObject.FindGameObjectsWithTag("Soldado");

        if (soldados.Length == 0) return null;

        GameObject maisProximo = null;
        float menorDistancia = Mathf.Infinity;
        Vector3 posicaoInimigo = transform.position;

        foreach (GameObject s in soldados)
        {
            float distancia = Vector3.Distance(posicaoInimigo, s.transform.position);

            
            if (s.activeInHierarchy && s.TryGetComponent<Soldado>(out Soldado soldadoComponent) && soldadoComponent.vida > 0)
            {
                if (distancia < menorDistancia)
                {
                    menorDistancia = distancia;
                    maisProximo = s;
                }
            }
        }
        return maisProximo;
    }

    private IEnumerator PermitirMovimentoAposDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        podeMover = true;
        
    }

   

    public void ReceberDano(int dano)
    {
        vida -= dano;
        vida = Mathf.Max(0, vida);

        if (vida <= 0)
        {
            GameManager.Instance.InimigoDerrotado();
            Destroy(gameObject);
        }
    }

    public void AtacarCastelo()
    {
        if (targetCastelo != null && !targetCastelo.Destruido())
        {
            targetCastelo.ReceberDano(danoAtaque);
        }
        cooldownTimer = tempoEntreAtaques;
    }

    public void AtacarSoldado(Soldado soldadoAlvo)
    {
        if (soldadoAlvo != null)
        {
            soldadoAlvo.ReceberDano(danoAtaque);
        }
        cooldownTimer = tempoEntreAtaques;
        
    }

    public void SetAnimation(string animName)
    {
        if (anim != null) anim.Play(animName);
    }
}