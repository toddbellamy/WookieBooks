using System;

namespace WookieBooks.DomainFramework
{
    public record ImageFile
    {
        public string FileName { get; protected set; }
        public byte[] ImageData { get; protected set; }

        public ImageFile(string fileName, byte[] imageData)
        {
            CheckValidity(fileName, imageData);
            FileName = fileName;
            ImageData = imageData;
        }

        protected static void CheckValidity(string fileName, byte[] imageData)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("Filename value may not be empty.", nameof(fileName));

            if (imageData == null || imageData.Length == 0)
                throw new ArgumentException("ImageData value may not be empty.", nameof(imageData));
        }
    }
}