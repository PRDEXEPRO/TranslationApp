# 🌍 Çeviri Web Uygulaması

Modern, HTML tabanlı çeviri uygulaması. Türkçe ve popüler diller (İngilizce, Almanca, Fransızca, İtalyanca, İspanyolca) arasında çeviri yapmanızı sağlar.

## 🚀 Hızlı Başlangıç

### Uygulamayı Çalıştırma

**API anahtarı gerektirmez!** Uygulama MyMemory Translation API kullanmaktadır ve tamamen ücretsizdir.

```bash
cd TranslateWebApp
dotnet run
```

Uygulama çalıştıktan sonra tarayıcınızda şu adresi açın:
- **HTTP**: http://localhost:5000
- **HTTPS**: https://localhost:5001

## ✨ Özellikler

- ✅ Türkçe ↔ İngilizce, Almanca, Fransızca, İtalyanca, İspanyolca çeviri
- ✅ Modern ve kullanıcı dostu HTML arayüzü
- ✅ Dilleri değiştirme butonu (ters çeviri)
- ✅ Gerçek zamanlı çeviri
- ✅ Responsive tasarım (mobil uyumlu)
- ✅ Hata yönetimi ve durum mesajları

## 🎯 Kullanım

1. **Kaynak dil** seçin (varsayılan: Türkçe)
2. **Kaynak metin** alanına çevrilecek metni yazın
3. **Hedef dil** seçin (varsayılan: İngilizce)
4. **"Çevir"** butonuna tıklayın
5. Çeviri sonucu **"Çeviri Sonucu"** alanında görünecektir

### Klavye Kısayolları

- **Ctrl + Enter**: Çeviri yap (kaynak metin alanındayken)

## 🛠️ Teknolojiler

- **ASP.NET Core 9.0** - Web framework
- **Razor Pages** - Sayfa yapısı
- **Bootstrap 5** - UI framework
- **Microsoft Translator API** - Çeviri servisi
- **Newtonsoft.Json** - JSON işleme

## 📁 Proje Yapısı

```
TranslateWebApp/
├── Controllers/
│   └── TranslationController.cs    # API endpoint
├── Services/
│   └── TranslationService.cs        # Çeviri servisi
├── Pages/
│   └── Index.cshtml                 # Ana sayfa
├── appsettings.json                 # API anahtarı ayarları
└── Program.cs                       # Uygulama yapılandırması
```

## 🔧 Geliştirme

### Bağımlılıkları Yükleme

```bash
dotnet restore
```

### Projeyi Derleme

```bash
dotnet build
```

### Uygulamayı Çalıştırma

```bash
dotnet run
```

## 📝 Notlar

- ✅ **API anahtarı gerektirmez** - MyMemory Translation API kullanılıyor
- ✅ **Tamamen ücretsiz** - Günlük 10,000 kelime limiti
- ✅ **Anında kullanıma hazır** - Hiçbir yapılandırma gerekmez

## 🐛 Sorun Giderme

### "API anahtarı bulunamadı" hatası
- `appsettings.json` dosyasında API anahtarınızın doğru girildiğinden emin olun
- JSON formatının doğru olduğunu kontrol edin

### Çeviri çalışmıyor
- İnternet bağlantınızı kontrol edin
- API anahtarınızın geçerli olduğundan emin olun
- Tarayıcı konsolunda hata mesajlarını kontrol edin

## 📄 Lisans

Bu proje eğitim amaçlıdır.

