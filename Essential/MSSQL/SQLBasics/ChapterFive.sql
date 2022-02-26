USE TSQL2012;

--1.1.
SELECT o.empid, MAX(o.orderdate) AS maxorderdate
FROM Sales.Orders AS o
GROUP BY o.empid;

--1.2.
SELECT maxorderdates.empid, maxorderdates.maxorderdate, o.orderid, o.custid
FROM Sales.Orders AS o
	JOIN 
		(SELECT o.empid, MAX(o.orderdate) AS maxorderdate
		 FROM Sales.Orders AS o
		 GROUP BY o.empid) AS maxorderdates
	ON o.empid = maxorderdates.empid
		AND o.orderdate = maxorderdate

--2.1.
WITH Tbl AS
(
	SELECT ord.orderid, ord.orderdate, ord.empid,
		ROW_NUMBER() OVER(ORDER BY ord.orderdate, ord.orderid) AS rownum
	FROM Sales.Orders AS ord
)
SELECT *
FROM Tbl
ORDER BY Tbl.orderid, tbl.rownum
OFFSET 10 ROWS FETCH NEXT 10 ROWS ONLY;

--3
WITH EmpCTE AS
(
	SELECT e.empid, e.mgrid, e.firstname, e.lastname
	FROM HR.Employees AS e
	WHERE e.empid = 9

	UNION ALL

	SELECT e.empid, e.mgrid, e.firstname, e.lastname
	FROM EmpCTE AS m
		JOIN HR.Employees AS e
			ON e.empid = m.mgrid
)
SELECT *
FROM EmpCTE

--4.1.
IF OBJECT_ID('Sales.VEmpOrders') IS NOT NULL
	DROP VIEW Sales.VEmpOrders;
GO
CREATE VIEW Sales.VEmpOrders
AS
SELECT empid,
	YEAR(orderdate) AS orderyear,
	SUM(qty) AS qty
FROM Sales.Orders AS O
	JOIN Sales.OrderDetails AS OD
	ON O.orderid = OD.orderid
GROUP BY empid, YEAR(orderdate);
GO

--4.2.
SELECT empid, orderyear, qty,
	(SELECT SUM(qty)
	 FROM Sales.VEmpOrders AS V2
	 WHERE V2.empid = V1.empid
		AND V2.orderyear <= V1.orderyear) AS runqty
FROM Sales.VEmpOrders AS V1
ORDER BY empid, orderyear;

--5.1
IF OBJECT_ID('Production.TopProducts') IS NOT NULL
	DROP FUNCTION Production.TopProducts;
GO
CREATE FUNCTION Production.TopProducts
	(@supid AS INT, @n AS INT) RETURNS TABLE
AS
RETURN 
	SELECT TOP(@n) p.productid, p.productname, p.unitprice
	FROM Production.Products AS p
	WHERE p.supplierid = @supid
	ORDER BY p.unitprice DESC
GO

--5.2.
SELECT s.supplierid, s.companyname, p.productid, p.productname, p.unitprice
FROM Production.Suppliers AS s
	CROSS APPLY Production.TopProducts(s.supplierid, 2) AS p;
