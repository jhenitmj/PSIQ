CREATE DATABASE PSIQ
GO

USE PSIQ
GO

CREATE TABLE DIAGNOSTICO
(
	COD		INTEGER PRIMARY KEY IDENTITY(1,1),
	NOME	VARCHAR(1000) NOT NULL
);

CREATE TABLE ESTADO
(
	COD 	INTEGER PRIMARY KEY,
	NOME 	VARCHAR(1000) NOT NULL
);

INSERT INTO ESTADO VALUES (1, 'INST�VEL'), (2, 'EST�VEL');
GO

CREATE TABLE USUARIO
(
	COD 		      INTEGER PRIMARY KEY IDENTITY(1,1),
	TIPO		      INTEGER NOT NULL,
	NOME 		      VARCHAR(300) NOT NULL,
	EMAIL 		  	  VARCHAR(500) NOT NULL,
	SENHA         	  VARCHAR(100) NOT NULL,
	CPF 		  	  VARCHAR(50),
	CRP 		  	  VARCHAR(50),
	DATA_NASCIMENTO   DATETIME,
	FOTO 		      VARCHAR(1000),
	DESCRICAO 	      VARCHAR(MAX),
	COD_TERAPEUTA     INTEGER REFERENCES USUARIO (COD),
	COD_ESTADO	      INTEGER REFERENCES ESTADO (COD)
);

INSERT INTO USUARIO (TIPO, NOME, EMAIL, SENHA, DATA_NASCIMENTO) VALUES (1, 'ADMIN', 'admin@psiq.com.br', '123', GETDATE());
GO

CREATE TABLE PACIENTE_X_DIAGNOSTICO
(
	COD 		    INTEGER PRIMARY KEY IDENTITY(1,1),
	COD_PACIENTE	INTEGER	NOT NULL REFERENCES	USUARIO (COD),
	COD_DIAGNOSTICO	INTEGER	NOT NULL REFERENCES	DIAGNOSTICO (COD),
	DATA_HORA       DATETIME NOT NULL DEFAULT GETDATE(),
	DESCRICAO	    VARCHAR(MAX)
);

CREATE TABLE POST
(
	COD 		   INTEGER PRIMARY KEY IDENTITY(1,1),
	COD_PACIENTE   INTEGER REFERENCES USUARIO (COD),
	COD_USUARIO    INTEGER REFERENCES USUARIO (COD),
	DATA_HORA      DATETIME NOT NULL DEFAULT GETDATE(),
	MENSAGEM	   VARCHAR(MAX)
);

SELECT *from USUARIO       
                               
--SELECT * FROM USUARIO WHERE EMAIL = 'jhe' AND SENHA = '12333';

UPDATE USUARIO SET TIPO = CASE WHEN CPF IS NULL THEN 1 ELSE 2 END;

--BULK INSERT DIAGNOSTICO
--FROM 'C:\Users\Aluno\Documents\GitHub\PSIQ\INSERT_DIAG.txt'
--WITH( CODEPAGE='ACP')
