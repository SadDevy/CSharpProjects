USE Northwind;

--13.1
--Написать процедуру, которая возвращает самый крупный заказ для каждого из продавцов 
--за определенный год. В результатах не может быть несколько заказов одного продавца, 
--должен быть только один и самый крупный. В результатах запроса должны быть выведены 
--следующие колонки: колонка с именем и фамилией продавца (FirstName и LastName – 
--пример: Nancy Davolio), номер заказа и его стоимость. В запросе надо учитывать 
--Discount при продаже товаров. Процедуре передается год, за который надо сделать 
--отчет, и количество возвращаемых записей. Результаты запроса должны быть 
--упорядочены по убыванию суммы заказа. Процедура должна быть реализована 2-мя 
--способами: с использованием оператора SELECT и с использованием курсора. 
--Название функций соответственно GreatestOrders и GreatestOrdersCur. 
--Необходимо продемонстрировать использование этих процедур. Также помимо 
--демонстрации вызовов процедур в скрипте QueryB.sql надо написать отдельный 
--ДОПОЛНИТЕЛЬНЫЙ проверочный запрос для тестирования правильности работы процедуры 
--GreatestOrders. Проверочный запрос должен выводить в удобном для сравнения с 
--результатами работы процедур виде для определенного продавца для всех его заказов 
--за определенный указанный год в результатах следующие колонки: имя продавца, 
--номер заказа, сумму заказа. Проверочный запрос не должен повторять запрос, 
--написанный в процедуре, - он должен выполнять только то, что описано в 
--требованиях по нему.
--13.1.1
IF OBJECT_ID('dbo.GetGreatestOrdersGTE', 'P') IS NOT NULL
	DROP PROC dbo.GetGreatestOrdersGTE;
GO

CREATE PROC dbo.GetGreatestOrdersGTE
	@year AS INT,
	@recordsCount AS INT
AS
SET NOCOUNT ON;

WITH AllOrders AS 
(
	SELECT O.EmployeeID, O.OrderID,
		MAX(SUM(OD.UnitPrice * (1 - OD.Discount) * OD.Quantity))
			OVER(PARTITION BY O.EmployeeID 
				 ORDER BY SUM(OD.UnitPrice * (1 - OD.Discount) * OD.Quantity) DESC) AS 'Cost',
		ROW_NUMBER() 
			OVER(PARTITION BY O.EmployeeID 
				 ORDER BY O.EmployeeID) AS 'RowNumber'
	FROM dbo.Orders AS O
		INNER JOIN dbo.[Order Details] AS OD ON O.OrderID = OD.OrderID
	WHERE YEAR(O.OrderDate) = @year
	GROUP BY O.EmployeeID, O.OrderID
)
SELECT TOP(@recordsCount) E.FirstName + ' ' + E.LastName AS 'Full Name',
	A.OrderID, A.Cost
FROM AllOrders AS A
	INNER JOIN dbo.Employees AS E ON E.EmployeeID = A.EmployeeID
WHERE A.RowNumber = 1
ORDER BY A.Cost;
GO

--Execution
EXEC dbo.GetGreatestOrdersGTE --60%
	@year = 1996,
	@recordsCount = 10;

--Using DISTINCT
IF OBJECT_ID('dbo.GetGreatestOrders', 'P') IS NOT NULL
	DROP PROC dbo.GetGreatestOrders;
GO

CREATE PROC dbo.GetGreatestOrders
	@year AS INT,
	@recordsCount AS INT
AS
SET NOCOUNT ON;

SELECT TOP(@recordsCount)
	(SELECT E.FirstName + ' ' + E.LastName
	 FROM dbo.Employees AS E
	 WHERE E.EmployeeID = O.EmployeeID) AS 'Full Name',
	O.OrderID, O.Cost
FROM (
		SELECT O.EmployeeID, O.OrderID,
			SUM(OD.UnitPrice * (1 - OD.Discount) * OD.Quantity) AS 'Cost',
			ROW_NUMBER() OVER(PARTITION BY O.EmployeeID 
							  ORDER BY SUM(OD.UnitPrice * (1 - OD.Discount) * OD.Quantity) DESC) AS 'RowNumber'
		FROM dbo.Orders AS O
			INNER JOIN dbo.[Order Details] AS OD
				ON O.OrderID = OD.OrderID
		WHERE YEAR(O.OrderDate) = @year
		GROUP BY O.EmployeeID, O.OrderID
	 ) AS O
WHERE O.RowNumber = 1
ORDER BY 3;
GO

--Execution
EXEC dbo.GetGreatestOrders --40%
	@year = 1996,
	@recordsCount = 10;


--13.1.2
IF OBJECT_ID('dbo.GetGreatestOrdersCur', 'P') IS NOT NULL
	DROP PROC dbo.GetGreatestOrdersCur;
GO

CREATE PROC dbo.GetGreatestOrdersCur
	@year AS INT,
	@recordsCount AS INT
AS
SET NOCOUNT ON;

DECLARE @GreatestOrders TABLE
(
	employeeId INT,
	orderId    INT,
	orderYear  INT,
	cost	   FLOAT,
	PRIMARY KEY(employeeId, orderId)
);

DECLARE
	@employeeId AS INT,
	@orderId AS INT,
	@previousYear AS INT,
	@orderYear AS INT,
	@cost AS FLOAT,
	@maxYearCost AS FLOAT
DECLARE C CURSOR FAST_FORWARD FOR
	SELECT O.EmployeeID, O.OrderID, YEAR(O.OrderDate),
		SUM(OD.UnitPrice * (1 - OD.Discount) * OD.Quantity)
	FROM dbo.Orders AS O
		INNER JOIN dbo.[Order Details] AS OD ON O.OrderID = OD.OrderID
	GROUP BY O.EmployeeID, O.OrderID, O.OrderDate
	ORDER BY 1, 3, 4 DESC;

OPEN C;

FETCH NEXT FROM C INTO @employeeId, @orderId, @orderYear, @cost;

SELECT @maxYearCost = @cost, @previousYear = @orderYear;

WHILE @@FETCH_STATUS  = 0
	BEGIN
		IF @previousYear != @orderYear
			SELECT @maxYearCost = @cost, @previousYear = @orderYear;

		IF @cost = @maxYearCost
			INSERT INTO @GreatestOrders VALUES(@employeeId, @orderId, @orderYear, @cost);
			
		FETCH NEXT FROM C INTO @employeeId, @orderId, @orderYear, @cost;
	END

CLOSE C;

DEALLOCATE C;

SELECT TOP(@recordsCount) E.FirstName + ' ' + E.LastName AS 'Full Name',
	G.orderId, G.cost
FROM @GreatestOrders AS G
	INNER JOIN dbo.Employees AS E ON E.EmployeeID = G.employeeId
WHERE G.orderYear = @year
ORDER BY G.cost;
GO

EXEC dbo.GetGreatestOrdersCur
	@year = 1998,
	@recordsCount = 10;

--Checked query
DECLARE 
	@employeeId AS INT,
	@year AS INT;

SET @employeeId = 1;
SET @year = 1998;

SELECT 
	(SELECT E.FirstName + ' ' + E.LastName
	 FROM dbo.Employees AS E
	 WHERE E.EmployeeID = O.EmployeeID) AS 'Full Name',
	O.OrderID,
	SUM(OD.UnitPrice * (1 - OD.Discount) * OD.Quantity) AS 'Cost'
FROM dbo.Orders AS O
	INNER JOIN dbo.[Order Details] AS OD ON O.OrderID = OD.OrderID
WHERE O.EmployeeID = @employeeId 
	AND YEAR(O.OrderDate) = @year
GROUP BY O.EmployeeID, O.OrderID, YEAR(O.OrderDate)
ORDER BY 3 DESC;

--13.2
--Написать процедуру, которая возвращает заказы в таблице Orders, согласно 
--указанному сроку доставки в днях (разница между OrderDate и ShippedDate). 
--В результатах должны быть возвращены заказы, срок которых превышает переданное 
--значение, или еще недоставленные заказы. Значению по умолчанию для передаваемого 
--срока 35 дней. Название процедуры ShippedOrdersDiff. Процедура должна высвечивать 
--следующие колонки: OrderID, OrderDate, ShippedDate, ShippedDelay (разность в 
--днях между ShippedDate и OrderDate), SpecifiedDelay (переданное в процедуру значение).  
--Необходимо продемонстрировать использование этой процедуры.
IF OBJECT_ID('dbo.SHippedOrdersDiff', 'P') IS NOT NULL
	DROP PROC dbo.SHippedOrdersDiff;
GO

CREATE PROC dbo.SHippedOrdersDiff
	@days AS INT = 35
AS
SET NOCOUNT ON;

SELECT O.OrderID, O.OrderDate, O.ShippedDate,
	DATEDIFF(DAY, O.OrderDate, O.ShippedDate) AS 'ShippedDelay',
	@days AS 'SpecifiedDelay'
FROM dbo.Orders AS O
WHERE DATEDIFF(DAY, O.OrderDate, O.ShippedDate) > @days
	OR O.ShippedDate IS NULL;
GO

EXEC dbo.SHippedOrdersDiff
	@days = 17;

--13.3
--Написать процедуру, которая высвечивает всех подчиненных заданного продавца, 
--как непосредственных, так и подчиненных его подчиненных. В качестве входного 
--параметра функции используется EmployeeID. Необходимо распечатать имена подчиненных 
--и выровнять их в тексте (использовать оператор PRINT) согласно иерархии подчинения. 
--Продавец, для которого надо найти подчиненных, также должен быть высвечен. 
--Название процедуры SubordinationInfo. В качестве алгоритма для решения этой 
--задачи надо использовать пример, приведенный в Books Online и рекомендованный 
--Microsoft для решения подобного типа задач. Продемонстрировать использование 
--процедуры.
IF OBJECT_ID('dbo.SubordinationInfo', 'P') IS NOT NULL
	DROP PROC dbo.SubordinationInfo;
GO

CREATE PROC dbo.SubordinationInfo
	@employeeId AS INT
AS
SET NOCOUNT ON;

DECLARE @result AS NVARCHAR(255);
SET @result = CHAR(13);

WITH Something(EmployeeID, ReportsTo, EmployeeLevel) AS
(
	SELECT E.EmployeeID, E.ReportsTo, 0
	FROM dbo.Employees AS E
	WHERE E.EmployeeID = @employeeId

	UNION ALL

	SELECT E.EmployeeID, E.ReportsTo, EmployeeLevel + 1
	FROM dbo.Employees AS E
		INNER JOIN Something AS S ON E.ReportsTo = S.EmployeeID
)
SELECT @result += 
	REPLICATE('	', S.EmployeeLevel) + ' ' + 
	CONVERT(nvarchar, S.EmployeeID) + ' ' +
	(SELECT E.FirstName + ' ' + E.LastName 
	 FROM dbo.Employees AS E 
	 WHERE E.EmployeeID = S.EmployeeID) +
	CHAR(13)
FROM Something AS S

PRINT @result;
GO

--Execution
EXEC dbo.SubordinationInfo --Output!!!
	@employeeId = 2;

--13.4
--Написать функцию, которая определяет, есть ли у продавца подчиненные. 
--Возвращает тип данных BIT. В качестве входного параметра функции используется 
--EmployeeID. Название функции IsBoss. Продемонстрировать использование функции 
--для всех продавцов из таблицы Employees.
IF OBJECT_ID('dbo.IsBoss') IS NOT NULL
	DROP FUNCTION dbo.IsBoss;
GO

CREATE FUNCTION dbo.IsBoss
(
	@employeeId AS INT
)
RETURNS BIT
AS
	BEGIN
		DECLARE @a AS BIT;

		WITH Something(EmployeeID, ReportsTo, EmployeeLevel) AS
		(
			SELECT E.EmployeeID, E.ReportsTo, 0
			FROM dbo.Employees AS E
			WHERE E.EmployeeID = @employeeId

			UNION ALL
	
			SELECT E.EmployeeID, E.ReportsTo, EmployeeLevel + 1
			FROM dbo.Employees AS E
				INNER JOIN Something AS S ON E.ReportsTo = S.EmployeeID
		)
		SELECT @a = CONVERT(BIT, COUNT(*) - 1) --Подумать еще!!!
		FROM Something

		RETURN
			@a;
	END;
GO

--Execution
DECLARE @i AS INT;
SET @i = 1;

WHILE @i != 9
BEGIN
	PRINT 'Id = ' + 
		  CONVERT(VARCHAR, @i) + ', ' + 
		  'Sub = ' +
		  CONVERT(VARCHAR, dbo.IsBoss(@i));

	SET @i += 1; --Execution plan
END;