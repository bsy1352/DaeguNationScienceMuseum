using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace MultiRobots.Viewer
{
    class Config
    {
        /// <summary>
        /// get location from file
        /// 0 : 대구 과학관, 1 : 창원 전시회
        /// </summary>
        /// <returns></returns>
        public static int GetLocation()
        {
            int loc = 0;

            try
            {
                StringBuilder location = new StringBuilder();
                GetPrivateProfileString("ROBOT", "LOC", "0", location, 32, string.Format(@"{0}\robot.ini", System.Windows.Forms.Application.StartupPath));
                loc = int.Parse(location.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return loc;
        }

        /// <summary>
        /// Set location to file
        /// </summary>
        /// <param name="location"></param>
        public static void SetLocationStepToFile(int location)
        {
            WritePrivateProfileString("ROBOT", "LOC", location.ToString(), string.Format(@"{0}\robot.ini", System.Windows.Forms.Application.StartupPath));
        }

        /// <summary>
        /// set last step to file
        /// </summary>
        /// <param name="lastStep"></param>
        public static void SetLastStepToFile(int lastStep)
        {
            WritePrivateProfileString("ROBOT", "STEP", lastStep.ToString(), string.Format(@"{0}\robot.ini", System.Windows.Forms.Application.StartupPath));
        }

        /// <summary>
        /// get last step from file
        /// </summary>
        /// <returns></returns>
        public static int GetLastStepFromFile()
        {
            int step = 0;

            try
            {
                StringBuilder lastStep = new StringBuilder();
                GetPrivateProfileString("ROBOT", "STEP", "0", lastStep, 32, string.Format(@"{0}\robot.ini", System.Windows.Forms.Application.StartupPath));
                step = int.Parse(lastStep.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return step;
        }

        /// <summary>
        /// set loop count to file
        /// </summary>
        /// <param name="loopCount"></param>
        public static void SetLoopCountToFile(int loopCount)
        {
            WritePrivateProfileString("ROBOT", "LOOP", loopCount.ToString(), string.Format(@"{0}\robot.ini", System.Windows.Forms.Application.StartupPath));
        }

        /// <summary>
        /// get loop count from file
        /// </summary>
        /// <returns></returns>
        public static int GetLoopCount()
        {
            int loopCount = 0;

            try
            {
                StringBuilder lastLoop = new StringBuilder();
                GetPrivateProfileString("ROBOT", "LOOP", "0", lastLoop, 32, string.Format(@"{0}\robot.ini", System.Windows.Forms.Application.StartupPath));
                loopCount = int.Parse(lastLoop.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return loopCount;
        }


        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
    }
}
