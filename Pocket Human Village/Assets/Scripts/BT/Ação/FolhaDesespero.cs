using UnityEngine;
using UnityEngine.AI;

public class FolhaDesespero : Node
{
    public 
        FolhaDesespero(Character c) : base(c) { }

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
        if(AgentFerreiro != null)
        {
            return AgentFerreiro.agentFerreiro;
        }
        if(AgentPedreiro != null)
        {
            return AgentPedreiro.agentPedreiro;
        }
        return null;
    }

    public override NodeState Execute()
    {
        NavMeshAgent navAgent = GetNavAgent();

        if (navAgent != null)
        {
            
            if (!navAgent.isStopped)
            {
                navAgent.isStopped = true;
                if (character.Fome >= 100)
                {
                    Debug.Log("Fome no maximo, parou");
                }
                else if (character.Energia <= 0)
                {
                    Debug.Log("Muito cansado, parou");
                }
            }
        }
        return NodeState.Running;
    }
}