USE TSQL2012;

--1.1
INSERT INTO	dbo.Customers(custid, companyname, country, region, city)
VALUES (100, N'Рога и копыта', N'США', N'WA', N'Редмонд');

--1.2
INSERT INTO dbo.Customers(custid, companyname, country, region, city)
SELECT c.custid, c.companyname, c.country, c.region, c.city
FROM Sales.Customers AS C
WHERE EXISTS
	(SELECT * 
	 FROM Sales.Orders AS O
	 WHERE O.custid = C.custid);

--1.3
IF OBJECT_ID('dbo.Orders', 'U') IS NOT NULL DROP TABLE dbo.Orders;
SELECT o.orderid, o.custid, o.empid, o.orderdate, o.requireddate, o.shippeddate,
	o.shipperid, o.freight, o.shipname, o.shipcity, o.shipregion, o.shippostalcode, 
	o.shipcountry
INTO dbo.Orders
FROM Sales.Orders AS o
WHERE YEAR(o.orderdate) >= 2006 
	AND YEAR(o.orderdate) < 2008;

--2
DELETE FROM dbo.Orders
	OUTPUT
		deleted.orderid,
		deleted.orderdate
WHERE orderdate < '20060801';

--3
DELETE FROM dbo.Orders
WHERE EXISTS
	(SELECT *
	 FROM dbo.Customers AS c
	 WHERE c.custid = Orders.custid
		AND	c.country = N'Brazil');

--4
UPDATE dbo.Customers
	SET region = '<None>'
	OUTPUT
		inserted.custid,
		deleted.region AS oldregion,
		inserted.region AS newregion
WHERE region IS NULL;

--5
UPDATE O
	SET O.shipcountry = C.country,
		O.shipregion = C.region,
		O.shipcity = C.city
FROM dbo.Orders AS O
	JOIN dbo.Customers AS C
		ON C.custid = O.custid
WHERE C.country = 'UK';

