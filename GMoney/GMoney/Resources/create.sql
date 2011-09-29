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

insert into m_major values (NULL, 'Fixed', 0, 1, datetime('now'), datetime('now'), 0);
insert into m_major values (NULL, 'Grocery Costs', 0, 2, datetime('now'), datetime('now'), 0);
insert into m_major values (NULL, 'Automobile', 0, 3, datetime('now'), datetime('now'), 0);
insert into m_major values (NULL, 'Childcare', 0, 4, datetime('now'), datetime('now'), 0);
insert into m_major values (NULL, 'Education', 0, 5, datetime('now'), datetime('now'), 0);
insert into m_major values (NULL, 'Healthcare', 0, 6, datetime('now'), datetime('now'), 0);
insert into m_major values (NULL, 'HouseHold', 0, 7, datetime('now'), datetime('now'), 0);
insert into m_major values (NULL, 'Other', 0, 99, datetime('now'), datetime('now'), 0);
insert into m_major values (NULL, 'Insurance', 2, 901, datetime('now'), datetime('now'), 0);
insert into m_major values (NULL, 'Taxes', 2, 902, datetime('now'), datetime('now'), 0);
insert into m_major values (NULL, 'Pension', 2, 903, datetime('now'), datetime('now'), 0);

insert into m_major values (NULL, 'Salary Income', 1, 951, datetime('now'), datetime('now'), 0);
insert into m_major values (NULL, 'Other Income', 1, 952, datetime('now'), datetime('now'), 0);
insert into m_major values (NULL, 'Retirement Income', 1, 953, datetime('now'), datetime('now'), 0);


insert into m_sub values (0, 0,'Expense', 280000, 30000, 0, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,1,'Cell Phone',0,0,101, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,1,'Electricity',5000,1000,102, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,1,'Internet',0,0,103, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,1,'Natural Gas/Oil',6000,1000,104, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,1,'Rent',0,0,105, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,1,'Telephone',0,0,106, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,1,'Television',0,0,107, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,1,'Transportation',0,0,108, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,1,'Water & Sewer',10000,2000,109, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,2,'Food',0,0,201, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,2,'Groceries',0,0,202, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,3,'Car Payment',0,0,301, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,3,'Gasoline',0,0,302, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,3,'Maintenance',0,0,303, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,4,'Child Support',0,0,401, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,4,'Children/Toys',0,0,402, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,4,'Daycare',0,0,403, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,5,'Books',0,0,501, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,5,'Fees',0,0,502, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,5,'Tuition',0,0,503, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,6,'Dental',0,0,601, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,6,'Eyecare',0,0,602, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,6,'Hospital',0,0,603, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,6,'Physician',0,0,604, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,6,'Prescriptions',0,0,605, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,7,'Electronics',0,0,701, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,7,'Furniture',0,0,702, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,7,'Homeowner''s Dues',0,0,703, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,7,'House Cleaning',0,0,704, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,7,'Yard Service ',0,0,705, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,8,'Alimony Payment',0,0,9901, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,8,'Beauty & Barber',0,0,9902, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,8,'Charitable Payment',0,0,9903, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,8,'Clothing',0,0,9904, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,8,'Dining Out',0,0,9905, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,8,'Expense Accounts/Membership',0,0,9906, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,8,'Garbage & Recycle',0,0,9907, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,8,'Gifts',0,0,9908, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,8,'Health Club',0,0,9909, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,8,'Hobbies/Leisure',0,0,9910, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,8,'Loan',0,0,9911, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,8,'Lottery',0,0,9912, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,8,'Miscellaneous',0,0,9913, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,8,'Mortgage Payment',0,0,9914, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,8,'Newspaper',0,0,9915, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,8,'Pet Care',0,0,9916, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,8,'Remittance',0,0,9917, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,8,'Travel/Vacation',0,0,9918, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,8,'Go Home',0,0,9919, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,9,'Automobile',0,0,90101, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,9,'Commercial',0,0,90102, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,9,'Employment',0,0,90103, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,9,'Health',0,0,90104, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,9,'Homeowner''s/Renter''s',0,0,90105, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,9,'Life',0,0,90106, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,10,'Real Estate Taxes',0,0,90201, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,10,'Resident Tax',0,0,90202, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,10,'Social Security Tax',0,0,90203, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,10,'Withholding Tax',0,0,90204, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,11,'Corporate Pension',0,0,90301, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,11,'Employee Pension',0,0,90302, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,11,'National Pension',0,0,90303, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,12,'Wages & Salary',0,0,95101, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,12,'Overtime',0,0,95102, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,12,'Bonus',0,0,95103, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,12,'Transportation',0,0,95104, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,12,'Travel allowance',0,0,95105, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,12,'Daily allowance',0,0,95106, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,13,'Child Support Received',0,0,95201, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,13,'Gifts Received',0,0,95202, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,13,'Lotteries',0,0,95203, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,13,'Other',0,0,95204, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,13,'State & Local Tax Refund',0,0,95205, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,14,'Pensions & Annuities',0,0,95301, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,14,'Social Security Benefits',0,0,95302, datetime('now'), datetime('now'), 0);

insert into m_payment values (NULL, 'Cash', datetime('now'), datetime('now'), 0);
insert into m_payment values (NULL, 'Credit', datetime('now'), datetime('now'), 0);
insert into m_payment values (NULL, 'Bank', datetime('now'), datetime('now'), 0);

insert into m_user values (0, 'Family', datetime('now'), datetime('now'), 0);