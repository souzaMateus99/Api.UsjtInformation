using USJT.Models.Enums;

namespace USJT.Models.Model
{
    public class Subject
    {
        public string Title { get; set; }
        public ClassHour ClassHour { get; set; }
        public string Classroom { get; set; }
        public Days ClassDay { get; set; }
    }
}