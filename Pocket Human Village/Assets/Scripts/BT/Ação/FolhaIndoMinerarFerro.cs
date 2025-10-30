using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class FolhaIndoMinerarFerro : Node
{
    public FolhaIndoMinerarFerro(Character c) : base(c) { }

    public override NodeState Execute()
    {
        Ferreiro agent = AgentFerreiro;
        if (agent == null) return NodeState.Failure;

        Vector3 destino;


        if (agent.targetFonteFerro == null)
        {

            Transform minaferroTransform = VilaEstruturasBasicas.Instance.fonteFerro;

            if (minaferroTransform == null)
            {
               
                return NodeState.Failure;
            }

            agent.targetFonteFerro = minaferroTransform.gameObject;
        }


        if (agent.targetFonteFerro != null)
        {
            destino = agent.targetFonteFerro.transform.position;
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