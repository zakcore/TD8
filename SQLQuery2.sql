CREATE DATABASE GestionScolarite;
GO

USE GestionScolarite;
GO

CREATE TABLE Etudiants (
    Id INT PRIMARY KEY IDENTITY,
    Nom NVARCHAR(50),
    Prenom NVARCHAR(50)
);

CREATE TABLE Cours (
    Id INT PRIMARY KEY IDENTITY,
    Titre NVARCHAR(100),
    Code NVARCHAR(10)
);

CREATE TABLE Inscriptions (
    EtudiantId INT,
    CoursId INT,
    Session NVARCHAR(10),
    Note INT,
    PRIMARY KEY (EtudiantId, CoursId, Session),
    FOREIGN KEY (EtudiantId) REFERENCES Etudiants(Id),
    FOREIGN KEY (CoursId) REFERENCES Cours(Id)
);