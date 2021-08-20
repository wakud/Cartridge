using Cartridge.Data;
using ClosedXML.Excel;

namespace Cartridge.Utils
{
    public class Excel
    {
        private readonly XLWorkbook wb;
        private IXLWorksheet ws;
        private readonly MainContext _context;

        public Excel(MainContext context)
        {
            _context = context;
            wb = new XLWorkbook();
        }


    }
}
