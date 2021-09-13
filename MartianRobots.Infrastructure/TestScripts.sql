delete from Robots
delete from RobotMovements
delete from Positions
delete from GridCoordinates
delete from DeadEnds
DBCC CHECKIDENT ('Robots', RESEED, 0)
DBCC CHECKIDENT ('RobotMovements', RESEED, 0)
DBCC CHECKIDENT ('Positions', RESEED, 0)
DBCC CHECKIDENT ('GridCoordinates', RESEED, 0)
DBCC CHECKIDENT ('DeadEnds', RESEED, 0)

select * from RobotMovements
select * from Positions
select * from GridCoordinates
select * from DeadEnds