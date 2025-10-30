using UnityEngine;

public class Castelo : MonoBehaviour
{
    public int vidaMaxima = 1000;
    public int vidaAtual;

    void Start()
    {
       

       
        if (GetComponent<SelecionarPersonagem>() is SelecionarPersonagem seletor)
        {
           
            seletor.dadosDoPersonagem = new Character(gameObject.name);

           
            seletor.dadosDoPersonagem.Vida = vidaAtual;

            
            seletor.characterName = "Castelo";

            
        }
    }
    void Update()
    {
        if(GetComponent<SelecionarPersonagem>()?.dadosDoPersonagem != null)
        {
            GetComponent<SelecionarPersonagem>().dadosDoPersonagem.Vida = vidaAtual;
        }    
    }
    public void ReceberDano(int dano)
    {
        if(GameManager.Instance.EstadoAtual != GameState.Horda)
        {
            return;
        }
        vidaAtual -= dano;
        vidaAtual = Mathf.Max(0, vidaAtual);

        if(vidaAtual <= 0)
        {
            GameManager.Instance.Derrota();
        }
    }
    public bool Destruido()
    {
        return vidaAtual <= 0;
    }
}
