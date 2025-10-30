using UnityEngine;

public class VerificarCarregamento : Node
{
    public VerificarCarregamento(Character c) : base(c) { }

   
    private GameObject GetItemCarregado()
    {
        if (AgentLenhador != null) return AgentLenhador.itemCarregado;
        if (AgentMinerador != null) return AgentMinerador.itemCarregado;
        if(AgentColetora != null) return AgentColetora.itemCarregado;
        if(AgentPedreiro != null ) return AgentPedreiro.itemCarregado; 
        if(AgentFerreiro != null) return AgentFerreiro.itemCarregado;
        return null;
    }

    public override NodeState Execute()
    {
       
        if (GetItemCarregado() != null)
        {
            return NodeState.Success;
        }
        return NodeState.Failure;
    }
}