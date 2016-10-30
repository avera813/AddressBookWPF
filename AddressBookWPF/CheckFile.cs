using System;
using System.IO;

namespace AddressBookWPF
{
    class CheckFile
    {
        private CheckFile() { }

        public static FileStream GetReadFileStream(string fileName)
        {
            FileStream fs = null;
            try
            {
                fs = File.OpenRead(@fileName);
            }
            catch (DirectoryNotFoundException)
            {
                throw new DirectoryNotFoundException("Cannot find directory: " + fileName);
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException("Cannot find file: " + fileName);
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
                fs = new FileStream(@fileName, FileMode.Create);
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
