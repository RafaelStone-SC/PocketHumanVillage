using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class FolhaIndoQuebrarPedra : Node
{
    public FolhaIndoQuebrarPedra(Character c) : base(c) { }

    public override NodeState Execute()
    {
        Pedreiro agent = AgentPedreiro;
        if (agent == null) return NodeState.Failure;

        Vector3 destino;


        if (agent.targetFontePedra == null)
        {

            Transform pedraTransform = VilaEstruturasBasicas.Instance.fontePedra;

            if (pedraTransform == null)
            {
                
                return NodeState.Failure;
            }

            agent.targetFontePedra = pedraTransform.gameObject;
        }


        if (agent.targetFontePedra != null)
        {
            destino = agent.targetFontePedra.transform.position;
        }
        else
        {
            return NodeState.Failure;
        }


        if (Vector3.Distance(agent.transform.position, destino) < 5f)
        {

           
            return NodeState.Success;
        }


        if (agent.destinoAtual != destino)
        {
            agent.Novodestino(destino);
           
        }

        return NodeState.Running;
    }
}
