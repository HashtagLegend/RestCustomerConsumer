﻿USE CustomerDB
GO

CREATE TABLE Customer
(
    id INT IDENTITY(100,1) PRIMARY KEY,
	firstname VARCHAR(50) NOT NULL,
	lastname VARCHAR(50) NOT NULL,
	year INT NOT NULL

);


