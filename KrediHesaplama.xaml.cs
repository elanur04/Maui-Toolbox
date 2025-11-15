namespace MauiApp1;

public partial class KrediHesaplama : ContentPage
{
    public KrediHesaplama()
    {
        InitializeComponent();
    }
    // Bu metot, Slider her kaydırıldığında çalışır
    private void vadeSlider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        // Slider'ın yeni değerini al, tamsayıya çevir
        int vade = (int)e.NewValue;
        // Bu değeri yandaki 'VadeEntry' (metin kutusuna) yaz
        VadeEntry.Text = vade.ToString();
    }

   
    // Bu metot, Metin Kutusuna yazıp 'Enter'a basınca çalışır
    private void VadeEntry_Completed(object sender, EventArgs e)
    {
        
        if (int.TryParse(VadeEntry.Text, out int vade))
        {
       
            if (vade < 1)
            {
                vade = 1;
                VadeEntry.Text = "1"; 
            }
            if (vade > 120)
            {
                vade = 120;
                VadeEntry.Text = "120"; 
            }
            // Slider'ın topuzunu metin kutusundaki yeni değere getir
            vadeSlider.Value = vade;
        }
        else
        {
            // Eğer kullanıcı geçersiz bir şey yazdıysa (örn: "abc"),
            // slider'daki mevcut değeri metin kutusuna geri yazarak düzelt.
            VadeEntry.Text = vadeSlider.Value.ToString();
        }
    }
    // Bu metot, 'Hesapla' butonuna tıklandığında çalışır
    private void hesaplaButton_Clicked(object sender, EventArgs e)
    {
            if (!double.TryParse(tutarEntry.Text, out double tutar) || tutar <= 0)
        {
            DisplayAlert("Hata", "Lütfen geçerli bir kredi tutarı girin.", "Tamam");
            return;
        }

        if (!double.TryParse(faizEntry.Text, out double aylikFaiz) || aylikFaiz <= 0)
        {
            DisplayAlert("Hata", "Lütfen geçerli bir faiz oranı girin.", "Tamam");
            return;
        }

        if (krediTipiPicker.SelectedItem == null)
        {
            DisplayAlert("Hata", "Lütfen bir kredi türü seçin.", "Tamam");
            return;
        }

        // Vadeyi doğrula. Metin kutusu ve slider zaten senkron.
        // O yüzden VadeEntry'den okumamız yeterli.
        if (!int.TryParse(VadeEntry.Text, out int vade) || vade < 1 || vade > 120)
        {
            DisplayAlert("Hata", "Lütfen geçerli bir vade (1-120 ay) girin.", "Tamam");
            // Hatalıysa slider'dan alıp düzeltelim
            VadeEntry.Text = vadeSlider.Value.ToString();
            vade = (int)vadeSlider.Value;
        }

        double Oran = aylikFaiz; // Kullanıcının girdiği yüzde 
        double Tutar = tutar;
        int Vade = vade;
        string krediTuru = krediTipiPicker.SelectedItem.ToString();

        // BSMV ve KKDF oranları için değişkenler (ondalık olarak)
        double BSMV = 0.0;
        double KKDF = 0.0;


        switch (krediTuru)
        {
            case "İhtiyaç Kredisi":
                KKDF = 0.15; // KKDF %15
                BSMV = 0.10; // BSMV %10
                break;

            case "Taşıt Kredisi":
                KKDF = 0.15; // KKDF %15
                BSMV = 0.05; // BSMV %5
                break;

            case "Konut Kredisi":
                KKDF = 0.0;  
                BSMV = 0.0; 
                break;
            
            case "Ticari Kredi":
                KKDF = 0.0; 
                BSMV = 0.5; 
                break;
        }

        //Brüt Faizi 

        double brutFaiz = ((Oran + (Oran * BSMV) + (Oran * KKDF)) / 100);

        //Taksit Hesaplama
        double taksit;

        if (brutFaiz > 0) 
        {
            // taksit = ((Math.Pow(1 + brutFaiz, Vade) * brutFaiz) / (Math.Pow(1 + brutFaiz, Vade) - 1)) * Tutar;
            double us_hesaplamasi = Math.Pow(1 + brutFaiz, Vade);
            taksit = ((us_hesaplamasi * brutFaiz) / (us_hesaplamasi - 1)) * Tutar;
        }
        else
        {
            // Eğer faiz 0 ise 
            taksit = Tutar / Vade;
        }
        // toplam = taksit * Vade;
        double toplamOdeme = taksit * Vade;
        double toplamFaiz = toplamOdeme - Tutar;

        aylikTaksitLabel.Text = $"{taksit:C}";
        toplamOdemeLabel.Text = $"{toplamOdeme:C}";
        toplamFaizLabel.Text = $"{toplamFaiz:C}";
    }
} 






