using UnityEngine;

public class VerificarModoHorda : Node
{
   public VerificarModoHorda(Character c) : base(c)
    {

    }
    public override NodeState Execute()
    {
        if(GameManager.Instance != null && GameManager.Instance.EstadoAtual == GameState.Horda)
        {
            return NodeState.Success;
        }
        return NodeState.Failure;
    }
}
