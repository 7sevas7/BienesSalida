-- BASE SISTEMA DE VIENES--

create database SistemaBien
use SistemaBien

create table Usuarios(
idUserSB int IDENTITY(1, 1),
idUserEU int,
Nombre varchar(255) NOT NULL,
Roll varchar(255) NOT NULL,
PRIMARY KEY (idUserSB));

create table Salida(
idSal int IDENTITY(1, 1),
idUserEU int NOT NULL,
fechaHora varchar(255) NOT NULL,
nombre varchar(255) NOT NULL,
noSal int NOT NULL,
noInven int NOT NULL,
descrip varchar(255) NOT NULL,
motivo varchar(255) NOT NULL,
observa varchar(255) NOT NULL,
area varchar(255) NOT NULL,
eArea varchar(255) NOT NULL,
estatus varchar(255) NOT NULL,

PRIMARY KEY (idSal ));

select * from Usuarios;
select * from Salida;

-- *************** PA USUARIOS *********************--

GO
CREATE PROCEDURE InsercLogin
    @ID_UserEU INT,
    @Nombre VARCHAR(250),
    @Roll VARCHAR(250)
AS
BEGIN
    IF NOT EXISTS (SELECT * FROM Usuarios WHERE idUserEU = @ID_UserEU AND Nombre = @Nombre)
    BEGIN
        INSERT INTO Usuarios (idUserEU, Nombre, Roll)
        VALUES (@ID_UserEU, @Nombre, @Roll);
    END; 
END;
GO

--DROP PROCEDURE InsercLogin;

-- *************** PA SALIDAS *********************--
-- INSERCCIÓN

GO
CREATE PROCEDURE InsercSalida
    @ID_UserEU int, 
	@FyH varchar(255), 
	@Nombre varchar(255), 
	@nSal int, 
	@nInv int, 
	@descrip varchar(255), 
	@moti varchar(255), 
	@obser varchar(255), 
	@area varchar(255), 
	@encArea varchar(255), 
	@estatus varchar(255)
AS
BEGIN
    IF NOT EXISTS (SELECT * FROM Salida WHERE noInven = @nInv)
    BEGIN
        INSERT INTO Salida (idUserEU,fechaHora,nombre,noSal,noInven,descrip,motivo,observa,area,eArea,estatus)
        VALUES (@ID_UserEU, @FyH, @Nombre, @nSal, @nInv, @descrip, @moti, @obser, @area, @encArea, @estatus);
    END; 
END;
GO

--CONSULTA



-- *************** BASE USUARIOS *********************--

use expediente_personas2018;

SELECT * FROM vw_acceso WHERE RFC = 'LESA480608' and pass = 'ND05'
SELECT id_empleado, nombre_completo FROM vw_acceso WHERE RFC = 'LESA480608' and pass = 'ND05'

GO
CREATE PROCEDURE ConsultaSistemaDeBienes
    @rfc VARCHAR(250),
    @pass VARCHAR(250)
AS
BEGIN
    SELECT id_empleado, nombre_completo FROM vw_acceso WHERE RFC = @rfc AND pass = @pass;
END;
GO

EXEC ConsultaSistemaDeBienes 'LESA480608', 'ND05';

--DROP PROCEDURE ConsultaSistemaDeBienes;