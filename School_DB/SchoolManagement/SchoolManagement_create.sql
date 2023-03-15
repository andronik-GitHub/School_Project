-- Created by Vertabelo (http://vertabelo.com)
-- Last modification date: 2023-03-14 12:48:05.105

CREATE DATABASE SchoolManagement
GO

USE SchoolManagement
GO

-- tables
-- Table: Assignments
CREATE TABLE Assignments (
    AssignmentId uniqueidentifier  NOT NULL,
    Name nvarchar(200)  NOT NULL,
    DeliveryTime date  NOT NULL,
    MaxScore int  NOT NULL,
    CONSTRAINT Assignments_pk PRIMARY KEY  (AssignmentId)
);

-- Table: Attendance
CREATE TABLE Attendance (
    AttendanceId uniqueidentifier  NOT NULL,
    EnrollmentId uniqueidentifier  NOT NULL,
    Date date  NOT NULL,
    Status bit  NOT NULL,
    CONSTRAINT Attendance_pk PRIMARY KEY  (AttendanceId)
);

-- Table: Courses
CREATE TABLE Courses (
    CourseId uniqueidentifier  NOT NULL,
    Name nvarchar(50)  NOT NULL,
    TeacherId uniqueidentifier  NOT NULL,
    CONSTRAINT Courses_pk PRIMARY KEY  (CourseId)
);

-- Table: Enrollments
CREATE TABLE Enrollments (
    EnrollmentId uniqueidentifier  NOT NULL,
    StudentId uniqueidentifier  NOT NULL,
    CourseId uniqueidentifier  NOT NULL,
    EnrollmentDate date  NOT NULL,
    CONSTRAINT Enrollments_pk PRIMARY KEY  (EnrollmentId)
);

-- Table: Schedule
CREATE TABLE Schedule (
    ScheduleId uniqueidentifier  NOT NULL,
    CourseId uniqueidentifier  NOT NULL,
    DayOfWeek nvarchar(15)  NOT NULL,
    StartTime date  NOT NULL,
    EndTime date  NOT NULL,
    Room int  NOT NULL,
    CONSTRAINT Schedule_pk PRIMARY KEY  (ScheduleId)
);

-- Table: Scores
CREATE TABLE Scores (
    ScoreId uniqueidentifier  NOT NULL,
    EnrollmentId uniqueidentifier  NOT NULL,
    AssignmentId uniqueidentifier  NOT NULL,
    Score int  NOT NULL,
    CONSTRAINT Scores_pk PRIMARY KEY  (ScoreId)
);

-- Table: Students
CREATE TABLE Students (
    StudentId uniqueidentifier  NOT NULL,
    FirstName nvarchar(50)  NOT NULL,
    LastName nvarchar(50)  NOT NULL,
    GradeLevel nvarchar(20)  NOT NULL,
    Gender nvarchar(10)  NOT NULL,
    BirthDate date  NOT NULL,
    Address nvarchar(100)  NOT NULL,
    Phone nvarchar(20)  NOT NULL,
    CONSTRAINT Students_pk PRIMARY KEY  (StudentId)
);

-- Table: Teachers
CREATE TABLE Teachers (
    TeacherId uniqueidentifier  NOT NULL,
    FirstName nvarchar(50)  NOT NULL,
    LastName nvarchar(50)  NOT NULL,
    Gender nvarchar(10)  NOT NULL,
    BirthDate date  NOT NULL,
    Address nvarchar(100)  NOT NULL,
    Phone nvarchar(20)  NOT NULL,
    CONSTRAINT Teachers_pk PRIMARY KEY  (TeacherId)
);

-- foreign keys
-- Reference: Attendance_Enrollments (table: Attendance)
ALTER TABLE Attendance ADD CONSTRAINT Attendance_Enrollments
    FOREIGN KEY (EnrollmentId)
    REFERENCES Enrollments (EnrollmentId);

-- Reference: Courses_Teachers (table: Courses)
ALTER TABLE Courses ADD CONSTRAINT Courses_Teachers
    FOREIGN KEY (TeacherId)
    REFERENCES Teachers (TeacherId);

-- Reference: Enrollments_Courses (table: Enrollments)
ALTER TABLE Enrollments ADD CONSTRAINT Enrollments_Courses
    FOREIGN KEY (CourseId)
    REFERENCES Courses (CourseId);

-- Reference: Enrollments_Students (table: Enrollments)
ALTER TABLE Enrollments ADD CONSTRAINT Enrollments_Students
    FOREIGN KEY (StudentId)
    REFERENCES Students (StudentId);

-- Reference: Schedule_Courses (table: Schedule)
ALTER TABLE Schedule ADD CONSTRAINT Schedule_Courses
    FOREIGN KEY (CourseId)
    REFERENCES Courses (CourseId);

-- Reference: Scores_Assignments (table: Scores)
ALTER TABLE Scores ADD CONSTRAINT Scores_Assignments
    FOREIGN KEY (AssignmentId)
    REFERENCES Assignments (AssignmentId);

-- Reference: Scores_Enrollments (table: Scores)
ALTER TABLE Scores ADD CONSTRAINT Scores_Enrollments
    FOREIGN KEY (EnrollmentId)
    REFERENCES Enrollments (EnrollmentId);

-- End of file.

