CREATE TABLE Persons (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Surname VARCHAR(100) NOT NULL,
    Email VARCHAR(100),
    PersonalNumber VARCHAR(20) NOT NULL UNIQUE,
    DateOfBirth DATE,
    Phone VARCHAR(20),
    Address VARCHAR(255)
);

CREATE TABLE DeskStaff (
    PersonId INT PRIMARY KEY,
    FOREIGN KEY (PersonId) REFERENCES Persons(Id)
);

CREATE TABLE Patients (
    PersonId INT PRIMARY KEY,
    InsuranceNumber VARCHAR(50),
    EmergencyContactName VARCHAR(100),
    EmergencyContactPhone VARCHAR(20),
    BloodType VARCHAR(3),
    Allergies TEXT,
    CONSTRAINT FK_Patients_Person FOREIGN KEY (PersonId) REFERENCES Persons(Id)
);

CREATE TABLE Doctors (
    PersonId INT PRIMARY KEY,
    Specialty VARCHAR(100),
    LicenseNumber VARCHAR(50),
    YearsOfExperience INT,
    CONSTRAINT FK_Doctors_Person FOREIGN KEY (PersonId) REFERENCES Persons(Id)
);

CREATE TABLE Appointments (
    AppointmentId INT IDENTITY(1,1) PRIMARY KEY,
    PatientId INT,
    DoctorId INT,
    AppointmentDateTime DATETIME NOT NULL,
    ReasonForVisit VARCHAR(255),
    Status VARCHAR(50), -- e.g., Scheduled, Completed, Cancelled
    Notes TEXT,
    CONSTRAINT FK_Appointments_Patient FOREIGN KEY (PatientId) REFERENCES Patients(PersonId),
    CONSTRAINT FK_Appointments_Doctor FOREIGN KEY (DoctorId) REFERENCES Doctors(PersonId)
);

CREATE TABLE Diagnoses (
    DiagnosisId INT IDENTITY(1,1) PRIMARY KEY,
    PatientId INT,
    DiagnosisDate DATE NOT NULL,
    Description VARCHAR(255),
    PrescribedTreatment TEXT,
    DoctorId INT,
    CONSTRAINT FK_Diagnoses_Patient FOREIGN KEY (PatientId) REFERENCES Patients(PersonId),
    CONSTRAINT FK_Diagnoses_Doctor FOREIGN KEY (DoctorId) REFERENCES Doctors(PersonId)
);

CREATE TABLE Payments (
    PaymentId INT IDENTITY(1,1) PRIMARY KEY,
    AppointmentId INT NOT NULL,
    Amount DECIMAL(10, 2) NOT NULL,
    PaymentDate DATETIME NOT NULL,
    PaymentMethod VARCHAR(50), -- e.g., cash, card, insurance
    Status VARCHAR(50), -- e.g., Paid, Pending, Failed
    Notes TEXT,
    CONSTRAINT FK_Payments_Appointment FOREIGN KEY (AppointmentId) REFERENCES Appointments(AppointmentId)
);

CREATE INDEX IX_Persons_Surname ON Persons(Surname);
-- Index for searching doctors by specialty
CREATE INDEX IX_Patients_PersonId ON Patients(PersonId);
CREATE INDEX IX_Doctors_Specialty ON Doctors(Specialty);
-- Indexes for filtering appointments
CREATE INDEX IX_Appointments_PatientId ON Appointments(PatientId);
CREATE INDEX IX_Appointments_DoctorId ON Appointments(DoctorId);
CREATE INDEX IX_Appointments_Date ON Appointments(AppointmentDateTime);
-- Indexes for diagnosis queries
CREATE INDEX IX_Diagnoses_PatientId ON Diagnoses(PatientId);
CREATE INDEX IX_Diagnoses_DoctorId ON Diagnoses(DoctorId);
CREATE INDEX IX_Diagnoses_Date ON Diagnoses(DiagnosisDate);
-- Index for reporting by payment date
CREATE INDEX IX_Payments_AppointmentId ON Payments(AppointmentId);
CREATE INDEX IX_Payments_PaymentDate ON Payments(PaymentDate);