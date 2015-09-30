/*servidor eccibdisw*/

USE g3inge

CREATE TABLE Funcionario(
cedula varchar(9) PRIMARY KEY ,
nombre varchar(20),
apellido1 varchar(20),
apellido2 varchar(20),
usuario varchar(20),
contrasena varchar(30),
login bit 
);

CREATE TABLE Telefono_Funcionario(
cedula_funcionario varchar(9),
num_telefono varchar(11),
PRIMARY KEY (cedula_funcionario, num_telefono),
FOREIGN KEY (cedula_funcionario) REFERENCES Funcionario(cedula) 
ON DELETE CASCADE
ON UPDATE CASCADE
);

CREATE TABLE Rol(
tipo_rol varchar(15) PRIMARY KEY
);

INSERT INTO Rol VALUES('Lìder de pruebas', 'Tester', 'Usuario');

CREATE TABLE Administrador(
cedula_admin varchar(9) PRIMARY KEY,
FOREIGN KEY (cedula_admin) REFERENCES Funcionario(cedula) 
ON DELETE CASCADE
ON UPDATE CASCADE
);

CREATE TABLE Miembro(
cedula_miembro varchar(9) PRIMARY KEY,
tipo_rol varchar(15),
FOREIGN KEY (cedula_miembro) REFERENCES Funcionario(cedula)
ON DELETE CASCADE
ON UPDATE CASCADE,
FOREIGN KEY (tipo_rol) REFERENCES Rol(tipo_rol)
ON DELETE CASCADE
ON UPDATE CASCADE
);


CREATE TABLE Oficina_Usuaria(
id_oficina int IDENTITY(1,1) PRIMARY KEY,
nombre_oficina varchar(20) UNIQUE,
nombre_rep varchar(20),
ape1_rep varchar(20),
ape2_rep varchar(20)
);

CREATE TABLE Telefono_Oficina(
id_oficina int,
num_telefono varchar(11),
PRIMARY KEY (id_oficina, num_telefono),
FOREIGN KEY (id_oficina) REFERENCES Oficina_Usuaria(id_oficina)
ON DELETE CASCADE
ON UPDATE CASCADE
);

CREATE TABLE Estado_Proceso(
tipo_estado varchar(25) PRIMARY KEY
);

INSERT INTO Estado_Proceso VALUES('Pendiente de asignación', 'Asignado', 'En ejecución', 'Finalizado', 'Cerrado');

CREATE TABLE Proyecto(
id_proyecto int IDENTITY(1,1) PRIMARY KEY,
nombre_proyecto varchar(20) UNIQUE,
obj_general varchar(30),
fecha_asignacion date,
tipo_estado varchar(25) FOREIGN KEY REFERENCES Estado_Proceso(tipo_Estado)
ON DELETE CASCADE
ON UPDATE CASCADE,
cedula_creador varchar(9) FOREIGN KEY REFERENCES Administrador(cedula_admin)
ON DELETE CASCADE
ON UPDATE CASCADE,
cedula_lider varchar(9) FOREIGN KEY REFERENCES Miembro(cedula_miembro)
ON DELETE CASCADE
ON UPDATE CASCADE,
id_oficina int FOREIGN KEY REFERENCES Oficina_Usuaria(id_oficina)
ON DELETE CASCADE
ON UPDATE CASCADE
);

CREATE TABLE Trabaja_En(
cedula_miembro varchar(9) FOREIGN KEY REFERENCES Miembro(cedula_miembro)
ON DELETE CASCADE
ON UPDATE CASCADE,
id_proyecto int FOREIGN KEY REFERENCES Proyecto(id_proyecto)
ON DELETE CASCADE
ON UPDATE CASCADE,
PRIMARY KEY (cedula_miembro, id_proyecto)
);

CREATE TABLE Tecnica(
tipo_tecnica varchar(15) PRIMARY KEY
);

INSERT INTO Tecnica VALUES ('Caja negra', 'Caja blanca', 'Exploratoria');