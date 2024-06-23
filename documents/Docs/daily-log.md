# [1. Gün] [08/06/2024] Proje Hazırlığı
- Case study dosyasını inceledim.
- Proje gereksinimlerini tespit ettim.
- Proje için GDD dökümanı hazırladım.
- GitHub reposu oluşturdum.
- Photon'un son güncelleme notlarına göz attım.
- Proje için mimari yapıyı belirledim.
- Yapılacaklar listesi oluşturdum.

# [2. Gün] [09/06/2024] Projenin Oluşturulması
- Unity Editor'ün 2022.3.10f1 versiyonunu yükledim.
- PlayFab dökümantasyonunu inceledim.
- flameborn-unity projesini oluşturdum. Proje **src** klasörü altına yerleştirildi.
- [Error] PlayFab Login hatası: PlayFab SDK'sini yapılandırırken, Editor Extension Resources klasörü altında bulunan PlayFabEditorPrefsSo.asset objesi statik tanımı üzerinden bulunamadı. Bu sebeple yeni bir instance yaratmaya çalışırken hata oluştu. İlgili kod bloğuna elle hata yönetimi eklenerek sorun aşıldı.
- Geliştirme loglarımı file'a kaydetmemi sağlayan Logger ekliyorum.
- PlayFab için login ve User bilgilerini barındıran json formatında configuration file oluşturdum. Login ve register için gerekli bilgileri barındıracak.
- Login sistemi tamamlandı, PlayFab üzerinde API test edildi.
- PlayFab üzerinde user register işlemleri için kullanıcının, ilk oyununu bitirmesini veya 2. kez oyunu başlatmasını bekleyecek şekilde yapılandıracağım.

# [3. Gün] [10/06/2024] PlayFab User Register
- Azure portal üzerinde tablo oluşturdum.
- Device temelli deneysel bir register sistemi kurguladım tabloyu buna uygun olarak şekillendirdim. 
  Tablo şunları içerir:
  - DeviceId
  - EMail
  - LaunchCount
  - Password
  - Rating
  - UserName
- Register olan kullanıcılar bu bilgilerle Azure'da kayıt edilecek.
- Tablodaki veriler üzerinde CRUD işlemleri yapabilmem için Azure Functionları oluşturdum.
- Functionları PowerShell kullanarak test ettim.
- Azure API oluşturmak için Unity'de logic'i şekillendirdim.
- Azure functionların bağlantı adresini tutan Azure Configuration dosyasını json formatında oluşturdum. Dosya değişiklikleri git tarafından izlenmeyecek şekilde ayarlandı.

# [4. Gün] [11/06/2024] Azure Konfigurasyonlarını Tamamla
- Refactoring yaptım.
- Azure tabloları ile yaptığım CRUD işlemleri için test senaryonları uyguladım.
- Login işlemlerini test ettim.
- Login, Register, Recovery için logic hazırladım ve UI testleri yaptım.

# [5. Gün] [12/06/2024]
- Azure functionları güncellendi.
- Mevcut kod yeni fonksiyonlar için uygun hale getirildi.
- Azure tabloları ile ilgili işlemler tamamlandı.
- Login, register ve recovery işlemleri tamamlandı.
- PlayFab için email ile giriş eklendi.

# [6. Gün] [12/06/2024]
- Login işlemleri tamamlandı.
- Register işlemleri oluşturuldu.
- Password reset yöntemi oluşturuldu.

# [7. Gün] [14/06/2024]
- Case study belgesini incelediğimde API yapısını yanlış ele aldığımı fark ettim. Yeni yapı için mimari tasarlandı.
- Temel işlevleri ele alan fonksiyonlar güncellendi.

# [8. Gün] [15/06/2024]
- API, Cloud scriptler ve app functionlar ile çalışacak şekilde yapılandırıldı.
- API bağımlılıklarını ortadan kaldırmak için soyutlama yapısı tasarlandı.
- Temel API fonksiyonları test edildi.
- Azure cloud scriptsler güncellendi.

# [9. Gün] [16/06/2024]
- Matchmaking yapısı oluşturuldu.
- Azure tabloları ile çalışma işlevleri tamamlandı.
- Photon'a giriş yapıldı.

# [10. Gün] [19/06/2024]
- Core mekanikler oluşturuldu.
- Photon senkronizasyonu tamamlandı.

# [11. Gün] [20/06/2024]
- Oyun varlıkları tamamlandı.
- UI tamamlandı.

# [12. Gün] [21/06/2024]
- Test, Test, Test.
- Fix, Fix, Fix. :D :D
