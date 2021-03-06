﻿using System;
using System.IO;

namespace Expelibrum.Services
{
    public class FileMoverService : IFileMoverService
    {
        public void Move(string source, string[] destination)
        {
            if (!source.Equals(destination))
            {
                string ext = Path.GetExtension(source);
                string destinationString = Path.Combine(destination);
                destinationString += ext;

                Directory.CreateDirectory(Path.GetDirectoryName(destinationString));
                Directory.Move(source, destinationString);
            }
        }
    }
}
