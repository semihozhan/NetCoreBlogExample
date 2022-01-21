using NetBlog.Shared.Utilities.Results.Abstract;
using NetBlog.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlog.Shared.Utilities.Results.Concreate
{
    public class DataResult<T> : IDataResult<T>
    {

        public DataResult(ResultStatus resultStatus,T data)
        {
            ResultStatus = resultStatus;
            Data = data;
        }
        public DataResult(ResultStatus resultStatus, string message, T data)
        {
            ResultStatus = resultStatus;
            Data = data;
            Message = Message;
        }
        public DataResult(ResultStatus resultStatus, string message,  T data, Exception exception)
        {
            ResultStatus = resultStatus;
            Data = data;
            Message = Message;
            Exception = exception;
        }
        public T Data { get; }

        public ResultStatus ResultStatus { get; }

        public string Message { get; }

        public Exception Exception { get; }
    }
}
