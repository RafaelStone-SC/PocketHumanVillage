using UnityEngine;

public class FolhaCortarMadeira : Node
{
    private float duracaoCortarMadeira = 3.0f;
    private float timer = 0f;
    private const int danoPorGolpe = 5;

    private const float FOME_POR_SEGUNDO = 0.1f;
    private const float ENERGIA_POR_SEGUNDO = 0.1f;

    public FolhaCortarMadeira(Character c) : base(c) { }

    public override NodeState Execute()
    {
        Lenhador agent = AgentLenhador;
        FonteMadeira fonte = null;
        character.Fome += Time.deltaTime * FOME_POR_SEGUNDO;
        character.Energia -= Time.deltaTime * ENERGIA_POR_SEGUNDO;

        character.Energia = Mathf.Max(0, character.Energia);
        
        if (AgentLenhador.targetMadeira != null)
        {
            fonte = AgentLenhador.targetMadeira.GetComponent<FonteMadeira>();
        }

        
        if (AgentLenhador.targetMadeira == null || fonte == null)
        {
            AgentLenhador.targetMadeira = null;
            return NodeState.Failure;
        }

       
        if (timer >= duracaoCortarMadeira)
        {
           
            fonte.ReceberDano(danoPorGolpe);

           
            if (AgentLenhador.machadoInstanciado != null)
            {
                AgentLenhador.machadoInstanciado.SetActive(false);
               
            }

           
            if (fonte.vidaAtual <= 0)
            {
                
                if (AgentLenhador.madeiraPrefab != null && AgentLenhador.localItem != null)
                {
                    GameObject novoRecurso = GameObject.Instantiate(
                        AgentLenhador.madeiraPrefab,
                        AgentLenhador.localItem.position,
                        AgentLenhador.localItem.rotation,
                        AgentLenhador.localItem
                    );
                    AgentLenhador.itemCarregado = novoRecurso;
                    Debug.Log("Carregando Madeira");
                }

               
                AgentLenhador.targetMadeira = null;
            }

            timer = 0f;
            AgentLenhador.agentLenhador.isStopped = false;
            return NodeState.Success;
        }

        
        if (timer == 0f)
        {
            AgentLenhador.agentLenhador.isStopped = true;
            Debug.Log("Começando a cortar madeira");

           
            if (AgentLenhador.machadoInstanciado == null)
            {
                
                AgentLenhador.machadoInstanciado = GameObject.Instantiate(
                    AgentLenhador.machadoPrefab,
                    AgentLenhador.localMachado.position,
                    AgentLenhador.localMachado.rotation,
                    AgentLenhador.localItem
                );
                
                if (AgentLenhador.machadoInstanciado.GetComponent<BalancarMachado>() == null)
                {
                    AgentLenhador.machadoInstanciado.AddComponent<BalancarMachado>();
                }
            }

            
            if (AgentLenhador.machadoInstanciado != null)
            {
                AgentLenhador.machadoInstanciado.SetActive(true);
                AgentLenhador.machadoInstanciado.GetComponent<BalancarMachado>().enabled = true;
            }
        }

        
        timer += Time.deltaTime;
        return NodeState.Running;
    }
}