
public class Biblioteca
{
    private List<Resursa> resurse = new List<Resursa>();
    private List<Utilizator> utilizatori = new List<Utilizator>();
    private List<Imprumut> imprumuturiActive = new List<Imprumut>();

    public List<Utilizator> GetUtilizatori()
    {
        return utilizatori;
    }
    public void AdaugaResursa(Resursa resursa)
    {
        if (resursa != null)
        {
            resurse.Add(resursa);
        }
        else
        {
            Console.WriteLine("Resursa nu poate fi adăugată. Obiectul este nul.");
        }
    }
    public void AdaugaUtilizator(Utilizator utilizator)
    {
        if (utilizator != null)
        {
            utilizatori.Add(utilizator);
        }
        else
        {
            Console.WriteLine("Utilizatorul nu poate fi adăugat. Obiectul este nul.");
        }
    }
    public bool ImprumutaResursa(Utilizator utilizator, string idResursa)
    {
        if (utilizator == null)
        {
            Console.WriteLine("Utilizatorul este nul.");
            return false;
        }
        var resursa = ObtineResursaDupaId(idResursa);

        if (resursa == null)
        {
            Console.WriteLine($"Resursa cu ID-ul '{idResursa}' nu a fost gasita.");
            return false;
        }
        if (resursa is not EBook && resursa.StocDisponibil == 0)
        {
            Console.WriteLine($"Resursa cu ID-ul '{idResursa}' nu este momentan in stoc.");
            return false;
        }
        if (resursa.PoateFiImprumutata(utilizator) && utilizator.IstoricImprumuturi.Count < utilizator.NumarMaximCarti)
        {
            if (resursa is not EBook)
            {
                resursa.StocDisponibil--;
            }

            var imprumut = new Imprumut(Guid.NewGuid().ToString(), idResursa, utilizator.ID, utilizator.PerioadaImprumut);
            imprumuturiActive.Add(imprumut);
            utilizator.IstoricImprumuturi.Add(imprumut);

            Console.WriteLine($"Resursa cu ID-ul '{idResursa}' a fost imprumutata cu succes!");

            if (resursa is EBook ebook)
            {
                Console.WriteLine($"ID-ul resursei: {ebook.ID}, Link-ul a fost trimis: {ebook.LinkDescarcare}");
            }
            return true;
        }
        else
        {
            Console.WriteLine($"Resursa cu ID-ul '{idResursa}' nu poate fi imprumutata.");
        }

        return false;
    }
    public void AfiseazaDateImprumut(Utilizator utilizator)
    {
        if (utilizator.IstoricImprumuturi.Any())
        {
            var ultimaDataImprumut = utilizator.IstoricImprumuturi.Last();
            Console.WriteLine($"Data imprumutului: {ultimaDataImprumut.DataImprumut.ToString("dd/MM/yyyy")}");
            Console.WriteLine($"Data scadentei: {ultimaDataImprumut.DataScadenta.ToString("dd/MM/yyyy")}");
        }
    }
    public void ReturneazaResursa(Utilizator utilizator, string idResursa)
    {
        if (utilizator == null)
        {
            Console.WriteLine("Utilizatorul este nul.");
            return;
        }
        var imprumut = imprumuturiActive.FirstOrDefault(i => i.IDUtilizator == utilizator.ID && i.IDResursa == idResursa && !i.EsteReturnat);
        if (imprumut != null)
        {
            imprumut.EsteReturnat = true;

            var resursa = resurse.FirstOrDefault(r => r.ID == imprumut.IDResursa);

            if (resursa != null)
            {
                resursa.StocDisponibil++;
                Console.WriteLine($"Resursa cu ID-ul '{idResursa}' a fost returnată cu succes.");
            }
            else
            {
                Console.WriteLine($"Resursa cu ID-ul '{idResursa}' nu a fost găsită.");
            }
        }
        else
        {
            Console.WriteLine($"Nu există împrumut activ pentru resursa cu ID-ul '{idResursa}'.");
        }
    }
    public void VerificaImprumuturiExpirate()
    {
        foreach (var imprumut in imprumuturiActive.Where(i => !i.EsteReturnat && i.DataScadenta < DateTime.Now))
        {
            var utilizator = utilizatori.FirstOrDefault(u => u.ID == imprumut.IDUtilizator);
            if (utilizator != null)
            {
                utilizator.NumarPenalizari++;
                Console.WriteLine($"Împrumutul cu ID-ul {imprumut.ID} este expirat! Penalizare aplicată.");
            }
        }
    }
    public void AfiseazaResurseDisponibile()
    {
        if (resurse.Any())
        {
            foreach (var resursa in resurse)
            {
                Console.WriteLine($"ID: {resursa.ID}, Titlu: {resursa.Titlu}, Stoc Disponibil: {resursa.StocDisponibil}");
            }
        }
        else
        {
            Console.WriteLine("Nu există resurse disponibile.");
        }
    }
    public void CautaResurse(string titlu = null, string autor = null, string domeniu = null)
    {
        var rezultate = resurse.Where(r =>
            (titlu == null || r.Titlu.Contains(titlu, StringComparison.OrdinalIgnoreCase)) &&
            (autor == null || r.Autor.Contains(autor, StringComparison.OrdinalIgnoreCase)) &&
            (domeniu == null || r.Domeniu.Contains(domeniu, StringComparison.OrdinalIgnoreCase))
        ).ToList();
        if (rezultate.Any())
        {
            Console.WriteLine("\nRezultate căutare:");
            foreach (var resursa in rezultate)
            {
                Console.WriteLine($"Titlu: {resursa.Titlu}, Autor: {resursa.Autor}, Domeniu: {resursa.Domeniu}");
            }
        }
        else
        {
            Console.WriteLine("\nNu s-au găsit resurse care să corespundă criteriilor de căutare.");
        }
    }
    public Resursa ObtineResursaDupaId(string id)
    {
        return resurse.FirstOrDefault(r => r.ID == id);
    }
}