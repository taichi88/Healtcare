
CREATE DATABASE HealthcareApi;
GO


USE HealthcareApi;
GO


CREATE SCHEMA healthcare;
GO


CREATE TABLE healthcare.Persons (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Surname VARCHAR(100) NOT NULL,
    Email VARCHAR(100),
    PersonalNumber VARCHAR(20) NOT NULL UNIQUE,
    DateOfBirth DATE,
    Phone VARCHAR(20),
    Address VARCHAR(255)
);


CREATE TABLE healthcare.DeskStaff (
    PersonId INT PRIMARY KEY,
    FOREIGN KEY (PersonId) REFERENCES healthcare.Persons(Id)
);

CREATE TABLE healthcare.Patients (
    PersonId INT PRIMARY KEY,
    InsuranceNumber VARCHAR(50),
    EmergencyContactName VARCHAR(100),
    EmergencyContactPhone VARCHAR(20),
    BloodType VARCHAR(3),
    Allergies TEXT,
    CONSTRAINT FK_Patients_Person FOREIGN KEY (PersonId) REFERENCES healthcare.Persons(Id)
);

CREATE TABLE healthcare.Doctors (
    PersonId INT PRIMARY KEY,
    Specialty VARCHAR(100),
    LicenseNumber VARCHAR(50),
    YearsOfExperience INT,
    CONSTRAINT FK_Doctors_Person FOREIGN KEY (PersonId) REFERENCES healthcare.Persons(Id)
);

CREATE TABLE healthcare.Appointments (
    AppointmentId INT IDENTITY(1,1) PRIMARY KEY,
    PatientId INT NOT NULL,
    DoctorId INT NOT NULL,
    AppointmentDateTime DATETIME NOT NULL,
    ReasonForVisit VARCHAR(255),
    Status VARCHAR(50),
    Notes TEXT,
    CONSTRAINT FK_Appointments_Patient FOREIGN KEY (PatientId) REFERENCES healthcare.Patients(PersonId),
    CONSTRAINT FK_Appointments_Doctor FOREIGN KEY (DoctorId) REFERENCES healthcare.Doctors(PersonId)
);

CREATE TABLE healthcare.Diagnoses (
    DiagnosisId INT IDENTITY(1,1) PRIMARY KEY,
    PatientId INT NOT NULL,
    DiagnosisDate DATE NOT NULL,
    Description VARCHAR(255),
    PrescribedTreatment TEXT,
    DoctorId INT NOT NULL,
    CONSTRAINT FK_Diagnoses_Patient FOREIGN KEY (PatientId) REFERENCES healthcare.Patients(PersonId),
    CONSTRAINT FK_Diagnoses_Doctor FOREIGN KEY (DoctorId) REFERENCES healthcare.Doctors(PersonId)
);

CREATE TABLE healthcare.Payments (
    PaymentId INT IDENTITY(1,1) PRIMARY KEY,
    AppointmentId INT NOT NULL,
    Amount DECIMAL(10, 2) NOT NULL,
    PaymentDate DATETIME NOT NULL,
    PaymentMethod VARCHAR(50),
    Status VARCHAR(50),
    Notes TEXT,
    CONSTRAINT FK_Payments_Appointment FOREIGN KEY (AppointmentId) REFERENCES healthcare.Appointments(AppointmentId)
);

CREATE INDEX IX_Persons_Surname ON healthcare.Persons(Surname);
CREATE INDEX IX_Patients_PersonId ON healthcare.Patients(PersonId);
CREATE INDEX IX_Doctors_Specialty ON healthcare.Doctors(Specialty);
CREATE INDEX IX_Appointments_PatientId ON healthcare.Appointments(PatientId);
CREATE INDEX IX_Appointments_DoctorId ON healthcare.Appointments(DoctorId);
CREATE INDEX IX_Appointments_Date ON healthcare.Appointments(AppointmentDateTime);
CREATE INDEX IX_Diagnoses_PatientId ON healthcare.Diagnoses(PatientId);
CREATE INDEX IX_Diagnoses_DoctorId ON healthcare.Diagnoses(DoctorId);
CREATE INDEX IX_Diagnoses_Date ON healthcare.Diagnoses(DiagnosisDate);
CREATE INDEX IX_Payments_AppointmentId ON healthcare.Payments(AppointmentId);
CREATE INDEX IX_Payments_PaymentDate ON healthcare.Payments(PaymentDate);