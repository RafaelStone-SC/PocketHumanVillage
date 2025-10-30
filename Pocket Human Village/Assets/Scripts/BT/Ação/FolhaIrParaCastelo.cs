using UnityEngine;
using UnityEngine.AI;

public class FolhaIrParaCastelo : Node
{
    private const float DISTANCIA_PARADA = 0.5f;
    public FolhaIrParaCastelo(Character c) : base(c)
    {
    }
    private NavMeshAgent GetNavAgent()
    {
        if (AgentLenhador != null) return AgentLenhador.agentLenhador;
        if (AgentMinerador != null) return AgentMinerador.agentMinerador;
        if (AgentColetora != null) return AgentColetora.agentColetora;
        if (AgentPedreiro != null) return AgentPedreiro.agentPedreiro;
        if (AgentFerreiro != null) return AgentFerreiro.agentFerreiro;
        if (AgentConstrutor != null) return AgentConstrutor.agentConstrutor;
        return null;
    }
    public override NodeState Execute()
    {
        NavMeshAgent navAgent = GetNavAgent();
        if (navAgent == null) return NodeState.Failure;


        Vector3 refúgioPos = VilaEstruturasBasicas.Instance.Localizacao("Deposito");
        if (refúgioPos == Vector3.zero) return NodeState.Failure;

       
        if (Vector3.Distance(navAgent.transform.position, refúgioPos) < DISTANCIA_PARADA)
        {
            navAgent.isStopped = true;
            return NodeState.Running; 
        }

       
        if (navAgent.destination != refúgioPos)
        {
            navAgent.isStopped = false;
            navAgent.SetDestination(refúgioPos);
            
        }

        return NodeState.Running;
    }
}

