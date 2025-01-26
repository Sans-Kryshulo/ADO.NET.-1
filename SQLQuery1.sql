/*
CREATE DATABASE VegetablesFruits;
*/
/*
USE VegetablesFruits;

CREATE TABLE Products (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Type NVARCHAR(50) CHECK (Type IN ('Овоч', 'Фрукт')) NOT NULL,
    Color NVARCHAR(50) NOT NULL,
    Calories INT NOT NULL
);
*/
/*
ALTER TABLE Products DROP CONSTRAINT CK__Products__Type__36B12243;

ALTER TABLE Products ADD CONSTRAINT CK_Products_Type 
CHECK (Type IN ('Vegetable', 'Fruit', 'Other'));
*/
INSERT INTO Products (Name, Type, Color, Calories)
VALUES 
    ('Apple', 'Fruit', 'Red', 52),
    ('Banana', 'Fruit', 'Yellow', 89),
    ('Carrot', 'Vegetable', 'Orange', 41),
    ('Tomato', 'Vegetable', 'Red', 18),
    ('Strawberry', 'Fruit', 'Red', 32),
    ('Potato', 'Vegetable', 'Brown', 77),
    ('Cucumber', 'Vegetable', 'Green', 16),
    ('Blueberry', 'Fruit', 'Blue', 57),
    ('Pumpkin', 'Vegetable', 'Orange', 26),
    ('Grapes', 'Fruit', 'Purple', 69);