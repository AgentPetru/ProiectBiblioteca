namespace ConsoleApp1;

using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        var biblioteca = new Biblioteca();
        biblioteca.AdaugaResursa(new Manual("M001", "Matematica de baza", "Ion Radulescu", "Educational", new DateTime(2015, 9, 15), 5, "Matematica elementara", "Matematica Avansata"));
        biblioteca.AdaugaResursa(new CarteLectura("CL001", "Morometii", "Marin Preda", "Fictiune", new DateTime(1955, 1, 1), 10, "Fictiune"));
        biblioteca.AdaugaResursa(new EBook("E001", "Introducere in informatica", "Grigore Moisil", "Tehnologie", new DateTime(1965, 5, 10), "https://ebook.informatica.ro", "Tehnologie"));
        biblioteca.AdaugaResursa(new Revista("R001", "Stiinta si Tehnica", "Autori Diversi", "Stiinta", new DateTime(2023, 1, 1), 15, "Stiinta", "2023", "01", "Ianuarie"));
        biblioteca.AdaugaResursa(new Manual("M002", "Fizica pentru liceu", "Alexandru Ionescu", "Educational", new DateTime(2010, 5, 20), 3, "Fizica generala", "Fizica Avansata"));
        biblioteca.AdaugaResursa(new CarteLectura("CL002", "Enigma Otiliei", "George Calinescu", "Fictiune", new DateTime(1938, 6, 15), 7, "Roman social"));
        biblioteca.AdaugaResursa(new EBook("E002", "Programare in C#", "John Smith", "Tehnologie", new DateTime(2020, 3, 25), "https://ebook.programare.ro", "Programare"));
        biblioteca.AdaugaResursa(new Revista("R002", "National Geographic", "National Geographic Society", "Stiinta", new DateTime(2023, 7, 1), 10, "Stiinta", "2023", "07", "Iulie"));
        biblioteca.AdaugaResursa(new Manual("M003", "Chimie Organică", "Vasile Popescu", "Educational", new DateTime(2018, 10, 10), 8, "Chimie", "Chimie avansată"));
        biblioteca.AdaugaResursa(new CarteLectura("CL003", "Ion", "Liviu Rebreanu", "Fictiune", new DateTime(1920, 1, 1), 12, "Roman psihologic"));
        biblioteca.AdaugaResursa(new EBook("E003", "Fundamentele Algoritmicii", "Dan Popa", "Tehnologie", new DateTime(2022, 9, 10), "https://ebook.algoritmi.ro", "Algoritmica"));
        biblioteca.AdaugaResursa(new Revista("R003", "Revista de Istorie", "Academia Romana", "Istorie", new DateTime(2023, 9, 1), 20, "Istorie", "2023", "09", "Septembrie"));
        
        var utilizatorStandard = new UtilizatorStandard("1", "Ion Popescu", "ionpopescu", "parola123");
        var utilizatorAvansat = new UtilizatorAvansat("2", "Maria Ionescu", "mariaionescu", "parola456");
        
        biblioteca.AdaugaUtilizator(utilizatorStandard);
        biblioteca.AdaugaUtilizator(utilizatorAvansat);
        
        utilizatorStandard.InscrieLaCurs("Matematica Avansata");
        
        Console.WriteLine("Stoc initial al resurselor:");
        biblioteca.AfiseazaResurseDisponibile();
        
        var utilizatori = biblioteca.GetUtilizatori();
        foreach (var utilizator in utilizatori)
        {
            Console.WriteLine($"\nUtilizatorul activ: {utilizator.Nume} ({utilizator.ID})");
            Console.WriteLine("\nDoriti sa efectuati o cautare? (da/nu)");
            string raspunsCautare = Console.ReadLine();
            if (raspunsCautare.ToLower() == "da")
            {
                Console.WriteLine("\nIntroduceti criteriile pentru cautare:");
                Console.Write("Titlu (lasati gol daca nu doriti sa cautati dupa titlu): ");
                string titlu = Console.ReadLine();
                Console.Write("Autor (lasati gol daca nu doriti sa cautati dupa autor): ");
                string autor = Console.ReadLine();
                Console.Write("Domeniu (lasati gol daca nu doriti sa cautati dupa domeniu): ");
                string domeniu = Console.ReadLine();
                biblioteca.CautaResurse(
                    titlu: string.IsNullOrWhiteSpace(titlu) ? null : titlu,
                    autor: string.IsNullOrWhiteSpace(autor) ? null : autor,
                    domeniu: string.IsNullOrWhiteSpace(domeniu) ? null : domeniu
                );
            }
            else
            {
                Console.WriteLine("Nu s-a efectuat nicio cautare.");
            }
            Console.WriteLine("\nImprumuturi:");
            Console.WriteLine("Introduceti ID-urile resurselor pe care doriti sa le imprumutati (separate prin virgula): ");
            string inputIds = Console.ReadLine();
            string[] idsResurse = inputIds.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                          .Select(id => id.Trim())
                                          .ToArray();
            foreach (var id in idsResurse)
            {
                var resursa = biblioteca.ObtineResursaDupaId(id);
                if (resursa != null)
                {
                    if (resursa is not EBook && resursa.StocDisponibil == 0)
                    {
                        Console.WriteLine($"Resursa cu ID-ul '{id}' nu este momentan în stoc.");
                    }
                    else
                    {
                        bool succes = biblioteca.ImprumutaResursa(utilizator, id);
                    }
                }
                else
                {
                    Console.WriteLine($"Resursa cu ID-ul '{id}' nu exista.");
                }
            }
            Console.WriteLine("\nStocuri disponibile după împrumuturi:");
            biblioteca.AfiseazaResurseDisponibile();
        }
        Console.WriteLine("\nReturnare resurse:");
        foreach (var utilizator in utilizatori)
        {
            Console.WriteLine($"\nUtilizatorul activ: {utilizator.Nume} ({utilizator.ID})");
            Console.WriteLine("Introduceti ID-urile resurselor pe care doriti sa le returnati (separate prin virgula): ");
            string inputReturnari = Console.ReadLine();
            string[] idsReturnari = inputReturnari.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(id => id.Trim())
                .ToArray();
            bool returnariEfectuate = false;
            foreach (var id in idsReturnari)
            {
                var resursa = biblioteca.ObtineResursaDupaId(id);
                if (resursa != null)
                {
                    biblioteca.ReturneazaResursa(utilizator, id);
                    returnariEfectuate = true;
                }
                else
                {
                    Console.WriteLine($"Resursa cu ID-ul '{id}' nu exista.");
                }
            }
            if (!returnariEfectuate)
            {
                Console.WriteLine("Nu au fost returnari efectuate.");
            }
        }
        Console.WriteLine("\nStocuri disponibile după returnări:");
        biblioteca.AfiseazaResurseDisponibile();
   }
}