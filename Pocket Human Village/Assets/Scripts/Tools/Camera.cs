using UnityEngine;

public class Camera : MonoBehaviour
{
    public float velocidade = 40;
    public float limiteX = 50f;
    public float limiteZ = 50f;
    public float zonaBorda = 10f;

    public float zoomVel = 200f;
    public float alturaMin = 10f;
    public float alturaMax = 60f;

   
  

    Vector3 posInicial;

    void Start()
    {
        posInicial = transform.position;
    }

    void Update()
    {
        Mover();
        Zoom();
        
    }

    void Mover()
    {
        Vector3 dir = Vector3.zero;

        if (Input.GetKey("w"))
        {
            dir += Vector3.back;
        }
        if (Input.GetKey("s"))
        {
            dir += Vector3.forward;
        }
        if (Input.GetKey("a"))
        {
            dir += Vector3.right;
        }
        if (Input.GetKey("d"))
        {
            dir += Vector3.left;
        }

        if (Input.mousePosition.y >= Screen.height - zonaBorda)
        {
            dir += Vector3.back;
        }
        if (Input.mousePosition.y <= zonaBorda)
        {
            dir += Vector3.forward;
        }
        if (Input.mousePosition.x <= zonaBorda)
        {
            dir += Vector3.right;
        }

        if (Input.mousePosition.x >= Screen.width - zonaBorda)
        {
            dir += Vector3.left;
        }

        Vector3 mov = dir.normalized * velocidade * Time.deltaTime;
        Vector3 novaPos = transform.position + mov;

        novaPos.x = Mathf.Clamp(novaPos.x, -limiteX, limiteX);
        novaPos.z = Mathf.Clamp(novaPos.z, -limiteZ, limiteZ);

        transform.position = novaPos;
    }

    void Zoom()
    {
        float rolagem = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;
        pos.y -= rolagem * zoomVel * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, alturaMin, alturaMax);
        transform.position = pos;
    }

   

    
}
