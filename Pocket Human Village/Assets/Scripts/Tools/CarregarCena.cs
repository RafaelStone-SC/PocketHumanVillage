using UnityEngine;
using UnityEngine.SceneManagement;

public class CarregarCena : MonoBehaviour
{
    
   
    public string nomeDaCenaAlvo;


    public void CarregarCenaPeloNome()
    {
       
        if (string.IsNullOrEmpty(nomeDaCenaAlvo))
        {
            
            return;
        }

       
        SceneManager.LoadScene(nomeDaCenaAlvo);
    }
}