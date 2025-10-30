using UnityEngine;
using UnityEngine.AI;
using System;
using System.Linq;

public class FolhaIndoCortarMadeira : Node
{
    public FolhaIndoCortarMadeira(Character c) : base(c) { }

    public override NodeState Execute()
    {
        Lenhador agent = AgentLenhador;
       
        if (agent == null) return NodeState.Failure;

        Vector3 destino;

        if (agent.targetMadeira == null)
        {
            GameObject[] arvores = GameObject.FindGameObjectsWithTag("Arvore");

            if (arvores.Length > 0)
            {
               
                GameObject arvoreMaisProxima = arvores
                    .Where(arvore => arvore != null)
                    .Where(arvore => arvore.GetComponent<FonteMadeira>() != null)
                    .OrderBy(arvore => Vector3.Distance(arvore.transform.position, agent.transform.position))
                    .FirstOrDefault();

                if (arvoreMaisProxima != null)
                {
                    agent.targetMadeira = arvoreMaisProxima;
                }
                else
                {
                    return NodeState.Failure;
                }
            }
            else
            {
                return NodeState.Failure;
            }
        }

        
        if (agent.targetMadeira != null)
        {
            
            if (agent.targetMadeira.GetComponent<FonteMadeira>() == null)
            {
                agent.targetMadeira = null;
                return NodeState.Failure;
            }
            destino = agent.targetMadeira.transform.position;
        }
        else
        {
            
            destino = VilaEstruturasBasicas.Instance.Localizacao("Madeira");
            if (destino == Vector3.zero) return NodeState.Failure; 
        }

        if (Vector3.Distance(agent.transform.position, destino) < 2.3f)
        {
            character.trabalhando = true;
            Debug.Log("Chegou na fonte de madeira");
            return NodeState.Success;
        }

        if (agent.destinoAtual != destino)
        {
            agent.Novodestino(destino);
        }

        return NodeState.Running;
    }
}