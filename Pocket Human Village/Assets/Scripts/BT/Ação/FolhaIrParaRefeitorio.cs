using UnityEngine;
using UnityEngine.AI;

public class FolhaIrParaRefeitorio : Node
{
    public FolhaIrParaRefeitorio(Character c) : base(c) { }

    
    private NavMeshAgent GetNavAgent(out Transform agentTransform)
    {
        NavMeshAgent navAgent = null;
        agentTransform = null;

        
        if (AgentLenhador != null)
        {
            navAgent = AgentLenhador.agentLenhador;
            agentTransform = AgentLenhador.transform;
            return navAgent;
        }

        
        if (AgentMinerador != null)
        {
            navAgent = AgentMinerador.agentMinerador;
            agentTransform = AgentMinerador.transform;
            return navAgent;
        }
        if(AgentColetora != null)
        {
            navAgent = AgentColetora.agentColetora;
            agentTransform = AgentColetora.transform;
            return navAgent;
        }
        if(AgentPedreiro != null)
        {
            navAgent = AgentPedreiro.agentPedreiro;
            agentTransform=AgentPedreiro.transform;
            return navAgent;
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
        else if(AgentColetora != null)
        {
            AgentColetora.Novodestino(destino);
        }
        else if(AgentPedreiro == null)
        {
            AgentPedreiro.Novodestino(destino);
        }
        else if(AgentFerreiro == null)
        {
            AgentFerreiro.Novodestino(destino);
        }
    }

    public override NodeState Execute()
    {
        Transform agentTransform;
        NavMeshAgent navAgent = GetNavAgent(out agentTransform);


        if (navAgent == null || agentTransform == null)
        {
           
            return NodeState.Failure;
        }

        
        Vector3 refeitorioPos = VilaEstruturasBasicas.Instance.Localizacao("Refeitorio");

        if (refeitorioPos == Vector3.zero) return NodeState.Failure;

       
        if (Vector3.Distance(agentTransform.position, refeitorioPos) < 1.5f)
        {
            Debug.Log("Chegou para comer");
           
            navAgent.isStopped = true;
            return NodeState.Success;
        }

      
        if (navAgent.destination != refeitorioPos)
        {
            SetNewDestination(refeitorioPos);
           
        }

        return NodeState.Running;
    }
}