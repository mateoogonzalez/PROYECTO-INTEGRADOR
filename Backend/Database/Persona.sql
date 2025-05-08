create table Persona (
    IdPersona INT PRIMARY KEY,
    IdGrado INT,
    IdArmEsp INT,
    Nombre VARCHAR (100),
    SegundoNombre VARCHAR (100),
    Apellido VARCHAR (100),
    TipoDni VARCHAR (20),
    NroDni VARCHAR (20),
    FOREIGN KEY (IdGrado) REFERENCES Grado (IdGrado),
    FOREIGN KEY (IdArmEsp) REFERENCES ArmEsp (IdArmEsp)
);