using UnityEngine;
using UnityEngine.AI;

public class FolhaPerseguirInimigo : Node
{
    public FolhaPerseguirInimigo(Character c) : base(c) { }

    public override NodeState Execute()
    {
        Soldado agent = AgentSoldado;

        
        if (agent == null || agent.targetInimigo == null || agent.agentSoldado == null)
        {
            return NodeState.Failure;
        }

        
        if (!agent.targetInimigo.TryGetComponent<Inimigo>(out Inimigo inimigoAlvo) || inimigoAlvo.vida <= 0)
        {
            agent.targetInimigo = null;
            return NodeState.Failure;
        }

        
        float distancia = Vector3.Distance(agent.transform.position, agent.targetInimigo.transform.position);

       
        if (distancia <= agent.alcanceAtaque)
        {
            agent.agentSoldado.isStopped = true;

            
            Vector3 direcao = (agent.targetInimigo.transform.position - agent.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direcao.x, 0, direcao.z));
            agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, lookRotation, Time.deltaTime * 10f);

            return NodeState.Success;
        }

        
        agent.agentSoldado.isStopped = false;
        agent.agentSoldado.SetDestination(agent.targetInimigo.transform.position);

        

        return NodeState.Running;
    }
}