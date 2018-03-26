CREATE DATABASE PSIQ
USE PSIQ

CREATE TABLE TERAPEUTA
(
CRP 	VARCHAR(50) PRIMARY KEY;
IDADE 	INTEGER,
FOTO 	VARCHAR(MAX),
NOME 	VARCHAR (MAX),
DTADASCIMENTO DATETIME
)

CREATE TABLE PACIENTE
(
COD 		VARCHAR(50) PRIMARY KEY;
IDADE 		INTEGER,
FOTO 		VARCHAR(MAX),
NOME 		VARCHAR (MAX),
DESCRICAO 	VARCHAR(MAX),
DTADASCIMENTO 	DATETIME,
COD 		INTEGER NULL REFERENCES PACIENTE,
COD 		INTEGER REFERENCES ESTADO
)

CREATE TABLE PACIENTEXDIAGNOSTICO
(
COD_D		INTEGER		REFERENCES	DIAGNOSTICO,
CPF		VARCHAR(14) REFERENCES	PACIENTE2,

CONSTRAINT 	PK_DP PRIMARY KEY (COD_D, CPF)
)

CREATE TABLE DIAGNOSTICO
(
COD 		INTEGER PRIMARY KEY,
NOME 		VARCHAR(MAX),
DESCRICAO 	VARCHAR(MAX)
)

CREATE TABLE ESTADO
(
COD 	INTEGER PRIMARY KEY,
NOME 	VARCHAR(MAX)
)

CREATE TABLE POST
(
COD 		INTEGER PRIMARY KEY,
CONTEUDO 	VARCHAR(MAX),
DATAHR 		DATETIME,
COD 		INTEGER REFERENCES EDICAO
)

CREATE TABLE EDICAO
(
COD 		INTEGER PRIMARY KEY,
CONTEUDO 	VARCHAR(MAX),
DATAHR 		DATETIME
)

CREATE TABLE COMENTARIO
(
COD 		INTEGER PRIMARY KEY,
CONTEUDO 	VARCHAR(MAX),
DATAHR 		DATETIME
COD 		INTEGER REFERENCES POST
)

CREATE TABLE VISUALIZACAO
(
COD 		INTEGER PRIMARY KEY,
DATAHR 		DATETIME
COD 		INTEGER NOT NULL REFERENCES POST
)







