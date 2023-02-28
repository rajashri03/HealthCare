Create or replace procedure SP_Register
(
	username varchar(200),
	useraddress varchar(200),
	phonenumber bigint,
	email varchar(200),
	password varchar(100),
	usertype varchar(100)
	
)
Language sql
as
$$
		Insert into Users(username,useraddress,phonenumber,email,password,usertype)    
		Values (username,useraddress,phonenumber,email,password,usertype) 
 
$$




Create or replace procedure SP_AddMedicine
(
	medicinename varchar(200),
	medicin_description varchar(200)
)
Language sql
as
$$
		Insert into medicines(medicinename,medicin_description)    
		Values (medicinename,medicin_description) 
 
$$

Create or replace procedure sp_approve
(
uid int, approve int
)
Language plpgsql
as
$$
BEGIN
IF approve = 0 THEN
update users set approve=1 where users.userid=uid;
else
update users set approve=0 where users.userid=uid;
END IF;
 END
$$

call sp_approve(9,1);