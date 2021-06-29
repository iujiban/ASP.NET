CREATE TABLE [dbo].[ERPDemo] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [User_Id]      NVARCHAR (20) NOT NULL,
    [Recipient]    NVARCHAR (20) NOT NULL,
    [PhoneNumber]  NVARCHAR (15) NOT NULL,
    [businessName] NVARCHAR (25) NOT NULL,
    [ServiceLevel] NVARCHAR (20) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

