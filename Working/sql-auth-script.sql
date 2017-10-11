SELECT * FROM aspnetroles;

INSERT INTO AspNetRoles (Id, Name) VALUES (1, 'Administrator');
INSERT INTO AspNetRoles (Id, Name) VALUES (2, 'Customer');

SELECT * FROM aspnetusers;

INSERT INTO AspNetUserRoles (UserId, RoleId)
VALUES ('[your-userid-here', 1);

SELECT * FROM AspNetUserRoles;