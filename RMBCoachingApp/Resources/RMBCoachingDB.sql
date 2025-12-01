-- RMBCoachingDB.sql

CREATE DATABASE RMBCoachingDB;
GO

USE RMBCoachingDB;
GO

-- Edzők tábla
CREATE TABLE Trainers (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Specialty NVARCHAR(100) NOT NULL,
    ExperienceYears INT DEFAULT 0,
    TrainedAthletes INT DEFAULT 0,
    Description NVARCHAR(MAX),
    Email NVARCHAR(100),
    Phone NVARCHAR(20),
    HireDate DATETIME DEFAULT GETDATE(),
    IsActive BIT DEFAULT 1,
    Rating DECIMAL(3,2) DEFAULT 0.0
);
GO

-- Edzéstervek tábla
CREATE TABLE TrainingPlans (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    Category NVARCHAR(100) NOT NULL,
    DurationWeeks INT NOT NULL,
    Price DECIMAL(10,2) NOT NULL,
    TrainerId INT FOREIGN KEY REFERENCES Trainers(Id),
    Description NVARCHAR(MAX),
    DifficultyLevel NVARCHAR(50) DEFAULT 'Közép',
    SessionsPerWeek INT DEFAULT 3,
    CreatedDate DATETIME DEFAULT GETDATE(),
    IsPublished BIT DEFAULT 1,
    PurchaseCount INT DEFAULT 0
);
GO

-- Sportolók tábla
CREATE TABLE Athletes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    Phone NVARCHAR(20),
    RegistrationDate DATETIME DEFAULT GETDATE(),
    Age INT NOT NULL,
    SportType NVARCHAR(100) DEFAULT 'Általános',
    IsActive BIT DEFAULT 1,
    Address NVARCHAR(200),
    EmergencyContact NVARCHAR(100),
    MedicalNotes NVARCHAR(MAX),
    LastLoginDate DATETIME,
    MembershipType NVARCHAR(50) DEFAULT 'Alap'
);
GO

-- Vásárlások tábla
CREATE TABLE Purchases (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    AthleteId INT FOREIGN KEY REFERENCES Athletes(Id),
    TrainingPlanId INT FOREIGN KEY REFERENCES TrainingPlans(Id),
    PurchaseDate DATETIME DEFAULT GETDATE(),
    Price DECIMAL(10,2) NOT NULL,
    PaymentMethod NVARCHAR(50) DEFAULT 'Bankkártya',
    IsPaid BIT DEFAULT 1,
    InvoiceNumber NVARCHAR(50),
    Notes NVARCHAR(MAX),
    Status NVARCHAR(50) DEFAULT 'Teljesítve'
);
GO

-- Naptár események tábla
CREATE TABLE CalendarEvents (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX),
    EventDate DATE NOT NULL,
    StartTime TIME NOT NULL,
    EndTime TIME NOT NULL,
    Location NVARCHAR(200),
    EventType NVARCHAR(50) DEFAULT 'Edzés',
    Participants NVARCHAR(MAX),
    TrainerId INT FOREIGN KEY REFERENCES Trainers(Id),
    IsCompleted BIT DEFAULT 0,
    Notes NVARCHAR(MAX),
    CreatedDate DATETIME DEFAULT GETDATE()
);
GO

-- Indexek létrehozása
CREATE INDEX IX_TrainingPlans_TrainerId ON TrainingPlans(TrainerId);
CREATE INDEX IX_Purchases_AthleteId ON Purchases(AthleteId);
CREATE INDEX IX_Purchases_TrainingPlanId ON Purchases(TrainingPlanId);
CREATE INDEX IX_CalendarEvents_EventDate ON CalendarEvents(EventDate);
CREATE INDEX IX_CalendarEvents_TrainerId ON CalendarEvents(TrainerId);
GO

-- Minta adatok beszúrása
INSERT INTO Trainers (Name, Specialty, ExperienceYears, TrainedAthletes, Email, Phone, Rating)
VALUES 
('Kovács János', 'Labdarúgás', 15, 120, 'janos.kovacs@rmbcoaching.hu', '+36123456789', 4.8),
('Nagy Éva', 'Kondicionálás', 8, 85, 'eva.nagy@rmbcoaching.hu', '+36234567890', 4.9),
('Tóth Péter', 'Általános sportedzés', 10, 95, 'peter.toth@rmbcoaching.hu', '+36345678901', 4.7),
('Szabó Anna', 'Erőnlét', 6, 60, 'anna.szabo@rmbcoaching.hu', '+36456789012', 4.6);
GO

INSERT INTO TrainingPlans (Title, Category, DurationWeeks, Price, TrainerId, Description, DifficultyLevel)
VALUES
('Pro Labdarúgó Program', 'Labdarúgás', 12, 29900, 1, 'Professzionális labdarúgó edzésprogram', 'Haladó'),
('Kondíció Felépítés', 'Kondicionálás', 8, 18900, 2, 'Általános kondicionáló program', 'Kezdő'),
('Teljes Test Erősítés', 'Általános sportedzés', 10, 22900, 3, 'Teljes testet átfogó edzés', 'Közép'),
('Maraton Felkészítés', 'Erőnlét', 16, 34900, 4, 'Maraton futók számára', 'Haladó');
GO

INSERT INTO Athletes (Name, Email, Phone, Age, SportType, MembershipType)
VALUES
('Kiss Gábor', 'gabor.kiss@example.com', '+36701234567', 25, 'Labdarúgás', 'Prémium'),
('Molnár Zsuzsa', 'zsuzsa.molnar@example.com', '+36711234568', 32, 'Kondicionálás', 'Alap'),
('Varga Tamás', 'tamas.varga@example.com', '+36721234569', 28, 'Általános sportedzés', 'VIP'),
('Farkas Ildikó', 'ildiko.farkas@example.com', '+36731234570', 19, 'Erőnlét', 'Prémium');
GO

INSERT INTO Purchases (AthleteId, TrainingPlanId, Price, PaymentMethod, InvoiceNumber)
VALUES
(1, 1, 29900, 'Bankkártya', 'INV-2023-001'),
(2, 2, 18900, 'Banki átutalás', 'INV-2023-002'),
(3, 3, 22900, 'Készpénz', 'INV-2023-003'),
(4, 4, 34900, 'Bankkártya', 'INV-2023-004');
GO

INSERT INTO CalendarEvents (Title, EventDate, StartTime, EndTime, Location, EventType, TrainerId, Participants)
VALUES
('Csapat edzés', '2024-01-15', '18:00', '20:00', 'Fő stadion', 'Edzés', 1, 'Kiss Gábor, Molnár Zsuzsa'),
('Kondicionáló óra', '2024-01-16', '17:00', '18:30', 'Konditerem', 'Edzés', 2, 'Farkas Ildikó'),
('További megbeszélés', '2024-01-17', '14:00', '15:00', 'Iroda', 'Találkozó', 3, 'Varga Tamás');
GO

-- Statisztikai lekérdezések
CREATE VIEW vw_Statistics AS
SELECT 
    (SELECT COUNT(*) FROM Trainers WHERE IsActive = 1) as TotalTrainers,
    (SELECT AVG(ExperienceYears) FROM Trainers WHERE IsActive = 1) as AvgExperience,
    (SELECT COUNT(*) FROM Athletes) as TotalAthletes,
    (SELECT COUNT(*) FROM Athletes WHERE IsActive = 1) as ActiveAthletes,
    (SELECT COUNT(*) FROM TrainingPlans WHERE IsPublished = 1) as TotalPlans,
    (SELECT SUM(Price) FROM Purchases WHERE IsPaid = 1) as TotalRevenue;
GO

PRINT 'Adatbázis létrehozva és inicializálva!';