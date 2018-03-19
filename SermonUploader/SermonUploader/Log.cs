using System;
using System.IO;

namespace SermonUploader {
    /// <summary>
    /// Write errors to a file, do so simply and cheaply by keeping the tiny file in memory and simply throwing strings at the end of it
    /// </summary>
    class Log {

        //The writer that is the error log
        StreamWriter writer;
        //The absolute path to the error log 
        String LogPath;

        /// <summary>
        /// Start up the log.
        /// </summary>
        /// <param name="path">The absolute path to the log directory, without a filename</param>
        public Log(String path) {
            this.LogPath = path + "\\" + (DateTime.Now.ToShortDateString().Replace("/", "-") + "_" + (DateTime.Now.ToShortTimeString()).Replace(":", ".") + ".txt").Replace(" ", "_");
            try {
                FileStream theStream = new FileStream(this.LogPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
                this.writer = new StreamWriter(theStream);
                this.writer.AutoFlush = true;
            }
            catch {
                //What, are we going to write out the error ? 
            }
        }
        
        /// <summary>
        /// Writes an line to the log
        /// </summary>
        /// <param name="msg">What you want written to the log</param>
        public void Write(String msg) {            
            this.writer.WriteLine(msg);
        }

        public void Close() {
            this.writer.Flush();
            this.writer.Close();
            this.writer.Dispose();
        }

        /// <summary>
        /// Let others know where I am 
        /// </summary>
        /// <returns></returns>
        public String Location() {
            return this.LogPath;
        }
    }
}