CREATE TABLE Boards (
    Id INT IDENTITY(1,1),
    BoardName NVARCHAR(50)
)

CREATE TABLE Grades (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    VGrade NVARCHAR(7),
    FontGrade NVARCHAR(7),
)

CREATE TABLE Routes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ActualMoonBoardId UNIQUEIDENTIFIER NULL,
    DateAdded DATETIMEOFFSET,
    Setter NVARCHAR(100),
    GradeId INT NOT NULL,
    BoardId INT NOT NULL
)

CREATE TABLE MoonboardHolds (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    BoardId INT NOT NULL,
    Position NVARCHAR(3) NOT NULL
)

CREATE TABLE HoldsUsed (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    RouteId INT NOT NULL,
    MoonboardHoldId INT NOT NULL,
    IsStartHold BIT NOT NULL,
    IsEndHold BIT NOT NULL
)

CREATE TABLE MoonboardLogs (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    DateAdded DATETIMEOFFSET,
    RouteId INT NOT NULL,
    LoginId INT NOT NULL,
    BetaVideoUrl NVARCHAR(2000),
    Attempts INT NULL
)