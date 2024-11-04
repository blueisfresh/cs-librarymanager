SELECT * FROM dbo.TblStudent
SELECT * FROM dbo.TblBook

INSERT INTO dbo.TblStudent (LibraryCardNum, FirstName, LastName) VALUES
(185432, 'Anna', 'Schmidt'),
(243567, 'Paul', 'Müller'),
(376589, 'Lena', 'Krause'),
(459321, 'Jonas', 'Becker'),
(512478, 'Mia', 'Hoffmann'),
(675849, 'Lukas', 'Schneider'),
(782456, 'Emma', 'Weber'),
(899034, 'Leon', 'Schäfer'),
(158937, 'Sophia', 'Fischer'),
(268745, 'Max', 'Wagner'),
(397845, 'Isabella', 'Schulz'),
(429876, 'Finn', 'Richter'),
(534891, 'Emilia', 'Klein'),
(682345, 'Ben', 'Wolf'),
(752189, 'Lara', 'Schmidt'),
(817934, 'Tim', 'Neumann'),
(914583, 'Lea', 'Zimmermann'),
(192837, 'Luis', 'Braun'),
(246389, 'Clara', 'Krüger'),
(395467, 'Noah', 'Hartmann');

INSERT INTO TblBook (BookNum, Title, Author, Publisher, ISBN, PublicationPlace, PublicationDate) VALUES
('00001-2024', 'Der alte Mann und das Meer', 'Ernest Hemingway', 'Rowohlt Verlag', '9783499551399', 'Hamburg', '1952-09-01'),
('00002-2024', '1984', 'George Orwell', 'Ullstein Verlag', '9783548234106', 'Berlin', '1949-06-08'),
('00003-2024', 'Stolz und Vorurteil', 'Jane Austen', 'Insel Verlag', '9783458361528', 'Frankfurt', '1813-01-28'),
('00004-2024', 'Krieg und Frieden', 'Leo Tolstoi', 'Hanser Verlag', '9783446234208', 'München', '1869-01-01'),
('00005-2024', 'Die Verwandlung', 'Franz Kafka', 'Fischer Verlag', '9783100590115', 'Frankfurt', '1915-10-01'),
('00006-2024', 'Faust', 'Johann Wolfgang von Goethe', 'Reclam Verlag', '9783150000000', 'Stuttgart', '1808-01-01'),
('00007-2024', 'Don Quijote', 'Miguel de Cervantes', 'Suhrkamp Verlag', '9783518458508', 'Berlin', '1753-01-01'), -- Adjusted to earliest valid date for `datetime`
('00008-2024', 'Die Brüder Karamasow', 'Fjodor Dostojewski', 'Diogenes Verlag', '9783257069286', 'Zürich', '1880-01-01'),
('00009-2024', 'Moby Dick', 'Herman Melville', 'Anaconda Verlag', '9783730600006', 'Köln', '1851-10-18'),
('00010-2024', 'Anna Karenina', 'Leo Tolstoi', 'Ullstein Verlag', '9783548287782', 'Berlin', '1878-01-01'),
('00011-2024', 'Der große Gatsby', 'F. Scott Fitzgerald', 'dtv', '9783423130039', 'München', '1925-04-10'),
('00012-2024', 'Schuld und Sühne', 'Fjodor Dostojewski', 'Aufbau Verlag', '9783351025002', 'Berlin', '1866-01-01'),
('00013-2024', 'Der Prozess', 'Franz Kafka', 'Fischer Verlag', '9783100590016', 'Frankfurt', '1925-01-01'),
('00014-2024', 'Ulysses', 'James Joyce', 'Suhrkamp Verlag', '9783518228347', 'Frankfurt', '1922-02-02'),
('00015-2024', 'Die Odyssee', 'Homer', 'Reclam Verlag', '9783150000642', 'Stuttgart', '2000-01-01'),
('00016-2024', 'Madame Bovary', 'Gustave Flaubert', 'Diogenes Verlag', '9783257235803', 'Zürich', '1857-01-01'),
('00017-2024', 'Jane Eyre', 'Charlotte Brontë', 'Reclam Verlag', '9783150198523', 'Stuttgart', '1847-10-16'),
('00018-2024', 'Das Bildnis des Dorian Gray', 'Oscar Wilde', 'Anaconda Verlag', '9783730600013', 'Köln', '1890-07-01'),
('00019-2024', 'Der Fänger im Roggen', 'J.D. Salinger', 'Knaur Verlag', '9783426784550', 'München', '1951-07-16'),
('00020-2024', 'Wuthering Heights', 'Emily Brontë', 'Fischer Verlag', '9783596211393', 'Frankfurt', '1847-12-01');


