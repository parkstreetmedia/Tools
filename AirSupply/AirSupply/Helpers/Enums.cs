using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirSupply
{
    public class Enums
    {      
        public enum Drive
        {
            UNKNOWN,
            ON,
            OFF
        };

        public enum Mode
        {
            UNKNOWN,
            COOL,
            DRY,
            FAN, 
            HEAT,
            AUTO,
            AUTOHEAT,
            AUTOCOOL
        };

        public enum FanSpeed
        {
            UNKNOWN,
            HIGH,
            MID0,
            MID1,
            MID2,           
            LOW
        };

        public enum AirDirection
        {
            UNKNOWN,
            HORIZONTAL,
            VERTICAL,
            MID0,
            MID1,
            MID2,
            SWING
        };

        public enum FilterSign
        {
            UNKNOWN,
            ON,
            OFF
        };
    }
}
