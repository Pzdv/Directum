-- 1 запрос
-- Если за "объем продаж в количественном выражении" считать кол-во проданных товаров то функцию count() надо поменять на sum(s.Quantity)
-- Если нам не надо показывать продавцов без продаж за этот период то используем обычный join

with TotalBySellers as 
(
	select s.IDSel, count(*) as Total from Sales as s
	where s.Date between '01-10-2013' and '07-10-2013'
	group by s.IDSel
)
select s.Name, s.Surname, t.Total from TotalBySellers as t
right join Sellers as s on s.ID = t.IDSel
order by s.Surname, s.Name;


-- 2 запрос
-- Если не нужно показывать продавцов которые ничего не продали за данный период то необходимо right join поменять на join

with cte as 
(
	select s.IDSel, a.IDProd, (100.0 * sum(s.Quantity)) / sum(sum(s.Quantity)) over (partition by a.IDProd) as [Percent]
	from Sales as s
	join Arrivals as a on a.IDProd = s.IDProd
	where s.Date between '01-10-2013' and '07-10-2013' and a.Date between '07-09-2013' and '07-10-2013'
	group by s.IDSel, a.IDProd
)
select s.Name, s.Surname, p.Name as [Product Name], cast(round(cte.[Percent], 2) as dec(12,2)) as [Percent] from cte
right join Sellers as s on s.ID = cte.IDSel
left join Products as p on p.ID = cte.IDProd
order by p.Name, s.Surname, s.Name