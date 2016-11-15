# sql-generator
Allows to extract stored procedures from a database

# Pre-requisites
You must have SQL Server Management Studio installer on the same computer. The program use sqlcmd.exe.

# How to operate :
- store your connection strings into app.config
- select the source environment (ex: DEV)
- load all stored procedures
- select all of juste some of these
- choose 'extract all stored procedures into one file' or 'extract each stored procedures into a separate file'
- do the same thing for the target environment
- merge each stored procedure 
- copy all merged procedures into the target directory
- run execute with the right target environment 
- and pray ;)

# RTFM !
In french only ;)

# FYI
Don't judge me about this program, it's a very old piece of code ;) 