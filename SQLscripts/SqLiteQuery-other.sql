select count(Id) kzp from books;

DELETE from books where Id > 0;

INSERT INTO Books ([Id], Name, Author, Price, [Year]) VALUES 
 (1, 'Война и мир', 'Л.Н. Толстой', 260, 1975);
INSERT INTO Books ([Id], Name, Author, Price, [Year]) VALUES
 (2, 'Отцы и дети', 'И. Тургенев', 180, 1979);
INSERT INTO Books ([Id], Name, Author, Price, [Year]) VALUES
 (3, 'Чайка', 'А. Чехов', 150, 1980);
INSERT INTO Books ([Id], Name, Author, Price, [Year]) VALUES
 (4, 'Когда проснётся Марс', 'В. Огнева', 350, 2015);
