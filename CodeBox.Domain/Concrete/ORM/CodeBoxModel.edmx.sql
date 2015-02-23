
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 08/13/2012 01:59:29
-- Generated from EDMX file: d:\userdata tower\tower documents\visual studio 2010\Projects\CodeBox\CodeBox.Domain\Concrete\ORM\CodeBoxModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [CodeBox];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UserSnippet]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Snippets] DROP CONSTRAINT [FK_UserSnippet];
GO
IF OBJECT_ID(N'[dbo].[FK_UserRole_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [FK_UserRole_User];
GO
IF OBJECT_ID(N'[dbo].[FK_UserRole_Role]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [FK_UserRole_Role];
GO
IF OBJECT_ID(N'[dbo].[FK_SnippetLanguage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Snippets] DROP CONSTRAINT [FK_SnippetLanguage];
GO
IF OBJECT_ID(N'[dbo].[FK_GroupUser_Group]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GroupUser] DROP CONSTRAINT [FK_GroupUser_Group];
GO
IF OBJECT_ID(N'[dbo].[FK_GroupUser_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GroupUser] DROP CONSTRAINT [FK_GroupUser_User];
GO
IF OBJECT_ID(N'[dbo].[FK_GroupSnippet_Group]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GroupSnippet] DROP CONSTRAINT [FK_GroupSnippet_Group];
GO
IF OBJECT_ID(N'[dbo].[FK_GroupSnippet_Snippet]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GroupSnippet] DROP CONSTRAINT [FK_GroupSnippet_Snippet];
GO
IF OBJECT_ID(N'[dbo].[FK_GroupAdminUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GroupAdmins] DROP CONSTRAINT [FK_GroupAdminUser];
GO
IF OBJECT_ID(N'[dbo].[FK_GroupAdminGroup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GroupAdmins] DROP CONSTRAINT [FK_GroupAdminGroup];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Snippets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Snippets];
GO
IF OBJECT_ID(N'[dbo].[Languages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Languages];
GO
IF OBJECT_ID(N'[dbo].[Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles];
GO
IF OBJECT_ID(N'[dbo].[Groups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Groups];
GO
IF OBJECT_ID(N'[dbo].[GroupAdmins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GroupAdmins];
GO
IF OBJECT_ID(N'[dbo].[UserRole]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserRole];
GO
IF OBJECT_ID(N'[dbo].[GroupUser]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GroupUser];
GO
IF OBJECT_ID(N'[dbo].[GroupSnippet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GroupSnippet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NULL,
    [Surname] nvarchar(50)  NULL,
    [Username] nvarchar(50)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [LastSeen] datetime  NULL,
    [Mail] nvarchar(max)  NOT NULL,
    [passwordQuestion] nvarchar(100)  NULL,
    [Comment] nvarchar(max)  NULL,
    [Approved] bit  NOT NULL,
    [LockedOut] bit  NOT NULL,
    [CreationDate] datetime  NOT NULL,
    [lastLoginDate] datetime  NULL,
    [LastPasswordChangeDate] datetime  NULL,
    [LastLockOutDate] datetime  NULL,
    [ProviderName] nvarchar(50)  NOT NULL,
    [LastActivityDate] datetime  NULL,
    [ImageData] varbinary(max)  NULL,
    [ImageMimeType] nvarchar(50)  NULL
);
GO

-- Creating table 'Snippets'
CREATE TABLE [dbo].[Snippets] (
    [SnippetId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(500)  NULL,
    [Description] nvarchar(max)  NULL,
    [CreationDate] datetime  NULL,
    [Code] nvarchar(max)  NULL,
    [ModifiedDate] datetime  NULL,
    [Public] bit  NOT NULL,
    [UserId] int  NOT NULL,
    [LanguageId] int  NOT NULL
);
GO

-- Creating table 'Languages'
CREATE TABLE [dbo].[Languages] (
    [LanguageId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [InfoUrl] nvarchar(max)  NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [RoleId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NULL
);
GO

-- Creating table 'Groups'
CREATE TABLE [dbo].[Groups] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [ImageData] varbinary(max)  NULL,
    [ImageMimeType] nvarchar(max)  NULL
);
GO

-- Creating table 'GroupAdmins'
CREATE TABLE [dbo].[GroupAdmins] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [GroupId] int  NOT NULL,
    [User_UserId] int  NOT NULL
);
GO

-- Creating table 'UserRole'
CREATE TABLE [dbo].[UserRole] (
    [Users_UserId] int  NOT NULL,
    [Roles_RoleId] int  NOT NULL
);
GO

-- Creating table 'GroupUser'
CREATE TABLE [dbo].[GroupUser] (
    [Groups_Id] int  NOT NULL,
    [Users_UserId] int  NOT NULL
);
GO

-- Creating table 'GroupSnippet'
CREATE TABLE [dbo].[GroupSnippet] (
    [Groups_Id] int  NOT NULL,
    [Snippets_SnippetId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [UserId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [SnippetId] in table 'Snippets'
ALTER TABLE [dbo].[Snippets]
ADD CONSTRAINT [PK_Snippets]
    PRIMARY KEY CLUSTERED ([SnippetId] ASC);
GO

-- Creating primary key on [LanguageId] in table 'Languages'
ALTER TABLE [dbo].[Languages]
ADD CONSTRAINT [PK_Languages]
    PRIMARY KEY CLUSTERED ([LanguageId] ASC);
GO

-- Creating primary key on [RoleId] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([RoleId] ASC);
GO

-- Creating primary key on [Id] in table 'Groups'
ALTER TABLE [dbo].[Groups]
ADD CONSTRAINT [PK_Groups]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'GroupAdmins'
ALTER TABLE [dbo].[GroupAdmins]
ADD CONSTRAINT [PK_GroupAdmins]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Users_UserId], [Roles_RoleId] in table 'UserRole'
ALTER TABLE [dbo].[UserRole]
ADD CONSTRAINT [PK_UserRole]
    PRIMARY KEY NONCLUSTERED ([Users_UserId], [Roles_RoleId] ASC);
GO

-- Creating primary key on [Groups_Id], [Users_UserId] in table 'GroupUser'
ALTER TABLE [dbo].[GroupUser]
ADD CONSTRAINT [PK_GroupUser]
    PRIMARY KEY NONCLUSTERED ([Groups_Id], [Users_UserId] ASC);
GO

-- Creating primary key on [Groups_Id], [Snippets_SnippetId] in table 'GroupSnippet'
ALTER TABLE [dbo].[GroupSnippet]
ADD CONSTRAINT [PK_GroupSnippet]
    PRIMARY KEY NONCLUSTERED ([Groups_Id], [Snippets_SnippetId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'Snippets'
ALTER TABLE [dbo].[Snippets]
ADD CONSTRAINT [FK_UserSnippet]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserSnippet'
CREATE INDEX [IX_FK_UserSnippet]
ON [dbo].[Snippets]
    ([UserId]);
GO

-- Creating foreign key on [Users_UserId] in table 'UserRole'
ALTER TABLE [dbo].[UserRole]
ADD CONSTRAINT [FK_UserRole_User]
    FOREIGN KEY ([Users_UserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Roles_RoleId] in table 'UserRole'
ALTER TABLE [dbo].[UserRole]
ADD CONSTRAINT [FK_UserRole_Role]
    FOREIGN KEY ([Roles_RoleId])
    REFERENCES [dbo].[Roles]
        ([RoleId])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserRole_Role'
CREATE INDEX [IX_FK_UserRole_Role]
ON [dbo].[UserRole]
    ([Roles_RoleId]);
GO

-- Creating foreign key on [LanguageId] in table 'Snippets'
ALTER TABLE [dbo].[Snippets]
ADD CONSTRAINT [FK_SnippetLanguage]
    FOREIGN KEY ([LanguageId])
    REFERENCES [dbo].[Languages]
        ([LanguageId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SnippetLanguage'
CREATE INDEX [IX_FK_SnippetLanguage]
ON [dbo].[Snippets]
    ([LanguageId]);
GO

-- Creating foreign key on [Groups_Id] in table 'GroupUser'
ALTER TABLE [dbo].[GroupUser]
ADD CONSTRAINT [FK_GroupUser_Group]
    FOREIGN KEY ([Groups_Id])
    REFERENCES [dbo].[Groups]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Users_UserId] in table 'GroupUser'
ALTER TABLE [dbo].[GroupUser]
ADD CONSTRAINT [FK_GroupUser_User]
    FOREIGN KEY ([Users_UserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GroupUser_User'
CREATE INDEX [IX_FK_GroupUser_User]
ON [dbo].[GroupUser]
    ([Users_UserId]);
GO

-- Creating foreign key on [Groups_Id] in table 'GroupSnippet'
ALTER TABLE [dbo].[GroupSnippet]
ADD CONSTRAINT [FK_GroupSnippet_Group]
    FOREIGN KEY ([Groups_Id])
    REFERENCES [dbo].[Groups]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Snippets_SnippetId] in table 'GroupSnippet'
ALTER TABLE [dbo].[GroupSnippet]
ADD CONSTRAINT [FK_GroupSnippet_Snippet]
    FOREIGN KEY ([Snippets_SnippetId])
    REFERENCES [dbo].[Snippets]
        ([SnippetId])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GroupSnippet_Snippet'
CREATE INDEX [IX_FK_GroupSnippet_Snippet]
ON [dbo].[GroupSnippet]
    ([Snippets_SnippetId]);
GO

-- Creating foreign key on [User_UserId] in table 'GroupAdmins'
ALTER TABLE [dbo].[GroupAdmins]
ADD CONSTRAINT [FK_GroupAdminUser]
    FOREIGN KEY ([User_UserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GroupAdminUser'
CREATE INDEX [IX_FK_GroupAdminUser]
ON [dbo].[GroupAdmins]
    ([User_UserId]);
GO

-- Creating foreign key on [GroupId] in table 'GroupAdmins'
ALTER TABLE [dbo].[GroupAdmins]
ADD CONSTRAINT [FK_GroupAdminGroup]
    FOREIGN KEY ([GroupId])
    REFERENCES [dbo].[Groups]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GroupAdminGroup'
CREATE INDEX [IX_FK_GroupAdminGroup]
ON [dbo].[GroupAdmins]
    ([GroupId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------