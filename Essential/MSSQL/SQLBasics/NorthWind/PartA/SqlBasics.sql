USE Northwind;

--1.1
--Выбрать в таблице Orders заказы, которые были доставлены после 6 мая 1998 года 
--(колонка ShippedDate) включительно и которые доставлены с ShipVia >= 2. 
--Формат указания даты должен быть верным при любых региональных настройках, 
--согласно требованиям статьи “Writing International Transact-SQL Statements” в 
--Books Online раздел “Accessing and Changing Relational Data Overview”. 
--Этот метод использовать далее для всех заданий. Запрос должен высвечивать 
--только колонки OrderID, ShippedDate и ShipVia.
--Пояснить почему сюда не попали заказы с NULL-ом в колонке ShippedDate.
SELECT O.OrderID, O.ShippedDate, O.ShipVia
FROM dbo.Orders AS O
WHERE O.ShippedDate >= CONVERT(DATETIME, '19980506', 101)
	AND O.ShipVia >= 2;
--При сравнении Null >= 06.05.1998 and something >= 2 возвращается unknown

--1.2
--Написать запрос, который выводит только недоставленные заказы из таблицы Orders. 
--В результатах запроса высвечивать для колонки ShippedDate вместо значений 
--NULL строку ‘Not Shipped’ – использовать системную функцию CASЕ. 
--Запрос должен высвечивать только колонки OrderID и ShippedDate.
SELECT O.OrderID, 
	CASE 
		WHEN O.ShippedDate IS NULL THEN 'Not Shipped'
	END
FROM dbo.Orders AS O
WHERE O.ShippedDate IS NULL;

--1.3
--Выбрать в таблице Orders заказы, которые были доставлены после 6 мая 1998 года 
--(ShippedDate) не включая эту дату или которые еще не доставлены. 
--В запросе должны высвечиваться только колонки OrderID (переименовать в 
--Order Number) и ShippedDate (переименовать в Shipped Date). 
--В результатах запроса высвечивать для колонки ShippedDate вместо значений 
--NULL строку ‘Not Shipped’, для остальных значений высвечивать дату в формате 
--по умолчанию.
SELECT O.OrderID AS 'Order Number',
	CASE 
		WHEN O.ShippedDate IS NULL THEN 'Not Shipped'
		ELSE CONVERT(varchar, O.ShippedDate, 101) 
	END AS 'Shipped Date'
FROM dbo.Orders AS O
WHERE O.ShippedDate >= CONVERT(DATETIME, '19980506', 101)
	OR O.ShippedDate IS NULL;

--2.1
--Выбрать из таблицы Customers всех заказчиков, проживающих в USA и Canada. 
--Запрос сделать только с помощью оператора IN. Высвечивать колонки с именем 
--пользователя и названием страны в результатах запроса. Упорядочить результаты 
--запроса по имени заказчиков и по месту проживания.
SELECT C.ContactName, C.Country
FROM dbo.Customers AS C
WHERE C.Country IN ('USA', 'Canada')
ORDER BY 1, 2;

--2.2
--Выбрать из таблицы Customers всех заказчиков, не проживающих в USA и Canada. 
--Запрос сделать с помощью оператора IN. Высвечивать колонки с именем пользователя 
--и названием страны в результатах запроса. Упорядочить результаты запроса по 
--имени заказчиков.
SELECT C.ContactName, C.Country
FROM dbo.Customers AS C
WHERE C.Country NOT IN ('USA', 'Canada')
ORDER BY 1;

--2.3
--Выбрать из таблицы Customers все страны, в которых проживают заказчики. 
--Страна должна быть упомянута только один раз и список отсортирован по убыванию. 
--Не использовать предложение GROUP BY. Высвечивать только одну колонку в 
--результатах запроса.
SELECT DISTINCT C.Country
FROM dbo.Customers AS C
ORDER BY 1 DESC;

--3.1
--Выбрать все заказы (OrderID) из таблицы Order Details 
--(заказы не должны повторяться), где встречаются продукты с количеством от 3 
--до 10 включительно – это колонка Quantity в таблице Order Details. 
--Использовать оператор BETWEEN. 
--Запрос должен высвечивать только колонку OrderID.
SELECT DISTINCT O.OrderID
FROM dbo.[Order Details] AS O
WHERE O.Quantity BETWEEN 3 AND 10;

--3.2
--Выбрать всех заказчиков из таблицы Customers, у которых название 
--страны начинается на буквы из диапазона b и g. Использовать оператор BETWEEN. 
--Проверить, что в результаты запроса попадает Germany. 
--Запрос должен высвечивать только колонки CustomerID и Country и 
--отсортирован по Country.
SELECT C.CustomerID, C.Country
FROM dbo.Customers AS C
WHERE LEFT(C.Country, 1) BETWEEN 'b' AND 'g'
ORDER BY C.Country;--execution plan

--3.3
--Выбрать всех заказчиков из таблицы Customers, у которых название 
--страны начинается на буквы из диапазона b и g, не используя оператор 
--BETWEEN. С помощью опции “Execution Plan” определить какой 
--запрос предпочтительнее 3.2 или 3.3 – для этого надо ввести в скрипт 
--выполнение текстового Execution Plan-a для двух этих запросов, 
--результаты выполнения Execution Plan надо ввести в скрипт в виде 
--комментария и по их результатам дать ответ на вопрос – по какому параметру 
--было проведено сравнение. Запрос должен высвечивать только колонки 
--CustomerID и Country и отсортирован по Country.
SELECT C.CustomerID, C.Country
FROM dbo.Customers AS C
WHERE C.Country LIKE '[b-g]%'
ORDER BY C.Country;--execution plan

--4.1
--В таблице Products найти все продукты (колонка ProductName), 
--где встречается подстрока 'chocolade'. Известно, что в подстроке 
--'chocolade' может быть изменена одна буква 'c' в середине - 
--найти все продукты, которые удовлетворяют этому условию. 
--Подсказка: результаты запроса должны высвечивать 2 строки.
SELECT *
FROM dbo.Products AS P
WHERE P.ProductName LIKE '%cho%olade%';

--5.1
--Найти общую сумму всех заказов из таблицы Order Details с учетом 
--количества закупленных товаров и скидок по ним. Результат округлить 
--до сотых и высветить в стиле 1 для типа данных money.  
--Скидка (колонка Discount) составляет процент из стоимости для данного 
--товара. Для определения действительной цены на проданный продукт надо 
--вычесть скидку из указанной в колонке UnitPrice цены. Результатом 
--запроса должна быть одна запись с одной колонкой с названием колонки 'Totals'.
SELECT 
	CONVERT(money, 
		ROUND(
			SUM(O.UnitPrice * (1 - O.Discount) * O.Quantity), 
			2), 
		1) AS 'Totals'
FROM dbo.[Order Details] AS O

--5.2
--По таблице Orders найти количество заказов, которые еще не были доставлены 
--(т.е. в колонке ShippedDate нет значения даты доставки). 
--Использовать при этом запросе только оператор COUNT. Не использовать 
--предложения WHERE и GROUP.
SELECT COUNT(*) - COUNT(O.ShippedDate)
FROM dbo.Orders AS O

--5.3
--По таблице Orders найти количество различных покупателей (CustomerID), 
--сделавших заказы. Использовать функцию COUNT и не использовать предложения 
--WHERE и GROUP.
SELECT COUNT(DISTINCT O.CustomerID)
FROM dbo.Orders AS O

--6.1
--По таблице Orders найти количество заказов с группировкой по годам. 
--В результатах запроса надо высвечивать две колонки c названиями Year и 
--Total. Написать проверочный запрос, который вычисляет количество всех заказов.
SELECT YEAR(O.OrderDate) AS 'Year',
	COUNT('Year') AS 'Total' --Проблема с Null
FROM dbo.Orders AS O
GROUP BY YEAR(O.OrderDate);

--Проверка:
SELECT COUNT(*) --Проблема с Null
FROM dbo.Orders AS O

--6.2
--По таблице Orders найти количество заказов, cделанных каждым продавцом. 
--Заказ для указанного продавца – это любая запись в таблице Orders, где в 
--колонке EmployeeID задано значение для данного продавца. В результатах 
--запроса надо высвечивать колонку с именем продавца (Должно высвечиваться 
--имя полученное конкатенацией LastName & FirstName. Эта строка LastName & 
--FirstName должна быть получена отдельным запросом в колонке основного запроса. 
--Также основной запрос должен использовать группировку по EmployeeID.) с 
--названием колонки ‘Seller’ и колонку c количеством заказов высвечивать с 
--названием 'Amount'. Результаты запроса должны быть упорядочены по убыванию 
--количества заказов.
SELECT 
	(SELECT E.LastName + ' ' + FirstName
	 FROM dbo.Employees AS E
	 WHERE E.EmployeeID = O.EmployeeID) AS 'Seller',
	COUNT(O.EmployeeID) AS 'Amount'
FROM dbo.Orders AS O
WHERE O.EmployeeID IS NOT NULL
GROUP BY O.EmployeeID
ORDER BY COUNT(O.EmployeeID) DESC;

--6.3
--По таблице Orders найти количество заказов, cделанных каждым продавцом и 
--для каждого покупателя. Необходимо определить это только для заказов 
--сделанных в 1998 году. В результатах запроса надо высвечивать колонку с 
--именем продавца (название колонки ‘Seller’), колонку с именем покупателя 
--(название колонки ‘Customer’)  и колонку c количеством заказов высвечивать 
--с названием 'Amount'. В запросе необходимо использовать специальный 
--оператор языка T-SQL для работы с выражением GROUP (Этот же оператор поможет 
--выводить строку “ALL” в результатах запроса). Группировки должны быть сделаны 
--по ID продавца и покупателя. Результаты запроса должны быть упорядочены по 
--продавцу, покупателю и по убыванию количества продаж. В результатах должна 
--быть сводная информация по продажам.
SELECT 
	CASE
		WHEN
			(SELECT E.FirstName + ' ' + E.LastName 
			 FROM dbo.Employees AS E
			 WHERE E.EmployeeID = O.EmployeeID) IS NULL THEN 'ALL'
		ELSE
			(SELECT E.FirstName + ' ' + E.LastName 
			 FROM dbo.Employees AS E
			 WHERE E.EmployeeID = O.EmployeeID)
	END AS 'Seller',
	CASE
		WHEN 
			(SELECT C.ContactName
			 FROM dbo.Customers AS C
			 WHERE C.CustomerID = O.CustomerID) IS NULL THEN 'ALL'
		ELSE 
			(SELECT C.ContactName
			 FROM dbo.Customers AS C
			 WHERE C.CustomerID = O.CustomerID)
	END AS 'Customer',
	COUNT(*)
FROM dbo.Orders AS O
WHERE YEAR(O.OrderDate) = 1998
GROUP BY GROUPING SETS(
			(O.EmployeeID, O.CustomerID), 
			O.CustomerID, 
			O.EmployeeID, 
			())
ORDER BY O.EmployeeID, O.CustomerID, COUNT(*) DESC;

--6.5
--Найти покупателей и продавцов, которые живут в одном городе.
--Если в городе живут только один или несколько продавцов или только один 
--или несколько покупателей, то информация о таких покупателя и продавцах 
--не должна попадать в результирующий набор. Не использовать конструкцию JOIN. 
--В результатах запроса необходимо вывести следующие заголовки для 
--результатов запроса: ‘Person’, ‘Type’ (здесь надо выводить строку ‘Customer’ 
--или  ‘Seller’ в зависимости от типа записи), ‘City’. Отсортировать 
--результаты запроса по колонке ‘City’ и по ‘Person’.
WITH IntersectedCities AS
(
	SELECT C.City
	FROM dbo.Customers AS C
	
	INTERSECT
	
	SELECT E.City
	FROM dbo.Employees AS E
)
SELECT E.FirstName + ' ' + E.LastName AS 'Person',
	'Employee' AS 'Type',
	E.City
FROM dbo.Employees AS E
WHERE E.City IN (SELECT *
				 FROM IntersectedCities)
UNION
SELECT C.ContactName AS 'Person',
	'Customer' AS 'Type',
	C.City
FROM dbo.Customers AS C
WHERE C.City IN (SELECT *
				 FROM IntersectedCities)
ORDER BY 3, 1;  --Сделать еще один вариант!!!
