using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VidLec
{
    [Serializable]
    public class Folder
    {
        public List<Folder> ChildFolders = new List<Folder>();

        public string Id { get; set; }
        public string ParentCatalogFolderId { get; set; }
        public string CatalogId { get; set; }
        public string DynamicFolderId { get; set; }
        public string DynamicFolderName { get; set; }
        public string RootDynamicFolderId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string DisplayOptions { get; set; }
        public string Shortcut { get; set; }
        public string IsReadOnly { get; set; }
        public string Owner { get; set; }
        public string IsStaticFolder { get; set; }
        public string IsDynamicFolder { get; set; }
        public string IsVirtualFolder { get; set; }
        public string IncludeSubFolders { get; set; }
        public string DatabaseId { get; set; }
        public string SecurityId { get; set; }
        public int Count { get
        {
            if (HasChilds())
                    return GetBaseCount();
            return 0;
        }
        }

        public bool HasChilds()
        {
            if (ChildFolders.Count > 0)
                return true;
            return false;
        }

        public bool IsParent(Folder parent)
        {
            if (parent.ChildFolders.Count > 0)
                foreach (Folder f in parent.ChildFolders)
                {
                    if (ParentCatalogFolderId == f.DynamicFolderId)
                        return true;
                }
            return false;
        }

        public int GetBaseCount()
        {
            return GetBaseList().Count;
        }

        public List<Folder> GetBaseList(bool reset = true, Folder parent = null)
        {
            if (reset)
                parent = this;
            List<Folder> baseList = new List<Folder>(); 
            if (parent.HasChilds())
            {
                foreach (Folder f in parent.ChildFolders)
                {
                    if (f.HasChilds())
                        baseList.AddRange(GetBaseList(false, f));
                    else
                        baseList.Add(f);
                }
            }
            return baseList;
        }

    }
}
