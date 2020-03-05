using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Hosting;
using System.Text;
using System.Threading.Tasks;

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;


namespace DAL
{
    public class Logg
    {
        public Logg()
        {

        }

        public void logg_til_change(String text)
        {
            var path = HostingEnvironment.ApplicationPhysicalPath + "\\logs\\Change.txt";

            string loggTekst = DateTime.Now.ToString() + ":  " + text+"\n";

            using (StreamWriter sr = File.AppendText(path))
            {
                sr.WriteLine(loggTekst);

                sr.Close();
            }

        }


        public void logg_til_exception(String text)
        {
            var path = HostingEnvironment.ApplicationPhysicalPath + "\\logs\\Exception.txt";

            string loggTekst = DateTime.Now.ToString() + ":  " + text + "\n";

            using (StreamWriter sr = File.AppendText(path))
            {
                sr.WriteLine(loggTekst);

                sr.Close();
            }

        }
    }
}
