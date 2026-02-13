
-- CREATE DATABASE
IF DB_ID('ParkingLotDB') IS NULL
BEGIN
    CREATE DATABASE ParkingLotDB;
END
GO

-- Use the database
USE ParkingLotDB;
GO

-- TABLE:ParkedVehicles

IF OBJECT_ID('ParkedVehicles', 'U') IS NOT NULL
    DROP TABLE ParkedVehicles;
GO

CREATE TABLE ParkedVehicles
(
    VehicleNumber NVARCHAR(20) PRIMARY KEY,
    VehicleType NVARCHAR(10) NOT NULL,
    EntryTime DATETIME NOT NULL
);
GO

-- TABLE:ParkingHistory
IF OBJECT_ID('ParkingHistory', 'U') IS NOT NULL
    DROP TABLE ParkingHistory;
GO

CREATE TABLE ParkingHistory
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    VehicleNumber NVARCHAR(20),
    VehicleType NVARCHAR(10),
    EntryTime DATETIME,
    ExitTime DATETIME,
    FeePaid DECIMAL(10,2)
);
GO

-- STORED PROCEDURES
-- ParkVehicle

IF OBJECT_ID('ParkVehicle', 'P') IS NOT NULL
    DROP PROCEDURE ParkVehicle;
GO

CREATE PROCEDURE ParkVehicle
    @VehicleNumber NVARCHAR(20),
    @VehicleType NVARCHAR(10),
    @EntryTime DATETIME,
    @MaxCapacity INT
AS
BEGIN
    -- Check duplicate vehicle
    IF EXISTS (SELECT 1 FROM ParkedVehicles WHERE VehicleNumber = @VehicleNumber)
    BEGIN
        RAISERROR('Vehicle number already exists!', 16, 1)
        RETURN
    END

    -- Check parking capacity
    DECLARE @CurrentCount INT
    SELECT @CurrentCount = COUNT(*) FROM ParkedVehicles

    IF @CurrentCount >= @MaxCapacity
    BEGIN
        RAISERROR('Parking Full!', 16, 1)
        RETURN
    END

    -- Insert vehicle
    INSERT INTO ParkedVehicles (VehicleNumber, VehicleType, EntryTime)
    VALUES (@VehicleNumber, @VehicleType, @EntryTime)
END
GO

-- RemoveVehicle
IF OBJECT_ID('RemoveVehicle', 'P') IS NOT NULL
    DROP PROCEDURE RemoveVehicle;
GO

CREATE PROCEDURE RemoveVehicle
    @VehicleNumber NVARCHAR(20),
    @ExitTime DATETIME,
    @Fee DECIMAL(10,2)
AS
BEGIN
    DECLARE @Type NVARCHAR(10), @EntryTime DATETIME;

    -- Get vehicle details
    SELECT @Type = VehicleType, @EntryTime = EntryTime
    FROM ParkedVehicles
    WHERE VehicleNumber = @VehicleNumber;

    IF @Type IS NULL
    BEGIN
        RAISERROR('Vehicle not found!', 16, 1);
        RETURN;
    END

    -- Insert into history
    INSERT INTO ParkingHistory (VehicleNumber, VehicleType, EntryTime, ExitTime, FeePaid)
    VALUES (@VehicleNumber, @Type, @EntryTime, @ExitTime, @Fee);

    -- Remove from parked vehicles
    DELETE FROM ParkedVehicles
    WHERE VehicleNumber = @VehicleNumber;
END
GO
-- ViewParkedVehicles
IF OBJECT_ID('ViewParkedVehicles', 'P') IS NOT NULL
    DROP PROCEDURE ViewParkedVehicles;
GO

CREATE PROCEDURE ViewParkedVehicles
AS
BEGIN
    SELECT VehicleNumber, VehicleType, EntryTime
    FROM ParkedVehicles;
END
GO

--UpdateVehicleNumber
IF OBJECT_ID('UpdateVehicleNumber', 'P') IS NOT NULL
    DROP PROCEDURE UpdateVehicleNumber;
GO

CREATE PROCEDURE UpdateVehicleNumber
    @OldNumber NVARCHAR(20),
    @NewNumber NVARCHAR(20)
AS
BEGIN
    UPDATE ParkedVehicles
    SET VehicleNumber = @NewNumber
    WHERE VehicleNumber = @OldNumber;

    IF @@ROWCOUNT = 0
        RAISERROR('Vehicle not found!', 16, 1);
END
GO

--GetParked Vehicle Count
IF OBJECT_ID('GetParkedCount', 'P') IS NOT NULL
    DROP PROCEDURE GetParkedCount;
GO

CREATE PROCEDURE GetParkedCount
AS
BEGIN
    SELECT COUNT(*) AS CurrentCount FROM ParkedVehicles;
END
GO
