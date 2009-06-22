CREATE TABLE s_system (app_name text primary key, app_value text);
CREATE TABLE [m_user] ([id] integer PRIMARY KEY NOT NULL, [name] text NOT NULL, [create_date] datetime NOT NULL, [update_date] datetime NOT NULL, [delete_flg] tinyint NOT NULL);
CREATE TABLE [m_payment] ([id] integer PRIMARY KEY NOT NULL, [name] text NOT NULL, [create_date] datetime NOT NULL, [update_date] datetime NOT NULL, [delete_flg] tinyint NOT NULL);
CREATE TABLE [m_major] ([id] integer PRIMARY KEY NOT NULL, [name] text NOT NULL, [type] tinyint NOT NULL, [sort] integer NOT NULL, [create_date] datetime NOT NULL, [update_date] datetime NOT NULL, [delete_flg] tinyint NOT NULL);
CREATE TABLE [m_sub] ([id] integer PRIMARY KEY NOT NULL, [major_id] integer NOT NULL, [name] text NOT NULL, [optimal] GMoney NOT NULL, [range] GMoney NOT NULL, [sort] integer NOT NULL, [create_date] datetime NOT NULL, [update_date] datetime NOT NULL, [delete_flg] tinyint NOT NULL);
CREATE TABLE [d_bill] ([id] integer PRIMARY KEY NOT NULL, [bill_date] varchar(10) NOT NULL, [user_id] integer NOT NULL, [sub_id] integer NOT NULL, [payment_id] integer NOT NULL, [annual_budget] tinyint NOT NULL, [amount] GMoney NOT NULL, [remarks] text, [fixed_id] integer, [create_date] datetime NOT NULL, [update_date] datetime NOT NULL);
CREATE TABLE [d_fixed_data] ([id] integer PRIMARY KEY NOT NULL, [user_id] integer NOT NULL, [sub_id] integer NOT NULL, [interval_type] tinyint NOT NULL, [interval] tinyint NOT NULL, [detail] text, [amount] GMoney NOT NULL, [start_date] datetime NOT NULL, [end_date] datetime NOT NULL, [create_date] datetime NOT NULL, [update_date] datetime NOT NULL);

CREATE VIEW v_sub as select major.id as major_id, major.name as major_name, major.type as bill_type, major.sort as major_sort, sub.id as sub_id, sub.name as sub_name, sub.id as ID, sub.name as Name, Optimal, Range, sub.sort as sub_sort from m_sub as sub left outer join m_major as major on sub.major_id = major.id and major.delete_flg = 0 where sub.delete_flg = 0;
CREATE VIEW v_fixed_data as select fixed.id as id, fixed.user_id as user_id, user.name as user_name, fixed.sub_id as sub_id, sub.name as sub_name, sub.bill_type as bill_type, sub.major_sort as major_sort, sub.sub_sort as sub_sort, fixed.interval_type as interval_type, fixed.interval as interval, fixed.detail as detail, fixed.amount as amount, fixed.start_date as start_date, fixed.end_date as end_date from d_fixed_data as fixed left outer join m_user as user on fixed.user_id = user.id left outer join v_sub as sub on fixed.sub_id = sub.id ;
CREATE VIEW v_bill as select bill.id, bill_date, bill.user_id as user_id, user.name as user_name, major.id as major_id, major.name as major_name, major.type as bill_type, major.sort as major_sort, sub_id, sub.name as sub_name, sub.sort as sub_sort, sub.name as Name, payment_id, pay.name as payment_name, annual_budget, amount, remarks,fixed_id, bill.create_date, bill.update_date from d_bill as bill left outer join m_user as user on user_id = user.id left outer join m_sub as sub on sub_id = sub.id left outer join m_major as major on sub.major_id = major.id left outer join m_payment as pay on payment_id = pay.id;

insert into s_system values ('login_password', null);
insert into s_system values ('last_vacuum_date', null);

insert into m_major values (NULL, 'Fixed', 0, 4, datetime('now'), datetime('now'), 0);
insert into m_major values (NULL, 'Grocery Costs', 0, 5, datetime('now'), datetime('now'), 0);
insert into m_major values (NULL, 'Automobile', 0, 6, datetime('now'), datetime('now'), 0);
insert into m_major values (NULL, 'Childcare', 0, 7, datetime('now'), datetime('now'), 0);
insert into m_major values (NULL, 'Other', 0, 8, datetime('now'), datetime('now'), 0);
insert into m_major values (NULL, 'Insurance', 2, 9, datetime('now'), datetime('now'), 0);
insert into m_major values (NULL, 'Taxes', 2, 10, datetime('now'), datetime('now'), 0);
insert into m_major values (NULL, 'Pension', 2, 11, datetime('now'), datetime('now'), 0);

insert into m_major values (NULL, 'Salary Income', 1, 1, datetime('now'), datetime('now'), 0);
insert into m_major values (NULL, 'Other Income', 1, 2, datetime('now'), datetime('now'), 0);
insert into m_major values (NULL, 'Retirement Income', 1, 3, datetime('now'), datetime('now'), 0);


insert into m_sub values (0, 0,'Expense', 280000, 30000, 0, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,1,'Cell Phone',0,0,1, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,1,'Electricity',5000,1000,2, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,1,'Internet',0,0,3, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,1,'Natural Gas/Oil',6000,1000,4, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,1,'Rent',0,0,5, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,1,'Telephone',0,0,6, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,1,'Television',0,0,7, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,1,'Transportation',0,0,8, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,1,'Water & Sewer',10000,2000,9, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,2,'Food',0,0,10, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,2,'Groceries',0,0,11, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,3,'Car Payment',0,0,12, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,3,'Gasoline',0,0,13, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,3,'Maintenance',0,0,14, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,4,'Child Support',0,0,15, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,4,'Children/Toys',0,0,16, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,4,'Daycare',0,0,17, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,5,'Alimony Payment',0,0,18, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,5,'Beauty & Barber',0,0,19, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,5,'Charitable Payment',0,0,20, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,5,'Clothing',0,0,21, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,5,'Dining Out',0,0,22, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,5,'Education',0,0,23, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,5,'Electronics',0,0,24, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,5,'Expense Accounts/Membership',0,0,25, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,5,'Furniture',0,0,26, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,5,'Garbage & Recycle',0,0,27, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,5,'Gifts',0,0,28, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,5,'Health Club',0,0,29, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,5,'Hobbies/Leisure',0,0,30, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,5,'Homeowner''s Dues',0,0,31, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,5,'Loan',0,0,32, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,5,'Lottery',0,0,33, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,5,'Medical/Dental',0,0,34, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,5,'Miscellaneous',0,0,35, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,5,'Mortgage Payment',0,0,36, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,5,'Newspaper',0,0,37, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,5,'Pet Care',0,0,38, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,5,'Remittance',0,0,39, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,5,'Travel/Vacation',0,0,40, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,5,'Go Home',0,0,41, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,6,'Automobile',0,0,42, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,6,'Commercial',0,0,43, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,6,'Employment',0,0,44, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,6,'Health',0,0,45, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,6,'Homeowner''s/Renter''s',0,0,46, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,6,'Life',0,0,47, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,7,'Real Estate Taxes',0,0,48, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,7,'Resident Tax',0,0,49, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,7,'Social Security Tax',0,0,50, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,7,'Withholding Tax',0,0,51, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,8,'Corporate Pension',0,0,52, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,8,'Employee Pension',0,0,53, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,8,'National Pension',0,0,54, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,9,'Wages & Salary',0,0,55, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,9,'Bonus',0,0,56, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,10,'Child Support Received',0,0,57, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,10,'Gifts Received',0,0,58, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,10,'Lotteries',0,0,59, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,10,'Other',0,0,60, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,10,'State & Local Tax Refund',0,0,61, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,11,'Pensions & Annuities',0,0,62, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,11,'Social Security Benefits',0,0,63, datetime('now'), datetime('now'), 0);

insert into m_payment values (NULL, 'Cash', datetime('now'), datetime('now'), 0);
insert into m_payment values (NULL, 'Credit', datetime('now'), datetime('now'), 0);
insert into m_payment values (NULL, 'Bank', datetime('now'), datetime('now'), 0);

insert into m_user values (0, 'Family', datetime('now'), datetime('now'), 0);