using UnityEngine;
using UnityEngine.AI;

public class FolhaIrParaDescanso : Node
{
    private Vector3 casaPos;
    private const float DISTANCIA_PARADA = 1f;

    public FolhaIrParaDescanso(Character c) : base(c) { }

    
    private NavMeshAgent GetNavAgent(out Transform agentTransform, out Vector3 currentDestination)
    {
        agentTransform = null;
        currentDestination = Vector3.zero;

        if (AgentLenhador != null)
        {
            agentTransform = AgentLenhador.transform;
            currentDestination = AgentLenhador.destinoAtual;
            return AgentLenhador.agentLenhador;
        }
        else if (AgentMinerador != null)
        {
            agentTransform = AgentMinerador.transform;
            currentDestination = AgentMinerador.destinoAtual;
            return AgentMinerador.agentMinerador;
        }
        else if(AgentColetora != null)
        {
            agentTransform = AgentColetora.transform;
            currentDestination = AgentColetora.destinoAtual;
            return AgentColetora.agentColetora;
        }
        else if(AgentPedreiro != null)
        {
            agentTransform = AgentPedreiro.transform;
            currentDestination = AgentPedreiro.destinoAtual;
            return AgentPedreiro.agentPedreiro;
        }
        else if(AgentFerreiro != null)
        {
            agentTransform = AgentFerreiro.transform;
            currentDestination = AgentFerreiro.destinoAtual;
            return AgentFerreiro.agentFerreiro;
        }
            return null;
    }

    
    private void SetNewDestination(Vector3 destino)
    {
        if (AgentLenhador != null)
        {
            AgentLenhador.Novodestino(destino);
        }

        else if (AgentMinerador != null)
        {
            AgentMinerador.Novodestino(destino);
        }
        else if(AgentColetora!= null)
        {
            AgentColetora.Novodestino(destino);
        }
        else if(AgentPedreiro!= null)
        {
            AgentPedreiro.Novodestino(destino);
        }
        else if(AgentFerreiro!= null)
        {
            AgentFerreiro.Novodestino(destino);
        }
    }

    public override NodeState Execute()
    {
        Transform agentTransform;
        Vector3 currentDestination;
       

       
        NavMeshAgent navAgent = GetNavAgent(out agentTransform, out currentDestination);

        if (navAgent == null || agentTransform == null)
        {
            Debug.Log("Sem navmesh");
            return NodeState.Failure;
        }

        casaPos = VilaEstruturasBasicas.Instance.Localizacao("Casa");

        if (casaPos == Vector3.zero)
        {
            Debug.Log("Sem casa para descansar");
            return NodeState.Failure;
        }

       
        if (Vector3.Distance(agentTransform.position, casaPos) < DISTANCIA_PARADA)
        {
          
            navAgent.isStopped = true;
            return NodeState.Success;
        }

        
        if (currentDestination != casaPos) 
        {
            SetNewDestination(casaPos); 
            Debug.Log("Indo para casa descansar");
        }

        return NodeState.Running;
    }
}