
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/05/2018 11:38:17
-- Generated from EDMX file: C:\Users\navraj.singh\Desktop\VP\VP04OCt2018\vendorportal2\VendorPortal\Models\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [VendorPortal];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserRoles_dbo_AspNetRoles_RoleId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo_AspNetUserRoles_dbo_AspNetRoles_RoleId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserRoles_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo_AspNetUserRoles_dbo_AspNetUsers_UserId];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[__MigrationHistory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[__MigrationHistory];
GO
IF OBJECT_ID(N'[dbo].[AspNetRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserClaims]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserClaims];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserLogins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserLogins];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[INVOICE]', 'U') IS NOT NULL
    DROP TABLE [dbo].[INVOICE];
GO
IF OBJECT_ID(N'[dbo].[INVOICE_DETAILS]', 'U') IS NOT NULL
    DROP TABLE [dbo].[INVOICE_DETAILS];
GO
IF OBJECT_ID(N'[dbo].[PO]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PO];
GO
IF OBJECT_ID(N'[dbo].[PO_DETAILS]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PO_DETAILS];
GO
IF OBJECT_ID(N'[dbo].[Schedules]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Schedules];
GO
IF OBJECT_ID(N'[dbo].[SUPPLIER_MASTER]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SUPPLIER_MASTER];
GO
IF OBJECT_ID(N'[dbo].[tblApprovedPackingStandard]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblApprovedPackingStandard];
GO
IF OBJECT_ID(N'[dbo].[tblFileMetaData]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblFileMetaData];
GO
IF OBJECT_ID(N'[dbo].[tblGRN]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblGRN];
GO
IF OBJECT_ID(N'[dbo].[tblInvoiceRejecttion]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblInvoiceRejecttion];
GO
IF OBJECT_ID(N'[dbo].[tblMaterial]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblMaterial];
GO
IF OBJECT_ID(N'[dbo].[tblUnit]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblUnit];
GO
IF OBJECT_ID(N'[VendorPortalModelStoreContainer].[tblUserRole]', 'U') IS NOT NULL
    DROP TABLE [VendorPortalModelStoreContainer].[tblUserRole];
GO
IF OBJECT_ID(N'[VendorPortalModelStoreContainer].[VENDOR_ASSOC]', 'U') IS NOT NULL
    DROP TABLE [VendorPortalModelStoreContainer].[VENDOR_ASSOC];
GO
IF OBJECT_ID(N'[VendorPortalModelStoreContainer].[POList]', 'U') IS NOT NULL
    DROP TABLE [VendorPortalModelStoreContainer].[POList];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'C__MigrationHistory'
CREATE TABLE [dbo].[C__MigrationHistory] (
    [MigrationId] nvarchar(150)  NOT NULL,
    [ContextKey] nvarchar(300)  NOT NULL,
    [Model] varbinary(max)  NOT NULL,
    [ProductVersion] nvarchar(32)  NOT NULL
);
GO

-- Creating table 'AspNetRoles'
CREATE TABLE [dbo].[AspNetRoles] (
    [Id] nvarchar(128)  NOT NULL,
    [Name] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'AspNetUserClaims'
CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(128)  NOT NULL,
    [ClaimType] nvarchar(max)  NULL,
    [ClaimValue] nvarchar(max)  NULL
);
GO

-- Creating table 'AspNetUserLogins'
CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider] nvarchar(128)  NOT NULL,
    [ProviderKey] nvarchar(128)  NOT NULL,
    [UserId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'AspNetUsers'
CREATE TABLE [dbo].[AspNetUsers] (
    [Id] nvarchar(128)  NOT NULL,
    [Email] nvarchar(256)  NULL,
    [EmailConfirmed] bit  NOT NULL,
    [PasswordHash] nvarchar(max)  NULL,
    [SecurityStamp] nvarchar(max)  NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [PhoneNumberConfirmed] bit  NOT NULL,
    [TwoFactorEnabled] bit  NOT NULL,
    [LockoutEndDateUtc] datetime  NULL,
    [LockoutEnabled] bit  NOT NULL,
    [AccessFailedCount] int  NOT NULL,
    [UserName] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'INVOICEs'
CREATE TABLE [dbo].[INVOICEs] (
    [ID] bigint IDENTITY(1,1) NOT NULL,
    [INVOICE_NUMBER] nvarchar(100)  NULL,
    [SUPP_CODE] nvarchar(150)  NULL,
    [SUPPLIER_GSTIN] nvarchar(150)  NULL,
    [SUPPLIER_CIN] nchar(10)  NULL,
    [INVOICE_DATE] datetime  NULL,
    [POID] bigint  NULL,
    [HSN_SAC_NO] nvarchar(150)  NULL,
    [CREATEDBY] int  NULL,
    [CREATEDON] datetime  NULL
);
GO

-- Creating table 'INVOICE_DETAILS'
CREATE TABLE [dbo].[INVOICE_DETAILS] (
    [ID] bigint IDENTITY(1,1) NOT NULL,
    [INV_ID] bigint  NULL,
    [MAT_CODE] nvarchar(50)  NULL,
    [QTY] decimal(12,2)  NULL,
    [RATE] decimal(12,2)  NULL,
    [AMOUNT] decimal(12,2)  NULL,
    [CGST] decimal(12,2)  NULL,
    [CGST_AMT] decimal(12,2)  NULL,
    [IGST] decimal(12,2)  NULL,
    [IGST_AMT] decimal(12,2)  NULL,
    [SGST] decimal(12,2)  NULL,
    [SGST_IMT] decimal(12,2)  NULL,
    [CESS] decimal(12,2)  NULL,
    [TOTAL_VAL_INC] decimal(12,2)  NULL,
    [TOTAL_VAL_EXC] decimal(12,2)  NULL,
    [CREATED_BY] int  NULL,
    [CREATED_ON] datetime  NULL,
    [STATUS] int  NULL,
    [MODEL] nvarchar(150)  NULL,
    [MFG_DATE] datetime  NULL,
    [BATCHCODE] nvarchar(150)  NULL,
    [CALC_VALUE_EXC] decimal(12,2)  NULL,
    [CALC_VALUE_INC] decimal(12,2)  NULL,
    [SCHEDULE_NO] nvarchar(150)  NULL,
    [DEL_DATE] datetime  NULL,
    [CONTRACT_NO] nvarchar(150)  NULL,
    [POSITION_NO] nvarchar(500)  NULL,
    [CGST_PER] decimal(12,2)  NULL,
    [SGST_PER] decimal(12,2)  NULL,
    [IGST_PER] decimal(12,2)  NULL,
    [SCH_ID] bigint  NULL
);
GO

-- Creating table 'Plant_Master'
CREATE TABLE [dbo].[Plant_Master] (
    [PlantId] int IDENTITY(1,1) NOT NULL,
    [Plant_Code] varchar(100)  NULL,
    [Plant_Name] varchar(200)  NULL
);
GO

-- Creating table 'POes'
CREATE TABLE [dbo].[POes] (
    [ID] bigint IDENTITY(1,1) NOT NULL,
    [PO_NUM] nvarchar(80)  NULL,
    [PO_DATE] datetime  NULL,
    [SUPP_CODE] nvarchar(128)  NULL,
    [STATUS] int  NULL,
    [ACC_REJ_Remark] varchar(500)  NULL,
    [ACC_REJ_DateTime] datetime  NULL,
    [STATUS1] int  NULL,
    [ACC_REJ_Remark1] varchar(500)  NULL,
    [ACC_REJ_DateTime1] datetime  NULL
);
GO

-- Creating table 'PO_DETAILS'
CREATE TABLE [dbo].[PO_DETAILS] (
    [ID] bigint IDENTITY(1,1) NOT NULL,
    [PO_NUM] nvarchar(20)  NULL,
    [PLANT_CODE] nvarchar(20)  NULL,
    [MaterialCode] nvarchar(128)  NULL,
    [HSN_SAC] nvarchar(50)  NULL,
    [GL_ACC] nvarchar(50)  NULL,
    [QUANTITY] decimal(18,3)  NULL,
    [UOM] nvarchar(50)  NULL,
    [UNIT_RATE] decimal(18,3)  NULL,
    [DISCOUNT] decimal(18,3)  NULL,
    [NET_AMOUNT] decimal(18,3)  NULL,
    [FREIGHT] decimal(18,3)  NULL,
    [PF] decimal(18,3)  NULL,
    [CGST] decimal(18,3)  NULL,
    [SGST] decimal(18,3)  NULL,
    [IGST] decimal(18,3)  NULL,
    [CESS] decimal(18,3)  NULL,
    [TOTAL_VALUE_INC] decimal(18,3)  NULL,
    [TOTAL_VALUE_EXC] decimal(18,3)  NULL,
    [CREATEDON] datetime  NULL,
    [STATUS] int  NULL,
    [ACC_REJ_BY] nvarchar(20)  NULL,
    [ACC_REJ_ON] datetime  NULL,
    [ACC_REJ_REMARKS] nvarchar(100)  NULL
);
GO

-- Creating table 'Schedules'
CREATE TABLE [dbo].[Schedules] (
    [ID] bigint IDENTITY(1,1) NOT NULL,
    [POID] bigint  NULL,
    [MAT_CODE] nvarchar(100)  NULL,
    [Position] nvarchar(100)  NULL,
    [Qty] decimal(18,2)  NULL,
    [DEL_DATE] datetime  NULL,
    [PLANT_CODE] nvarchar(100)  NULL,
    [CONTRACT_NO] nvarchar(100)  NULL,
    [SCHEDULE_NO] nvarchar(100)  NULL,
    [Status] int  NULL,
    [REMARKS] nvarchar(500)  NULL,
    [IS_BILL_GENERATED] varchar(1)  NULL,
    [CreatedOn] datetime  NULL,
    [CreatedBy] int  NULL,
    [UpdatedOn] datetime  NULL,
    [UpdatedBy] int  NULL,
    [UploadDate] datetime  NULL
);
GO

-- Creating table 'SUPPLIER_MASTER'
CREATE TABLE [dbo].[SUPPLIER_MASTER] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [SUPP_CODE] nvarchar(256)  NULL,
    [SUPP_TYPE] varchar(100)  NULL,
    [PURCHASING_RELATED] varchar(10)  NULL,
    [SUPP_NAME] varchar(200)  NULL,
    [ADDRESS1] varchar(100)  NULL,
    [ADDRESS2] varchar(100)  NULL,
    [ADDRESS3] varchar(100)  NULL,
    [CITY] varchar(100)  NULL,
    [PIN] varchar(10)  NULL,
    [DISTRICT] varchar(50)  NULL,
    [STATE] varchar(50)  NULL,
    [COUNTRY] varchar(50)  NULL,
    [COUNTRY_EXTRA] varchar(50)  NULL,
    [CIN] varchar(100)  NULL,
    [VATREG_NO1] varchar(100)  NULL,
    [VATREG_NO2] varchar(100)  NULL,
    [TIN2_1] varchar(100)  NULL,
    [TIN2_2] varchar(100)  NULL,
    [GSTIN] varchar(100)  NULL,
    [COMM_TEL1] varchar(100)  NULL,
    [COMM_TEL2] varchar(100)  NULL,
    [COMM_TEL3] varchar(100)  NULL,
    [COMM_FAX] varchar(100)  NULL,
    [COMM_EMAIL] varchar(100)  NULL,
    [PAYMENT_TERM] varchar(100)  NULL,
    [PAYMENT_METHOD] varchar(100)  NULL,
    [BANK_NAME] varchar(100)  NULL,
    [BANK_ADDRESS1] varchar(100)  NULL,
    [BANK_ADDRESS2] varchar(100)  NULL,
    [BANK_ACC_NO] varchar(100)  NULL,
    [BANK_SWIFT_CODE1] varchar(100)  NULL,
    [BANK_SWIFT_CODE2] varchar(100)  NULL,
    [BANK_SWIFT_CODE3] varchar(100)  NULL,
    [BANK_CURRENCY1] varchar(100)  NULL,
    [BANK_CURRENCY2] varchar(100)  NULL,
    [PUR_ORD_CUR] varchar(100)  NULL,
    [PUR_ORD_INCO1] varchar(100)  NULL,
    [PUR_ORD_INCO2] varchar(100)  NULL,
    [PUR_ORD_SCH_GROUP_VENDOR] varchar(100)  NULL,
    [PUR_ORD_CONTACT_PERSON] varchar(100)  NULL,
    [PUR_ORD_CONTACT_NO] varchar(100)  NULL,
    [PUR_ORD_GR_BASED_IV] varchar(100)  NULL,
    [PUR_ORD_ERS_VENDOR] varchar(100)  NULL,
    [ADDITIONAL_INFO1] varchar(100)  NULL,
    [ADDITIONAL_INFO2] varchar(100)  NULL,
    [ADDITIONAL_INFO3] varchar(100)  NULL,
    [ADDITIONAL_INFO4] varchar(100)  NULL,
    [REQUESTER_ID] varchar(100)  NULL,
    [REQUEST_DATE] datetime  NULL,
    [REQUEST_DEP] varchar(100)  NULL,
    [ISACTIVE] varchar(10)  NULL,
    [EMAIL_ID] varchar(100)  NULL,
    [CONTACT_NO] varchar(100)  NULL,
    [CreatedBy] int  NULL,
    [CreatedOn] datetime  NULL,
    [UpdatedOn] int  NULL,
    [UpdatedBy] datetime  NULL
);
GO

-- Creating table 'tblApprovedPackingStandards'
CREATE TABLE [dbo].[tblApprovedPackingStandards] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [SupplierName] nvarchar(256)  NOT NULL,
    [MaterialCode] varchar(100)  NOT NULL,
    [FileName] varchar(50)  NOT NULL,
    [UploadDate] datetime  NOT NULL,
    [Status] bit  NOT NULL,
    [CreatedOn] datetime  NOT NULL,
    [CreatedBy] nvarchar(128)  NULL,
    [ModifyBy] nvarchar(128)  NULL,
    [ModifiedOn] datetime  NULL
);
GO

-- Creating table 'tblCities'
CREATE TABLE [dbo].[tblCities] (
    [CityId] int IDENTITY(1,1) NOT NULL,
    [CityName] varchar(100)  NOT NULL,
    [StateName] varchar(100)  NOT NULL,
    [CreatedBy] int  NOT NULL,
    [CreatedOn] datetime  NOT NULL,
    [UpdatedBy] int  NULL,
    [UpdatedOn] datetime  NULL,
    [Active] bit  NOT NULL,
    [StateId] bigint  NULL
);
GO

-- Creating table 'tblFileMetaDatas'
CREATE TABLE [dbo].[tblFileMetaDatas] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [FileName] varchar(200)  NOT NULL,
    [FileType] varchar(10)  NOT NULL,
    [Location] nvarchar(100)  NOT NULL,
    [DocumentType] varchar(50)  NOT NULL,
    [UploadDate] datetime  NOT NULL,
    [LastModified] datetime  NULL,
    [ValidTill] datetime  NULL,
    [SupplierId] bigint  NULL,
    [SupplierName] nvarchar(256)  NULL,
    [MaterialId] int  NULL,
    [MaterialCode] varchar(100)  NULL,
    [Status] bit  NOT NULL,
    [Active] int  NOT NULL,
    [UploadedBy] nvarchar(256)  NOT NULL,
    [Tags] varchar(50)  NULL,
    [Field1] varchar(50)  NULL,
    [Field2] varchar(50)  NULL,
    [Field3] varchar(50)  NULL,
    [Field4] varchar(50)  NULL,
    [Field5] varchar(50)  NULL
);
GO

-- Creating table 'tblLineStopages'
CREATE TABLE [dbo].[tblLineStopages] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [MatCode] varchar(100)  NULL,
    [Instance] varchar(10)  NULL,
    [DurationFrom] datetime  NULL,
    [DurationTo] datetime  NULL,
    [ReasonNo] varchar(10)  NULL,
    [ReasonDescription] varchar(255)  NULL,
    [Remarks] varchar(255)  NULL,
    [UploadDate] datetime  NULL,
    [UploadedBy] nvarchar(256)  NULL,
    [ValidTill] datetime  NULL,
    [Active] int  NOT NULL
);
GO

-- Creating table 'tblMaterials'
CREATE TABLE [dbo].[tblMaterials] (
    [MaterialID] int IDENTITY(1,1) NOT NULL,
    [MaterialCode] varchar(100)  NULL,
    [MaterialDescription] varchar(200)  NULL,
    [Unit] varchar(20)  NULL,
    [MaterialGroup] varchar(100)  NULL,
    [SNP] int  NULL,
    [CreatedBy] int  NULL,
    [CreatedOn] datetime  NULL,
    [UpdatedBy] int  NULL,
    [UpdatedOn] datetime  NULL,
    [Active] bit  NULL
);
GO

-- Creating table 'tblPPMs'
CREATE TABLE [dbo].[tblPPMs] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Plant] varchar(20)  NULL,
    [SuppCode] nvarchar(256)  NULL,
    [SuppName] nvarchar(256)  NULL,
    [FinYear] varchar(20)  NULL,
    [Apr] decimal(12,2)  NULL,
    [May] decimal(12,2)  NULL,
    [Jun] decimal(12,2)  NULL,
    [Jul] decimal(12,2)  NULL,
    [Aug] decimal(12,2)  NULL,
    [Sep] decimal(12,2)  NULL,
    [Oct] decimal(12,2)  NULL,
    [Nov] decimal(12,2)  NULL,
    [Dec] decimal(12,2)  NULL,
    [Jan] decimal(12,2)  NULL,
    [Feb] decimal(12,2)  NULL,
    [Mar] decimal(12,2)  NULL,
    [CummPPM] decimal(12,2)  NULL,
    [UploadDate] datetime  NULL,
    [UploadedBy] nvarchar(256)  NULL,
    [ValidTill] datetime  NULL,
    [Active] int  NOT NULL
);
GO

-- Creating table 'tblRatings'
CREATE TABLE [dbo].[tblRatings] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [CummRank] varchar(20)  NULL,
    [SuppCode] nvarchar(256)  NOT NULL,
    [MDeliveryRating] decimal(10,2)  NULL,
    [MQualityRating] decimal(10,2)  NULL,
    [MOverallRating] decimal(10,2)  NULL,
    [CDeliveryRating] decimal(10,2)  NULL,
    [CQualityRating] decimal(10,2)  NULL,
    [COverallRating] decimal(10,2)  NULL,
    [Grade] varchar(10)  NULL,
    [UploadDate] datetime  NULL,
    [UploadedBy] nvarchar(256)  NULL,
    [ValidTill] datetime  NULL,
    [Active] int  NOT NULL
);
GO

-- Creating table 'tblUnits'
CREATE TABLE [dbo].[tblUnits] (
    [UnitId] int IDENTITY(1,1) NOT NULL,
    [UnitCode] varchar(10)  NOT NULL,
    [UnitDescription] varchar(50)  NOT NULL,
    [CreatedBy] int  NOT NULL,
    [CreatedOn] datetime  NOT NULL,
    [UpdatedBy] int  NULL,
    [UpdatedOn] int  NULL,
    [Active] bit  NOT NULL
);
GO

-- Creating table 'tblWarrantyPartsRejecteds'
CREATE TABLE [dbo].[tblWarrantyPartsRejecteds] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [SuppCode] nvarchar(256)  NOT NULL,
    [WarrantyNumber] varchar(30)  NULL,
    [Model] varchar(30)  NULL,
    [PartCode] varchar(30)  NULL,
    [WarrantyDescription] varchar(255)  NULL,
    [WarrantyCost] decimal(12,2)  NULL,
    [UploadDate] datetime  NULL,
    [UploadedBy] nvarchar(256)  NULL,
    [ValidTill] datetime  NULL,
    [Active] int  NOT NULL
);
GO

-- Creating table 'tblUserRoles'
CREATE TABLE [dbo].[tblUserRoles] (
    [UserId] nvarchar(50)  NOT NULL,
    [RoleId] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'VENDOR_ASSOC'
CREATE TABLE [dbo].[VENDOR_ASSOC] (
    [ID] bigint IDENTITY(1,1) NOT NULL,
    [VENDOR_CODE] nvarchar(max)  NULL,
    [USER_CODE] nvarchar(max)  NULL
);
GO

-- Creating table 'POLists'
CREATE TABLE [dbo].[POLists] (
    [ID] bigint  NOT NULL,
    [PO_DATE] datetime  NULL,
    [SUPP_CODE] nvarchar(128)  NULL,
    [PLANT_CODE] nvarchar(20)  NULL,
    [PO_NUM] nvarchar(20)  NULL,
    [MaterialCode] nvarchar(128)  NULL,
    [HSN_SAC] nvarchar(50)  NULL,
    [GL_ACC] nvarchar(50)  NULL,
    [QUANTITY] decimal(18,3)  NULL,
    [UOM] nvarchar(50)  NULL,
    [UNIT_RATE] decimal(18,3)  NULL,
    [DISCOUNT] decimal(18,3)  NULL,
    [NET_AMOUNT] decimal(18,3)  NULL,
    [FREIGHT] decimal(18,3)  NULL,
    [PF] decimal(18,3)  NULL,
    [SGST] decimal(18,3)  NULL,
    [CGST] decimal(18,3)  NULL,
    [IGST] decimal(18,3)  NULL,
    [CESS] decimal(18,3)  NULL,
    [TOTAL_VALUE_INC] decimal(18,3)  NULL,
    [TOTAL_VALUE_EXC] decimal(18,3)  NULL,
    [STATUS] int  NULL,
    [CREATEDON] datetime  NULL,
    [ACC_REJ_BY] nvarchar(20)  NULL,
    [ACC_REJ_ON] datetime  NULL,
    [ACC_REJ_REMARKS] nvarchar(100)  NULL
);
GO

-- Creating table 'Schedules1'
CREATE TABLE [dbo].[Schedules1] (
    [REFNO] int  NULL,
    [SUPPLIER_CODE] varchar(100)  NULL,
    [PLANT_CODE] varchar(100)  NULL,
    [MAT_CODE] varchar(100)  NULL,
    [CONTRACT_NO] varchar(100)  NULL,
    [POSITION_NO] varchar(100)  NULL,
    [SCHEDULE_NO] varchar(100)  NULL,
    [QUANTITY] varchar(100)  NULL,
    [DEL_DATE] varchar(100)  NULL,
    [STATUS] varchar(20)  NULL,
    [ACC_REJ_REMARKS] varchar(500)  NULL,
    [Is_BillGenerated] varchar(1)  NOT NULL
);
GO

-- Creating table 'tblGRNs'
CREATE TABLE [dbo].[tblGRNs] (
    [ID] bigint IDENTITY(1,1) NOT NULL,
    [SupplierCode] varchar(20)  NULL,
    [SupplierNAme] varchar(200)  NULL,
    [MaterialCode] varchar(50)  NULL,
    [MaterialDescription] varchar(200)  NULL,
    [PlantCode] varchar(20)  NULL,
    [PlantName] varchar(200)  NULL,
    [PONO] varchar(20)  NULL,
    [MRNNo] varchar(20)  NULL,
    [InvoiceNo] varchar(20)  NULL,
    [InvoiceDate] datetime  NULL,
    [PstngDate] datetime  NULL,
    [Qty] decimal(18,0)  NULL,
    [UOM] varchar(20)  NULL,
    [CreatedOn] datetime  NULL,
    [Active] bit  NULL
);
GO

-- Creating table 'tblInvoiceRejecttions'
CREATE TABLE [dbo].[tblInvoiceRejecttions] (
    [ID] bigint IDENTITY(1,1) NOT NULL,
    [SupplierCode] varchar(50)  NULL,
    [SupplierName] varchar(150)  NULL,
    [PlantCode] varchar(50)  NULL,
    [PlantName] varchar(200)  NULL,
    [InvoiceNo] varchar(50)  NULL,
    [InvoiceDate] datetime  NULL,
    [HsnSac] varchar(50)  NULL,
    [MaterialCode] varchar(50)  NULL,
    [MaterialDescription] varchar(150)  NULL,
    [UOM] varchar(50)  NULL,
    [RejectedQty] varchar(50)  NULL,
    [BasicPrice] varchar(50)  NULL,
    [AccessiblePrice] varchar(50)  NULL,
    [TotalBasicPrice] varchar(50)  NULL,
    [CGST] varchar(50)  NULL,
    [SGST] varchar(50)  NULL,
    [IGST] varchar(50)  NULL,
    [TotalTax] varchar(50)  NULL,
    [TotalValue] varchar(50)  NULL,
    [CreatedOn] datetime  NOT NULL,
    [Active] bit  NOT NULL
);
GO

-- Creating table 'STATE_MST'
CREATE TABLE [dbo].[STATE_MST] (
    [StateId] int IDENTITY(1,1) NOT NULL,
    [STATE_CODE] varchar(50)  NULL,
    [STATE_NAME] varchar(100)  NULL,
    [ISACTIVE] varchar(1)  NULL
);
GO

-- Creating table 'MATERIAL_MST'
CREATE TABLE [dbo].[MATERIAL_MST] (
    [SLNO] bigint IDENTITY(1,1) NOT NULL,
    [MAT_CODE] varchar(100)  NULL,
    [MAT_DESC] varchar(200)  NULL,
    [UNIT_UOM] varchar(20)  NULL,
    [MAT_GROUP] varchar(100)  NULL,
    [SNP] int  NULL,
    [ISACTIVE] varchar(1)  NULL,
    [CREATED_BY] int  NULL,
    [CREATED_ON] datetime  NULL,
    [RELEASE_BY] int  NULL,
    [RELEASE_ON] datetime  NULL,
    [UPDATED_BY] int  NULL,
    [UPDATED_ON] datetime  NULL
);
GO

-- Creating table 'AspNetUserRoles'
CREATE TABLE [dbo].[AspNetUserRoles] (
    [AspNetRoles_Id] nvarchar(128)  NOT NULL,
    [AspNetUsers_Id] nvarchar(128)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [MigrationId], [ContextKey] in table 'C__MigrationHistory'
ALTER TABLE [dbo].[C__MigrationHistory]
ADD CONSTRAINT [PK_C__MigrationHistory]
    PRIMARY KEY CLUSTERED ([MigrationId], [ContextKey] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetRoles'
ALTER TABLE [dbo].[AspNetRoles]
ADD CONSTRAINT [PK_AspNetRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [PK_AspNetUserClaims]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [LoginProvider], [ProviderKey], [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [PK_AspNetUserLogins]
    PRIMARY KEY CLUSTERED ([LoginProvider], [ProviderKey], [UserId] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUsers'
ALTER TABLE [dbo].[AspNetUsers]
ADD CONSTRAINT [PK_AspNetUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [ID] in table 'INVOICEs'
ALTER TABLE [dbo].[INVOICEs]
ADD CONSTRAINT [PK_INVOICEs]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'INVOICE_DETAILS'
ALTER TABLE [dbo].[INVOICE_DETAILS]
ADD CONSTRAINT [PK_INVOICE_DETAILS]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [PlantId] in table 'Plant_Master'
ALTER TABLE [dbo].[Plant_Master]
ADD CONSTRAINT [PK_Plant_Master]
    PRIMARY KEY CLUSTERED ([PlantId] ASC);
GO

-- Creating primary key on [ID] in table 'POes'
ALTER TABLE [dbo].[POes]
ADD CONSTRAINT [PK_POes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'PO_DETAILS'
ALTER TABLE [dbo].[PO_DETAILS]
ADD CONSTRAINT [PK_PO_DETAILS]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Schedules'
ALTER TABLE [dbo].[Schedules]
ADD CONSTRAINT [PK_Schedules]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Id] in table 'SUPPLIER_MASTER'
ALTER TABLE [dbo].[SUPPLIER_MASTER]
ADD CONSTRAINT [PK_SUPPLIER_MASTER]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'tblApprovedPackingStandards'
ALTER TABLE [dbo].[tblApprovedPackingStandards]
ADD CONSTRAINT [PK_tblApprovedPackingStandards]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [CityId] in table 'tblCities'
ALTER TABLE [dbo].[tblCities]
ADD CONSTRAINT [PK_tblCities]
    PRIMARY KEY CLUSTERED ([CityId] ASC);
GO

-- Creating primary key on [Id] in table 'tblFileMetaDatas'
ALTER TABLE [dbo].[tblFileMetaDatas]
ADD CONSTRAINT [PK_tblFileMetaDatas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'tblLineStopages'
ALTER TABLE [dbo].[tblLineStopages]
ADD CONSTRAINT [PK_tblLineStopages]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [MaterialID] in table 'tblMaterials'
ALTER TABLE [dbo].[tblMaterials]
ADD CONSTRAINT [PK_tblMaterials]
    PRIMARY KEY CLUSTERED ([MaterialID] ASC);
GO

-- Creating primary key on [Id] in table 'tblPPMs'
ALTER TABLE [dbo].[tblPPMs]
ADD CONSTRAINT [PK_tblPPMs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'tblRatings'
ALTER TABLE [dbo].[tblRatings]
ADD CONSTRAINT [PK_tblRatings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [UnitId] in table 'tblUnits'
ALTER TABLE [dbo].[tblUnits]
ADD CONSTRAINT [PK_tblUnits]
    PRIMARY KEY CLUSTERED ([UnitId] ASC);
GO

-- Creating primary key on [Id] in table 'tblWarrantyPartsRejecteds'
ALTER TABLE [dbo].[tblWarrantyPartsRejecteds]
ADD CONSTRAINT [PK_tblWarrantyPartsRejecteds]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [UserId], [RoleId] in table 'tblUserRoles'
ALTER TABLE [dbo].[tblUserRoles]
ADD CONSTRAINT [PK_tblUserRoles]
    PRIMARY KEY CLUSTERED ([UserId], [RoleId] ASC);
GO

-- Creating primary key on [ID] in table 'VENDOR_ASSOC'
ALTER TABLE [dbo].[VENDOR_ASSOC]
ADD CONSTRAINT [PK_VENDOR_ASSOC]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'POLists'
ALTER TABLE [dbo].[POLists]
ADD CONSTRAINT [PK_POLists]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Is_BillGenerated] in table 'Schedules1'
ALTER TABLE [dbo].[Schedules1]
ADD CONSTRAINT [PK_Schedules1]
    PRIMARY KEY CLUSTERED ([Is_BillGenerated] ASC);
GO

-- Creating primary key on [ID] in table 'tblGRNs'
ALTER TABLE [dbo].[tblGRNs]
ADD CONSTRAINT [PK_tblGRNs]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tblInvoiceRejecttions'
ALTER TABLE [dbo].[tblInvoiceRejecttions]
ADD CONSTRAINT [PK_tblInvoiceRejecttions]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [StateId] in table 'STATE_MST'
ALTER TABLE [dbo].[STATE_MST]
ADD CONSTRAINT [PK_STATE_MST]
    PRIMARY KEY CLUSTERED ([StateId] ASC);
GO

-- Creating primary key on [SLNO] in table 'MATERIAL_MST'
ALTER TABLE [dbo].[MATERIAL_MST]
ADD CONSTRAINT [PK_MATERIAL_MST]
    PRIMARY KEY CLUSTERED ([SLNO] ASC);
GO

-- Creating primary key on [AspNetRoles_Id], [AspNetUsers_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [PK_AspNetUserRoles]
    PRIMARY KEY CLUSTERED ([AspNetRoles_Id], [AspNetUsers_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserClaims]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserLogins]
    ([UserId]);
GO

-- Creating foreign key on [AspNetRoles_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_AspNetUserRoles_AspNetRoles]
    FOREIGN KEY ([AspNetRoles_Id])
    REFERENCES [dbo].[AspNetRoles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [AspNetUsers_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_AspNetUserRoles_AspNetUsers]
    FOREIGN KEY ([AspNetUsers_Id])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetUserRoles_AspNetUsers'
CREATE INDEX [IX_FK_AspNetUserRoles_AspNetUsers]
ON [dbo].[AspNetUserRoles]
    ([AspNetUsers_Id]);
GO

-- Creating foreign key on [INV_ID] in table 'INVOICE_DETAILS'
ALTER TABLE [dbo].[INVOICE_DETAILS]
ADD CONSTRAINT [FK_INVOICE_DETAILS_INVOICE]
    FOREIGN KEY ([INV_ID])
    REFERENCES [dbo].[INVOICEs]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_INVOICE_DETAILS_INVOICE'
CREATE INDEX [IX_FK_INVOICE_DETAILS_INVOICE]
ON [dbo].[INVOICE_DETAILS]
    ([INV_ID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------