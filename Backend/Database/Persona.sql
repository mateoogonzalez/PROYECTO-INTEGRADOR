CREATE TABLE Persona (
    id_persona INT PRIMARY KEY,
    id_grado INT,
    id_armesp INT,
    nombre VARCHAR (100),
    apellido VARCHAR (100),
    nro_dni VARCHAR (20),
    FOREIGN KEY (id_grado) REFERENCES grado (id_grado),
    FOREIGN KEY (id_armesp) REFERENCES armesp (id_armesp)
);  