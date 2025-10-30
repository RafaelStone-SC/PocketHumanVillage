using System.Collections.Generic;

public class FerreiroBT
{
    private Node rootNode;
    private Character character;

    public FerreiroBT(Character c)
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
        var ChecarRisco = new VerificarRiscoDeVida(character);
        var EntrarEmDesespero = new FolhaDesespero(character);
        var RiscoVida = new Sequence(character, new List<Node> { ChecarRisco, EntrarEmDesespero });

        //Comer
        var VerificarFome = new VerificarFome(character, 90);
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
        var IndoMinerarFerro = new FolhaIndoMinerarFerro(character);
        var MinerarFerro = new FolhaMinerarFerro(character);
        var Trabalho = new Sequence(character, new List<Node> { new Negation(character, ChecaItem), IndoMinerarFerro, MinerarFerro });

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