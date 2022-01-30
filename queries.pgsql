-- SQL
-- SQL-Complient. (SQL Server (Transact-SQL) iyo PostgreSQL (PGSQL))

CREATE TABLE users
(
	id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
	name text NOT NULL,
	email text NOT NULL unique,
	created_at timestamp DEFAULT now()
);


SELECT * FROM users WHERE email ILIKE '%TEST%' ORDER BY id DESC OFFSET 1 LIMIT 1;


SELECT row_to_json(u) FROM users AS u;


SELECT jsonb_build_object('userId', u.id, 'userName', u.name) FROM users AS u;


INSERT INTO users (name, email) VALUES ('Test User', 'test@test.com');


INSERT INTO users (name, email, created_at)
	VALUES ('Another test user',
	'testing3@test.com', now() - interval '1 day');

UPDATE users SET name = 'Mohamed Ali' WHERE id = 1;

DELETE FROM users WHERE id = 2;

