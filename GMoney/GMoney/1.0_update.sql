delete from m_major;
delete from m_sub;

insert into m_major values (NULL,'Housing',0,1, datetime('now'), datetime('now'), 0);
insert into m_major values (NULL,'Food',0,2, datetime('now'), datetime('now'), 0);
insert into m_major values (NULL,'Cost of utilities',0,3, datetime('now'), datetime('now'), 0);
insert into m_major values (NULL,'Communication cost',0,4, datetime('now'), datetime('now'), 0);
insert into m_major values (NULL,'Transportation',0,5, datetime('now'), datetime('now'), 0);
insert into m_major values (NULL,'Education',0,6, datetime('now'), datetime('now'), 0);
insert into m_major values (NULL,'Recreational',0,7, datetime('now'), datetime('now'), 0);
insert into m_major values (NULL,'Acquaintanceship',0,8, datetime('now'), datetime('now'), 0);
insert into m_major values (NULL,'Beauty & Clothing',0,9, datetime('now'), datetime('now'), 0);
insert into m_major values (NULL,'Healthcare cost',0,10, datetime('now'), datetime('now'), 0);
insert into m_major values (NULL,'Investment',0,11, datetime('now'), datetime('now'), 0);
insert into m_major values (NULL,'Other',0,99, datetime('now'), datetime('now'), 0);
insert into m_major values (NULL,'Insurance',2,901, datetime('now'), datetime('now'), 0);
insert into m_major values (NULL,'Social Insurance',2,902, datetime('now'), datetime('now'), 0);
insert into m_major values (NULL,'Taxes',2,903, datetime('now'), datetime('now'), 0);
insert into m_major values (NULL,'Salary Income',1,951, datetime('now'), datetime('now'), 0);
insert into m_major values (NULL,'Investment Income',1,952, datetime('now'), datetime('now'), 0);
insert into m_major values (NULL,'Other Income',1,953, datetime('now'), datetime('now'), 0);

insert into m_sub values (0, 0,'Expense', 280000, 30000, 0, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,1,'Rent',0,0,1, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,1,'Groceries & Ornaments',0,0,2, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,1,'Furniture & Electronics',0,0,3, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,1,'Household services',0,0,4, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,1,'Homeowner''s Dues',0,0,5, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,1,'Waste disposal fee',0,0,6, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,1,'Repair cost',0,0,7, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,1,'Other',0,0,8, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,2,'Food',0,0,1, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,2,'Dinning out',0,0,2, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,2,'School meal fees',0,0,3, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,2,'Other',0,0,4, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,3,'Natural Gas/Oil',0,0,1, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,3,'Water & Sewer',0,0,2, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,3,'Other',0,0,3, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,3,'Electric bill',0,0,4, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,4,'Television',0,0,1, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,4,'Telephone & Mobile',0,0,2, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,4,'Shipping charges',0,0,3, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,4,'Postcards, stamps & etc.',0,0,4, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,4,'Internet',0,0,5, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,5,'Train pass',0,0,1, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,5,'Gasoline',0,0,2, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,5,'Car maintenance costs',0,0,3, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,5,'Parking fee',0,0,4, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,5,'Other',0,0,5, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,6,'Books & Magazines',0,0,1, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,6,'Tuition',0,0,2, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,6,'School & Training',0,0,3, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,1,'Childcare fee',0,0,4, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,7,'Hobbies/Leisure',0,0,1, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,7,'Recreational supplies',0,0,2, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,7,'Toys',0,0,3, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,7,'Enrichment lessons',0,0,4, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,7,'Trip',0,0,5, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,7,'Lottery',0,0,6, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,8,'Ceremonial fees',0,0,1, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,8,'Donation',0,0,2, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,8,'Expense Accounts/Membership',0,0,3, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,8,'Gift',0,0,4, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,8,'Other',0,0,5, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,9,'Clothing',0,0,1, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,9,'Cleaning',0,0,2, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,9,'Beauty & Barber',0,0,3, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,10,'Drug',0,0,1, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,10,'Medical apparatus',0,0,2, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,10,'Hospital',0,0,3, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,10,'Other',0,0,4, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,11,'Stockholders',0,0,1, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,11,'Other',0,0,2, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,12,'Allowance',0,0,1, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,12,'Incidentals',0,0,2, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,12,'Remittance',0,0,3, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,12,'Miscellaneous',0,0,4, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,13,'Personal pension',0,0,1, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,13,'Corporate pension',0,0,2, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,13,'Car insurance',0,0,3, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,13,'Life insurance',0,0,4, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,13,'Property and casualty insurance',0,0,5, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,14,'Health insurance',0,0,1, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,14,'Employment insurance',0,0,2, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,14,'Employment Pension',0,0,3, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,14,'National Pension',0,0,4, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,15,'Withholding tax',0,0,1, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,15,'Fixed asset tax and city planning tax',0,0,2, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,15,'Car tax',0,0,3, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,15,'Resident tax',0,0,4, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,15,'Other',0,0,5, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,16,'Part-time job or sideline income',0,0,1, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,16,'Salaries and wages',0,0,2, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,16,'Overtime pay',0,0,3, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,16,'Other allowances',0,0,4, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,16,'Severance pay',0,0,5, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,16,'Bonus',0,0,6, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,16,'Extraordinary revenue',0,0,7, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,17,'Dividend',0,0,1, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,17,'Trading profit and loss',0,0,2, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,17,'Contribution',0,0,3, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,18,'Health insurance benefits',0,0,1, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,18,'Bounty',0,0,2, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,18,'Lotteries',0,0,3, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,18,'Public pension benefits',0,0,4, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,18,'Life insurance and private pension',0,0,5, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,18,'Unemployment insurance benefits',0,0,6, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,18,'Child allowance',0,0,7, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,18,'From beneficiary',0,0,8, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,18,'Income tax refunds',0,0,9, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,18,'Stock option',0,0,10, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,18,'Other',0,0,11, datetime('now'), datetime('now'), 0);




update d_bill set sub_id=81 where sub_id in (59);
update d_bill set sub_id=85 where sub_id in (57);
update d_bill set sub_id=87 where sub_id in (61);
update d_bill set sub_id=61 where sub_id in (44);
update d_bill set sub_id=44 where sub_id in (19);
update d_bill set sub_id=89 where sub_id in (60,73);
update d_bill set sub_id=60 where sub_id in (45);
update d_bill set sub_id=45 where sub_id in (68,69);

update d_bill set sub_id=38 where sub_id in (18);
update d_bill set sub_id=18 where sub_id in (1);
update d_bill set sub_id=1 where sub_id in (5,31);

update d_bill set sub_id=42 where sub_id in (21);
update d_bill set sub_id=21 where sub_id in (3);
update d_bill set sub_id=3 where sub_id in (24,26);
update d_bill set sub_id=26 where sub_id in (12,14);
update d_bill set sub_id=14 where sub_id in (9);
update d_bill set sub_id=9 where sub_id in (10);
update d_bill set sub_id=10 where sub_id in (22);
update d_bill set sub_id=22 where sub_id in (8);

update d_bill set sub_id=74 where sub_id in (56);
update d_bill set sub_id=56 where sub_id in (52);

update d_bill set sub_id=13 where sub_id in (4);
update d_bill set sub_id=4 where sub_id in (70);
update d_bill set sub_id=70 where sub_id in (55);

update d_bill set sub_id=63 where sub_id in (54);
update d_bill set sub_id=54 where sub_id in (35,41);
update d_bill set sub_id=35 where sub_id in (40);
update d_bill set sub_id=40 where sub_id in (28);
update d_bill set sub_id=28 where sub_id in (23);

update d_bill set sub_id=71 where sub_id in (72);

update d_bill set sub_id=67 where sub_id in (49);

update d_bill set sub_id=62 where sub_id in (53);
update d_bill set sub_id=53 where sub_id in (39);
update d_bill set sub_id=39 where sub_id in (25);

update d_bill set sub_id=27 where sub_id in (64);
update d_bill set sub_id=64 where sub_id in (51);

update d_bill set sub_id=58 where sub_id in (43,47);
update d_bill set sub_id=47 where sub_id in (34,66);
update d_bill set sub_id=34 where sub_id in (15);

update d_bill set sub_id=32 where sub_id in (30);
update d_bill set sub_id=30 where sub_id in (17);

update d_bill set sub_id=36 where sub_id in (33);
update d_bill set sub_id=33 where sub_id in (16);
update d_bill set sub_id=16 where sub_id in (2);
update d_bill set sub_id=2 where sub_id in (11);


update d_fixed_data set sub_id=18 where sub_id=1;
update d_fixed_data set sub_id=1 where sub_id=5;

update d_fixed_data set sub_id=16 where sub_id=2;
update d_fixed_data set sub_id=21 where sub_id=3;
update d_fixed_data set sub_id=13 where sub_id=4;
update d_fixed_data set sub_id=14 where sub_id=9;
update d_fixed_data set sub_id=60 where sub_id=45;
update d_fixed_data set sub_id=62 where sub_id=53;


update d_fixed_data set sub_id=56 where sub_id=52;
update d_fixed_data set sub_id=67 where sub_id=49;

update d_fixed_data set sub_id=58 where sub_id=47;
update d_fixed_data set sub_id=58 where sub_id=43;
update d_fixed_data set sub_id=30 where sub_id=17;
update d_fixed_data set sub_id=34 where sub_id=15;
update d_fixed_data set sub_id=39 where sub_id=25;
update d_fixed_data set sub_id=70 where sub_id=55;
