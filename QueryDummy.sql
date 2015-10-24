INSERT INTO Funcionario
(cedula, nombre, apellido1, apellido2, email, usuario, contrasena, login)
     VALUES('000000000','Dummy', 'Dummy', 'Dummy', 'Dummy', 'Dummy', 'Dummy', 'False');

INSERT INTO Administrador
           (cedula_admin)
     VALUES
           ('000000000');

INSERT INTO Funcionario
(cedula, nombre, apellido1, apellido2, email, usuario, contrasena, login)
     VALUES('000000001','Dummy', 'Dummy', 'Dummy', 'Dummy1', 'Dummy1', 'Dummy', 'False');


INSERT INTO Miembro
           (cedula_miembro, tipo_rol)
     VALUES('000000001', 'Líder de pruebas');

INSERT INTO Oficina_Usuaria(nombre_oficina)
     VALUES('Dummy');

DECLARE @a int;
SELECT @a = (Select id_oficina From Oficina_Usuaria WHERE nombre_oficina='Dummy');

INSERT INTO dbo.Proyecto(nombre_proyecto,obj_general,fecha_asignacion,tipo_estado,cedula_creador,cedula_lider,id_oficina)
     VALUES('Dummy','Dummy', 'Dummy', 'Asignado','000000000','000000001',@a);


DECLARE @b int;
SELECT @b = (Select id_proyecto From Proyecto WHERE nombre_proyecto='Dummy');

INSERT INTO dbo.Diseno_Pruebas(proposito_diseno,fecha,procedimiento_diseno,ambiente_diseno,criterios_aceptacion,tecnica,nivel,tipo,id_proyecto
,cedula_responsable)
     VALUES('Dummy','Dummy','Dummy','Dummy','Dummy','Caja negra','Unitaria','Funcional',@b,'000000001');