USE master;

IF NOT EXISTS (SELECT name 
               FROM sys.databases 
               WHERE name = 'sbdb')
BEGIN
    CREATE DATABASE sbdb;
    PRINT 'Database sbdb has been created.';
END

USE sbdb;
GO

DROP TABLE IF EXISTS Reservation, Property, Person, Type, Region;
GO

CREATE TABLE Type (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    type_name CHAR(50) NOT NULL,
    area CHAR(50) NOT NULL,
    beds CHAR(50) NOT NULL,
    quality_level CHAR(50) NOT NULL
);

CREATE TABLE Region (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    region_name CHAR(50) NOT NULL
);

CREATE TABLE Person (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    full_name CHAR(50) NOT NULL
);

CREATE TABLE Property (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    address CHAR(50) NOT NULL,
    fk_type INT NOT NULL,
    fk_owner INT NOT NULL,
    fk_region INT NOT NULL,
    FOREIGN KEY (fk_type) REFERENCES Type(Id),
    FOREIGN KEY (fk_owner) REFERENCES Person(Id),
    FOREIGN KEY (fk_region) REFERENCES Region(Id)
);

CREATE TABLE Reservation (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    customer CHAR(50) NOT NULL,
    start_date DATE NOT NULL,
    end_date DATE NOT NULL,
    fk_property INT NOT NULL,
    FOREIGN KEY (fk_property) REFERENCES Property(Id) ON DELETE CASCADE
	);