*Thank you for your interest in contributing*

I built this blog engine primarily for educational purposes but if you would like to tackle an [open issue](https://github.com/ByteBlast/Medium/issues) then you should feel welcome to do so.

## Setting up the database

 1. Open the [**Server Explorer**](http://www.codeguru.com/columns/vb/the-server-explorer-in-visual-studio.html) pane
 2. Right click the **Data Connections** node then press the **Add Connection..** menu item
 3. In the **Server name** field enter "(localdb)\v11.0" and in the  **Select or enter a database name** field enter "Medium"
 5. Press the **Ok** button. 
 6. Right click the **..Medium.dbo** node and press the **New Query** menu button
 7. Copy and paste the schema from [here](https://github.com/ByteBlast/Medium/blob/master/MediumDomainModel/MediumSchema.sql) and paste it in the query editor. Make sure to remove the first two statements from the script (Create Database [..] Go)
 8. Run the query and you are done. 