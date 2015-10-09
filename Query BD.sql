/*servidor eccibdisw*/
CREATE DATABASE g3inge;

USE g3inge

CREATE TABLE Funcionario(
cedula varchar(9) PRIMARY KEY ,
nombre varchar(20),
apellido1 varchar(20),
apellido2 varchar(20),
email varchar(30),
usuario varchar(20) UNIQUE,
contrasena varchar(100),
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
tipo_rol varchar(20) PRIMARY KEY
);

INSERT INTO Rol VALUES('Líder de pruebas');
INSERT INTO Rol VALUES('Tester');
INSERT INTO Rol VALUES('Usuario');

CREATE TABLE Administrador(
cedula_admin varchar(9) PRIMARY KEY,
FOREIGN KEY (cedula_admin) REFERENCES Funcionario(cedula) 
ON DELETE CASCADE
ON UPDATE CASCADE
);

CREATE TABLE Miembro(
cedula_miembro varchar(9) PRIMARY KEY,
tipo_rol varchar(20),
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

INSERT INTO Estado_Proceso VALUES('Pendiente de asignación');
INSERT INTO Estado_Proceso VALUES('Asignado');
INSERT INTO Estado_Proceso VALUES('En ejecución');
INSERT INTO Estado_Proceso VALUES('Finalizado');

CREATE TABLE Proyecto(
id_proyecto int IDENTITY(1,1) PRIMARY KEY,
nombre_proyecto varchar(20) UNIQUE,
obj_general varchar(30),
fecha_asignacion date,
tipo_estado varchar(25) FOREIGN KEY REFERENCES Estado_Proceso(tipo_Estado)
ON DELETE CASCADE
ON UPDATE CASCADE,
cedula_creador varchar(9) FOREIGN KEY REFERENCES Administrador(cedula_admin)
ON DELETE NO ACTION /*Da problemas si es cascade*/
ON UPDATE NO ACTION, /*Da problemas si es cascade*/
cedula_lider varchar(9) FOREIGN KEY REFERENCES Miembro(cedula_miembro)
ON DELETE NO ACTION 
ON UPDATE CASCADE,
id_oficina int FOREIGN KEY REFERENCES Oficina_Usuaria(id_oficina)
ON DELETE NO ACTION
ON UPDATE CASCADE
);

CREATE TABLE Trabaja_En(
cedula_miembro varchar(9) FOREIGN KEY REFERENCES Miembro(cedula_miembro)
ON DELETE NO ACTION /*Da problemas si es cascade*/
ON UPDATE NO ACTION,/*Da problemas si es cascade*/
id_proyecto int FOREIGN KEY REFERENCES Proyecto(id_proyecto)
ON DELETE CASCADE
ON UPDATE CASCADE,
PRIMARY KEY (cedula_miembro, id_proyecto)
);

CREATE TABLE Tecnica(
tipo_tecnica varchar(20) PRIMARY KEY
);

INSERT INTO Tecnica VALUES ('Caja negra');
INSERT INTO Tecnica VALUES ( 'Caja blanca');
INSERT INTO Tecnica VALUES ('Exploratoria');

CREATE TABLE Nivel_Prueba(
nivel_prueba varchar(20) PRIMARY KEY
);

INSERT INTO Nivel_Prueba VALUES ('Unitaria');
INSERT INTO Nivel_Prueba VALUES ('De Integración');
INSERT INTO Nivel_Prueba VALUES ('Del Sistema');
INSERT INTO Nivel_Prueba VALUES ( 'De Aceptación');

CREATE TABLE Tipo_Prueba(
tipo_prueba varchar(20) PRIMARY KEY
);

INSERT INTO Tipo_Prueba VALUES ('Funcional');
INSERT INTO Tipo_Prueba VALUES ('Interfaz de Usuario');
INSERT INTO Tipo_Prueba VALUES ('Rendimiento');
INSERT INTO Tipo_Prueba VALUES ('Stress');
INSERT INTO Tipo_Prueba VALUES ('Volumen');
INSERT INTO Tipo_Prueba VALUES ('Configuración');
INSERT INTO Tipo_Prueba VALUES ('Instalación');

CREATE TABLE Diseno_Pruebas(
id_diseno varchar(20) PRIMARY KEY,
proposito_dise varchar(30),
fecha date, 
procedimiento varchar(100),
criterios_aceptacion varchar(100),
tecnica varchar(20) FOREIGN KEY REFERENCES Tecnica (tipo_tecnica)
ON DELETE CASCADE
ON UPDATE CASCADE,
nivel varchar(20) FOREIGN KEY REFERENCES Nivel_Prueba(nivel_prueba)
ON DELETE CASCADE
ON UPDATE CASCADE,
tipo varchar(20) FOREIGN KEY REFERENCES Tipo_Prueba(tipo_prueba)
ON DELETE CASCADE
ON UPDATE CASCADE,
id_proyecto int FOREIGN KEY REFERENCES Proyecto(id_proyecto)
ON DELETE CASCADE 
ON UPDATE CASCADE,
cedula_responsable varchar(9) FOREIGN KEY REFERENCES Miembro(cedula_miembro)
ON DELETE NO ACTION /*Da problemas si es cascade*/
ON UPDATE NO ACTION/*Da problemas si es cascade*/
);

CREATE TABLE Requerimiento(
id_req varchar(15),
id_proyecto int FOREIGN KEY REFERENCES Proyecto(id_proyecto)
ON DELETE CASCADE 
ON UPDATE CASCADE,
id_diseno varchar(20) FOREIGN KEY REFERENCES Diseno_Pruebas(id_diseno)
ON DELETE NO ACTION /*El requerimiento puede quedar no asociado a otro diseño*/ 
ON UPDATE NO ACTION, /*Da problemas si es cascade*/
nombre_req varchar (30),
PRIMARY KEY (id_req, id_proyecto)
);

CREATE TABLE Caso_Prueba(
id_caso varchar(20) PRIMARY KEY,
proposito_caso varchar(30),
flujo_central varchar(100),
entrada_datos varchar(100),
resultado_esperado varchar(30),
id_diseno varchar(20) FOREIGN KEY REFERENCES Diseno_Pruebas(id_diseno)
ON DELETE CASCADE 
ON UPDATE CASCADE,
id_req varchar(15),
id_proyecto int,
CONSTRAINT FkReq FOREIGN KEY (id_req, id_proyecto) REFERENCES Requerimiento(id_req, id_proyecto)
ON DELETE NO ACTION  /*Da problemas si es cascade*/
ON UPDATE NO ACTION /*Da problemas si es cascade*/
);

CREATE TABLE Estado_Ejecucion(
estado_ejecucion varchar(20) PRIMARY KEY
);

INSERT INTO Estado_Ejecucion VALUES('Satisfactoria');
INSERT INTO Estado_Ejecucion VALUES( 'Faliida');
INSERT INTO Estado_Ejecucion VALUES('Pendiente');
INSERT INTO Estado_Ejecucion VALUES('Cancelada');

CREATE TABLE Ejecucion_Prueba(
id_ejecucion int IDENTITY(1,1) PRIMARY KEY,
justificacion varchar(40),
desc_incidencia varchar(70),
fecha date,
hora time, 
imagen image,
estado_ejecucion varchar (20) FOREIGN KEY REFERENCES Estado_Ejecucion(estado_ejecucion)
ON DELETE CASCADE
ON UPDATE CASCADE, 
cedula_responsable varchar(9) FOREIGN KEY REFERENCES Miembro(cedula_miembro)
ON DELETE NO ACTION
ON UPDATE CASCADE 
);

CREATE TABLE Tipo_NC(
id_tipoNC varchar(70) PRIMARY KEY,
desc_NC varchar(150)
);

INSERT INTO Tipo_NC (id_tipoNC, desc_NC) VALUES('Funcionalidad (FN)', 'Al realizar una acción determinada el resultado que se muestra no está acorde con el esperado.');
INSERT INTO Tipo_NC (id_tipoNC, desc_NC) VALUES('Validación (VA)', 'Existen errores relativos a la falta de validación.');
INSERT INTO Tipo_NC (id_tipoNC, desc_NC) VALUES('Opciones que no funcionan (NF)', 'Al realizar una acción determinada no se muestra resultado alguno.');
INSERT INTO Tipo_NC (id_tipoNC, desc_NC) VALUES('Errores de usabilidad (US)', 'Se encuentran aquellas inconformidades de efecto visual que provoquen las interfaces de las aplicaciones.');
INSERT INTO Tipo_NC (id_tipoNC, desc_NC) VALUES('Excepciones (EXC)', 'El sistema muestra un mensaje señalando que ha ocurrido un error inesperado o que no ha sido tratado.');
INSERT INTO Tipo_NC (id_tipoNC, desc_NC) VALUES('No correspondencia de lo implementado con lo documentado (NC)', 'Consiste en el incumplimiento de la correspondencia que debe existir entre una aplicación informática y lo que está documentado al respecto.');
INSERT INTO Tipo_NC (id_tipoNC, desc_NC) VALUES('Ortografía (ORT)', 'Errores de ortografía o mal escritos.');

CREATE TABLE Ejecucion_NC(
id_tipoNC varchar(70) FOREIGN KEY REFERENCES Tipo_NC(id_tipoNC)
ON DELETE CASCADE
ON UPDATE CASCADE,
id_ejecucion int FOREIGN KEY REFERENCES Ejecucion_Prueba(id_ejecucion)
ON DELETE CASCADE
ON UPDATE CASCADE,
PRIMARY KEY (id_tipoNC, id_ejecucion)
);