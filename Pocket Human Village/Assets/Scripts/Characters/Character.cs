using System;
using System.Diagnostics;
using UnityEngine; 

public class Character
{
    public float Fome { get; set; } = 0f; 
    public float Energia { get; set; } = 100f;
    public float Vida { get; set; } = 100f;
    public string Name { get; set; }
    public bool Ocupado { get; set; } = false;
    public bool trabalhando { get; set; } = false;
    public bool descansando { get; set; } = false;

    
    public object AgentComponent { get; private set; }

    
    public Lenhador AgentLenhador => AgentComponent as Lenhador;
    public Minerador AgentMinerador => AgentComponent as Minerador;

    public Coletora AgentColetora => AgentComponent as Coletora;

    public Pedreiro AgentPedreiro => AgentComponent as Pedreiro;
    public Ferreiro AgentFerreiro => AgentComponent as Ferreiro;
    public Construtor AgentConstrutor => AgentComponent as Construtor;

    public Soldado AgentSoldado => AgentComponent as Soldado;

    
    public Character(string name, object agentComponent)
    {
        Name = name;
        this.AgentComponent = agentComponent;
    }

    public Character(string name)
    {
        Name = name;
    }

   
}