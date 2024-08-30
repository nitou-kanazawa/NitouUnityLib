using System;

namespace nitou.Networking {

    [Serializable]
    public class HttpResponseStatus{
        public int ok;
        public int error_code;
    }


    public abstract class HttpResponse {
        public HttpResponseStatus status;
    }
}
