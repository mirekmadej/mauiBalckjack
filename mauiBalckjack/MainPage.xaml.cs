namespace mauiBalckjack
{
    class Karta
    {//"A;S;11"
        public string kolor { get; private set; }
        public string figura { get; private set; }
        public int wartosc { get; private set; }
        public string grafika { get; private set; }
        public Karta(string opis)
        {
            string[] pola = opis.Split(';');
            kolor = pola[1];
            figura = pola[0];
            wartosc = int.Parse(pola[2]);
            //s9.png
            grafika = $"{kolor.ToLower()}{figura.ToLower()}.png";
        }
        //public Karta(string kolor, string figura, int wartosc, string grafika)  { }
    }

    public partial class MainPage : ContentPage
    {
        private List<Karta> talia = new List<Karta>();
        private List<Karta> taliaOryg = new List<Karta>();
        private int punktyGracza;
        Random r = new Random();

        public MainPage()
        {
            InitializeComponent();
            czytajDane();

        }
        private void czytajDane()
        {
            string[] dane;
            string plik = "deck.txt";
            dane = File.ReadAllLines(plik);
            foreach (string linia in dane)
                taliaOryg.Add(new Karta(linia));
        }

        private void BtnNowaGraKlik(object sender, EventArgs e)
        {
            slGra.IsVisible = true;
            slStart.IsVisible = false;
            talia.Clear();
            //talia = taliaOryg.Slice(0, taliaOryg.Count);
            foreach (var k in taliaOryg)
                talia.Add(k);
            punktyGracza = 0;
            hslGracz.Children.Clear();

            dajKarte();
            dajKarte();
        }

        private void dajKarte()
        {
            int poz;
            var i1 = new Image();
            poz = r.Next(talia.Count);
            punktyGracza += talia[poz].wartosc;
            i1.Source = talia[poz].grafika;
            i1.WidthRequest = 100;
            talia.RemoveAt(poz);
            hslGracz.Children.Add(i1);
            lblPunkty.Text = $"Punkty gracza: {punktyGracza}";
        }

        private void BtnDajKarteKlik(object sender, EventArgs e)
        {
            dajKarte();
        }


    }
}
