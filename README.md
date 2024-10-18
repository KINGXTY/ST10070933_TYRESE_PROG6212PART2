# ST10070933_TYRESE_PROG6212PART2
PART 2 for PROG6212

Once the project solution is open navigate to "appsettings.json" and replace the connection string with your own local database connection string.
run the command in the package cosole "add-migration" to add a new migration once that is done run the command "update-database"
For me personally i had to manually create my tables 
So on the database right-click>new query and run the following sql query 


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
