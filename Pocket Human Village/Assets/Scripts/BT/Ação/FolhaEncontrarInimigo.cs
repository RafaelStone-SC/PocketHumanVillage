using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class FolhaEncontrarInimigo : Node
{
    public FolhaEncontrarInimigo(Character c) : base(c) { }

    public override NodeState Execute()
    {
        Soldado agent = AgentSoldado;
        if (agent == null) return NodeState.Failure;

        
        if (agent.targetInimigo != null && agent.targetInimigo.activeInHierarchy && agent.targetInimigo.TryGetComponent<Inimigo>(out Inimigo alvo) && alvo.vida > 0)
        {
            return NodeState.Success;
        }

        
        agent.targetInimigo = null;

       
        GameObject[] inimigos = GameObject.FindGameObjectsWithTag("Inimigo");

        
        if (inimigos.Length > 0)
        {
           
            GameObject inimigoMaisProximo = inimigos
                .Where(i => i != null && i.activeInHierarchy && i.GetComponent<Inimigo>()?.vida > 0)
                .OrderBy(i => Vector3.Distance(i.transform.position, agent.transform.position))
                .FirstOrDefault();

            if (inimigoMaisProximo != null)
            {
                agent.targetInimigo = inimigoMaisProximo;
                return NodeState.Success;
            }
        }

        
        return NodeState.Failure;
    }
}
