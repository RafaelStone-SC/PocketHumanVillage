using UnityEngine;

public class VerificarCasaParaContruir : Node
{
    public VerificarCasaParaContruir(Character c) : base(c)
    {
    }
    public override NodeState Execute()
    {
        Transform casaTransform = VilaEstruturasBasicas.Instance.casa;
        EstruturaCasa casa = (casaTransform != null) ? casaTransform.GetComponent<EstruturaCasa>() : null;

        
        if (casa != null && casa.PrecisaDeConstrucao())
        {
            
            AgentConstrutor.targetConstrucao = casa;
            return NodeState.Success;
        }

        
        AgentConstrutor.targetConstrucao = null;
        return NodeState.Failure;
    }
}
