#Development Report
Bu belge flameborn multiplayer game projesine ilişkin rapordur.

Projenin göstermek istediği skill set;

    * Multiplayer game api'ları ile çalışma
    * Azure Function ve cloud scripyler ile çalışma
    * Playfab ile çalışma
    * Programlama skilleri
    * Programlama mimarisi tasarlaya bilme yeteneği
    * Unity ve oyun geliştirme ve oyun mekanikleri üzerinde ki yetkinlik
    * Veri setleri ve tablolar ile çalışmadaki yetkinlik
    * Kodalam becerileri
    * Kod dökümantae edebilme yeteneği

flameborn şunları içerir;

    Oyuncuların  api'lar yardımı ile login, register, password recovery ve Azure tabloları üzerinde temel crud işlemleri
    Api yönetimi
    Matchmaking
    Event yönetimi
    Player yönetimi
    Oyun yönetimi
    Oyun içi temel işlevler

Nasıl Çalışır?

    Uygulama Loader isimli sahne ile başlar bu sahnenin temel görevi SDK katmanından enjecte edelen [IApiRequest][Link] arayüzü aracığı ile Api ile iletişim kurarak, kullanıcının account bilgilerini ve player verileni otantikate etmek ve kullanım için hazır hale getirmektir.
        IApiRequest türü [IApiController][Link] türü enjecte edilerek kullanılır. Böylelikle üst katmanda bir soyutlama sağlıyarak farklı türde Api'larla çalışa bilmeyi, Çok biçimliliği destekler

    Loader scene'de veri işlemi bitince UI için bu verilen set edilme süreci başlar, Sürec tamalandığında MainMenu scene'i yüklenir.
    MainMenu scene oyuncunun, Login, register ve yeni match başlatmak gibi işlevlerini gerçkleştire bileceği ana oyun sahnesidir.
        Eğer oyuncu daha önceden giriş yaptı ise Uygulama bir sonraki girişinde otomatik olarak oyuncunun account'unu yükler

    Oyuncu yeni bir match başlatmak istediğinde, Api üzerinden bir ticket oluşturulur, ticket ile oyuncular Rating bilgileri baz alınarak eşleştirilir
    Eşleşme başarılı olduğunda match oluşturlur ve match id oyunculara dağıtılır. Oyuncular match id kullanarak network api'ı üzerinden bir room oluşturur.
    Newtwork Api üzerinden oyuncuların maçın başlaması için hazır olup olmadığı ve nick name gibi bilgileri dağıtılır.
    Network api üzerinde bilgiler işlendiğinde oyun sahnesi yüklenir

    Oyun sahnesinde her oyuncu bir campfire ve işciye sahiptir
    Oyuncular Rating miktarları kadar başlangıç kaynağına sahiptir
    Oyuncular campfireda ranting puanlarından harcayarak yeni işciler spawn edebilir.
    Oyuncular sahip oldukları işçileri seçtikten sonra ekranda boş bir noktaya tıklayarak işcileri hareket ettire bilir
    Oyuncular parmaklarını ekranda sürükleyerek camera'yı hareket ettire bilir
    Oyuncular sahip oldukları işcilere kaynak toplatarak, kaynaklarını ve Rating miktarını arttıra bilir.


Eklenmesi Planlana Özellikler

    Oyuncuların diğer bir oyuncunun işcilerine kendi işcileri ile saldırabilmesi
    Oyuncuların diğer oyuncunun campfire'ına kendi işcileri ile saldırması
    Maç için zaman sınırı
    Farklı türde karakterler (Asker, Rahip vs)
    Win Lose eventlerinin eklenmesi