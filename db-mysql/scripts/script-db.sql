use mobile_development;

CREATE TABLE Books
( Id VARCHAR(100) NOT NULL,
  Autor VARCHAR(100) NOT NULL,
  Titulo VARCHAR(250),
  Ano INT,
  CONSTRAINT book_pk PRIMARY KEY (Id)
);

CREATE TABLE Accounts
( Id VARCHAR(100) NOT NULL,
  Username VARCHAR(30) NOT NULL UNIQUE,
  Name VARCHAR(250),
  Password VARCHAR(50),
  CONSTRAINT account_pk PRIMARY KEY (Id)
);

/*
To start MySQL server: sudo service mysqld start
To stop MySQL server: sudo service mysqld stop
To restart MySQL server: sudo service mysqld restart
*/