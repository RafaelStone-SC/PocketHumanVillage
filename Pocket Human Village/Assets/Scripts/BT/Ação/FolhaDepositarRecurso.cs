using UnityEngine;
using UnityEngine.AI;

public class FolhaDepositarRecurso : Node
{
    

    public FolhaDepositarRecurso(Character c) : base(c) { }

    
    private GameObject GetItemCarregado()
    {
        if (AgentLenhador != null) return AgentLenhador.itemCarregado;
        if (AgentMinerador != null) return AgentMinerador.itemCarregado;
        if(AgentColetora != null) return AgentColetora.itemCarregado;
        if(AgentPedreiro != null ) return AgentPedreiro.itemCarregado;
        if(AgentFerreiro != null) return AgentFerreiro.itemCarregado;
        return null;
    }
    private void SetItemCarregado(GameObject item)
    {
        if (AgentLenhador != null) AgentLenhador.itemCarregado = item;
        else if (AgentMinerador != null) AgentMinerador.itemCarregado = item;
        else if (AgentColetora != null) AgentColetora.itemCarregado = item;
        else if (AgentPedreiro != null) AgentPedreiro.itemCarregado = item;
        else if (AgentFerreiro != null) AgentFerreiro.itemCarregado= item;
    }
    private NavMeshAgent GetNavAgent()
    {
        if (AgentLenhador != null) return AgentLenhador.agentLenhador;
        if (AgentMinerador != null) return AgentMinerador.agentMinerador;
        if (AgentColetora != null) return AgentColetora.agentColetora;
        if (AgentPedreiro != null) return AgentPedreiro.agentPedreiro;
        if (AgentFerreiro != null) return AgentFerreiro.agentFerreiro;
        return null;
    }

    public override NodeState Execute()
    {
        GameObject itemCarregado = GetItemCarregado();
        NavMeshAgent navAgent = GetNavAgent();

        if (itemCarregado == null)
        {
            Debug.Log("sem item");
            return NodeState.Failure;
        }

       
        string recursoTipo = "Desconhecido";
        int quantidade = 0;

       
        PedraItem pedraData = itemCarregado.GetComponent<PedraItem>();
        if (pedraData != null)
        {
            recursoTipo = pedraData.tipoRecurso;
            quantidade = pedraData.amount;
        }
        
        else if (itemCarregado.GetComponent<MadeiraItem>() is MadeiraItem madeiraData)
        {
            recursoTipo = madeiraData.tipoRecurso;
            quantidade = madeiraData.amount;
        }
        
        else if (itemCarregado.GetComponent<OuroItem>() is OuroItem ouroData)
        {
            recursoTipo = ouroData.tipoRecurso;
            quantidade = ouroData.amount;
        }
       
        else if (itemCarregado.GetComponent<ComidaItem>() is ComidaItem comidaData)
        {
            recursoTipo = comidaData.tipoRecurso;
            quantidade = comidaData.amount;
        }
        else if(itemCarregado.GetComponent<FerroItem>() is FerroItem ferroData)
        {
            recursoTipo = ferroData.tipoRecurso;
            quantidade = ferroData.amount;
        }


        else
        {

            return NodeState.Failure;
        }
       

       
        if (quantidade <= 0)
        {
            return NodeState.Failure;
        }

        
        RecursosManager.Instance.AddRecurso(recursoTipo, quantidade);
        GameObject.Destroy(itemCarregado);
        SetItemCarregado(null);

        if (navAgent != null)
        {
            navAgent.isStopped = false;
        }
        return NodeState.Success;
    }
}