use Stock;

CREATE TABLE Goods (
  ID INT PRIMARY KEY,
  Name_Goods VARCHAR(50),
  Type_Goods VARCHAR(50),
  ID_Provider INT,
  Quantity_Goods INT,
  Cost_Price MONEY,
  Date_Delivery DATE
);
CREATE TABLE Providers (
  ID INT PRIMARY KEY,
  Name_Company VARCHAR(50),
  Adres VARCHAR(100),
  Phone VARCHAR(20)
);

ALTER TABLE Goods
ADD FOREIGN KEY (ID_Provider) REFERENCES Providers(ID);


--INSERT INTO Товары (ID, Name_Goods, Type_Goods, ID_Provider, Quantity_Goods, Cost_Price, Date_Delivery)