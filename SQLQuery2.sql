SELECT * 
FROM Books
	INNER JOIN Borrow ON Books.BookId = Borrow.BookId
	INNER JOIN Students ON Borrow.StudentId = Students.StudentId
ORDER BY Name_author;