--Создание БД 
   CREATE DATABASE TestDB
   ON  PRIMARY 
   (
         NAME = N'TestDB', --Логическое имя файла БД
         FILENAME = N'D:\DataBases\TestDB.mdf' --Имя и местоположение файла БД
   )
   LOG ON 
   (
        NAME = N'TestDB_log', --Логическое имя файла журнала
        FILENAME = N'D:\DataBases\TestDB_log.ldf' --Имя и местоположение файла журнала
   )
   GO
   
   -- Создание таблиц
 USE TestDB
 GO  
   --ученики
CREATE TABLE Students
( [StudentsId] [int] IDENTITY (1,1) NOT NULL,
  [Name] [nvarchar](150) NOT NULL,
  [Surname] [nvarchar](150) NOT NULL,
  [Age] [int] NOT NULL,
  [ClassId] [int] NULL,
  [IsDelete] bit NOT NULL DEFAULT 0
  CONSTRAINT PK_Students Primary key NONCLUSTERED (StudentsId)
  );
  GO
  
  --классы
CREATE TABLE Classes
( [ClassId] [int] IDENTITY (1,1) NOT NULL,
  [Number] [int] NOT NULL,
  [Letter] [nvarchar](1) NULL,
  [IsDelete] bit NOT NULL DEFAULT 0
  CONSTRAINT PK_Classes Primary key NONCLUSTERED (ClassId)
  );
  GO
  
  ALTER TABLE Students  
  ADD CONSTRAINT FK_Students_Classes_ FOREIGN KEY (ClassId)  
    REFERENCES Classes (ClassId) ;  
  GO 
  
  -- добавление данных
  INSERT INTO Classes (Number, Letter)
     VALUES(1, 'А')
  GO
     
  INSERT INTO Students (Name, Surname, Age, ClassId)
     VALUES('Вася', 'Пупкин', 7, 1),
           ('Коля', 'Лосев',  8, null)
  GO
  
  SELECT * FROM Classes
  SELECT * FROM Students