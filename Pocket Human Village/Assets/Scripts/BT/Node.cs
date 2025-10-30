using UnityEngine;

public abstract class Node
{
    protected Character character;

    
    protected Lenhador AgentLenhador => character.AgentLenhador;
    protected Minerador AgentMinerador => character.AgentMinerador;
    protected Coletora AgentColetora => character.AgentColetora;
    protected Pedreiro AgentPedreiro => character.AgentPedreiro;
    protected Ferreiro AgentFerreiro => character.AgentFerreiro;
    protected Construtor AgentConstrutor => character.AgentConstrutor;
    protected Soldado AgentSoldado => character.AgentSoldado;

    public Node(Character c)
    {
        this.character = c;
    }
    public abstract NodeState Execute();
}