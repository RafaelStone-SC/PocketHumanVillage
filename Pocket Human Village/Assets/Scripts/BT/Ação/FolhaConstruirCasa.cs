using UnityEngine;
using UnityEngine.AI;

public class FolhaConstruirCasa : Node
{
    private const float FOME_POR_SEGUNDO = 0.1f;
    private const float ENERGIA_POR_SEGUNDO = 0.5f;

    public FolhaConstruirCasa(Character c) : base(c) { }

    private void DesequiparFerramenta(Construtor agent)
    {
        if (agent != null && agent.ferramentaInstanciada != null)
        {
            agent.ferramentaInstanciada.SetActive(false);
            BalancarMachado balanco = agent.ferramentaInstanciada.GetComponent<BalancarMachado>();
            if (balanco != null) balanco.enabled = false;
        }
    }

    public override NodeState Execute()
    {
        Construtor agent = AgentConstrutor;
        EstruturaCasa casa = AgentConstrutor.targetConstrucao;
        NavMeshAgent navAgent = agent?.agentConstrutor;
        if (casa == null)
        {
            return NodeState.Failure;
        }

      
       

       
        character.Energia -= Time.deltaTime * ENERGIA_POR_SEGUNDO;
        character.Fome += Time.deltaTime * FOME_POR_SEGUNDO;
        character.Energia = Mathf.Max(0, character.Energia);

        
        if (casa == null || navAgent == null || !casa.PrecisaDeConstrucao() || character.Energia <= 0)
        {
            DesequiparFerramenta(agent);
            if (navAgent != null) navAgent.isStopped = false;
            return NodeState.Failure;
        }
        if (character.Energia <= 10f)
        {
            
            DesequiparFerramenta(agent);
            if (navAgent != null) navAgent.isStopped = false;

            
            return NodeState.Failure;
        }

        if (!navAgent.isStopped)
        {
            navAgent.isStopped = true;
        }

       
        if (agent.ferramentaInstanciada == null && agent.marteloPrefab != null && agent.localFerramenta != null)
        {
            agent.ferramentaInstanciada = GameObject.Instantiate(
              agent.marteloPrefab,
              agent.localFerramenta.position,
              agent.localFerramenta.rotation,
              agent.localFerramenta
            );

            if (agent.ferramentaInstanciada.GetComponent<BalancarMachado>() == null)
            {
                agent.ferramentaInstanciada.AddComponent<BalancarMachado>();
            }
        }

       
        if (agent.ferramentaInstanciada != null)
        {
           
            if (!agent.ferramentaInstanciada.activeSelf)
            {
                agent.ferramentaInstanciada.SetActive(true);
            }

            BalancarMachado balanco = agent.ferramentaInstanciada.GetComponent<BalancarMachado>();
            if (balanco != null) balanco.enabled = true;
        }

        
        casa.ReceberTrabalho(Time.deltaTime);

       
        if (!casa.PrecisaDeConstrucao())
        {
            DesequiparFerramenta(agent);

            if (navAgent != null) navAgent.isStopped = false;
            return NodeState.Success;
        }

        return NodeState.Running;
    }
}