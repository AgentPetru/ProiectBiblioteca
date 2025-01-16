
public abstract class Resursa
{
    public string ID { get; set; }
    public string Titlu { get; set; }
    public string Autor { get; set; }
    public string Gen { get; set; }
    public DateTime DataPublicarii { get; set; }
    public int StocDisponibil { get; set; }
    public string Domeniu { get; set; }
    public Resursa(string id, string titlu, string autor, string gen, DateTime dataPublicarii, int stocDisponibil, string domeniu)
    {
        ID = id;
        Titlu = titlu;
        Autor = autor;
        Gen = gen;
        DataPublicarii = dataPublicarii;
        StocDisponibil = stocDisponibil;
        Domeniu = domeniu;
    }
    public abstract bool PoateFiImprumutata(Utilizator utilizator);
    public override string ToString()
    {
        return $"ID: {ID}, Titlu: {Titlu}, Autor: {Autor}, Gen: {Gen}, Data Publicarii: {DataPublicarii.ToShortDateString()}, Stoc Disponibil: {StocDisponibil}, Domeniu: {Domeniu}, An Publicatie: {DataPublicarii.Year}";
    }
}
public class Manual : Resursa
{
    public string Curs { get; set; }
    public Manual(string id, string titlu, string autor, string gen, DateTime dataPublicarii, int stocDisponibil, string domeniu, string curs)
        : base(id, titlu, autor, gen, dataPublicarii, stocDisponibil, domeniu)
    {
        Curs = curs;
    }
    public override bool PoateFiImprumutata(Utilizator utilizator)
    {
        return utilizator.CursuriInscris.Contains(Curs) && StocDisponibil > 0;
    }
    public override string ToString()
    {
        return base.ToString() + $", Curs: {Curs}";
    }
}
public class CarteLectura : Resursa
{
    public CarteLectura(string id, string titlu, string autor, string gen, DateTime dataPublicarii, int stocDisponibil, string domeniu)
        : base(id, titlu, autor, gen, dataPublicarii, stocDisponibil, domeniu) { }
    public override bool PoateFiImprumutata(Utilizator utilizator)
    {
        return StocDisponibil > 0;
    }
}
public class EBook : Resursa
{
    public string LinkDescarcare { get; set; }
    public EBook(string id, string titlu, string autor, string gen, DateTime dataPublicarii, string linkDescarcare, string domeniu)
        : base(id, titlu, autor, gen, dataPublicarii, 0, domeniu)
    {
        LinkDescarcare = linkDescarcare;
    }
    public override bool PoateFiImprumutata(Utilizator utilizator)
    {
        return true; 
    }
    public override string ToString()
    {
        return $"ID: {ID}, Titlu: {Titlu}, Autor: {Autor}, Gen: {Gen}, Data Publicarii: {DataPublicarii.ToShortDateString()}, Stoc Disponibil: nelimitat, Link Descarcare: {LinkDescarcare}, Domeniu: {Domeniu}, An Publicatie: {DataPublicarii.Year}";
    }
}
public class Revista : Resursa
{
    public string Editie { get; set; }
    public string Numar { get; set; }
    public string Luna { get; set; }
    public Revista(string id, string titlu, string autor, string gen, DateTime dataPublicarii, int stocDisponibil, string domeniu, string editie, string numar, string luna)
        : base(id, titlu, autor, gen, dataPublicarii, stocDisponibil, domeniu)
    {
        Editie = editie;
        Numar = numar;
        Luna = luna;
    }
    public override bool PoateFiImprumutata(Utilizator utilizator)
    {
        return StocDisponibil > 0;
    }
    public override string ToString()
    {
        return base.ToString() + $", Editie: {Editie}, Numar: {Numar}, Luna: {Luna}";
    }
}