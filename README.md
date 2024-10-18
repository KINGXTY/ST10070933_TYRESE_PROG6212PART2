# ST10070933_TYRESE_PROG6212PART2
PART 2 for PROG6212

PROG6212 Part 2 - Setup Guide
Database Configuration
1. Connection String Setup

Open the project solution
Navigate to appsettings.json
Replace the existing connection string with your local database connection string

2. Database Migration
Run the following commands in the Package Manager Console:
powershellCopyadd-migration InitialMigration
update-database
3. Manual Table Creation
If needed, create the required tables manually by executing the following SQL queries:
Claims Table
    CopyCREATE TABLE Claims (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    LecturerId INT NOT NULL,
    HoursWorked DECIMAL(10, 2) NOT NULL 
    CHECK (HoursWorked >= 0.1 AND HoursWorked <= 1000),
    HourlyRate DECIMAL(10, 2) NOT NULL 
    CHECK (HourlyRate >= 0.01 AND HourlyRate <= 1000),
    Notes NVARCHAR(MAX),
    Status NVARCHAR(50) DEFAULT 'Pending',
    SubmissionDate DATETIME DEFAULT GETDATE()
);
Supporting Documents Table
    CREATE TABLE SupportingDocuments (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ClaimId INT NOT NULL,
    FileName NVARCHAR(255) NOT NULL,
    FilePath NVARCHAR(MAX) NOT NULL,
    ContentType NVARCHAR(100) NOT NULL,
    FileSize BIGINT NOT NULL,
    UploadDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (ClaimId) REFERENCES Claims(Id)
); 
Application Features
Submit Page
Allows employees to submit claims with the following details:

Lecturer ID
Hours Worked
Hourly Rate
Supporting Documents
Status

Review Page
Enables administrators to:

Review submitted claims
Approve or reject claims
View supporting documentation

Running the Application
After completing the setup steps above, run the web application. The system should now be fully functional with both submission and review capabilities.
