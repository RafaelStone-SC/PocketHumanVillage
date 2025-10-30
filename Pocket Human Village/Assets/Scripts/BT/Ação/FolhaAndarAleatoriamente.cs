using UnityEngine;
using UnityEngine.AI;

public class FolhaAndarAleatoriamente : Node
{
    private const float RAIO_BUSCA = 20f;
    private Vector3 destinoAtual = Vector3.zero;

    public FolhaAndarAleatoriamente(Character c) : base(c) { }

    private NavMeshAgent GetNavAgent()
    {
        if (AgentConstrutor != null)
        {
            return AgentConstrutor.agentConstrutor;
            
        }

        if (AgentSoldado != null)
        {
            return AgentSoldado.agentSoldado;
        }

        return null;
    }

    private void SetNewDestination(Vector3 destino)
    {
        if (AgentConstrutor != null)
        {
            AgentConstrutor.NovoDestino(destino);
        }
        if (AgentSoldado != null)
        {
            AgentSoldado.NovoDestino(destino);
        }
    }

    public override NodeState Execute()
    {
        NavMeshAgent navAgent = GetNavAgent();
        if (navAgent == null) return NodeState.Failure;

        
        if (navAgent.remainingDistance <= navAgent.stoppingDistance && !navAgent.pathPending)
        {
            

            if (EncontrarNovoDestino(navAgent.transform.position, RAIO_BUSCA, out Vector3 novoDestino))
            {
                destinoAtual = novoDestino;
                SetNewDestination(destinoAtual);
                navAgent.isStopped = false;
                return NodeState.Running; 
            }
            else
            {
               
                navAgent.isStopped = true;
                return NodeState.Running;
            }
        }

       
        if (navAgent.velocity.sqrMagnitude > 0.01f || navAgent.pathPending)
        {
            return NodeState.Running;
        }

      
        return NodeState.Failure;
    }

    private bool EncontrarNovoDestino(Vector3 origem, float distancia, out Vector3 resultado)
    {
       
        Vector3 randomPoint = origem + Random.insideUnitSphere * distancia;
        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomPoint, out hit, distancia, NavMesh.AllAreas))
        {
            resultado = hit.position;
            return true;
        }

        resultado = Vector3.zero;
        return false;
    }
}