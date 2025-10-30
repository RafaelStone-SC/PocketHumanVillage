using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InfoPersonagens : MonoBehaviour
{
    public static InfoPersonagens Instance { get; private set; }

    [Header("Componentes de UI")]
    public GameObject panelRoot; 
    public TextMeshProUGUI nameText;
    public Image characterImage;
    public TextMeshProUGUI fomeText;
    public TextMeshProUGUI energiaText;
    public TextMeshProUGUI vidaText; 

    

    
    private Character selectedCharacter;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

       
        if (panelRoot != null) panelRoot.SetActive(false);
    }

   
    void Update()
    {
        if (panelRoot.activeInHierarchy && selectedCharacter != null)
        {
            UpdateStatus(selectedCharacter);
        }
    }

    
    public void DisplayCharacter(Character character, string characterName, Sprite characterSprite)
    {
        selectedCharacter = character;

        if (panelRoot != null)
        {
            panelRoot.SetActive(true); 
        }
        if (characterImage != null)
        {
            characterImage.sprite = characterSprite;
        }


        nameText.text = characterName;
        

       
        UpdateStatus(character);
    }

    
    public void UpdateStatus(Character character)
    {
       
        fomeText.text = $"Fome:{Mathf.RoundToInt(character.Fome)}/100";
        energiaText.text = $"Energia:{Mathf.RoundToInt(character.Energia)}/100";

       
        vidaText.text = $"Vida:{Mathf.RoundToInt(character.Vida)}/100";
    }

   
    public void ClosePanel()
    {
        if (panelRoot != null) panelRoot.SetActive(false);
        selectedCharacter = null;
    }
}