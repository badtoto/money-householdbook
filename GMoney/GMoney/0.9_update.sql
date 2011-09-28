--m_major

INSERT INTO m_major VALUES (NULL,"Education",0,5,datetime('now'),datetime('now'),0);
INSERT INTO m_major VALUES (NULL,"Healthcare",0,6,datetime('now'),datetime('now'),0);
INSERT INTO m_major VALUES (NULL,"HouseHold",0,7,datetime('now'),datetime('now'),0);

UPDATE m_major SET sort = 1, update_date = datetime('now') WHERE name = 'Fixed';
UPDATE m_major SET sort = 2, update_date = datetime('now') WHERE name = 'Grocery Costs';
UPDATE m_major SET sort = 3, update_date = datetime('now') WHERE name = 'Automobile';
UPDATE m_major SET sort = 4, update_date = datetime('now') WHERE name = 'Childcare';
UPDATE m_major SET sort = 99, update_date = datetime('now') WHERE name = 'Other';
UPDATE m_major SET sort = 901, update_date = datetime('now') WHERE name = 'Insurance';
UPDATE m_major SET sort = 902, update_date = datetime('now') WHERE name = 'Taxes';
UPDATE m_major SET sort = 903, update_date = datetime('now') WHERE name = 'Pension';
UPDATE m_major SET sort = 951, update_date = datetime('now') WHERE name = 'Salary Income';
UPDATE m_major SET sort = 952, update_date = datetime('now') WHERE name = 'Other Income';
UPDATE m_major SET sort = 953, update_date = datetime('now') WHERE name = 'Retirement Income';

--m_sub

--new
insert into m_sub values (NULL,12,'Books',0,0,501, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,12,'Tuition',0,0,503, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,13,'Dental',0,0,601, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,13,'Eyecare',0,0,602, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,13,'Physician',0,0,604, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,13,'Prescriptions',0,0,605, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,14,'House Cleaning',0,0,704, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,14,'Yard Service ',0,0,705, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,9,'Overtime',0,0,95102, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,9,'Transportation',0,0,95104, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,9,'Travel allowance',0,0,95105, datetime('now'), datetime('now'), 0);
insert into m_sub values (NULL,9,'Daily allowance',0,0,95106, datetime('now'), datetime('now'), 0);

--mod
UPDATE m_sub SET major_id=12, name = 'Fees', sort = 502, update_date = datetime('now') WHERE id = 23;
UPDATE m_sub SET major_id=13, name = 'Hospital', sort = 603, update_date = datetime('now') WHERE id = 34;
UPDATE m_sub SET major_id=14, name = 'Electronics', sort = 701, update_date = datetime('now') WHERE id = 24;
UPDATE m_sub SET major_id=14, name = 'Furniture', sort = 702, update_date = datetime('now') WHERE id = 26;
UPDATE m_sub SET major_id=14, name = 'Homeowner''s Dues', sort = 703, update_date = datetime('now') WHERE id = 31;

--sort
update m_sub set sort=101, update_date=datetime('now') where major_id=1 and name='Cell Phone';
update m_sub set sort=102, update_date=datetime('now') where major_id=1 and name='Electricity';
update m_sub set sort=103, update_date=datetime('now') where major_id=1 and name='Internet';
update m_sub set sort=104, update_date=datetime('now') where major_id=1 and name='Natural Gas/Oil';
update m_sub set sort=105, update_date=datetime('now') where major_id=1 and name='Rent';
update m_sub set sort=106, update_date=datetime('now') where major_id=1 and name='Telephone';
update m_sub set sort=107, update_date=datetime('now') where major_id=1 and name='Television';
update m_sub set sort=108, update_date=datetime('now') where major_id=1 and name='Transportation';
update m_sub set sort=109, update_date=datetime('now') where major_id=1 and name='Water & Sewer';
update m_sub set sort=201, update_date=datetime('now') where major_id=2 and name='Food';
update m_sub set sort=202, update_date=datetime('now') where major_id=2 and name='Groceries';
update m_sub set sort=301, update_date=datetime('now') where major_id=3 and name='Car Payment';
update m_sub set sort=302, update_date=datetime('now') where major_id=3 and name='Gasoline';
update m_sub set sort=303, update_date=datetime('now') where major_id=3 and name='Maintenance';
update m_sub set sort=401, update_date=datetime('now') where major_id=4 and name='Child Support';
update m_sub set sort=402, update_date=datetime('now') where major_id=4 and name='Children/Toys';
update m_sub set sort=403, update_date=datetime('now') where major_id=4 and name='Daycare';
update m_sub set sort=9901, update_date=datetime('now') where major_id=5 and name='Alimony Payment';
update m_sub set sort=9902, update_date=datetime('now') where major_id=5 and name='Beauty & Barber';
update m_sub set sort=9903, update_date=datetime('now') where major_id=5 and name='Charitable Payment';
update m_sub set sort=9904, update_date=datetime('now') where major_id=5 and name='Clothing';
update m_sub set sort=9905, update_date=datetime('now') where major_id=5 and name='Dining Out';
update m_sub set sort=9906, update_date=datetime('now') where major_id=5 and name='Expense Accounts/Membership';
update m_sub set sort=9907, update_date=datetime('now') where major_id=5 and name='Garbage & Recycle';
update m_sub set sort=9908, update_date=datetime('now') where major_id=5 and name='Gifts';
update m_sub set sort=9909, update_date=datetime('now') where major_id=5 and name='Health Club';
update m_sub set sort=9910, update_date=datetime('now') where major_id=5 and name='Hobbies/Leisure';
update m_sub set sort=9911, update_date=datetime('now') where major_id=5 and name='Loan';
update m_sub set sort=9912, update_date=datetime('now') where major_id=5 and name='Lottery';
update m_sub set sort=9913, update_date=datetime('now') where major_id=5 and name='Miscellaneous';
update m_sub set sort=9914, update_date=datetime('now') where major_id=5 and name='Mortgage Payment';
update m_sub set sort=9915, update_date=datetime('now') where major_id=5 and name='Newspaper';
update m_sub set sort=9916, update_date=datetime('now') where major_id=5 and name='Pet Care';
update m_sub set sort=9917, update_date=datetime('now') where major_id=5 and name='Remittance';
update m_sub set sort=9918, update_date=datetime('now') where major_id=5 and name='Travel/Vacation';
update m_sub set sort=9919, update_date=datetime('now') where major_id=5 and name='Go Home';
update m_sub set sort=90101, update_date=datetime('now') where major_id=6 and name='Automobile';
update m_sub set sort=90102, update_date=datetime('now') where major_id=6 and name='Commercial';
update m_sub set sort=90103, update_date=datetime('now') where major_id=6 and name='Employment';
update m_sub set sort=90104, update_date=datetime('now') where major_id=6 and name='Health';
update m_sub set sort=90105, update_date=datetime('now') where major_id=6 and name='Homeowner''s/Renter''s';
update m_sub set sort=90106, update_date=datetime('now') where major_id=6 and name='Life';
update m_sub set sort=90201, update_date=datetime('now') where major_id=7 and name='Real Estate Taxes';
update m_sub set sort=90202, update_date=datetime('now') where major_id=7 and name='Resident Tax';
update m_sub set sort=90203, update_date=datetime('now') where major_id=7 and name='Social Security Tax';
update m_sub set sort=90204, update_date=datetime('now') where major_id=7 and name='Withholding Tax';
update m_sub set sort=90301, update_date=datetime('now') where major_id=8 and name='Corporate Pension';
update m_sub set sort=90302, update_date=datetime('now') where major_id=8 and name='Employee Pension';
update m_sub set sort=90303, update_date=datetime('now') where major_id=8 and name='National Pension';
update m_sub set sort=95101, update_date=datetime('now') where major_id=9 and name='Wages & Salary';
update m_sub set sort=95103, update_date=datetime('now') where major_id=9 and name='Bonus';
update m_sub set sort=95201, update_date=datetime('now') where major_id=10 and name='Child Support Received';
update m_sub set sort=95202, update_date=datetime('now') where major_id=10 and name='Gifts Received';
update m_sub set sort=95203, update_date=datetime('now') where major_id=10 and name='Lotteries';
update m_sub set sort=95204, update_date=datetime('now') where major_id=10 and name='Other';
update m_sub set sort=95205, update_date=datetime('now') where major_id=10 and name='State & Local Tax Refund';
update m_sub set sort=95301, update_date=datetime('now') where major_id=11 and name='Pensions & Annuities';
update m_sub set sort=95302, update_date=datetime('now') where major_id=11 and name='Social Security Benefits';

