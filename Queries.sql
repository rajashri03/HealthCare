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