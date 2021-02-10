// -----------------------------------------------------------------------
// <copyright file=IFileSystem company="Siemens Technology Solutions">
// Copyright © Siemens Technology Solutions All rights reserved.
// </copyright>

using System;
namespace TinyJewel.Common
{
    public class StorageContainer
    {
        public StorageContainer()
        {
        }

        private static StorageContainer instance;
        public static StorageContainer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StorageContainer();
                }
                return instance;
            }
        }

        public object UserType { get; set; }
    }
}
