-- Created by Vertabelo (http://vertabelo.com)
-- Last modification date: 2023-03-14 13:30:46.387

CREATE DATABASE SchoolTransport
GO

USE SchoolTransport
GO

-- tables
-- Table: BusAssignments
CREATE TABLE BusAssignments (
    BusAssignmentId uniqueidentifier  NOT NULL,
    BusId uniqueidentifier  NOT NULL,
    RouteId uniqueidentifier  NOT NULL,
    DriverId uniqueidentifier  NOT NULL,
    AssignedDate date  NOT NULL,
    CONSTRAINT BusAssignments_pk PRIMARY KEY  (BusAssignmentId)
);

-- Table: Buses
CREATE TABLE Buses (
    BusId uniqueidentifier  NOT NULL,
    Make nvarchar(50)  NOT NULL,
    Model nvarchar(30)  NOT NULL,
    Year int  NOT NULL,
    LicensePlate nvarchar(15)  NOT NULL,
    Capacity int  NOT NULL,
    CONSTRAINT Buses_pk PRIMARY KEY  (BusId)
);

-- Table: Drivers
CREATE TABLE Drivers (
    DriverId uniqueidentifier  NOT NULL,
    FirstName nvarchar(50)  NOT NULL,
    LastName nvarchar(50)  NOT NULL,
    Phone nvarchar(20)  NOT NULL,
    Email nvarchar(80)  NOT NULL,
    Address nvarchar(100)  NOT NULL,
    CONSTRAINT Drivers_pk PRIMARY KEY  (DriverId)
);

-- Table: ParentGuardians
CREATE TABLE ParentGuardians (
    ParentGuardianId uniqueidentifier  NOT NULL,
    FirstName nvarchar(50)  NOT NULL,
    LastName nvarchar(50)  NOT NULL,
    Phone nvarchar(20)  NOT NULL,
    Email nvarchar(80)  NOT NULL,
    Address nvarchar(100)  NOT NULL,
    CONSTRAINT ParentGuardians_pk PRIMARY KEY  (ParentGuardianId)
);

-- Table: Routes
CREATE TABLE Routes (
    RouteId uniqueidentifier  NOT NULL,
    Name nvarchar(30)  NOT NULL,
    Description nvarchar(300)  NOT NULL,
    Distance decimal(4,2)  NOT NULL,
    CONSTRAINT Routes_pk PRIMARY KEY  (RouteId)
);

-- Table: StopAssignments
CREATE TABLE StopAssignments (
    StopAssignmentId uniqueidentifier  NOT NULL,
    BusAssignmentId uniqueidentifier  NOT NULL,
    StopId uniqueidentifier  NOT NULL,
    ArrivalTime date  NOT NULL,
    DepartureTime date  NOT NULL,
    CONSTRAINT StopAssignments_pk PRIMARY KEY  (StopAssignmentId)
);

-- Table: Stops
CREATE TABLE Stops (
    StopId uniqueidentifier  NOT NULL,
    Name nvarchar(30)  NOT NULL,
    Description nvarchar(300)  NOT NULL,
    Time date  NOT NULL,
    CONSTRAINT Stops_pk PRIMARY KEY  (StopId)
);

-- Table: Students
CREATE TABLE Students (
    StudentId uniqueidentifier  NOT NULL,
    FirstName nvarchar(50)  NOT NULL,
    LastName nvarchar(50)  NOT NULL,
    GradeLevel nvarchar(30)  NOT NULL,
    Phone nvarchar(20)  NOT NULL,
    Email nvarchar(80)  NOT NULL,
    Address nvarchar(100)  NOT NULL,
    ParentGuardianId uniqueidentifier  NOT NULL,
    CONSTRAINT Students_pk PRIMARY KEY  (StudentId)
);

-- Table: StudentsBusAssignments
CREATE TABLE StudentsBusAssignments (
    StudentId uniqueidentifier  NOT NULL,
    BusAssignmentId uniqueidentifier  NOT NULL,
    CONSTRAINT StudentsBusAssignments_pk PRIMARY KEY  (StudentId,BusAssignmentId)
);

-- foreign keys
-- Reference: BusAssignments_Buses (table: BusAssignments)
ALTER TABLE BusAssignments ADD CONSTRAINT BusAssignments_Buses
    FOREIGN KEY (BusId)
    REFERENCES Buses (BusId);

-- Reference: BusAssignments_Drivers (table: BusAssignments)
ALTER TABLE BusAssignments ADD CONSTRAINT BusAssignments_Drivers
    FOREIGN KEY (DriverId)
    REFERENCES Drivers (DriverId);

-- Reference: BusAssignments_Routes (table: BusAssignments)
ALTER TABLE BusAssignments ADD CONSTRAINT BusAssignments_Routes
    FOREIGN KEY (RouteId)
    REFERENCES Routes (RouteId);

-- Reference: StopAssignments_BusAssignments (table: StopAssignments)
ALTER TABLE StopAssignments ADD CONSTRAINT StopAssignments_BusAssignments
    FOREIGN KEY (BusAssignmentId)
    REFERENCES BusAssignments (BusAssignmentId);

-- Reference: Stops_StopAssignments (table: StopAssignments)
ALTER TABLE StopAssignments ADD CONSTRAINT Stops_StopAssignments
    FOREIGN KEY (StopId)
    REFERENCES Stops (StopId);

-- Reference: StudentsBusAssignments_BusAssignments (table: StudentsBusAssignments)
ALTER TABLE StudentsBusAssignments ADD CONSTRAINT StudentsBusAssignments_BusAssignments
    FOREIGN KEY (BusAssignmentId)
    REFERENCES BusAssignments (BusAssignmentId);

-- Reference: StudentsBusAssignments_Students (table: StudentsBusAssignments)
ALTER TABLE StudentsBusAssignments ADD CONSTRAINT StudentsBusAssignments_Students
    FOREIGN KEY (StudentId)
    REFERENCES Students (StudentId);

-- Reference: Students_ParentGuardians (table: Students)
ALTER TABLE Students ADD CONSTRAINT Students_ParentGuardians
    FOREIGN KEY (ParentGuardianId)
    REFERENCES ParentGuardians (ParentGuardianId);

-- End of file.

