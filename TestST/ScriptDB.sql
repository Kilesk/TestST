--�������� �� 
   CREATE DATABASE TestDB
   ON  PRIMARY 
   (
         NAME = N'TestDB', --���������� ��� ����� ��
         FILENAME = N'D:\DataBases\TestDB.mdf' --��� � �������������� ����� ��
   )
   LOG ON 
   (
        NAME = N'TestDB_log', --���������� ��� ����� �������
        FILENAME = N'D:\DataBases\TestDB_log.ldf' --��� � �������������� ����� �������
   )
   GO
   
   -- �������� ������
 USE TestDB
 GO  
   --�������
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
  
  --������
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
  
  -- ���������� ������
  INSERT INTO Classes (Number, Letter)
     VALUES(1, '�')
  GO
     
  INSERT INTO Students (Name, Surname, Age, ClassId)
     VALUES('����', '������', 7, 1),
           ('����', '�����',  8, null)
  GO
  
  SELECT * FROM Classes
  SELECT * FROM Students