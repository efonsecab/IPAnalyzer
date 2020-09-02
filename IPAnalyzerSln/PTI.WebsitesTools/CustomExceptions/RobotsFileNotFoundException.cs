using System;
namespace PTI.WebsitesTools.CustomExceptions
{
    public class RobotsFileNotFoundException: Exception
    {
        public RobotsFileNotFoundException(string message,
            Exception innerException):base(message, innerException)
        {

        }
    }
}
