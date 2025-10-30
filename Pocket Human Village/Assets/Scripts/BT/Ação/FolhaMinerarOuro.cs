using System;
using UnityEngine;
using UnityEngine.AI;

public class FolhaMinerarOuro : Node
{
   
    private const float duracaoMinerarOuro = 50.0f;

    
    private float timer = 0f;

    private const float FOME_POR_SEGUNDO = 0.2f;
    private const float ENERGIA_POR_SEGUNDO = 0.2f;

    public FolhaMinerarOuro(Character c) : base(c) { }

    
    private NavMeshAgent GetNavAgent()
    {
        if (AgentMinerador != null) return AgentMinerador.agentMinerador;
        return null;
    }
    private GameObject GetItemCarregado()
    {
        if (AgentMinerador != null) return AgentMinerador.itemCarregado;
        return null;
    }
    private void SetItemCarregado(GameObject item)
    {
        if (AgentMinerador != null) AgentMinerador.itemCarregado = item;
    }
   

    public override NodeState Execute()
    {
        Minerador minerador = AgentMinerador;
        NavMeshAgent navAgent = GetNavAgent();

        if (minerador == null || navAgent == null)
        {
          
            return NodeState.Failure;
        }

        
        if (GetItemCarregado() != null)
        {
            timer = 0f;
            return NodeState.Failure;
        }

        
        character.Fome += Time.deltaTime * FOME_POR_SEGUNDO;
        character.Energia -= Time.deltaTime * ENERGIA_POR_SEGUNDO;
        character.Energia = Mathf.Max(0, character.Energia);

        
        if (minerador.targetMina == null)
        {
            timer = 0f;
            return NodeState.Failure;
        }


       
        if (timer == 0f)
        {
            navAgent.isStopped = true;
            Debug.Log("Minerando Ouro");

           
            if (minerador.picaretaInstanciada == null)
            {
                
                minerador.picaretaInstanciada = GameObject.Instantiate(
                    minerador.picaretaPrefab,
                    minerador.localPicareta.position,
                    minerador.localPicareta.rotation,
                    minerador.localPicareta 
                );

                
                if (minerador.picaretaInstanciada.GetComponent<BalancarMachado>() == null)
                {
                    minerador.picaretaInstanciada.AddComponent<BalancarMachado>();
                }
            }

            if (minerador.picaretaInstanciada != null)
            {
                minerador.picaretaInstanciada.SetActive(true);

               
                BalancarMachado balanco = minerador.picaretaInstanciada.GetComponent<BalancarMachado>();
                if (balanco != null)
                {
                    balanco.enabled = true;
                }
            }
        }

        
        else
        {
            
            if (minerador.picaretaInstanciada != null)
            {
                BalancarMachado balanco = minerador.picaretaInstanciada.GetComponent<BalancarMachado>();
                if (balanco != null && !balanco.enabled)
                {
                    balanco.enabled = true;
                }
            }
        }


        
        if (timer >= duracaoMinerarOuro)
        {
            
            if (minerador.ouroPrefab != null && minerador.localItem != null)
            {
                GameObject novoRecurso = GameObject.Instantiate(
                    minerador.ouroPrefab,
                    minerador.localItem.position,
                    minerador.localItem.rotation,
                    minerador.localItem
                );
                SetItemCarregado(novoRecurso);
                Debug.Log("Carregando Ouro");
            }

           
            if (minerador.picaretaInstanciada != null)
            {
                minerador.picaretaInstanciada.SetActive(false);

                
                BalancarMachado balanco = minerador.picaretaInstanciada.GetComponent<BalancarMachado>();
                if (balanco != null)
                {
                    balanco.enabled = false;
                }
            }

            timer = 0f;
            navAgent.isStopped = false; 
            minerador.targetMina = null; 
            return NodeState.Success;
        }

        
        timer += Time.deltaTime;
        return NodeState.Running;
    }
}