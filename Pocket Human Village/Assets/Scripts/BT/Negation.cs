
using System.Collections.Generic;

public class Negation : Node
{
    private Node child;

   
    public Negation(Character c, Node nodeToInvert) : base(c)
    {
        this.child = nodeToInvert;
    }

    public override NodeState Execute()
    {
        
        NodeState state = child.Execute();

        
        switch (state)
        {
            case NodeState.Success:
                return NodeState.Failure; 
            case NodeState.Failure:
                return NodeState.Success; 
            case NodeState.Running:
                return NodeState.Running;
            default:
                return NodeState.Failure;
        }
    }
}