using System;
using System.Collections.Generic;
using server_app.Database;

namespace server_app.Utils
{
    public class CodeGenerator
    {
        public static string GenerateSessionCode()
        {
            // Generate a unique session code (e.g., using GUID or random characters)
            return Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
        }
    }
}