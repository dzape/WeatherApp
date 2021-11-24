CREATE TABLE [dbo].[Accounts] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [Username]     NVARCHAR (MAX) NULL,
    [Password]     NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED ([Id] ASC)
);

