using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;




    public class Lenhador : MonoBehaviour
    {
        public Character dadosPersonagem;
        private LenhadorBT btLenhador;
        public Transform localItem;
        public Transform localMachado;
        public GameObject itemCarregado { get; set; } = null;
        public GameObject madeiraPrefab;
        public GameObject machadoPrefab;
        
        public GameObject targetMadeira;
        public GameObject machadoInstanciado;

        public NavMeshAgent agentLenhador;
        private VilaEstruturasBasicas fonte;
        public Vector3 destinoAtual { get; private set; }


        void Start()
        {
            agentLenhador = GetComponent<NavMeshAgent>();

            dadosPersonagem = new Character(gameObject.name, this);
            btLenhador = new LenhadorBT(dadosPersonagem);
            SelecionarPersonagem seletor = GetComponent<SelecionarPersonagem>();
            if (seletor != null)
            {

                seletor.dadosDoPersonagem = dadosPersonagem;
            }
        }


        void Update()
        {

            if (dadosPersonagem == null || btLenhador == null) return;


            if (agentLenhador != null && agentLenhador.velocity.sqrMagnitude > 0.01f)
            {
                dadosPersonagem.Fome += Time.deltaTime * 0.1f;
                dadosPersonagem.Energia -= Time.deltaTime * 0.1f;
            }


            dadosPersonagem.Energia = Mathf.Max(0, dadosPersonagem.Energia);
            dadosPersonagem.Fome = Mathf.Max(0, dadosPersonagem.Fome);
            dadosPersonagem.Energia = Mathf.Min(100f, dadosPersonagem.Energia);

            btLenhador.Updade();
        }
        public void Novodestino(Vector3 novoDestino)
        {

            if (agentLenhador == null || !agentLenhador.isActiveAndEnabled)
            {

                return;
            }

            destinoAtual = novoDestino;

            agentLenhador.isStopped = false;

            if (!agentLenhador.SetDestination(novoDestino))
            {

            }
        }
    }

