using UnityEngine;
using UnityEngine.AI;

public class FolhaDescansar : Node
{
    private EstruturaCasa casaOcupada = null;
    private const float TAXA_RECUPERACAO_ENERGIA = 2.0f;

    public FolhaDescansar(Character c) : base(c) { }

    
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
            return AgentColetora.agentColetora;
        }
        else if(AgentPedreiro != null)
        {
            agentTransform= AgentPedreiro.transform;
            return AgentPedreiro.agentPedreiro;
        }
        else if(AgentFerreiro != null)
        {
            agentTransform = AgentFerreiro.transform;
            return AgentFerreiro.agentFerreiro;
        }
            return null;
    }
    private void SetStopped(NavMeshAgent agent, bool isStopped)
    {
        if (agent != null) agent.isStopped = isStopped;
    }

    public override NodeState Execute()
    {
        Transform agentTransform;
        NavMeshAgent navAgent = GetNavAgent(out agentTransform);

        if (navAgent == null || agentTransform == null) return NodeState.Failure;

        Vector3 casaPos = VilaEstruturasBasicas.Instance.Localizacao("Casa");

       
        if (Vector3.Distance(agentTransform.position, casaPos) > 1.5f && character.descansando)
        {
            if (casaOcupada != null)
            {
                casaOcupada.Sair();
                casaOcupada = null;
            }
            character.descansando = false;
            SetStopped(navAgent, false);
            return NodeState.Failure;
        }

       
        if (casaOcupada == null)
        {
            Transform casaTransform = VilaEstruturasBasicas.Instance.casa;
            if (casaTransform != null)
            {
                casaOcupada = casaTransform.GetComponent<EstruturaCasa>();
                if (casaOcupada != null && casaOcupada.TentarEntrar())
                {
                    SetStopped(navAgent, true);
                    character.descansando = true;
                    Debug.Log("Entrou para descansar");
                }
                else
                {
                    casaOcupada = null;
                    return NodeState.Failure;
                }
            }
            else
            {
                return NodeState.Failure;
            }
        }

       
        if (character.Energia < 100)
        {
            character.Energia += Time.deltaTime * TAXA_RECUPERACAO_ENERGIA;
            character.Energia = Mathf.Min(100f, character.Energia);
            return NodeState.Running;
        }

        
        else
        {
            SetStopped(navAgent, false);

            if (casaOcupada != null)
            {
                casaOcupada.Sair();
                casaOcupada = null;
            }
            character.descansando = false;
            return NodeState.Success;
        }
    }
}