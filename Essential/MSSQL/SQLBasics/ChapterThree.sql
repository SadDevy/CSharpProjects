USE TSQL2012;

--1.1
SELECT e.empid, e.firstname, e.lastname, n.n 
FROM HR.Employees AS e
	CROSS JOIN dbo.Nums as n
WHERE n.n < 6;

--1.2
SELECT e.empid, DATEADD(DAY, d.n - 1, '20090612') AS dt
FROM HR.Employees AS e
	CROSS JOIN dbo.Nums AS d
WHERE d.n <= DATEDIFF(DAY, '20090612', '20090616') + 1
ORDER BY e.empid, dt;

--2
SELECT c.custid, COUNT(DISTINCT o.orderid) as numorders,
	SUM(od.qty) as totalqty
FROM Sales.Customers as c
	JOIN Sales.Orders AS o
		ON o.custid = c.custid
	JOIN Sales.OrderDetails AS od
		ON od.orderid = o.orderid
WHERE c.country = N'USA'
GROUP BY c.custid;

--3
SELECT c.custid, c.companyname, o.orderid, o.orderdate  
FROM Sales.Customers AS c
	LEFT JOIN Sales.Orders AS o
		ON o.custid = c.custid;

--4
SELECT c.custid, c.companyname
FROM Sales.Customers AS c
	LEFT JOIN Sales.Orders AS o
		ON o.custid = c.custid
WHERE o.orderid IS NULL;

--5 
SELECT c.custid, c.companyname, o.orderid, o.orderdate
FROM Sales.Customers AS c
	RIGHT JOIN Sales.Orders AS o
		ON o.custid = c.custid
WHERE o.orderdate = '20070212';

--6
SELECT c.custid, c.companyname, o.orderid, o.orderdate
FROM Sales.Customers AS c
	LEFT JOIN Sales.Orders AS o
		ON o.custid = c.custid
		AND o.orderdate = '20070212';

--7
SELECT DISTINCT c.custid, c.companyname,
	CASE WHEN o.orderid IS NULL THEN 'Да' ELSE 'Нет' END AS 
		[HasOrderOn20070212]
FROM Sales.Customers AS c
	LEFT JOIN Sales.Orders AS o
		ON o.custid = c.custid
		AND o.orderdate = '20070212';
