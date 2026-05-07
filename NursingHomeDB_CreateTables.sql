USE NursingHomeDB;

-- Drop tables in correct order
DROP TABLE IF EXISTS EMEGENCY_ALERT;
DROP TABLE IF EXISTS RES_TO_NURSE;
DROP TABLE IF EXISTS MED_TO_RES;
DROP TABLE IF EXISTS ORDER_MED;
DROP TABLE IF EXISTS MED_STOCK;
DROP TABLE IF EXISTS RESIDENT;
DROP TABLE IF EXISTS NURSE;

-- Create tables
CREATE TABLE RESIDENT (
    ResID INT PRIMARY KEY,
    ID_Number VARCHAR(20),
    Surname VARCHAR(50),
    Name VARCHAR(50),
    Room VARCHAR(20),
    UserName VARCHAR(50),
    Login VARCHAR(50)
);

CREATE TABLE NURSE (
    NurseID INT PRIMARY KEY,
    ID_Number VARCHAR(20),
    Surname VARCHAR(50),
    Name VARCHAR(50),
    UserName VARCHAR(50),
    Login VARCHAR(50)
);

CREATE TABLE MED_STOCK (
    MedID INT PRIMARY KEY,
    Name VARCHAR(100),
    Stock INT
);

CREATE TABLE ORDER_MED (
    MedBatch INT PRIMARY KEY,
    MedID INT,
    Stock INT,
    Expiry_date DATE,
    FOREIGN KEY (MedID) REFERENCES MED_STOCK(MedID)
);

CREATE TABLE MED_TO_RES (
    ResID INT,
    MedID INT,
    Dosage VARCHAR(50),
    Time TIME,
    MedBatch INT,
    PRIMARY KEY (ResID, MedID),
    FOREIGN KEY (ResID) REFERENCES RESIDENT(ResID),
    FOREIGN KEY (MedID) REFERENCES MED_STOCK(MedID),
    FOREIGN KEY (MedBatch) REFERENCES ORDER_MED(MedBatch)
);

CREATE TABLE RES_TO_NURSE (
    NurseID INT,
    ResID INT,
    PRIMARY KEY (NurseID, ResID),
    FOREIGN KEY (NurseID) REFERENCES NURSE(NurseID),
    FOREIGN KEY (ResID) REFERENCES RESIDENT(ResID)
);

CREATE TABLE EMEGENCY_ALERT (
    AlertID INT PRIMARY KEY,
    ResID INT,
    NurseID INT,
    Date_of_Alert DATE,
    Time_of_Alert TIME,
    Description TEXT,  
    Response_Time TIME,
    FOREIGN KEY (ResID) REFERENCES RESIDENT(ResID),
    FOREIGN KEY (NurseID) REFERENCES NURSE(NurseID)
);

-- Insert test data
INSERT INTO RESIDENT (ResID, ID_Number, Surname, Name, Room, UserName, Login) VALUES
(1, '9001015009087', 'Smith', 'John', 'A101', 'jsmith', 'pass123'),
(2, '8503126009082', 'Mokoena', 'Thandi', 'A102', 'tmokoena', 'pass456'),
(3, '7805194009084', 'Naidoo', 'Rajesh', 'B201', 'rnaidoo', 'pass789');


INSERT INTO NURSE (NurseID, ID_Number, Surname, Name, UserName, Login) VALUES
(1, '8102047009085', 'Brown', 'Alice', 'abrown', 'nurse123'),
(2, '8807158009083', 'Khumalo', 'Sipho', 'skhumalo', 'nurse456');

INSERT INTO MED_STOCK (MedID, Name, Stock) VALUES
(1, 'Paracetamol 500mg', 200),
(2, 'Amoxicillin 250mg', 150),
(3, 'Insulin', 100);

INSERT INTO ORDER_MED (MedBatch, MedID, Stock, Expiry_date) VALUES
(101, 1, 50, '2026-01-15'),
(102, 2, 40, '2025-12-01'),
(103, 3, 30, '2025-08-31');

INSERT INTO MED_TO_RES (ResID, MedID, Dosage, Time, MedBatch) VALUES
(1, 1, '1 Tablet', '08:00:00', 101),
(1, 3, '10 Units', '07:30:00', 103),
(2, 2, '1 Capsule', '12:00:00', 102),
(3, 1, '2 Tablets', '20:00:00', 101);

INSERT INTO RES_TO_NURSE (NurseID, ResID) VALUES
(1, 1),
(1, 2),
(2, 3);

INSERT INTO EMEGENCY_ALERT (AlertID, ResID, NurseID, Date_of_Alert, Time_of_Alert, Description, Response_Time) VALUES
(1, 1, 1, '2025-08-20', '02:15:00', 'Resident fell in bathroom', '02:20:00'),
(2, 2, 1, '2025-08-21', '04:30:00', 'Difficulty breathing', '04:40:00'),
(3, 3, 2, '2025-08-22', '22:00:00', 'Low blood sugar attack', '22:05:00');
