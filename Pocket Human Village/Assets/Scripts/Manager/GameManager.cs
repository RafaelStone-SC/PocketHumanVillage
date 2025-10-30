using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum GameState
{
    Preparacao,
    Horda,
    Vitoria,
    Derrota
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public float tempoPreparoMax = 900f;
    private float tempoAtual;
    private GameState estadoAtual = GameState.Preparacao;

    public GameObject inimigoPrefab;
    public Transform[] spawPoints;
    public int inimigosPorWave = 10;
    public int totalWave = 3;
    private int waveAtual = 0;
    private int inimigoDerrotadosNaWave = 0;

    public GameState EstadoAtual => estadoAtual;
    public int WaveAtual => waveAtual;

    private void Awake()
    {
       if(Instance == null)
        {
            Instance = this;
            tempoAtual = tempoPreparoMax;
        }
       else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if(estadoAtual == GameState.Preparacao)
        {
            GerenciarTempoDePreparo();
        }
        else if((estadoAtual == GameState.Horda))
        {

        }
    }
    private void GerenciarTempoDePreparo()
    {
        if(tempoAtual > 0)
        {
            tempoAtual -= Time.deltaTime;
        }
        else
        {
            tempoAtual = 0;
            estadoAtual = GameState.Horda;
            IniciarHorda();
        }
    }

    private void IniciarHorda()
    {
        waveAtual = 1;
        inimigoDerrotadosNaWave = 0;
        SpawnarWave();
    }
    private void  SpawnarWave()
    {
        
        if(spawPoints.Length == 0)
        {
            return;
        }
        for(int i = 0; i < inimigosPorWave; i++)
        {
            Transform spawnPoint = spawPoints[Random.Range(0, spawPoints.Length)];
            Instantiate(inimigoPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
    public void InimigoDerrotado()
    {
        if(estadoAtual != GameState.Horda)
        {
            return ;
        }
        inimigoDerrotadosNaWave++;
        if(inimigoDerrotadosNaWave >= inimigosPorWave)
        {
            WaveFinalizada();
        }
    }
    private void WaveFinalizada()
    {
        waveAtual++;
        if(waveAtual > totalWave)
        {
            estadoAtual = GameState.Vitoria;

        }
        else
        {
            inimigoDerrotadosNaWave = 0;
            SpawnarWave();
        }
    }
    public void Derrota()
    {
        if(estadoAtual == GameState.Derrota)
        {
            return;
        }
        estadoAtual = GameState.Derrota;
        Time.timeScale = 0;
    }
}