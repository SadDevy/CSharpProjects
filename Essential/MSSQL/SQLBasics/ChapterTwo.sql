USE TSQL2012;

--1
SELECT * 
FROM Sales.Orders AS ord
WHERE MONTH(ord.orderdate) = 6 AND YEAR(ord.orderdate) = 2007;

--2
SELECT *
FROM Sales.Orders AS ord
WHERE DAY(ord.orderdate) = DAY(EOMONTH(ord.orderdate));

--3
SELECT *
FROM HR.Employees AS emp
WHERE LEN(emp.lastname) - LEN(REPLACE(emp.lastname, 'o', '')) > 1;

--4
SELECT ord.orderid, SUM(ord.qty * ord.unitprice) AS 'totalvalue'
FROM Sales.OrderDetails AS ord
GROUP BY ord.orderid
HAVING SUM(ord.qty * ord.unitprice) > 10000;

--5
SELECT TOP(3) ord.shipcountry, AVG(ord.freight) AS 'avgfreight' 
FROM Sales.Orders AS ord
GROUP BY ord.shipcountry, YEAR(ord.shippeddate)
HAVING YEAR(ord.shippeddate) = 2007
ORDER BY AVG(ord.freight) DESC;

--6
SELECT ord.custid, ord.orderdate, ord.orderid,
    ROW_NUMBER() OVER(PARTITION BY ord.custid ORDER BY ord.orderdate, ord.orderid) AS rownum
FROM Sales.Orders AS ord
ORDER BY ord.custid, rownum;

--7
SELECT emp.empid, emp.firstname, emp.lastname, emp.titleofcourtesy,
	CASE emp.titleofcourtesy
		WHEN 'Ms.' THEN 'Woman'
		WHEN 'Mrs.' THEN 'Woman'
		WHEN 'Mr.' THEN 'Man'
		ELSE 'Unknown'
	END AS 'gender'
FROM HR.Employees AS emp

--8
SELECT c.custid, c.region 
FROM Sales.Customers AS c
ORDER BY 
	CASE WHEN region IS NULL THEN 1 ELSE 0 END, region;