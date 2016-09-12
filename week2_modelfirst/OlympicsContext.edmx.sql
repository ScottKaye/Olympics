
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/12/2016 15:24:09
-- Generated from EDMX file: C:\Users\Scott\documents\visual studio 2015\Projects\week2_modelfirst\week2_modelfirst\OlympicsContext.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Olympics];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_AthleteSport_Athlete]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AthleteSport] DROP CONSTRAINT [FK_AthleteSport_Athlete];
GO
IF OBJECT_ID(N'[dbo].[FK_AthleteSport_Sport]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AthleteSport] DROP CONSTRAINT [FK_AthleteSport_Sport];
GO
IF OBJECT_ID(N'[dbo].[FK_CountryAthlete]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Athletes] DROP CONSTRAINT [FK_CountryAthlete];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Athletes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Athletes];
GO
IF OBJECT_ID(N'[dbo].[AthleteSport]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AthleteSport];
GO
IF OBJECT_ID(N'[dbo].[Countries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Countries];
GO
IF OBJECT_ID(N'[dbo].[Sports]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sports];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Athletes'
CREATE TABLE [dbo].[Athletes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [Age] int  NOT NULL,
    [Gender] nvarchar(max)  NOT NULL,
    [Country_Id] int  NOT NULL
);
GO

-- Creating table 'Countries'
CREATE TABLE [dbo].[Countries] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Population] int  NOT NULL
);
GO

-- Creating table 'Sports'
CREATE TABLE [dbo].[Sports] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Rules] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Media'
CREATE TABLE [dbo].[Media] (
    [Id] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'AthleteSport'
CREATE TABLE [dbo].[AthleteSport] (
    [Athletes_Id] int  NOT NULL,
    [Sports_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Athletes'
ALTER TABLE [dbo].[Athletes]
ADD CONSTRAINT [PK_Athletes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Countries'
ALTER TABLE [dbo].[Countries]
ADD CONSTRAINT [PK_Countries]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Sports'
ALTER TABLE [dbo].[Sports]
ADD CONSTRAINT [PK_Sports]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Media'
ALTER TABLE [dbo].[Media]
ADD CONSTRAINT [PK_Media]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Athletes_Id], [Sports_Id] in table 'AthleteSport'
ALTER TABLE [dbo].[AthleteSport]
ADD CONSTRAINT [PK_AthleteSport]
    PRIMARY KEY CLUSTERED ([Athletes_Id], [Sports_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Country_Id] in table 'Athletes'
ALTER TABLE [dbo].[Athletes]
ADD CONSTRAINT [FK_CountryAthlete]
    FOREIGN KEY ([Country_Id])
    REFERENCES [dbo].[Countries]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CountryAthlete'
CREATE INDEX [IX_FK_CountryAthlete]
ON [dbo].[Athletes]
    ([Country_Id]);
GO

-- Creating foreign key on [Athletes_Id] in table 'AthleteSport'
ALTER TABLE [dbo].[AthleteSport]
ADD CONSTRAINT [FK_AthleteSport_Athletes]
    FOREIGN KEY ([Athletes_Id])
    REFERENCES [dbo].[Athletes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Sports_Id] in table 'AthleteSport'
ALTER TABLE [dbo].[AthleteSport]
ADD CONSTRAINT [FK_AthleteSport_Sports]
    FOREIGN KEY ([Sports_Id])
    REFERENCES [dbo].[Sports]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AthleteSport_Sports'
CREATE INDEX [IX_FK_AthleteSport_Sports]
ON [dbo].[AthleteSport]
    ([Sports_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------