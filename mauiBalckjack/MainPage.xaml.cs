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
        private int punktyKomputera;
        Random r = new Random();
        private int wygraneGracza = 0;
        private int wygraneKomputera = 0; 

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
            lblPunktyKomputera.Text = "";
            slGra.IsVisible = true;
            slStart.IsVisible = false;
            talia.Clear();
            //talia = new List<Karta>(taliaOryg);
            foreach (var k in taliaOryg)
                talia.Add(k);
            punktyGracza = 0;
            punktyKomputera = 0;
            hslGracz.Children.Clear();
            hslKomputer.Children.Clear();

            dajKarte();
            dajKarte();

            

            if(punktyGracza == 21 || punktyGracza == 22)
            {
                lblPunkty.Text = $"Oczko! wygrał gracz!";
                wygraneGracza++;
                koniecGry();
            }

        }
        private void koniecGry()
        {
            slGra.IsVisible = false;
            slStart.IsVisible = true;
            lblStatystyka.Text = $"Wygrane gracza: {wygraneGracza}, wygrane komputera: {wygraneKomputera}";
        }

        private void dajKarte(bool czyGracz = true)
        {

            int poz;
            var i1 = new Image();
            poz = r.Next(talia.Count);
            i1.Source = talia[poz].grafika;
            i1.WidthRequest = 100;
            if(czyGracz)
            {
                hslGracz.Children.Add(i1);
                punktyGracza += talia[poz].wartosc;
                lblPunkty.Text = $"Punkty gracza: {punktyGracza}";
            }
            else
            {
                hslKomputer.Children.Add(i1);
                punktyKomputera += talia[poz].wartosc;
                lblPunktyKomputera.Text = $"Punkty komputera: {punktyKomputera}";
            }
            talia.RemoveAt(poz);
        }

        private void BtnDajKarteKlik(object sender, EventArgs e)
        {
            dajKarte();
            if(punktyGracza == 21)
            {
                lblPunkty.Text = $"Oczko! wygrał gracz!";
                wygraneGracza++;
                koniecGry();
            }
            if(punktyGracza > 21)
            {
                lblPunkty.Text = $"za dużo! gracz przegrał";
                wygraneKomputera++;
                koniecGry();
            }
        }
        private void BtnPassKlik(object sender, EventArgs e)
        {
            graKomputer();
        }
        private void graKomputer()
        {
            dajKarte(false);
            dajKarte(false);
            if (punktyKomputera == 21 || punktyKomputera == 22)
            {
                lblPunktyKomputera.Text = $"Oczko! wygrał komputer!";
                wygraneKomputera++;
                koniecGry();
                return;
            }
            while(punktyKomputera < 16)
            {
                dajKarte(false);
                if (punktyKomputera == 21)
                {
                    lblPunktyKomputera.Text = $"Oczko! wygrał komputer!";
                    wygraneKomputera++;
                    koniecGry();
                    return;
                }
                if (punktyKomputera > 21)
                {
                    lblPunktyKomputera.Text = $"komputer przekroczył! wygrał gracz!";
                    wygraneGracza++;
                    koniecGry();
                    return;
                }
            }

            if(punktyKomputera == punktyGracza)
            {
                lblPunktyKomputera.Text = $"remis";
                koniecGry() ;
            }
            if(punktyGracza < punktyKomputera)
            {
                lblPunktyKomputera.Text = $"komputer wygrał, pinkty: {punktyKomputera}";
                wygraneKomputera++;
                koniecGry();
            }
            if(punktyGracza > punktyKomputera)
            {
                lblPunktyKomputera.Text = $"gracz wygrał, punkty: {punktyKomputera}";
                wygraneGracza++;
                koniecGry();
            }
        }

    }
}
