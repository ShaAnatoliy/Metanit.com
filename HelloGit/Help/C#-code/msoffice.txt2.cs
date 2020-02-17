private static void Excel(string fileName, List<IDirectoryInventoryDataCollector> list)
{
    try
    {
        var xlApp = new Excel.Application();
        var xlWorkBook = xlApp.Workbooks.Add();
        var xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
        ExcelTitleRow(list[0], 1, xlWorkSheet);

        int row = 2;
        foreach (var item in list)
        {
            ExcelFillRow(item, row++, xlWorkSheet);
        }

        for (int i = 1; i < list[0].MaxLevel - 1; i++)
        {
            ((Range)xlWorkSheet.Columns[i]).ColumnWidth = 2;
        }
        ((Range)xlWorkSheet.Columns[list[0].MaxLevel - 1]).ColumnWidth = 30;
        ((Range)xlWorkSheet.Rows[1]).WrapText = true;
        ((Range)xlWorkSheet.Rows[1]).HorizontalAlignment = HorizontalAlignment.Center;
        ((Range)xlWorkSheet.Cells[1, 1]).WrapText = false;

        xlWorkBook.SaveAs(fileName);
        xlWorkBook.Close();
        xlApp.Quit();
    }
    catch (AccessViolationException)
    {
        System.Windows.Forms.MessageBox.Show(
             "Have encountered access violation. This could be issue with Excel 2000 if that is only version installed on computer",
             "Access Violation");
    }
    catch (Exception)
    {
        System.Windows.Forms.MessageBox.Show("Unknown error",
             "Unknown error");
    }
}

private static void ExcelFillRow(IDirectoryInventoryDataCollector item, int row, Excel.Worksheet sheet)
{
    sheet.Cells[row, item.Level] = item.Name;
    int column = item.MaxLevel;
    foreach (var property in item.GetProperties())
    {
        sheet.Cells[row, column++] = property;
    }
}

private static void ExcelTitleRow(IDirectoryInventoryDataCollector item, int row, Excel.Worksheet sheet)
{
    sheet.Cells[row, 1] = "Name";
    int column = item.MaxLevel;
    foreach (var property in item.GetPropertyNames())
    {
        sheet.Cells[row, column++] = property;
    }
}


------------------------

object sheet = ExcelApp.ActiveSheet;   
if (sheet is Excel.Worksheet) {  
  Excel.Worksheet wks = sheet as Excel.Worksheet;  
  object selection = ExcelApp.Selection;   
  if (selection is Excel.Range) {   
    Excel.Range range = selection as Excel.Range;   
    Excel.Names names = wks.Names; 
    Excel.Name name = names.Add("MyName", range);  
    Marshal.ReleaseComObject(names); names = null; 
    Marshal.ReleaseComObject(name); name = null; 
  }  
  if (selection != null) Marshal.ReleaseComObject( selection); selection = null; 
}  
if (sheet != null) Marshal.ReleaseComObject(sheet); sheet = null; 

