alter table verifyaccounts alter column userid varchar(100)
alter table verifyaccounts drop constraint FK__VerifyAcc__useri__267ABA7A

truncate table tbl_users
truncate table VerifyAccounts;
select *from Tbl_Users