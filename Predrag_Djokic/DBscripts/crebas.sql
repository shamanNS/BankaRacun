/*==============================================================*/
/*  IME BAZE JE "AlephDB", videti Web.config fajl

DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     12.06.2017 17:07:57                          */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('UPLATNICA') and o.name = 'FK_UPLATNIC_POSEDUJE_RACUN')
alter table UPLATNICA
   drop constraint FK_UPLATNIC_POSEDUJE_RACUN
go

if exists (select 1
            from  sysobjects
           where  id = object_id('RACUN')
            and   type = 'U')
   drop table RACUN
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('UPLATNICA')
            and   name  = 'POSEDUJE_FK'
            and   indid > 0
            and   indid < 255)
   drop index UPLATNICA.POSEDUJE_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('UPLATNICA')
            and   type = 'U')
   drop table UPLATNICA
go

/*==============================================================*/
/* Table: RACUN                                                 */
/*==============================================================*/
create table RACUN (
   ID_RACUNA            int                  identity,
   NOSILAC_RACUNA       varchar(150)         null,
   BROJ_RACUNA          varchar(100)         null,
   AKTIVAN_RACUN        bit                  null,
   ONLINE_BANKING       bit                  null,
   constraint PK_RACUN primary key nonclustered (ID_RACUNA)
)
go

/*==============================================================*/
/* Table: UPLATNICA                                             */
/*==============================================================*/
create table UPLATNICA (
   ID_UPLATNICE         int                  identity,
   ID_RACUNA            int                  not null,
   IZNOS_UPLATE         numeric(15,2)        null,
   DATUM_PROMETA        datetime             null,
   SVRHA_UPLATE         varchar(200)         null,
   UPLATILAC            varchar(200)         null,
   HITNO                bit                  null,
   constraint PK_UPLATNICA primary key nonclustered (ID_UPLATNICE)
)
go

/*==============================================================*/
/* Index: POSEDUJE_FK                                           */
/*==============================================================*/
create index POSEDUJE_FK on UPLATNICA (
ID_RACUNA ASC
)
go

alter table UPLATNICA
   add constraint FK_UPLATNIC_POSEDUJE_RACUN foreign key (ID_RACUNA)
      references RACUN (ID_RACUNA)
go

