MASSAGE RESERVATION DESKTOP APP - VISUAL STUDIO 2022

This is the desktop counterpart of the PHP Massage Reservation System.
It connects to the SAME MySQL database: massage_reservation_system

REQUIREMENTS:
1. Visual Studio 2022
2. .NET 6 Desktop Development workload
3. XAMPP / MySQL running
4. The PHP web system database imported already using database.sql

HOW TO RUN:
1. Start XAMPP and run Apache + MySQL.
2. Open phpMyAdmin and confirm the database name is:
   massage_reservation_system
3. Extract this ZIP.
4. Open MassageReservationDesktop.sln in Visual Studio 2022.
5. Restore NuGet packages if Visual Studio asks.
6. Press F5 to run.

DEFAULT ADMIN LOGIN:
Email: admin@example.com
Password: admin123

DATABASE CONNECTION:
Open:
MassageReservationDesktop/AppSettings.cs

Default connection string:
Server=localhost;Port=3306;Database=massage_reservation_system;Uid=root;Pwd=;

If your MySQL has a password, edit Pwd=yourpassword;

WHAT IS CONNECTED WITH THE WEB APP:
- Services added in desktop appear in the PHP web system.
- Therapists added in desktop appear in the PHP web system.
- Reservations from the web appear in the desktop app.
- Admin status changes in desktop reflect in the web system.
- Both use the same users, services, therapists, and reservations tables.

NOTE ABOUT PASSWORDS:
The included admin account works directly.
Users created inside the desktop app use SHA256 password hashing.
Users created in the PHP web app use PHP password_hash, so the desktop app may not verify those customer passwords unless bcrypt support is added.
Recommended use: desktop app is mainly for admin/management side.
