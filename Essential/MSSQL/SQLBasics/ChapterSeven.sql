USE TSQL2012;

--1
SELECT o.custid, o.orderid, o.qty,
	RANK() OVER(PARTITION BY o.custid ORDER BY o.qty) AS rnk,
	DENSE_RANK() OVER(PARTITION BY o.custid ORDER BY o.qty) AS drnk
FROM dbo.Orders AS o

--2
SELECT o.custid, o.orderid, o.qty,
	o.qty - LAG(o.qty) OVER(PARTITION BY o.custid ORDER BY o.orderdate, o.qty) AS diffprev,
	o.qty - LEAD(o.qty) OVER(PARTITION BY o.custid ORDER BY o.orderdate, o.qty) AS diffnext
FROM dbo.Orders AS o

--3
SELECT empid, [2007] AS cnt2007, [2008] AS cnt2008, [2009] AS cnt2009
FROM (SELECT empid, YEAR(orderdate) AS orderyear
	  FROM dbo.Orders) AS D
		PIVOT(COUNT(orderyear) FOR orderyear IN ([2007], [2008], [2009])) AS P;

--4
SELECT empid, CAST(RIGHT(orderyear, 4) AS INT) AS orderyear, numorders
FROM dbo.EmpYearOrders
	UNPIVOT(numorders FOR orderyear IN (cnt2007, cnt2008, cnt2009)) AS P
WHERE numorders != 0;

--5
SELECT
	GROUPING_ID(empid, custid, YEAR(Orderdate)) AS groupingset,
	empid, custid, YEAR(Orderdate) AS orderyear, SUM(qty) AS sumqty
FROM dbo.Orders
GROUP BY
	GROUPING SETS
	(
		(empid, custid, YEAR(orderdate)),
		(empid, YEAR(orderdate)),
		(custid, YEAR(orderdate))
	);