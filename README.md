# HairBello ✂️

**HairBello** is a mobile-first barbershop appointment booking app built with **.NET MAUI Blazor** and **ASP.NET Core API**.

1111 ← *(Image of the app interface on mobile)*

---

## 📦 Project Structure

### 1. `ClientSide` - MAUI Blazor Mobile App
- Built using .NET MAUI Blazor targeting Android & iOS.
- login via **phone number verification only**.
- Mobile-friendly UI with clean navigation.
- Permissions handled directly by the system.

#### Key Screens & Components:
- `PhonAuth.razor`: Phone number login.
- `Home.razor`: Booking overview.
- `ScheduleView.razor`: View barber schedules.
- `Admin.razor`: Admin dashboard.
- `OrdersManagement.razor`: Manage bookings.
- `ImagesManagement.razor`: Manage gallery images.
- `AppManagement.razor`: Update system settings.
- `PastOrderMangment`: View Past Orders.
- `BarbersManagement`: Mange Barbers

![WhatsApp Image 2025-04-03 at 04 55 46_0b69c11e](https://github.com/user-attachments/assets/96849db6-7c15-4923-9b69-e78449dd2f5b)
![WhatsApp Image 2025-04-03 at 04 56 29_e7744626](https://github.com/user-attachments/assets/3773cb05-3d24-4e04-9496-63de84da5b93)
![WhatsApp Image 2025-04-03 at 04 57 05_56de04c6](https://github.com/user-attachments/assets/74cb96e2-0304-4ad7-b170-9d59698e1ad5)

---

### 2. `ServerSide` - ASP.NET Core Web API

#### Key Features:
- **Phone number login** with OTP.
- **User management** and admin features.
- **Appointments**: create, cancel, edit, fetch.
- **Barbers management**: CRUD operations + schedule & breaks.
- **Services**: assign to barbers with time & price.
- **Gallery**: upload/delete images.
- **Settings**: manage working hours & limits.

#### Main Controllers:
- `AuthController`
- `OrdersController`
- `BarberController`
- `ServiceController`
- `ImagesController`
- `ScheduleController`
- `UsersController`
- `SettingsController`

![image](https://github.com/user-attachments/assets/7672207c-4e02-4762-bfab-b9e6a2dcfe3f)

---

### 3. `Domain` - Shared DTO Layer

- `CreateOrderDto`, `CreateBarberDto`, `CreateServiceDto`
- `CreateBarberScheduleDto`, `CreateBarberBreak`
- `CreatePreviewImageDto`, `CreateUserDto`
- `GetElementsLimtedDto`: filtering data by date/barber

All DTOs are clearly structured for smooth API interaction.

---

## 🔐 Role System

| Role     | Permissions                                                   |
|----------|---------------------------------------------------------------|
| User     | Book appointments, view history, view gallery.               |
| Admin    | Manage barbers, services, orders, images, settings.          |

---

## 🛠️ Tech Stack
- .NET 9.0 MAUI Blazor
- ASP.NET Core Web API
- SQLite or MySQL
- C# 12

---

## 📱 User Experience
- Responsive UI made for mobile.
- Fast navigation between views.
- All features accessible within the app.

![WhatsApp Image 2025-04-03 at 05 02 05_517d3ebf](https://github.com/user-attachments/assets/7b7ecc68-4eae-4f62-b898-41456a4b80c7)

---

## 📞 Contact
**Developer:** Ali Sohel Zedan   
📧 Email: [your-email@example.com](mailto:ali.sohel.zedan.1.4.2004@gmail.com)  
📱 WhatsApp: [+9720507397306](https://wa.me/9720507397306)  
🌐 Website: [hairbello.site](https://hairbello.site)

---

---

# 💬 هير بيلو - بالعربية

**HairBello** هو تطبيق لحجز مواعيد صالون حلاقة، مصمم خصيصًا للجوال باستخدام **.NET MAUI Blazor** و**ASP.NET Core API**.

## 📦 مكونات المشروع

### 1. الواجهة - MAUI Blazor
- تطبيق موبايل لأنظمة Android و iOS.
- تسجيل دخول فقط عبر رقم الهاتف مع رمز تحقق.
- تصميم بسيط ومتجاوب للجوال.

#### الصفحات الأساسية:
- صفحة تسجيل الدخول برقم الهاتف.
- الحجز ومواعيد المزينين.
- لوحة تحكم للإدارة.
- إدارة الصور والخدمات والإعدادات.

### 2. API - ASP.NET Core
- تسجيل الدخول وإرسال كود التحقق.
- إنشاء وتعديل وحذف المواعيد.
- إدارة المزينين والاستراحات.
- إدارة الصور والخدمات.
- إعدادات العمل والأوقات.

### 3. طبقة Domain
- كل DTO منظم مثل: `CreateOrderDto`, `CreateServiceDto`, `CreateBarberDto`...
- يدعم كل العمليات المطلوبة.

## 🔐 الصلاحيات:
| الدور       | الصلاحيات                                                      |
|-------------|------------------------------------------------------------------|
| المستخدم    | حجز موعد، عرض حجوزاته، تصفح المعرض.                             |
| الإداري     | إدارة المزينين، الخدمات، الصور، الطلبات، الإعدادات.             |

## 💡 تجربة الاستخدام:
- تصميم بسيط وسريع.
- كل شيء داخل التطبيق.
- مناسب لجميع المستخدمين.

## 🧑‍💻 المطور:
**Ali sohel Zedan**

