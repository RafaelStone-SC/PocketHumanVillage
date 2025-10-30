using System.Collections.Generic;

public class Sequence : Node
{
    private List<Node> children = new List<Node>();

    public Sequence (Character c, List<Node> nodes) : base(c)
    {
        children = nodes;
    }
    public override NodeState Execute()
    {
        foreach (Node node in children)
        {
            NodeState state  = node.Execute();
            if(state != NodeState.Success)
            {
                return state;
            }
        }
        return NodeState.Success;
    }
}