
public class Imprumut
{
    public string ID { get; private set; }
    public string IDResursa { get; private set; }
    public string IDUtilizator { get; private set; }
    public DateTime DataImprumut { get; private set; }
    public DateTime DataScadenta { get; private set; }
    public bool EsteReturnat { get; set; }
    public Imprumut(string id, string idResursa, string idUtilizator, int perioadaImprumut)
    {
        ID = id; IDResursa = idResursa; IDUtilizator = idUtilizator;
        DataImprumut = DateTime.Now; DataScadenta = DateTime.Now.AddDays(perioadaImprumut);
        EsteReturnat = false;
    }
}