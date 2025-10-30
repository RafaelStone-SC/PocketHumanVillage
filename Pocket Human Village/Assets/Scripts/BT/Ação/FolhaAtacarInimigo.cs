using UnityEngine;

public class FolhaAtacarInimigo : Node
{
    public FolhaAtacarInimigo(Character c) : base(c)
    {
    }

    
    private void DesativarArmaESoldado(Soldado agent)
    {
        if (agent != null)
        {
            agent.AtivarArma(false); 
            agent.agentSoldado.isStopped = false; 
            
        }
    }

    public override NodeState Execute()
    {
        Soldado agent = AgentSoldado;

        
        if (agent == null || agent.targetInimigo == null)
        {
            DesativarArmaESoldado(agent);
            return NodeState.Failure;
        }

        
        agent.agentSoldado.isStopped = true;

        
        agent.AtivarArma(true);


        
        if (agent.targetInimigo.GetComponent<Inimigo>() == null && agent.targetInimigo.GetComponent<Castelo>() == null)
        {
            agent.targetInimigo = null;
            DesativarArmaESoldado(agent);
            return NodeState.Failure;
        }

       
        if (agent.ProntoParaAtacar())
        {
            if (agent.targetInimigo.TryGetComponent<Inimigo>(out Inimigo inimigo))
            {
               
                inimigo.ReceberDano(agent.danoAtaque);
            }
            
            else if (agent.targetInimigo.TryGetComponent<Castelo>(out Castelo castelo))
            {
                
            }

           
            agent.tempoDoCooldown = agent.tempoEntreAtaques;
           
        }

        
        if (agent.targetInimigo == null)
        {
            DesativarArmaESoldado(agent);
            return NodeState.Success;
        }

        
        return NodeState.Running;
    }
}