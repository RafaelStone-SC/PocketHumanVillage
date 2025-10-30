using System;
using UnityEngine;
using UnityEngine.AI;

public class FolhaMinerarFerro : Node
{

    private const float duraçãoMinerarFerro = 40.0f;


    private float timer = 0f;

    private const float FOME_POR_SEGUNDO = 0.1f;
    private const float ENERGIA_POR_SEGUNDO = 0.1f;

    public FolhaMinerarFerro(Character c) : base(c) { }


    private NavMeshAgent GetNavAgent()
    {
        if (AgentFerreiro != null) return AgentFerreiro.agentFerreiro;
        return null;
    }
    private GameObject GetItemCarregado()
    {
        if (AgentFerreiro != null) return AgentFerreiro.itemCarregado;
        return null;
    }
    private void SetItemCarregado(GameObject item)
    {
        if (AgentFerreiro != null) AgentFerreiro.itemCarregado = item;
    }


    public override NodeState Execute()
    {
        Ferreiro ferreiro = AgentFerreiro;
        NavMeshAgent navAgent = GetNavAgent();

        if (ferreiro == null || navAgent == null)
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


        if (ferreiro.targetFonteFerro == null)
        {
            timer = 0f;
            return NodeState.Failure;
        }



        if (timer == 0f)
        {
            navAgent.isStopped = true;
           


            if (ferreiro.ferramentaInstanciada == null)
            {

                ferreiro.ferramentaInstanciada = GameObject.Instantiate(
                    ferreiro.picaretaReforcadaPrefab,
                    ferreiro.localFerramenta.position,
                    ferreiro.localFerramenta.rotation,
                    ferreiro.localFerramenta
                );


                if (ferreiro.ferramentaInstanciada.GetComponent<BalancarMachado>() == null)
                {
                    ferreiro.ferramentaInstanciada.AddComponent<BalancarMachado>();
                }
            }

            if (ferreiro.ferramentaInstanciada != null)
            {
                ferreiro.ferramentaInstanciada.SetActive(true);


                BalancarMachado balanco = ferreiro.ferramentaInstanciada.GetComponent<BalancarMachado>();
                if (balanco != null)
                {
                    balanco.enabled = true;
                }
            }
        }


        else
        {

            if (ferreiro.ferramentaInstanciada != null)
            {
                BalancarMachado balanco = ferreiro.ferramentaInstanciada.GetComponent<BalancarMachado>();
                if (balanco != null && !balanco.enabled)
                {
                    balanco.enabled = true;
                }
            }
        }



        if (timer >= duraçãoMinerarFerro)
        {

            if (ferreiro.ferroPrefab != null && ferreiro.localItem != null)
            {
                GameObject novoRecurso = GameObject.Instantiate(
                    ferreiro.ferroPrefab,
                    ferreiro.localItem.position,
                    ferreiro.localItem.rotation,
                    ferreiro.localItem
                );
                SetItemCarregado(novoRecurso);
               
            }


            if (ferreiro.ferramentaInstanciada != null)
            {
                ferreiro.ferramentaInstanciada.SetActive(false);


                BalancarMachado balanco = ferreiro.ferramentaInstanciada.GetComponent<BalancarMachado>();
                if (balanco != null)
                {
                    balanco.enabled = false;
                }
            }

            timer = 0f;
            navAgent.isStopped = false;
            ferreiro.targetFonteFerro = null;
            return NodeState.Success;
        }


        timer += Time.deltaTime;
        return NodeState.Running;
    }
}