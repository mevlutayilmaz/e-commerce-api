# E-Ticaret API

Bu proje, bir e-ticaret web sitesi için backend API'sini sağlar. Modern yazılım mimarisi prensipleri izleyerek, Entity Framework Core ve Code First yaklaşımı ile Microsoft SQL Server veritabanı kullanılarak geliştirilmiştir. API, ürünlerin, kategorilerin, sepetlerin ve siparişlerin yönetimini sağlar ve kullanıcı kimlik doğrulaması için JWT (JSON Web Token) kullanır. Proje, Onion Architecture prensipleri doğrultusunda katmanlı bir yapıya sahiptir.

## Proje Mimarisi

Proje, bağımlılıkları yönetmek ve kodu daha düzenli hale getirmek için Onion Architecture prensipleri doğrultusunda katmanlı bir mimari kullanır. Her katman belirli bir sorumluluğa sahiptir ve sadece kendisinden daha düşük seviyeli katmanlara bağımlıdır.

### 1. Core Katmanı

Bu katman, uygulamanın iş mantığını ve kurallarını içerir. İki yapıdan oluşur:

- **Domain**:
  - Uygulamanın temel yapı taşlarını (Entities) tanımlar. Örneğin, `Product`, `Category`, `Basket`, `Order` entity'leri burada bulunur.
  - İş kurallarını ve doğrulamalarını içerir.
  - Veritabanı veya diğer altyapı teknolojilerinden bağımsızdır.

- **Application**:
  - Use case'leri ve iş mantığının uygulanmasını içerir.
  - Domain katmanındaki entity'leri ve arayüzleri kullanarak iş operasyonlarını gerçekleştirir.
  - Veritabanı veya diğer altyapı teknolojilerinden bağımsızdır.
  - CQRS (Command Query Responsibility Segregation) desenini kullanarak komutları ve sorguları ayırır.

### 2. Infrastructure Katmanı

Bu katman, uygulamanın altyapısal bileşenlerini içerir. İki yapıdan oluşur:

- **Infrastructure**:
  - Uygulama genelinde kullanılan ortak altyapı bileşenlerini içerir.
  - Örneğin, e-posta gönderme, logging gibi işlemler için servisler burada bulunabilir.

- **Persistence**:
  - Veritabanı erişimini yönetir.
  - Entity Framework Core kullanarak veritabanı işlemlerini gerçekleştirir.
  - Repository desenini kullanarak veritabanı erişimini soyutlar.
  - Domain katmanındaki entity'leri veritabanı tablolarına eşler.

### 3. Presentation Katmanı

Bu katman, kullanıcı arayüzünü ve API'yi içerir. Tek bir yapıdan oluşur:

- **API (.NET Web API)**:
  - Uygulamanın dış dünyaya açılan kapısıdır.
  - HTTP isteklerini alır ve Application katmanındaki use case'leri çağırarak işler.
  - JSON formatında yanıtlar döndürür.
  - Kullanıcı kimlik doğrulaması ve yetkilendirme için JWT (JSON Web Token) kullanır.

## Kimlik Doğrulama ve Yetkilendirme

Proje, kullanıcı kimlik doğrulaması ve yetkilendirme için JWT (JSON Web Token) kullanır. Kullanıcılar, kullanıcı adı ve şifreleriyle giriş yaparak bir Access Token ve bir Refresh Token alırlar.

- **Access Token**: API'ye erişmek için kullanılır ve sınırlı bir geçerlilik süresine sahiptir.
- **Refresh Token**: Access Token'ın süresi dolduğunda yeni bir Access Token almak için kullanılır ve daha uzun bir geçerlilik süresine sahiptir.

## Proje Detayları

- **Product (Ürün)**: Ürün bilgilerini içerir (Ad, Açıklama, Fiyat, Resim, vb.).
- **Category (Kategori)**: Ürün kategorilerini içerir (Ad, Açıklama, vb.).
- **Basket (Sepet)**: Kullanıcıların sepetlerini içerir (Ürünler, Miktarlar, vb.).
- **Order (Sipariş)**: Kullanıcıların siparişlerini içerir (Ürünler, Miktarlar, Adres, vb.).
- **AppUser**: Uygulama kullanıcılarını temsil eder.
- **AppRole**: Kullanıcı rollerini temsil eder.

## Kurulum ve Çalıştırma

1. **Projeyi klonlayın:**
   ```bash
   git clone https://github.com/mevlutayilmaz/e-commerce-api.git
   ```

2. **`ECommerce.API` klasörüne gidin.**

3. **`appsettings.json` dosyasında veritabanı bağlantı bilgilerini ayarlayın.**

4. **Paketleri yükleyin:**
   ```bash
   dotnet restore
   ```

5. **Veritabanını oluşturun ve migrate edin:**
   ```bash
   dotnet ef database update
   ```

6. **Projeyi çalıştırın:**
   ```bash
   dotnet run
   ```

## Sonuç

Bu proje, temiz bir mimari ve modern teknolojiler kullanarak geliştirilmiş, güvenli ve ölçeklenebilir bir e-ticaret API'sidir. Proje, katmanlı yapısı sayesinde bakımı ve geliştirilmesi kolaydır.

**Bu API'yi kullanan React projesi:** [E-Commerce UI](https://github.com/mevlutayilmaz/e-commerce-ui)

**Web Scraping işlemi ile veritabanına ürünler eklenebilir:** [Web Scraping](https://github.com/mevlutayilmaz/web-scraping)
