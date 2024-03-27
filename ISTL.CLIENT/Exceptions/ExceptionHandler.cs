using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ISTL.RAB.Controllers;
using NLog;

namespace ISTL.RAB.Exceptions
{
    public class ExceptionHandler
    {
        public enum ExceptionStatus
        {
            SUCCESS,
            FAULT_EXCEPTION,
            OTHER_EXCEPTION
        }

        private static Logger logger = LogManager.GetCurrentClassLogger();
        private const string caption = "ISTL";
        private const string WEB_EXCEPTION = "System.Net.WebException";
        private const string TIMEOUT_EXCEPTION = "System.TimeoutException";
        private const string ENDPOINT_NOT_FOUND_EXCEPTION = "System.ServiceModel.EndpointNotFoundException";

        public static ExceptionStatus HandleExceptions(System.Exception x, string webMessage, string timeoutMessage)
        {
            switch (x.GetType().FullName)
            {
                case WEB_EXCEPTION:
                    logger.Debug("Connection problems. This is not serious." + x.Message);
                    MessageBoxController.ShowError(caption, webMessage);
                    return ExceptionStatus.SUCCESS;
                case TIMEOUT_EXCEPTION:
                    logger.Error("Connection timed out!\n" + x.ToString());
                    MessageBoxController.ShowError(caption, timeoutMessage);
                    return ExceptionStatus.SUCCESS;
                default:
                    return ExceptionStatus.OTHER_EXCEPTION;
            }
        }

        public static ExceptionStatus HandleExceptions(System.Exception x, string webMessage, string timeoutMessage, string endpointMessage)
        {
            switch (x.GetType().FullName)
            {
                case WEB_EXCEPTION:
                    logger.Debug("Connection problems. This is not serious." + x.Message);
                    MessageBoxController.ShowError(caption, webMessage);
                    return ExceptionStatus.SUCCESS;
                case TIMEOUT_EXCEPTION:
                    logger.Error("Connection timed out!\n" + x.ToString());
                    MessageBoxController.ShowError(caption, timeoutMessage);
                    return ExceptionStatus.SUCCESS;
                case ENDPOINT_NOT_FOUND_EXCEPTION:
                    logger.Debug("EndPointNotFound!" + x.Message);
                    MessageBoxController.ShowError(caption, endpointMessage);
                    return ExceptionStatus.SUCCESS;
                default:
                    return ExceptionStatus.OTHER_EXCEPTION;
            }
        }
    }
}
