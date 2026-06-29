using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QRcodeHelper
{
    public class ExcelFileGenerator<T>
    {
        private HSSFWorkbook workbook { get; set; }
        private ISheet sheet { get; set; }
        private ICellStyle headStyle { get; set; }
        private Type type;
        private PropertyInfo[] typePropertyInfo { get; set; }
        private List<T> dataToExport { get; set; }

        public ExcelFileGenerator(List<T> data)
        {
            Init();
            dataToExport = data;
        }

        private void Init()
        {
            workbook = new HSSFWorkbook();
            CreateSheet();
            CreateHeaderStyle();
            type = typeof(T);
            if (type != null)
            {
                typePropertyInfo = type.GetProperties();
            }
        }
        private void CreateSheet()
        {
            sheet = workbook.CreateSheet("导出数据");
            sheet.DefaultColumnWidth = 20;
            sheet.ForceFormulaRecalculation = true;
        }

        private void CreateHeaderStyle()
        {
            var headFont = workbook.CreateFont();
            headFont.IsBold = true;

            //标题列样式
            headStyle = workbook.CreateCellStyle();
            headStyle.Alignment = HorizontalAlignment.Center;
            headStyle.BorderBottom = BorderStyle.Thin;
            headStyle.BorderLeft = BorderStyle.Thin;
            headStyle.BorderRight = BorderStyle.Thin;
            headStyle.BorderTop = BorderStyle.Thin;
            headStyle.SetFont(headFont);

        }
        private void CreateHeader(params string[] headers)
        {
            var rowIndex = 0;
            int columnIndex = 0;
            var row = sheet.CreateRow(rowIndex);
            foreach (string header in headers)
            {
                var cell = row.CreateCell(columnIndex);
                cell.SetCellValue(header);
                cell.CellStyle = headStyle;
                columnIndex++;
            }
        }
        public MemoryStream ExportDataToExcel(Dictionary<string, string> dataMap, Dictionary<string, Func<int, string>> customMap = null)
        {
            string[] headers = dataMap.Select(m => m.Value).ToArray();
            CreateHeader(headers);

            for (int i = 1; i <= dataToExport.Count; i++)
            {
                var tempRow = sheet.CreateRow(i);
                T tempData = dataToExport[i - 1];

                for (int j = 0; j < dataMap.Count; j++)
                {
                    var tempCell = tempRow.CreateCell(j);

                    PropertyInfo tempPropertyInfo = typePropertyInfo.FirstOrDefault(m =>
                        m.Name.Equals(dataMap.ElementAt(j).Key, StringComparison.CurrentCultureIgnoreCase));
                    if (tempPropertyInfo == null)
                        continue;

                    var cellValue = tempPropertyInfo.GetValue(tempData)?.ToString();

                    if (customMap != null && customMap.ContainsKey(dataMap.ElementAt(j).Key))
                    {
                        Func<int, string> tempFunc = customMap[dataMap.ElementAt(j).Key];
                        cellValue = tempFunc(int.Parse(cellValue));
                    }

                    tempCell.SetCellValue(cellValue);
                }
            }
            for (int i = 0; i <= dataMap.Count; i++)
            {
                sheet.AutoSizeColumn(i);
            }
            var memoryStream = new MemoryStream();
            if (!workbook.IsWriteProtected)
            {
                workbook.Write(memoryStream);
            }

            return memoryStream;
        }
    }
}
