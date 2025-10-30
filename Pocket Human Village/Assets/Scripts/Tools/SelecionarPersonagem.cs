using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SelecionarPersonagem : MonoBehaviour
{
    
    public Character dadosDoPersonagem;

    public string characterName = "Personagem";
    public Sprite characterSprite;

    void OnMouseDown()
    {
        
        if (dadosDoPersonagem == null)
        {
           
            return;
        }

        if (InfoPersonagens.Instance != null)
        {
            InfoPersonagens.Instance.DisplayCharacter(
                dadosDoPersonagem,
                characterName,
                characterSprite
            );
        }
        else
        {
            Debug.Log("Sem info");
        }
    }
}