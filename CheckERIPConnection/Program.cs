using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.NetworkInformation;
using System.Diagnostics;
using NLog;

namespace CheckERIPConnection
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger logger = LogManager.GetCurrentClassLogger();
            logger.Info("%%% START %%%");
            try
            {
                Configuration configApp = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None) as Configuration;
                Erip configErip = configApp.GetSection("Erip") as Erip;
                string systemDir = Environment.SystemDirectory;
                //bool flagEripAdapter = false, flagEripAdapterafterStart = false; ;
                //Console.WriteLine(systemDir);
                string pathApp = systemDir + "\\rasdial.exe";
                string argumentsErip = string.Format(" {0} {1} {2}", configErip.AdapterName, configErip.Login, configErip.Password);
                IEnumerable<NetworkInterface> interfaces = NetworkInterface.GetAllNetworkInterfaces();
                /*foreach (NetworkInterface nwi in interfaces)
                {
                    if (nwi.Name.Equals(configErip.AdapterName))
                    {
                        flagEripAdapter = true;
                        logger.Info("+++ WORKING +++");
                    }
                }*/
                if (!ChechUpAdapter(configErip.AdapterName))
                {
                    logger.Warn("--- STOPED ---");
                    Process rasdial = new Process();
                    rasdial.StartInfo.FileName = pathApp;
                    rasdial.StartInfo.Arguments = argumentsErip;
                    Console.WriteLine(pathApp + argumentsErip);
                    if (rasdial.Start())
                    {
                        logger.Info("+++ RASDIAL STARTED +++");
                    }
                    else
                        logger.Warn("--- RASDIAL HAS NOT STARTED ---");

                }
                else
                    logger.Info("+++ WORKING +++");
                

            }
            catch (Exception ex)
            {
                //Console.WriteLine("!!! {0} !!!", ex.Message);
                logger.Error(ex.Message);
                logger.Trace(ex.StackTrace);
            }
            finally {
                logger.Info("%%% FINISH %%%");
            }
            Console.ReadKey();
        }

        private static bool ChechUpAdapter(string nameAdapter) {
            bool flagUpNetworkAdapter = false;
            IEnumerable<NetworkInterface> interfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface intrface in interfaces) {
                if (intrface.Name.Equals(nameAdapter))
                    flagUpNetworkAdapter = true;
            }
            return flagUpNetworkAdapter;
        }
    }
}
