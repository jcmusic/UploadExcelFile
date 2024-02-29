# UploadExcelFile

Upload an Excel (.xlxs) file to an API and convert the worksheet content to a generic List.  
This utilizes a generic method to populate a list of any class.

Just create a POCO object with property names that match the names of column headers in the file.
The columns can be in any order though!

I've include 2 passing test files (different column order) and a failing file (wrong data type).
You can add error logging/handling based on your use case.
