# [1.Gün] [08/06/2024] Proje Hazırlığı
- Case study dosyasını inceledim.
- Proje gereksinimlerini tespit ettim.
- Proje için GDD dökümanı hazırladım.
- Github reposu oluşturdum.
- Photon'un son güncelleme notlarına göz attım.
- Proje için mimari yapıyı belirledim.
- Yapılacaklar listesi oluşturdum.

# [2.Gün] [09/06/2024] Projenin oluşturulması
- [Architecture Diagram](https://github.com/gkhanC/flameborn-game/blob/dev/images/Architecture%20Diagram.png) oluşturuldu ve repo'ya eklendi.
- Unity Editor'ün 2022.3.10f1 versiyonunu yükledim.
- PlayFab dökümantasyonunu inceledim.
- flameborn-unity projesini oluşturdum. Proje **src** klasörü altına yerleştirildi.
- [Error] PlayFab Login hatası: PlayFab SDK'sini yapılandırırken, Editor Extension Resources klasörü altında bulunan PlayFabEditorPrefsSo.asset objesi statik tanımı üzerinden bulunamadı. Bu sebeple yeni bir instance yaratmaya çalışırken hata oluştu. İlgili kod bloğuna elle hata yönetimi eklenerek sorun aşıldı.
- API bilgilerinin gizliliğini sağlamak için, ***.\src\flameborn-unity\Assets\PlayFabEditorExtensions\Editor\Resources\PlayFabEditorPrefsSO.asset*** ve ***.\src\flameborn-unity\Assets\PlayFabSDK\Shared\Public\Resources\PlayFabSharedSettings.asset*** dosyalarındaki değişiklikler git tarafından izlenmeyecek şekilde ayarlandı.
- [HypeFire](https://github.com/gkhanC/flameborn-game/blob/dev/packages/HypeFire-v0f1-OLD.unitypackage) eklendi.
- Geliştirme loglarımı file'a kaydetmemi sağlayan Logger ekliyorum.
- [Geliştirme Logları](https://github.com/gkhanC/flameborn-game/blob/dev/logs/Logs.md)
- PlayFab için login ve User bilgilerini barındıran json formatında configuration file oluşturdum. Login ve register için gerekli bilgileri barındıracak.
- Login sistemi tamamlandı, PlayFab üzerinde API test edildi. 
- PlayFab üzerinde user register işlemleri için kullanıcının, ilk oyununu bitirmesini veya 2.kez oyunu başlatmasını bekleyecek şekilde yapılandıracağım.

# [3.Gün] [10/06/2024] PlayFab User Register
