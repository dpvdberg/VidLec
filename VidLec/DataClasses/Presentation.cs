using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VidLec
{
    public class Presentation
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string PresentationRootId { get; set; }
        public string Description { get; set; }
        public string AirDateDisplay { get; set; }
        public string AirTimeDisplay { get; set; }
        public string FullStartDate { get; set; }
        public string FullEndDate { get; set; }
        public string TimeZoneId { get; set; }
        public string DurationDisplay { get; set; }
        public string PlayerUrl { get; set; }
        public string ModerateUrl { get; set; }
        public string SlideFormatUrl { get; set; }
        public string ThumbnailImageUrl { get; set; }
        public string PresenterImageUrl { get; set; }
        public string PresentationContentUploadUrl { get; set; }
        public string PresenterContentUploadUrl { get; set; }
        public string CardImageIsThumbnail { get; set; }
        public string StatusDisplay { get; set; }
        public string Status { get; set; }
        public string IsShortcut { get; set; }
        public string IsViewableOnDemand { get; set; }
        public string IsLive { get; set; }
        public string IsOnDemand { get; set; }
        public string SlideCount { get; set; }
        public string FolderId { get; set; }
        public string[] SupportingLinks { get; set; }
        public string[] Tags { get; set; }
        public string IsEditable { get; set; }
        public string IsPublishable { get; set; }
        public string CanModerate { get; set; }
        public string AllowPresentationDownload { get; set; }
        public string Available { get; set; }
        public string TimelineHits { get; set; }
        public int Views { get; set; }

        public List<Presenter> presenters { get; set; }
    }
}
