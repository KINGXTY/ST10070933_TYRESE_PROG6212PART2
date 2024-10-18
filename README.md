# ST10070933_TYRESE_PROG6212PART2
PART 2 for PROG6212



CREATE TABLE Claims (
    Id INT IDENTITY(1,1) PRIMARY KEY, -- Auto-incrementing primary key
    LecturerId INT NOT NULL,           -- Required field for LecturerId
    HoursWorked DECIMAL(10, 2) NOT NULL CHECK (HoursWorked >= 0.1 AND HoursWorked <= 1000), -- Range validation
    HourlyRate DECIMAL(10, 2) NOT NULL CHECK (HourlyRate >= 0.01 AND HourlyRate <= 1000),  -- Range validation
    Notes NVARCHAR(MAX),               -- Optional notes
    Status NVARCHAR(50) DEFAULT 'Pending', -- Status default to 'Pending'
    SubmissionDate DATETIME DEFAULT GETDATE() -- Automatically set the submission date
);
CREATE TABLE SupportingDocuments (
    Id INT IDENTITY(1,1) PRIMARY KEY,    -- Auto-incrementing primary key
    ClaimId INT NOT NULL,                -- Foreign key to Claims table
    FileName NVARCHAR(255) NOT NULL,     -- Name of the uploaded file
    FilePath NVARCHAR(MAX) NOT NULL,     -- Path where the file is stored
    ContentType NVARCHAR(100) NOT NULL,  -- Type of file (e.g., pdf, docx)
    FileSize BIGINT NOT NULL,            -- File size in bytes
    UploadDate DATETIME DEFAULT GETDATE(), -- Automatically set the upload date
    FOREIGN KEY (ClaimId) REFERENCES Claims(Id) -- Foreign key constraint linking to Claims table
);
These will create the tables needed for the database 

Then you run the the web application and it should work 
