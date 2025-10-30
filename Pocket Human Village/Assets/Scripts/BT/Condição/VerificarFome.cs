using System;
using UnityEngine;


public class VerificarFome : Node
{
    private float nivelFome;
    public VerificarFome(Character c, float nivel) : base(c)
    {
        nivelFome = nivel;
    }
    public override NodeState Execute()
    {
        if (character.Fome >= nivelFome)
        {
            Debug.Log("Precisa Comer");
           
            if (RecursosManager.Instance.Valorrecurso("Comida") > 0)
            {
                return NodeState.Success;
            }
            return NodeState.Failure;
        }
       
            
        
        return NodeState.Failure;

    }
}