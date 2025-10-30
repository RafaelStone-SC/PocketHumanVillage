using System.Collections.Generic;
using System;
using UnityEngine.AI;

public class LenhadorBT
{
    private Node rootNode;
    private Character character;
    

    public LenhadorBT(Character c)
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

        //RiscoVIda
        var ChecarRisco = new VerificarRiscoDeVida(character);
        var EntrarEmDesespero = new FolhaDesespero(character);
        var RiscoVida = new Sequence(character,new List<Node> { ChecarRisco, EntrarEmDesespero });

       //Comer
        var VerificarFome = new VerificarFome(character, 90f);
        var IrParaRefeitorio = new FolhaIrParaRefeitorio(character);
        var FolhaComer = new FolhaComer(character);
        var Comer = new Sequence(character, new List<Node> { VerificarFome, IrParaRefeitorio, FolhaComer });

        //Descanso
        var VerificarCansaço = new VerificarEnergia(character, 10f);
        var IrParaDescanso = new FolhaIrParaDescanso(character);
        var FolhaDescansar = new FolhaDescansar(character);
        var Descansar = new Sequence(character, new List<Node> { VerificarCansaço, IrParaDescanso, FolhaDescansar });

        //Item
        var ChecaItem = new VerificarCarregamento(character);
        var IrParaDeposito = new FolhaIrParaDeposito(character);
        var DepositarRecurso = new FolhaDepositarRecurso(character);
        var Item = new Sequence(character, new List<Node> { ChecaItem, IrParaDeposito, DepositarRecurso });

        //Trabalho
        var IndoCortarMadeira = new FolhaIndoCortarMadeira(character);
        var CortarMadeira = new FolhaCortarMadeira(character);        
        var Trabalho = new Sequence(character, new List<Node> { new Negation(character, ChecaItem), IndoCortarMadeira, CortarMadeira });

        
        this.rootNode = new Selector(character, new List<Node>
    {
            PerigoHorda,
            RiscoVida,
            Comer, 
            Descansar, 
            Item,   
            Trabalho
    });
    }

    public void Updade()
    {
        rootNode.Execute();
    }

}