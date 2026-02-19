# 🏠 Emlak Sitesi — Gayrimenkul İlan ve Yönetim Platformu

Emlak Sitesi, **ASP.NET MVC 5** ile geliştirilmiş, gayrimenkul ilanlarını listeleme, detay görüntüleme ve yönetim paneli aracılığıyla içerik yönetimi sağlayan bir web uygulamasıdır. Ziyaretçiler ilanları inceleyebilir, emlak danışmanlarını görebilir ve iletişim formu üzerinden mesaj gönderebilir. Yönetici paneli sayesinde ilanlar, danışmanlar, sayfalar ve gelen mesajlar kolayca yönetilir.

---

## ✨ Özellikler

### Kullanıcı (Ziyaretçi) Tarafı
- **Ana Sayfa**: Slider (öne çıkan ilanlar), ajans/danışman bilgileri ve dinamik içerik
- **İlan Listeleme**: Tüm gayrimenkul ilanlarını sayfalama (pagination) ile görüntüleme
- **İlan Detay**: Seçilen ilanın detay bilgileri, çoklu görselleri ve sorumlu danışman bilgisi
- **Hakkımızda**: Şirket bilgileri ve açıklamalar
- **İletişim**: Konum, hizmet saatleri, telefon, e-posta bilgileri ve mesaj gönderme formu

### Yönetim (Admin) Paneli
- **Giriş/Çıkış**: Session tabanlı admin kimlik doğrulama
- **İlan (Slider) Yönetimi**: İlan ekleme, düzenleme, silme — başlık, şehir, ilçe, adres, fiyat, yatak/banyo sayısı, durum ve görsel yükleme
- **Detay Yönetimi**: İlanlara detay bilgisi ve çoklu görsel (3 adet) ekleme/düzenleme/silme
- **Danışman (Ajans) Yönetimi**: Danışman ekleme, düzenleme, silme — ad-soyad, görev, açıklama, sosyal medya bağlantıları ve profil fotoğrafı
- **Hakkımızda Sayfası Yönetimi**: İçerik ve görselleri düzenleme
- **İletişim Sayfası Yönetimi**: Konum, hizmet saatleri, e-posta, telefon bilgilerini düzenleme
- **Kullanıcı Yönetimi**: Admin kullanıcı ekleme, düzenleme, silme
- **Mesaj Yönetimi**: Ziyaretçilerden gelen mesajları listeleme ve okuma

---

## 🛠️ Kullanılan Teknolojiler

| Katman | Teknoloji | Sürüm |
|---|---|---|
| **Framework** | ASP.NET MVC | 5.2.7 |
| **Runtime** | .NET Framework | 4.7.2 |
| **ORM** | Entity Framework (Database First — EDMX) | 6.2.0 |
| **Veritabanı** | Microsoft SQL Server (Express) | — |
| **Ön Yüz** | Bootstrap | 3.4.1 |
| **JavaScript** | jQuery | 3.7.1 |
| **DataTables** | jquery.datatables | 1.10.15 |
| **Sayfalama** | PagedList.Mvc | 4.5.0 |
| **JSON** | Newtonsoft.Json | 12.0.2 |
| **Diğer** | Modernizr, WebGrease, jQuery Validation | — |
| **IDE** | Visual Studio 2019+ (Solution Format v12) | — |

---

## 📋 Gereksinimler

- **Visual Studio 2019** veya üzeri (ASP.NET ve web geliştirme iş yükü yüklü)
- **.NET Framework 4.7.2** Developer Pack
- **SQL Server 2017+** veya **SQL Server Express** (LocalDB de kullanılabilir)
- **SQL Server Management Studio (SSMS)** (veritabanı script'ini çalıştırmak için)

---

## 🚀 Kurulum ve Çalıştırma

### 1. Projeyi Klonlayın

```bash
git clone https://github.com/<KULLANICI_ADI>/emlaksitesi.git
cd emlaksitesi
```

### 2. Veritabanını Oluşturun

1. **SQL Server Management Studio (SSMS)** uygulamasını açın.
2. Proje kök dizinindeki **`emlak.sql`** dosyasını SSMS'te açın.
3. Script'i çalıştırarak `Emlak` veritabanını ve tablolarını oluşturun.

### 3. Connection String Ayarı

`emlaksitesi/emlaksitesi/Web.config` dosyasında, `<connectionStrings>` bölümündeki bağlantı dizesini kendi SQL Server bilgilerinize göre güncelleyin:

```xml
<connectionStrings>
  <add name="emlakEntities"
       connectionString="metadata=res://*/Models.database.Model1.csdl|res://*/Models.database.Model1.ssdl|res://*/Models.database.Model1.msl;
       provider=System.Data.SqlClient;
       provider connection string=&quot;
         data source=SUNUCU_ADI\INSTANCE_ADI;
         initial catalog=Emlak;
         integrated security=True;
         trustservercertificate=True;
         MultipleActiveResultSets=True;
         App=EntityFramework&quot;"
       providerName="System.Data.EntityClient" />
</connectionStrings>
```

> **⚠️ Not:** `data source` alanını kendi SQL Server instance adınızla değiştirin. Örneğin: `BILGISAYAR_ADI\SQLEXPRESS` veya `localhost`.

### 4. Projeyi Açın ve Çalıştırın

1. `emlaksitesi/emlaksitesi/emlaksitesi.sln` dosyasını **Visual Studio** ile açın.
2. **NuGet paketlerini geri yükleyin**: Solution Explorer → Sağ tık → "Restore NuGet Packages"
3. Projeyi derleyin: `Ctrl + Shift + B`
4. Çalıştırın: `F5` (Debug) veya `Ctrl + F5` (Debug olmadan)
5. Uygulama varsayılan tarayıcınızda açılacaktır.

---

## 🗄️ Veritabanı

### Şema

Proje, **Entity Framework Database First** yaklaşımı kullanır. Veritabanı `Emlak` adıyla oluşturulmuştur ve aşağıdaki tablolardan oluşur:

| Tablo | Açıklama |
|---|---|
| `slider` | Gayrimenkul ilanları (başlık, şehir, ilçe, adres, fiyat, yatak/banyo sayısı, durum, görsel) |
| `detay` | İlanlara ait detay bilgileri ve görselleri (3 adet resim, açıklama) |
| `ajans` | Emlak danışmanları (ad-soyad, görev, açıklama, profil resmi, sosyal medya linkleri) |
| `anasayfa` | Ana sayfa içeriği (başlık, görseller) |
| `hakkimizda` | Hakkımızda sayfası içeriği (başlık, açıklamalar, görseller) |
| `bize_ulasin` | İletişim bilgileri (konum, hizmet saatleri, e-posta, telefon) |
| `mesaj` | Ziyaretçi mesajları (isim, e-posta, konu, mesaj) |
| `footer` | Alt bilgi alanı (adres, telefon, e-posta, sosyal medya) |
| `yonetim` | Admin kullanıcıları (ad, soyad, kullanıcı adı, şifre) |

### İlişkiler

- `slider` ↔ `detay` → Bir ilana ait birden fazla detay (1:N)
- `ajans` ↔ `detay` → Bir danışmana ait birden fazla detay (1:N)

### Connection String Örneği

```
data source=SUNUCU_ADI\SQLEXPRESS;initial catalog=Emlak;integrated security=True;
```

> **⚠️ Uyarı:** Gerçek sunucu adınızı, varsa kullanıcı adı/şifrenizi burada paylaşmayın.

### Veritabanı Script'i

Proje kök dizinindeki `emlak.sql` dosyası veritabanı yapısını ve (varsa) örnek verileri içerir. SSMS üzerinde çalıştırarak veritabanını kurabilirsiniz.

> **ℹ️ Not:** Projede Code First migration bulunmamaktadır. Veritabanı, SQL script ile manuel olarak oluşturulur.

---

## 📁 Proje Yapısı

```
dogukan_guney_Proje/
├── emlak.sql                          # Veritabanı oluşturma SQL script'i
└── emlaksitesi/
    └── emlaksitesi/
        ├── emlaksitesi.sln            # Visual Studio Solution dosyası
        └── emlaksitesi/               # Ana proje klasörü
            ├── Controllers/           # MVC Controller'lar
            │   ├── HomeController.cs         # Ana sayfa, hakkımızda, iletişim, ilanlar, detay
            │   ├── SliderController.cs       # İlan CRUD (admin)
            │   ├── DetayController.cs        # İlan detay CRUD (admin)
            │   ├── AjansController.cs        # Danışman CRUD (admin)
            │   ├── AboutController.cs        # Hakkımızda düzenleme (admin)
            │   ├── ContactController.cs      # İletişim düzenleme (admin)
            │   ├── UserController.cs         # Admin kullanıcı CRUD
            │   ├── YonetimController.cs      # Admin giriş/çıkış
            │   └── MessagesController.cs     # Gelen mesajları görüntüleme (admin)
            ├── Models/
            │   └── database/          # Entity Framework EDMX ve model sınıfları
            ├── Views/
            │   ├── Home/              # Ziyaretçi sayfaları (Index, About, Contact, Properties, Detay)
            │   ├── Slider/            # İlan yönetimi view'ları
            │   ├── Detay/             # Detay yönetimi view'ları
            │   ├── Ajans/             # Danışman yönetimi view'ları
            │   ├── About/             # Hakkımızda yönetimi view'ları
            │   ├── Contact/           # İletişim yönetimi view'ları
            │   ├── User/              # Kullanıcı yönetimi view'ları
            │   ├── Yonetim/           # Admin giriş view'ları
            │   ├── Messages/          # Mesaj view'ları
            │   └── Shared/
            │       ├── _Layout.cshtml         # Ziyaretçi sayfa düzeni
            │       └── _AdminLayout.cshtml    # Yönetim paneli sayfa düzeni
            ├── Content/               # CSS dosyaları ve statik içerikler
            ├── Scripts/               # JavaScript dosyaları
            ├── Admin_Template/        # Admin paneli HTML/CSS/JS şablonu
            ├── property-1.0.0/        # Ön yüz HTML şablonu (Property teması)
            ├── uploads/               # Yüklenen görsellerin saklandığı klasör
            ├── fonts/                 # Font dosyaları
            ├── Web.config             # Uygulama yapılandırması ve connection string
            └── packages.config        # NuGet paket listesi
```

---

## 📖 Kullanım

### Ziyaretçi Akışı

1. **Ana Sayfa** → Öne çıkan ilanları slider'da görüntüleyin → Danışman bilgilerini inceleyin.
2. **Properties** → Tüm ilanları sayfalama ile listeleyin.
3. **İlan Detay** → Bir ilana tıklayarak detay bilgilerini, çoklu görselleri ve sorumlu danışmanı görüntüleyin.
4. **Hakkımızda** → Şirket hakkında bilgi alın.
5. **İletişim** → İletişim bilgilerini görün ve iletişim formunu doldurup mesaj gönderin.

### Admin Akışı

1. `/Yonetim/login` adresine gidin.
2. Kullanıcı adı ve şifre ile giriş yapın.
3. **Yönetim panelinden**:
   - İlan ekleyin/düzenleyin/silin (görsel yükleme destekli)
   - Danışman profillerini yönetin
   - Hakkımızda ve İletişim sayfalarının içeriklerini güncelleyin
   - Admin kullanıcılarını yönetin
   - Gelen mesajları okuyun
4. Çıkış yapmak için **Logout** butonuna tıklayın.

