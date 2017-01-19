/*==============================================================*/
/* DBMS name:      MySQL 5.0                                    */
/* Created on:     2011-3-2 14:49:37                            */
/*==============================================================*/
drop database if exists SEGMC;

/*==============================================================*/
/* Database: SEGMC                                              */
/*==============================================================*/
create database SEGMC;

use SEGMC;

drop table if exists tADMIN;

drop table if exists tCP_INF;

drop table if exists tCP_RECORD;

drop table if exists tUSER;

/*==============================================================*/
/* Table: tADMIN                                                */
/*==============================================================*/
create table tADMIN
(
   user_id              int not null auto_increment,
   user_name            varchar(30),
   user_pwd             varchar(15),
   del			int,
   test                 char(10),
   primary key (user_id)
);

/*==============================================================*/
/* Table: tCP_INF                                               */
/*==============================================================*/
create table tCP_INF
(
   cp_inf_id            int not null auto_increment,
   user_id              int,
   cp_inf_user_type     bool,
   cp_inf_peop_numb     int,
   cp_inf_avilable_num  int,
   cp_inf_sex           varchar(30),
   cp_inf_publish_time  datetime,
   cp_inf_start         varchar(200),
   cp_inf_scoordinate   varchar(200),
   cp_inf_end           varchar(200),
   cp_inf_ecoordinate   varchar(200),
   cp_inf_depart_time   varchar(100),
   cp_inf_status        varchar(30),
   cp_inf_remark        varchar(10000),
   del                  int,
   cp_inf_frequency     varchar(30),
   Test3                char(10),
   cp_register_time     datetime,
   cp_register_main_ID  int,
   cp_register_ID1      int,
   cp_register_ID2      int,
   cp_register_ID3      int,
   primary key (cp_inf_id)
);

/*==============================================================*/
/* Table: tCP_RECORD                                            */
/*==============================================================*/
create table tCP_RECORD
(
   cp_record_id         int not null auto_increment,
   user_id              int,
   cp_inf_id            int,
   cp_record_user_type  bool,
   cp_record_time       int,
   cp_record_status     datetime,
   cp_record_comment    int,
   del                  int,
   Test2                varchar(1000),
   Test3                char(10),
   primary key (cp_record_id)
);

/*==============================================================*/
/* Table: tUSER                                                 */
/*==============================================================*/
create table tUSER
(
   user_id              int not null auto_increment,
   user_name            varchar(30),
   user_nickname        varchar(30),
   user_pwd             varchar(30),
   user_sex             varchar(30),
   user_regTime         datetime,
   user_loginTimes      int,
   user_goodTimes       int,
   user_badTimes        int,
   user_level           int,
   user_email           varchar(30),
   user_successTimes    int,
   user_contact         varchar(30),
   user_profile         varchar(30),
   user_indr            varchar(1000),
   del                  int,
   test                 char(10),
   primary key (user_id)
);

alter table tCP_INF add constraint FK_Relationship_3 foreign key (user_id)
      references tUSER (user_id) on delete restrict on update restrict;

alter table tCP_RECORD add constraint FK_Relationship_2 foreign key (cp_inf_id)
      references tCP_INF (cp_inf_id) on delete restrict on update restrict;

alter table tCP_RECORD add constraint FK_Relationship_1 foreign key (user_id)
      references tUSER (user_id) on delete restrict on update restrict;

