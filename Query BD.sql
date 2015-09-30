/*servidor eccibdisw*/

USE g3inge

CREATE TABLE Funcionario(
cedula varchar(9) PRIMARY KEY ,
nombre varchar(20),
apellido1 varchar(20),
apellido2 varchar(20),
usuario varchar(20),
contrasena varchar(30)
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

CREATE TABLE Estado_Proceso(
tipo_estado varchar(25) PRIMARY KEY
);

INSERT INTO Estado_Proceso VALUES('Pendiente de asignación', 'Asignado', 'En ejecución', 'Finalizado', 'Cerrado');

CREATE TABLE Oficina_Usuaria(
id_oficina int IDENTITY(1,1) PRIMARY KEY,
nombre_oficina varchar(20) UNIQUE,

);

