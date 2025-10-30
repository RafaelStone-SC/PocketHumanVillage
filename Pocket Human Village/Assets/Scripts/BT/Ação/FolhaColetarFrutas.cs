using System;
using UnityEngine;
using UnityEngine.AI;

public class FolhaColetarFrutas : Node
{
    private const float duracaoColeta =2f;
    private float timer = 0f;

    private const float FOME_POR_SEGUNDO = 0.1f;
    private const float ENERGIA_POR_SEGUNDO = 0.1f;

    public FolhaColetarFrutas(Character c) : base(c) { }

    
    private NavMeshAgent GetNavAgent()
    {
        if (AgentColetora != null) return AgentColetora.agentColetora;
        return null;
    }
    private GameObject GetItemCarregado()
    {
        if (AgentColetora != null) return AgentColetora.itemCarregado;
        return null;
    }
    private void SetItemCarregado(GameObject item)
    {
        if (AgentColetora != null) AgentColetora.itemCarregado = item;
    }

   

    public override NodeState Execute()
    {
        Coletora agent = AgentColetora;
        NavMeshAgent navAgent = GetNavAgent();

        if (agent == null || navAgent == null) return NodeState.Failure;

        if (GetItemCarregado() != null)
        {
            timer = 0f;
            return NodeState.Failure;
        }

      
        character.Fome += Time.deltaTime * FOME_POR_SEGUNDO;
        character.Energia -= Time.deltaTime * ENERGIA_POR_SEGUNDO;
        character.Energia = Mathf.Max(0, character.Energia);

        if (agent.targetFonteFrutas == null) return NodeState.Failure;


      
        if (timer == 0f)
        {
            navAgent.isStopped = true;
            FonteFrutas fonte = agent.targetFonteFrutas.GetComponent<FonteFrutas>();
            if (fonte != null)
            {
                fonte.IniciarColeta();
            }

        }

        
        if (timer >= duracaoColeta)
        {
            
            if (agent.comidaPrefab != null && agent.localItem != null)
            {
                GameObject novoRecurso = GameObject.Instantiate(
                    agent.comidaPrefab,
                    agent.localItem.position,
                    agent.localItem.rotation,
                    agent.localItem
                );
                SetItemCarregado(novoRecurso);
                Debug.Log("Carregando frutas");
            }

            timer = 0f;
            navAgent.isStopped = false;
            agent.targetFonteFrutas = null;
            return NodeState.Success;
        }

       
        timer += Time.deltaTime;
        return NodeState.Running;
    }
}