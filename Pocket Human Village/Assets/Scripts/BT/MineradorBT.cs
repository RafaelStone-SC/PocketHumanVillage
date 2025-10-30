using System.Collections.Generic;
using System;
using UnityEngine.AI;

public class MineradorBT
{
    private Node rootNode;
    private Character character;

    public MineradorBT(Character c)
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

       //Item
        var ChecaItem = new VerificarCarregamento(character);
        var IrParaDeposito = new FolhaIrParaDeposito(character);
        var DepositarRecurso = new FolhaDepositarRecurso(character);
        var Item = new Sequence(character, new List<Node> { ChecaItem, IrParaDeposito, DepositarRecurso });

       //Trabalho
        var IndoMinarOuro = new FolhaIndoMinerarOuro(character);
        var MinarOuro = new FolhaMinerarOuro(character);
        var Trabalho = new Sequence(character, new List<Node> { new Negation(character, ChecaItem), IndoMinarOuro, MinarOuro });

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