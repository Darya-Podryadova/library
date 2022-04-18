SELECT TOP (1) Name_author
FROM
	Books
	INNER JOIN Borrow ON Books.BookId = Borrow.BookId
	INNER JOIN Students ON Borrow.StudentId = Students.StudentId
WHERE YEAR(Borrow) = N'2021' 
GROUP BY Name_author
ORDER BY COUNT(Name_author) DESC;