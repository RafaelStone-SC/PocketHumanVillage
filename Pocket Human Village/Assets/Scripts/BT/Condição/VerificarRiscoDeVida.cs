using UnityEngine;

public class VerificarRiscoDeVida : Node
{
    
    private const float LIMITE_CRITICO = 0f;

    

    public VerificarRiscoDeVida(Character c) : base(c) { }

    public override NodeState Execute()
    {
        
        bool fomeMax = character.Fome >= 100f;

        if (fomeMax && RecursosManager.Instance.Valorrecurso("Comida") <= 0)
        {
            Debug.Log("Sem comida");
            return NodeState.Success;
        }

        
        if (character.Energia <= LIMITE_CRITICO)
        {
           
            Transform casaTransform = VilaEstruturasBasicas.Instance.casa;
            EstruturaCasa casa = (casaTransform != null) ? casaTransform.GetComponent<EstruturaCasa>() : null;

            
            if (casa == null || casa.OcuapacaoAtual >= casa.capacidadeMaxima)
            {
                Debug.Log("Sem casa para descansar");
                return NodeState.Success;
            }
        }

        return NodeState.Failure;
    }
}