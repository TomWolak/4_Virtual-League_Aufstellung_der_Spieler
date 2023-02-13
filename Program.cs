using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _4_Virtual_League_Aufstellung_der_Spieler
{
    public enum PlayerRolle
    {
        Torwart,
        Abwehrspieler,
        Mittelfeldspieler,
        Stuermer
    }

    public enum Nation
    {
        Belaruss,
        Deutschland,
        Polen,
        Slovakei,
        Tschechien,
        Ukraine
    }

    public static class RandomProvider   // eine Klasse, die Objekte vom Typ Random bereitstellt
    {
        public static Random Random => random;                //  property
        private static readonly Random random = new Random();  //  Field, privat, unveränderbar
    }

    public class Player
    {
        public string Name   // property, readonly
        {
            get
            {
                return name;
            }
        }

        public int SpielerStaerke => spielerStaerke;

        public PlayerRolle Rolle
        {
            get => rolle;
            set => rolle = value;
        }

        public DateTime Geburtsdatum => geburtsdatum;    //  property

        public Player(string name)   // Basiskonstruktor, enthält gemeinsame Funktionen Player verschiedener Typen
        {
            this.name = name;
            Random random = RandomProvider.Random;
            int tag = random.Next() % 28 + 1;                // Bereich von Monatstage
            int monat = random.Next() % 12 + 1;              // Bereich von Monaten
            int jahr = random.Next() % 21 + 1980;            // Jahr-Bereich; der älteste Spieler könnte 1980 geboren sein
            geburtsdatum = new DateTime(jahr, monat, tag);   // Field
            spielerStaerke = random.Next() % 9 + 1;          // Random-Spielerstaerke im Bereich 1-9
        }

        public virtual string getFunction()            // Methoda, diese Methode gibt Art des Spielers zurück
        {
            throw new NotImplementedException(nameof(getFunction));    // Ausnahme
        }

        public int PlayerAlter()      // Methode
        {
            int alter = DateTime.Now.Year - geburtsdatum.Year;
            return alter;
        }

        private string name;
        private PlayerRolle rolle;       // Fields
        private DateTime geburtsdatum;
        private int spielerStaerke;
    }

    public class Torwart : Player
    {
        public Torwart(string name) : base(name)
        {
            Rolle = PlayerRolle.Torwart;
        }

        public override string getFunction()     // diese Methode überschreibt die getFunction-Methode der Player-Klasse,
        {                                   // weil es vom gleichen Typ ist und die gleichen Argumente verwendet
            return Rolle.ToString();    
        }                                    
    }

    public class Abwehrspieler : Player
    {
        public Abwehrspieler(string name) : base(name)
        {
            Rolle = PlayerRolle.Abwehrspieler;
        }

        public override string getFunction()     
        {                                   
            return Rolle.ToString();
        }
    }

    public class Mittelfeldspieler : Player
    {
        public Mittelfeldspieler(string name) : base(name)
        {
            Rolle = PlayerRolle.Mittelfeldspieler;
        }

        public override string getFunction()     
        {                                   
            return Rolle.ToString();
        }
    }

    public class Stuermer : Player
    {
        public Stuermer(string name) : base(name)
        {
            Rolle = PlayerRolle.Stuermer;
        }

        public override string getFunction()     
        {                                   
            return Rolle.ToString();
        }
    }

    public class Team
    {
        public Nation Nation => nation;   // property, readonly

        public static Team Create(Nation nation)   // Die statische Methode wird verwendet, um den Konstruktor 'private Team(Nation ...') aufzurufen
        {
            Team resultat = new Team(nation);
            return resultat;
        }

        private Team(Nation nation)    // Konstruktor
        {
            this.nation = nation;
            players = new List<Player>();

            players.Add(new Torwart("Torwart"));

            players.Add(new Abwehrspieler("Abwehspieler1"));
            players.Add(new Abwehrspieler("Abwehspieler2"));
            players.Add(new Abwehrspieler("Abwehspieler3"));
            players.Add(new Abwehrspieler("Abwehspieler4"));

            players.Add(new Mittelfeldspieler("Mittelfeldspieler1"));
            players.Add(new Mittelfeldspieler("Mittelfeldspieler1"));
            players.Add(new Mittelfeldspieler("Mittelfeldspieler1"));
            players.Add(new Mittelfeldspieler("Mittelfeldspieler1"));

            players.Add(new Stuermer("Stuermer1"));
            players.Add(new Stuermer("Stuermer2"));
        }

        public int getPower()
        {
            int summeDerSpielerStaerke = 0;
            for (int i = 0; i < players.Count; i++)
            {
                summeDerSpielerStaerke += players[i].SpielerStaerke;
            }
            return summeDerSpielerStaerke;
        }

        public string EntnehmenAlsAufschrift()
        {
            string resultat = "Unten sind die Players von Nation: " + nation + ". Gesamtstaerke der Mannshaft: " + getPower().ToString() + "\n";
            for (int i = 0; i < players.Count; i++)
            {
                resultat += ("Spielername: " + players[i].Name + ", " + "Spielerart: " + players[i].Rolle + ", " + "Spieler-alter: " + players[i].PlayerAlter() + ", " + "Spieler-Staerke: " + players[i].SpielerStaerke + " " + "\n");
            }
            return resultat;
        }

        private Nation nation;         // Field >>> enum
        private List<Player> players;
    }

        internal class Program
    {
        static void Main(string[] args)
        {
            Team Team_Deutschland = Team.Create(Nation.Deutschland);    // Erstellen einer neuen Instanz der Team-Klasse, hinzufügen des Enum
            Console.WriteLine(Team_Deutschland.EntnehmenAlsAufschrift());

            Team Team_Polen = Team.Create(Nation.Polen);
            Console.WriteLine(Team_Polen.EntnehmenAlsAufschrift());

            Team Team_Slovakei = Team.Create(Nation.Slovakei);
            Console.WriteLine(Team_Slovakei.EntnehmenAlsAufschrift());

            Console.ReadLine();
        }
    }
}

/*
Folgende Fragen sind zu beantworten:
Was wird zurück gegeben, bei
a) Spieler spieler1 = new Spieler();
spieler1.getFunction(); ?
Antwort: throw new NotImplementedException(nameof(getFunction)); 


b) Torwart torwart = new Torwart();
torwart.getFunction(); ?
Antwort: string = "Torwart"


c) Spieler spieler2 = new Mittelfeldspieler();
spieler2.getFunction(); ?
Antwort: string = "Mittelfeldspieler"

*/
