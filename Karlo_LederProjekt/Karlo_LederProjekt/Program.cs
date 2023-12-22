string pathToFile = "C:\\Users\\User\\Desktop\\KlijentiLamboo.txt";
List<Lamboo> CompanyInformation= LoadInfo(pathToFile);

bool ShouldTheProgramBeRunning = true;
while (ShouldTheProgramBeRunning)
{
    Console.WriteLine("\nInsert an option...\n" +
        "1. Output of all information\n" +
        "2. Input a new Client or an Employee\n" +
        "3. Delete a Client or an Employee\n" +
        "4. Save information\n" +
        "5. End Programme\n");

    int input = Convert.ToInt32(Console.ReadLine());
    switch (input)
    {
        case 1:
            // Ispis svih filmova te povratak na meni
            IspisiFilmove(CompanyInformation);
            break;
        case 2:
            // Nakon što korisnik unese novi film, dodajemo ga u listu svih filmova.
            // Ovdje ponovno koristimo 'single-line-of-code' pristup
            CompanyInformation.Add(InsertElement());
            break;
        case 3:
            // Brisanje filma
            DeleteItem(CompanyInformation);
            break;
        case 4:
            // Spremanje filmova u fajl
            SpremiFilmoveUFajl(pathToFile, CompanyInformation);
            break;
        case 5:
            // Prekid rada programa. Ovdje je zgodno napraviti konačno spremanje svih promjena u fajl. 
            // No, s ovim treba biti pažljiv jer se možda želite igrati sa opcijama, ali ne želite da se sve promjene spreme u fajl.
            // SpremiFilmoveUFajl(putanjaDoFajla, Information);
            ShouldTheProgramBeRunning = false;
            break;
        default:
            Console.WriteLine("Wrong Input");
            break;
    }
}
void DeleteItem(List<Lamboo> CompanyInformation)
{
  
    Console.WriteLine("Insert an Item you wish to delete");
    string imeFilma = Console.ReadLine();

    // Pretraga filma kojeg želimo brisati
    foreach (Lamboo name in CompanyInformation)
    {
        // ukoliko se naslov podudara...
        if (Lamboo.name == imeFilma)
        {
            Console.WriteLine("{0} found. We're deleting the element", name.ToString());

            // Ukloni film iz liste pomoću naredbe Remove()
            Lamboo.Remove(CompanyInformation);

            // Izlazak iz metode. Film je pronađen i nema potrebe ići dalje kroz listu.
            // Također, liste (kao ni nizovi) 'ne trpe' da se kroz njih istovremeno prolazi pomoću for (ili foreach) naredbe,
            // te briše sadržaj. Ovo zbuni listu (niz). da nemamo return; program bi puknuo (probajte zakomentirati liniju return;)
            // Također, vidite zakomentirani kod ispod koji je "de facto" standard kako se briše element iz liste.
            return;
        }
    }

    /*
     // Kao što je prethodno spomenuto, liste ne trpe istovremeno iteriranje i brisanje
     // Stoga radimo pomoćnu varijablu koja će sadržavati film koji treba brisati
    Film filmKojiTrebaBrisati = null;
    foreach (Film film in Information)
    {
        // ukoliko se naslov podudara...
        if (film.naziv == imeFilma)
        {
            Console.WriteLine("{0} pronađen. Brišemo film...", film.ToString());
            // spremi film koji treba brisati, ali nemoj ga stvarno brisati...
            filmKojiTrebaBrisati = film;
        }
    }

    // ukoliko smo našli film koji treba brisati (nije više null), onda ga možemo i obrisati
    if (filmKojiTrebaBrisati != null)
    {
        Information.Remove(filmKojiTrebaBrisati);
    }
    // Ovdje je bitno primjetiti da se u for petlji ne vrši brisanje, pa nemamo greške.
    */
}
void SpremiFilmoveUFajl(string putanjaDoFajla, List<Lamboo> Information)
{
    List<string> linijeZaUpis = new List<string>();
    foreach (Lamboo film in Information)
    {
        // Kreiranje linije u istom formatu koji se koristi za čitanje iz fajla
        string linija = String.Format("{0};{1};{2}", Lamboo.name, Lamboo.function, Lamboo.investment);

        // Nakon kreiranja linije, spremamo istu u lisztu linija za upis
        linijeZaUpis.Add(linija);
    }

    // Zapiši sve linije, jednu po jednu, u fajl. WriteAllLines naredba prebrisuje kompletan trenutni sadržaj fajla.
    // Stoga je dobro napraviti kopiju fajla 'filmovi.txt' prije pokretanja programa.
    File.WriteAllLines(putanjaDoFajla, linijeZaUpis);
}

void IspisiFilmove(List<Lamboo> filmovi)
{
    foreach (Lamboo film in filmovi)
    {
        // film.GetString() vraća string. Console.WriteLine() ispisuje dani string
        Console.WriteLine(film.ToString());
    }
}

Lamboo InsertElement()
{
    Console.WriteLine("Unesite naziv filma...");
    string name = Console.ReadLine();

    Console.WriteLine("Unesite ime i prezime redatelja filma...");
    string function= Console.ReadLine();

    Console.WriteLine("Unesite godinu...");
    string investment = Console.ReadLine();

    return new Lamboo(name, function,investment); // Kreiranje i vraćanje Filma u jednoj liniji koda
}

List<Lamboo> LoadInfo(string putanja)
{
    // filmovi sadrže sve filmove koje učitavamo iz datoteke
    List<Lamboo>  = new List<Lamboo>();

    // Prolazimo kroz linije fajla. Svaka linija će postati jedan novi Film kojeg ćemo spremiti u listu
    foreach (string linija in File.ReadAllLines(putanja))
    {
        // Primjer linije: "ime_filma;ime_redatelja;godina"
        // Kada liniju splitamo po znaku ';', dobivamo niz stringova
        string[] Parts = linija.Split(";");

        // Na mjestu stringovi[0] se nalazi ime filma,
        // stringovi[1] sadrži ime redatelja
        // stringovi[2] sadrži godinu u string formatu -> Pretvaramo je u int
        // Ova linija pokazuje kako možemo, u jednoj liniji, instancirati objekt Film te ga odmah dodati u listu filmova
        Lamboo.Add(new Lamboo(Parts[0], Parts[1], Parts[2]));
    }

    return filmovi;
}


public class Lamboo
{
    public string name;
    public string function;
    public string investment;

    // Konstruktor je 'posebna' metoda koja služi za kreiranje nove instance klase Film.
    // Ovo je "školski" primjer kreiranja novog filma.
    public Lamboo (string Name, string Function, string Investment)
    {
        this.name = Name;
        this.function = Function;
        this.investment = Investment;
    }

    // Ova metoda kreira string koji sadrži "lijepo formatiranu" string reprezentaciju klase Film.
    public string ToString()
    {
        return String.Format("Name {0}, function {1}, investment{2}", name, function, investment);
    }
}