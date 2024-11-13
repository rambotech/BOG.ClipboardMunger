using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Helper;
using BOG.ClipboardMunger.Common.Interface;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
    public class XSLtoXML : ClipboardMungerProviderBase, IClipboardMungerProvider
    {
        public override string MethodName { get => "XSL format of XML"; }
        public override string GroupName { get => "Transform"; }
        public override string Description { get => "XML and XSL from clipboard or filename specified on clipboard, when prompted."; }

        public XSLtoXML() : base()
        {
            base.SetArgument(new Argument
            {
                Name = "XML",
                Title = "The XML document or full file name of source file to read",
                DefaultValue = string.Empty,
                Description = string.Empty,
                ValidatorRegex = @".+"
            });
            base.SetArgument(new Argument
            {
                Name = "XSL",
                Title = "The XSL style-sheet document or full file name of source file to read",
                DefaultValue = string.Empty,
                Description = string.Empty,
                ValidatorRegex = @".+"
            });
        }

        public override string Munge(string textToMunge)
        {
            var xmlSource = base.GetArgumentValue("XML");
            Stream xmlStream = null;
            if (File.Exists(xmlSource))
            {
                xmlStream = new FileStream(xmlSource, FileMode.Open, FileAccess.Read);
            }
            else
            {
                xmlStream = new StringStream(xmlSource);
            }
            XPathDocument myXPathDoc = new XPathDocument(xmlStream);

            var xslSource = base.GetArgumentValue("XSL");
            XmlDocument xsl = new XmlDocument();
            if (File.Exists(xslSource))
            {
                xsl.Load(xslSource);
            }
            else
            {
                xsl.LoadXml(xslSource);
            }

            XslCompiledTransform myXslTrans = new XslCompiledTransform();
            myXslTrans.Load(xsl);
            var tempOutput = Path.GetTempFileName();
            XmlTextWriter myWriter = new XmlTextWriter(tempOutput, null);
            myXslTrans.Transform(myXPathDoc, null, myWriter);

            var output = new StringBuilder();
            using (var sr = new StreamReader(tempOutput))
            {
                output.Append(sr.ReadToEnd());
            }
            File.Delete(tempOutput);
            return output.ToString();
        }
    }
}
