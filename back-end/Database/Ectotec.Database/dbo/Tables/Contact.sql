CREATE TABLE [dbo].[Contact] (
    [Id]          BIGINT        IDENTITY (1, 1) NOT NULL,
    [FullName]    VARCHAR (100) NOT NULL,
    [Email]       VARCHAR (50)  NOT NULL,
    [PhoneNumber] VARCHAR (20)  NOT NULL,
    [ContactDate] DATETIME      NOT NULL,
    [Country]     VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED ([Id] ASC)
);

