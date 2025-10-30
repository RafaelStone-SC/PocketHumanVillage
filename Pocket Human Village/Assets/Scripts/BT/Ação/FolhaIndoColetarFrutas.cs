using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class FolhaIndoColetarFrutas : Node
{
    public FolhaIndoColetarFrutas(Character c) : base(c) { }

    public override NodeState Execute()
    {
        Coletora agent = AgentColetora;
        if (agent == null) return NodeState.Failure;

       
        if (agent.targetFonteFrutas == null || agent.targetFonteFrutas.GetComponent<FonteFrutas>() == null)
        {
           
            GameObject[] fontes = GameObject.FindGameObjectsWithTag("FonteFrutas");

            if (fontes.Length == 0) return NodeState.Failure;

           
            GameObject arvoreMaisProxima = fontes
                .Where(f => f != null)
                .Select(f => new { GameObject = f, Fonte = f.GetComponent<FonteFrutas>() })
               
                .Where(x => x.Fonte != null && x.Fonte.TemFrutasDisponiveis()) 
                .OrderBy(x => Vector3.Distance(x.GameObject.transform.position, agent.transform.position))
                .Select(x => x.GameObject)
                .FirstOrDefault();

            if (arvoreMaisProxima != null)
            {
                agent.targetFonteFrutas = arvoreMaisProxima;
            }
            else
            {
                Debug.Log("Sem arvores de frutas");
                return NodeState.Failure;
            }
        }

        
        Vector3 destino = agent.targetFonteFrutas.transform.position;

        if (Vector3.Distance(agent.transform.position, destino) < 5f)
        {
            Debug.Log("Chegou na fonte de frutas.");
            return NodeState.Success;
        }

        if (agent.destinoAtual != destino)
        {
            agent.Novodestino(destino);
            Debug.Log("Indo coletar frutas.");
        }

        return NodeState.Running;
    }

}