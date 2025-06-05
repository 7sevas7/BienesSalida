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
idUserSBFK int NOT NULL,
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

PRIMARY KEY (idSal ),
FOREIGN KEY (idUserSBFK) REFERENCES Usuarios(idUserSB));

select * from Usuarios;
select * from Salida;
