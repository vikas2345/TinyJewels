// -----------------------------------------------------------------------
// <copyright file=IFileSystem company="Siemens Technology Solutions">
// Copyright © Siemens Technology Solutions All rights reserved.
// </copyright>

using System;
namespace TinyJewel.Common
{
    public class PermissionHelper
    {
        public PermissionHelper()
        {
        }

        private static PermissionHelper instance;
        public static PermissionHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PermissionHelper();
                }
                return instance;
            }
        }
    }
}
