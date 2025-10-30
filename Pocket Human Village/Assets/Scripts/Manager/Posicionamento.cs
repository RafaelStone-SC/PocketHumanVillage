using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Posicionamento : MonoBehaviour
{
   public static Posicionamento Instance { get; private set; }
    public GameObject prefabEmEpsera;
    private GameObject visualizador;
    public LayerMask LayerChao;
    public float raioDeChecagem = 2f;

    public Material materialValido;
    public Material materialInvalido;

   
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    private void Update()
    {
        if(visualizador != null)
        {
            VerPosicao();
        }
    }
   
    public void EntrarModoPosicionamento(GameObject prefab)
    {
        if (visualizador != null) return;
        prefabEmEpsera = prefab;

        visualizador = Instantiate(prefabEmEpsera);
        DesativarComponentes(visualizador);
    }
    private void VerPosicao()
    {
        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
        if(visualizador != null)
        {

            Debug.DrawRay(ray.origin, ray.direction * 1000f, Color.yellow);
        }

        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, LayerChao))
        {;
            Debug.Log("Local encontrado para colocar a casa");
            visualizador.transform.position = hit.point;
            bool localValido = ChecarValidadePosicao(hit.point);
            MudarMaterialVisualizador(localValido ? materialValido : materialInvalido);

            if(Input.GetMouseButtonDown(0) && localValido)
            {
                Colocarestrutura(hit.point);
            }
        }
        else if(visualizador != null)
        {
            Debug.Log("o raio nao atinge o chao");
        }
        if (Input.GetMouseButtonDown(1))
        {
            SairModoPosicionamento(false);
        }
    }
    private bool ChecarValidadePosicao(Vector3 position)
    {
        if( Physics.CheckSphere(position, raioDeChecagem, LayerMask.GetMask("Obstaculos")))
        {
            return false;

        }
        NavMeshHit hit;
        if(!NavMesh.SamplePosition(position, out hit, 1.0f, NavMesh.AllAreas)) return false;
        return true;
    }
    private void MudarMaterialVisualizador(Material mat)
    {
        Renderer[] renderers = visualizador.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in renderers)
        {
            r.material = mat;
        }
    }
    private void DesativarComponentes(GameObject go)
    {
        if(go.GetComponent<EstruturaCasa>() != null)
        {
            go.GetComponent<EstruturaCasa>().enabled = false;
        }
        Collider[] colliders = go.GetComponentsInChildren<Collider>();
        foreach (Collider c  in colliders)
        {
            c.enabled = false;
        }
    }

    private void Colocarestrutura(Vector3 posicaoFinal)
    {
        GameObject casaFinal = Instantiate(prefabEmEpsera, posicaoFinal, Quaternion.identity);
        casaFinal.name = "Casa";

        RecursosManager.Instance.FinalizarTransacaoCasa(posicaoFinal);
        VilaEstruturasBasicas.Instance.RegistrarEstrutura("Casa", casaFinal.transform);
        SairModoPosicionamento(true);
    }
    public void SairModoPosicionamento(bool sucesso)
    {
        Destroy(visualizador);
        visualizador = null;
        prefabEmEpsera = null;
    }
}
