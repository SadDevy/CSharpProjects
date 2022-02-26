USE TSQL2012;

--1
SELECT o.orderid, o.orderdate, o.custid, o.empid
FROM Sales.Orders AS o
WHERE o.orderdate = 
	(SELECT MAX(ord.orderdate) 
	 FROM Sales.Orders AS ord);

--2
SELECT custid, orderid, orderdate, empid
FROM Sales.Orders
WHERE custid IN
 (SELECT TOP (1) WITH TIES O.custid
 FROM Sales.Orders AS O
 GROUP BY O.custid
 ORDER BY COUNT(*) DESC);

 --3
 SELECT e.empid, e.firstname, e.lastname 
 FROM HR.Employees AS e
 WHERE empid NOT IN
	(SELECT o.empid
	 FROM Sales.Orders AS o
	 WHERE o.orderdate >= '20080501');

--4
SELECT DISTINCT country
FROM Sales.Customers
WHERE country NOT IN
	(SELECT E.country 
	 FROM HR.Employees AS E);

--5
SELECT o.custid, o.orderid, o.orderdate, o.empid
FROM Sales.Orders AS o
WHERE o.orderdate =
	(SELECT  MAX(ord.orderdate)
	 FROM Sales.Orders AS ord
	 WHERE ord.custid = o.custid)

--6
SELECT c.custid, c.companyname
FROM Sales.Customers AS c
WHERE EXISTS
 (SELECT *
  FROM Sales.Orders AS O
  WHERE O.custid = C.custid
	AND O.orderdate >= '20070101'
	AND O.orderdate < '20080101')
	AND NOT EXISTS
 (SELECT *
  FROM Sales.Orders AS O
  WHERE O.custid = C.custid
	AND O.orderdate >= '20080101'
	AND O.orderdate < '20090101');

--7
SELECT c.custid, c.companyname
FROM Sales.Customers AS c
WHERE EXISTS
	(SELECT *
	 FROM Sales.Orders AS O
	 WHERE O.custid = C.custid
		AND EXISTS
	(SELECT *
	 FROM Sales.OrderDetails AS OD
	 WHERE OD.orderid = O.orderid
		AND OD.ProductID = 12));

--8
SELECT custid, ordermonth, qty,
	(SELECT SUM(O2.qty)
	 FROM Sales.CustOrders AS O2
	 WHERE O2.custid = O1.custid
		AND O2.ordermonth <= O1.ordermonth) AS runqty
FROM Sales.CustOrders AS O1
ORDER BY custid, ordermonth;