using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveIO_Main
{
    /// <summary>
    /// This class is to keep track of songs 
    /// </summary>
    class SongItem
    {
        /// <summary>
        /// Directory of where the song is stored.
        /// </summary>
        public String directory
        {
            get;
            set;
        }
        /// <summary>
        /// The name of file
        /// </summary>
        public String fileName
        {
            get;
            set;
        }
        /// <summary>
        /// Constructor used to instantiate the properties
        /// </summary>
        /// <param name="dir">Directory of the file</param>
        /// <param name="fName">File name</param>
        public SongItem(String dir, String fName)
        {
            this.directory = dir;
            this.fileName = fName;
        }
        /// <summary>
        /// Returns the fileName only
        /// </summary>
        /// <returns>File name</returns>
        public override string ToString()
        {
            return fileName;
        }
    }
}
