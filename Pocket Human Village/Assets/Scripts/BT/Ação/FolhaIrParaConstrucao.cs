using UnityEngine;

public class FolhaIrParaConstrucao : Node
{
    
    private const float DISTANCIA_CONSTRUCAO = 1f;
    
    public FolhaIrParaConstrucao(Character c) : base(c)
    {

    }
    public override NodeState Execute()
    {
        Construtor agent = AgentConstrutor;
        if (agent == null)
        {
            return NodeState.Failure;
        }
        EstruturaCasa casa = AgentConstrutor.targetConstrucao;
        if (casa == null)
        {
            return NodeState.Failure;
        }

        Vector3 casaPos = casa.transform.position;
        if (casaPos == Vector3.zero)
        {
            return NodeState.Failure;
        }
       
        if (Vector3.Distance(agent.transform.position, casaPos) < DISTANCIA_CONSTRUCAO)
        {
            agent.agentConstrutor.isStopped = true;
            return NodeState.Success;
        }

        
        if (agent.destinoAtual != casaPos)
        {
            agent.NovoDestino(casaPos);
        }
        return NodeState.Running;
    }
}