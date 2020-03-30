create table Historico(
	Id serial primary key,
	BossName varchar(70),
	KilledDate date,
	InsertionDate date
);

alter table historico add Server varchar(30);
