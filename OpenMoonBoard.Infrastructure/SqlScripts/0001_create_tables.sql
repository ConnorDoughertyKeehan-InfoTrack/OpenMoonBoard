CREATE TABLE Boards (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL
)

CREATE TABLE MoonBoardHoldSets (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    BoardId INT NOT NULL,
    Name NVARCHAR(30) NOT NULL,
    FOREIGN KEY (BoardId) REFERENCES Boards(Id)
)

CREATE TABLE Grades (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    VGrade NVARCHAR(7) NOT NULL,
    FontGrade NVARCHAR(7) NOT NULL
)

CREATE TABLE Routes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ActualMoonBoardId UNIQUEIDENTIFIER NULL,
    DateAdded DATETIMEOFFSET NOT NULL,
    Setter NVARCHAR(100),
    GradeId INT NOT NULL,
    BoardId INT NOT NULL,
    FOREIGN KEY (GradeId) REFERENCES Grades(Id),
    FOREIGN KEY (BoardId) REFERENCES Boards(Id)
)

CREATE TABLE MoonBoardHolds (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    MoonBoardHoldSetId INT NOT NULL,
    Position NVARCHAR(3) NOT NULL,
    FOREIGN KEY (MoonBoardHoldSetId) REFERENCES MoonBoardHoldSets(Id)
)

CREATE TABLE HoldsUsed (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    RouteId INT NOT NULL,
    MoonBoardHoldId INT NOT NULL,
    IsStartHold BIT NOT NULL,
    IsEndHold BIT NOT NULL,
    FOREIGN KEY (RouteId) REFERENCES Routes(Id),
    FOREIGN KEY (MoonBoardHoldId) REFERENCES MoonBoardHolds(Id)
)

CREATE TABLE MoonBoardLogs (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    DateAdded DATETIMEOFFSET NOT NULL,
    RouteId INT NOT NULL,
    LoginId INT NOT NULL,
    BetaVideoUrl NVARCHAR(2000),
    Attempts INT NULL,
    FeltLikeGradeId INT NOT NULL,
    FOREIGN KEY (RouteId) REFERENCES Routes(Id),
    FOREIGN KEY (FeltLikeGradeId) REFERENCES Grades(Id)
)
