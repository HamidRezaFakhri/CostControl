using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;

namespace CostControl.API.Models
{
    public class ServiceResponse<TEntity>
    {
        public ServiceResponse()
        {
            data = null;
            result = false;
            messages = null;
            totalRowCount = 0;
            pageSize = 0;
            currentPage = 0;
            totalPage = 0;
            sortOrder = string.Empty;
            sortDirection = string.Empty;
            searchKey = string.Empty;
            hasPreviousPage = false;
            hasNextPage = false;
        }

        public HttpStatusCode statusCode { get; set; }

        public IEnumerable<TEntity> data;

        public bool result { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string[] messages { get; set; }

        public int totalRowCount { get; set; }

        public int pageSize { get; set; }

        public int currentPage { get; set; }

        public int totalPage { get; set; }

        public string sortOrder { get; set; }

        public string sortDirection { get; set; }
        
        public string searchKey { get; set; }

        public bool hasPreviousPage { get; set; }

        public bool hasNextPage { get; set; }

        //public override string ToString()
        //{
        //}
    }
}