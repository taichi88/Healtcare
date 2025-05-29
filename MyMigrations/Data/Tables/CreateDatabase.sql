
CREATE DATABASE HealthcareApi;
GO

USE HealthcareApi;
GO

CREATE SCHEMA Core;
GO

CREATE SCHEMA Clinical;
GO

CREATE SCHEMA Billing;
GO



CREATE TABLE Core.Persons (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Surname VARCHAR(100) NOT NULL,
    Email VARCHAR(100),
    PersonalNumber VARCHAR(20) NOT NULL UNIQUE,
    DateOfBirth DATE,
    Phone VARCHAR(20),
    Address VARCHAR(255)
);


CREATE TABLE Core.DeskStaff (
    PersonId INT PRIMARY KEY,
    FOREIGN KEY (PersonId) REFERENCES Core.Persons(Id)
);

CREATE TABLE Core.Patients (
    PersonId INT PRIMARY KEY,
    InsuranceNumber VARCHAR(50),
    EmergencyContactName VARCHAR(100),
    EmergencyContactPhone VARCHAR(20),
    BloodType VARCHAR(3),
    Allergies TEXT,
    CONSTRAINT FK_Patients_Person FOREIGN KEY (PersonId) REFERENCES Core.Persons(Id)
);

CREATE TABLE Core.Doctors (
    PersonId INT PRIMARY KEY,
    Specialty VARCHAR(100),
    LicenseNumber VARCHAR(50),
    YearsOfExperience INT,
    CONSTRAINT FK_Doctors_Person FOREIGN KEY (PersonId) REFERENCES Core.Persons(Id)
);

CREATE TABLE Clinical.Appointments (
    AppointmentId INT IDENTITY(1,1) PRIMARY KEY,
    PatientId INT NOT NULL,
    DoctorId INT NOT NULL,
    AppointmentDateTime DATETIME NOT NULL,
    ReasonForVisit VARCHAR(255),
    Status VARCHAR(50),
    Notes TEXT,
    CONSTRAINT FK_Appointments_Patient FOREIGN KEY (PatientId) REFERENCES Core.Patients(PersonId),
    CONSTRAINT FK_Appointments_Doctor FOREIGN KEY (DoctorId) REFERENCES Core.Doctors(PersonId)
);

CREATE TABLE Clinical.Diagnoses (
    DiagnosisId INT IDENTITY(1,1) PRIMARY KEY,
    PatientId INT NOT NULL,
    DiagnosisDate DATE NOT NULL,
    Description VARCHAR(255),
    PrescribedTreatment TEXT,
    DoctorId INT NOT NULL,
    CONSTRAINT FK_Diagnoses_Patient FOREIGN KEY (PatientId) REFERENCES Core.Patients(PersonId),
    CONSTRAINT FK_Diagnoses_Doctor FOREIGN KEY (DoctorId) REFERENCES Core.Doctors(PersonId)
);

CREATE TABLE Billing.Payments (
    PaymentId INT IDENTITY(1,1) PRIMARY KEY,
    AppointmentId INT NOT NULL,
    Amount DECIMAL(10, 2) NOT NULL,
    PaymentDate DATETIME NOT NULL,
    PaymentMethod VARCHAR(50),
    Status VARCHAR(50),
    Notes TEXT,
    CONSTRAINT FK_Payments_Appointment FOREIGN KEY (AppointmentId) REFERENCES Clinical.Appointments(AppointmentId)
);

CREATE INDEX IX_Persons_Surname ON Core.Persons(Surname);
CREATE INDEX IX_Patients_PersonId ON Core.Patients(PersonId);
CREATE INDEX IX_Doctors_Specialty ON Core.Doctors(Specialty);
CREATE INDEX IX_Appointments_PatientId ON Clinical.Appointments(PatientId);
CREATE INDEX IX_Appointments_DoctorId ON Clinical.Appointments(DoctorId);
CREATE INDEX IX_Appointments_Date ON Clinical.Appointments(AppointmentDateTime);
CREATE INDEX IX_Diagnoses_PatientId ON Clinical.Diagnoses(PatientId);
CREATE INDEX IX_Diagnoses_DoctorId ON Clinical.Diagnoses(DoctorId);
CREATE INDEX IX_Diagnoses_Date ON Clinical.Diagnoses(DiagnosisDate);
CREATE INDEX IX_Payments_AppointmentId ON Billing.Payments(AppointmentId);
CREATE INDEX IX_Payments_PaymentDate ON Billing.Payments(PaymentDate);