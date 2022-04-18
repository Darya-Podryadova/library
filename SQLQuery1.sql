SELECT Name_author 
FROM
	Books
	INNER JOIN Borrow ON Books.BookId = Borrow.BookId
	INNER JOIN Students ON Borrow.StudentId = Students.StudentId
WHERE Surname = 'Иванов';