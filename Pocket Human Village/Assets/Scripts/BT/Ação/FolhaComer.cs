using System;
using UnityEngine;
using UnityEngine.AI;

public class FolhaComer : Node
{
    private const int quantidadeComida = 1;

    public FolhaComer(Character c) : base(c) { }

    
    private NavMeshAgent GetNavAgent()
    {
        if (AgentLenhador != null)
        {
            return AgentLenhador.agentLenhador;
        }

        if (AgentMinerador != null)
        {
            return AgentMinerador.agentMinerador;
        }
        if(AgentColetora != null)
        {
            return AgentColetora.agentColetora;
        }
        if(AgentPedreiro != null)
        {
            return AgentPedreiro.agentPedreiro;
        }
        if(AgentFerreiro != null)
        {
            return AgentFerreiro.agentFerreiro;
        }
        return null;
    }

    public override NodeState Execute()
    {
        NavMeshAgent navAgent = GetNavAgent();
       

        if (RecursosManager.Instance.RemoverRecurso("Comida", quantidadeComida))
        {
           
            Debug.Log("Fome zerada");
            character.Fome = 0f;

           
            if (navAgent != null)
            {
               
                navAgent.isStopped = false;
            }
            return NodeState.Success;
        }
        else
        {
            
            Debug.Log("Nao tem comida, voltando para o trabalho");
            if (navAgent != null)
            {
                
                navAgent.isStopped = false;
            }
            return NodeState.Failure;
        }
    }
}