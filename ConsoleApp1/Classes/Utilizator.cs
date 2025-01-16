
public abstract class Utilizator
{
    public string ID { get; set; }
    public string Nume { get; set; }
    public string NumeUtilizator { get; set; }
    public string Parola { get; set; }
    public List<string> CursuriInscris { get; set; }
    public List<Imprumut> IstoricImprumuturi { get; set; }
    public int NumarPenalizari { get; set; }
    public List<Resursa> ListaAsteptare { get; set; }
    public Utilizator(string id, string nume, string numeUtilizator, string parola)
    {
        ID = id; Nume = nume; NumeUtilizator = numeUtilizator; Parola = parola;
        CursuriInscris = new List<string>(); IstoricImprumuturi = new List<Imprumut>();
        NumarPenalizari = 0; ListaAsteptare = new List<Resursa>();
    }
    public abstract int NumarMaximCarti { get; }
    public abstract int PerioadaImprumut { get; }
    public void InscrieLaCurs(string curs) => CursuriInscris.Add(curs);
}
public class UtilizatorStandard : Utilizator
{
    public UtilizatorStandard(string id, string nume, string numeUtilizator, string parola)
        : base(id, nume, numeUtilizator, parola) { }
    public override int NumarMaximCarti => NumarPenalizari > 0 ? 1 : 3;
    public override int PerioadaImprumut => 14;
}
public class UtilizatorAvansat : Utilizator
{
    public UtilizatorAvansat(string id, string nume, string numeUtilizator, string parola)
        : base(id, nume, numeUtilizator, parola) { }
    public override int NumarMaximCarti => NumarPenalizari > 0 ? 2 : 5;
    public override int PerioadaImprumut => 21;
}