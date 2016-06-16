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
        public List<Folder> childFolders = new List<Folder>();

        public string id { get; set; }
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
        public int Count { get {
                if (hasChilds())
                    return getBaseCount();
                else
                    return 0;
            } }

        public bool hasChilds()
        {
            if (childFolders.Count > 0)
                return true;
            else
                return false;
        }

        public bool isParent(Folder parent)
        {
            if (parent.childFolders.Count > 0)
                foreach (Folder f in parent.childFolders)
                {
                    if (ParentCatalogFolderId == f.DynamicFolderId)
                        return true;
                }
            return false;
        }

        public int getBaseCount()
        {
            return getBaseList().Count;
        }

        public List<Folder> getBaseList(bool reset = true, Folder parent = null)
        {
            if (reset)
                parent = this;
            List<Folder> baseList = new List<Folder>(); 
            if (parent.hasChilds())
            {
                foreach (Folder f in parent.childFolders)
                {
                    if (f.hasChilds())
                        baseList.AddRange(getBaseList(false, f));
                    else
                        baseList.Add(f);
                }
            }
            return baseList;
        }

    }
}
