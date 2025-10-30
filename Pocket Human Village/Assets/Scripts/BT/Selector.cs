using System.Collections.Generic;

public class Selector : Node
{
    private List<Node> children = new List<Node>();

    public Selector (Character c, List<Node> nodes) : base(c)
    {
        children = nodes;
    }
    public override NodeState Execute()
    {
        foreach (Node node in children)
        {
            NodeState state = node.Execute();

            if(state != NodeState.Failure)
            {
                return state;
            }
        }
        return NodeState.Failure;
    }
}
