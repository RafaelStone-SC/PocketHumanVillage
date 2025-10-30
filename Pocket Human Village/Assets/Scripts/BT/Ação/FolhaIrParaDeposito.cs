using UnityEngine;
using UnityEngine.AI;

public class FolhaIrParaDeposito : Node
{
    public FolhaIrParaDeposito(Character c) : base(c) { }

   
    private NavMeshAgent GetNavAgent(out Transform agentTransform)
    {
        agentTransform = null;
        if (AgentLenhador != null)
        {
            agentTransform = AgentLenhador.transform;
            return AgentLenhador.agentLenhador;
        }
        else if (AgentMinerador != null)
        {
            agentTransform = AgentMinerador.transform;
            return AgentMinerador.agentMinerador;
        }
        else if(AgentColetora != null)
        {
            agentTransform = AgentColetora.transform;
            return AgentColetora .agentColetora;
        }
        else if(AgentPedreiro != null)
        {
            agentTransform =(AgentPedreiro.transform) ;
            return AgentPedreiro .agentPedreiro;
        }
        else if(AgentFerreiro != null)
        {
            agentTransform=(AgentFerreiro.transform) ;
            return AgentFerreiro .agentFerreiro;
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
        else if( AgentPedreiro != null)
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
        NavMeshAgent navAgent = GetNavAgent(out agentTransform);

        if (navAgent == null || agentTransform == null) return NodeState.Failure;

        Vector3 depositoPos = VilaEstruturasBasicas.Instance.Localizacao("Deposito");

        if (depositoPos == Vector3.zero) return NodeState.Failure;

        
        if (Vector3.Distance(agentTransform.position, depositoPos) < 1.5f)
        {
            
            navAgent.isStopped = true;  
            return NodeState.Success;
        }

        
        if (navAgent.destination != depositoPos)
        {
           
            SetNewDestination(depositoPos);
            Debug.Log("indo pro deposito");
        }
        navAgent.isStopped = false;

        return NodeState.Running;
    }
}