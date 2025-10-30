using System.Collections;
using UnityEngine;

public class FonteMadeira : MonoBehaviour
{
    [Header("Config Recurso")]
    public string tipoRecurso = "Madeira";
    public int vidaMaxima = 100;
    public int vidaAtual;
    

    [Header("FeedBack Visual")]
   
    public float tempoDeDestruição = 2.0f;
    public GameObject troncoRestantePrefab;



    private const string CAIU = "QuedaArvore";
    private const string GOLPE = "Golpe";
    private Animator anim;

    

    private void Awake()
    {
        anim = GetComponent<Animator>();
        vidaAtual = vidaMaxima;
       
       

    }
    private void Update()
    {

    }

    public void ReceberDano(int dano)
    {

        anim.Play(GOLPE);
        if (vidaAtual <= 0) return;
        vidaAtual -= dano;

        if (vidaAtual <= 0)
        {
            Cortar();
        }
    }
    private void Cortar()
    {
       
        Debug.Log("A arvore foi cortada");
        if (troncoRestantePrefab != null)
        {
            Instantiate(troncoRestantePrefab, transform.position, transform.rotation);
        }
        
        anim.Play(CAIU);
        StartCoroutine(DestruirComFade(tempoDeDestruição));
    }
    private IEnumerator DestruirComFade( float duracao)
    {
        float timer = 0f;
        
        while(timer < duracao)
        {
            timer += Time.deltaTime;
            float t = 1f - (timer / duracao);

            
            yield return null;
        }
        Destroy(gameObject);
    }

}
