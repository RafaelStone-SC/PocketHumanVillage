using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class FolhaIndoMinerarOuro : Node
{
    public FolhaIndoMinerarOuro(Character c) : base(c) { }

    public override NodeState Execute()
    {
        Minerador agent = AgentMinerador;
        if (agent == null) return NodeState.Failure;

        Vector3 destino;

        
        if (agent.targetMina == null)
        {
            
            Transform minaTransform = VilaEstruturasBasicas.Instance.fonteOuro;

            if (minaTransform == null)
            {
                Debug.Log("Sem fonte de ouro");
                return NodeState.Failure;
            }

            agent.targetMina = minaTransform.gameObject;
        }

        
        if (agent.targetMina != null)
        {
            destino = agent.targetMina.transform.position;
        }
        else
        {
            return NodeState.Failure;
        }

       
        if (Vector3.Distance(agent.transform.position, destino) < 5f)
        {
           
            Debug.Log("Chegou na mina de ouro");
            return NodeState.Success;
        }

       
        if (agent.destinoAtual != destino)
        {
            agent.Novodestino(destino);
            Debug.Log("Indo para a mina de ouro");
        }

        return NodeState.Running;
    }
}