-- unos racuna
INSERT INTO dbo.RACUN (NOSILAC_RACUNA, BROJ_RACUNA, AKTIVAN_RACUN, ONLINE_BANKING) VALUES ('Pera Peric', '340-0003424224-42', 1, 1);
INSERT INTO dbo.RACUN (NOSILAC_RACUNA, BROJ_RACUNA, AKTIVAN_RACUN, ONLINE_BANKING) VALUES ('Fakultet Tehnickih Nauka', '840-000666435-23', 1, 0);

-- unos uplatnica
-- ===========

-- Peri Pericu uplacuje Miodrag Peric 123.5 
INSERT INTO dbo.UPLATNICA (ID_RACUNA, IZNOS_UPLATE, DATUM_PROMETA, SVRHA_UPLATE, UPLATILAC, HITNO) VALUES (1, 123.5, '2017-05-11', 'Naknada','Miodrag Peric', 0);

-- FTN isplacuje jednog zaposlenog, i uplacuje mu jedan student
-- isplacuje zaposlenog
INSERT INTO dbo.UPLATNICA (ID_RACUNA, IZNOS_UPLATE, DATUM_PROMETA, SVRHA_UPLATE, UPLATILAC, HITNO) VALUES (2, -400.0, '2017-11-23', 'Isplata mesecnih prihoda', 'Branislav Lazarevic', 0);

-- uplacuje mu student
INSERT INTO dbo.UPLATNICA (ID_RACUNA, IZNOS_UPLATE, DATUM_PROMETA, SVRHA_UPLATE, UPLATILAC, HITNO) VALUES (2, 200.0, '2017-02-27', 'Naknada za ispite', 'Studentko Studenkovic', 0);
