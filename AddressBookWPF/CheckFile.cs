using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookWPF
{
    class CheckFile
    {
        private CheckFile() { }

        public static FileStream GetReadFileStream(string fileName, bool ignoreNotFound)
        {
            FileStream fs = null;
            try
            {
                fs = File.OpenRead(@fileName);
            }
            catch (DirectoryNotFoundException)
            {
                if (ignoreNotFound)
                {
                    Directory.CreateDirectory(Directory.GetParent(@fileName).ToString());
                    fs = new FileStream(@fileName, FileMode.OpenOrCreate);
                }
                else
                {
                    throw new DirectoryNotFoundException("Cannot find directory: " + fileName);
                }
            }
            catch (FileNotFoundException)
            {
                if (ignoreNotFound)
                {
                    fs = new FileStream(@fileName, FileMode.OpenOrCreate);
                }
                else
                {
                    throw new FileNotFoundException("Cannot find file: " + fileName);
                }
            }
            catch (UnauthorizedAccessException)
            {
                throw new UnauthorizedAccessException("Read access denied to file: " + fileName);
            }
            return fs;
        }

        public static FileStream GetWriteFileStream(string fileName)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(@fileName, FileMode.OpenOrCreate);
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory(Directory.GetParent(@fileName).ToString());
                fs = new FileStream(@fileName, FileMode.OpenOrCreate);
            }
            catch (UnauthorizedAccessException)
            {
                throw new UnauthorizedAccessException("Write access denied to file: " + fileName);
            }
            return fs;
        }
    }
}
