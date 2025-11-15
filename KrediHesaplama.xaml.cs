namespace MauiApp1;

public partial class KrediHesaplama : ContentPage
{
    public KrediHesaplama()
    {
        InitializeComponent();
    }
    // Bu metot, Slider her kaydýrýldýðýnda çalýþýr
    private void vadeSlider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        // Slider'ýn yeni deðerini al, tamsayýya çevir
        int vade = (int)e.NewValue;
        // Bu deðeri yandaki 'VadeEntry' (metin kutusuna) yaz
        VadeEntry.Text = vade.ToString();
    }

   
    // Bu metot, Metin Kutusuna yazýp 'Enter'a basýnca çalýþýr
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
            // Slider'ýn topuzunu metin kutusundaki yeni deðere getir
            vadeSlider.Value = vade;
        }
        else
        {
            // Eðer kullanýcý geçersiz bir þey yazdýysa (örn: "abc"),
            // slider'daki mevcut deðeri metin kutusuna geri yazarak düzelt.
            VadeEntry.Text = vadeSlider.Value.ToString();
        }
    }
    // Bu metot, 'Hesapla' butonuna týklandýðýnda çalýþýr
    private void hesaplaButton_Clicked(object sender, EventArgs e)
    {
            if (!double.TryParse(tutarEntry.Text, out double tutar) || tutar <= 0)
        {
            DisplayAlert("Hata", "Lütfen geçerli bir kredi tutarý girin.", "Tamam");
            return;
        }

        if (!double.TryParse(faizEntry.Text, out double aylikFaiz) || aylikFaiz <= 0)
        {
            DisplayAlert("Hata", "Lütfen geçerli bir faiz oraný girin.", "Tamam");
            return;
        }

        if (krediTipiPicker.SelectedItem == null)
        {
            DisplayAlert("Hata", "Lütfen bir kredi türü seçin.", "Tamam");
            return;
        }

        // Vadeyi doðrula. Metin kutusu ve slider zaten senkron.
        // O yüzden VadeEntry'den okumamýz yeterli.
        if (!int.TryParse(VadeEntry.Text, out int vade) || vade < 1 || vade > 120)
        {
            DisplayAlert("Hata", "Lütfen geçerli bir vade (1-120 ay) girin.", "Tamam");
            // Hatalýysa slider'dan alýp düzeltelim
            VadeEntry.Text = vadeSlider.Value.ToString();
            vade = (int)vadeSlider.Value;
        }

        double Oran = aylikFaiz; // Kullanýcýnýn girdiði yüzde 
        double Tutar = tutar;
        int Vade = vade;
        string krediTuru = krediTipiPicker.SelectedItem.ToString();

        // BSMV ve KKDF oranlarý için deðiþkenler (ondalýk olarak)
        double BSMV = 0.0;
        double KKDF = 0.0;


        switch (krediTuru)
        {
            case "Ýhtiyaç Kredisi":
                KKDF = 0.15; // KKDF %15
                BSMV = 0.10; // BSMV %10
                break;

            case "Taþýt Kredisi":
                KKDF = 0.15; // KKDF %15
                BSMV = 0.05; // BSMV %5
                break;

            case "Konut Kredisi":
                KKDF = 0.0;  // Muaf
                BSMV = 0.0;  // Muaf
                break;
            
            case "Ticari Kredi":
                KKDF = 0.0; 
                BSMV = 0.5; 
                break;
        }

        // 3. Brüt Faizi 

        double brutFaiz = ((Oran + (Oran * BSMV) + (Oran * KKDF)) / 100);

        // 4. Taksit Hesaplama
        double taksit;

        if (brutFaiz > 0) 
        {
            // taksit = ((Math.Pow(1 + brutFaiz, Vade) * brutFaiz) / (Math.Pow(1 + brutFaiz, Vade) - 1)) * Tutar;
            double us_hesaplamasi = Math.Pow(1 + brutFaiz, Vade);
            taksit = ((us_hesaplamasi * brutFaiz) / (us_hesaplamasi - 1)) * Tutar;
        }
        else
        {
            // Eðer faiz 0 ise (örn: Konut Kredisi ve faiz 0 girildi)
            taksit = Tutar / Vade;
        }

        // 5. Kalan Hesaplamalar
        // toplam = taksit * Vade;
        double toplamOdeme = taksit * Vade;
        double toplamFaiz = toplamOdeme - Tutar;


        // --- 3. Sonuçlarý Ekrana Yazdýrma ---
        aylikTaksitLabel.Text = $"{taksit:C}";
        toplamOdemeLabel.Text = $"{toplamOdeme:C}";
        toplamFaizLabel.Text = $"{toplamFaiz:C}";
    }
} 





