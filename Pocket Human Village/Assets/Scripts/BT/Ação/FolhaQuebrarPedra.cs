using System;
using UnityEngine;
using UnityEngine.AI;

public class FolhaQuebrarPedra : Node
{

    private const float duracaoQuebrarPedra = 45.0f;


    private float timer = 0f;

    private const float FOME_POR_SEGUNDO = 0.1f;
    private const float ENERGIA_POR_SEGUNDO = 0.1f;

    public FolhaQuebrarPedra (Character c) : base(c) { }


    private NavMeshAgent GetNavAgent()
    {
        if (AgentPedreiro != null) return AgentPedreiro.agentPedreiro;
        return null;
    }
    private GameObject GetItemCarregado()
    {
        if (AgentPedreiro != null) return AgentPedreiro.itemCarregado;
        return null;
    }
    private void SetItemCarregado(GameObject item)
    {
        if (AgentPedreiro != null) AgentPedreiro.itemCarregado = item;
    }


    public override NodeState Execute()
    {
        Pedreiro pedreiro = AgentPedreiro;
        NavMeshAgent navAgent = GetNavAgent();

        if (pedreiro == null || navAgent == null)
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


        if (pedreiro.targetFontePedra == null)
        {
            timer = 0f;
            return NodeState.Failure;
        }



        if (timer == 0f)
        {
            navAgent.isStopped = true;
            Debug.Log("Pedrerando");


            if (pedreiro.ferramentaInstanciada == null)
            {

                pedreiro.ferramentaInstanciada = GameObject.Instantiate(
                    pedreiro.marteloPrefab,
                    pedreiro.localFerramenta.position,
                    pedreiro.localFerramenta.rotation,
                    pedreiro.localFerramenta
                );


                if (pedreiro.ferramentaInstanciada.GetComponent<BalancarMachado>() == null)
                {
                    pedreiro.ferramentaInstanciada.AddComponent<BalancarMachado>();
                }
            }

            if (pedreiro.ferramentaInstanciada != null)
            {
                pedreiro.ferramentaInstanciada.SetActive(true);


                BalancarMachado balanco = pedreiro.ferramentaInstanciada.GetComponent<BalancarMachado>();
                if (balanco != null)
                {
                    balanco.enabled = true;
                }
            }
        }


        else
        {

            if (pedreiro.ferramentaInstanciada != null)
            {
                BalancarMachado balanco = pedreiro.ferramentaInstanciada.GetComponent<BalancarMachado>();
                if (balanco != null && !balanco.enabled)
                {
                    balanco.enabled = true;
                }
            }
        }



        if (timer >= duracaoQuebrarPedra)
        {

            if (pedreiro.pedraPrefab != null && pedreiro.localItem != null)
            {
                GameObject novoRecurso = GameObject.Instantiate(
                    pedreiro.pedraPrefab,
                    pedreiro.localItem.position,
                    pedreiro.localItem.rotation,
                    pedreiro.localItem
                );
                SetItemCarregado(novoRecurso);
                Debug.Log("Carregando pedra");
            }


            if (pedreiro.ferramentaInstanciada != null)
            {
                pedreiro.ferramentaInstanciada.SetActive(false);


                BalancarMachado balanco = pedreiro.ferramentaInstanciada.GetComponent<BalancarMachado>();
                if (balanco != null)
                {
                    balanco.enabled = false;
                }
            }

            timer = 0f;
            navAgent.isStopped = false;
            pedreiro.targetFontePedra = null;
            return NodeState.Success;
        }


        timer += Time.deltaTime;
        return NodeState.Running;
    }
}