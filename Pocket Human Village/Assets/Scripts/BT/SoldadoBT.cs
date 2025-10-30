using System.Collections.Generic;
using System.Linq;

public class SoldadoBT
{
    private Node rootNode;
    private Character character;

    public SoldadoBT(Character c)
    {
        this.character = c;
        SetupTree();
    }

    private void SetupTree()
    { 
        
        //RiscoVIda
        var ChecarRisco = new VerificarRiscoDeVida(character);
        var EntrarEmDesespero = new FolhaDesespero(character);
        var RiscoVida = new Sequence(character, new List<Node> { ChecarRisco, EntrarEmDesespero });

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

        //Trabalho
        var EncontrarInimigo = new FolhaEncontrarInimigo(character);
        var PerseguirInimigo = new FolhaPerseguirInimigo(character);
        var AtacarInimigo = new FolhaAtacarInimigo(character);
        var Trabalho = new Sequence(character, new List<Node> { EncontrarInimigo, PerseguirInimigo, AtacarInimigo });

        //Patrulhar
        var Patrulha = new FolhaAndarAleatoriamente(character);

        this.rootNode = new Selector(character, new List<Node>
        {
            RiscoVida,
            Comer,
            Descansar,        
            Trabalho,
            Patrulha                    
        });
    }

    public void Update()
    {
        
        rootNode.Execute();
    }
}