namespace MauiApp1;

public partial class VkiHesaplama : ContentPage
{
    public VkiHesaplama()   //constructor
    {
        InitializeComponent();  //XAML'deki kontrolleri yükle
        Hesapla();      //Başlangıçta bir kez hesapla
    }
    //kullanıcı slider'ı her kaydırdığında bu metot çalışır
    private void Vki_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        Hesapla();
    }
    private void Hesapla()
    {
        // Kontroller yüklenmediyse çık (Hata koruması)
        if (kiloSlider == null || boySlider == null) return;

        //Değerleri Al
        double kilo = kiloSlider.Value;
        double boy = boySlider.Value;

        //Etiketleri Güncelle (Slider yanındaki sayılar)
        // "F1" = virgülden sonra 1 basamak gösterir (örn: 58.5)
        kiloLabel.Text = kilo.ToString("F1");
        boyLabel.Text = boy.ToString("F1");

        // VKİ Hesapla
        double boyMetre = boy / 100.0;
        double vki = 0;
        if (boyMetre > 0)  // Sıfıra bölme hatasına karşı
        {
            vki = kilo / (boyMetre * boyMetre);
        }

        //Sonucu Ekrana Yaz
        sonucLabel.Text = vki.ToString("F2");

        //Durumu Belirle
        string durum = "";
        if (vki < 16) durum = "İleri Düzeyde Zayıf";
        else if (vki < 17) durum = "Orta Düzeyde Zayıf";
        else if (vki < 18.5) durum = "Hafif Düzeyde Zayıf";
        else if (vki < 25) durum = "Normal";
        else if (vki < 30) durum = "Hafif Şişman / Fazla Kilolu";
        else if (vki < 35) durum = "1. Derecede Obez";
        else if (vki < 40) durum = "2. Derecede Obez";
        else durum = "3. Derecede Obez / Morbid Obez";

        durumLabel.Text = durum;
    }
}