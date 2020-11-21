CREATE TABLE [Status]
(
    [Id] [int] NOT NULL identity(1,1),
    [Name] [nvarchar] (40) NOT NULL

    CONSTRAINT PK_Status_Id Primary key ([Id]),
    CONSTRAINT UQ_Status_Name UNIQUE ([Name])
);

INSERT INTO [Status] VALUES
    ('Created'),
    ('Ready'),
    ('Sent'),
    ('Failed')

CREATE TABLE [Contact]
(
    [Id] [int] NOT NULL identity(1,1),
    [Name] [nvarchar] (32) NOT NULL

    CONSTRAINT PK_Contact_Id Primary key ([Id]),
    CONSTRAINT UQ_Contact_Name UNIQUE ([Name])
);

INSERT INTO [Contact] VALUES
    ('Id223451'),
    ('ABIR12345'),
    ('PS3811123'),
    ('LOTR9291923')

CREATE table [Storage]
(
    [Id] [int] not NULL identity(1,1),
    [Guid] [char] (32) NOT NULL ,
    [DateTime] [datetimeoffset] NOT NULL,
    [StatusId] [int] NOT NULL,
    [Message] [nvarchar] (512) NULL,
    [ContactId] [int] NOT NULL

    CONSTRAINT PK_Storage_Id Primary key ([Id]),
    CONSTRAINT FK_StatusId_Status_Id FOREIGN KEY ([StatusId])
        REFERENCES [Status] ([Id])
            ON DELETE CASCADE
            ON UPDATE CASCADE,
    CONSTRAINT FK_ContactId_Contact_Id FOREIGN KEY ([ContactId])
        REFERENCES [Contact] ([Id])
            ON DELETE CASCADE
            On UPDATE CASCADE
);

insert into [Storage] VALUES
    ('22345200abe84f6090c80d43c5f6c0f6', '2020-12-17 11:19:20 +03:00', 1, 'Не забыть сходить в рейд', 1),
    ('22345200ase84s6000c80d43c5f6c0f6', '2020-11-22 11:19:20 +03:00', 2, 'Купить подарки на новый год', 3),
    ('22345200abe84s1050c86d47c5f2c0f6', '2020-11-14 11:19:20 +03:00', 3, 'Оплатить интернет', 2),
    ('22323200abe84f6090c80d43c5f6c0f6', '2020-12-17 11:19:20 +03:00', 4, 'Поздравить с др друга', 4)

select * from [Status]
select * from [Contact]
select * from [Storage]

drop table [storage]