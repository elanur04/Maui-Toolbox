namespace MauiApp1;

public partial class RenkSecici : ContentPage
{
    private Random random = new Random();
    public RenkSecici()
	{
		InitializeComponent();
        GuncelRengiAyarla();
    }

    // Herhangi bir slider'ýn deðeri deðiþtiðinde çalýþr
    private void Renk_Slider_ValueChanged(object sender, ValueChangedEventArgs e) //sender hangi slider olduðunu belirtir(ör: redSlider) valuechangedeventargs ise yeni deðeri belirtir
    {
        GuncelRengiAyarla();
    }

    // 'Rastgele Renk' butonu
    private void RastgeleButton_Clicked(object sender, EventArgs e)
    {
        redSlider.Value = random.Next(0, 256);
        greenSlider.Value = random.Next(0, 256);
        blueSlider.Value = random.Next(0, 256);
    }
    // 'Kopyala' butonu
    //async = eþzamansýz c# ta metodun içinde await kullanýlacaksa metot async olarak iþaretlenir, await olan metotlar bitene kadar bekler
    private async void KopyalaButton_Clicked(object sender, EventArgs e) //burda sender kopyala butonunu belirtir 
    {
        string renk_kodu = hexLabel.Text; // hexLabel ýn Text özelliðinden hex kodunu al 
        await Clipboard.SetTextAsync(renk_kodu);
        // bir 'popup' (açýlýr pencere) göster
        await DisplayAlert("Kopyalandý", $"{renk_kodu}", "OK");
    }
    private void GuncelRengiAyarla()
    {
        if (redSlider == null || greenSlider == null || blueSlider == null)
            return;

        // Slider deðerlerini al
        int red = (int)redSlider.Value;
        int green = (int)greenSlider.Value;
        int blue = (int)blueSlider.Value;

        //sliderlarýn yanýndaki etiketleri güncelle
        
        redLabel.Text = red.ToString();
        greenLabel.Text = green.ToString();
        blueLabel.Text = blue.ToString();

        Color guncelRenk = Color.FromRgb(red, green, blue);
        renkKutusu.BackgroundColor = guncelRenk;

        // Hex kodunu etikete yaz
        hexLabel.Text = $"#{red:X2}{green:X2}{blue:X2}";
    }
}

