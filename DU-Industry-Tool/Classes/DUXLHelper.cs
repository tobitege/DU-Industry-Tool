using ClosedXML.Excel;

namespace DU_Helpers
{
    public static class DUXLHelper
    {
        private static readonly string XL_int_format = "#,##0";
        private static readonly string XL_num_format = "#,##0.00";

        public static void XCellFmtDec(this IXLWorksheet ws, int row, int col)
        {
            ws.Cell(row, col).Style.NumberFormat.Format = XL_num_format;
        }

        public static void XCellFmtInt(this IXLWorksheet ws, int row, int col)
        {
            ws.Cell(row, col).Style.NumberFormat.Format = XL_int_format;
        }

        public static void XRangeFmtDec(this IXLWorksheet ws, int row, int col, int row2, int col2)
        {
            ws.Range(row, col, row2, col2).Style.NumberFormat.Format = XL_num_format;
        }

        public static void XRangeFmtInt(this IXLWorksheet ws, int row, int col, int row2, int col2)
        {
            ws.Range(row, col, row2, col2).Style.NumberFormat.Format = XL_int_format;
        }

        public static void XRangeBold(this IXLWorksheet ws, int row, int col, int row2, int col2)
        {
            ws.Range(row, col, row2, col2).Style.Font.SetBold();
        }

        public static void XRangeRight(this IXLWorksheet ws, int row, int col, int row2, int col2)
        {
            ws.Range(row, col, row2, col2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
        }

        public static void XA1(this IXLWorksheet ws, int row, int col, string formula)
        {
            ws.Cell(row, col).FormulaA1 = formula;
        }

        public static void XR1C1(this IXLWorksheet ws, int row, int col, string formula)
        {
            ws.Cell(row, col).FormulaR1C1 = formula;
        }

        public static void XMergeCells(this IXLWorksheet ws, int row, int col, int mergeCnt)
        {
            if (mergeCnt < 1) return;
            var cell = ws.Cell(row, col);
            ws.Range(cell.Address, cell.CellRight(mergeCnt).Address).Merge();
        }

        public static void XSetCellSum(this IXLWorksheet ws, ref int row, ref int col, int pStart, ref char letter)
        {
            col++;
            letter = Utils.LetterByIndex(col);
            ws.Cell(row, col).FormulaA1 = $"=SUM({letter}{pStart}:{letter}{row - 1})";
        }

        public static void CellInc(this IXLWorksheet ws, int row, ref int col, XLCellValue val,
                                    int merge = 0, bool inBold = false,
                                    string numFormat = null, string comment = null)
        {
            col++;
            CellSet(ws, row, col, val, merge, inBold, numFormat, comment);
        }
        
        public static void CellSet(this IXLWorksheet ws, int row, int col, XLCellValue val,
                                    int merge = 0, bool inBold = false,
                                    string numFormat = null, string comment = null)
        {
            var cell = ws.Cell(row, col);
            XMergeCells(ws, row, col, merge);
            cell.SetValue(val);
            if (inBold)
            {
                cell.Style.Font.SetBold();
            }
            if (numFormat != null)
            {
                cell.Style.NumberFormat.Format = numFormat;
            }
            if (!string.IsNullOrEmpty(comment))
            {
                cell.CreateComment().AddText(comment);
            }
        }
    }
}