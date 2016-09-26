using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VidLec.DataClasses.Requests
{
    class GetPresentationsForFolderRequest
    {
        public bool IsViewPage { get; set; }
        public bool IsNewFolder { get; set; }
        public object AuthTicket { get; set; }
        public string CatalogId { get; set; }
        public string CurrentFolderId { get; set; }
        public string RootDynamicFolderId { get; set; }
        public int ItemsPerPage { get; set; }
        public int PageIndex { get; set; }
        public string PermissionMask { get; set; }
        public string CatalogSearchType { get; set; }
        public string SortBy { get; set; }
        public string SortDirection { get; set; }
        public object StartDate { get; set; }
        public object EndDate { get; set; }
        public object StatusFilterList { get; set; }
        public object PreviewKey { get; set; }
        public List<object> Tags { get; set; }
    }
}
