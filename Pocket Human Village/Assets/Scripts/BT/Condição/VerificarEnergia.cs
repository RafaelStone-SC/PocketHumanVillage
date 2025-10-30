using UnityEngine;


public class VerificarEnergia : Node
{
   
    public float energiaLenhador;
    public VerificarEnergia(Character c, float nivel) : base(c)
    {
        energiaLenhador = nivel;
    }
    public override NodeState Execute()
    {
        if (character.descansando)
        {
            return NodeState.Success;
        }

       
        if (character.Energia <= energiaLenhador)
        {
            
            Transform casaTransform = VilaEstruturasBasicas.Instance.casa;

            if (casaTransform != null)
            {
                EstruturaCasa casa = casaTransform.GetComponent<EstruturaCasa>();
                if (casa != null) 
                {
                   
                    if (casa.OcuapacaoAtual < casa.capacidadeMaxima)
                    {
                        return NodeState.Success;
                    }
                    else
                    {
                        
                        

                       
                        return NodeState.Failure;
                    }
                }
            }
          
            return NodeState.Failure;
        }
        return NodeState.Failure; 
    }
}