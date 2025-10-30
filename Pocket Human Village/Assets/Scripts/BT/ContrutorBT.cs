using System.Collections.Generic;
using UnityEngine.AI;

public class ConstrutorBT
{
    private Node rootNode;
    private Character character;
    

    public ConstrutorBT(Character c)
    {
        this.character = c;
        SetupTree();
    }

    private void SetupTree()
    {

        //PerigoHorda
        var ChecarHorda = new VerificarModoHorda(character);
        var IrParaRefugio = new FolhaIrParaCastelo(character);
        var PerigoHorda = new Sequence(character, new List<Node> { ChecarHorda, IrParaRefugio });

        //RiscoVida
        var ChecarRsico = new VerificarRiscoDeVida(character);
        var EntrarEmDesespero = new FolhaDesespero(character);
        var RiscoVida = new Sequence(character, new List<Node> { ChecarRsico, EntrarEmDesespero });

        //Comer
        var VerificarFome = new VerificarFome(character, 20f);
        var IrParaRefeitorio = new FolhaIrParaRefeitorio(character);
        var FolhaComer = new FolhaComer(character);
        var Comer = new Sequence(character, new List<Node> { VerificarFome, IrParaRefeitorio, FolhaComer });

        //Descansar
        var VerificarCansaço = new VerificarEnergia(character, 10f);
        var IrParaDescanso = new FolhaIrParaDescanso(character);
        var FolhaDescansar = new FolhaDescansar(character);
        var Descansar = new Sequence(character, new List<Node> { VerificarCansaço, IrParaDescanso, FolhaDescansar });

        //Trabalho
        var ChecarCasa = new VerificarCasaParaContruir(character);
        var IrParaContrucao = new FolhaIrParaConstrucao(character);
        var Construir = new FolhaConstruirCasa(character);    
        var Trabalho = new Sequence(character, new List<Node>(){ChecarCasa,IrParaContrucao,Construir});

      //Andar
        var Andar = new FolhaAndarAleatoriamente(character);

        
        this.rootNode = new Selector(character, new List<Node>
        {
            PerigoHorda,
            RiscoVida,          
            Comer,          
            Descansar,    
            Trabalho,            
            Andar                           
        });
    }

    public void Update()
    {
       
        rootNode.Execute();
    }
}