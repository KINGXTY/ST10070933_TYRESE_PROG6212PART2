# ST10070933_TYRESE_PROG6212PART2
PART 2 for PROG6212

Once the project solution is open navigate to "appsettings.json" and replace the connection string with your own local database connection string.
run the command in the package cosole "add-migration" to add a new migration once that is done run the command "update-database"
For me personally i had to manually create my tables 
So on the database right-click>new query and run the following sql query 

CREATE TABLE Claims (
    Id INT IDENTITY(1,1) PRIMARY KEY, -- Auto-incrementing primary key
    LecturerId INT NOT NULL,           -- Required field for LecturerId
    HoursWorked DECIMAL(10, 2) NOT NULL CHECK (HoursWorked >= 0.1 AND HoursWorked <= 1000), -- Range validation
    HourlyRate DECIMAL(10, 2) NOT NULL CHECK (HourlyRate >= 0.01 AND HourlyRate <= 1000),  -- Range validation
    Notes NVARCHAR(MAX),               -- Optional notes
    Status NVARCHAR(50) DEFAULT 'Pending', -- Status default to 'Pending'
    SubmissionDate DATETIME DEFAULT GETDATE() -- Automatically set the submission date
);

