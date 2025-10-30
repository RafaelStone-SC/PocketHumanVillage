using UnityEngine;

public class BalancarMachado : MonoBehaviour
{
    [Header("Config Machado")]
    public float velocidadeRotacao = 500f;
    public float limiteAngulo = 45f;

    private int direcao = 1;

    private void Update()
    {
        float anguloDelta = velocidadeRotacao * Time.deltaTime * direcao;
        transform.Rotate(anguloDelta, 0 ,0);

        if (transform.eulerAngles.x >= limiteAngulo && transform.localEulerAngles.x < 180f)
        {
            direcao = -1;
        }
        else if(transform.localEulerAngles.x <= (360f - limiteAngulo) && transform.localEulerAngles.x > 180f)
        {
            direcao = 1;
        }

    }
}
