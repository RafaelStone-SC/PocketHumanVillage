using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro; 

public class UIRecursos : MonoBehaviour
{
    
    public static UIRecursos Instance { get; private set; }

    [Header("UI")]
    public TextMeshProUGUI madeiraText;
    public TextMeshProUGUI pedraText;
    public TextMeshProUGUI ferroText;
    public TextMeshProUGUI ouroText;
    public TextMeshProUGUI comidaText;

    private void Awake()
    {
       
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

   
    private void Update()
    {
       if(RecursosManager.Instance != null)
         {
            AtualizarHUD(); 
         }
    }
    

    
    public void AtualizarHUD()
    {
        if (RecursosManager.Instance == null) return;

        
        int madeira = RecursosManager.Instance.Valorrecurso("Madeira");
        int pedra = RecursosManager.Instance.Valorrecurso("Pedra");
        int ferro = RecursosManager.Instance.Valorrecurso("Ferro");
        int ouro = RecursosManager.Instance.Valorrecurso("Ouro");
        int comida = RecursosManager.Instance.Valorrecurso("Comida");

        if (madeiraText != null) madeiraText.text = $"{madeira}";
        if (pedraText != null) pedraText.text = $"{pedra}";
        if (ferroText != null) ferroText.text = $"{ferro}";
        if (ouroText != null) ouroText.text = $"{ouro}";
        if (comidaText != null) comidaText.text = $"{comida}";
    }
}