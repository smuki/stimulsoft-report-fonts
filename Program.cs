using Stimulsoft.Report;
using Stimulsoft.Report.Components;
using Stimulsoft.Report.Export;
using System;
using System.Data;
using System.IO;

namespace Render_Report_in_the_Console_App
{
    class Program
    {
        static void Main(string[] args)
        {
            string sCurrentDirectory = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar.ToString();
            string ReportsDirectory = sCurrentDirectory + "Reports";

            string sFontLocation = sCurrentDirectory + @"fonts";
            // add all fonts to StiFontCollection
            if (Directory.Exists(sFontLocation))
            {
                Console.WriteLine("Loading User define fonts... in " + sFontLocation);

                foreach (string sFontFile in Directory.GetFiles(sFontLocation, "*.*"))
                {
                    if (File.Exists(sFontFile) && ".ttf,.otf,.ttc".IndexOf(Path.GetExtension(sFontFile))>=0)
                    {
                        Console.WriteLine("     "+sFontFile);
                        Stimulsoft.Base.StiFontCollection.AddFontFile(sFontFile, Path.GetFileNameWithoutExtension(sFontFile));
                    }
                }
            }
            Console.WriteLine("List all StiFontCollection FontFamilies... ");

            //show all
            foreach (var font in Stimulsoft.Base.StiFontCollection.GetFontFamilies())
            {
                Console.WriteLine("      "+font.Name);
            }
            Console.WriteLine("Loading report and data... ");

            var dataSet = new DataSet();
            dataSet.ReadXml($"{ReportsDirectory}/Data/Demo.xml");

            var report = new StiReport();
            report.Load($"{ReportsDirectory}/TwoSimpleLists.mrt");
            report.RegData(dataSet);

            Console.WriteLine("OK");
            Console.WriteLine("Rendering and exporting a report... ");

            var exportFilePath = $"{sCurrentDirectory}/TwoSimpleLists_{DateTime.Now.ToString("yyyy-dd-MM_HH-mm-ss")}.pdf";

            //ProcessFonts(report);//replace all fonts to chinese fonts

            report.Render(false);
            var settings = new StiPdfExportSettings();
            settings.CreatorString = "PDF Service";
            settings.UseUnicode = true;
            settings.EmbeddedFonts = true;
            settings.StandardPdfFonts = false;
            settings.PdfComplianceMode= StiPdfComplianceMode.A1;

            StiOptions.Engine.FullTrust = true;
            StiOptions.Export.Pdf.ReduceFontFileSize = true;
            if (Path.DirectorySeparatorChar.ToString() == @"\")
            {
                StiOptions.Export.Pdf.AllowImportSystemLibraries = true; //windows
            }
            else
            {
                StiOptions.Export.Pdf.AllowImportSystemLibraries = false; //linux
            }

            report.ExportDocument(StiExportFormat.Pdf, exportFilePath, settings);

            Console.WriteLine("OK");
            Console.WriteLine("Exported to:");
            Console.WriteLine(Path.GetFullPath(exportFilePath));
            Console.WriteLine("press any key to exit");
            Console.ReadLine();

        }
        public static StiReport ProcessFonts(StiReport report)
        {
            string sName = "Source Han Sans SC";//it's chinese fonts
            Console.WriteLine("change all Components Font to ."+ sName);
            try
            {
                foreach (StiComponent comp in report.GetComponents())
                {
                    IStiFont fnt = comp as IStiFont;
                    if (fnt != null)
                    {
                        fnt.Font = Stimulsoft.Base.Drawing.StiFontUtils.ChangeFontName(fnt.Font, sName);
                        Console.Write(".");
                    }
                }
            }
            catch (Exception ex)
            {

            }
            Console.WriteLine("");

            return report;
        }
    }
}
