USE sbdb;

INSERT INTO Region (region_name)
VALUES 
('Vestkysten'),
('Sydfyn'),
('Nordjylland'),
('Bornholm');

INSERT INTO Person (full_name)
VALUES 
('Hans Jensen'),
('Karen Larsen'),
('Peter Madsen'),
('Mette Nielsen'),
('Lars Pedersen');

INSERT INTO Property (address, owner, fk_region)
VALUES 
('Strandvejen 1, 6700 Esbjerg', 'Anders Hansen', 1), 
('Skovvej 5, 5700 Svendborg','Sofie Petersen', 2),   
('Havnevej 10, 9900 Frederikshavn', 'Jens Andersen', 3),
('Kystvej 20, 3700 Rønne', 'Maria Sørensen', 4);      

INSERT INTO Reservation (customer, start_date, end_date, fk_property)
VALUES 
('Anders Hansen', '2024-07-06', '2024-07-13', 1),  
('Sofie Petersen', '2024-08-10', '2024-08-17', 2), 
('Jens Andersen', '2024-09-14', '2024-09-21', 3), 
('Maria Sørensen', '2024-10-05', '2024-10-12', 4); 