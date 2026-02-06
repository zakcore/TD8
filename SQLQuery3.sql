use GestionScolarite;
INSERT INTO Etudiants (Nom, Prenom) VALUES ('Tremblay', 'Marie');
INSERT INTO Etudiants (Nom, Prenom) VALUES ('Dupont', 'Jean');

INSERT INTO Cours (Titre, Code) VALUES ('Programmation 1', 'INF1001');
INSERT INTO Cours (Titre, Code) VALUES ('Bases de données', 'INF2001');

INSERT INTO Inscriptions (EtudiantId, CoursId, Session, Note) VALUES (1, 1, 'H25', 85);
INSERT INTO Inscriptions (EtudiantId, CoursId, Session, Note) VALUES (1, 2, 'H25', NULL);
INSERT INTO Inscriptions (EtudiantId, CoursId, Session, Note) VALUES (2, 1, 'H25', 92);