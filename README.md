# MAUI Toolbox

Vücut kitle indeksi ve kredi hesaplama ekranları ile renk seçici. C# / XAML ve .NET 8 MAUI ile kullanıcı etkileşimi ve olay yönetimini öğrenmek için hazırlanmıştır.

## Kaynak kodu

Sayfa tasarımları `.xaml`, ekran davranışları `.xaml.cs` dosyalarındadır. `MauiApp1.csproj` Android, iOS, Mac Catalyst ve Windows hedeflerini tanımlar.

## Mevcut durum

Depoda `Platforms/` ve `Resources/` klasörleri bulunmuyor; proje dosyası bu kaynaklara başvuruyor. Bu nedenle mevcut haliyle temiz klondan eksiksiz uygulama derlemesi garanti edilmez. Derleme için orijinal platform/varlık dosyaları geri eklenmeli veya aynı ad alanıyla oluşturulan .NET 8 MAUI projesine ekranlar aktarılmalıdır.

Bu depo ekran ve hesaplama mantığını incelemek için kullanılabilir. Otomatik testler ve platform derleme doğrulaması henüz eklenmemiştir.
